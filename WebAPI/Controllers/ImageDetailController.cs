using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageDetailController : ControllerBase
    {
        IImageDetailService _imageDetailService;

        public ImageDetailController(IImageDetailService imageDetailService)
        {
            _imageDetailService = imageDetailService;
        }


        [HttpGet("getbyid")]
        public IActionResult GetById(int imageId)
        {
            var result = _imageDetailService.GetById(imageId);


            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result);
        }

        [HttpPost("Add")]
        public IActionResult Add([FromForm] ImageDetailUpload imageDetailUpload)
        {


            if (imageDetailUpload.Images.Length > 0)
            {
                var result = _imageDetailService.Add(imageDetailUpload);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("delete")]
        public IActionResult Delete(int imageId)
        {
            var result = _imageDetailService.Delete(imageId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("Update")]
        public IActionResult Update([FromForm] ImageDetailUpload imageDetailUpload)
        {
            if (imageDetailUpload.Images.Length > 0)
            {
                var result = _imageDetailService.Update(imageDetailUpload);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            else
            {
                return BadRequest();
            }

            
        }







    }
}
