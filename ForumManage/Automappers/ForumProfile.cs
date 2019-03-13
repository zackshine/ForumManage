using AutoMapper;
using ForumManage.Models;
using ForumManage.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumManage.Automappers
{
    public class ForumProfile : Profile
    {
        public ForumProfile()
        {
            CreateMap<Forum, ForumVM>().ReverseMap();
        }
    }
}
