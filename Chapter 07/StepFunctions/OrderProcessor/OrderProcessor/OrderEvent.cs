using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessor
{
    public class OrderEvent
    {
        public string version { get; set; }
        public string id { get; set; }
        [JsonProperty("detail-type")]
        public string DetailType { get; set; }
        public string source { get; set; }
        public string account { get; set; }
        public DateTime time { get; set; }
        public string region { get; set; }
        public List<object> resources { get; set; }
        public Detail detail { get; set; }
    }

    public class Detail
    {
        public string orderId { get; set; }
        public string source { get; set; }
        public string message { get; set; }
    }
}
