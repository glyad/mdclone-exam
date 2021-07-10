﻿using Caliburn.Micro;
using JetBrains.Annotations;

namespace MdClone.Presentation.ViewModels
{
    [UsedImplicitly]
    public class EmailScreenViewModel : Conductor<EmailViewModel>
    {
        public override string DisplayName
        {
            get => "E-Mail";
            set { }
        }
    }
}