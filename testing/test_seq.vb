
Module test_seq
    Function sim_test(prm As TestParameter,
        test As Func(Of TestParameter, Double(), Boolean)) As Double

        Dim count As Integer = 0
        Dim log As New log(prm.LogPath, True)
        Dim rnd As New clsRandomNumberGenerator(1007, 1977)

        For t As Integer = 1 To prm.ntoss

            Dim res As Boolean = False
            Dim rp As Double() = prm.p.dbl_simulate(prm.n, rnd)

            res = test(prm, rp)

            If res Then count = count + 1
            If t / 1000 = t \ 1000 Then
                System.Console.WriteLine(t.ToString & " paths done.")
            End If
        Next


        Dim pow As Double = count / prm.ntoss

        'log.WriteTestReport(prm, pow)
        log.WriteLn(pow)
        log.close()
        Return pow
    End Function

  
    Function sim_test_mathnet_long(prm As TestParameter,
       test As Func(Of TestParameter, Double(), Boolean),
       ls As List(Of Double())) As Double

        Dim count As Integer = 0
        Dim log As New log(prm.LogPath, True)
        Dim r As New System.Random(10071977)

        For Each rp In ls
            Dim res As Boolean = False
            res = test(prm, rp)
            If res Then count = count + 1
        Next

        Dim pow As Double = count / ls.Count

        log.WriteTestReport(prm, pow)
        log.close()
        Return pow
    End Function



    Sub test_real_exmp(prm As TestParameter,
                       test As Func(Of TestParameter, Double(), Boolean))

        Dim out As New List(Of String)
        System.Console.WriteLine("start optimisation")
        Dim eps_min As Double

        Dim f As Func(Of Double, Boolean)

        f = Function(eps As Double)
                prm.eps = eps
                Return test(prm, prm.p.get_pr)
            End Function

        eps_min = binSearch(f, 0, prm.eps)

        With prm
            Dim log As New log(.LogPath, True)
            log.WriteLn("--------------------------")
            log.WriteLn(.key)
            log.WriteLn("n")
            log.WriteLn(.n)
            log.WriteLn("alpha")
            log.WriteLn(.alpha)
            log.WriteLn("eps")
            log.WriteLn(.eps)
            log.WriteLn("l2")
            log.WriteLn(l2(.p.get_pr, .q.get_pr))
            log.WriteLn("l1")
            log.WriteLn(l1(.p.get_pr, .q.get_pr))
            log.WriteLn("smooth l1=")
            log.WriteLn(smooth_l1(.a)(.p.get_pr, .q.get_pr))

            If Not IsNothing(prm.ex_point) Then
                log.WriteLn("number of directions:")
                log.WriteLn(prm.ex_point.Count)
            End If

            log.WriteLn("min epsilon")
            log.WriteLn(eps_min.ToString)
            log.close()
        End With
    End Sub


   

End Module
