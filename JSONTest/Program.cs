using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace jsonTest
{
    class Program
    {
        public class veri
        {
            [JsonProperty(PropertyName = "id")]
            public string ID { get; set; }

            [JsonProperty(PropertyName = "channel")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "title")]
            public string Title { get; set; }

            [JsonProperty(PropertyName = "full_link")]
            public string Link { get; set; }
        }


        static void Main(string[] args)
        {
            string str = "[{\"id\":\"-UKfDXK2ADk\",\"channel\":\"Test Divertidos\",\"title\":\"\u00bfCU\u00c1L ES TU TALENTO? Test Divertidos de verialidad\",\"full_link\":\"https://youtube.com/watch?v=-UKfDXK2ADk\"},{\"id\":\"t-mQvJ9Onhs\",\"channel\":\"Test Ex\",\"title\":\"Experiment Car vs Pepsi | Crushing crunchy &amp; soft things by car | Test Ex\",\"full_link\":\"https://youtube.com/watch?v=t-mQvJ9Onhs\"}]";
            var degisken = JsonConvert.DeserializeObject<List<veri>>(str);

            foreach (var veri in degisken)
            {
                Console.WriteLine(
                    string.Format("ID: {0}, Channel: {1}, Title: {2}, Link: {3}",
                                    veri.ID,
                                    veri.Name,
                                    veri.Title,
                                    veri.Link));
            }

            Console.ReadKey();
        }
    }
}