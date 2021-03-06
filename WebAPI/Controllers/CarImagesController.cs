﻿using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        ICarImageService _carImageService;

        public CarImagesController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }
        [HttpPost("add")]
        public IActionResult Add([FromForm] CarImagesUploadedForCarImageFormFileListDto carImagesUploaded)
        {

            var result = _carImageService.Add(carImagesUploaded);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm] CarImageUploadedForSingleFormFileDto carImageUploaded)
        {

            var result = _carImageService.Update(carImageUploaded);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromForm] CarImageUploadedForSingleFormFileDto carImageUploaded)
        {

            var result = _carImageService.Delete(carImageUploaded);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

         [HttpGet("getbycarimageid")]
         public IActionResult GetByCarImageId(int id)
         {
            var result = _carImageService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
         }
        [HttpGet("getimagesbycarid")]
        public IActionResult GetCarImagesByCarId(int id)
        {
            var result = _carImageService.GetCarImagesByCarId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //----------------------------------------------------------------------------

        [HttpGet("getimage")]
        public IActionResult GetImage(int id)
        {
            var result = _carImageService.GetImageById(id);
            if (result.Success)
            {
                return Ok(result.Data); //result.Data seklinde yazinca calisiyor ama diger turlu timeout hatasini firlatiyor.
            }
            return BadRequest(result);
        }

        [HttpGet("getimages")]
        public IActionResult GetImages(int id)
        {
            var result = _carImageService.GetImagesById(id);
            if (result.Success)
            {
                return Ok(result.Data); 
            }
            return BadRequest(result);
        }




    }
}
