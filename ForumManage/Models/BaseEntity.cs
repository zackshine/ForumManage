using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumManage.Models
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        //public bool IsDeleted { get; set; }
       
    }
     public interface ILogicInterface
    {
        bool IsDeleted { get; set; }
        
    }
}
