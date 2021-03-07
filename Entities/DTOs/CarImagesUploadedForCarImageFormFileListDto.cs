using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class CarImagesUploadedForCarImageFormFileListDto//bu arkadas toplu resim ekleme islemleri icin(teklidede buna gectim) bunu kullaniyorum/
                                                 // carimageuploadedapi sinifi niye var diye sorarsaniz IformFilenin list olmayan seklini oraya yazdim.  
                                                 //eger oyle olmazsa delete update methodlari bizi is sinifta uzuyor.
                                                 // ama bu sayede iki yerden tum methodlarin icini kontrol edebiliyorum.
                                                 //arti olarak toplu sekilde resim ekleyebiliyorum.
    {
        public List<IFormFile>? Images { get; set; }// bunla default resimlerini duzenle//get i null a gore ayarlasak nolur
        public int CarId { get; set; }
        public int CarImageId { get; set; }//= new List<IFormFile>();
    }
}
