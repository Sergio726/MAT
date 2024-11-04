using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Integration.BackendApi.Models
{
    
    public class ErrorDto
    {
        [JsonProperty("ok")]
        public bool ok { get; set; }

        [JsonProperty("error")]
        public string error { get; set; }

        [JsonProperty("statusCode")]
        public int statusCode { get; set; }

        [JsonProperty("timestamp")]
        public DateTime timestamp { get; set; }

        [JsonProperty("path")]
        public string path { get; set; }

        [JsonProperty("cause")]
        public Cause cause { get; set; }
    }

    public class Cause
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("clientVersion")]
        public string clientVersion { get; set; }
    }
}