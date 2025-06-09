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
            Console.WriteLine("\n[1] InsertIntoDepartamentos" +
                              "\n[2] InsertIntoEmpleados" +
                              "\n[3] UpdateDepartment" +
                              "\n[4] UpdateEmployee" +
                              "\n[5] DeleteEmployee" +
                              "\n[6] PrintEmpleatWhenSalaryLowerThan2500" +
                              "\n[7] PrintDepartamentNameOfEmployeetWithId8" +
                              "\n[8] PrintEmployeesOfDepartamentId3" +
                              "\n[9] Drop tables" +
                              "\n[10] Run script" +
                              "\n[0] Exit");
        }

        public static bool Menu()
        {
            string option;
            bool isValidOption;
            var generalCrud = new GeneralCrud();
            do
            {
                option = Console.ReadLine();
                Console.WriteLine("\n");
                isValidOption = true;
                switch (option)
                {
                    case "1":
                        InsertDepartments();
                        break;
                    case "2":
                        InsertEmployees();
                        break;
                    case "3":
                        UpdateDepartment();
                        break;
                    case "4": 
                        UpdateEmployee();
                        break;
                    case "5":
                        DeleteEmpleat();
                        break;
                    case "6":
                        PrintEmpleatWhenSalaryLowerThan2500();
                        break;
                    case "7": 
                        PrintDepartamentOfEmployeeId8();
                        break;
                    case "8": 
                        PrintEmployeesFromDepartamentId3();
                        break;
                    case "9":
                        generalCrud.DropTables(GetTablesToDrop());
                        break;
                    case "10":
                        generalCrud.RestoreHR2DBSession();
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
        public static void InsertDepartments()
        {
            Console.WriteLine("Ex:1 Inserim tres departaments nous");
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

            var depCRUD = new DepartamentosCRUD();
            depCRUD.Insert(dep1);
            depCRUD.Insert(dep2);
            depCRUD.Insert(dep3);
        }
        
        //Exercici 2
        
        public static void InsertEmployees()
        {
            Console.WriteLine("Ex:2 Inserim dos empleats");
            var emp1 = new Empleado
            {
                Empno= 8000,
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

        //Exercici 3
        public static void UpdateDepartment()
        {
            Console.WriteLine("Ex:3 Actualitzem un departament");
            var depCRUD = new DepartamentosCRUD();
            Departamento dep = depCRUD.SelectById(3);
            dep.Dnombre = "MARKETING";
            depCRUD.Update(dep);
        }

        //Exercici 4
        public static void UpdateEmployee()
        {
            Console.WriteLine("Ex:4 Actualitzem un empleat");
            var eCRUD = new EmpleadosCRUD();
            var emp = eCRUD.SelectByEmpno(7839);
            emp.Salario = 5500;
            eCRUD.Update(emp);
        }

        //Exercici 5
        public static void DeleteEmpleat()
        {
            Console.WriteLine("Ex:5 Eliminem un empleat");
            var empCRUD = new EmpleadosCRUD();
            var emp = empCRUD.SelectBySurname("NEGRO");
            empCRUD.Delete(emp);
        }

        //Exercici 6
        public static void PrintEmpleatWhenSalaryLowerThan2500()
        {
            Console.WriteLine("Ex:6 Empleats que cobren menys de 2500");
            var empCRUD = new EmpleadosCRUD();
            IList<Empleado> emps = empCRUD.SelectBySalaryLowerThan(2500);
            foreach (var emp in emps) 
                Console.WriteLine(emp.Apellido + " - " + emp.Salario);
        }
        
        //Exercici 7
        public static void PrintDepartamentOfEmployeeId8()
        {
            Console.WriteLine("Ex:7 Nom i ciutat del departament de l'empleat amb Id igual a 8");
            var eCRUD = new EmpleadosCRUD();
            Empleado emp = eCRUD.SelectById(8);
            Console.WriteLine(emp.Departamento.Dnombre + "  " + emp.Departamento.Loc);
        }

        //Exercici 8
        public static void PrintEmployeesFromDepartamentId3()
        {
            Console.WriteLine("Ex:8 Empleats del departament amb Id igual a 3");
            var depCRUD = new DepartamentosCRUD();
            var deps = depCRUD.SelectById(3);
            foreach (var emp in deps.Empleados)
                Console.WriteLine(emp.Apellido + " - " + emp.Oficio + " - " + emp.Salario);
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