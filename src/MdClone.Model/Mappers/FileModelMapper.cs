using AutoMapper;
using JetBrains.Annotations;
using MdClone.Data.Contracts.Dto;
using MdClone.Model.Contracts;

namespace MdClone.Model.Mappers
{
    [UsedImplicitly]
    public class FileModelMapper
    {
        private readonly IMapper _mapper;

        public FileModelMapper(IMapper mapper) => _mapper = mapper;

        public IFileModel MapToModel(FileModelDto dto) => _mapper.Map<IFileModel>(dto);

        public FileModelDto MapToDto(IFileModel model) => _mapper.Map<FileModelDto>(model);
    }
}