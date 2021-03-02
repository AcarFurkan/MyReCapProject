using Core.Utilities.Result;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Business.Helpers
{
    public class FileHelper
    {
        public static string defaultPath = @"C:\Users\furka\source\repos\MyReCapProject\WebAPI\Images\default.jpg";
        public static IDataResult<List<string>> Add(CarImagesUploaded carImagesUploaded)
        {
            //string path = @"C:\Users\furka\source\repos\MyReCapProject\WebAPI\Images\";

            List<string> tempPath = new List<string>();
            if (carImagesUploaded.Images == null)//bu add image olmayan eklemeler icin
            {
                tempPath.Add(null);

                return new SuccessDataResult<List<string>>(tempPath,"resimsiz sekilde olusturuldu");
            }
            for (int i = 0; i < carImagesUploaded.Images.Count; i++)
            {
                string guidKey = Guid.NewGuid().ToString("N");
                string path = @"C:\Users\furka\source\repos\MyReCapProject\WebAPI\Images\";
                /*var fileExists = CheckFileExists(carImagesUploaded.Images[i]);
                if (fileExists.Message != null)
                {
                    return new ErrorDataResult<string>(fileExists.Message);
                }*/

                CreateDirectory(path);
                path += guidKey;
                path = CreateImageFile(path, carImagesUploaded.Images[i]);
                tempPath.Add(path);
            }
            if (tempPath.Count == 1)
            {
                return new SuccessDataResult<List<string>>(tempPath, "file added");
            }
            return new SuccessDataResult<List<string>>(tempPath, "files added");

        }
        public static IDataResult<string> Update(CarImageUploadedApi carImageUploaded,string sourcePath)//helperi core katmanindan kaldirirsan iki parametre problemini cozersin
        {
            // araba id kontroluude yapmak lazimm ama o business da yapmallisin
            
            string guidKey = Guid.NewGuid().ToString("N");
            string path = @"C:\Users\furka\source\repos\MyReCapProject\WebAPI\Images\";
            var fileExists = CheckFileExists(carImageUploaded.Images);
            if (fileExists.Message != null)
            {
                return new ErrorDataResult<string>(fileExists.Message);
            }
            File.Delete(sourcePath);
            CreateDirectory(path);
            path += guidKey;
            path = CreateImageFile(path, carImageUploaded.Images);
            return new SuccessDataResult<string>(path, "file updated!!");

            // datanin file path donmesi lazim bunu unutma
        }
        public static IResult Delete(string sourcePath)//helperi core katmanindan kaldirirsan iki parametre problemini cozersin
        {
            File.Delete(sourcePath);
            return new SuccessResult("file updated!!");

        }

        private static IResult CheckFileExists(IFormFile file)
        {
            if (file.Length > 0)
            {
                return new SuccessResult();
            }
            return new ErrorResult("dosya mevcut degil.");
        }

        private static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        private static string CreateImageFile(string path, IFormFile file)
        {
            var type = Path.GetExtension(file.FileName);
            var typevalid = CheckFileTypeValid(type);
            if (!typevalid.Success)
            {
                return typevalid.Message;
            }

            path += type;
            using (FileStream fileStream = File.Create(path))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            return path;
        }
        private static IResult CheckFileTypeValid(string type)
        {
            if (type != ".jpeg" && type != ".png" && type != ".jpg")
            {
                return new ErrorResult("Wrong file type.");
            }
            return new SuccessResult();
        }

       


        /// YENISIINI YAZDIM
        /*
        public static IDataResult<string> Add(CarImageUploadedApi carImageUploaded) 
        {
            //string path = @"C:\Users\furka\source\repos\MyReCapProject\WebAPI\Images\";
            string guidKey = Guid.NewGuid().ToString("N");
            string path = @"C:\Users\furka\source\repos\MyReCapProject\WebAPI\Images\";
            var fileExists = CheckFileExists(carImageUploaded.Images);
            if (fileExists.Message != null)
            {
                return new ErrorDataResult<string>(fileExists.Message);
            }
            
            CreateDirectory(path);
            path += guidKey;
            path = CreateImageFile(path, carImageUploaded.Images);
            return new SuccessDataResult<string>(path,"file added!!"); // messagese a file added i eklemeyi unutma
            
        }*/



    }
}
