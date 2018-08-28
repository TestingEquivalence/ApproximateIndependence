Imports alglib
Public Class clsRandomNumberGenerator
    'Inherits System.Random
    Private RndGenState As New hqrndstate() 'random generator state

    Sub New(seed1 As Double, seed2 As Double)
        'init state
        hqrndseed(seed1, seed2, RndGenState)
    End Sub

    Sub New(seeds() As Double)
        hqrndseed(seeds(0), seeds(1), RndGenState)
    End Sub

    Function SNV() As Double
        Return hqrndnormal(RndGenState)
    End Function
    Function SNV2() As Double()
        Dim x(1) As Double
        hqrndnormal2(RndGenState, x(0), x(1))
        Return x
    End Function
    Function SU() As Double
        Return hqrnduniformr(RndGenState)
    End Function
    Sub randomise()
        hqrndrandomize(Me.RndGenState)
    End Sub

    Function discrete(X() As Double) As Double
        Return hqrnddiscrete(Me.RndGenState, X, X.Length)
    End Function


End Class
