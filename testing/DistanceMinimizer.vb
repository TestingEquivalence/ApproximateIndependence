
Public MustInherit Class DistanceMinimizer
    'p is always the probability measure
    'distance to which should be minimized
    MustOverride Function StartValue(p() As Double) As Double()
    MustOverride Function ResultValue(x() As Double) As Double()
    Public dst As Func(Of Double(), Double(), Double)
    Private p() As Double

    Friend eps As Double
    Friend lp, lq As Double()

    Friend bound As Double


    Sub New(dst As Func(Of Double(), Double(), Double))
        Me.dst = dst
    End Sub

#Region "helper"

    Function Symplex2Space(p() As Double) As Double()
        Dim i, n As Integer
        n = p.Length
        Dim x(n - 1) As Double

        For i = 0 To n - 1
            x(i) = Math.Log(p(i) + 1)
        Next
        Return x
    End Function

    Function Space2Symplex(x() As Double) As Double()
        Dim i, n As Integer
        n = x.Length
        Dim p(n - 1) As Double

        For i = 0 To n - 1
            p(i) = Math.Max(Math.Exp(x(i)) - 1, 0)
        Next

        Dim s As Double = sum(p)
        For i = 0 To n - 1
            p(i) = p(i) / s
        Next
        Return p
    End Function

    Function RandomPoint(d As Integer,
                       rnd As clsRandomNumberGenerator
                               ) As Double()
        Dim x(d - 1) As Double
        For i = 0 To d - 1
            x(i) = rnd.SU
        Next

        Return Me.Space2Symplex(x)
    End Function

    Function LinComb(v1 As Double(), v2 As Double(), alpha As Double) As Double()
        Dim lc(v1.Length - 1) As Double

        For i As Integer = 0 To v1.Length - 1
            lc(i) = v1(i) * alpha + v2(i) * (1 - alpha)
            If lc(i) < 0 Then lc(i) = 0
        Next

        Dim s As Double = sum(lc)
        For i As Integer = 0 To v1.Length - 1
            lc(i) = lc(i) / s
        Next

        Return lc
    End Function

#End Region

#Region "min distance"

    Function min_dist_val(p() As Double) As Double
        Dim q = min_dist(p)
        Return Me.dst(p, q)
    End Function

    Function min_dist(p() As Double) As Double()

        'initial conditions
        'x is not constrained to use
        'uncostrained minimisation
        Dim x() As Double
        x = StartValue(p)
        Me.p = p

        Dim state As alglib.minbleicstate = New alglib.minbleicstate() ' initializer can be dropped, but compiler will issue warning
        Dim rep As alglib.minbleicreport = New alglib.minbleicreport() ' initializer can be dropped, but compiler will issue warning

        '  These variables define stopping conditions for the optimizer.
        '  We use very simple condition - |g|<=epsg
        Dim epsg As Double = 0.000001
        Dim epsf As Double = 0
        Dim epsx As Double = 0
        Dim maxits As Integer = 0
        Dim diffstep As Double = 0.000001

        'run optimisation
        alglib.minbleiccreatef(x, diffstep, state)
        alglib.minbleicsetcond(state, epsg, epsf, epsx, maxits)
        alglib.minbleicoptimize(state, AddressOf aim, Nothing, state)
        alglib.minbleicresults(state, x, rep)

        Return ResultValue(x)
    End Function

    Public Sub aim(x() As Double, ByRef res As Double, obj As Object)
        Dim md() As Double
        md = ResultValue(x)

        res = Me.dst(Me.p, md)
    End Sub

    Function quick_min_dist(p() As Double, bound As Double) As Boolean

        'initial conditions
        'x is not constrained to use
        'uncostrained minimisation
        Dim x() As Double
        x = StartValue(p)
        Me.p = p
        Me.bound = bound

        Dim state As alglib.minbleicstate = New alglib.minbleicstate() ' initializer can be dropped, but compiler will issue warning
        Dim rep As alglib.minbleicreport = New alglib.minbleicreport() ' initializer can be dropped, but compiler will issue warning

        '  These variables define stopping conditions for the optimizer.
        '  We use very simple condition - |g|<=epsg

        Dim epsg As Double = 0.0001
        Dim epsf As Double = 0.0001
        Dim epsx As Double = 0.0001
        Dim maxits As Integer = 100
        Dim diffstep As Double = 0.0001

        'run optimisation
        alglib.minbleiccreatef(x, diffstep, state)
        alglib.minbleicsetcond(state, epsg, epsf, epsx, maxits)
        Try
            alglib.minbleicoptimize(state, AddressOf quick_aim, Nothing, state)
        Catch
            'forseeing
            Return True
        End Try
        alglib.minbleicresults(state, x, rep)

        Dim res As Double() = ResultValue(x)

        If Me.dst(Me.p, res) <= Me.bound Then
            Return True
        Else
            Return False
        End If
    End Function

    Sub quick_aim(x() As Double, ByRef res As Double, obj As Object)
        Me.aim(x, res, obj)
        If res <= Me.bound Then
            Throw New Exception("foreseeing")
        End If
    End Sub

