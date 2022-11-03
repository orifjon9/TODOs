using System;
namespace TODOs.Api.Mappings.Profiles
{
    public class TodoProfile: AutoMapper.Profile
    {
        public TodoProfile()
        {
            CreateMap<Models.Requests.CreateListRequest, Data.Entities.List>();
            CreateMap<Data.Entities.List, Models.Responses.ListViewModel>();
        }
    }
}

