using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ControlPlane.Data.GatewayModels
{
    public partial class ExchangeAsset
    {
        [JsonProperty("organizationId")]
        public Guid OrganizationId { get; set; }

        [JsonProperty("groupId")]
        public Guid GroupId { get; set; }

        [JsonProperty("assetId")]
        public string AssetId { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("minorVersion")]
        public string MinorVersion { get; set; }

        [JsonProperty("versionGroup")]
        public string VersionGroup { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("isPublic")]
        public bool IsPublic { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("contactName")]
        public object ContactName { get; set; }

        [JsonProperty("contactEmail")]
        public object ContactEmail { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("createdById")]
        public Guid CreatedById { get; set; }
    }
}
