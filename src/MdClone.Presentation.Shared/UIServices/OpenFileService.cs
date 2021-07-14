using System.Collections.Generic;
using System.IO;
using System.Linq;
using Caliburn.Micro;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel.Services;
using Microsoft.Win32;

namespace MdClone.Presentation.Shared.UIServices
{
    [UsedImplicitly]
    public sealed class OpenFileService : IOpenFileService
    {
        private OpenFileDialog _openFileDialog;

        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenFileService" /> class.
        /// </summary>
        public OpenFileService()
        {
            Execute.OnUIThread(() =>
            {
                _openFileDialog = new OpenFileDialog
                {
                    CheckFileExists = true,
                    CheckPathExists = true
                };
            });
        }

        /// <summary>
        ///     Gets a <see cref="FileInfo" /> object for the selected file. If multiple files are selected, returns the first selected file.
        /// </summary>
        public FileInfo File => new FileInfo(_openFileDialog.FileName);

        /// <summary>
        ///     Gets a collection of <see cref="FileInfo" /> objects for the selected files.
        /// </summary>
        public IEnumerable<FileInfo> Files
        {
            get { return _openFileDialog.FileNames.Select(name => new FileInfo(name)); }
        }

        /// <summary>
        ///     Gets or sets a filter string that specifies the file types and descriptions to display.
        /// </summary>
        public string Filter
        {
            get => _openFileDialog.Filter;
            set => _openFileDialog.Filter = value;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is allows to select multiple files.
        /// </summary>
        public bool Multiselect
        {
            get => _openFileDialog.Multiselect;
            set => _openFileDialog.Multiselect = value;
        }

        /// <summary>
        ///     Gets or sets the initial directory displayed by the file dialog box.
        /// </summary>
        public string InitialDirectory
        {
            get => _openFileDialog.InitialDirectory;
            set => _openFileDialog.InitialDirectory = value;
        }

        /// <summary>
        ///     Gets or sets a string shown in the title bar of the file dialog.
        /// </summary>
        public string Title
        {
            get => _openFileDialog.Title;
            set => _openFileDialog.Title = value;
        }

        /// <summary>
        ///     Determines the filename of the file what will be used.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if a file is selected; otherwise <c>false</c>.
        /// </returns>
        public bool DetermineFile()
        {
            return _openFileDialog.ShowDialog().GetValueOrDefault();
        }
    }
}