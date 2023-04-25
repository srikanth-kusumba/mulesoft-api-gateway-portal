using System;
using Newtonsoft.Json;

namespace ControlPlane.Data.GatewayModels
{
    public class CreateAPIProxyRequest
    {
        [JsonProperty("technology")]
        public string Technology { get; set; } = "mule4";

        [JsonProperty("EndpointUri")]
        public string endpointUri { get; set; }

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

        [JsonProperty("deployment")]
        public ProxyDeployment Deployment { get; set; }
    }

    public class ProxyDeployment
    {
        [JsonProperty("environmentId")]
        public string EnvironmentId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("expectedStatus")]
        public string ExpectedStatus { get; set; } = "deployed";

        [JsonProperty("overwrite")]
        public bool Overwrite { get; set; } = false;

        [JsonProperty("gatewayVersion")]
        public string GatewayVersion { get; set; }

        [JsonProperty("applicationName")]
        public string ApplicationName { get; set; }

        [JsonProperty("targetId")]
        public string TargetId { get; set; }

        [JsonProperty("targetName")]
        public string TargetName { get; set; }

        [JsonProperty("targetType")]
        public string TargetType { get; set; }

        [JsonProperty("target")]
        public ProxyTarget Target { get; set; }
    }

    public class ProxyTarget
    {
        [JsonProperty("targetId")]
        public string TargetId { get; set; }

        [JsonProperty("deploymentSettings")]
        public ProxyDeploymentSettings DeploymentSettings { get; set; }
    }

    public class ProxyDeploymentSettings
    {
        [JsonProperty("runtimeVersion")]
        public string RuntimeVersion { get; set; } = "4.4.0";
    }

}
