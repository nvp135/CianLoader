using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using LocalAPI.JsonModels;

namespace LocalAPI.Controllers
{
    [Produces("application/json")]
    [Route("cian")]
    [ApiController]
    public class HomeController : Controller
    {
        const string _baseUrl = "https://ekb.cian.ru/cian-api/mobile-site/v2/offers/clusters/?deal_type=sale&engine_version=2&offer_type=flat&screen_area=652&bbox=56.82676879275881,60.59311078029706,56.846763478366775,60.67550824123453&deal_type=2&allow_precision_correction=0&zoom=15";
        public async Task<CianJson> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}");
            if (response.IsSuccessStatusCode)
            {
                var cianJson = JsonConvert.DeserializeObject<CianJson>(await response.Content.ReadAsStringAsync());
                return cianJson;
            }
            return null;
            //return View();
        }
    }
}