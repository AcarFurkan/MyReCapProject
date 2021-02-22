﻿using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {

            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        public IResult Delete(Car car)
        {
            if (DateTime.Now.Hour == 20)
            {
                return new ErrorResult();
            }
           
            _carDal.Delete(car);
            return new SuccessResult();
            
        }

        public IDataResult<List<Car>> GetAll()
        {
            //_carDal.GetAll()

            if (DateTime.Now.Hour == 20)
            {
                return new ErrorDataResult<List<Car>>(Messages.CarNameInvalid);
            }

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(),Messages.CarsListed);
        }

        public IDataResult<Car> GetById(int Id)
        {
            //_carDal.GetById(c=>c.Id==Id)
           
            if (DateTime.Now.Hour == 20)
            {
                return new ErrorDataResult<Car>(Messages.CarNameInvalid);
            }
            Console.WriteLine(Id);
            var result = new SuccessDataResult<Car>(_carDal.GetById(c => c.CarId == Id));
            return result;
        }

        public IDataResult<List<Car>> GetByUnitPrice(decimal min, decimal max)
        {
           

            return new SuccessDataResult<List<Car>>
                (_carDal.GetAll(c => c.DailyPrice >= min && c.DailyPrice <= max)
                .OrderBy(c => c.DailyPrice).ToList());//KONTROL ET
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            // return _carDal.GetCarDetails();
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int Id)
        {
            //return _carDal.GetAll(c => c.BrandId == Id);
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == Id));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int Id)
        {
            //return _carDal.GetAll(c => c.ColorId == Id);
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == Id));
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
           
            _carDal.Update(car);
            return new Result(true);

        }
    }
}
