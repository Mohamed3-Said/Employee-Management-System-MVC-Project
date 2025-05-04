using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBusinessLogic.Services.AttachmentService
{
    public interface IAttachmentService
    {
        //Upload
        public string? Upload(IFormFile file, string FolderName);

        //Delete
        bool Delete(string filePath);
    }
}
