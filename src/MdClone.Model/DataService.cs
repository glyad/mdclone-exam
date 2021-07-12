using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MdClone.Data.Contracts.Providers;
using MdClone.Model.Contracts;
using MdClone.Model.Mappers;

namespace MdClone.Model
{
    internal class DataService : IDataService
    {
        private readonly IDataProvider _dataProvider;
        private readonly TableDataModelMapper _tableDataModelMapper;
        private IFileTypeModel[] _fileTypes;

        public DataService(IDataProvider dataProvider, TableDataModelMapper tableDataModelMapper)
        {
            _dataProvider = dataProvider;
            _tableDataModelMapper = tableDataModelMapper;
        }

        Task<ITableDataModel> IDataService.LoadData(IFileModel fileModel, CancellationToken ct)
        {
            return Task.Run(() => _tableDataModelMapper.MapToModel(_dataProvider.LoadData(fileModel.Name)), ct);
        }

        IFileTypeModel[] IDataService.FileTypes => _fileTypes = _fileTypes ?? GenerateFileTypes();

        private IFileTypeModel[] GenerateFileTypes() =>
            _dataProvider.GetSupportedFormats()
                .Select(info => new FileTypeModel
                {
                    DisplayName = info.SupportedFormatName,
                    Filter = string.Join(";", info.AllowedFileExtensions)
                } as IFileTypeModel)
                .ToArray();
    }
}