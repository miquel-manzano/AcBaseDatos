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
                               "\n[9] Exercici 9" +
                               "\n[10] Exercici 10" +
                               "\n[11] Drop tables" +
                               "\n[12] Run script" +
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
                Ex9();
                break;
            case 10:
                Ex10();
                break;
            case 11:
                var tables = new List<string>
                {
                    "departamentos",
                    "empleados"
                };

                gCRUD.DropTables(tables);
                break;
            case 12:
                gCRUD.RunScriptHR();
                break;
            default:
                Console.WriteLine("Not an option...");
                break;
        }
    }

    public static void Ex1()
    {
        Console.WriteLine("--- Ex1: Mostra les dades de tots els departaments utiltzant HQL ---");

        var dCRUD = new DepartamentosCRUD();
        var deps = dCRUD.SelectAllHql();

        foreach (var dep in deps)
        {
            Console.WriteLine(dep.Id + " - " + dep.Dnombre + " - " + dep.Loc);
        }
    }
    public static void Ex2()
    {
        Console.WriteLine("--- Ex2: Mostra les dades de tots els empleats utiltzant Criteria ---");

        var eCRUD = new EmpleadosCRUD();
        var emps = eCRUD.SelectAllUsingCriteria();

        foreach (var emp in emps)
        {
            Console.WriteLine("{0} - {1} - {2} - {3} - {4} - {5} - {6} - {7} - {8}",
                    emp.Id, emp.Empno, emp.Apellido, emp.Oficio, emp.Dir, emp.Fechaalt, emp.Salario, emp.Comision, emp.Departamento.Id);
        }
    }
    public static void Ex3()
    {
        Console.WriteLine("--- Ex3: Mostra per pantalla el cognom i el salari dels empleats que cobrin més de 2000 utilitzant Criteria ---");

        var eCRUD = new EmpleadosCRUD();
        var emps = eCRUD.SelectBySalaryBiggerThanUsingCriteria(2000);

        foreach (var emp in emps)
        {
            Console.WriteLine(emp.Apellido + " - " + emp.Salario);
        }
    }
    public static void Ex4()
    {
        Console.WriteLine("--- Ex4: Mostra la localització del departament de VENTAS utiltzant HQL ---");

        var dCRUD = new DepartamentosCRUD();
        var dep = dCRUD.SelectByNameHql("VENTAS");
        Console.WriteLine(dep.Loc);
    }
    public static void Ex5()
    {
        Console.WriteLine("--- Ex5: Mostra les dades dels empleats que el seu ofici sigui VENDEDOR i els ordenes per salari de forma descendent utilitzant QueryOver ---");

        var eCRUD = new EmpleadosCRUD();
        var emps = eCRUD.SelectByJobSortingBySalaryUsingQueryOver("VENDEDOR");

        foreach (var emp in emps)
        {
            Console.WriteLine("{0} - {1} - {2} - {3} - {4} - {5} - {6} - {7} - {8}",
                    emp.Id, emp.Empno, emp.Apellido, emp.Oficio, emp.Dir, emp.Fechaalt, emp.Salario, emp.Comision, emp.Departamento.Id);
        }
    }
    public static void Ex6()
    {
        Console.WriteLine("--- Ex6: Mostra el cognom, l’ofici i el salari de tots els empleats que el seu cognom comenci per «S» utilitzant HQL ---");

        var eCRUD = new EmpleadosCRUD();
        var emps = eCRUD.SelectBySurnameStartsWithUsingHql("S");

        foreach (var emp in emps)
        {
            Console.WriteLine(emp.Apellido + " - " + emp.Oficio + " - " + emp.Salario);
        }
    }
    public static void Ex7()
    {
        Console.WriteLine("--- Ex7: Mostra les dades dels departaments que estigui a SEVILLA o a BARCELONA utilitzant QueryOver ---");

        var dCRUD = new DepartamentosCRUD();
        var deps = dCRUD.SelectByLocationUsingQueryOver("SEVILLA", "BARCELONA");

        foreach (var dep in deps)
        {
            Console.WriteLine(dep.Id + " - " + dep.Dnombre + " - " + dep.Loc);
        }
    }
    public static void Ex8()
    {
        Console.WriteLine("--- Ex8: Mostra només els cognoms dels empleats que el seu salari estigui entre 2000 i 3500, els ordenes de foma ascendent per cognom i fes la projecció del cognom utilitzant QueryOver ---");

        var eCRUD = new EmpleadosCRUD();
        var emps = eCRUD.SelectBySalaryBetweenAndSortAscUsingQueryOver(2000, 3500);

        foreach (var emp in emps)
        {
            Console.WriteLine(emp);
        }
    }
    public static void Ex9()
    {
        Console.WriteLine("--- Ex9: Mostra els cognoms i els salaris dels empleats que el seu ofici sigui EMPLEADO i cobrin més de 1400. Utilitzant QueryOver ---");

        var eCRUD = new EmpleadosCRUD();
        var dadesEmps = eCRUD.SelectByJobAndSalaryBiggerThan("EMPLEADO", 1400);

        foreach (var emp in dadesEmps)
        {
            Console.WriteLine(emp[0] + " - " + emp[1]);
        }
    }
    public static void Ex10()
    {
        Console.WriteLine("--- Ex10: Mostra el cognom, l’ofici i el salari de l’empleat que tingui el salari més alt utilitzant les Subqueries del QueryOver ---");

        var eCRUD = new EmpleadosCRUD();
        var emp = eCRUD.SelectByBiggestSalaryUsingSubqueriesQueryOver();
        Console.WriteLine(emp.Apellido + " - " + emp.Salario);
    }
}