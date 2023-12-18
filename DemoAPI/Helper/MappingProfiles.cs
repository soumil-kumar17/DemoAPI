using AutoMapper;
using DemoAPI.DTO;
using DemoAPI.Models;

namespace DemoAPI.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Student, CreateStudentDto>();
        CreateMap<CreateStudentDto, Student>();
        CreateMap<Branch, CreateBranchDto>();
        CreateMap<CreateBranchDto, Branch>(); 
        CreateMap<Elective, CreateElectiveDto>();
        CreateMap<CreateElectiveDto, Elective>();
    }
}
