//Npgsql .NET Data Provider for PostgreSQL
using UF2_test.cruds;
using UF2_test.model; 

class Program
{
   public static void Main(string[] args)
    {
        //CRUDs
        var clientCRUD = new ClientCRUD();
        var empleatCRUD = new EmpleatCRUD();
        var producteCRUD = new ProducteCRUD();
        var generalCRUD = new GeneralCRUD();


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


        int option = int.Parse(Console.ReadLine());

        switch (option)
        {
            case 0:
                break;
            case 1:
                Console.WriteLine("--- Ex1: Insertar els següents nous productes a la base de dades utilitzant el PreparedStatement ---");

                var producte1 = new Producte
                {
                    prodNum = 300388,
                    descripcio = "producte1"
                };

                var producte2 = new Producte
                {
                    prodNum = 400552,
                    descripcio = "producte2"
                };

                var producte3 = new Producte
                {
                    prodNum = 400333,
                    descripcio = "producte3"
                };

                var productes = new List<Producte> { producte1, producte2, producte3};

                producteCRUD.InsertPS(productes);

                break;
            case 2:
                Console.WriteLine("--- Ex2: Insertar els següents nous empleats a la base de dades ---");

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

                empleatCRUD.Insert(emp1);
                empleatCRUD.Insert(emp2);
                empleatCRUD.Insert(emp3);
                break;
            case 3:
                Console.WriteLine("--- Ex3: Actualitzar el límit de crèdit dels següents clients utilitzant el PreparedStatement ---");

                clientCRUD.UpdateClientLimitCreditPS(104, 20000);
                clientCRUD.UpdateClientLimitCreditPS(106, 20000);
                clientCRUD.UpdateClientLimitCreditPS(107, 12000);
                break;
            case 4:
                Console.WriteLine("--- Ex4: Mostrar totes les dades del client 106 ---");
                var clie = clientCRUD.Select(106);

                if (clie is null)
                {
                    Console.WriteLine("Client not found.");
                }
                else
                {
                    Console.WriteLine("clientCod: {0}\nNom: {1}\nAdreça: {2}\nCiutat: {3}\nEstat: {4}\nCodiPostal: {5}\n" + "Area: {6}\nTelefon: {7}\nReprCod: {8}\nLimitCredit: {9}\nObservacions: {10}",
                        clie.clientCod, clie.nom, clie.adreca, clie.ciutat, clie.estat, clie.codiPostal, clie.Area, clie.telefon, clie.reprCod, clie.limitCredit, clie.observacions);
                }
                break;
            case 5:
                Console.WriteLine("--- Ex5: Mostrar totes les dades de l’empleat 7788 ---");
                var emp = empleatCRUD.SelectPS(7788);

                if (emp is null)
                {
                    Console.WriteLine("Employee not found.");
                }
                else
                {
                    Console.WriteLine("empNo: {0}\nCognom: {1}\nOfici: {2}\nCap: {3}\nDataAlta: {4}\nSalari: {5}\ncomissio: {6}\nDeptNo: {7}",
                        emp.empNo, emp.cognom, emp.ofici, emp.cap, emp.dataAlta, emp.salari, emp.comissio, emp.deptNo);
                }
                break;
            case 6:
                Console.WriteLine("--- Ex6: Mostrar totes les dades del producte 101860 utilitzant el PreparedStatement ---");
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
            case 7:
                Console.WriteLine("--- Ex7: Mostrar els cognoms i el salari de tots els empleats ---");

                var emps = empleatCRUD.SelectAll();

                foreach (Empleat empt in emps)
                {
                    Console.WriteLine("cognom: {0}, salari: {1}", empt.cognom, empt.salari);
                }
                break;
            case 8:
                Console.WriteLine("--- Ex8: Eliminar el client amb codi 109 utilitzant el PreparedStatement ---");
                clientCRUD.DeletePS(109);
                break;
            case 9:
                Console.WriteLine("--- Ex9: Eliminar l’empleat amb codi 4885 ---");
                empleatCRUD.Delete(4885);
                break;
            case 10:
                Console.WriteLine("--- Ex10: Eliminar el producte 400552 utilitzant el PreparedStatement ---");
                producteCRUD.DeletePS(400552);
                break;
            case 11:
                Console.Clear();
                generalCRUD.RunScriptEmpresa();
                break;
            case 12:
                var tablesToDrop = new List<string>
                {
                    "client",
                    "comanda",
                    "dept",
                    "detall",
                    "emp",
                    "producte"
                };
                generalCRUD.DropTables(tablesToDrop);
                break;
        }
    }
}


