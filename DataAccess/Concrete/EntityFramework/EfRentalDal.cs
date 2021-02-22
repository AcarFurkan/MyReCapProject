using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, DatabaseContext>, IRentalDal
    {
      
        
        public List<RentalDetailDto> GetRentalDetails(Expression<Func<RentalDetailDto, bool>> filter = null)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var result = from r in context.Rentals
                             join c in context.Cars
                             on r.CarId equals c.CarId
                             join cu in context.Customers
                             on r.CustomerId equals cu.CustomerId
                             join b in context.Brands
                             on c.BrandId equals b.BrandId

                             select new RentalDetailDto
                             {
                                 RentalDetailId = r.RentalId,
                                 BrandName = b.BrandName,
                                 CustomerName = cu.CompanyName,
                                 DailyPrice = c.DailyPrice,
                                 RentDate = r.RentDate,
                                 ReturnDate = r.ReturnDate
                             };

                //return result.ToList();
                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();


            }
        }
    }
}
/*
using (DatabaseContext context = new DatabaseContext())
{
    var result = from c in context.Cars
                 join cc in context.Colors
                 on c.BrandId equals cc.ColorId
                 join b in context.Brands
                 on c.BrandId equals b.BrandId
                 select new CarDetailDto { BrandName = b.BrandName, ColorName = cc.ColorName, UnitPrice = c.DailyPrice };
    return result.ToList();
}
*/