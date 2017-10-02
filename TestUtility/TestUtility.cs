using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using TestUtility.Model;

namespace eKoodi.Utilities.Test
{

    public class ForecastNow
    {
        public Coord Coord { get; set; }
        public IList<Weather> Weather { get; set; }
        public string Base { get; set; }
        public Main Main { get; set; }
        public int Visibility { get; set; }
        public Wind Wind { get; set; }
        public Clouds Clouds { get; set; }
        public int Dt { get; set; }
        public Sys Sys { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cod { get; set; }
    }

    public class Forecast
    {
        public string Cod { get; set; }
        public double Message { get; set; }
        public int Cnt { get; set; }
        public IList<List> List { get; set; }
        public City City { get; set; }
    }

    public class TestUtility
    {

        static public string JsonFiles(string id)
        {
            var fileList = new Dictionary<string, string>
                        {
                            { "1", "ForecastNowJson.txt" },
                            { "2", "ForecastFiveDaysJson.txt" }
                        };

            return fileList[id];
        }

        static HttpClient client = new HttpClient();

        static public string GetAndSave(string type, string city, string country)
        {
            string url = string.Empty;
            if(type == "2")
            {
                DateTime fileUpdated = File.GetLastWriteTime("C:\\dev\\weather-app\\" + JsonFiles("2"));
                DateTime timeNow = DateTime.Now;
                TimeSpan span = timeNow.Subtract(fileUpdated);

                int differenceInMinutes;
                int.TryParse(span.Minutes.ToString(), out differenceInMinutes);

                if (differenceInMinutes < 180)
                {
                    Console.WriteLine("Displaying old resurt as it's less than 3 hours old.");
                    return "";
                }

                url = "http://api.openweathermap.org/data/2.5/forecast?q="+ city +","+ country + "&appid=c2d646c8555f1b4df7bb255df791dce1&units=metric&cnt=7";
            }
            else
            {
                DateTime fileUpdated = File.GetLastWriteTime("C:\\dev\\weather-app\\" + JsonFiles("1"));
                DateTime timeNow = DateTime.Now;
                TimeSpan span = timeNow.Subtract(fileUpdated);

                int differenceInMinutes;
                int.TryParse(span.Minutes.ToString(), out differenceInMinutes);

                if(differenceInMinutes < 10) {
                    Console.WriteLine("Displaying old resurt as it's less than 10 minutes old.");
                    return "";
                }

                url = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "," + country + "&appid=c2d646c8555f1b4df7bb255df791dce1&units=metric";
            }
            
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        StreamWriter json = new StreamWriter(ms);

                        json.WriteLine(result);
                        json.Flush();

                        //You have to rewind the MemoryStream before copying
                        ms.Seek(0, SeekOrigin.Begin);

                        using (FileStream fs = new FileStream("C:\\dev\\weather-app\\"+ JsonFiles(type), FileMode.OpenOrCreate))
                        {
                            ms.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                    return "API saved as Json";
                }
                else
                {
                    return "Fail";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Fail";
            }
        }

        static public void ForecastNow()
        {            
            FileStream fs = new FileStream("C:\\dev\\weather-app\\"+ JsonFiles("1"), FileMode.Open, FileAccess.Read);
            string json = string.Empty;
            using (StreamReader r = new StreamReader(fs))
            {
                json = r.ReadToEnd().Trim();
            }
            ForecastNow forecast = JsonConvert.DeserializeObject<ForecastNow>(json);

            Console.WriteLine("City: {0}", forecast.Name);
            Console.WriteLine("Temperature: {0} celcius", forecast.Main.Temp);
            Console.WriteLine("Wind: {0} meters / second", forecast.Wind.Speed);
            Console.WriteLine("Description: {0}", forecast.Weather[0].Description);
        }

        static public void ForecastFiveDays()
        {
            FileStream fs = new FileStream("C:\\dev\\weather-app\\" + JsonFiles("2"), FileMode.Open, FileAccess.Read);
            string json = string.Empty;
            using (StreamReader r = new StreamReader(fs))
            {
                json = r.ReadToEnd().Trim();
            }
            Forecast forecast = JsonConvert.DeserializeObject<Forecast>(json);

            for(int i = 0; i < forecast.List.Count; i++)
            {
                Console.WriteLine("\n Day {0}:", i);
                Console.WriteLine("Temperature: {0} celcius", forecast.List[i].Main.Temp);
                Console.WriteLine("Wind: {0} meters / second", forecast.List[i].Wind.Speed);
                Console.WriteLine("Description: {0}", forecast.List[i].Weather[0].Description);
            }
        }

    }
}
