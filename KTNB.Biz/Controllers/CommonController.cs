﻿using System.Collections.Generic;
using System.Web.Http;

namespace KTNB.Biz.Controllers
{
    public class CommonController : BaseApiController
    {
        // GET api/Common
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/Common/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/Common
        public void Post([FromBody]string value)
        {
        }

        // PUT api/Common/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/Common/5
        public void Delete(int id)
        {
        }
    }
}