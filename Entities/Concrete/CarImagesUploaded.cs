using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class CarImagesUploaded
    {
        public List<IFormFile> Images { get; set; }
        public int CarId { get; set; }
        public int CarImageId { get; set; }
    }
}
