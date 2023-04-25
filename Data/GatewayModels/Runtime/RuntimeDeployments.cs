using System.Collections.Generic;

namespace ControlPlane.Data.GatewayModels.Runtime
{
    public class RuntimeDeployments
    {
        public int total { get; set; }
        public List<Item> items { get; set; }
    }
   
    public class Item
    {
        public string id { get; set; }
        public string name { get; set; }
        public long creationDate { get; set; }
        public long lastModifiedDate { get; set; }
        public Target target { get; set; }
        public string status { get; set; }
        public Application application { get; set; }
    }

    public class Target
    {
        public string provider { get; set; }
        public string targetId { get; set; }
        public DeploymentSettings deploymentSettings { get; set; }

        public int replicas { get; set; } = 1;
    }

    public class Application
    {
        public string status { get; set; }
    }

    public class PatchDeployment
    {
        public string id { get; set; }
        public string name { get; set; }
        public Target target { get; set; }
    }
    public class DeploymentSettings
    {
        public Http http { get; set; }
        public string publicUrl { get; set; }
        public bool generateDefaultPublicUrl { get; set; }

        public string updateStrategy { get; set; } = "rolling";

        public string cpuReserved { get; set; } = "200m";
        public string cpuMax { get; set; } = "1000m";
        public string memoryReserved { get; set; } = "1000Mi";
        public string memoryMax { get; set; } = "1000Mi";

    }

    public class Http
    {
        public Inbound inbound { get; set; }
    }

    public class Inbound
    {
        public string publicUrl { get; set; }
    }

   

}
