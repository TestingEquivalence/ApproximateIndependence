
Public Class clsRandomNumberGenerator
    'Inherits System.Random
    Private RndGenState As New alglib.hqrndstate() 'random generator state

    Sub New(seed1 As Double, seed2 As Double)
        'init state
        alglib.hqrndseed(seed1, seed2, RndGenState)
    End Sub

    Sub New(seeds() As Double)
        alglib.hqrndseed(seeds(0), seeds(1), RndGenState)
    End Sub

    Function SNV() As Double
        Return alglib.hqrndnormal(RndGenState)
    End Function
    Function SNV2() As Double()
        Dim x(1) As Double
        alglib.hqrndnormal2(RndGenState, x(0), x(1))
        Return x
    End Function
    Function SU() As Double
        Return alglib.hqrnduniformr(RndGenState)
    End Function
    Sub randomise()
        alglib.hqrndrandomize(Me.RndGenState)
    End Sub

    Function discrete(X() As Double) As Double
        Return alglib.hqrnddiscrete(Me.RndGenState, X, X.Length)
    End Function


End Class
