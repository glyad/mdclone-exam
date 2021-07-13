using System.Threading;
using JetBrains.Annotations;
using MdClone.Data.Contracts.Dto;
using MdClone.Data.Contracts.Providers;

namespace MdClone.Data.Real.Providers
{
    [UsedImplicitly]
    internal sealed class EmailProvider : IEmailProvider
    {
        public void SendEmail(EmailDto emailDto)
        {
            Thread.Sleep(500);
        }

        public EmailRecipientDto[] GetRecipients()
        {
            Thread.Sleep(1000);

            return new[]
            {
                new EmailRecipientDto{Address = "pmosciski@yahoo.com"},
                new EmailRecipientDto{Address = "smith.nakia@yahoo.com"},
                new EmailRecipientDto{Address = "fmann@yahoo.com"},
                new EmailRecipientDto{Address = "claudie.willms@ferry.net"},
                new EmailRecipientDto{Address = "hjakubowski@hotmail.com"},
                new EmailRecipientDto{Address = "rupert.boehm@johnson.com"},
                new EmailRecipientDto{Address = "zroob@hotmail.com"},
                new EmailRecipientDto{Address = "heloise60@gmail.com"},
                new EmailRecipientDto{Address = "schultz.maud@feeney.biz"},
                new EmailRecipientDto{Address = "chet.upton@yahoo.com"}
            };
        }
    }
}