
using AutoMapper;
using WarehouseTraineeTask.Application.DTOs.RequestDTO;
using WarehouseTraineeTask.Application.DTOs.ResponseDTO;
using WarehouseTraineeTask.Domain.Entity;

namespace WarehouseTraineeTask.Application
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Department, DepartmentRequestDTO>().ReverseMap();
            CreateMap<Department, DepartmentResponseDTO>().ReverseMap();

            CreateMap<Product, ProductRequestDTO>().ReverseMap();
            CreateMap<Product, ProductResponseDTO>().ReverseMap();

            CreateMap<User, UserRequestDTO>().ReverseMap()
                .ForMember(dest=> dest.PasswordHash, opt=> opt.MapFrom(src=>src.Password));
            CreateMap<User, UserResponseDTO>().ReverseMap();

            CreateMap<Worker, WorkerRequestDTO>().ReverseMap();
            CreateMap<Worker, WorkerResponseDTO>().ReverseMap();
        }
    }
}