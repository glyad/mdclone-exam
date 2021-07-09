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

        public IRowDataModel MapToModel(RowDataDto dto) => _mapper.Map<IRowDataModel>(dto);

        public RowDataDto MapToDto(IRowDataModel model) => _mapper.Map<RowDataDto>(model);
    }
}