using AutoMapper;
using JetBrains.Annotations;
using MdClone.Data.Contracts.Dto;
using MdClone.Model.Contracts;

namespace MdClone.Model.Mappers
{
    [UsedImplicitly]
    public class EmailRecipientModelMapper
    {
        private readonly IMapper _mapper;

        public EmailRecipientModelMapper(IMapper mapper) => _mapper = mapper;

        public IEmailRecipientModel MapToModel(EmailRecipientDto dto) => _mapper.Map<IEmailRecipientModel>(dto);

        public EmailRecipientDto MapToDto(IEmailRecipientModel model) => _mapper.Map<EmailRecipientDto>(model);
    }
}