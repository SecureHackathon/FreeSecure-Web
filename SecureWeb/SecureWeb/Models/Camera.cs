using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecureWeb.Models {
    public class Camera :BaseModel {
      
        public string Name { get; set; }
        public bool Active { get; set; }
        
    }
}