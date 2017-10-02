using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;


namespace TestUtility.Model
{
    public class Main
    {/*
        public double Temp { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        [JsonProperty("temp_min")]
        public double TempMin { get; set; }
        [JsonProperty("temp_max")]
        public double TempMax { get; set; }
        [JsonProperty("sea_level")]
        public double SeaLevel { get; set; }
        [JsonProperty("grnd_level")]
        public double GroundLevel { get; set; }
        */
        public double Temp { get; set; }
        [JsonProperty("temp_min")]
        public double TempMin { get; set; }
        [JsonProperty("temp_max")]
        public double TempMax { get; set; }
        public double Pressure { get; set; }
        [JsonProperty("sea_level")]
        public double SeaLevel { get; set; }
        [JsonProperty("grnd_level")]
        public double GroundLevel { get; set; }
        public int Humidity { get; set; }
        [JsonProperty("temp_kf")]
        public double TempKf { get; set; }
    }
}
