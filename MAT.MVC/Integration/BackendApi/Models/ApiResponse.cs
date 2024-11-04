using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Integration.BackendApi.Models
{
    public class ApiResponse<T>: ErrorDto
    {
        public bool Ok { get; set; }
        public T Data { get; set; }
        public int StatusCode { get; set; }
    }
}