using System;
using System.Collections.Generic;
using System.IO;
using cat.itb.M6UF3EA1.connections;
using cat.itb.M6UF3EA1.model;
using MongoDB.Bson;
//using MongoDB.Bson.IO;
using MongoDB.Driver;
using Newtonsoft.Json;


namespace cat.itb.M6UF3EA1
{
    internal class Program
    {
        public static void Main()
        {
          Start();
        }

        private static void Start()
        {
            do
            {
                PrintMenu();
            } while (!ChooseOption());
        }

        private static void PrintMenu()
        {
            Console.WriteLine("[1] InsertThreeStudents" +
                              "\n[2] SelectStudentsFromGroup" +
                              "\n[3] SelectStudentsWithScore2" +
                              "\n[4] SelectStudentsWithScoreMoreThan" +
                              "\n[5] SelectInterestsStudentId" +
                              "\n[6] SelectNameStudentId" +
                              "\n[7] LoadPeopleCollection" +
                              "\n[8] LoadBooksCollection" +
                              "\n[9] LoadProductsCollection" +
                              "\n[10] LoadRestaurantsCollection" +
                              "\n[11] LoadStudentsCollection" +
                              "\n[12] LoadGradesCollection" +
                              "\n[13] LoadCountriesCollection" +
                              "\n[0] Exit");

            
        }

       private static bool ChooseOption()
        {
            bool isValid;
            do
            {
                isValid = true;
                switch (Console.ReadLine())
                {
                    case "1":
                        InsertThreeStudents();
                        break;
                    case "2":
                        SelectStudentsFromGroup("DAMv1");
                        break;
                    case "3":
                        SelectStudentsWithScore2();
                        break;
                    case "4":
                        SelectStudentsWithScoreMoreThan(99);
                        break;
                    case "5":
                        SelectInterestsStudentId(888666333);
                        break;
                    case "6":
                        SelectNameStudentId(444777888);
                        break;
                    case "7":
                        LoadPeopleCollection();
                        break;
                    case "8":
                        LoadBooksCollection();
                        break;
                    case "9":
                        LoadProductsCollection();
                        break;
                    case "10":
                        LoadRestaurantsCollection();
                        break;
                    case "11":
                        LoadStudentsCollection();
                        break;
                    case "12":
                        LoadGradesCollection();
                        break;
                    case "13":
                        LoadCountriesCollection();
                        break;
                    case "0":
                        return true;
                    default:
                        Console.WriteLine();
                        isValid = false;
                        break;
                }
            } while (!isValid);

            return false;
        }

        private static void InsertThreeStudents()
        {
            var database = MongoLocalConnection.GetDatabase("sample_training");
            var collection = database.GetCollection<BsonDocument>("grades");
        
            var estudiant1 = new BsonDocument
            {
                { "student_id", 222333999 },
                { "name", "Joan" },
                { "surname", "Colomer" },
                { "class_id", "555"},
                { "group", "DAMv1" },
                { "scores", new BsonArray
                    {
                        new BsonDocument { { "type", "exam" }, { "score", 100 } },
                        new BsonDocument { { "type", "teamWork" }, { "score", 50 } }
                    }
                }
            };

            var estudiant2 = new BsonDocument
            {
                { "student_id", 444777888 },
                { "name", "Sergi" },
                { "surname", "Martorell Fernández" },
                { "class_id", "555" },
                { "group", "DAMv1" },
                { "interests", new BsonArray {"music", "gym", "code", "electronics"} }
            };

            var estudiant3 = new BsonDocument
            {
                { "student_id", 888666333 },
                { "name", "Roser" },
                { "surname", "Escapa" },
                { "class_id", "555"},
                { "group", "DAMv1" },
                { "interests", new BsonArray { "rap", "runner", "movies", "comic" } },
                { "scores", new BsonArray
                    {
                        new BsonDocument { { "type", "exam" }, { "score", 90 } },
                        new BsonDocument { { "type", "teamWork" }, { "score", 60 } },
                        new BsonDocument { { "type", "quiz" }, { "score", 96 } },
                        new BsonDocument { { "type", "homework" }, { "score", 23 } }
                    }
                }
            };

            collection.InsertOne(estudiant1);
            collection.InsertOne(estudiant2);
            collection.InsertOne(estudiant3);

            Console.WriteLine("Students added successfully");
        }
        
