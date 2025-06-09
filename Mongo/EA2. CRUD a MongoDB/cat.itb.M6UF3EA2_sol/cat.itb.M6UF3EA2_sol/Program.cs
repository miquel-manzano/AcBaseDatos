using System;
using System.Text.Json;
using cat.itb.M6UF3EA2_sol.connections;
using cat.itb.M6UF3EA2_sol.cruds;
using MongoDB.Bson;
using MongoDB.Driver;

namespace cat.itb.M6UF3EA2_sol
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
            } while (!ChooseOption());
        }

        // Els exercicis estàn ordenats tal com els de la tasca
        public static void PrintMenu()
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
        }

        public static bool ChooseOption()
        {
            var countriesCrud = new CountriesCrud();
            var peopleCrud = new PeopleCrud();
            var restaurantsCrud = new RestaurantsCrud();
            var booksCrud = new BooksCrud();
            var productsCrud = new ProductsCrud();
            bool isValidOption;
            do
            {
                isValidOption = true;
                switch (Console.ReadLine())
                {
                    case "1":
                        countriesCrud.LoadCountriesCollection();
                        break;
                    case "2":
                        productsCrud.LoadProductsCollection();
                        break;
                    case "3":
                        peopleCrud.LoadPeopleCollection();
                        break;
                    case "4":
                        booksCrud.LoadBooksCollection();
                        break;
                    case "5":
                        restaurantsCrud.LoadRestaurantsCollection();
                        break;
                    case "6":
                        countriesCrud.CountPopulationByRegion("Europe");
                        break;
                    case "7":
                        countriesCrud.SelectCountryCapitalPopulationLatLngByName("Madagascar");
                        break;
                    case "8":
                        booksCrud.SelectAllPrintTitlePagesAndCategoriesSortByPgNoPages();
                        break;
                    case "9":
                        restaurantsCrud.SelectByZipcodePrintNameAndCuisine("10462");
                        break;
                    case "10":
                        restaurantsCrud.SelectByBoroughAndCuisinePrintAllData("Bronx", "Chinese");
                        break;
                    case "11":
                        booksCrud.SelectLessThanAnyPagesPrintTitlePgCountAndAutors(130);
                        break;
                    case "12":
                        peopleCrud.SelectByNamePrintFriendsName("Caroline Webster");
                        break;
                    case "13":
                        restaurantsCrud.UpdateZipcodeWhereStreetEq("Driggs Avenue", "10443");
                        break;
                    case "14":
                        productsCrud.UpdateProductsAddNewFieldWherePriceGreaterThan("stockminim", 20, 2000);
                        break;
                    case "15":
                        booksCrud.UpdateBooksAddAuthorByName("Code Generation in Action", "Sam Watters");
                        break;
                    case "16":
                        productsCrud.UpdateProductsAddGammaField();
                        break;
                    case "17":
                        productsCrud.UpdateProductCategoryByName("MacBook Pro", "notebook", "ipad");
                        break;
                    case "18":
                        productsCrud.UpdateStockWherePriceBetween(800,1000, 60);
                        break;
                    case "19":
                        countriesCrud.UpdateCountryCallingCodesByName("Iceland", "356");
                        break;
                    case "20":
                        restaurantsCrud.DeleteRestaurantsFromBorough("Manhattan");
                        break;
                    case "21":
                        productsCrud.DeleteFirstCategoryFromProductWithName("iPhone 7");
                        break;
                    case "22":
                        booksCrud.DeleteBooksByPageCountBetween(0,100);
                        break;
                    case "23":
                        productsCrud.DeleteProductWithNameEq("Apple TV");
                        break;
                    case "24":
                        booksCrud.DeleteBookLastCategoryByISBN("1933988177");
                        break;
                    case "25":
                        productsCrud.DeleteProductsByCategory("phone"); ;
                        break;
                     case "26":
                        peopleCrud.DeleteFieldByRandomArrayItem("tags", "teacher");
                        break;
                    case "27":
                        countriesCrud.DeleteCountriesBySpeakingLanguage("Spanish");
                        break;
                    case "28":
                        DropCollection("itb", GetStringPrintInstructions());
                        break;
                    case "0":
                        return true;
                    default:
                        Console.WriteLine("\n\nIntrodueix una opció vàlida:");
                        isValidOption = false;
                        break;
                }
            } while (!isValidOption);

            Console.WriteLine("-- Presiona qualsevol tecla per tornar al menú");
            Console.ReadKey();
            Console.Clear();
            return false;
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