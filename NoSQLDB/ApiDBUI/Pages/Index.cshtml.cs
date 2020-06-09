using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ApiDBUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory httpClientFactory;

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        public void OnGet()
        {

        }

        private async Task GetAllContacts()
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:44381/api/Contacts");

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    //when mapping back to objects ignore case
                    PropertyNameCaseInsensitive = true
                };

                string responseText = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
