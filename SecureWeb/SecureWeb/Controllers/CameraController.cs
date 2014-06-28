using SecureWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SecureWeb.Controllers
{
    public class CameraController :BaseApiController
    {
        public CameraController()
            : base() {
            Camera cam = new Camera {
                Name = "Cam1",
                Active = false,
                DateCreated = DateTime.Now
            };

            Camera cam1 = new Camera {
                Name = "Cam2",
                Active = false,
                DateCreated = DateTime.Now
            };

            Camera cam2 = new Camera {
                Name = "Cam3",
                Active = false,
                DateCreated = DateTime.Now
            };

            _repository.Save<Camera>(cam);
            _repository.Save<Camera>(cam1);
            _repository.Save<Camera>(cam2);
        }
        
        public IEnumerable<Camera> Get() {
            return _repository.GetAll<Camera>();
        }
        
    }
}
