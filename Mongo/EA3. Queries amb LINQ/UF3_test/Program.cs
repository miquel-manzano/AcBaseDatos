using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Nodes;
using System.Text.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using UF3_test.connections;
using UF3_test.model;
using static System.Formats.Asn1.AsnWriter;
using UF3_test.cruds;
using System.ComponentModel;

namespace UF3_test
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Exercici1:" +
                          "\n   [1] LoadBooksCollection" +
                          "\n   [2] LoadProductsCollection" +
                          "\nExercici2:" +
                          "\n   [3] DropCollection" +
                          "\nExercici3" +
                          "\n   [4] SelectMoreExpensiveProduct" +
                          "\n   [5] SelectTotalStock" +
                          "\n   [6] SelectBooksByPageCountInterval" +
                          "\n   [7] SelectBooksByAuthors" +
                          "\n   [8] SelectJustTitleAnsStatusBooks" +
                          "\n   [9] SelectTitleAndCategsSortedByNumPages" +
                          "\n   [10] SelectBooksByAuthor" +
                          "\n   [11] SelectBooksByCategNotAuthor" +
                          "\n\n[0] Exit");

            int option = int.Parse(Console.ReadLine());
            var loadCollection = new LoadCollectionsCRUD();
            var booksCRUD = new BooksCrud();
            var productsCRUD = new ProductsCrud();

            switch (option)
            {
                case 0:
                    Console.WriteLine("Good night...");
                    break;
                case 1:
                    loadCollection.LoadBooksCollection();
                    break;
                case 2:
                    loadCollection.LoadProductsCollection();
                    break;              
                case 3:
                    DropCollection("itb", GetStringPrintInstructions());
                    break;
                case 4:
                    productsCRUD.SelectMoreExpensiveProduct();
                    break;
                case 5:
                    productsCRUD.SelectTotalStock();
                    break;
                case 6:
                    booksCRUD.SelectBooksByPageCountInterval(300, 350, "Java");
                    break;
                case 7:
                    booksCRUD.SelectBooksByAuthors(new[] { "Charlie Collins", "Robi Sen" });
                    break;
                case 8:
                    booksCRUD.SelectJustTitleAnsStatusBooks();
                    break;
                case 9:
                    booksCRUD.SelectTitleAndCategsSortedByNumPages();
                    break;
                case 10:
                    booksCRUD.SelectBooksByAuthor("Danno Ferrin");
                    break;
                case 11:
                    booksCRUD.SelectBooksByCategNotAuthor("Java", "Vikram Goyal");
                    break;
            }
        }
        public static void DropCollection(string database, string collection)
        {
            //var db = MongoClusterConnection.GetDatabase(database);
            var db = MongoLocalConnection.GetDatabase("itb");
            Console.WriteLine("S'eliminaràn {0} documents",
                db.GetCollection<Object>(collection).CountDocuments(new BsonDocument()));
            db.DropCollection(collection);
            Console.WriteLine("Resten les següents col·leccions:");
            foreach (var collectionName in db.ListCollectionNames().ToList())
                Console.WriteLine("     " + collectionName);
        }

        public static string GetStringPrintInstructions()
        {
            Console.WriteLine("Introdueix el nom de la col·lecció que vols esborrar:");
            return Console.ReadLine();
        }
    }
}