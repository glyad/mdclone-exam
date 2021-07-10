using System;
using System.IO;
using System.Windows;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel.Services;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace MdClone.Presentation.Shared.UIServices
{
    [UsedImplicitly]
    public sealed class BrowseFolderService : IBrowseFolderService
    {
        public bool DetermineFolder()
        {
            var dialog = new CommonOpenFileDialog
            {
                Title = Description,
                InitialDirectory = SelectedPath?.FullName,
                IsFolderPicker = true
            };

            if (dialog.ShowDialog(Application.Current.MainWindow) != CommonFileDialogResult.Ok)
            {
                return false;
            }

            SelectedPath = new DirectoryInfo(dialog.FileName);
            return true;
        }

        public bool ShowNewFolderButton { get; set; }
        
        public DirectoryInfo SelectedPath { get; set; }
        
        public Environment.SpecialFolder RootFolder { get; set; }
        
        public string Description { get; set; }
    }
}