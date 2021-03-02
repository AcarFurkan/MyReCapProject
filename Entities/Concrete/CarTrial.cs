using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Entities.Concrete
{
    public class CarTrial
    {
        public List<FileStream> ListFileStream = new List<FileStream>();
        public int CarId { get; set; }
    }
}
