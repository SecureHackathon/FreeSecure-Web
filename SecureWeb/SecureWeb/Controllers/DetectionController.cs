using SecureWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SecureWeb.Controllers
{
    public class DetectionController : BaseApiController
    {

        public IEnumerable<Detection> Get() {
            return _repository.GetAll<Detection>();
        }

        public void Post( [FromBody]string value ) {
        }
    }
}
