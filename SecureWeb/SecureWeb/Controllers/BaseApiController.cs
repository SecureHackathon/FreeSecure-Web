using SecureWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SecureWeb.Controllers
{
    public class BaseApiController : ApiController
    {

        protected Repository _repository;
        public BaseApiController() {
            _repository = new Repository();
        }
    }
}
