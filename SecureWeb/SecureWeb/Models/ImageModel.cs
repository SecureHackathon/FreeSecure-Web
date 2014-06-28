using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecureWeb.Models {
    public class ImageModel :BaseModel {
        public bool Viewed { get; set; }
        public string Url { get; set; }
    }
}