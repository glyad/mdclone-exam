﻿using System.Threading;
using System.Threading.Tasks;

namespace MdClone.Model.Contracts
{
    public interface IEmailService
    {
        IEmailModel CreateNewEmail();

        Task SendEmail(IEmailModel emailModel, CancellationToken ct = default);
    }
}