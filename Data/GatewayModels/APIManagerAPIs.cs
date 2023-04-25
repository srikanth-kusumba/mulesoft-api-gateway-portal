using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ControlPlane.Data.GatewayModels
{
    public class APIManagerAPIs
    {
        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("assets")]
        public List<Asset> Assets { get; set; }
    }

    public class Asset
    {
        //[JsonProperty("audit")]
        //public AssetAudit Audit { get; set; }

        [JsonProperty("masterOrganizationId")]
        public Guid MasterOrganizationId { get; set; }

        [JsonProperty("organizationId")]
        public Guid OrganizationId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("exchangeAssetName")]
        public string ExchangeAssetName { get; set; }

        [JsonProperty("groupId")]
        public Guid GroupId { get; set; }

        [JsonProperty("assetId")]
        public string AssetId { get; set; }

        [JsonProperty("apis")]
        public Api[] Apis { get; set; }

        [JsonProperty("totalApis")]
        public long TotalApis { get; set; }

        [JsonProperty("autodiscoveryApiName")]
        public string AutodiscoveryApiName { get; set; }
    }

    public class Api
    {
        [JsonProperty("audit")]
        public ApiAudit Audit { get; set; }

        [JsonProperty("masterOrganizationId")]
        public Guid MasterOrganizationId { get; set; }

        [JsonProperty("organizationId")]
        public Guid OrganizationId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("instanceLabel")]
        public string InstanceLabel { get; set; }

        [JsonProperty("groupId")]
        public Guid GroupId { get; set; }

        [JsonProperty("assetId")]
        public string AssetId { get; set; }

        [JsonProperty("assetVersion")]
        public string AssetVersion { get; set; }

        [JsonProperty("productVersion")]
        public string ProductVersion { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }

        [JsonProperty("tags")]
        public object[] Tags { get; set; }

        [JsonProperty("order")]
        public long Order { get; set; }

        [JsonProperty("providerId")]
        public object ProviderId { get; set; }

        [JsonProperty("deprecated")]
        public bool Deprecated { get; set; }

        [JsonProperty("lastActiveDate")]
        public DateTimeOffset? LastActiveDate { get; set; }

        [JsonProperty("endpointUri")]
        public object EndpointUri { get; set; }

        [JsonProperty("environmentId")]
        public Guid EnvironmentId { get; set; }

        [JsonProperty("isPublic")]
        public bool IsPublic { get; set; }

        [JsonProperty("stage")]
        public string Stage { get; set; }

        [JsonProperty("technology")]
        public string Technology { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("deployment")]
        public Deployment2 Deployment { get; set; }

        [JsonProperty("lastActiveDelta")]
        public long LastActiveDelta { get; set; }

        [JsonProperty("pinned")]
        public bool Pinned { get; set; }

        [JsonProperty("activeContractsCount")]
        public long ActiveContractsCount { get; set; }

        [JsonProperty("autodiscoveryInstanceName")]
        public string AutodiscoveryInstanceName { get; set; }
    }

    public class ApiAudit
    {
        [JsonProperty("created")]
        public PurpleAted Created { get; set; }

        [JsonProperty("updated")]
        public PurpleAted Updated { get; set; }
    }

    public class PurpleAted
    {
        [JsonProperty("date")]
        public DateTimeOffset? Date { get; set; }
    }

    public class Deployment2
    {
        [JsonProperty("applicationId")]
        public Guid? ApplicationId { get; set; }

        [JsonProperty("targetId")]
        public Guid? TargetId { get; set; }

        [JsonProperty("expectedStatus")]
        public string ExpectedStatus { get; set; }
    }

}

