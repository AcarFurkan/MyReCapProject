using Core.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class ImageDetail: IEntity
    {
        [Key]
        public int ImageId { get; set; }
        public int CarImageId { get; set; }
        public string ImagePath { get; set; }
    }
}
