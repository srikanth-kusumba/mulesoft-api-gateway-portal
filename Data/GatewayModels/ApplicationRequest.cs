using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ControlPlane.Data.GatewayModels
{
    public class ApplicationRequest
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("grantTypes")]
        public List<string> GrantTypes { get; set; } = new List<string>();

        [JsonProperty("apiEndpoints")]
        public bool ApiEndpoints { get; set; } = false;
    }
}
