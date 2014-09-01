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

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse <IEnumerable<Detection>>(HttpStatusCode.OK, _repository.GetAll<Detection>());
        }

        public HttpResponseMessage Get(string id)
        {
            return Request.CreateResponse<Detection>(HttpStatusCode.OK, _repository.Get<Detection>(id));
        }

        public HttpResponseMessage Post(Detection detection)
        {
            if (_repository.Save<Detection>(detection))
            {

            }
            return null;
        }

        public HttpResponseMessage Post(string value ) {
            var queryvals = Request.RequestUri.ParseQueryString();
            return Request.CreateResponse(HttpStatusCode.Accepted, queryvals);
        }
    }
}
