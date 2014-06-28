﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecureWeb.Models {
    public class Detection : BaseModel  {

        public string CameraName { get; set; }
        public string Image { get; set; }
        public DateTime dateUploaded { get; set; }
    }
}