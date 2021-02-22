using Core.Utilities.Result;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<Rental> GetById(int Id);
        IDataResult<List<Rental>> GetAll();
        IResult Update(Rental rental);
        //IResult Deliver(int rentalId);
        //IResult Rent(int carId, int customerId);
      
        IResult Add(Rental rental);
        IResult Delete(Rental rental);
        IDataResult<List<RentalDetailDto>> GetRentalDetails();
        //*********//UST TEKI REISIN PARAMETRESINDEKI OLAYI TEKRAR IZLE TAM HATILAMIYORUM.//******************//
        //IDataResult<List<RentalDetailDto>> GetRentalDetails();
        IDataResult<List<RentalDetailDto>> GetByUnitPrice(decimal min, decimal max);
    }
}
