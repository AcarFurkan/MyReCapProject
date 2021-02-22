using Core.Utilities.Result;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IColorService
    {
        IDataResult<CarColor> GetById(int Id);
        IDataResult<List<CarColor>> GetAll();
        IResult Add(CarColor carColor);
        IResult Update(CarColor carColor);
        IResult Delete(CarColor carColor);

    }
}
