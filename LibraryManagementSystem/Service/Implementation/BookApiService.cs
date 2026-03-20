using Domain.DTO;
using Newtonsoft.Json;
using System.Net.Http;

namespace Service.Implementation
{
    public class BookApiService
    {
        private readonly HttpClient _httpClient;
        public BookApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<BookApiDTO?> GetBookByISBN(string isbn)
        {
            var url = $"https://www.googleapis.com/books/v1/volumes?q=isbn:{isbn}";
            var response = await _httpClient.GetStringAsync(url);
            dynamic data = JsonConvert.DeserializeObject(response);
            if (data == null || data.items == null || data.items.Count == 0)
                return null;
            var volume = data.items[0].volumeInfo;
            return new BookApiDTO
            {
                Title = volume.title,
                Author = volume.authors != null ? volume.authors[0].ToString() : "Unknown Author",
                Description = volume.description != null ? volume.description.ToString() : "No description available."
            };
        }
    }
}