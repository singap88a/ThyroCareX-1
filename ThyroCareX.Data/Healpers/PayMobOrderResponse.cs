using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ThyroCareX.Data.Healpers
{
    public class PayMobOrderResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
