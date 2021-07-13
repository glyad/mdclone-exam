using MdClone.Data.Contracts.Dto;

namespace MdClone.Data.Contracts.Providers
{
    public interface IEmailProvider
    {
        void SendEmail(EmailDto emailDto);

        EmailRecipientDto[] GetRecipients();
    }
}