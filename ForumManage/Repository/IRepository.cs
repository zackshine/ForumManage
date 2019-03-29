using ForumManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumManage.Repository
{
    public interface IRepository<T> where T : BaseEntity,ILogicInterface
    {
        List<T> GetAll();
        Task<T> GetById(long id);
        Task<T> Add(T entity);
        Task<T> Update(long id,T entity);
        Task Delete(long id);
    }
}
