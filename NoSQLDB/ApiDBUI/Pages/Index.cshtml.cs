using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ApiDBUI.Models;
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

        public async Task OnGet()
        {
            await CreateContact();
            await GetAllContacts();
        }

        private async Task GetAllContacts()
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:44381/api/Contacts");

            List<ContactModel> contacts;

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    //when mapping back to objects ignore case
                    PropertyNameCaseInsensitive = true
                };

                string responseText = await response.Content.ReadAsStringAsync();

                contacts = JsonSerializer.Deserialize<List<ContactModel>>(responseText, options);
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        private async Task CreateContact()
        {
            ContactModel nc = new ContactModel
            {
                FirstName = "nFname1",
                LastName = "nLname1"
            };
            nc.EmailAddresses.Add(new EmailAddressModel { EmailAddress = "ncEmail1@mail.com" });
            nc.EmailAddresses.Add(new EmailAddressModel { EmailAddress = "ncEmail2@mail.com" });
            nc.PhoneNumbers.Add(new PhoneNumberModel { PhoneNumber = "555-123-1111" });
            nc.PhoneNumbers.Add(new PhoneNumberModel { PhoneNumber = "555-123-2222" });

            var client = httpClientFactory.CreateClient();
            var response = await client.PostAsync("https://localhost:44381/api/Contacts", new StringContent(JsonSerializer.Serialize(nc), Encoding.UTF8, "application/json"));

        }
    }
}
