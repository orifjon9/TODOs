using System;
namespace TODOs.Api.Mappings.Profiles
{
    public class ListProfile: AutoMapper.Profile
    {
        public ListProfile()
        {
            CreateMap<Models.Requests.CreateUpdateTodoRequest, Data.Entities.Todo>();
            CreateMap<Data.Entities.Todo, Models.Responses.TodoViewModel>();
        }
    }
}

