using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class CarImageUploadedApi// bu arkadas update ve delete icin kullaniyorum formfilenin list olmasi add de avantajken delete ve updatede sikinti yaratayior.
    {
        public IFormFile Images { get; set; }
        public int CarId { get; set; }
        public int CarImageId { get; set; }


    }
}
