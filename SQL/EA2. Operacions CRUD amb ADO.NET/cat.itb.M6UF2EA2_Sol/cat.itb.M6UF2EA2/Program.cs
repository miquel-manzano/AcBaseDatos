using System;
using System.Collections.Generic;
using cat.itb.M6UF2EA2.cruds;
using cat.itb.M6UF2EA2.model;

namespace cat.itb.M6UF2EA2
{
    /// <summary>
    /// Classe on el programa comença
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Mètode on el programa comença
        /// </summary>
        public static void Main()
        {
            Start();
        }

        /// <summary>
        /// Mètode amb el loop inicial del programa
        /// </summary>
        public static void Start()
        {
            do
            {
                PrintMenu();
            } while (!ChooseOption());
        }

        /// <summary>
        /// Mètode que printa el menú
        /// </summary>
        public static void PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("    --- MENU ---");
            Console.ResetColor();
            Console.WriteLine("\n[1] Ex1 - Insert in producte (PS)" +
                              "\n[2] Ex2 - Insert in emp" +
                              "\n[3] Ex3 - Update limit in client" +
                              "\n[4] Ex4 - Show client with id 106" +
                              "\n[5] Ex5 - Show employee with id 7788" +
                              "\n[6] Ex6 - Show product with id 101860 (PS)" +
                              "\n[7] Ex7 - Show all surnames and salaries from 'emp' table" +
                              "\n[8] Ex8 - Delete client with code 109 (PS)" +
                              "\n[9] Ex9 - Delete emp with empNo 4885" +
                              "\n[10] Ex10 - Delete producte with prodNum 400552 (PS)" +
                              "\n[11] Run SQL script" +
                              "\n[12] Drop tables" +
                              "\n[0] Exit\n");
        }

        /// <summary>
        /// Mètode a travès del qual l'usuari escull l'opció que vol dur a terme
        /// </summary>
        /// <returns>Bool que decideix si el programa acabarà</returns>
        public static bool ChooseOption()
        {
            bool isValidOption;
            var clientCRUD = new ClientCRUD();
            var empleatCRUD = new EmpleatCRUD();
            var producteCRUD = new ProducteCRUD();
            var generalCRUD = new GeneralCRUD();
            
            do
            {
                string option = Console.ReadLine();
                isValidOption = true;
                switch (option)
                {
                    case "1":
                        PrintExercise(1);
            
                        var producte1 = new Producte
                        {
                            prodNum = 300388,
                            descripcio = "RH GUIDE TO PADDLE"
                        };

                        var producte2 = new Producte
                        {
                            prodNum = 400552,
                            descripcio = "RH GUIDE TO BOX"
                        };

                        var producte3 = new Producte
                        {
                            prodNum = 400333,
                            descripcio = "ACE TENNIS BALLS-10 PACK"
                        };

                        List<Producte> productes = new List<Producte>();
                        productes.Add(producte1);
                        productes.Add(producte2);
                        productes.Add(producte3);
                        producteCRUD.InsertIntoProducteWithPreparedStatement(productes);
                        break;
                    case "2":
                        PrintExercise(2);
                        var emp1 = new Empleat
                        {
                            empNo = 4885,
                            cognom = "BORREL",
                            ofici = "EMPLEAT",
                            cap = 7902,
                            dataAlta = new DateTime(1981, 12, 25).Date,
                            salari = 104000,
                            comissio = null,
                            deptNo = 30
                        };
                        
                        var ins1 = empleatCRUD.Insert(emp1);
                        if (ins1) Console.WriteLine("Emp with empNo {0} added", emp1.empNo);
                        else Console.WriteLine("Couldn't add emp with empNo {0}", emp1.empNo);
                   
                        var emp2 = new Empleat
                        {
                            empNo = 8772,
                            cognom = "PUIG",
                            ofici = "VENEDOR",
                            cap = 7698,
                            dataAlta = new DateTime(1990, 01, 23).Date,
                            salari = 108000,
                            comissio = null,
                            deptNo = 30
                        };
                        
                        var ins2 = empleatCRUD.Insert(emp2);
                        if (ins2) Console.WriteLine("Emp with empNo {0} added", emp2.empNo);
                        else Console.WriteLine("Couldn't add emp with empNo {0}", emp2.empNo);
            
                        var emp3 = new Empleat
                        {
                            empNo = 9945,
                            cognom = "FERRER",
                            ofici = "ANALISTA",
                            cap = 7698,
                            dataAlta = new DateTime(1988, 05, 17).Date,
                            salari = 169000,
                            comissio = 39000,
                            deptNo = 20
                        };
                        
                        var ins3 = empleatCRUD.Insert(emp3);
                        if (ins3) Console.WriteLine("Emp with empNo {0} added", emp3.empNo);
                        else Console.WriteLine("Couldn't add emp with empNo {0}", emp3.empNo);
                       break;
                    
                    case "3":
                        PrintExercise(3);
                        List<int> codis = new List<int>();
                        List<double> limits = new List<double>();
                        codis.Add(104);
                        codis.Add(106);
                        codis.Add(107);
                        
                        limits.Add(20000);
                        limits.Add(20000);
                        limits.Add(12000);
                    
                        clientCRUD.UpdateClientLimitCredit(codis,limits);
                        break;
                    case "4":
                        PrintExercise(4);
                        var client = clientCRUD.Select(109);
                        if (client is null)
                        {
                            Console.WriteLine("Client not found.");
                        }
                        else
                        {
                            Console.WriteLine("clientCod: {0}\nNom: {1}\nAdreça: {2}\nCiutat: {3}\nEstat: {4}\nCodiPostal: {5}\n" + "Area: {6}\nTelefon: {7}\nReprCod: {8}\nLimitCredit: {9}\nObservacions: {10}", 
                                client.clientCod, client.nom, client.adreca, client.ciutat, client.estat, client.codiPostal, client.Area, client.telefon, client.reprCod, client.limitCredit, client.observacions);
                        }
                        break;
                    case "5":
                        PrintExercise(5);
                        var emp = empleatCRUD.Select(7788);
                        if (emp is null)
                        {
                            Console.WriteLine("Employee not found.");
                        }
                        else
                        {
                            Console.WriteLine(
                                "empNo: {0}\nCognom: {1}\nOfici: {2}\nCap: {3}\nDataAlta: {4}\nSalari: {5}\n" +
                                "comissio: {6}\nDeptNo: {7}", emp.empNo, emp.cognom, emp.ofici, emp.cap, emp.dataAlta,
                               emp.salari, emp.comissio, emp.deptNo);

                        }

                        break;
                    case "6":
                        PrintExercise(6);
                        var prod = producteCRUD.Select(101860);
                        if (prod is null)
                        {
                            Console.WriteLine("Product not found.");
                        }
                        else
                        {
                            Console.WriteLine("prodNum: {0}\nDescripcio: {1}", prod.prodNum, prod.descripcio);
                        }
                        break;
                    case "7":
                        PrintExercise(7);
                        var emps = empleatCRUD.SelectAll(); 
                        foreach (Empleat empt in emps)
                        {
                           Console.WriteLine("cognom: {0}, salari: {1}", empt.cognom,empt.salari);
                        }
                        break;
                    case "8":
                        PrintExercise(8);
                        var del1 = clientCRUD.Delete(109);
                        if (del1) Console.WriteLine("Client deleted");
                        else Console.WriteLine("Client doesn't exist");
                        break;
                    case "9":
                        PrintExercise(9);
                        var del2 = empleatCRUD.Delete(4885);
                        if (del2) Console.WriteLine("Employee deleted");
                        else Console.WriteLine("Employee doesn't exist");
                        break;
                    case "10":
                        PrintExercise(10);
                        var del3 = producteCRUD.Delete(400552);
                        if (del3) Console.WriteLine("Product deleted");
                        else Console.WriteLine("Product doesn't exist");
                        break;
                    case "11":
                        Console.Clear();
                        generalCRUD.RunScriptEmpresa();
                        break;
                    case "12":
                        Console.Clear();
                        generalCRUD.DropTables(GetTablesToDrop());
                        break;
                    case "0":
                        return true;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("    -- Introduce a valid option\n");
                        Console.ResetColor();
                        isValidOption = false;
                        break;
                }
            } while (!isValidOption);

            PressKeyToMenu();
            
            return false;
        }

        /// <summary>
        /// Mètode per mostrar que s'ha de fer per tornar al menú
        /// </summary>
        public static void PressKeyToMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n    -- Press any key to return to the menu");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Mètode que printa el número d'exercici que s'està realitzant
        /// </summary>
        /// <param name="exerciseNumber">Int amb el número de l'exercici</param>
        public static void PrintExercise(int exerciseNumber)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("    --- Exercise {0}\n", exerciseNumber);
            Console.ResetColor();
        }
        

        /// <summary>
        /// Mètode que retorna les taules que s'han d'esborrar
        /// </summary>
        /// <returns>Lista amb els strings amb el nom de les taules</returns>
        public static List<string> GetTablesToDrop()
        {
            var tablesToDrop = new List<string>();
            tablesToDrop.Add("client");
            tablesToDrop.Add("comanda");
            tablesToDrop.Add("dept");
            tablesToDrop.Add("detall");
            tablesToDrop.Add("emp");
            tablesToDrop.Add("producte");

            return tablesToDrop;
        }
    }
}