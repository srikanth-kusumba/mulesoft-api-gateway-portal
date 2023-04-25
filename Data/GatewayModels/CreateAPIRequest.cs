using System;
using System.Collections.Generic;

using System.Globalization;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ControlPlane.Data.GatewayModels
{
    public class CreateAPIRequest
    {
        [JsonProperty("Endpoint")]
        public Endpoint Endpoint { get; set; }

        [JsonProperty("providerId")]
        public object ProviderId { get; set; } = null;

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("instanceLabel")]
        public string InstanceLabel { get; set; }

        [JsonProperty("Spec")]
        public Spec Spec { get; set; }
    }

    [DataContract(Name = "Endpoint")]
    public partial class Endpoint
    {
        [JsonProperty("deploymentType")]
        public string DeploymentType { get; set; } = "RF";

        [JsonProperty("isCloudHub")]
        public bool IsCloudHub { get; set; } = false;

        [JsonProperty("muleVersion4OrAbove")]
        public bool MuleVersion4OrAbove { get; set; } = true;

        [JsonProperty("proxyUri")]
        public string ProxyUri { get; set; } = "http://0.0.0.0:8081/";

        [JsonProperty("ProxyTemplate")]
        public ProxyTemplate ProxyTemplate { get; set; }

        [JsonProperty("referencesUserDomain")]
        public bool ReferencesUserDomain { get; set; } = false;

        [JsonProperty("responseTimeout")]
        public object ResponseTimeout { get; set; } = null;

        [JsonProperty("type")]
        public string Type { get; set; } = "rest-api";

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("validation")]
        public string Validation { get; set; } = "ENABLED";

    }

    public partial class ProxyTemplate
    {
        [JsonProperty("assetId")]
        public string AssetId { get; set; }

        [JsonProperty("assetVersion")]
        public string AssetVersion { get; set; }

        [JsonProperty("groupId")]
        public string GroupId { get; set; }
    }

    [DataContract(Name = "Spec")]
    public partial class Spec
    {
        [JsonProperty("assetId")]
        public string AssetId { get; set; }

        [JsonProperty("groupId")]
        public string GroupId { get; set; } = "03ca87cb-d3f4-49ff-969e-b9cc24e19446";

        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