#End Region

#Region "special points"

    Function LinearEpsPoint(p As Double(), q As Double(), eps As Double) As Double()
        Me.lp = p
        Me.lq = q
        Me.eps = eps

        'initial conditions
        'x is not constrained to use
        'uncostrained minimisation
        Dim x(0) As Double
        x(0) = 1


        Dim state As alglib.minbleicstate = New alglib.minbleicstate() ' initializer can be dropped, but compiler will issue warning
        Dim rep As alglib.minbleicreport = New alglib.minbleicreport() ' initializer can be dropped, but compiler will issue warning

        '  These variables define stopping conditions for the optimizer.
        '  We use very simple condition - |g|<=epsg
        Dim epsg As Double = 0.00001
        Dim epsf As Double = 0.00001
        Dim epsx As Double = 0.00001
        Dim maxits As Integer = 100
        Dim diffstep As Double = 0.00001


        'run optimisation
        alglib.minbleiccreatef(x, diffstep, state)
        alglib.minbleicsetcond(state, epsg, epsf, epsx, maxits)
        alglib.minbleicoptimize(state, AddressOf aim_lin_est_point, Nothing, Nothing)
        alglib.minbleicresults(state, x, rep)

        Dim lc() As Double
        lc = LinComb(Me.lp, Me.lq, x(0))
        Dim s As Double
        s = Me.dst(Me.min_dist(lc), lc)

        Return lc
    End Function

    Sub aim_lin_est_point(x() As Double, ByRef res As Double, obj As Object)
        Dim lc() As Double
        Dim s As Double
        lc = LinComb(Me.lp, Me.lq, x(0))

        s = Me.dst(Me.min_dist(lc), lc)

        res = Math.Pow((Me.eps - s), 2)
    End Sub

    Function RandomOuterPoint(lowBound As Double,
                              d As Integer,
                              rnd As clsRandomNumberGenerator) As Double()
        Dim res() As Double
        Dim max As Double = 0

        Do
            res = Me.RandomPoint(d, rnd)
        Loop Until (Me.min_dist_val(res) > lowBound)

        Return res
    End Function

    Function RandomBoundaryPoint(eps As Double,
                                 d As Integer,
                                 rnd As clsRandomNumberGenerator) As Double()
        Dim q = Me.RandomOuterPoint(eps, d, rnd)
        Dim p = Me.min_dist(q)
        Return Me.LinearEpsPointBis(p, q, eps)
    End Function


    Public Function RandomLinBoundary(eps As Double,
                                      p() As Double,
                                   rnd As clsRandomNumberGenerator,
                                   dst As Func(Of Double(), Double(), Double)) As Double()
        Dim q As Double()
        Dim d As Integer = p.Length

        q = Me.RandomPoint(d, rnd)

        Do While dst(q, Me.min_dist(q)) <= eps
            q = Me.RandomPoint(d, rnd)
        Loop

        Dim f As Func(Of Double, Double)
        f = Function(alpha)
                Dim lc As Double()
                lc = Me.LinComb(q, p, alpha)
                Dim r As Double
                r = dst(lc, Me.min_dist(lc))
                Return r
            End Function

        Dim s As Double
        s = optimisation.bisection(f, eps, 0, 1)

        q = LinComb(q, p, s)

        s = dst(q, Me.min_dist(q))

        Return q
    End Function

    Function LinearEpsPointBis(low As Double(), high As Double(), eps As Double) As Double()
        Dim f As Func(Of Double, Double)
        f = Function(a As Double)
                Dim gues = LinComb(high, low, a)
                Return Me.min_dist_val(gues)
            End Function

        Dim alpha = optimisation.bisection(f, eps, 0, 1)
        Return LinComb(high, low, alpha)
    End Function

#End Region

End Class