        private static void SelectStudentsFromGroup(string group)
        {
          
            var database = MongoLocalConnection.GetDatabase("sample_training");
            var collection = database.GetCollection<BsonDocument>("grades");
            
            var filter = Builders<BsonDocument>.Filter.Eq("group", group);
            var studentDocument = collection.Find(filter).ToList();
            foreach (var student in studentDocument)
                Console.WriteLine(student.ToString());
        }
        
        private static void SelectStudentsWithScore()
        {
            var database = MongoLocalConnection.GetDatabase("sample_training");
            var collection = database.GetCollection<BsonDocument>("grades");

            var examScoreFilter = Builders<BsonDocument>.Filter.ElemMatch<BsonValue>(
                "scores", new BsonDocument { { "type", "exam" },
                    { "score", new BsonDocument { { "$eq", 90 } } }
                });
            var examScores = collection.Find(examScoreFilter).ToList();

            foreach (var student in examScores)
                Console.WriteLine(student.ToString());
        }
        
        private static void SelectStudentsWithScore2()
        {
            var database = MongoLocalConnection.GetDatabase("sample_training");
            var collection = database.GetCollection<BsonDocument>("grades");

            var filter = Builders<BsonDocument>.Filter.Eq("scores.type", "exam"); 
            filter &= Builders<BsonDocument>.Filter.Eq("scores.score", 90);
            var studentDocument = collection.Find(filter).ToList();
            foreach (var student in studentDocument)
                Console.WriteLine(student.ToString());
        }
       
        
        private static void SelectStudentsWithScoreMoreThan(double score)
        {
            var database = MongoLocalConnection.GetDatabase("sample_training");
            var collection = database.GetCollection<BsonDocument>("grades");
            
            var examScoreFilter = Builders<BsonDocument>.Filter.ElemMatch<BsonValue>(
                "scores", new BsonDocument { { "type", "quiz" },
                    { "score", new BsonDocument { { "$gt", score } } }
                });
            var examScores = collection.Find(examScoreFilter).ToList();

            foreach (var student in examScores)
                Console.WriteLine(student.ToString());
        }
        
        private static void SelectStudentsWithScoreMoreThan2(double score)
        {
            var database = MongoLocalConnection.GetDatabase("sample_training"); ;
            var collection = database.GetCollection<BsonDocument>("grades");

            var filter = Builders<BsonDocument>.Filter.Eq("scores.type", "quiz"); 
            filter &= Builders<BsonDocument>.Filter.Gt("scores.score", score);
            var studentDocument = collection.Find(filter).ToList();
            foreach (var student in studentDocument)
                Console.WriteLine(student.ToString());
        }
        
        
        private static void SelectInterestsStudentId(int id)
        {
            var database = MongoLocalConnection.GetDatabase("sample_training");
            var collection = database.GetCollection<BsonDocument>("grades");
            
            var filter = Builders<BsonDocument>.Filter.Eq("student_id", id);
            var studentDocument = collection.Find(filter).FirstOrDefault();
            
                var interests = studentDocument.GetElement("interests");
                Console.WriteLine(interests.ToString());
           
        }

        private static void SelectNameStudentId(int id)
        {
            var database = MongoLocalConnection.GetDatabase("sample_training");
            var collection = database.GetCollection<BsonDocument>("grades");

            var filter = Builders<BsonDocument>.Filter.Eq("student_id", id);
            var studentDocument = collection.Find(filter).FirstOrDefault();
            
                var name = studentDocument.GetElement("name");
                var surname = studentDocument.GetElement("surname");
            Console.WriteLine(name.ToString() + "  " + surname.ToString());
           
        }


        private static void LoadPeopleCollection()
        {
            FileInfo file = new FileInfo("../../files/people.json");
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

        private static void LoadBooksCollection()
        {
            FileInfo file = new FileInfo("../../files/books.json");
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

        private static void LoadProductsCollection()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("products");
            var collection = database.GetCollection<BsonDocument>("products");            

            FileInfo file = new FileInfo("../../files/products.json");

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
        
        private static void LoadRestaurantsCollection()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("restaurants");
            var collection = database.GetCollection<BsonDocument>("restaurants");

            FileInfo file = new FileInfo("../../files/restaurants.json");

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

        private static void LoadStudentsCollection()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("students");
            var collection = database.GetCollection<BsonDocument>("students");

            FileInfo file = new FileInfo("../../files/students.json");

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

        private static void LoadGradesCollection()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("grades");
            var collection = database.GetCollection<BsonDocument>("grades");

            FileInfo file = new FileInfo("../../files/Grades.json");

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

        private static void LoadCountriesCollection()
        {
            FileInfo file = new FileInfo("../../files/countries.json");
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