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
        }

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse <IEnumerable<Camera>>(HttpStatusCode.Created, _repository.GetAll<Camera>());
        }

        public HttpResponseMessage Get(string id)
        {
            var camera = _repository.Get<Camera>(id);
            return Request.CreateResponse<Camera>(HttpStatusCode.OK, camera);
        }

        public HttpResponseMessage Post(Camera camera) 
        {
            if (_repository.Save<Camera>(camera))
            {
                return Request.CreateResponse<Camera>(HttpStatusCode.Created, camera);
            }
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,"Error trying to process request");
        }

        public HttpResponseMessage Put(Camera camera)
        {
            if (_repository.Update<Camera>(camera))
            {
                return Request.CreateResponse<Camera>(HttpStatusCode.Created, camera);
            }
           return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error trying to process request");
        }

        public HttpResponseMessage Delete(string id)
        {
            if (_repository.Delete<Camera>(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.Accepted, "");
        }
    }
}
