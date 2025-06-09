//Npgsql .NET Data Provider for PostgreSQL

using System.Configuration;
using cat.itb.M6UF2EA3.model;
using UF2_test.cruds;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("\n[1] Exercici 1" +
                               "\n[2] Exercici 2" +
                               "\n[3] Exercici 3" +
                               "\n[4] Exercici 4" +
                               "\n[5] Exercici 5" +
                               "\n[6] Exercici 6" +
                               "\n[7] Exercici 7" +
                               "\n[8] Exercici 8" +
                               "\n[9] Drop tables" +
                               "\n[10] Run script" +
                               "\n[0] Exit");

        int option = int.Parse(Console.ReadLine());
        var gCRUD = new GeneralCRUD();

        switch (option)
        {
            case 0:
                Console.WriteLine("Good night..");
                break;
            case 1:
                Ex1();
                break;
            case 2:
                Ex2();
                break;
            case 3:
                Ex3();
                break;
            case 4:
                Ex4();
                break;
            case 5:
                Ex5();
                break;
            case 6:
                Ex6();
                break;
            case 7:
                Ex7();
                break;
            case 8:
                Ex8();
                break;
            case 9:
                var tables = new List<string>
                {
                    "departamentos",
                    "empleados"
                };

                gCRUD.DropTables(tables);
                break;
            case 10:
                gCRUD.RunScriptHR();
                break;
        }
    }

    public static void Ex1()
    {
        Console.WriteLine("--- Ex1: Inserta aquests tres nous departaments a la taula DEPARTAMENTOS ---");
        var dep1 = new Departamento
        {
            Dnombre = "TIC",
            Loc = "BURGOS"
        };

        var dep2 = new Departamento
        {
            Dnombre = "DESARROLLO",
            Loc = "ALICANTE"
        };

        var dep3 = new Departamento
        {
            Dnombre = "INVESTIGACIÓN",
            Loc = "ALMERIA"
        };

        var dCRUD = new DepartamentosCRUD();
        dCRUD.Insert(dep1);
        dCRUD.Insert(dep2);
        dCRUD.Insert(dep3);
    }
    public static void Ex2()
    {
        Console.WriteLine("--- Ex2: Inserta un nou empleat a cada departament nou, tria tu les dades dels nous empleats ---");
        var emp1 = new Empleado
        {
            Empno = 8000,
            Apellido = "FONT",
            Oficio = "DIRECTIU",
            Dir = 7934,
            Fechaalt = new DateTime(2019, 12, 31, 5, 10, 20, DateTimeKind.Utc),
            Salario = 9000,
            Comision = 2000,
        };

        var emp2 = new Empleado
        {
            Empno = 8500,
            Apellido = "ARNAUS",
            Oficio = "SECRETARI",
            Dir = 7902,
            Fechaalt = new DateTime(2020, 10, 20, 5, 10, 20, DateTimeKind.Utc),
            Salario = 400,
        };

        var emp3 = new Empleado
        {
            Empno = 9991,
            Apellido = "LLOPIS",
            Oficio = "INFORMÀTIC",
            Dir = 7900,
            Fechaalt = new DateTime(2018, 11, 01, 5, 10, 20, DateTimeKind.Utc),
            Salario = 400,
        };

        var dCRUD = new DepartamentosCRUD();
        emp1.Departamento = dCRUD.SelectById(5);
        emp2.Departamento = dCRUD.SelectById(6);
        emp3.Departamento = dCRUD.SelectById(7);

        var eCRUD = new EmpleadosCRUD();
        eCRUD.Insert(emp1);
        eCRUD.Insert(emp2);
        eCRUD.Insert(emp3);
    }
    public static void Ex3()
    {
        Console.WriteLine("--- Ex3: Actualitza el nom del departament número 3, ara es dirà MARKETING ---");
        var dCRUD = new DepartamentosCRUD();
        Departamento dep = dCRUD.SelectById(3);
        dep.Dnombre = "MARKETING";
        dCRUD.Update(dep);
    }
    public static void Ex4()
    {
        Console.WriteLine("--- Ex4: Actualitza el salari de l’empleat amb empno = 7839, ara cobra 5500 ---");
        var eCRUD = new EmpleadosCRUD();
        var emp = eCRUD.SelectByEmpno(7839);
        emp.Salario = 5500;
        eCRUD.Update(emp);
    }
    public static void Ex5()
    {
        Console.WriteLine("--- Ex5: Elimina l’empleat que es diu NEGRO ---");
        var empCRUD = new EmpleadosCRUD();
        var emp = empCRUD.SelectBySurname("NEGRO");
        empCRUD.Delete(emp);
    }
    public static void Ex6()
    {
        Console.WriteLine("--- Ex6: Mostra per pantalla el cognom i el salari dels empleats que cobrin menys de 2500 ---");
        var empCRUD = new EmpleadosCRUD();
        IList<Empleado> emps = empCRUD.SelectBySalaryLowerThan(2500);
        foreach (var emp in emps)
        {
            Console.WriteLine(emp.Apellido + " - " + emp.Salario);
        }
    }
    public static void Ex7()
    {
        Console.WriteLine("--- Ex7: Mostra el nom i la ciutat del department de l’empleat amb id = 8 ---");
        var eCRUD = new EmpleadosCRUD();
        Empleado emp = eCRUD.SelectById(8);
        Console.WriteLine(emp.Departamento.Dnombre + "  " + emp.Departamento.Loc);
    }
    public static void Ex8()
    {
        Console.WriteLine("--- Ex8: Mostra el cognom, l’ofici i el salari de tots els empleats del departament amb id = 3 ---");
        var depCRUD = new DepartamentosCRUD();
        var deps = depCRUD.SelectById(3);
        foreach (var emp in deps.Empleados)
            Console.WriteLine(emp.Apellido + " - " + emp.Oficio + " - " + emp.Salario);
    }
}