using System;

namespace ControlPlane.Shared
{
    public partial class BaseEntities
    {
        public BaseEntities()
        {
            this.CreatedDate = DateTime.UtcNow.ToString();
            //this.UpdatedDate = DateTime.UtcNow.ToString();
        }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public string? Comment { get; set; }

    }
}
