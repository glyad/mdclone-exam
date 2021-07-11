using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Input;
using Caliburn.Micro;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.Commanding;
using LogoFX.Client.Mvvm.ViewModel;
using LogoFX.Client.Mvvm.ViewModel.Services;
using MdClone.Model.Contracts;

namespace MdClone.Presentation.ViewModels
{
    [UsedImplicitly]
    public class TableScreenViewModel : Conductor<TableDataViewModel>
    {
        private readonly IDataService _dataService;
        private readonly IBrowseFolderService _browseFolderService;
        private readonly IFileSearchService _fileSearchService;
        private readonly IViewModelCreatorService _viewModelCreatorService;
        private readonly WrappingCollection _fileListCollection;
        private IFileModel _loadingFile;

        public TableScreenViewModel(
            IDataService dataService,
            IBrowseFolderService browseFolderService,
            IFileSearchService fileSearchService,
            IViewModelCreatorService viewModelCreatorService)
        {
            _dataService = dataService;
            _browseFolderService = browseFolderService;
            _fileSearchService = fileSearchService;
            _viewModelCreatorService = viewModelCreatorService;

            _fileListCollection = new WrappingCollection
            {
                FactoryMethod = o =>
                {
                    var fileViewModel = _viewModelCreatorService.CreateViewModel<IFileModel, FileViewModel>((IFileModel) o);
                    return fileViewModel;
                }
            };

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
            private set => Set(ref _isDataLoaded, value);
        }

        private bool _isFileListUpdating;

        public bool IsFileListUpdating
        {
            get => _isFileListUpdating;
            private set => Set(ref _isFileListUpdating, value);
        }

        private bool _isDataUpdating;

        public bool IsDataUpdating
        {
            get => _isDataUpdating;
            private set => Set(ref _isDataUpdating, value);
        }

        private string _path = System.IO.Path.GetFullPath(@"..\..\TestData");

        public string Path
        {
            get => _path;
            private set
            {
                if (Set(ref _path, value))
                {
                    UpdateFileList();
                }
            }
        }

        public WrappingCollection FileListCollection => _fileListCollection;

        private FileViewModel _selectedFile;
        public FileViewModel SelectedFile
        {
            get => _selectedFile;
            set
            {
                if (Set(ref _selectedFile, value))
                {
                    LoadData(_selectedFile?.Model);
                }
            }
        }

        private async void LoadData(IFileModel fileModel)
        {
            IsDataUpdating = true;
            IsDataLoaded = false;
            _loadingFile = fileModel;

            if (fileModel == null)
            {
                ActivateItem(null);
            }
            else
            {
                var data = await _dataService.LoadData(fileModel);
                ActivateItem(_viewModelCreatorService.CreateViewModel<ITableDataModel, TableDataViewModel>(data));
            }

            if (_loadingFile != fileModel)
            {
                return;
            }

            IsDataLoaded = true;
            IsDataUpdating = false;
        }

        public IFileTypeModel[] FileTypes => _dataService.FileTypes;

        private IFileTypeModel _selectedFileType;

        public IFileTypeModel SelectedFileType
        {
            get => _selectedFileType;
            set
            {
                if (Set(ref _selectedFileType, value))
                {
                    UpdateFileList();
                }
            }
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
                
                _fileListCollection.ClearSources();
                var fileList = await _fileSearchService.GetFiles(Path, SelectedFileType);
                _fileListCollection.AddSource(fileList);
            }

            finally
            {
                IsFileListUpdating = false;
            }
        }
    }
}