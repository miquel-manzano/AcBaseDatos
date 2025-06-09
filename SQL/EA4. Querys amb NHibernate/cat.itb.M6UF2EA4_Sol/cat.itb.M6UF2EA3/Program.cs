using System;
using System.Collections.Generic;
using cat.itb.M6UF2EA3.cruds;
using cat.itb.M6UF2EA3.model;

namespace cat.itb.M6UF2EA3
{
    internal class Program
    {
        public static void Main()
        {
            Start();
        }

        public static void Start()
        {
            do
            {
                PrintMenu();
            } while (!Menu());
        }
        
        public static void PrintMenu()
        {
            Console.WriteLine("\n[1] PrintAllDepartamentsDataUsingHql" +
                              "\n[2] PrintAllEmployeesDataUsingCriteria" +
                              "\n[3] PrintSurnameAndSalariesEmployeesWithSalaryGreaterThanUsingCriteria" +
                              "\n[4] PrintDepartamentLocUsingHql" +
                              "\n[5] PrintEmployeesDataWithJobVendedorSortingForSalaryDescUsingQueryOver" +
                              "\n[6] PrintSurnameJobSalaryEmployeesStartingWithSUsingHql" +
                              "\n[7] PrintDepartamentDataLocatedInSevillaOrBarcelona" +
                              "\n[8] PrintSurnameWhoseSalaryIsBetween2000And3500SortingBySurnameAscUsingQueryOver" +
                              "\n[9] PrintSurnameAndSalaryEmpsJobIsEmpleadoAndSalaryBiggerThan1400UsingQueryOver" +
                              "\n[10] PrintSurnameJobAndSalaryEmpsWithBiggerSalaryUsingSubqueriesQueryOver" +
                              "\n[0] Exit" +
                              "\n[11] Drop tables" +
                              "\n[12] Run script");
        }
        
        public static bool Menu()
        {
            string option;
            bool isValidOption;
            var gCRUD = new GeneralCRUD();
            do
            {
                option = Console.ReadLine();
                Console.WriteLine("\n");
                isValidOption = true;
                switch (option)
                {
                    case "1":
                        PrintAllDepartamentsDataUsingHql();
                        break;
                    case "2":
                        PrintAllEmployeesDataUsingCriteria();
                        break;
                    case "3":
                        PrintSurnameAndSalariesEmployeesWithSalaryGreaterThanUsingCriteria();
                        break;
                    case "4":
                        PrintDepartamentLocUsingHql();
                        break;
                    case "5":
                        PrintEmployeesDataWithJobVendedorSortingForSalaryDescUsingQueryOver();
                        break;
                    case "6":
                        PrintSurnameJobSalaryEmployeesStartingWithSUsingHql();
                        break;
                    case "7":
                        PrintDepartamentDataLocatedInSevillaOrBarcelona();
                        break;
                    case "8":
                        PrintSurnameWhoseSalaryIsBetween2000And3500SortingBySurnameAscUsingQueryOver();
                        break;
                    case "9":
                        PrintSurnameAndSalaryEmpsWhoseJobIsEmpleadoAndSalaryBiggerThan1400UsingQueryOver();
                        break;
                    case "10":
                        PrintSurnameJobAndSalaryFromEmpsWithBiggerSalaryUsingSubqueriesQueryOver();
                        break;
                    case "11":
                        gCRUD.DropTables(GetTablesToDrop());
                        break;
                    case "12":
                        gCRUD.RestoreHR2DBSession();
                        break;
                    case "0":
                    return true;
                    default:
                        isValidOption = false;
                        Console.WriteLine("Introdueix una opció vàlida:");
                        break;
                }

            } while (!isValidOption);
            
            return false;
        }
        
        //Exercici 1
        public static void PrintAllDepartamentsDataUsingHql()
        {
            Console.WriteLine("Ex1: Mostrar les dades de tots els departaments utiltzant HQL");
            var dCRUD = new DepartamentosCRUD();
            var deps = dCRUD.SelectAllHql();
            foreach (var dep in deps)
                Console.WriteLine(dep.Id + " - " + dep.Dnombre + " - " + dep.Loc);
        }

        //Exercici 2
        public static void PrintAllEmployeesDataUsingCriteria()
        {
            Console.WriteLine("Ex2: Mostrar les dades de tots els empleats utiltzant Criteria.");
            var eCRUD = new EmpleadosCRUD();
            var emps = eCRUD.SelectAllUsingCriteria();
            foreach (var emp in emps)
                Console.WriteLine("{0} - {1} - {2} - {3} - {4} - {5} - {6} - {7} - {8}",
                    emp.Id, emp.Empno, emp.Apellido, emp.Oficio, emp.Dir, emp.Fechaalt,
                    emp.Salario, emp.Comision, emp.Departamento.Id);
        }

