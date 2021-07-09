using AutoMapper;
using JetBrains.Annotations;
using MdClone.Data.Contracts.Dto;
using MdClone.Model.Contracts;

namespace MdClone.Model.Mappers
{
    [UsedImplicitly]
    public class RowDataModelMapper
    {
        private readonly IMapper _mapper;

        public RowDataModelMapper(IMapper mapper) => _mapper = mapper;

        public IRowDataModel MapToModel(RowDataModelDto dto) => _mapper.Map<IRowDataModel>(dto);

        public RowDataModelDto MapToDto(IRowDataModel model) => _mapper.Map<RowDataModelDto>(model);
    }
}