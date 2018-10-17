using System;

namespace Project.Domain {
    public class User : Entity {
        public string WorkNo { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public Guid BusinessId { get; set; }
        public virtual Business Business { get; set; }
        public string Permission { get; set; }
    }
}