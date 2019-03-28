using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumManage.Models
{
    public class Forum : BaseEntity, ILogicInterface
    {

       // public int ForumId { get; set; }
        public string Name { get; set; }

        public List<ForumEngineer> ForumEngineers { get; set; }//M-M
        public List<Case> Cases { get; set; }//O-M

        public bool IsDeleted { get; set; }

    }

    public class ForumEngineer
    {
        public long ForumId { get; set; }
        public Forum Forum { get; set; }

        public long EngineerId { get; set; }
        public Engineer Engineer { get; set; }
    }
}
