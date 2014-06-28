using SecureWeb.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace SecureWeb.Controllers {
    public class ImageController :BaseApiController {

        public ImageController() {
            ImageModel img = new ImageModel();
            img.Url = "http://catthoughtalog.tumblr.com/image/17235419661";
            ImageModel img2 = new ImageModel();
            img2.Url = "http://catthoughtalog.tumblr.com/image/17235419661";
            _repository.Save<ImageModel>(img);
            _repository.Save<ImageModel>(img2);
            _repository.Save<ImageModel>(img);

        }

        public async Task<HttpResponseMessage> Post() { // Check whether the POST operation is MultiPart? 
            if (!Request.Content.IsMimeMultipartContent()) {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType); 
            }
            // Prepare CustomMultipartFormDataStreamProvider in which our multipart form 
            // data will be loaded. 
            string fileSaveLocation = HttpContext.Current.Server.MapPath("~/App_Data"); 
            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation); 
            List<string> files = new List<string>(); 
            try { 
                // Read all contents of multipart message into CustomMultipartFormDataStreamProvider. 
                await Request.Content.ReadAsMultipartAsync(provider); 
                foreach (MultipartFileData file in provider.FileData) { 
                    files.Add(Path.GetFileName(file.LocalFileName)); 
                }
                ImageModel model = new ImageModel { Url = files[0], DateCreated = DateTime.Now };
                _repository.Save<ImageModel>(model); 
                // Send OK Response along with saved file names to the client. 
                return Request.CreateResponse(HttpStatusCode.OK, files); 
            } catch (System.Exception e) { 
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e); 
            } 
        }

        public IEnumerable<ImageModel> Get() {
            var newImages = _repository.GetAll<ImageModel>().Where(o => o.Viewed == false);
            var allImages = _repository.GetAll<ImageModel>();
            foreach ( ImageModel img in allImages ) {
                img.Viewed = true;
                _repository.Update<ImageModel>(img);
            }
            return newImages;
        }

        public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider { 
            public CustomMultipartFormDataStreamProvider(string path) : base(path) { } 
            
            public override string GetLocalFileName(HttpContentHeaders headers) { 
                return headers.ContentDisposition.FileName.Replace("\"", string.Empty); 
            } 
        }
    }
}