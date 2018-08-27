Module CompositeMultinomial
    Public boolWelleksTest As Func(Of TestParameter, Double(), Boolean) =
        Function(prm As TestParameter, rp() As Double) As Boolean
            Dim res = WelleksTest(prm, rp)
            Return res.result
        End Function

    Private WelleksTest As Func(Of TestParameter, Double(), TestResult) =
    Function(prm As TestParameter, rp() As Double) As TestResult
        With prm

            Dim mprod As New minProduct(.n1, .n2, l22)
            Dim q_pr = mprod.getStartPoint(rp)
            .q = New clsMultinomialDistribution(q_pr)

            'Dim dmin = mprod.min_dist_val(rp)
            Dim dw = l22(q_pr, rp)
            .opt = mprod
        End With

        Dim res = tests.universalAsymptPointTest(prm, rp)
        Return res
    End Function

    Public bool_comp_asymp_universal As Func(Of TestParameter, Double(), Boolean) =
        Function(prm As TestParameter, rp() As Double) As Boolean
            Dim res = comp_asymp_universal(prm, rp)
            Return res.result
        End Function

    Public comp_asymp_universal As Func(Of TestParameter, Double(), TestResult) =
        Function(prm As TestParameter, rp() As Double) As TestResult
            Dim res As New TestResult

            'distance
            Dim q_opt() As Double

            Try
                q_opt = prm.opt.min_dist(rp)
            Catch
                res.result = False
                res.minEps = 0
                Return res
            End Try

            'save q
            Dim save_q = prm.q
            prm.q = New clsMultinomialDistribution(q_opt)

            'test as a singular previous
            res = tests.universalAsymptPointTest(prm, rp)
            'restore q
            prm.q = save_q
            Return res
        End Function

    Public simply_bst As Func(Of TestParameter, Double(), Boolean) =
        Function(prm As TestParameter, rp() As Double) As Boolean
            'distance
            Dim q_opt() As Double
            q_opt = prm.opt.min_dist(rp)
            'save q
            Dim save_q = prm.q
            prm.q = New clsMultinomialDistribution(q_opt)

            'test as previous
            Dim res = tests.simplifiedBstPointTest(prm, rp)
            'restore q
            prm.q = save_q
            Return res
        End Function



    Public composite_bst_multidirectional As Func(Of TestParameter, Double(), Boolean) =
        Function(prm As TestParameter, rp() As Double) As Boolean

            'check if the value outside of the H0
            Dim bound As Double
            bound = prm.opt.min_dist_val(rp)

            If bound > prm.eps Then
                Return False
            End If

            'find linear nearest boundary point
            Dim p_lin_est As Double() = Nothing

            'go trought all external points
            Dim min_dist As Double = 1
            For Each exp In prm.ex_point
                'bisektion, do not fail and  also quick
                Dim lin_p = prm.opt.LinearEpsPointBis(rp, exp, prm.eps)
                'closer rand point is found
                'reset the values
                If prm.opt.dst(lin_p, rp) < min_dist Then
                    min_dist = prm.opt.dst(lin_p, rp)
                    p_lin_est = lin_p
                End If
            Next


            'define test statistics
            Dim BoolT As Func(Of Double(), Boolean)

            BoolT = Function(a() As Double)
                        'find nearest  p and q
                        Dim q As Boolean
                        q = prm.opt.quick_min_dist(a, bound)
                        Return q
                    End Function

            'calculate bootstrap p value
            Dim bst As New diskr_bootstrap(New clsMultinomialDistribution(p_lin_est),
                                           prm.n, BoolT, prm.reset, prm.nBstSamples)
            Dim p_val As Double
            p_val = bst.EmpProb

            If p_val <= prm.alpha Then
                Return True
            Else
                Return False
            End If
        End Function

    Public Function _WelleksTest(p As Double(,), n As Integer, alpha As Double, epsilon As Double) As TestResult
        Dim parameter As New TestParameter

        With parameter
            .test_statistic = l22
            .derivative = l22_derivative
            .n1 = p.GetUpperBound(0) + 1
            .n2 = p.GetUpperBound(1) + 1

            Dim mprod As New minProduct(.n1, .n2, .test_statistic)
            .p = New clsMultinomialDistribution(mprod.Matrix2Vector(p))
            Dim q_pr = mprod.min_dist(.p.get_pr())
            .q = New clsMultinomialDistribution(q_pr)
            .eps = epsilon * epsilon
            .n = n
            .nBstSamples = 1 * 1000
            .alpha = alpha
            .opt = mprod
            .a = 0.001
        End With

        Dim res = CompositeMultinomial.WelleksTest(parameter, parameter.p.get_pr())
        res.minEps = Math.Sqrt(res.minEps)
        Return res
    End Function

End Module
