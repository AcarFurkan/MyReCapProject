using Core.Utilities.Result;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Business.Abstract
{
    public interface ICarImageService
    {
        IDataResult<CarImage> GetById(int Id);
        //IDataResult<List<CarImage>> GetAll();
        IDataResult<List<CarImage>> GetCarImagesByCarId(int Id);
        IDataResult<CarTrial> GetCarImageDetailById(int id);
        IResult Add(CarImagesUploaded carImagesUploaded);
        //IResult AddImages(CarImagesUploaded carImagesUploaded);
        IResult Update(CarImageUploadedApi carImageUploaded);
        IResult Delete(CarImageUploadedApi carImageUploaded);
    }
}
