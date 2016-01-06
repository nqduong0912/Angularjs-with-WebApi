using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace KTNB.Biz.Controllers.FileExplorer
{
    public class FileExplorerController : BaseApiController
    {
        private readonly IFileManager _fileManager;

        public FileExplorerController()
            : this(new FileManager("~/Uploads/"))
        {
        }

        public FileExplorerController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        // GET: api/Upload/Get/5
        [HttpGet]
        public async Task<IHttpActionResult> Get(string id)
        {
            string fileName = id;

            var results = await _fileManager.Get(fileName);

            return Ok(new { photos = results });
        }

        // POST: api/Upload/Post
        [HttpPost]
        public async Task<IHttpActionResult> Post()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            try
            {
                var fileResult = await _fileManager.Add(Request);
                fileResult.Message = "Files uploaded ok";
                return Ok(fileResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        // DELETE: api/Upload/Delete/abc.exe
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string id)
        {
            string fileName = id;
            if (!_fileManager.FileExists(fileName))
            {
                return NotFound();
            }

            var result = await _fileManager.Delete(fileName);

            if (result.Successful)
            {
                return Ok(new { message = result.Message });
            }

            return BadRequest(result.Message);
        }
    }
}
