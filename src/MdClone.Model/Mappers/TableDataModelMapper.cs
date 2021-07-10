using System.Linq;
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

        public ITableDataModel MapToModel(TableDataDto dto)
        {
            //TODO: revert auto mapper usage
            // return _mapper.Map<ITableDataModel>(dto);

            var result = new TableDataModel
            {
                Header = dto.Header,
                Rows = dto.Rows.Select(r => _mapper.Map<IRowDataModel>(r)).ToArray()
            };

            return result;
        }

        public TableDataDto MapToDto(ITableDataModel model) => _mapper.Map<TableDataDto>(model);
    }
}