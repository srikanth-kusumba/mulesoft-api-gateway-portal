using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ControlPlane.Data
{
    public class APP
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }

    public class Product
    {
        public string Name { get; set; }
        public List<Proxy> Proxies { get; set; }
    }

    public class Proxy
    {
        public string Name { get; set; }
        public string URI { get; set; }
        public string Stage { get; set; }
        public string API_ID { get; set; }
        public string HttpMethod { get; set; }
    }
}
