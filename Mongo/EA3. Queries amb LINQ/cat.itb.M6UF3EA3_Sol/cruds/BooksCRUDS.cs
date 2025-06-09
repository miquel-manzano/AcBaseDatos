using cat.itb.M6UF3EA3_Sol.connections;
using cat.itb.M6UF3EA3_Sol.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;


namespace cat.itb.M6UF3EA3_Sol.cruds
{
    public class BooksCRUD
    {
        public void LoadBooksCollection()
        {
            FileInfo file = new FileInfo("../../../files/books.json");
            string fileString = file.OpenText().ReadToEnd();
            List<Book> books = JsonConvert.DeserializeObject<List<Book>>(fileString);

            var database = MongoLocalConnection.GetDatabase("itb");

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


        public void SelectJustTitleAnsStatusBooks()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            var booksCollection = database.GetCollection<Book>("books");

            var books =
                booksCollection.AsQueryable<Book>();

            foreach (var book in books)
            {
                Console.WriteLine($"Títol {book.Title} Estat: {book.Status}");               
            }
        }

        public void SelectTitleAndCategsSortedByNumPages()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            var booksCollection = database.GetCollection<Book>("books");

            var books =
                booksCollection.AsQueryable<Book>()
                    .OrderByDescending(b => b.PageCount);

            foreach (var book in books)
            {
                Console.WriteLine("Titol: {0}", book.Title);

                foreach (var categ in book.Categories)
                {
                    Console.WriteLine("Categoria: {0}", categ);
                }
            }

        }

        public void SelectBooksByAuthor(String pAuthor)
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            var booksCollection = database.GetCollection<Book>("books");

            var books =
                booksCollection.AsQueryable<Book>()
                    .Where(b => b.Authors.Contains(pAuthor));

            foreach (var book in books)
            {
                Console.WriteLine("Title :{0}", book.Title);

                foreach (var author in book.Authors)
                {
                    Console.WriteLine("Autor :{0}", author);
                }
            }
        }

        public void SelectBooksByPageCountInterval(int pMin, int pMax, String pCat)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var booksCollection = database.GetCollection<Book>("books");

            var query =
                booksCollection.AsQueryable<Book>()
                    .Where(b => b.PageCount >= pMin && b.PageCount <= pMax)
                    .Where(b => b.Categories.Contains(pCat));

            foreach (var book in query)
            {
                Console.WriteLine("Title :{0}", book.Title);
                Console.WriteLine("Number of pages :{0}", book.PageCount);
                foreach (var author in book.Authors)
                {
                    Console.WriteLine("Author :{0}", author);
                }
            }
        }

        public void SelectBooksByAuthors(String[] pAutors)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var booksCollection = database.GetCollection<Book>("books");

            var query =
                booksCollection.AsQueryable<Book>()
                    .Where(b => b.Authors.Contains(pAutors[0]))
                    .Where(b => b.Authors.Contains(pAutors[1]));

            foreach (var book in query)
            {
                Console.WriteLine("Title :{0}", book.Title);
                foreach (var author in book.Authors)
                {
                    Console.WriteLine("Author :{0}", author);
                }
            }
        }

        public void SelectBooksByCategNotAuthor(String pCateg, String pAutor)
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            var booksCollection = database.GetCollection<Book>("books");

            var query =
                booksCollection.AsQueryable<Book>()
                    .Where(b => b.Categories.Contains(pCateg))
                    .Where(b => !b.Authors.Contains(pAutor))
                    .OrderBy(b => b.Title);

            foreach (var book in query)
            {
                Console.WriteLine("Title :{0}", book.Title);

            }
        }
    }
}
