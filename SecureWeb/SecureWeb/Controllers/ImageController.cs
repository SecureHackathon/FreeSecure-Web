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

        public void Post(string id) {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            String fullPath="";
            if ( Request.Content.IsMimeMultipartContent() ) {
                Request.Content.ReadAsMultipartAsync<MultipartMemoryStreamProvider>(new MultipartMemoryStreamProvider()).ContinueWith(( task ) => {
                    MultipartMemoryStreamProvider provider = task.Result;
                    foreach ( HttpContent content in provider.Contents ) {
                        Stream stream = content.ReadAsStreamAsync().Result;
                        Image image = Image.FromStream(stream);
                        var testName = content.Headers.ContentDisposition.Name;
                        String filePath = HostingEnvironment.MapPath("~/Images/");
                        String fileName = id + ".jpg";
                        fullPath = Path.Combine(filePath, fileName);
                        image.Save(fullPath);
                        
                    }
                });
                ImageModel img = new ImageModel();
                img.Url = fullPath;
                _repository.Save<ImageModel>(img);

            }   
        }

        public IEnumerable<ImageModel> Get() {

            return _repository.GetAll<ImageModel>().Where(o => o.Viewed == false);
        }
       
    }
}
