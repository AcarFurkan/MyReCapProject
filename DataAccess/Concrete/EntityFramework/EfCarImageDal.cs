using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarImageDal : EfEntityRepositoryBase<CarImage, DatabaseContext>, ICarImageDal
    {
        public List<CarImageDto> GetCarImageDetails(Expression<Func<CarImageDto, bool>> filter = null)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var result = from c in context.CarImages
                             join i in context.ImageDetails
                             on c.CarImageId equals i.CarImageId
                             select new CarImageDto
                             {
                                 CarImageId =c.CarImageId,
                                 CarId = c.CarId,
                                 ImageId = i.ImageId,
                                 ImagePath = i.ImagePath,


                             };


                return filter == null
                                   ? result.ToList()
                                   : result.Where(filter).ToList();
            }
        }
        /*
        public List<CarDetailDto> GetCarDetails()
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
        }*/
    }
}
