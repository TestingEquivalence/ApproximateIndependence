Public Class minProduct
    Inherits DistanceMinimizer

    Dim n, m As Integer
    Sub New(n As Integer, m As Integer, dst As Func(Of Double(), Double(), Double))
        MyBase.New(dst)
        Me.n = n
        Me.m = m
    End Sub

    Function ProductMatrix(p As Double(), q As Double()) As Double(,)
        Dim f(n - 1, m - 1) As Double
        For i As Integer = 1 To n
            For j As Integer = 1 To m
                f(i - 1, j - 1) = p(i - 1) * q(j - 1)
            Next
        Next
        Return f

    End Function

    Function Vector2Matrix(v As Double()) As Double(,)
        Dim f(n - 1, m - 1) As Double
        For i As Integer = 1 To n
            For j As Integer = 1 To m
                f(i - 1, j - 1) = v((i - 1) * m + j - 1)
            Next
        Next
        Return f
    End Function
    Function Matrix2Vector(f As Double(,)) As Double()
        Dim v(n * m - 1) As Double
        For i As Integer = 1 To n
            For j As Integer = 1 To m
                v((i - 1) * m + j - 1) = f(i - 1, j - 1)
            Next
        Next
        Return v
    End Function

    Function StartPoint1(pq As Double()) As Double()
        Dim f As Double(,)
        f = Vector2Matrix(pq)
        Dim p(n - 1) As Double
        Dim i, j As Integer
        For i = 0 To n - 1
            p(i) = 0
            For j = 0 To m - 1
                p(i) = p(i) + f(i, j)
            Next
        Next
        Return p
    End Function


    Function StartPoint2(pq As Double()) As Double()
        Dim f As Double(,)
        f = Vector2Matrix(pq)
        Dim q(m - 1) As Double
        Dim i, j As Integer
        For j = 0 To m - 1
            q(j) = 0
            For i = 0 To n - 1
                q(j) = q(j) + f(i, j)
            Next
        Next
        Return q
    End Function

    Public Overrides Function StartValue(pq() As Double) As Double()
        Dim p, q As Double()
        Dim vec(n + m - 1) As Double
        p = StartPoint1(pq)
        q = StartPoint2(pq)

        Dim i As Integer
        For i = 0 To n - 1
            vec(i) = p(i)
        Next
        For i = n To n + m - 1
            vec(i) = q(i - n)
        Next
        Return Symplex2Space(vec)
    End Function

    Public Function getStartPoint(pq() As Double) As Double()
        Dim p, q As Double()
        p = StartPoint1(pq)
        q = StartPoint2(pq)

        Dim f(n - 1, m - 1) As Double
        For i As Integer = 1 To n
            For j As Integer = 1 To m
                f(i - 1, j - 1) = p(i - 1) * q(j - 1)
            Next
        Next

        Return Me.Matrix2Vector(f)
    End Function

    Public Overrides Function ResultValue(x() As Double) As Double()
        Dim p(n - 1), q(m - 1) As Double

        Dim i As Integer
        For i = 0 To n - 1
            p(i) = x(i)
        Next
        For i = n To n + m - 1
            q(i - n) = x(i)
        Next

        p = Space2Symplex(p)
        q = Space2Symplex(q)

        Dim pq() As Double
        pq = Matrix2Vector(ProductMatrix(p, q))

        Return pq
    End Function
End Class
