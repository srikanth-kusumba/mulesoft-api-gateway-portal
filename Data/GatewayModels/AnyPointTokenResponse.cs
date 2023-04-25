namespace ControlPlane.Data.GatewayModels
{
    public class AnyPointTokenResponse
    {
        public string access_token { get; set; }
        public string TokenType { get; set; }
        public long? expires_in { get; set; }
        public string id_token { get; set; }
    }
}
