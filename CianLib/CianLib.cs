using CianLib.EF;
using CianLib.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RestSharp;

namespace CianLib
{
    public class CianLibrary
    {
        Random random = new Random();
        private RestClient client;
        private RestRequest request;

        public IConfiguration Configuration { get; set; }
        public DbContextOptions<CianContext> DbContextOptions { get; set; }
        public List<City> CityList { get; set; } = new List<City>();

        public List<string> ErrorUrls = new List<string>();

        public CianLibrary()
        {
            LoadConfiguration();
            InitClient();
            GetDbConfiguration();
            GetCitiesConfiguration();
        }

        private void InitClient(string antibot = "")
        {
            string antibotValue = "";
            if (!String.IsNullOrEmpty(antibot))
            {
                antibotValue = $"anti_bot={antibot}";
            }
            else
            {
                antibotValue = $"anti_bot={Configuration.GetSection("Antibot")?.Value}";
            }
            client = new RestClient();
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:81.0) Gecko/20100101 Firefox/81.0";
            request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            request.AddHeader("Accept-Language", "en-US,en;q=0.5");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Cookie", $"__cfduid=da5612afab5692304217730ce90b9ec651600795469; __cf_bm=32f020ff4ab64c3bc2ccc458103c3f43e07ff19d-1600797547-1800-AfBJd5qs8cqxWhZORyfXbmz2bh6Zm4/2aGlHAMTVYNrTSMeXBfr2Z4LV0Zl9ByREFXF3VnPy/9dwBLB4ST+LEDA=; _CIAN_GK=bcfacd4e-2f5c-4960-890f-8df728a5c006; _gcl_au=1.1.456326789.1600795470; _ga=GA1.2.790058914.1600795470; _gid=GA1.2.1569458049.1600795470; _dc_gtm_UA-30374201-1=1; {antibotValue}");
            request.AddHeader("Upgrade-Insecure-Requests", "1");
            request.AddHeader("Cache-Control", "max-age=0");
            request.AddHeader("TE", "Trailers");
            
        }

        private void LoadConfiguration()
        {
            string jsonPath = $"{Directory.GetCurrentDirectory()}\\settings.json";
            if (File.Exists(jsonPath))
            {
                Configuration = new ConfigurationBuilder()
                    .AddJsonFile(jsonPath, optional: true, reloadOnChange: true)
                    .Build();
            }
            else
            {
                Console.WriteLine($"ERROR while loading settings. File settings.json not found.");
            }
        }

