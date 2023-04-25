using System.Text.Json.Serialization;

namespace ControlPlane.Data.GatewayModels
{
    public class ExchangeAssetResponse
    {
        [JsonPropertyName("organizationId")]
        public string OrganizationId { get; set; }

        [JsonPropertyName("groupId")]
        public string GroupId { get; set; }

        [JsonPropertyName("assetId")]
        public string AssetId { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
