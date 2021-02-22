using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, DatabaseContext>, ICarDal
    {
        
        public List<CarDetailDto> GetCarDetails()
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var result = from c in context.Cars
                             join co in context.Colors
                             on c.ColorId equals co.ColorId
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             select new CarDetailDto {
                                 BrandName = b.BrandName,
                                 ColorName = co.ColorName,
                                 UnitPrice = c.DailyPrice,
                                 Id = c.CarId,
                                 ColorId = c.ColorId,
                                 BrandId = c.BrandId
                             };
                return result.ToList();
            }
        }
    }
}
