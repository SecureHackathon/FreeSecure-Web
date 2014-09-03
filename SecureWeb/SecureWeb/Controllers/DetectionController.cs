using SecureWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SecureWeb.Controllers
{
    public class DetectionController : BaseApiController
    {

        public HttpResponseMessage Get()
        {
            IEnumerable<Detection> detections = _repository.GetAll<Detection>().Where(o => o.ImageViewed = false);
            return Request.CreateResponse <IEnumerable<Detection>>(HttpStatusCode.OK, detections);
        }

        public HttpResponseMessage Get(string id)
        {
            return Request.CreateResponse<Detection>(HttpStatusCode.OK, _repository.Get<Detection>(id));
        }

        public async Task<HttpResponseMessage> Post()
        {  
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            
            string fileSaveLocation = HttpContext.Current.Server.MapPath("~/Detections");
            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);
            List<string> files = new List<string>();
            try
            {
           
                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (MultipartFileData file in provider.FileData)
                {
                    var filename = Path.GetFileName(file.LocalFileName);
                    var cameraName = filename.Split(new char[] {'_'})[0];
                    files.Add(Path.GetFileName(file.LocalFileName));
                    Detection model = new Detection() { 
                        CameraName=cameraName,
                        ImageViewed = false,
                        ImageUrl=filename, 
                        DateCreated = DateTime.Now
                    };
                    _repository.Save<Detection>(model);
                }
                
                return Request.CreateResponse(HttpStatusCode.Created, "Image successfully uploaded to server");
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed to save image to server");
            }
        }

        public HttpResponseMessage Post(string value ) {
            var queryvals = Request.RequestUri.ParseQueryString();
            return Request.CreateResponse(HttpStatusCode.Accepted, queryvals);
        }
    }

    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            return headers.ContentDisposition.FileName.Replace("\"", string.Empty);
        }
    }
}
