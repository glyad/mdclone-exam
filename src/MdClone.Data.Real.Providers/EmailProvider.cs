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
    }
}