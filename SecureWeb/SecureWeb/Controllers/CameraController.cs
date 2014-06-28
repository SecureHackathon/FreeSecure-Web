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
        public IEnumerable<Camera> Get() {
            return _repository.GetAll<Camera>();
        }
    }
}
