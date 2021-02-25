using Business.Abstract;
using Business.Constants;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Business.Concrete
{
    public class ImageDetailManager :IImageDetailService
    {
       
        IImageDetailDal _imageDetailDal;
    


        public ImageDetailManager(ICarDal carDal, IImageDetailDal imageDetailDal)
        {
            _imageDetailDal = imageDetailDal;
        }

        //[ValidationAspect(typeof(CarValidator))]  // 5 den fazla resim olamaz
        

        public IResult Add(ImageDetailUpload imageDetailUpload)
        {

            string path = @"Images\";
            string guidKey = Guid.NewGuid().ToString();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream fileStream = File.Create(path + guidKey))
            {
                path += guidKey;
                imageDetailUpload.Images.CopyTo(fileStream);
                fileStream.Flush();
                
            }
            ImageDetail imageDetail = new ImageDetail();
            imageDetail.ImagePath = path;
            imageDetail.CarImageId = imageDetailUpload.CarImageId;

            _imageDetailDal.Add(imageDetail);
            return new SuccessResult("ok");
        }

        // [ValidationAspect(typeof(CarValidator))]
        public IResult Update(ImageDetailUpload imageDetailUpload)
        {
            var result = _imageDetailDal.GetById(c => c.ImageId == imageDetailUpload.GelenResimId);
            var silinecekPath = result.ImagePath;

            File.Delete(silinecekPath);

            string eklenecekPath = @"Images\";
            string guidKey = Guid.NewGuid().ToString();

            if (!Directory.Exists(eklenecekPath))
            {
                Directory.CreateDirectory(eklenecekPath);
            }
            using (FileStream fileStream = File.Create(eklenecekPath + guidKey))
            {
                eklenecekPath += guidKey;
                imageDetailUpload.Images.CopyTo(fileStream);
                fileStream.Flush();

            }
            result.ImagePath = eklenecekPath;
            //result.CarImageId = imageDetailUpload.CarImageId;
            _imageDetailDal.Update(result);
            return new SuccessResult("OK");

        }

        public IResult Delete(int imageId)
        {
            
            var result = _imageDetailDal.GetById(c => c.ImageId == imageId);
            var path = result.ImagePath;
            File.Delete(path);
            _imageDetailDal.Delete(result);
            return new SuccessResult();

        }



        public IDataResult<FileStream> GetById(int imageId)
        {
            var path = _imageDetailDal.GetById(c => c.ImageId == imageId).ImagePath;

            var formFile = File.OpenRead(path);
            
            var result = new SuccessDataResult<FileStream>(formFile);
            return result;
        }

    }
}
