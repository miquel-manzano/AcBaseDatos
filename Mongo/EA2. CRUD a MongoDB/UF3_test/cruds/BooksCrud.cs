using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using UF3_test.connections;

namespace UF3_test.cruds
{
    public class BooksCrud
    {
        public void SelectAllPrintTitlePagesAndCategoriesSortByPgNoPages()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("books");

            var sort = Builders<BsonDocument>.Sort.Descending("pageCount");
            var books = collection.Find(new BsonDocument()).Sort(sort).ToList();
            foreach (var book in books)
            {
                Console.WriteLine(book.GetElement("title"));
                Console.WriteLine(book.GetElement("pageCount"));
                Console.WriteLine(book.GetElement("categories") + "\n");
            }
        }

        public void SelectLessThanAnyPagesPrintTitlePgCountAndAutors(int pPagesCount)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("books");

            var filter = Builders<BsonDocument>.Filter.Lt("pageCount", pPagesCount);
            var books = collection.Find(filter).ToList();
            foreach (var book in books)
            {
                Console.WriteLine(book.GetElement("title"));
                Console.WriteLine(book.GetElement("pageCount"));
                Console.WriteLine(book.GetElement("authors") + "\n");
            }
        }

        public void UpdateBooksAddAuthorByName(string pName, string pAuthor)
        {
            var db = MongoLocalConnection.GetDatabase("itb");
            var collection = db.GetCollection<BsonDocument>("books");

            var filter = Builders<BsonDocument>.Filter.Eq("title", pName);
            var update = Builders<BsonDocument>.Update.AddToSet("authors", pAuthor);

            var result = collection.Find(filter).ToList();
            Console.WriteLine($"\nLlibre amb nom {pName} ABANS de l'actualització:");
            foreach (var doc in result)
            {
                Console.WriteLine(doc);
            }

            collection.UpdateOne(filter, update);
            result = collection.Find(filter).ToList();
            Console.WriteLine($"\nLlibre amb nom {pName} DESPRÉS de l'actualització:");
            foreach (var doc in result)
            {
                Console.WriteLine(doc);
            }
        }

        public void DeleteBooksByPageCountBetween(int pMin, int pMax)
        {
            var db = MongoLocalConnection.GetDatabase("itb");
            var collection = db.GetCollection<BsonDocument>("books");

            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Gte("pageCount", pMin),
                Builders<BsonDocument>.Filter.Lte("pageCount", pMax)
            );

            var result = collection.DeleteMany(filter);
            Console.WriteLine($"Documents eliminats: {result.DeletedCount}");
        }

        public void DeleteBookLastCategoryByISBN(string pIsbn)
        {
            var db = MongoLocalConnection.GetDatabase("itb");
            var collection = db.GetCollection<BsonDocument>("books");

            var filter = Builders<BsonDocument>.Filter.Eq("isbn", pIsbn);
            var update = Builders<BsonDocument>.Update.PopLast("categories");


            var bookB = collection.Find(filter).FirstOrDefault();
            Console.WriteLine("Llibre amb nom " + bookB.GetElement("title") + " abans de l'actualització:");
            Console.WriteLine(bookB.GetElement("categories"));

            collection.UpdateOne(filter, update);
            Console.WriteLine("Eliminada l'última categoria del llibre amb isbn " + pIsbn);


            var bookA = collection.Find(filter).FirstOrDefault();
            Console.WriteLine("Llibre amb nom " + bookA.GetElement("title") + " després de l'actualització:");
            Console.WriteLine(bookA.GetElement("categories"));
        }
    }
}
