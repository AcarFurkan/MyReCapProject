using Core.Utilities.Result;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
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
        
        IResult Add(CarImagesUploadedForCarImageFormFileListDto carImagesUploaded);
        //IResult AddImages(CarImagesUploaded carImagesUploaded);
        IResult Update(CarImageUploadedForSingleFormFileDto carImageUploaded);
        IResult Delete(CarImageUploadedForSingleFormFileDto carImageUploaded);
        IDataResult<FileStream> GetImageById(int id);
        IDataResult<List<Stream>> GetImagesById(int id);
    }
}