        //Exercici 3
        public static void PrintSurnameAndSalariesEmployeesWithSalaryGreaterThanUsingCriteria()
        {
            Console.WriteLine("Ex3: Mostrar el cognom i salari dels empleats que cobrin més de 2000 utilitzant Criteria.");
            var eCRUD = new EmpleadosCRUD();
            var emps = eCRUD.SelectBySalaryBiggerThanUsingCriteria(2000);
            foreach (var emp in emps)
                Console.WriteLine(emp.Apellido + " - " + emp.Salario);
        }

        //Exercici 4
        public static void PrintDepartamentLocUsingHql()
        {
            Console.WriteLine("Ex4: Mostrar la localització del departament de VENTAS utiltzant HQL.");
            var dCRUD = new DepartamentosCRUD();
            var dep = dCRUD.SelectByNameHql("VENTAS");
            Console.WriteLine(dep.Loc);
        }

        //Exercici 5
        public static void PrintEmployeesDataWithJobVendedorSortingForSalaryDescUsingQueryOver()
        {
            Console.WriteLine("Ex5: Mostrar les dades dels empleats que el seu ofici sigui VENDEDOR i els ordenes per salari de forma descendent utilitzant QueryOver.");
            var eCRUD = new EmpleadosCRUD();
            var emps = eCRUD.SelectByJobSortingBySalaryUsingQueryOver("VENDEDOR");
            foreach (var emp in emps)
                Console.WriteLine("{0} - {1} - {2} - {3} - {4} - {5} - {6} - {7} - {8}",
                    emp.Id, emp.Empno, emp.Apellido, emp.Oficio, emp.Dir, emp.Fechaalt,
                    emp.Salario, emp.Comision, emp.Departamento.Id);
        }

        //Exercici 6
        public static void PrintSurnameJobSalaryEmployeesStartingWithSUsingHql()
        {
            Console.WriteLine("Ex6: Mostra el cognom, l’ofici i el salari de tots els empleats que el seu cognom comenci per 'S' utilitzant HQL.");
            var eCRUD = new EmpleadosCRUD();
            var emps = eCRUD.SelectBySurnameStartsWithUsingHql("S");
            foreach (var emp in emps)
                Console.WriteLine(emp.Apellido + " - " + emp.Oficio + " - " + emp.Salario);
        }

       //Exercici 7
        public static void PrintDepartamentDataLocatedInSevillaOrBarcelona()
        {
            Console.WriteLine("Ex7: Mostra les dades dels departaments que estigui a SEVILLA o a BARCELONA utilitzant QueryOver.");
            var dCRUD = new DepartamentosCRUD();
            var deps = dCRUD.SelectByLocationUsingQueryOver("SEVILLA","BARCELONA");
            foreach (var dep in deps)
                Console.WriteLine(dep.Id + " - " + dep.Dnombre + " - " + dep.Loc);
        }

        //Exercici 8
        public static void PrintSurnameWhoseSalaryIsBetween2000And3500SortingBySurnameAscUsingQueryOver()
        {
            Console.WriteLine("Ex8: Mostra només els cognoms dels empleats que el seu salari estigui entre 2000 i 3500, els ordenes de foma ascendent per cognom i fes la projecció del cognom utilitzant QueryOver.");
            var eCRUD = new EmpleadosCRUD();
            var emps = eCRUD.SelectBySalaryBetweenAndSortAscUsingQueryOver(2000, 3500);
            foreach (var emp in emps)
                Console.WriteLine(emp);
        }
        
        //Exercici 9
        public static void PrintSurnameAndSalaryEmpsWhoseJobIsEmpleadoAndSalaryBiggerThan1400UsingQueryOver()
        {
            Console.WriteLine("Ex9: Mostra els cognoms i els salaris dels empleats que el seu ofici sigui EMPLEADO i cobrin més de 1400 utilitzant QueryOver.");
            var eCRUD = new EmpleadosCRUD();
            var dadesEmps = eCRUD.SelectByJobAndSalaryBiggerThan("EMPLEADO", 1400);
            foreach (var emp in dadesEmps)
                Console.WriteLine(emp[0] + " - " + emp[1]);
        }

        //Exercici 10
        public static void PrintSurnameJobAndSalaryFromEmpsWithBiggerSalaryUsingSubqueriesQueryOver()
        {
            Console.WriteLine("Ex10: Mostra el cognom, l’ofici i el salari de l’empleat que tingui el salari més alt utilitzant les Subqueries del QueryOver");
            var eCRUD = new EmpleadosCRUD();
            var emp = eCRUD.SelectByBiggestSalaryUsingSubqueriesQueryOver();
            Console.WriteLine(emp.Apellido + " - " + emp.Salario);
        }
        
        public static List<string> GetTablesToDrop()
        {
            var tablesToDrop = new List<string>();
            tablesToDrop.Add("departamentos");
            tablesToDrop.Add("empleados");

            return tablesToDrop;
        }
    }
}