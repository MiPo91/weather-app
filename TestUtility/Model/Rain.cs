using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestUtility.Model
{
    public class Rain
    {
        [JsonProperty("3h")]
        public double treehours { get; set; }
    }
}
