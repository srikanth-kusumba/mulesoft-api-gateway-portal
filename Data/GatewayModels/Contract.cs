using Newtonsoft.Json;
using System;

namespace ControlPlane.Data.GatewayModels
{
    public class Contract
    {
        [JsonProperty("apiId")]
        public string ApiId { get; set; }

        [JsonProperty("instanceType")]
        public string InstanceType { get; set; } = "api";

        [JsonProperty("acceptedTerms")]
        public bool AcceptedTerms { get; set; }

        [JsonProperty("organizationId")]
        public string OrganizationId { get; set; }

        [JsonProperty("groupId")]
        public string GroupId { get; set; }

        [JsonProperty("assetId")]
        public string AssetId { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("versionGroup")]
        public string VersionGroup { get; set; } = "v1";
    }
}
