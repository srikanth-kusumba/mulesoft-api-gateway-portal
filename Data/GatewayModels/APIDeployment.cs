using System;
using Newtonsoft.Json;

namespace ControlPlane.Data.GatewayModels
{
    public partial class APIDeployment
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "RF";

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("target")]
        public Target Target { get; set; } 
    }

    public partial class Target
    {
        [JsonProperty("targetId")]
        public string TargetId { get; set; }

        [JsonProperty("targetName")]
        public string TargetName { get; set; }

        [JsonProperty("deploymentSettings")]
        public DeploymentSettings DeploymentSettings { get; set; }
    }

    public partial class DeploymentSettings
    {
        [JsonProperty("runtimeVersion")]
        public string RuntimeVersion { get; set; } = "4.4.0";
    }
}

