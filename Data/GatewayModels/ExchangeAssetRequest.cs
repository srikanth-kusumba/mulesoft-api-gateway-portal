namespace ControlPlane.Data.GatewayModels
{
    public class ExchangeAssetRequest
    {
        public string OrganizationId { get; set; }
        public string GroupId { get; set; }
        public string AssetId { get; set; }
        public string AssetVersion { get; set; }
        public string AssetName { get; set; }
        public string Description { get; set; }
        public string PropertiesMainFile { get; set; }
        public string PropertiesAPIVersion { get; set; }
        public string OASFileName { get; set; }
        public byte[] OASFileBytes { get; set; }
        public string Classifier { get; set; }

    }
}
