using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(CarColor carColor)
        {
            _colorDal.Add(carColor);
            return new SuccessResult(Messages.ColarAdded);
        }

        public IResult Delete(CarColor carColor)
        {
            _colorDal.Delete(carColor);
            return new SuccessResult();
        }

        public IDataResult<List<CarColor>> GetAll()
        {
            return new SuccessDataResult<List<CarColor>>(_colorDal.GetAll());
        }

        public IDataResult<CarColor> GetById(int Id)
        {
            return new SuccessDataResult<CarColor>(_colorDal.GetById(c=>c.ColorId==Id));
        }

        [ValidationAspect(typeof(ColorValidator))]
        public IResult Update(CarColor carColor)
        {
            _colorDal.Update(carColor);
            return new SuccessResult();
        }
    }
}
