using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumManage.Models
{
    public class Tag : BaseEntity
    {
       
        //public int TagId { get; set; }
        public string Name { get; set; }

        //Tag-Case M-M
        public List<TagCase> TagCases { get; set; }
    }
}
