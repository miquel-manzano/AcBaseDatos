using M6RA5Ex.connections;
using M6RA5Ex.model;
using MongoDB.Bson.IO;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewtonsoftJson = Newtonsoft.Json.JsonConvert;

namespace M6RA5Ex.cruds
{
    public class BooksCRUD
    {
        public void LoadCollection()
        {
            const string fileName = "books.json";
            const string filePath = @"..\..\..\files\" + fileName;

            FileInfo fileInfo = new FileInfo(filePath);

            string fileString = fileInfo.OpenText().ReadToEnd();
            List<Book> books = NewtonsoftJson.DeserializeObject<List<Book>>(fileString);

            var database = MongoConnection.GetDatabase("itb");
            database.DropCollection("books");
            var collection = database.GetCollection<Book>("books");

            try
            {
                if (books != null)
                    collection.InsertMany(books);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nCollection books loaded");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Collection couldn't be inserted");
            }
            Console.ResetColor();
        }
        public void UpdateAddAuthorToTitle(string title, string author, string newAuthor)
        {
            var settings = new JsonWriterSettings { Indent = true };

            var titleFilter = Builders<BsonDocument>.Filter.Eq("title", title);
            var update = Builders<BsonDocument>.Update.Set("authors.$[author]", newAuthor);


            var database = MongoConnection.GetDatabase("itb");
            var books = database.GetCollection<BsonDocument>("books");

            var arrayFilter = new List<ArrayFilterDefinition>
            {
                new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("author", author))
            };
            var updateOptions = new UpdateOptions { ArrayFilters = arrayFilter };

            foreach (var book in books.Find(titleFilter).ToList())
            {
                Console.WriteLine($"\n {book.ToJson(settings)}");
            }
            
            var result = books.UpdateMany(titleFilter, update, updateOptions);

            Console.WriteLine($"{result.ModifiedCount} libros actualizados.");
            foreach (var book in books.Find(titleFilter).ToList())
            {
                Console.WriteLine($"\n {book.ToJson(settings)}");
            }

        }
        public void UpdateAddCategory(string title, string category)
        {
            var settings = new JsonWriterSettings { Indent = true };

            var titleFilter = Builders<BsonDocument>.Filter.Eq("title", title);
            var update = Builders<BsonDocument>.Update.Push("categories", category);

            var database = MongoConnection.GetDatabase("itb");
            var books = database.GetCollection<BsonDocument>("books");

            var bookToUpdate = books.Find(titleFilter).FirstOrDefault();
            Console.WriteLine($"\n Llibre a actualitzar: \n --------------------- \n {bookToUpdate.ToJson(settings)}");

            books.UpdateOne(titleFilter, update);

            var bookUpdated = books.Find(titleFilter).FirstOrDefault();
            Console.WriteLine($"\n Llibre actualitzat: \n ------------------- \n {bookUpdated.ToJson(settings)}");
        }
        public void AddSizeFieldToBooks()
        {
            var database = MongoConnection.GetDatabase("itb");
            var books = database.GetCollection<BsonDocument>("books");

            // Filtrar libros con más de 700 páginas
            var filterHuge = Builders<BsonDocument>.Filter.Gt("pageCount", 700);
            var updateHuge = Builders<BsonDocument>.Update.Set("size", "huge");

            // Filtrar libros con 700 páginas o menos
            var filterNormal = Builders<BsonDocument>.Filter.Lte("pageCount", 700);
            var updateNormal = Builders<BsonDocument>.Update.Set("size", "normal");

            // Aplicar la actualización
            var resultHuge = books.UpdateMany(filterHuge, updateHuge);
            var resultNormal = books.UpdateMany(filterNormal, updateNormal);

            Console.WriteLine($"\n{resultHuge.ModifiedCount} libros marcados como 'huge'.");
            Console.WriteLine($"{resultNormal.ModifiedCount} libros marcados como 'normal'.");
        }
        public  void DeleteFirstAuthorFromXISBN(string isbn) 
        {
            var settings = new JsonWriterSettings { Indent = true };

            var isbnFilter = Builders<BsonDocument>.Filter.Eq("isbn", isbn);
            var update = Builders<BsonDocument>.Update.PopFirst("authors");
            var database = MongoConnection.GetDatabase("itb");
            var books = database.GetCollection<BsonDocument>("books");

            var docToUpdate = books.Find(isbnFilter).FirstOrDefault();
            Console.WriteLine($"\n {docToUpdate.ToJson(settings)}");

            books.UpdateOne(isbnFilter, update);

            var docUpdated = books.Find(isbnFilter).FirstOrDefault();
            Console.WriteLine($"\n {docUpdated.ToJson(settings)}");
        }

    }
}
