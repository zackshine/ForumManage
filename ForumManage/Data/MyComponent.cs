using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumManage.Data
{
    public class MyComponent : IMyComponent
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public MyComponent(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetDataFromSession()
        {

            _contextAccessor.HttpContext.Session.SetString("Key", "value");
            return _contextAccessor.HttpContext.Session.GetString("Key");
        }
    }

    public interface IMyComponent
    {
        string GetDataFromSession();
    }
}
