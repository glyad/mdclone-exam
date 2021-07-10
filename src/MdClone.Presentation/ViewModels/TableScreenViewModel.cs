using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Input;
using JetBrains.Annotations;
using LogoFX.Client.Core;
using LogoFX.Client.Mvvm.Commanding;
using LogoFX.Client.Mvvm.ViewModel;
using LogoFX.Client.Mvvm.ViewModel.Services;
using MdClone.Model.Contracts;

namespace MdClone.Presentation.ViewModels
{
    [UsedImplicitly]
    public class TableScreenViewModel : ObjectViewModel<ITableDataModel>
    {
        private readonly IDataService _dataService;
        private readonly IBrowseFolderService _browseFolderService;
        private readonly IFileSearchService _fileSearchService;

        public TableScreenViewModel(
            ITableDataModel model, 
            IDataService dataService,
            IBrowseFolderService browseFolderService,
            IFileSearchService fileSearchService)
            : base(model)
        {
            _dataService = dataService;
            _browseFolderService = browseFolderService;
            _fileSearchService = fileSearchService;
            if (FileTypes.Length > 0)
            {
                SelectedFileType = FileTypes[0];
            }
        }

        private ICommand _browseCommand;

        public ICommand BrowseCommand => _browseCommand ??= ActionCommand
            .When(() => true)
            .Do(() =>
            {
                _browseFolderService.SelectedPath = new DirectoryInfo(Path);
                if (!_browseFolderService.DetermineFolder())
                {
                    return;
                }

                Path = _browseFolderService.SelectedPath.FullName;
            });


        public override string DisplayName
        {
            get => "Data Table";
            set { }
        }

        private bool _isDataLoaded;

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        public bool IsDataLoaded
        {
            get => _isDataLoaded;
            private set => SetProperty(ref _isDataLoaded, value);
        }

        private bool _isFileListUpdating;

        public bool IsFileListUpdating
        {
            get => _isFileListUpdating;
            private set => SetProperty(ref _isFileListUpdating, value);
        }

        private string _path = System.IO.Path.GetFullPath(".");

        public string Path
        {
            get => _path;
            private set =>
                SetProperty(ref _path, value, new SetPropertyOptions
                {
                    AfterValueUpdate = UpdateFileList
                });
        }

        public IFileTypeModel[] FileTypes => _dataService.FileTypes;

        private IFileTypeModel _selectedFileType;

        public IFileTypeModel SelectedFileType
        {
            get => _selectedFileType;
            set =>
                SetProperty(ref _selectedFileType, value, new SetPropertyOptions
                {
                    AfterValueUpdate = UpdateFileList
                });
        }

        private async void UpdateFileList()
        {
            if (IsFileListUpdating)
            {
                return;
            }

            IsFileListUpdating = true;
            try
            {
                IsFileListUpdating = true;
                var fileList = await _fileSearchService.GetFiles(".", SelectedFileType);
            }

            finally
            {
                IsFileListUpdating = false;
            }
        }
    }
}