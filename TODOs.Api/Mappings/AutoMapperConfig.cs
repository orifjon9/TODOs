using System;
using TODOs.Api.Mappings.Profiles;

namespace TODOs.Api.Mappings
{
    public class AutoMapperConfig
    {
        public static AutoMapper.IMapper Create(IServiceProvider provider)
        {
            var config = new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile<ListProfile>();
                options.AddProfile<TodoProfile>();
            });

            return config.CreateMapper();
        }
    }
}

