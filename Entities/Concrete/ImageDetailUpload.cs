using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Concrete

{
    public class ImageDetailUpload
    {
        public IFormFile Images { get; set; }
        public int GelenResimId { get; set; }
        public int CarImageId { get; set; }

    }
}
