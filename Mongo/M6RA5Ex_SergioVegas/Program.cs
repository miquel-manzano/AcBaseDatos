using M6RA5Ex.connections;
using M6RA5Ex.cruds;
using M6RA5Ex.model;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Collections;
using System.Xml.Linq;


namespace M6RA5Ex
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RestaurantsCRUD rCRUD = new RestaurantsCRUD();
            //rCRUD.LoadCollection();

            ProductsCRUD pCRUD = new ProductsCRUD();
            //pCRUD.LoadCollection();

            BooksCRUD bCRUD = new BooksCRUD();
            //bCRUD.LoadCollection();

            PeopleCRUD peCRUD = new PeopleCRUD();
            //peCRUD.LoadCollection();

            //rCRUD.InsertRestaurants();
            //rCRUD.SelectAllByBoroughAndNotThatCuisine("Queens", "American");
            //rCRUD.SelectAllByZipcodeAndCuisine("11209","Delicatessen");
            //rCRUD.SelectScore("40358429");

            //bCRUD.UpdateAddAuthorToTitle("Mule in Action", "David Dossot", "Rob Pen");
            //rCRUD.UpdateCoordenadesRestaurant("40368271", -61.8869, 38.721);
            //bCRUD.AddSizeFieldToBooks();
            rCRUD.SelectBoroughByZipcode();


        }  
    }
}
