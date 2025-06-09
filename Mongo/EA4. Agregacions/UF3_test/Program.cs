

using UF3_test.cruds;

namespace UF3_test
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Menú:" +
                "\n[1] Exercici 1" +
                "\n[2] Exercici 2a" +
                "\n[3] Exercici 2b" +
                "\n[4] Exercici 2c" +
                "\n[5] Exercici 2d" +
                "\n[6] Exercici 2e" +
                "\n[7] Exercici 2f" +
                "\n[8] Exercici 2g" +
                "\n[9] Exercici 2h" +
                "\n[10] Exercici 2i" +
                "\n[11] Exercici 2j" +
                "\n[12] Exercici 2k" +
                "\n[13] Exercici 2l" +
                "\n[0] Sortir" +
                "\n");

            string? option = Console.ReadLine();


            switch (option)
            {
                case "0":
                    Console.WriteLine("Good night...");
                    break;
                case "1":
                    Exercise1();
                    break;
                case "2":
                    Exercise2a();
                    break;
                case "3":
                    Exercise2b();
                    break;
                case "4":
                    Exercise2c();
                    break;
                case "5":
                    Exercise2d();
                    break;
                case "6":
                    Exercise2e();
                    break;
                case "7":
                    Exercise2f();
                    break;
                case "8":
                    Exercise2g();
                    break;
                case "9":
                    Exercise2h();
                    break;
                case "10":
                    Exercise2i();
                    break;
                case "11":
                    Exercise2j();
                    break;
                case "12":
                    Exercise2k();
                    break;
                case "13":
                    Exercise2l();
                    break;
                default:
                    break;
            }
        }

        public static void Exercise1()
        {
            Console.WriteLine("Exercici 1: Importar els fitxers Json com a col·leccions d'objectes");

            CountriesCRUD countriesCRUD = new CountriesCRUD();
            ProductsCRUD productsCRUD = new ProductsCRUD();
            RestaurantsCRUD restaurantsCRUD = new RestaurantsCRUD();

            countriesCRUD.LoadCountriesCollection();
            productsCRUD.LoadProductsCollection();
            restaurantsCRUD.LoadRestaurantsCollection();
        }

        public static void Exercise2a()
        {
            Console.WriteLine("Exercici 2a: Mostrar el número de països on es parla anglès");

            CountriesCRUD countriesCRUD = new CountriesCRUD();
            countriesCRUD.GetCountriesLanguageCount("English");
        }
        public static void Exercise2b()
        {
            Console.WriteLine("Exercici 2b: Mostrar de countries la regió on hi ha més països");

            CountriesCRUD countriesCRUD = new CountriesCRUD();

            countriesCRUD.GetRegionWithMoreCountries();
        }
        public static void Exercise2c()
        {
            Console.WriteLine("Exercici 2c: Mostrar quants països conté cada subregió.");

            CountriesCRUD countriesCRUD = new CountriesCRUD();

            countriesCRUD.GetCountriesSubregionCount();
        }
        public static void Exercise2d()
        {
            Console.WriteLine("Exercici 2d: Mostrar el país on es parlen més idiomes");

            CountriesCRUD countriesCRUD = new CountriesCRUD();

            countriesCRUD.GetCountryWithMoreLanguages();
        }
        public static void Exercise2e()
        {
            Console.WriteLine("Exercici 2e: Mostrar quantes vegades apareix cada puntuació 'score' en tots els restaurants");

            RestaurantsCRUD restaurantsCRUD = new RestaurantsCRUD();

            restaurantsCRUD.GetRestaurantsGradesScoreCount();
        }
        public static void Exercise2f()
        {
            Console.WriteLine("Exercici 2f: Mostrar els codis postals de cada barri");

            RestaurantsCRUD restaurantsCRUD = new RestaurantsCRUD();

            restaurantsCRUD.GetZipcodesByBorough();
        }
        public static void Exercise2g()
        {
            Console.WriteLine("Exercici 2g: Mostrar el número de restaurants que hi ha per a cada cuina");

            RestaurantsCRUD restaurantsCRUD = new RestaurantsCRUD();

            restaurantsCRUD.GetRestaurantsCuisineCount();
        }
        public static void Exercise2h()
        {
            Console.WriteLine("Exercici 2h: Mostrar el número de grades de cada restaurant");

            RestaurantsCRUD restaurantsCRUD = new RestaurantsCRUD();

            restaurantsCRUD.GetRestaurantsGradesCount();
        }
        public static void Exercise2i()
        {
            Console.WriteLine("Exercici 2i: Mostrar el nom dels tipus de cuina per a cada barri");

            RestaurantsCRUD restaurantsCRUD = new RestaurantsCRUD();

            restaurantsCRUD.GetCuisinesByBorough();
        }
        public static void Exercise2j()
        {
            Console.WriteLine("Exercici 2j: Mostrar de cada restaurant la seva valoració més alta");

            RestaurantsCRUD restaurantsCRUD = new RestaurantsCRUD();

            restaurantsCRUD.GetHighScoreByRestaurant();
        }
        public static void Exercise2k()
        {
            Console.WriteLine("Exercici 2k: Mostrar el número de categories que té cada producte");

            ProductsCRUD productsCRUD = new ProductsCRUD();

            productsCRUD.GetCategoriesProductCount();
        }
        public static void Exercise2l()
        {
            Console.WriteLine("Exercici 2l: Mostrar totes les categories sense que es repeteixi cap categoria");

            ProductsCRUD productsCRUD = new ProductsCRUD();

            productsCRUD.GetAllCategories();
        }
    }
}