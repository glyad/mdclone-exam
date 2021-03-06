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
            CreateTableMaps();
            CreateEMailMaps();
        }

        private void CreateTableMaps()
        {
            CreateDomainObjectMap<FileDto, IFileModel, FileModel>();
            CreateDomainObjectMap<ItemDataDto, IItemDataModel, ItemDataModel>();
            CreateDomainObjectMap<RowDataDto, IRowDataModel, RowDataModel>();
            CreateDomainObjectMap<TableDataDto, ITableDataModel, TableDataModel>();
        }

        private void CreateEMailMaps()
        {
            CreateDomainObjectMap<EmailRecipientDto, IEmailRecipientModel, EmailRecipientModel>();
            CreateDomainObjectMap<EmailRecipientsDto, IEmailRecipientsModel, EmailRecipientsModel>();
            CreateDomainObjectMap<EmailDto, IEmailModel, EmailModel>();
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