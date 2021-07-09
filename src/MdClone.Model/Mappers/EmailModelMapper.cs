using AutoMapper;
using JetBrains.Annotations;
using MdClone.Data.Contracts.Dto;
using MdClone.Model.Contracts;

namespace MdClone.Model.Mappers
{
    [UsedImplicitly]
    public class EmailModelMapper
    {
        private readonly IMapper _mapper;

        public EmailModelMapper(IMapper mapper) => _mapper = mapper;

        public IEmailModel MapToModel(EmailDto dto) => _mapper.Map<IEmailModel>(dto);

        public EmailDto MapToDto(IEmailModel model) => _mapper.Map<EmailDto>(model);
    }
}