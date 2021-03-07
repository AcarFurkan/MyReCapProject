using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Helpers;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performence;
using Core.Aspects.Autofac.Transaction;
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
        
        [PerformanceAspect(5)] 
        [SecuredOperation("carimage.add")]// kontrollerden sonra ac
        [CacheRemoveAspect("ICarImageService.Get")]  
        [TransactionScopeAspect]    // chech images limitexceeded methodunun icinde default image i sildiginde bir hata cikarsa transactionscopeaspect sayesinde bu onu yakaliyor.  
        public IResult Add(CarImagesUploadedForCarImageFormFileListDto carImagesUploaded)
        {   //CheckForNullStatus  bunu RUN dan ayirirsan veritabanina iki kere gitmekten kurtulursun. 
            IResult result = BusinessRules.Run(CheckImagesLimitExceeded(carImagesUploaded), CheckForNullStatus(carImagesUploaded));
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

        [CacheRemoveAspect("ICarImageService.Get")]
        //[SecuredOperation("carimage.add")]
        public IResult Delete(CarImageUploadedForSingleFormFileDto carImageUploaded)
        {
            var carImage = _carImageDal.GetById(c => c.CarImageId == carImageUploaded.CarImageId);// business kontrolunu ekle 
            if (carImage.ImagePath == null)
            {
                _carImageDal.Delete(carImage);
                return new SuccessResult();
            }
            FileHelper.Delete(carImage.ImagePath);
            //File.Delete(carImage.ImagePath); bunu burda yaparsam methoda parametre gonddermekten kurtulurum bujnu da bir dusun
            _carImageDal.Delete(carImage);
            return new SuccessResult("Ok");
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        //[SecuredOperation("carimage.add")] 
        public IResult Update(CarImageUploadedForSingleFormFileDto carImageUploaded)
        {
            var carImage = _carImageDal.GetById(c => c.CarImageId == carImageUploaded.CarImageId);
            if (carImage.ImagePath == null)
            {
                new ErrorResult(" guncellestirme islemi icin resim yuklemeneiz gerekmektedir");
            }
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

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<CarImage> GetById(int carImageId)
        {
            IResult result = BusinessRules.Run(CheckIfImageIsNull(carImageId));
            var carImage = _carImageDal.GetById(c => c.CarImageId == carImageId);
            if (result != null)
            {
                carImage.ImagePath = defaultPath;
                return new SuccessDataResult<CarImage>(carImage);
            }

            return new SuccessDataResult<CarImage>(carImage);
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetCarImagesByCarId(int carId)
        {
            var carImages = _carImageDal.GetAll(c => c.CarId == carId);
            return new SuccessDataResult<List<CarImage>>(carImages);
        }

        //--business
        private IResult CheckIfImageIsNull(int id)
        {
            var result = _carImageDal.GetById(c => c.CarImageId == id);
            if (result.ImagePath == null)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

        private IResult CheckImagesLimitExceeded(CarImagesUploadedForCarImageFormFileListDto carImagesUploaded)// bu method uzarsa todlari bol
        {
            int uploadedImageCount = carImagesUploaded.Images == null ? 0 : carImagesUploaded.Images.Count;
            if (uploadedImageCount > 5)// bunu yazdim ki database den veri cekmedende kontrol yapilip error verilsin.
                return new ErrorResult("5 den fazla resim eklenemez");// messagesea ekle

            var dbcarImagecount = _carImageDal.GetAll(c => c.CarId == carImagesUploaded.CarId).Count; // count hello helllo 
            int totalImageCount = dbcarImagecount + uploadedImageCount;
            if (totalImageCount > 5)
                return new ErrorResult("5 den fazla resim eklenemez");// messagesea ekle

            return new SuccessResult();
        }

        private IResult CheckForNullStatus(CarImagesUploadedForCarImageFormFileListDto carImagesUploaded)
        {
            var tempCarImage = _carImageDal.GetAll(c => c.CarId == carImagesUploaded.CarId);

            for (int i = 0; i < tempCarImage.Count; i++)
            {
                if (tempCarImage[i].ImagePath == null && carImagesUploaded.Images == null)
                    return new ErrorResult(" default bir resim eklendi tekrar default bir resim eklenemez.");

                if (tempCarImage[i].ImagePath == null && carImagesUploaded.Images != null)
                    _carImageDal.Delete(tempCarImage[i]);//  ekleme durumunda bir hata olusursa default resmi bosu bosuna kaldirmis omayalim diye TransactionScopeAspect islemleri geri aliyor.

                if (tempCarImage[i].ImagePath != null && carImagesUploaded.Images == null)
                    return new ErrorResult("zaten resimli car image ogeleri bulunmaktadir resimsiz ekleme islemi yapilamaz.");
            }
            return new SuccessResult();
        }

        //------------------------------------alttaki kodlar gercek resim islemleri icin
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

