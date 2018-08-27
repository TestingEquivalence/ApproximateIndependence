Public Class constants
    Public WMultDistr As New Dictionary(Of String, Double())
    Public FMultDistr As New Dictionary(Of String, Double())
    Public qdic As New Dictionary(Of String, clsMultinomialDistribution)
    Public ProdDict As New Dictionary(Of String, Double(,))
    Public TwoWayCls As New Dictionary(Of String, Double())

    Public Sub New()
        Me.InitProdDic()
        Me.InitProdDic2()
        Me.InitProdDic3()
        Me.initProdRW()
        Me.addProdRand()
        InitProdDic3x4()
        InitProdDic4x5()
        InitProdDic5x5()
    End Sub

    Sub InitProdDic()
        
        Dim minProd As New minProduct(2, 4, l2)
        Dim p(1), q(3) As Double
        Dim pq As Double(,)

        p = {0.25, 0.75}
        q = {0.1, 0.4, 0.3, 0.2}
        pq = minProd.ProductMatrix(p, q)
        Me.ProdDict.Add("p24_1", pq)
        Me.WMultDistr.Add("p24_1", minProd.Matrix2Vector(pq))

        p = {0.5, 0.5}
        q = {0.25, 0.25, 0.25, 0.25}
        pq = minProd.ProductMatrix(p, q)
        Me.ProdDict.Add("p24_2", pq)
        Me.WMultDistr.Add("p24_2", minProd.Matrix2Vector(pq))

        p = {0.33, 0.67}
        q = {0.15, 0.15, 0.15, 0.55}
        pq = minProd.ProductMatrix(p, q)
        Me.ProdDict.Add("p24_3", pq)
        Me.WMultDistr.Add("p24_3", minProd.Matrix2Vector(pq))

        p = {0.1, 0.9}
        q = {0.1, 0.4, 0.25, 0.25}
        pq = minProd.ProductMatrix(p, q)
        Me.ProdDict.Add("p24_4", pq)
        Me.WMultDistr.Add("p24_4", minProd.Matrix2Vector(pq))

        p = {0.5, 0.5}
        q = {0.1, 0.1, 0.1, 0.7}
        pq = minProd.ProductMatrix(p, q)
        Me.ProdDict.Add("p24_5", pq)
        Me.WMultDistr.Add("p24_5", minProd.Matrix2Vector(pq))
    End Sub
    Sub InitProdDic2()
        

        Dim minProd As New minProduct(3, 3, l1)
        Dim p(3), q(3) As Double
        Dim pq As Double(,)

        p = {0.33, 0.33, 0.34}
        q = {0.33, 0.33, 0.34}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p33_1", minProd.Matrix2Vector(pq))

        p = {0.1, 0.45, 0.45}
        q = {0.33, 0.33, 0.34}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p33_2", minProd.Matrix2Vector(pq))


        p = {0.1, 0.1, 0.8}
        q = {0.33, 0.33, 0.34}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p33_3", minProd.Matrix2Vector(pq))

        p = {0.1, 0.45, 0.45}
        q = {0.1, 0.45, 0.45}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p33_4", minProd.Matrix2Vector(pq))


        p = {0.1, 0.1, 0.8}
        q = {0.1, 0.45, 0.45}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p33_5", minProd.Matrix2Vector(pq))

    End Sub

    Sub InitProdDic3x4()


        Dim minProd As New minProduct(3, 4, l2)
        Dim p(3), q(4) As Double
        Dim pq As Double(,)

        p = {0.33, 0.33, 0.34}
        q = {0.25, 0.25, 0.25, 0.25}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p34_1", minProd.Matrix2Vector(pq))

        p = {0.1, 0.45, 0.45}
        q = {0.1, 0.4, 0.3, 0.2}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p34_2", minProd.Matrix2Vector(pq))


        p = {0.1, 0.1, 0.8}
        q = {0.1, 0.1, 0.1, 0.7}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p34_3", minProd.Matrix2Vector(pq))

        p = {0.1, 0.45, 0.45}
        q = {0.15, 0.15, 0.15, 0.55}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p34_4", minProd.Matrix2Vector(pq))


        p = {0.1, 0.1, 0.8}
        q = {0.25, 0.25, 0.25, 0.25}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p34_5", minProd.Matrix2Vector(pq))

    End Sub

    Sub InitProdDic4x5()


        Dim minProd As New minProduct(4, 5, l2)
        Dim p(4), q(5) As Double
        Dim pq As Double(,)

        p = {0.25, 0.25, 0.25, 0.25}
        q = {0.2, 0.2, 0.2, 0.2, 0.2}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p45_1", minProd.Matrix2Vector(pq))

        p = {0.1, 0.4, 0.3, 0.2}
        q = {0.1, 0.1, 0.2, 0.2, 0.4}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p45_2", minProd.Matrix2Vector(pq))


        p = {0.1, 0.1, 0.1, 0.7}
        q = {0.1, 0.1, 0.1, 0.1, 0.6}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p45_3", minProd.Matrix2Vector(pq))

        p = {0.15, 0.15, 0.15, 0.55}
        q = {0.35, 0.1, 0.1, 0.1, 0.35}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p45_4", minProd.Matrix2Vector(pq))


        p = {0.05, 0.05, 0.05, 0.85}
        q = {0.2, 0.2, 0.2, 0.2, 0.2}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p45_5", minProd.Matrix2Vector(pq))

    End Sub

    Sub InitProdDic5x5()


        Dim minProd As New minProduct(5, 5, l2)
        Dim p(5), q(5) As Double
        Dim pq As Double(,)

        p = {0.2, 0.2, 0.2, 0.2, 0.2}
        q = {0.2, 0.2, 0.2, 0.2, 0.2}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p55_1", minProd.Matrix2Vector(pq))

        p = {0.1, 0.1, 0.3, 0.2, 0.3}
        q = {0.1, 0.1, 0.2, 0.2, 0.4}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p55_2", minProd.Matrix2Vector(pq))


        p = {0.1, 0.1, 0.1, 0.1, 0.6}
        q = {0.1, 0.1, 0.1, 0.1, 0.6}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p55_3", minProd.Matrix2Vector(pq))

        p = {0.1, 0.1, 0.6, 0.1, 0.1}
        q = {0.35, 0.1, 0.1, 0.1, 0.35}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p55_4", minProd.Matrix2Vector(pq))


        p = {0.05, 0.05, 0.05, 0.05, 0.8}
        q = {0.2, 0.2, 0.2, 0.2, 0.2}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p55_5", minProd.Matrix2Vector(pq))

    End Sub

    Sub InitProdDic3()
        

        Dim minProd As New minProduct(4, 4, l1)
        Dim p(4), q(4) As Double
        Dim pq As Double(,)

        p = {0.1, 0.1, 0.1, 0.7}
        q = {0.1, 0.4, 0.3, 0.2}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p44_1", minProd.Matrix2Vector(pq))


        p = {0.1, 0.4, 0.3, 0.2}
        q = {0.25, 0.25, 0.25, 0.25}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p44_2", minProd.Matrix2Vector(pq))

        p = {0.1, 0.4, 0.25, 0.25}
        q = {0.15, 0.15, 0.15, 0.55}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p44_3", minProd.Matrix2Vector(pq))

        p = {0.25, 0.25, 0.25, 0.25}
        q = {0.25, 0.25, 0.25, 0.25}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p44_4", minProd.Matrix2Vector(pq))

        p = {0.15, 0.15, 0.15, 0.55}
        q = {0.1, 0.1, 0.1, 0.7}
        pq = minProd.ProductMatrix(p, q)
        Me.WMultDistr.Add("p44_5", minProd.Matrix2Vector(pq))

    End Sub

    Sub addProdRand()
        Dim p() As Double
        Dim minProd As New minProduct(2, 4, l2)

        p = {0.05, 0.05, 0.05, 0.05, 0.7556430054, 0.01, 0.01, 0.0243569946}
        Me.WMultDistr.Add("rp1", p)
        
        p = {0.05, 0.05, 0.05, 0.1, 0.4403266907, 0.05, 0.15, 0.1096733093}
        Me.WMultDistr.Add("rp2", p)

        p = {0.05, 0.15, 0.1, 0.35, 0.0186694336, 0.05, 0.15, 0.1313305664}
        Me.WMultDistr.Add("rp3", p)

        p = {0.1, 0.05, 0.2, 0.15, 0.1820472717, 0.1, 0.1, 0.1179527283}
        Me.WMultDistr.Add("rp4", p)

        p = {0.1, 0.1, 0.1, 0.15, 0.1885494232, 0.1, 0.2, 0.0614505768}
        Me.WMultDistr.Add("rp5", p)

        p = {0.05, 0.1, 0.15, 0.2, 0.1707311249, 0.05, 0.1, 0.1792688751}
        Me.WMultDistr.Add("rp6", p)

        p = {0.25, 0.2, 0.05, 0.05, 0.0932899475, 0.2, 0.1, 0.0567100525}
        Me.WMultDistr.Add("rp7", p)

        p = {0.15, 0.15, 0.15, 0.15, 0.1, 0.05, 0.05, 0.2}
        Me.WMultDistr.Add("rp8", p)

        p = {0.2, 0.05, 0.05, 0.1, 0.1720413208, 0.2, 0.1, 0.1279586792}
        Me.WMultDistr.Add("rp9", p)

        p = {0.2, 0.05, 0.05, 0.2, 0.0983612061, 0.05, 0.15, 0.2016387939}
        Me.WMultDistr.Add("rp10", p)

        p = {0.01274551, 0.0625, 0.0625, 0.0625,
            0.0625, 0.0625, 0.0625, 0.0625,
            0.0625, 0.0625, 0.0625, 0.0625,
            0.0625, 0.0625, 0.0625, 0.11225449}


        Me.WMultDistr.Add("4x4_p1", p)

        p = {0.01274551, 0.01592144, 0.0625, 0.0625,
0.0625, 0.0625, 0.0625, 0.0625,
0.0625, 0.0625, 0.0625, 0.0625,
0.0625, 0.0625, 0.10907856, 0.11225449}

        Me.WMultDistr.Add("4x4_p2", p)

        p = {0.01274551, 0.01592144, 0.0625, 0.0625,
0.0625, 0.02246172, 0.0625, 0.0625,
0.0625, 0.0625, 0.10253828, 0.0625,
0.0625, 0.0625, 0.10907856, 0.11225449}

        Me.WMultDistr.Add("4x4_p3", p)

        p = {0.01274551, 0.01592144, 0.0625, 0.02051667,
0.0625, 0.02246172, 0.0625, 0.0625,
0.0625, 0.0625, 0.10253828, 0.0625,
0.10448333, 0.0625, 0.10907856, 0.11225449}


        Me.WMultDistr.Add("4x4_p4", p)

        p = {0.01274551, 0.01592144, 0.0625, 0.02051667,
0.0625, 0.02246172, 0.0625, 0.03466441,
0.09033559, 0.0625, 0.10253828, 0.0625,
0.10448333, 0.0625, 0.10907856, 0.11225449}


        Me.WMultDistr.Add("4x4_p5", p)

        p = {0.05240987, 0.04, 0.025, 0.025,
0.01, 0.04, 0.025, 0.025,
0.01, 0.04, 0.025, 0.025,
0.07, 0.28, 0.175, 0.13259013}


        Me.WMultDistr.Add("4x4_p6", p)

        p = {0.05240987, 0.04, 0.025, 0.01090649,
0.01, 0.05409351, 0.025, 0.025,
0.01, 0.04, 0.025, 0.025,
0.07, 0.28, 0.175, 0.13259013}


        Me.WMultDistr.Add("4x4_p7", p)

        p = {0.05240987, 0.04, 0.02105765, 0.01090649,
0.01, 0.05409351, 0.02894235, 0.025,
0.01, 0.04, 0.025, 0.025,
0.07, 0.28, 0.175, 0.13259013}


        Me.WMultDistr.Add("4x4_p8", p)

        p = {0.05240987, 0.05471962, 0.02105765, 0.01090649,
0.01, 0.05409351, 0.02894235, 0.025,
0.01, 0.04, 0.025, 0.01028038,
0.07, 0.28, 0.175, 0.13259013}


        Me.WMultDistr.Add("4x4_p9", p)

        p = {0.05240987, 0.05471962, 0.02105765, 0.01090649,
            0.01, 0.05409351, 0.02894235, 0.02707414,
            0.01, 0.04, 0.02292586, 0.01028038,
            0.07, 0.28, 0.175, 0.13259013}


        Me.WMultDistr.Add("4x4_p10", p)
    End Sub

    Sub initProdRW()
        Dim p As clsMultinomialDistribution
        Dim minProd As New minProduct(2, 4, l2)

        FMultDistr.Add("nitren", {9, 13, 13, 48, 24, 18, 20, 72})
        p = New clsMultinomialDistribution(FMultDistr("nitren"))
        ProdDict.Add("nitren", minProd.Vector2Matrix(p.get_pr()))


        minProd = New minProduct(4, 4, l2)
        FMultDistr.Add("eye_hair", {68, 119, 26, 7, _
                                           20, 84, 17, 94, _
                                           15, 54, 14, 10, _
                                            5, 29, 14, 16})
        p = New clsMultinomialDistribution(FMultDistr("eye_hair"))
        ProdDict.Add("eye_hair", minProd.Vector2Matrix(p.get_pr()))


        FMultDistr.Add("child_income", {2161, 3577, 2184, 1636, _
                                       2755, 5081, 2222, 1052, _
                                        936, 1753, 640, 306, _
                                        225, 419, 96, 38, _
                                         39, 98, 31, 14})

        p = New clsMultinomialDistribution(FMultDistr("child_income"))
        ProdDict.Add("child_income", minProd.Vector2Matrix(p.get_pr()))

    End Sub

    
End Class
