using BookStoreApiMongoDB.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApiMongoDB.Services
{
    public class BooksService
    {
        private readonly IMongoCollection<Book> _booksCollection;

        public BooksService(IOptions<BookStoreDatabaseSettings> bookStoreDataBaseSettings)
        {
            //MongoBD - connectionString/ GetDataBase/ GetCollection:

            //reads the server
            var mongoClient = new MongoClient(bookStoreDataBaseSettings.Value.ConnectionString);
            //read the datdabase instance
            var mongoDatabase = mongoClient.GetDatabase(bookStoreDataBaseSettings.Value.DataBaseName);
            //Get the collection
            _booksCollection = mongoDatabase.GetCollection<Book>(
                    bookStoreDataBaseSettings.Value.BooksCollectionName);
        }

        //getOne
        public async Task<Book?> GetByIdAsync(string id)
        {
            //var book = _booksCollection.Find(b => b.Id == id).FirstOrDefaultAsync();
            return await _booksCollection.Find(id).FirstOrDefaultAsync();
        }
        //getAll
        public async Task<List<Book>> GetAllAsync()
        {
            var listBook = _booksCollection.Find(_ => true).ToListAsync();
            return await listBook;
        }
        //Add
        public async Task CreateSync(Book book)
        {
            await _booksCollection.InsertOneAsync(book);
        }
        //UpdateAsync
        public async Task UpdateAsync(string id, Book book)
        {
            await _booksCollection.ReplaceOneAsync(b => b.Id == id, book);
        }
        //DeleteAsync
        public async Task DeleteAsync(string id)
        {
            await _booksCollection.DeleteOneAsync(b => b.Id == id);
        }
    }
}
