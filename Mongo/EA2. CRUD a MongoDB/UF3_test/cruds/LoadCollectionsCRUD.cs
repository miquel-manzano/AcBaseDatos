using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using Newtonsoft.Json;
using UF3_test.connections;
using UF3_test.model;

namespace UF3_test.cruds
{
    public class LoadCollectionsCRUD
    {
        public void LoadPeopleCollection()
        {
            FileInfo file = new FileInfo("../../../files/people.json");
            StreamReader sr = file.OpenText();
            string fileString = sr.ReadToEnd();
            sr.Close();
            List<Person> people = JsonConvert.DeserializeObject<List<Person>>(fileString);

            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("people");
            var collection = database.GetCollection<BsonDocument>("people");

            if (people != null)
                foreach (var person in people)
                {
                    Console.WriteLine(person.Name);
                    string json = JsonConvert.SerializeObject(person);
                    var document = new BsonDocument();
                    document.AddRange(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                }
        }

        public void LoadBooksCollection()
        {
            FileInfo file = new FileInfo("../../../files/books.json");
            StreamReader sr = file.OpenText();
            string fileString = sr.ReadToEnd();
            sr.Close();
            List<Book> books = JsonConvert.DeserializeObject<List<Book>>(fileString);

            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("books");
            var collection = database.GetCollection<BsonDocument>("books");

            if (books != null)
                foreach (var book in books)
                {
                    Console.WriteLine(book.Title);
                    string json = JsonConvert.SerializeObject(book);
                    var document = new BsonDocument();
                    document.AddRange(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                }
        }

        public void LoadProductsCollection()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("products");
            var collection = database.GetCollection<BsonDocument>("products");

            FileInfo file = new FileInfo("../../../files/products.json");

            using (StreamReader sr = file.OpenText())
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Product product = JsonConvert.DeserializeObject<Product>(line);
                    Console.WriteLine(product.Name);
                    string json = JsonConvert.SerializeObject(product);
                    var document = new BsonDocument();
                    document.AddRange(BsonDocument.Parse(json)); ;
                    collection.InsertOne(document);
                }
            }

        }

        public void LoadRestaurantsCollection()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("restaurants");
            var collection = database.GetCollection<BsonDocument>("restaurants");

            FileInfo file = new FileInfo("../../../files/restaurants.json");

            using (StreamReader sr = file.OpenText())
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Restaurant rest = JsonConvert.DeserializeObject<Restaurant>(line);
                    Console.WriteLine(rest.Name);
                    string json = JsonConvert.SerializeObject(rest);
                    var document = new BsonDocument();
                    document.AddRange(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                }
            }

        }

        public void LoadStudentsCollection()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("students");
            var collection = database.GetCollection<BsonDocument>("students");

            FileInfo file = new FileInfo("../../../files/students.json");

            using (StreamReader sr = file.OpenText())
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Student student = JsonConvert.DeserializeObject<Student>(line);
                    Console.WriteLine(student.Firstname);
                    string json = JsonConvert.SerializeObject(student);
                    var document = new BsonDocument();
                    document.AddRange(BsonDocument.Parse(json)); ;
                    collection.InsertOne(document);
                }
            }

        }

        public void LoadGradesCollection()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("grades");
            var collection = database.GetCollection<BsonDocument>("grades");

            FileInfo file = new FileInfo("../../../files/Grades.json");

            using (StreamReader sr = file.OpenText())
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Student2 student = JsonConvert.DeserializeObject<Student2>(line);
                    Console.WriteLine(student.Student_id._NumberInt);
                    string json = JsonConvert.SerializeObject(student);
                    var document = new BsonDocument();
                    document.AddRange(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                }
            }

        }

        public void LoadCountriesCollection()
        {
            FileInfo file = new FileInfo("../../../files/countries.json");
            StreamReader sr = file.OpenText();
            string fileString = sr.ReadToEnd();
            sr.Close();
            List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(fileString);

            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("countries");
            var collection = database.GetCollection<BsonDocument>("countries");

            if (countries != null)
                foreach (var country in countries)
                {
                    Console.WriteLine(country.Name);
                    string json = JsonConvert.SerializeObject(country);
                    var document = new BsonDocument();
                    document.AddRange(BsonDocument.Parse(json)); ;
                    collection.InsertOne(document);
                }
        }
    }
}
