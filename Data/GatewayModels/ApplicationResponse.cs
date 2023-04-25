using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ControlPlane.Data.GatewayModels
{
    public partial class ApplicationResponse
    {

        //[JsonProperty("apiEndpoints")]
        //public bool ApiEndpoints { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        [JsonProperty("clientSecret")]
        public string ClientSecret { get; set; }

        [JsonProperty("masterOrganizationId")]
        public Guid MasterOrganizationId { get; set; }

        [JsonProperty("grantTypes")]
        public string[] GrantTypes { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("clientProvider")]
        public ClientProvider ClientProvider { get; set; }
    }

    public partial class ClientProvider
    {
        [JsonProperty("providerId")]
        public object ProviderId { get; set; }
    }
}

