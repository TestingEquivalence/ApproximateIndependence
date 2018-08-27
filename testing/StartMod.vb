Imports System.Threading
Imports System.Threading.Tasks
Module StartMod
    Sub main()
        research_equivalence.real_world_Welleks_test()
        Console.WriteLine("press key")
        Console.ReadKey()
        Console.WriteLine("-----------------------------------------")

        research_equivalence.real_world_asymptotic_test()
        Console.WriteLine("press key")
        Console.ReadKey()
        Console.WriteLine("-----------------------------------------")

        research_equivalence.real_world_Bootstrap_test()
        Console.WriteLine("press key")
        Console.ReadKey()
        Console.WriteLine("-----------------------------------------")
    End Sub
End Module
