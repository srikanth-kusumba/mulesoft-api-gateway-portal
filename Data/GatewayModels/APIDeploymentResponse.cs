using System;
using Newtonsoft.Json;

namespace ControlPlane.Data.GatewayModels
{
    public partial class APIDeploymentResponse
    {
        [JsonProperty("applicationName")]
        public string ApplicationName { get; set; }

        [JsonProperty("environmentId")]
        public Guid EnvironmentId { get; set; }

        [JsonProperty("targetId")]
        public Guid TargetId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("gatewayVersion")]
        public string GatewayVersion { get; set; }

        [JsonProperty("applicationId")]
        public Guid ApplicationId { get; set; }

        //[JsonProperty("audit")]
        //public Audit Audit { get; set; }

        [JsonProperty("masterOrganizationId")]
        public Guid MasterOrganizationId { get; set; }

        [JsonProperty("organizationId")]
        public Guid OrganizationId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("expectedStatus")]
        public string ExpectedStatus { get; set; }

        [JsonProperty("apiId")]
        public long ApiId { get; set; }
    }
    
}
