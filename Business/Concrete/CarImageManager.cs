using Business.Abstract;
using Business.Constants;
using Business.Helpers;
using Core.Utilities.Business;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }
        
        public IResult Add(CarImagesUploaded carImagesUploaded)
        {
            IResult result = BusinessRules.Run(CheckImagesLimitExceeded(carImagesUploaded.CarId,
                carImagesUploaded.Images.Count));
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
            }



            return new SuccessResult("Ok");
        }


        public IResult Delete(CarImageUploadedApi carImageUploaded)
        {
            var carImage = _carImageDal.GetById(c => c.CarImageId == carImageUploaded.CarImageId);// business kontrolunu ekle 
            FileHelper.Delete(carImage.ImagePath);
            //File.Delete(carImage.ImagePath); bunu burda yaparsam methoda parametre gonddermekten kurtulurum bujnu da bir dusun
            _carImageDal.Delete(carImage);
            return new SuccessResult("Ok");
        }

        public IDataResult<CarImage> GetById(int Id)
        {
            var result = _carImageDal.GetById(c =>c.CarImageId == Id);
            return new SuccessDataResult<CarImage>(result);
        }

        public IDataResult<CarTrial> GetCarImageDetailById(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<CarImage>> GetCarImagesByCarId(int Id)
        {
            var result = _carImageDal.GetAll(c=>c.CarId==Id);
            return new SuccessDataResult<List<CarImage>>(result);
        }

        public IResult Update(CarImageUploadedApi carImageUploaded)
        {
            var carImage = _carImageDal.GetById(c => c.CarImageId == carImageUploaded.CarImageId);// business kontrolunu ekle 
            //File.Delete(carImage.ImagePath); bunu burda yaparsam methoda parametre gonddermekten kurtulurum bujnu da bir dusun
            var filePath = FileHelper.Update(carImageUploaded,carImage.ImagePath);
            if (!filePath.Success)//bussinesrulles methodunu olustur
            {
                return new ErrorResult(filePath.Message);
            }
           
            carImage.Date = DateTime.Now;
            carImage.ImagePath = filePath.Data;
            _carImageDal.Update(carImage);

            return new SuccessResult("Ok");
        }
        
        private IResult CheckImagesLimitExceeded(int carid, int numberOfIncomingImages)
        {
            var carImagecount = _carImageDal.GetAll(p => p.CarId == carid).Count;
            carImagecount += numberOfIncomingImages;
            if (carImagecount > 5)
            {
                return new ErrorResult("5 den fazla resim eklenemez");// messagesea ekle
            }

            return new SuccessResult();
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