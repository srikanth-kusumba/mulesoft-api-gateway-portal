using System;
using Newtonsoft.Json;

namespace ControlPlane.Data.GatewayModels.Deployments
{
    public class DeploymentRequest
    {
        [JsonProperty("masterOrganizationId")]
        public string MasterOrganizationId { get; set; }

        [JsonProperty("organizationId")]
        public string OrganizationId { get; set; }

        //[JsonProperty("id")]
        //public long Id { get; set; }

        [JsonProperty("instanceLabel")]
        public string InstanceLabel { get; set; }

        [JsonProperty("groupId")]
        public string GroupId { get; set; }

        [JsonProperty("assetId")]
        public string AssetId { get; set; }

        [JsonProperty("assetVersion")]
        public string AssetVersion { get; set; }

        [JsonProperty("productVersion")]
        public string ProductVersion { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("order")]
        public long Order { get; set; }

        [JsonProperty("providerId")]
        public object ProviderId { get; set; }

        [JsonProperty("deprecated")]
        public bool Deprecated { get; set; }

        [JsonProperty("lastActiveDate")]
        public DateTimeOffset LastActiveDate { get; set; }

        [JsonProperty("endpointUri")]
        public string EndpointUri { get; set; }

        [JsonProperty("environmentId")]
        public string EnvironmentId { get; set; }

        [JsonProperty("isPublic")]
        public bool IsPublic { get; set; }

        [JsonProperty("stage")]
        public string Stage { get; set; }

        [JsonProperty("technology")]
        public string Technology { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("endpoint")]
        public Endpoint Endpoint { get; set; }

        [JsonProperty("Spec")]
        public Spec Spec { get; set; }

        [JsonProperty("deployment")]
        public Deployment Deployment { get; set; }

        [JsonProperty("autodiscoveryInstanceName")]
        public string AutodiscoveryInstanceName { get; set; }
    }

    public class Deployment
    {
        //[JsonProperty("id")]
        //public long Id { get; set; }

        [JsonProperty("applicationId")]
        public string ApplicationId { get; set; }

        [JsonProperty("applicationName")]
        public string ApplicationName { get; set; }

        [JsonProperty("gatewayVersion")]
        public string GatewayVersion { get; set; }

        [JsonProperty("environmentName")]
        public object EnvironmentName { get; set; }

        [JsonProperty("environmentId")]
        public string EnvironmentId { get; set; }

        [JsonProperty("targetId")]
        public string TargetId { get; set; }

        [JsonProperty("targetName")]
        public string TargetName { get; set; }

        [JsonProperty("deploymentId")]
        public object DeploymentId { get; set; }

        [JsonProperty("updatedDate")]
        public DateTimeOffset UpdatedDate { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("expectedStatus")]
        public string ExpectedStatus { get; set; }

        //[JsonProperty("apiId")]
        //public long ApiId { get; set; }
    }

    public class Endpoint
    {
        //[JsonProperty("id")]
        //public long Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("apiGatewayVersion")]
        public string ApiGatewayVersion { get; set; }

        [JsonProperty("proxyUri")]
        public string ProxyUri { get; set; }

        [JsonProperty("proxyRegistrationUri")]
        public object ProxyRegistrationUri { get; set; }

        [JsonProperty("lastActiveDate")]
        public object LastActiveDate { get; set; }

        [JsonProperty("isCloudHub")]
        public bool IsCloudHub { get; set; }

        [JsonProperty("deploymentType")]
        public string DeploymentType { get; set; }

        [JsonProperty("policiesVersion")]
        public object PoliciesVersion { get; set; }

        [JsonProperty("referencesUserDomain")]
        public bool ReferencesUserDomain { get; set; }

        [JsonProperty("responseTimeout")]
        public object ResponseTimeout { get; set; }

        [JsonProperty("wsdlConfig")]
        public object WsdlConfig { get; set; }

        [JsonProperty("muleVersion4OrAbove")]
        public bool MuleVersion4OrAbove { get; set; }
    }

}
