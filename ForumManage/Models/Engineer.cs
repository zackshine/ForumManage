using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumManage.Models
{
    public class Engineer : BaseEntity
    {
        // public int EngineerId { get; set; }
        public string Name { get; set; }
        public byte[] Photo { get; set; }
        public DateTime CreateTime { get; set; }

        public List<ForumEngineer> ForumEngineers { get; set; }
        public List<Case> Cases { get; set; }
    }
}
