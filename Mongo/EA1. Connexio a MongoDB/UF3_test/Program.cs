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

namespace UF3_test
{
    internal class Program
    {
        public static void Main(string[] args)
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

            int option = int.Parse(Console.ReadLine());
            var loadCollection = new LoadCollections();

            switch (option)
            {
                case 0:
                    Console.WriteLine("Good night...");
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
                    loadCollection.LoadPeopleCollection();
                    break;
                case 8:
                    loadCollection.LoadBooksCollection();
                    break;
                case 9:
                    loadCollection.LoadProductsCollection();
                    break;
                case 10:
                    loadCollection.LoadRestaurantsCollection();
                    break;
                case 11:
                    loadCollection.LoadStudentsCollection();
                    break;
                case 12:
                    loadCollection.LoadCountriesCollection();
                    break;
                case 13:
                    Ex13();
                    break;
            }
        }

        public static void Ex1()
        {
            Console.WriteLine("--- Ex1 ---");

            var database = MongoLocalConnection.GetDatabase("sample_training");
            var collection = database.GetCollection<BsonDocument>("grades");

            var estudiant1 = new BsonDocument
            {
                { "student_id", 222333999 },
                { "name", "Miquel" },
                { "surname", "Manzano" },
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
        public static void Ex2()
        {
            Console.WriteLine("--- Ex2 ---");

            string group = "DAMv1";

            var database = MongoLocalConnection.GetDatabase("sample_training");
            var collection = database.GetCollection<BsonDocument>("grades");

            var filter = Builders<BsonDocument>.Filter.Eq("group", group);
            var studentDocument = collection.Find(filter).ToList();
            foreach (var student in studentDocument)
                Console.WriteLine(student.ToString());
        }
        public static void Ex3()
        {
            Console.WriteLine("--- Ex3 ---");

            var database = MongoLocalConnection.GetDatabase("sample_training");
            var collection = database.GetCollection<BsonDocument>("grades");

            var filter = Builders<BsonDocument>.Filter.Eq("scores.type", "exam");
            filter &= Builders<BsonDocument>.Filter.Eq("scores.score", 90);
            var studentDocument = collection.Find(filter).ToList();
            foreach (var student in studentDocument)
                Console.WriteLine(student.ToString());
        }
        public static void Ex4()
        {
            Console.WriteLine("--- Ex4 ---");

            double score = 99;

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
        public static void Ex5()
        {
            Console.WriteLine("--- Ex5 ---");

            int id = 888666333;

            var database = MongoLocalConnection.GetDatabase("sample_training");
            var collection = database.GetCollection<BsonDocument>("grades");

            var filter = Builders<BsonDocument>.Filter.Eq("student_id", id);
            var studentDocument = collection.Find(filter).FirstOrDefault();

            var interests = studentDocument.GetElement("interests");
            Console.WriteLine(interests.ToString());
        }
        public static void Ex6()
        {
            Console.WriteLine("--- Ex6 ---");

            var id = 444777888;

            var database = MongoLocalConnection.GetDatabase("sample_training");
            var collection = database.GetCollection<BsonDocument>("grades");

            var filter = Builders<BsonDocument>.Filter.Eq("student_id", id);
            var studentDocument = collection.Find(filter).FirstOrDefault();

            var name = studentDocument.GetElement("name");
            var surname = studentDocument.GetElement("surname");
            Console.WriteLine(name.ToString() + "  " + surname.ToString());
        }
        public static void Ex7()
        {
            Console.WriteLine("--- Ex7 ---");
        }
        public static void Ex8()
        {
            Console.WriteLine("--- Ex8 ---");
        }
        public static void Ex9()
        {
            Console.WriteLine("--- Ex9 ---");
        }
        public static void Ex10()
        {
            Console.WriteLine("--- Ex11 ---");
        }
        public static void Ex11()
        {
            Console.WriteLine("--- Ex12 ---");
        }
        public static void Ex12()
        {
            Console.WriteLine("--- Ex13 ---");
        }
        public static void Ex13()
        {
            Console.WriteLine("--- Ex1 ---");
        }
    }
}