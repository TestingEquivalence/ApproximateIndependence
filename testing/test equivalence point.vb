
Module tests
#Region "universal point test"
    Public universalAsymptPointTest As Func(Of TestParameter, Double(), TestResult) =
        Function(prm As TestParameter, rp As Double()) As TestResult
            Dim vol As Double
            Dim res As New TestResult

            With prm
                vol = clsMultinomialDistribution.getAsymptoticVolatility(
                    rp, .q.get_pr, .derivative) / Math.Sqrt(.n)

                Dim crit As Double = .eps - alglib.invnormaldistribution(1 - .alpha) * vol

                Dim T As Double
                T = prm.test_statistic(rp, prm.q.get_pr)

                res.minEps = T + alglib.invnormaldistribution(1 - .alpha) * vol
                If T < crit Then
                    res.result = True
                Else
                    res.result = False
                End If
            End With
            Return res
        End Function

    Function universalBstPointTest(prm As TestParameter, rp() As Double) As Boolean

        'check if the value outside of the H0
        Dim T As Double
        T = prm.test_statistic(rp, prm.q.get_pr)

        If T > prm.eps Then
            Return False
        End If

        'define test statistics
        Dim BoolT As Func(Of Double(), Boolean)

        BoolT = Function(a() As Double) As Boolean
                    Return prm.test_statistic(a, prm.q.get_pr) < prm.eps
                End Function

        'calculate bootstrap p value
        Dim bst As New diskr_bootstrap(New clsMultinomialDistribution(rp),
                                       prm.n, BoolT, prm.reset, prm.nBstSamples)
        Dim p_val As Double
        p_val = bst.EmpProb

        If p_val <= prm.alpha Then
            Return True
        Else
            Return False
        End If
    End Function

    Public simplifiedBstPointTest As Func(Of TestParameter, Double(), Boolean) =
Function(prm As TestParameter, rp() As Double) As Boolean

    'check if the value outside of the H0
    Dim T As Double
    T = prm.test_statistic(rp, prm.q.get_pr)

    If T > prm.eps Then
        Return False
    End If

    'define test statistics
    Dim boolT As Func(Of Double(), Boolean)

    boolT = Function(a() As Double) As Boolean
                Return prm.test_statistic(a, prm.q.get_pr) > prm.eps
            End Function

    'calculate bootstrap p value
    Dim bst As New diskr_bootstrap(New clsMultinomialDistribution(rp),
                                   prm.n, boolT, prm.reset, prm.nBstSamples)
    Dim p_val As Double
    p_val = bst.EmpProb
    If p_val <= prm.alpha Then
        Return True
    Else
        Return False
    End If
End Function

#End Region
End Module
