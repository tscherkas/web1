using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ImagesModel
    {
        public List<Uri> ImagesURIs { get; }
        public ImagesModel()
        {
            ImagesURIs = new List<Uri>();
        }

    }
}