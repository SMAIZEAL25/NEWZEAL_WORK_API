using Microsoft.AspNetCore.Mvc;

namespace NZWalksUI.Controllers
{
    public class RegionController1 : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionController1(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult>  Index()
        {
            // Get All Regions from web API  short cut learnt crt KS
            try
            {
                var client = httpClientFactory.CreateClient();

                var response = await client.GetAsync("/https://localhost:7060//api/regions");

                response.EnsureSuccessStatusCode();

                var stringReponse = await response.Content.ReadAsStringAsync();

                ViewBag.Response = stringReponse;
            }
            catch (Exception ex)
            {
                // log the exception 
            }
            return View();
        }
    }
}
