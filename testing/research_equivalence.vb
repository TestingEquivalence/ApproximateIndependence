Imports System.Threading
Imports System.Threading.Tasks
Imports System.Collections.Concurrent


Module research_equivalence
#Region "product"

    Sub printNearestPoint(key As String, n1 As Integer, n2 As Integer,
                 dst As Func(Of Double(), Double(), Double))
        Dim cons As New constants()
        Dim mprod As New minProduct(n1, n2, dst)
        Dim p = New clsMultinomialDistribution(cons.WMultDistr(key))
        Dim log As New log("D:\log.txt", True)

        log.WriteLn("rand point " + key)

        log.WriteMatrix(p.get_pr, n1, n2)

        Dim q = mprod.min_dist(p.get_pr)
        log.WriteLn("nearest point of " + key)
        log.WriteMatrix(q, n1, n2)

        log.close()
    End Sub

    Function prodRand(key As String, n As Integer, _
                 n1 As Integer, n2 As Integer,
                 test As Func(Of TestParameter, Double(), Boolean),
                 alpha As Double, flag_sf As Boolean,
                 nDirections As Integer,
                 eps As Double,
                 p As Double()) As Double
        '-----------
        Dim parameter As New TestParameter
        Dim cons As New constants()
        Dim rnd As New clsRandomNumberGenerator(1007, 1997)

        With parameter
            .test_statistic = l22
            .derivative = l22_derivative

            Dim mprod As New minProduct(n1, n2, .test_statistic)
            '.p = New clsMultinomialDistribution(cons.WMultDistr(key))
            .p = New clsMultinomialDistribution(p)
            Dim q_pr = mprod.min_dist(.p.get_pr())
            .q = New clsMultinomialDistribution(q_pr)
            .eps = eps
            .key = key
            .ntoss = 10 * 1000
            .n = n
            .nBstSamples = 1 * 1000
            .alpha = alpha
            If flag_sf Then
                .LogPath = "log.txt"
            Else
                .LogPath = key + ".txt"
            End If
            .reset = True
            .n1 = n1
            .n2 = n2
            .opt = mprod
            .rnd = rnd
            .a = 0.001
        End With

        'erstelle the directions
        For i = 1 To nDirections
            With parameter
                Dim ex_point = .opt.RandomOuterPoint(.eps * 1.1, .n1 * .n2, .rnd)
                .ex_point.Add(ex_point)
            End With
        Next


        Console.WriteLine(key)
        Dim power = sim_test(parameter, test)
        Console.WriteLine(n & " done!")
        Return power
    End Function


    Sub perform_rw_tests_product(tests() As Func(Of TestParameter, Double(), Boolean))
        Dim cns As New constants
        Dim p As clsMultinomialDistribution
        Dim prm As New TestParameter()
        Dim dist As Func(Of Double(), Double(), Double) = FreqUsedFunc.l2
        Dim nDirections As Integer = 1000
        Dim rnd As New clsRandomNumberGenerator(1007, 1977)


        For Each t In tests
            With prm
                .eps = 0.2
                .ntoss = 1
                .nBstSamples = 1 * 1000
                .alpha = 0.05
                .LogPath = "D:\rw_examples_prod.txt"
                .reset = False
                .rnd = rnd


                'first example "nitren" 2x4
                .key = "nitren"
                .n1 = 2
                .n2 = 4

                ''send example: "eye_hair" 4x4
                '.key = "eye_hair"
                '.n1 = 4
                '.n2 = 4

                'send example: "child_income" 4x4
                '.key = "child_income"
                '.n1 = 5
                '.n2 = 4

                .n = sum(cns.FMultDistr(.key))

                Dim opt As minProduct

                p = New clsMultinomialDistribution(cns.FMultDistr(.key))
                .p = p
                opt = New minProduct(.n1, .n2, dist)
                .q = New clsMultinomialDistribution(opt.min_dist(p.get_pr))
                .opt = opt

                'erstelle the directions
                For i = 1 To nDirections
                    With prm
                        Dim ex_point = .opt.RandomOuterPoint(.eps * 1.1, .n1 * .n2, .rnd)
                        .ex_point.Add(ex_point)
                    End With
                Next

                test_real_exmp(prm, t)
            End With
        Next
    End Sub

    Sub real_world_asymptotic_test()
        Dim cnst = New constants()
        Dim pCounts = cnst.FMultDistr("nitren")
        Dim p = cnst.ProdDict("nitren")
        'Dim log = New log("D:\rw_asymptotic.txt", False)
        Dim log As New log()

        Dim res = tests_two_way_collapsibility.asymptoticTest(p, sum(pCounts), 0.05, 0.2)
        log.WriteLn("asymptotic test: nitrendepine monotherapy vs gender")
        log.Write(res)
        log.WriteLn("")

        pCounts = cnst.FMultDistr("eye_hair")
        p = cnst.ProdDict("eye_hair")
        res = tests_two_way_collapsibility.asymptoticTest(p, sum(pCounts), 0.05, 0.2)
        log.WriteLn("asymptotic test: eye color vs hair color")
        log.Write(res)
        log.WriteLn("")

        pCounts = cnst.FMultDistr("child_income")
        p = cnst.ProdDict("child_income")
        res = tests_two_way_collapsibility.asymptoticTest(p, sum(pCounts), 0.05, 0.2)
        log.WriteLn("asymptotic test: number of children vs income")
        log.Write(res)
        log.WriteLn("")

        log.close()



    End Sub


    Sub real_world_Welleks_test()
        Dim cnst = New constants()
        Dim pCounts = cnst.FMultDistr("nitren")
        Dim p = cnst.ProdDict("nitren")
        'Dim log = New log("D:\rw_Welleks_test.txt", False)
        Dim log = New log()

        Dim res = _WelleksTest(p, sum(pCounts), 0.05, 0.2)
        log.WriteLn("Wellek's test: nitrendepine monotherapy vs gender")
        log.Write(res)
        log.WriteLn("")

        pCounts = cnst.FMultDistr("eye_hair")
        p = cnst.ProdDict("eye_hair")
        res = _WelleksTest(p, sum(pCounts), 0.05, 0.2)
        log.WriteLn("Wellek's test: eye color vs hair color")
        log.Write(res)
        log.WriteLn("")

        pCounts = cnst.FMultDistr("child_income")
        p = cnst.ProdDict("child_income")
        res = _WelleksTest(p, sum(pCounts), 0.05, 0.2)
        log.WriteLn("Wellek's test: number of children vs income")
        log.Write(res)
        log.WriteLn("")

        log.close()

    End Sub

    Sub real_world_Bootstrap_test()
        Dim cnst = New constants()
        Dim pCounts = cnst.FMultDistr("nitren")
        Dim p = cnst.ProdDict("nitren")
        'Dim log = New log("D:\rw_bootstrap.txt", False)
        Dim log As New log()
        Dim nDirections As Integer = 10

        Dim res = tests_two_way_collapsibility.bootstrapTest(p, sum(pCounts), 0.05, 0.2, nDirections, 1000)
        log.WriteLn("bootstrap test: nitrendepine monotherapy vs gender")
        log.Write(res)
        log.WriteLn("")

        pCounts = cnst.FMultDistr("eye_hair")
        p = cnst.ProdDict("eye_hair")
        res = tests_two_way_collapsibility.bootstrapTest(p, sum(pCounts), 0.05, 0.2, nDirections, 1000)
        log.WriteLn("bootstrap test: eye color vs hair color")
        log.Write(res)
        log.WriteLn("")

        pCounts = cnst.FMultDistr("child_income")
        p = cnst.ProdDict("child_income")
        res = tests_two_way_collapsibility.bootstrapTest(p, sum(pCounts), 0.05, 0.2, nDirections, 1000)
        log.WriteLn("bootstrap test: number of children vs income")
        log.Write(res)
        log.WriteLn("")

        log.close()



    End Sub

    Function get_rp(rp As String, lb As Double, ub As Double) As Double()
        Dim opt = New minProduct(2, 4, l2)
        Dim cnst = New constants()

        Dim p = cnst.WMultDistr(rp)
        Dim res = opt.min_dist_val(p)

        Dim f As Func(Of Double, Double)

        f = Function(x As Double)
                p(7) = x
                p(4) = 1 - sum(p) + p(4)

                Dim dst = opt.min_dist_val(p)
                Return dst
            End Function

        Dim log As New log("D:\" + rp + ".txt", False)

        Dim p4 = bisection(f, 0.1, lb, ub)
        p(7) = p4
        p(4) = 1 - sum(p) + p(4)

        res = opt.min_dist_val(p)
        log.WriteLn(res)
        Dim pr = New clsMultinomialDistribution(p)
        log.Write(pr)
        log.WriteLn("")
        log.close()

        Return p
    End Function

    Sub powerAtBoundary(n As Integer,
                        n1 As Integer, n2 As Integer,
                 test As Func(Of TestParameter, Double(), Boolean),
                 alpha As Double, flag_sf As Boolean,
                 nDirections As Integer,
                 eps As Double,
                 nBoundaryPoints As Integer,
                 shrink_eps As Double)
        Dim rnd As New clsRandomNumberGenerator(1007, 1977)

        'generate random boundary points
        Dim opt = New minProduct(n1, n2, l22)
        Dim ls As New List(Of Double())
        Dim index As New List(Of Integer)
        For i As Integer = 1 To nBoundaryPoints
            ls.Add(opt.RandomBoundaryPoint(eps, n1 * n2, rnd))
            index.Add(i)
        Next

        Dim boundaryPoints As New log("boundary_points.txt", False)
        For i As Integer = 1 To nBoundaryPoints
            Dim res = opt.min_dist_val(ls(i - 1))
            boundaryPoints.Write(i.ToString + ";")
            boundaryPoints.Write(New clsMultinomialDistribution(ls(i - 1)))
            boundaryPoints.WriteLn("")
        Next
        boundaryPoints.close()

        Dim results As New ConcurrentDictionary(Of Integer, Double)

        index.Clear()
        index.Add(0)
        index.Add(10)
        index.Add(20)
        index.Add(30)
        index.Add(40)
        index.Add(50)
        index.Add(60)
        index.Add(70)
        index.Add(80)
        index.Add(90)

        'shrink eps
        Parallel.ForEach(index, Sub(i)
                                    For j As Integer = 0 To 9
                                        Dim pos As Integer = i + j
                                        Dim p = ls(pos)
                                        Dim res = prodRand("rp" & pos.ToString, n, n1, n2, test, alpha, flag_sf, nDirections, shrink_eps, p)
                                        results(pos + 1) = res
                                    Next
                                End Sub)


        Dim powerFunction As New log("power_function.txt", False)
        For i As Integer = 1 To nBoundaryPoints
            powerFunction.Write(i.ToString + ";")
            powerFunction.Write(results(i).ToString)
            powerFunction.WriteLn("")
        Next
        powerFunction.close()

    End Sub
#End Region
End Module
