using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Dtos;
using Ecommerce.Entities;

namespace Ecommerce.MappingProfiles;
public class FilesProfile : Profile
{
    public FilesProfile()
    {
        CreateMap<FormFile, FileModel>()
            .ForMember(f => f.Type, option => option.MapFrom(f => f.ContentType))
            .ForMember(f => f.Size, option => option.MapFrom(f => f.Length))
            .ForMember(f => f.Name, option => option.MapFrom(f => f.FileName));

        CreateMap<FileModel, FileModelDto>();
        CreateMap<UpdateFileModelDto, FileModel>();
    }

}