﻿using Core.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class CarImage :IEntity
    {
        public int CarImageId { get; set; }
        public int CarId { get; set; }
        public int ImagePathId { get; set; }
        public DateTime ?Date { get; set; }
    }
}
