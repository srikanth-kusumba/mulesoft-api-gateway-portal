/*
Implemented by srikanth.kusumba
MuleSoft API portal=> Class for Dynamo Client Policy Serialization & Deserialization
*/

using Newtonsoft.Json;
using System.Collections.Generic;

namespace ControlPlane.Data
{
    public class DynamoClientPolicy
    {
        [JsonProperty(PropertyName = "config_id")]
        public string Config_id { get; set; }

        [JsonProperty(PropertyName = "operation")]
        public string Operation { get; set; }

        [JsonProperty(PropertyName = "iamPolicy")]
        public AuthPolicy AuthPolicy { get; set; }
    }

    [JsonObject ("iamPolicy")]
    public class AuthPolicy
    {
        [JsonProperty(PropertyName = "principalId")]
        public string PrincipalId { get; set; }

        [JsonProperty(PropertyName = "policyDocument")]
        public PolicyDocument PolicyDocument { get; set; }

    }
    public class PolicyDocument
    {
        [JsonProperty(PropertyName = "Version")]
        public string Version { get; set; }

        [JsonProperty(PropertyName = "Statement")]
        public List<Statement> Statement { get; set; }
    }

    public class Statement
    {
        [JsonProperty(PropertyName = "Action")]
        public string Action { get; set; }
        [JsonProperty(PropertyName = "Effect")]
        public string Effect { get; set; } = "Deny"; // Default to Deny to ensure Allows are explicitly set
        [JsonProperty(PropertyName = "Resource")]
        public List<string> Resource { get; set; }
    }
}
