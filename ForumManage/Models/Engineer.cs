using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumManage.Models
{
    public class Engineer : BaseEntity, ILogicInterface
    {
        public string Name { get; set; }
        //public byte[] Photo { get; set; }
        public string ImageName { get; set; }

        public List<ForumEngineer> ForumEngineers { get; set; }
        public List<Case> Cases { get; set; }

        public bool IsDeleted { get; set; }
    }
}
