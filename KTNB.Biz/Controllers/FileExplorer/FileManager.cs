using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using KTNB.Extended.Entities.FileExplorer;
using Newtonsoft.Json;

namespace KTNB.Biz.Controllers.FileExplorer
{
    public class FileManager : IFileManager
    {
        private string WorkingFolder { get; set; }

        public FileManager()
        {
        }

        public FileManager(string workingFolder)
        {
            this.WorkingFolder = workingFolder;
            CheckTargetDirectory();
        }

        public async Task<IEnumerable<FileResult>> Get(string fileName)
        {
            List<FileResult> files = new List<FileResult>();

            DirectoryInfo fileFolder = new DirectoryInfo(HttpContext.Current.Server.MapPath(WorkingFolder));

            await Task.Factory.StartNew(() =>
            {
                files = fileFolder.EnumerateFiles()
                                            .Where(fi => new[] { ".jpg", ".bmp", ".png", ".gif", ".tiff" }.Contains(fi.Extension.ToLower()))
                                            .Select(fi => new FileResult
                                            {
                                                Name = fi.Name,
                                                Created = fi.CreationTime,
                                                Modified = fi.LastWriteTime,
                                                Size = fi.Length / 1024
                                            })
                                            .ToList();
            });

            return files;
        }

        public async Task<UploadResult> Add(HttpRequestMessage request)
        {
            UploadResult fileResult = new UploadResult { Successful = true, ServerTime = DateTime.Now };
            var provider = new FileMultipartFormDataStreamProvider(HttpContext.Current.Server.MapPath(this.WorkingFolder));

            var result = await request.Content.ReadAsMultipartAsync(provider);

            // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
            // so this is how you can get the original file name
            // var originalFileName = GetDeserializedFileName(result.FileData.First());

            // uploadedFileInfo object will give you some additional stuff like file length,
            // creation time, directory name, a few filesystem methods etc..
            // var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);

            // Remove this line as well as GetFormData method if you're not
            // sending any form data with your upload request
            UploadDataModel fileUploadObj = GetFormData<UploadDataModel>(result);
            if (fileUploadObj != null)
            {
                fileResult.Environment = fileUploadObj.Environment;
            }

            fileResult.Files = new List<FileResult>();
            foreach (var file in provider.FileData)
            {
                var fileInfo = new FileInfo(file.LocalFileName);
                var uploadResult = new FileResult
                {
                    Id = Guid.NewGuid(),
                    Name = fileInfo.Name,
                    Path = WorkingFolder + fileInfo.Name,
                    Created = fileInfo.CreationTime,
                    Modified = fileInfo.LastWriteTime,
                    Size = fileInfo.Length / 1024
                };
                fileResult.Files.Add(uploadResult);
            }

            return fileResult;
        }

        public async Task<UploadResult> Delete(string fileName)
        {
            try
            {
                var filePath = Directory.GetFiles(HttpContext.Current.Server.MapPath(this.WorkingFolder), fileName).FirstOrDefault();

                await Task.Factory.StartNew(() =>
                {
                    File.Delete(filePath);
                });

                return new UploadResult { Successful = true, Message = fileName + "deleted successfully" };
            }
            catch (Exception ex)
            {
                return new UploadResult { Successful = false, Message = "error deleting fileName " + ex.GetBaseException().Message };
            }
        }

        public bool FileExists(string fileName)
        {
            var file = Directory.GetFiles(HttpContext.Current.Server.MapPath(this.WorkingFolder), fileName).FirstOrDefault();

            return file != null;
        }

        private void CheckTargetDirectory()
        {
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(this.WorkingFolder)))
            {
                throw new ArgumentException("The destination path " + this.WorkingFolder + " could not be found.");
            }
        }

        // Extracts Request FormatData as a strongly typed model
        private T GetFormData<T>(MultipartFormDataStreamProvider result)
           where T : class, new()
        {
            if (result.FormData.HasKeys())
            {
                var unescapedFormData = Uri.UnescapeDataString(result.FormData.GetValues(0).FirstOrDefault() ?? string.Empty);
                if (!string.IsNullOrEmpty(unescapedFormData))
                {
                    return JsonConvert.DeserializeObject<T>(unescapedFormData);
                }
            }

            return (T)null;
        }

        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }
    }
}