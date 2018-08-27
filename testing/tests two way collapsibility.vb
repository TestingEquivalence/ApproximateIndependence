Public Module tests_two_way_collapsibility
    Public Class TestResult
        Public result As Boolean
        Public minEps As Double
    End Class

    ''' <summary>
    ''' The asymptotic test is based on the asymptotic distribution of the test statistic. 
    ''' Therefore the asymptotic test need some sufficiently large number of the observations.
    '''  It should be used carefully because the test is approximate 
    ''' and may be anti conservative at some points. 
    ''' In order to obtain a conservative test reducing of alpha  (usually halving) or
    '''  slight shrinkage of the tolerance parameter epsilon may be appropriate. 
    ''' </summary>
    ''' <param name="p">two way contingency table</param>
    ''' <param name="n">number of observations</param>
    ''' <param name="alpha">significance level</param>
    ''' <param name="epsilon">tolerance parameter</param>
    ''' <returns>
    ''' It returns the test result, which is true if the test can reject H_0. 
    ''' Additionally it returns the smallest epsilon 
    ''' for which test can reject H_0.
    ''' </returns>
    ''' <remarks></remarks>

    Public Function asymptoticTest(p As Double(,), n As Integer, alpha As Double, epsilon As Double) As TestResult
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

        Dim res = CompositeMultinomial.comp_asymp_universal(parameter, parameter.p.get_pr())
        res.minEps = Math.Sqrt(res.minEps)
        Return res
    End Function

    ''' <summary>
    ''' The bootstrap test is based on the re-sampling method called bootstrap. 
    ''' The bootstrap test is more precise and reliable than the asymptotic test. 
    ''' However, it should be used carefully because the test is approximate 
    ''' and may be anti conservative at some points. 
    ''' In order to obtain a conservative test reducing of alpha
    ''' (usually halving) or slight shrinkage of the tolerance parameter epsilon
    ''' may be appropriate. We prefer the slight shrinkage of the tolerance parameter 
    ''' because it is more effective and the significance level remains unchanged.
    ''' </summary>
    ''' <param name="p">two way contingency table</param>
    ''' <param name="n">number of observations</param>
    ''' <param name="alpha">significance level</param>
    ''' <param name="epsilon">tolerance parameter</param>
    ''' <param name="nDirections">
    ''' number of random directions to search for a boundary point
    ''' the number of random directions has a negative impact on the computation time
    ''' </param>
    ''' <param name="nBootstrapSamples">number of bootstrap samples</param>
    ''' <returns>
    ''' It returns the test result, which is true if test can reject H_0. 
    ''' Additionally if the test result is true, it returns the smallest epsilon 
    ''' for which test can reject H_0.
    ''' </returns>
    ''' <remarks></remarks>
    Public Function bootstrapTest(p As Double(,), n As Integer, alpha As Double, epsilon As Double,
                                  nDirections As Integer, nBootstrapSamples As Integer) As TestResult
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
            .nBstSamples = nBootstrapSamples
            .alpha = alpha
            .opt = mprod
            .rnd = New clsRandomNumberGenerator(1007, 1977)
        End With

        'generate directions
        For i = 1 To nDirections
            With parameter
                Dim ex_point = .opt.RandomOuterPoint(.eps * 1.1, .n1 * .n2, .rnd)
                .ex_point.Add(ex_point)
            End With
        Next

        Dim res As New TestResult
        res.result = CompositeMultinomial.composite_bst_multidirectional(parameter, parameter.p.get_pr())

        'calculate minimum epsilon
        If res.result Then
            Dim f As Func(Of Double, Boolean)

            f = Function(eps As Double)
                    parameter.eps = eps
                    Return CompositeMultinomial.composite_bst_multidirectional(parameter, parameter.p.get_pr)
                End Function

            res.minEps = binSearch(f, 0, parameter.eps)
        End If

        res.minEps = Math.Sqrt(res.minEps)
        Return res
    End Function
End Module
