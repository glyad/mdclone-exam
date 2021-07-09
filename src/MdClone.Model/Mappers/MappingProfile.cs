using System;
using AutoMapper;
using MdClone.Data.Contracts.Dto;
using MdClone.Model.Contracts;

namespace MdClone.Model.Mappers
{
    internal sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateDomainObjectMap<EmailModelDto, IEmailModel, EmailModel>();
            CreateDomainObjectMap<FileModelDto, IFileModel, FileModel>();
            CreateDomainObjectMap<RowDataModelDto, IRowDataModel, RowDataModel>();
            CreateDomainObjectMap<TableDataModel, ITableDataModel, TableDataModel>();
        }

        private void CreateDomainObjectMap<TDto, TContract, TModel>()
            where TModel : TContract
            where TContract : class => CreateDomainObjectMap(typeof(TDto), typeof(TContract), typeof(TModel));

        private void CreateDomainObjectMap(Type dtoType, Type contractType, Type modelType)
        {
            CreateMap(dtoType, contractType).As(modelType);
            CreateMap(dtoType, modelType);
            CreateMap(contractType, dtoType);
            CreateMap(modelType, dtoType);
        }
    }
}