        private void GetCitiesConfiguration()
        {
            var cities = new List<City>();
            try
            {
                Configuration.GetSection("Cities").Bind(cities);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            CityList.AddRange(cities);
            foreach (var item in cities)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"{item.CityId} {item.CityName}");
                Console.ResetColor();
            }
        }

        private void GetDbConfiguration()
        {
            DbContextOptions = new DbContextOptionsBuilder<CianContext>()
                .UseSqlServer(Configuration.GetConnectionString("Default")).Options;
        }

        public async Task LoadOffers()
        {
            var watch = Stopwatch.StartNew();

            foreach (var city in CityList)
            {
                if (city.DealTypes.Count > 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"Loading {city.CityName}.");
                    Console.ResetColor();
                    await GetOffers(city);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"{city.CityName} has been loaded.");
                    Console.ResetColor();
                }
                SaveOffers(city);
            }

            watch.Stop();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Finish for {watch.Elapsed}");
            Console.ResetColor();
        }

        public async Task GetOffers(City city)
        {
            string link = "";
            foreach (var dealType in city.DealTypes)
            {
                var offersList = new List<Offer>();
                for (float lat = city.UpperCorner.Latitude; lat > city.LowerCorner.Latitude; lat -= city.Step)
                {
                    for (float lon = city.UpperCorner.Longtitude; lon > city.LowerCorner.Longtitude; lon -= city.Step)
                    {
                        try
                        {
                            bool isLoaded = false;
                            int attempts = 0;
                            link = $@"https://ekb.cian.ru/cian-api/mobile-site/v2/offers/clusters/?deal_type={dealType}&engine_version=2&offer_type=flat&screen_area=800&bbox={lat-city.Step:F4},{lon-city.Step:F4},{lat:F4},{lon:F4}&allow_precision_correction=0&zoom=15";
                            do
                            {
                                attempts++;
                                var offers = await ExtractPart(link);
                                if (offers != null)
                                {
                                    isLoaded = true;
                                    if(offers.Count > 0)
                                    {
                                        offersList.AddRange(offers.Where(o => !offersList.Contains(o)));
                                    }
                                    break;
                                }
                                if (attempts > 2 && !isLoaded)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine($"<<<ERROR {link} WASN'T LOADED! ATTEMPTS IS UP >>>");
                                    Console.ResetColor();
                                    isLoaded = true;
                                }
                            } while (!isLoaded);
                        }
                        catch (Exception ex)
                        {
                            Log(ex.Message);
                        }
                    }
                }

                foreach (var item in ErrorUrls)
                {
                    LogToFile(item);
                }
                ErrorUrls.Clear();
                    
                offersList.Select(o => { o.insert_date = DateTime.Now; o.city = city.CityId; return o; }).ToList();
                city.CityOffers.Add(offersList);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"{DateTime.Now}: For {city.CityName} {dealType} was loaded {offersList.Count} offers.");
                Console.ResetColor();
            }
        }

        private async Task<List<Offer>> ExtractPart(string url)
        {
            string responseContent = default(string);
            try
            {
                await Task.Delay(100 + random.Next(100));
                client.BaseUrl = new Uri(url);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    responseContent = response.Content;
                    var cianJson = JsonConvert.DeserializeObject<OffersResponse>(responseContent);
                    if (cianJson?.data?.itemsCount > 0)
                    {
                        Console.WriteLine($"{cianJson.data.offers.Length}");
                        return cianJson.data.offers.ToList();
                    }
                    else
                    {
                        Log(url);
                        return new List<Offer>();
                    }
                }    
            }
            catch (Exception ex)
            {
                CheckCaptcha(responseContent);
                ErrorUrls.Add(url);
                Console.WriteLine(ex.Message);
                return null;
            }
            return null;
        }

        private void CheckCaptcha(string page)
        {
            var htmldoc = new HtmlDocument();
            htmldoc.LoadHtml(page);
            var captcha = htmldoc.DocumentNode.SelectSingleNode("//form[@id='form_captcha']")?.Name;//?. innertext.trim();
            if (!String.IsNullOrEmpty(captcha))
            {
                Console.WriteLine("Enter new antibot token:");
                var token = Console.ReadLine();
                InitClient(token);
            }
            //var captcha = htmldoc.DocumentNode.SelectSingleNode(@"//form[@id=""form_captcha""");//?. innertext.trim();
        }

        public void SaveOffers(City city)
        {
            if (city.SaveToCSV)
            {
                SaveToCsv(city);
            }
            if (city.SaveToBase)
            {
                SaveToBase(city);
            }
        }

        private void SaveToBase(City city)
        {
            try
            {
                using (var dbContext = new CianContext(DbContextOptions))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"Saving {city.CityName} to database.");
                    Console.ResetColor();
                    foreach (var offerList in city.CityOffers)
                    {
                        dbContext.AddRange(offerList);
                    }
                    dbContext.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"{city.CityName} saved to database.");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        private void SaveToCsv(City city)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"Saving {city.CityName} to csv.");
                Console.ResetColor();
                using (StreamWriter sw = File.CreateText($"{city.CityName} {DateTime.Now.ToString("yyyy-MM-dd")}.csv"))
                {
                    sw.WriteLine("cian_id,category,village_id,added,house_id,newobject_id,photo,price,object_type,lon,filter_type,creation_date,deal_type,from_developer,lat,service_id,property_type,id,type,city,insert_data");
                    foreach (var offerList in city.CityOffers)
                    {
                        foreach (var item in offerList)
                        {
                            sw.WriteLine(item);
                        }
                    }
                }
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"{city.CityName} saved to csv.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        private void Log(string message)
        {
            Console.WriteLine(message);
        }

        private void LogToFile(string message) 
        {
            string path = @"market.txt";

            if (!File.Exists(path))
            {
                File.CreateText(path);
            }

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(message);
            }
        }
    }
}
