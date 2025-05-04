using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DemoBusinessLogic.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        List<string> AllowedExtension = [".jpg" , ".png",".jpeg"];
        const  int maxsize = 2_097_152; //2MB
        public string? Upload(IFormFile file, string FolderName)
        {
            //1.Check Extension
            var extention = Path.GetExtension(file.FileName); //.jpg,.png,.jpeg
            if(!AllowedExtension.Contains(extention)) return null;

            //2.Check Size
            if (file.Length == 0 || file.Length > maxsize)
                return null;

            //3.Get Located Folder Path
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);

            //4.Make Attachment Name Unique-- GUID
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";

            //5.Get File Path
            var filepath = Path.Combine(FolderPath, fileName); // FileLocated

            //6.Create File Stream To Copy File[Unmanaged]
            using FileStream fs = new FileStream(filepath, FileMode.Create);

            //7.Use Stream To Copy File
            file.CopyTo(fs); // Copy File To FileStream

            //8.Return FileName To Store In Database
            return fileName; 
        }


        public bool Delete(string filePath)
        {
            if(!File.Exists(filePath)) return false;
            else
            {
                File.Delete(filePath);
                return true;
            }
        }

       
    }
}
