using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            //_rentalDal.Delete(rental);
            var result = _rentalDal.GetById(r => r.CarId == rental.CarId && r.ReturnDate == null);// burda sikinti kullanici returndatei bozuk ogonderebilir.

            //if (result != null)
            //{
            //    return new ErrorResult(Messages.GeneralError);//arac kirada yazdir
            //}
           
            rental.RentDate = DateTime.Now;//bu da kullanicin yanlis sagati girerse diye(kullanici benim ama olsun aklima geldi oyle koydum)
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);
        }
        public IResult Update(Rental rental)
        {
            var result = _rentalDal.GetById(r => r.CarId == rental.CarId && r.ReturnDate == null);
            if (result != null)
            {
                result.ReturnDate = DateTime.Now;
                _rentalDal.Update(result);
                return new SuccessResult(Messages.ReturnTimeAdded);
            }
            return new ErrorResult(Messages.GeneralError);// arac zaten teslim edildi yaz

        }
        public IResult Delete(Rental rental)
        {
            var result = _rentalDal.GetById(r => r.CarId == rental.CarId && r.ReturnDate != null);
            if (result != null)
            {
                _rentalDal.Delete(result);
                return new SuccessResult(Messages.RentalDeleted);

            }
            return new ErrorResult(Messages.GeneralError); // TESLIM OLMAYAN ARACLAR SILINEMEZ DIYE EKLE

        }
        

        public IDataResult<List<Rental>> GetAll()
        {
            if (true)// denemek icin
            {
                return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
            }
            return new ErrorDataResult<List<Rental>>(Messages.GeneralError);
        }

        public IDataResult<Rental> GetById(int Id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.GetById(r => r.RentalId == Id));
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }


       

        public IDataResult<List<RentalDetailDto>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<RentalDetailDto>>
                (_rentalDal.GetRentalDetails(r => r.DailyPrice >= min && r.DailyPrice <= max)
                .OrderBy(r=>r.DailyPrice).ToList());
        }
    }
}

/*
 public IResult Rent(int carId, int customerId)
        {
            Rental result = _rentalDal.GetById(r => r.CarId == carId && r.ReturnDate == null);
            if (result.ReturnDate == null)
            {
                new ErrorResult(Messages.GeneralError);
            }
            _rentalDal.Add(new Rental
            {
                CarId = carId,
                CustomerId = customerId,
                RentDate = DateTime.Now,
                ReturnDate = null
            });
            return new SuccessResult(Messages.RentalAdded);
        }

        //update yerine denemek icin yazdim.
        public IResult Deliver(int rentalId)
        {
            var result = _rentalDal.GetById(r => r.RentalId == rentalId && r.ReturnDate == null);
            if (result.ReturnDate == null)
            {
                result.ReturnDate = DateTime.Now;
                _rentalDal.Update(result);
                return new SuccessResult(Messages.ReturnTimeAdded);
            }
            return new ErrorResult(Messages.GeneralError);
        }
 */