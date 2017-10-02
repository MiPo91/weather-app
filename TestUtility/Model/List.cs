using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestUtility.Model
{
    public class List
    {
        public int Dt { get; set; }
        public Main Main { get; set; }
        public IList<Weather> Weather { get; set; }
        public Clouds Clouds { get; set; }
        public Wind Wind { get; set; }
        public Rain Rain { get; set; }
        public Sys Sys { get; set; }
        [JsonProperty("dt_txt")]
        public string Dttxt { get; set; }
    }
}
