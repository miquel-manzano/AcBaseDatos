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
            Console.WriteLine("\nExercici1" +
                              "\n   [1] LoadCountriesCollection" +
                              "\n   [2] LoadProductsCollection" +
                              "\n   [3] LoadPeopleCollection" +
                              "\n   [4] LoadBooksCollection" +
                              "\n   [5] LoadRestaurantsCollection" +
                              "\nExercici2" +
                              "\n   [6] CountPopulationByRegion" +
                              "\n   [7] SelectCountryCapitalPopulationLatLngByName" +
                              "\n   [8] SelectAllPrintTitlePagesAndCategoriesSortByPgNoPages" +
                              "\n   [9] SelectByZipcodePrintNameAndCuisine" +
                              "\n   [10] SelectByBoroughAndCuisinePrintAllData" +
                              "\n   [11] SelectLessThan130PagesPrintTitlePgCountAndAutors" +
                              "\n   [12] SelectByNamePrintFriendsName" +
                              "\nExercici3" +
                              "\n   [13] UpdateZipcodeWhereStreetEq" +
                              "\n   [14] UpdateProductsAddNewFieldWherePriceGreaterThan" +
                              "\n   [15] UpdateBooksAddAuthorByName" +
                              "\n   [16] UpdateProductsAddGammaField" +
                              "\n   [17] UpdateProductCategoryByName" +
                              "\n   [18] UpdateStockWherePriceBetween" +
                              "\n   [19] UpdateCountryCallingCodesByName" +
                              "\nExercici4" +
                              "\n   [20] DeleteRestaurantsFromBorough" +
                              "\n   [21] DeleteFirstCategoryFromProductWithName" +
                              "\n   [22] DeleteBooksByPageCountBetween" +
                              "\n   [23] DeleteProductWithNameEq" +
                              "\n   [24] DeleteBookLastCategoryByISBN" +
                              "\n   [25] DeleteProductsByCategory" +
                              "\n   [26] DeleteFieldByRandomArrayItem" +
                              "\n   [27] DeleteCountriesBySpeakingLanguage" +
                              "\nExercici5" +
                              "\n   [28] DropCollection" +
                              "\n\n[0] Exit");

            int option = int.Parse(Console.ReadLine());
            var loadCollection = new LoadCollectionsCRUD();
            var booksCRUD = new BooksCrud();
            var countriesCRUD = new CountriesCrud();
            var peopleCRUD = new PeopleCrud();
            var productsCRUD = new ProductsCrud();
            var restaurantsCRUD = new RestaurantsCrud();

            switch (option)
            {
                case 0:
                    Console.WriteLine("Good night...");
                    break;
                case 1:
                    loadCollection.LoadCountriesCollection();
                    break;
                case 2:
                    loadCollection.LoadProductsCollection();
                    break;
                case 3:
                    loadCollection.LoadPeopleCollection();
                    break;
                case 4:
                    loadCollection.LoadBooksCollection();
                    break;
                case 5:
                    loadCollection.LoadRestaurantsCollection();
                    break;
                case 6:
                    countriesCRUD.CountPopulationByRegion("Europe");
                    break;
                case 7:
                    countriesCRUD.SelectCountryCapitalPopulationLatLngByName("Madagascar");
                    break;
                case 8:
                    booksCRUD.SelectAllPrintTitlePagesAndCategoriesSortByPgNoPages();
                    break;
                case 9:
                    restaurantsCRUD.SelectByZipcodePrintNameAndCuisine("10019");
                    break;
                case 10:
                    restaurantsCRUD.SelectByBoroughAndCuisinePrintAllData("Bronx", "Chinese");
                    break;
                case 11:
                    booksCRUD.SelectLessThanAnyPagesPrintTitlePgCountAndAutors(130);
                    break;
                case 12:
                    peopleCRUD.SelectByNamePrintFriendsName("Caroline Webster");
                    break;
                case 13:
                    restaurantsCRUD.UpdateZipcodeWhereStreetEq("Driggs Avenue", "10443");
                    break;
                case 14:
                    productsCRUD.UpdateProductsAddNewFieldWherePriceGreaterThan("stockminim", 20, 2000);
                    break;
                case 15:
                    booksCRUD.UpdateBooksAddAuthorByName("Code Generation in Action", "Sam Watters");
                    break;
                case 16:
                    productsCRUD.UpdateProductsAddGammaField();
                    break;
                case 17:
                    productsCRUD.UpdateProductCategoryByName("MacBook Pro", "notebook", "ipad");
                    break;
                case 18:
                    productsCRUD.UpdateStockWherePriceBetween(800, 1000, 60);
                    break;
                case 19:
                    countriesCRUD.UpdateCountryCallingCodesByName("Iceland", "356");
                    break;
                case 20:
                    restaurantsCRUD.DeleteRestaurantsFromBorough("Manhattan");
                    break;
                case 21:
                    productsCRUD.DeleteFirstCategoryFromProductWithName("iPhone 7");
                    break;
                case 22:
                    booksCRUD.DeleteBooksByPageCountBetween(0, 100);
                    break;
                case 23:
                    productsCRUD.DeleteProductWithNameEq("Apple TV");
                    break;
                case 24:
                    booksCRUD.DeleteBookLastCategoryByISBN("1933988177");
                    break;
                case 25:
                    productsCRUD.DeleteProductsByCategory("phone"); ;
                    break;
                case 26:
                    peopleCRUD.DeleteFieldByRandomArrayItem("tags", "teacher");
                    break;
                case 27:
                    countriesCRUD.DeleteCountriesBySpeakingLanguage("Spanish");
                    break;
                case 28:
                    DropCollection("itb", GetStringPrintInstructions());
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