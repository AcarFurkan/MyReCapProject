using Business.Abstract;
using Business.Constants;
using Business.Helpers;
using Core.Utilities.Business;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        public static string defaultPath = @"C:\Users\furka\source\repos\MyReCapProject\WebAPI\Images\default.jpg";

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }


        public IResult Add(CarImagesUploaded carImagesUploaded)
        {
            IResult result = BusinessRules.Run(CheckImagesLimitExceeded(carImagesUploaded));
            if (result != null)
            {
                return result;
            }

            var filePath = FileHelper.Add(carImagesUploaded);
            if (!filePath.Success)//bussinesrulles methodunu olusturabilirsin 
            {
                return new ErrorResult(filePath.Message);
            }

            for (int i = 0; i < filePath.Data.Count; i++)
            {
                CarImage carImage = new CarImage
                {
                    CarId = carImagesUploaded.CarId,
                    Date = DateTime.Now,
                    ImagePath = filePath.Data[i]
                };
                _carImageDal.Add(carImage);
                if (carImage.ImagePath!=null)
                {
                    CheckIfContainImageIsNull(carImagesUploaded.CarId);// eger eklenmeden once resimsiz eklenen carimage nesnesi varsa onlari kaldirir.
                }
                
            }
            
            return new SuccessResult("Ok");
        }

        public IResult Delete(CarImageUploadedApi carImageUploaded)
        {
            var carImage = _carImageDal.GetById(c => c.CarImageId == carImageUploaded.CarImageId);// business kontrolunu ekle 
            if (carImage.ImagePath==null)
            {
                _carImageDal.Delete(carImage);
                return new SuccessResult();
            }
            FileHelper.Delete(carImage.ImagePath);
            //File.Delete(carImage.ImagePath); bunu burda yaparsam methoda parametre gonddermekten kurtulurum bujnu da bir dusun
            _carImageDal.Delete(carImage);
            return new SuccessResult("Ok");
        }


        public IResult Update(CarImageUploadedApi carImageUploaded)
        {
            var carImage = _carImageDal.GetById(c => c.CarImageId == carImageUploaded.CarImageId);// business kontrolunu ekle 
            //File.Delete(carImage.ImagePath); bunu burda yaparsam methoda parametre gonddermekten kurtulurum bujnu da bir dusun
            var filePath = FileHelper.Update(carImageUploaded, carImage.ImagePath);
            if (!filePath.Success)//bussinesrulles methodunu olustur
            {
                return new ErrorResult(filePath.Message);
            }

            carImage.Date = DateTime.Now;
            carImage.ImagePath = filePath.Data;
            _carImageDal.Update(carImage);

            return new SuccessResult("Ok");
        }
        public IDataResult<CarImage> GetById(int Id)
        {
            IResult result = BusinessRules.Run(CheckIfImageIsNull(Id));
            var carImage = _carImageDal.GetById(c => c.CarImageId == Id);
            if (result != null)
            {
                carImage.ImagePath = defaultPath;
                return new SuccessDataResult<CarImage>(carImage);
            }

            return new SuccessDataResult<CarImage>(carImage);
        }

        public IDataResult<List<CarImage>> GetCarImagesByCarId(int Id)
        {
            IResult result = BusinessRules.Run(CheckIfImageIsNull(Id));
            var carImages = _carImageDal.GetAll(c => c.CarId == Id);
            if (result != null)
            {
                carImages[0].ImagePath = defaultPath;
                return new SuccessDataResult<List<CarImage>>(carImages);
            }

            return new SuccessDataResult<List<CarImage>>(carImages);
        }
        private IResult CheckIfImageIsNull(int id)
        {
            var result = _carImageDal.GetAll(c => c.CarId == id);
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].ImagePath.Equals(null) )
                {
                    return new ErrorResult();
                }
            }
           return new SuccessResult();

        }
        private void CheckIfContainImageIsNull(int id)// bu method resim eklendiginde default carImage oglesini silmek icin
        {
            var result = _carImageDal.GetAll(c => c.CarId == id);
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].ImagePath== null && result.Count != 1)//result[0].ImagePath==""
                {
                    _carImageDal.Delete(result[i]);
                    
                }
            }
        }

        //business
        private IResult CheckImagesLimitExceeded(CarImagesUploaded carImagesUploaded)
        {
            if (carImagesUploaded.Images != null)//bunun sayesinde resim icermiyorsa assagidaki kontrollere girmiyor.
            {
                var carImagecount = _carImageDal.GetAll(p => p.CarId == carImagesUploaded.CarId).Count;
                carImagecount += carImagesUploaded.Images.Count;
                if (carImagecount > 5)
                {
                    return new ErrorResult("5 den fazla resim eklenemez");// messagesea ekle
                }

                return new SuccessResult();
            }
            return new SuccessResult();


        }



        //------------------------------------
        public IDataResult<FileStream> GetImageById(int id)
        {
            var carImage = _carImageDal.GetById(c => c.CarImageId == id);
            var formFile = File.OpenRead(carImage.ImagePath);
            var result = new SuccessDataResult<FileStream>(formFile);
            return result;
        }

        public IDataResult<List<Stream>> GetImagesById(int id)
        {
            var carImages = _carImageDal.GetAll(c => c.CarId == id);
            List<string> path = new List<string>();
            List<Stream> formFile = new List<Stream>();

            for (int i = 0; i < carImages.Count; i++)
            {
                path.Add(carImages[i].ImagePath);
                formFile.Add(File.OpenRead(path[i]));

            }

            return new SuccessDataResult<List<Stream>>(formFile);
           
        }



    }
}

//// YENIISINII YAZDIIM 
/*
        public IResult Add(CarImageUploadedApi carImageUploaded)
        {
            IResult result = BusinessRules.Run(CheckImageLimitExceeded(carImageUploaded.CarId));
            if (result != null)
            {
                return result;
            }

            var filePath = FileHelper.Add(carImageUploaded);
            if (!filePath.Success)//bussinesrulles methodunu olusturabilirsin 
            {
                return new ErrorResult(filePath.Message);
            }
            CarImage carImage = new CarImage {
                CarId = carImageUploaded.CarId,
                Date = DateTime.Now,
                ImagePath = filePath.Data// 
            };
            _carImageDal.Add(carImage);

            return new SuccessResult("Ok");
        }*/