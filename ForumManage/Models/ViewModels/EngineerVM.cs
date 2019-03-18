using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumManage.Models.ViewModels
{
    public class EngineerVM
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        //public byte[] Photo { get; set; }
    }
}
