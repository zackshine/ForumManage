using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumManage.Models
{
    public class Case : BaseEntity
    {
       
        public string Title { get; set; }
        public string Link { get; set; }


        //Forum-Case One-Many
        public long ForumId { get; set; }
        public Forum Forum { get; set; }

        //Engineer-Case One-Many
        public long EngineerId { get; set; }
        public Engineer Engineer { get; set; }

        //Case-Tag M-M
        public List<TagCase> TagCases { get; set; }
    }

    public class TagCase
    {
        public long CaseId { get; set; }
        public Case Case { get; set; }
        public long TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
