using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Integration.BackendApi.Models
{
    public class ImageResponse
    {
        public int Id { get; set; }
        public string PublicId { get; set; }
        public string FileName { get; set; }
        public int Size { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}