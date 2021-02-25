using Business.Abstract;
using Business.Constants;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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


        //[ValidationAspect(typeof(CarValidator))]  // 5 den fazla resim olamaz
        public IResult Add(CarImage carImage)
        {
            _carImageDal.Add(carImage);
            return new SuccessResult();
        }

        public IResult Delete(CarImage carImage)
        {

            _carImageDal.Delete(carImage);
            return new SuccessResult();

        }

        

        public IDataResult<CarImage> GetById(int Id)
        {
            var result = new SuccessDataResult<CarImage>(_carImageDal.GetById(c => c.CarId == Id));
            return result;
        }

        public IDataResult<List<FileStream>> GetCarImageDetailById(int id)
        {
            //var result = new SuccessDataResult<List<CarImageDto>>(_carImageDal.GetCarImageDetails(c => c.CarImageId == id));
            var result = _carImageDal.GetCarImageDetails(c => c.CarImageId == id);
            List<string> path = new List<string>(result.Count);
            List<FileStream> formFile = new List<FileStream>(result.Count);

            for (int i = 0; i < result.Count; i++)
            {
                path.Add(result[i].ImagePath);
                formFile.Add( File.OpenRead(path[i]));
               
            }
           

           
            return new SuccessDataResult<List<FileStream>>(formFile);

        }

        // [ValidationAspect(typeof(CarValidator))]
        public IResult Update(CarImage carImage)
        {
            _carImageDal.Update(carImage);
            return new SuccessResult();

        }
    }
}
