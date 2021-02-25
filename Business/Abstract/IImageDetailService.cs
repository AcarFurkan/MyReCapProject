using Core.Utilities.Result;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Business.Abstract
{
    public interface IImageDetailService
    {
        IDataResult<FileStream> GetById(int Id);
        IResult Add(ImageDetailUpload imageDetailUpload);
        IResult Update(ImageDetailUpload imageDetailUpload);
        IResult Delete(int imageId);
    }
}
