using AutoMapper;
using JetBrains.Annotations;
using MdClone.Data.Contracts.Dto;
using MdClone.Model.Contracts;

namespace MdClone.Model.Mappers
{
    [UsedImplicitly]
    public class TableDataModelMapper
    {
        private readonly IMapper _mapper;

        public TableDataModelMapper(IMapper mapper) => _mapper = mapper;

        public ITableDataModel MapToModel(TableDataModelDto dto) => _mapper.Map<ITableDataModel>(dto);

        public TableDataModelDto MapToDto(ITableDataModel model) => _mapper.Map<TableDataModelDto>(model);
    }
}