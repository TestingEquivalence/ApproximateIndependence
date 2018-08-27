Public Class MinDistance
    Dim Q As Double()
    Dim P As Double()
    Dim eps As Double
    Dim T As Func(Of Double(), Double(), Double)
    Sub New(Q() As Double, P() As Double, eps As Double, _
            T As Func(Of Double(), Double(), Double))
        Me.Q = Q
        Me.P = P
        Me.eps = eps
        Me.T = T
    End Sub
    
    
    
   
   
    Shared Function L2_min_dist(p As Double(), q As Double(), eps As Double) As Double()
        Dim d As Integer = q.GetUpperBound(0)
        Dim a As Double
        a = eps / l2(p, q)
        Dim res(d) As Double
        For i As Integer = 0 To d
            res(i) = (1 - a) * q(i) + a * p(i)
        Next
        Return res
    End Function

    Shared Function L1_min_dist(p As Double(), q As Double(), eps As Double) As Double()
        Dim d As Integer = q.GetUpperBound(0)
        Dim a As Double
        a = eps / l1(p, q)
        Dim res(d) As Double
        For i As Integer = 0 To d
            res(i) = (1 - a) * q(i) + a * p(i)
        Next
        Return res
    End Function
End Class
