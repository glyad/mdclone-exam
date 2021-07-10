using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel;
using MdClone.Model.Contracts;

namespace MdClone.Presentation.ViewModels
{
    [UsedImplicitly]
    public class TableScreenViewModel : ObjectViewModel<ITableDataModel>
    {
        private readonly IDataService _dataService;

        public TableScreenViewModel(ITableDataModel model, IDataService dataService)
            : base(model)
        {
            _dataService = dataService;
            if (FileTypes.Length > 0)
            {
                SelectedFileType = FileTypes[0];
            }
        }

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

        public IFileTypeModel[] FileTypes => _dataService.FileTypes;

        private IFileTypeModel _selectedFileType;

        public IFileTypeModel SelectedFileType
        {
            get => _selectedFileType;
            set => SetProperty(ref _selectedFileType, value);
        }
    }
}