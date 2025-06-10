using System.Net;
using System.Reflection.Emit;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using cat.itb.M6NF2Prac.models;
using cat.itb.M6NF2Prac_FinalRec.cruds;
using FluentNHibernate.Conventions;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("\n--- EXERCICIS DE LA PRACTICA ---" +
                               "\n[1] Exercici 1" +
                               "\n[2] Exercici 2" +
                               "\n[3] Exercici 3" +
                               "\n[4] Exercici 4" +
                               "\n[5] Exercici 5" +
                               "\n[6] Exercici 6" +
                               "\n[7] Exercici 7" +
                               "\n[8] Exercici 8" +
                               "\n[9] Exercici 9" +
                               "\n[10] Exercici 10" +
                               "\n[11] Exercici 11" +
                               "\n[12] Exercici 12" +
                               "\n[13] Exercici 13" +
                               "\n[14] Exercici 14" +
                               "\n--- EXERCICIS DEL EXAMEN ---" +
                               "\n[15] Exercici 1" +
                               "\n[16] Exercici 2" +
                               "\n[17] Exercici 3" +
                               "\n[18] Exercici 4" +
                               "\n[19] Exercici 5" +
                               "\n[20] Exercici 6" +
                               "\n[21] Exercici 8" +

                               "\n[50] Drop tables" +
                               "\n[60] Run script" +
                               "\n[0] Exit");

        int option = int.Parse(Console.ReadLine());
        var generalCRUD = new GeneralCRUD();

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
                Ex11();
                break;
            case 12:
                Ex12();
                break;
            case 13:
                Ex13();
                break;
            case 14:
                Ex14();
                break;
            case 15:
                ExamEx1();
                break;
            case 16:
                ExamEx2();
                break;
            case 17:
                ExamEx3();
                break;
            case 18:
                ExamEx4();
                break;
            case 19:
                ExamEx5();
                break;
            case 20:
                ExamEx6();
                break;
            case 21:
                ExamEx8();
                break;
            case 50:
                var tables = new List<string>
                {
                    "client",
                    "orderprod",
                    "product",
                    "provider",
                    "salesperson"
                };

                generalCRUD.DropTables(tables);
                break;
            case 60:
                generalCRUD.RunScriptStore();
                break;
        }
    }

    //Console.WriteLine("\n--- Ex1: aaa ---\n");

    public static void Ex1()
    {
        Console.WriteLine("\n--- Ex1: Insertar els següents nous clients a la taula CLIENT sense utilitzar el PreparedStatement. El mètode del ClientCRUD.cs es dirà InsertADO i rep com a paràmetre una llista d’objectes Client que crearàs en la classe Program.cs. Si s’han inserit correctament els clients el mètode InsertADO mostra a l’usuari el missatge ---\n");

        var clie1 = new Client
        {
            Code = 2998,
            Name = "Sun Systems",
            Credit = 4500
        };
        var clie2 = new Client
        {
            Code = 2677,
            Name = "Roxy Stars",
            Credit = 5000
        };
        var clie3 = new Client
        {
            Code = 2865,
            Name = "Clen Ferrant",
            Credit = 3000
        };
        var clie4 = new Client
        {
            Code = 2873,
            Name = "Roast Coast",
            Credit = 4500
        };


        var clies = new List<Client> { clie1, clie2, clie3, clie4 };

        var clientCRUD = new ClientCRUD();
        clientCRUD.InsertADO(clies);
    }
    public static void Ex2()
    {
        Console.WriteLine("\n--- Ex2: Eliminar el client anomenat \"Roast Coast\". Al Program.cs has d’obtenir l’objecte Client utilitzant el mètode de ClientCRUD.cs anomenat SelectByNameADO que rep com a paràmetre el nom del client. L’objecte Client obtingut s’ha de passar com a paràmetre al mètode DeleteADO implementat a la classe ClientCRUD.cs que farà l’eliminació de la taula CLIENT i mostrarà un missatge a l’usuari s’ha eliminat correctament el client amb id <Client.Id> ---\n");

        var clientCRUD = new ClientCRUD();
        var clie = clientCRUD.SelectByNameADO("Roast Coast");

        clientCRUD.DeleteADO(clie);
    }
    public static void Ex3()
    {
        Console.WriteLine("\n--- Ex3: Actualitzar el preu dels següents productes. Al Program.cs has d’obtenir cada objecte Product utilitzant el mètode de ProductCRUD.cs anomenat SelectByCodeADO que rep com a paràmetre el codi del producte i retorna l’objecte Product. Es modifica el preu i l’objecte modificat es passa com a paràmetre al mètode UpdateADO que fara la modificació a la taula PRODUCT i mostrarà un missatge a l’usuari si s’ha modificat correctament el producte ---\n");

        var productCRUD = new ProductCRUD();

        var prod = productCRUD.SelectByCodeADO(100890);
        prod.Price = 59.05m;
        productCRUD.UpdatePriceADO(prod);

        prod = productCRUD.SelectByCodeADO(200376);
        prod.Price = 25.56m;
        productCRUD.UpdatePriceADO(prod);

        prod = productCRUD.SelectByCodeADO(200380);
        prod.Price = 33.12m;
        productCRUD.UpdatePriceADO(prod);

        prod = productCRUD.SelectByCodeADO(100861);
        prod.Price = 17.34m;
        productCRUD.UpdatePriceADO(prod);
    }
    public static void Ex4()
    {
        Console.WriteLine("\n--- Ex4: Mostrar totes les dades dels poveïdors que el seu credit sigui inferior a 6000 . No es pot utilitzar el PreparedStatement. Al Program.cs has d’obtenir la llista d’objectes Provider utilitzant el mètode de ProviderCRUD.cs anomenat SelectCreditLowerThanADO que rep com a paràmetre el credit i retorna la llista d’objectes Provider que tenen el credit més petit del número passat com a paràmetre. Llavors es mostren totes les dades del proveïdors obtinguts per pantalla ---\n");

        var providerCRUD = new ProviderCRUD();
        var provs = providerCRUD.SelectCreditLowerThanADO(6000);

        foreach (var prov in provs)
        {
            Console.WriteLine($"\n--- Provider: {prov.Id} ---");
            Console.WriteLine($"Name: {prov.Name}, Address: {prov.Address}, City: {prov.City}, StateCode: {prov.StateCode}, ZipCode: {prov.ZipCode}, Area: {prov.Area}, Phone: {prov.Phone}, Product: {prov.Product.Id}, Amount: {prov.Amount}, Credit: {prov.Credit}\nRemark: {prov.Remark}");
        }
    }
    public static void Ex5()
    {
        Console.WriteLine("\n--- Ex5: Insertar els següents nous venedors a la taula SALESPERSON utilitzant el PreparedStatement. El mètode del SalespersonCRUD.cs es dirà InsertADO i rep com a paràmetre una llista d’objectes Salesperson que es crearan en la classe Program.cs. Si s’han inserit correctament els empleats el mètode InsertADO mostra a l’usuari el missatge «Venedors inserits correctament» ---\n");

        var sp1 = new Salesperson()
        {
            Surname = "WASHINGTON",
            Job = "MANAGER",
            StartDate = new DateTime(1974, 10, 1),
            Salary = 139000,
            Commission = 62000,
            Department = "REPAIR"
        };
        var sp2 = new Salesperson()
        {
            Surname = "FORD",
            Job = "ASSISTANT",
            StartDate = new DateTime(1985, 03, 25),
            Salary = 105000,
            Commission = 25000,
            Department = "REPAIR"
        };
        var sp3 = new Salesperson()
        {
            Surname = "FREEMAN",
            Job = "ASSISTANT",
            StartDate = new DateTime(1965, 09, 12),
            Salary = 90000,
            Commission = null,
            Department = "REPAIR"
        };
        var sp4 = new Salesperson()
        {
            Surname = "DAMON",
            Job = "ASSISTANT",
            StartDate = new DateTime(1995, 11, 15),
            Salary = 90000,
            Commission = null,
            Department = "WOOD"
        };

        var sps = new List<Salesperson> { sp1, sp2, sp3, sp4 };

        var salespersonCRUD = new SalespersonCRUD();
        salespersonCRUD.InsertADO(sps);
    }
    public static void Ex6()
    {
        Console.WriteLine("\n--- Ex6: aaa ---\n");

        var clientCRUD = new ClientCRUD();
        var clie = clientCRUD.SelectByName("Carter & Sons");

        int amount = 0;
        decimal totalCost = 0;

        var orders = clie.Orders;

        if (!orders.Any())
        {
            Console.WriteLine("No hi ha orders");
        }
        foreach (var or in orders)
        {
            ++amount;
            totalCost = totalCost + or.Cost;
        }

        Console.WriteLine($"El client amb id {clie.Id} ha realitzat {amount} comandes i s’ha gastat en total {totalCost}.");
    }
    public static void Ex7()
    {
        Console.WriteLine("\n--- Ex7: aaa ---\n");

        var salespersonCRUD = new SalespersonCRUD();
        var salesperson = salespersonCRUD.SelectBySurname("YOUNG");

        var products = salesperson.Products;

        if (!products.Any())
        {
            Console.WriteLine("No hi ha productes");
        }
        foreach (var product in products)
        {
            Console.WriteLine($"\n--- Informacio del proveidor del producte: {product.Code} ---");
            Console.WriteLine($"Nom: {product.Provider.Name}, Ciutat: {product.Provider.City}, Codi Postal: {product.Provider.ZipCode}, Telefon: {product.Provider.Phone}");
        }
    }
    public static void Ex8()
    {
        Console.WriteLine("\n--- Ex8: aaa ---\n");

        var orderCRUD = new OrderCRUD();
        var orders = orderCRUD.SelectByCostHigherThan(100, 12000);

        foreach (var order in orders)
        {
            Console.WriteLine($"Id: {order.Id}, Producte descripcio: {order.Product.Description}, Producte preu: {order.Product.Price}, Cost: {order.Cost}, Amount: {order.Amount}");
        }
    }
    public static void Ex9()
    {
        Console.WriteLine("\n--- Ex9: aaa ---\n");

        var providerCRUD = new ProviderCRUD();
        var provider = providerCRUD.SelectLowestAmount();

        Console.WriteLine($"Nom: {provider.Name}, Quantitat: {provider.Amount}, Descripcio producte: {provider.Product.Description}, Stock producte: {provider.Product.CurrentStock}");
    }
    public static void Ex10()
    {
        Console.WriteLine("\n--- Ex10: aaa ---\n");

        var product1 = new Product
        {
            Code = 1001,
            Description = "Tòner làser HP 305A negre",
            CurrentStock = 25,
            MinStock = 10,
            Price = 59.99m,
            Salesperson = null,
            Provider = null
        };

        var product2 = new Product
        {
            Code = 1002,
            Description = "Pack 5 llibretes A4 Oxford",
            CurrentStock = 100,
            MinStock = 30,
            Price = 12.50m,
            Salesperson = null,
            Provider = null
        };

        var productCRUD = new ProductCRUD();
        productCRUD.Insert(product1);
        productCRUD.Insert(product2);

        var provider1 = new Provider
        {
            Name = "Ofimarket S.A.",
            Address = "Carrer Gran Via, 123",
            City = "Barcelona",
            StateCode = "ES",
            ZipCode = "08010",
            Area = 93,
            Phone = "933456789",
            Product = product1,
            Amount = 50,
            Credit = 5000.00m,
            Remark = "Subministra mensualment tòners HP"
        };

        var provider2 = new Provider
        {
            Name = "Papereria Global",
            Address = "Av. Diagonal, 456",
            City = "Lleida",
            StateCode = "ES",
            ZipCode = "25001",
            Area = 97,
            Phone = "973112233",
            Product = product2,
            Amount = 200,
            Credit = 10000.00m,
            Remark = "Especialistes en material escolar i d’oficina"
        };

        var providerCRUD = new ProviderCRUD();
        providerCRUD.Insert(provider1);
        providerCRUD.Insert(provider2);
    }
    public static void Ex11()
    {
        Console.WriteLine("\n--- Ex11: aaa ---\n");

        var clientCRUD = new ClientCRUD();
        var clies = clientCRUD.SelectAllNadiu();

        foreach (var clie in clies)
        {
            Console.WriteLine($"ID: {clie.Id}, Codi: {clie.Code}, Nom: {clie.Name}, Credit: {clie.Credit}");
        }
    }
    public static void Ex12()
    {
        Console.WriteLine("\n--- Ex12: aaa ---\n");

        var providerCRUD = new ProviderCRUD();
        var providers = providerCRUD.SelectByCity("BELMONT");

        foreach (var provider in providers)
        {
            provider.Credit = 25000;
            providerCRUD.Update(provider);
        }
    }
    public static void Ex13()
    {
        Console.WriteLine("\n--- Ex13: aaa ---\n");

        var productsCRUD = new ProductCRUD();
        var productsData = productsCRUD.SelectByPriceHigherThan(100);

        foreach (var product in productsData)
        {
            Console.WriteLine($"Descripcio: {product[0]}, Price: {product[1]}");
        }
    }
    public static void Ex14()
    {
        Console.WriteLine("\n--- Ex14: aaa ---\n");

        var clientCRUD = new ClientCRUD();
        var clies = clientCRUD.SelectByCreditHigherThan(50000);

        foreach (var clie in clies)
        {
            Console.WriteLine($"Nom: {clie.Name}, Credit: {clie.Credit}");
        }
    }

    public static void ExamEx1()
    {
        Console.WriteLine("\n--- Ex1 ---\n");

        var pCRUD = new ProviderCRUD();
        var provs = pCRUD.SelectByCityADO("BELMONT");

        foreach (var prov in provs)
        {
            prov.Amount = 550;
            prov.Credit = 30000;
            pCRUD.UpdateADO(prov);
        }
    }

    public static void ExamEx2()
    {
        Console.WriteLine("\n--- Ex2 ---\n");

        var pCRUD = new ProductCRUD();
        var filtredProducts = pCRUD.SelectByHighPriceADO(250);

        foreach (var pro in filtredProducts)
        {
            Console.WriteLine($"Id: {pro.Id}, Code: {pro.Code}, Des: {pro.Description}, CurrentStock: {pro.CurrentStock}, MinStock: {pro.MinStock}, Price: {pro.Price}, Salesp: {pro.Salesperson}");
        }
    }

    public static void ExamEx3()
    {
        var sCRUD = new SalespersonCRUD();
        
        var MyProcuct = new Product
        {
            Code = 999999,
            Description = "Nintendo Switch 2",
            CurrentStock = 2,
            MinStock = 1,
            Price = 500,
            Salesperson = sCRUD.SelectBySurenameADO("REYES")
        };

        var pCRUD = new ProductCRUD();
        pCRUD.InsertADO(MyProcuct);

        var MyProvider = new Provider
        {
            Name = "Paco",
            Address = "Mi casa",
            City = "Barcelona",
            StateCode = "ES",
            ZipCode = "08016",
            Area = 222,
            Phone = "644613331",
            Product = pCRUD.SelectByCodeADO(999999),
            Amount = 2,
            Credit = 50000,
            Remark = "Nothing"
        };

        var provCRUD = new ProviderCRUD();
        provCRUD.InserADO(MyProvider);
    }

    public static void ExamEx4()
    {
        var pCRUD = new ProductCRUD();
        List<Product> products = pCRUD.SelectBySalesSurnameADO("REYES");

        foreach (var p in products)
        {
            Console.WriteLine($"Code: {p.Code}, Price: {p.Price}");
        }
    }

    public static void ExamEx5()
    {
        var oCRUD = new OrderCRUD();
        var orders = oCRUD.SelectAllHQL();

        foreach (var o in orders)
        {
            Console.WriteLine($"Id: {o.Id}, Product: {o.Product.Description}, Client: {o.Client.Name}, Date: {o.OrderDate}, Amount: {o.Amount}, Deliveri: {o.DeliveryDate}, Cost: {o.Cost}");
        }
    }

    public static void ExamEx6()
    {
        var oCRUD = new OrderCRUD();
        var order = oCRUD.SelectLowAmount();

        Console.WriteLine($"Ordre\nId: {order.Id}, Amount: {order.Amount}, Cost: {order.Cost}\nProducte:\nCode: {order.Product.Code}, Price: {order.Product.Price}");
    }
    public static void ExamEx8()
    {
        var cCRUD = new ClientCRUD();
        var clients = cCRUD.SelectByCreditLowerThan(30000);

        foreach (var c in clients)
        {
            Console.WriteLine($"Code: {c[0]}, Name: {c[1]}, Credit:{c[2]}");
        }
    }
}