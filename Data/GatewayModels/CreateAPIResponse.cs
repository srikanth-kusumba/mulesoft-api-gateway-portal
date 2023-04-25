using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ControlPlane.Data.GatewayModels
{
    public partial class CreateAPIResponse
    {
        [JsonProperty("environmentId")]
        public Guid EnvironmentId { get; set; }

        [JsonProperty("instanceLabel")]
        public string InstanceLabel { get; set; }

        [JsonProperty("providerId")]
        public object ProviderId { get; set; }

        [JsonProperty("technology")]
        public string Technology { get; set; }

        [JsonProperty("assetVersion")]
        public string AssetVersion { get; set; }

        [JsonProperty("productVersion")]
        public string ProductVersion { get; set; }

        [JsonProperty("order")]
        public long Order { get; set; }

        [JsonProperty("stage")]
        public string Stage { get; set; }

        [JsonProperty("masterOrganizationId")]
        public Guid MasterOrganizationId { get; set; }

        [JsonProperty("organizationId")]
        public Guid OrganizationId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("groupId")]
        public Guid GroupId { get; set; }

        [JsonProperty("assetId")]
        public string AssetId { get; set; }

        [JsonProperty("tags")]
        public object[] Tags { get; set; }

        [JsonProperty("endpoint")]
        public Endpoint Endpoint { get; set; }

        [JsonProperty("autodiscoveryInstanceName")]
        public string AutodiscoveryInstanceName { get; set; }
    }
    
}
