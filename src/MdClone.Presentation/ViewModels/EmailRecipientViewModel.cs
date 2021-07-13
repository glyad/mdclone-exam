﻿using LogoFX.Client.Mvvm.ViewModel;
using MdClone.Model.Contracts;

namespace MdClone.Presentation.ViewModels
{
    public class EmailRecipientViewModel : ObjectViewModel<IEmailRecipientModel>
    {
        public EmailRecipientViewModel(IEmailRecipientModel model)
            : base(model)
        {

        }

        public override string ToString()
        {
            return Model.Address;
        }
    }
}