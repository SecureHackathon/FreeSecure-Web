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
                //Camera cam = new Camera
                //{
                //    Name = "CamCoder",
                //    Active = false
                //};

                //Camera cam1 = new Camera
                //{
                //    Name = "Cam2",
                //    Active = false,
                //    DateCreated = DateTime.Now
                //};

                //Camera cam2 = new Camera
                //{
                //    Name = "Cam3",
                //    Active = false,
                //    DateCreated = DateTime.Now
                //};

                //_repository.Save<Camera>(cam);
                //_repository.Save<Camera>(cam1);
                //_repository.Save<Camera>(cam2);
        }
        
        public IEnumerable<Camera> Get() {
            return _repository.GetAll<Camera>();
        }

        public Camera Get(string id){
            var cams = _repository.GetAll<Camera>();
            var cam = cams.FirstOrDefault(e => e.Id == id);
            return cam;
        }

        public HttpResponseMessage Post(Camera camera) {
            bool saved;
            saved = _repository.Save<Camera>(camera);
            if (saved)
            {
                var response = Request.CreateResponse<Camera>(HttpStatusCode.Created, camera);
                string uri = Url.Link("DefaultApi", new { id = camera.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else
            {
                var response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError,"Error trying to process request");
                return response;
            }
    
        }
        public HttpResponseMessage Put(Camera camera)
        {
            bool saved;
            saved = _repository.Update<Camera>(camera);
            if (saved)
            {
                var response = Request.CreateResponse<Camera>(HttpStatusCode.Created, camera);
                string uri = Url.Link("DefaultApi", new { id = camera.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else
            {
                var response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error trying to process request");
                return response;
            }
        }

        public void Delete(string id)
        {
            _repository.Delete<Camera>(id);
        }
        
    }
}
