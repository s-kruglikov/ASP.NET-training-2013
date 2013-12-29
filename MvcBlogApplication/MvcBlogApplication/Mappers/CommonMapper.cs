using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MvcBlog.WebUI.Concrete;
using MvcBlog.WebUI.Models;

namespace MvcBlog.WebUI.Mappers
{
    public class CommonMapper : IMapper
    {
        static CommonMapper()
        {
            Mapper.CreateMap<ConfigService, SiteConfigViewModel>();
            Mapper.CreateMap<SiteConfigViewModel, ConfigService>();
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return Mapper.Map(source, sourceType, destinationType);
        }
    }
}