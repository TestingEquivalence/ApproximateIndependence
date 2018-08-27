Module optimisation
    Function binSearch(test As Func(Of Double, Boolean),
                     Optional pl As Double = 0,
                     Optional pu As Double = 1) As Double
        If test(pu) = False Then
            Return pu
        End If

        If test(pl) = True Then
            Return pl
        End If


        Do Until pu - pl < 0.0001
            Dim alpha As Double = (pl + pu) / 2
            If test(alpha) Then
                pu = alpha
            Else
                pl = alpha
            End If
        Loop

        Return pu
    End Function

    Function bisection(f As Func(Of Double, Double),
                       aim As Double, pl As Double, pu As Double,
                       Optional tolerance As Double = 0.000001) As Double
        If f(pu) < aim Then
            Return pu
        End If

        If f(pl) > aim Then
            Return pl
        End If


        Do Until pu - pl < tolerance
            Dim alpha As Double = (pl + pu) / 2
            If f(alpha) >= aim Then
                pu = alpha
            Else
                pl = alpha
            End If
        Loop

        Return pu
    End Function

    End Module
