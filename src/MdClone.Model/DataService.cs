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
        private readonly EmailModelMapper _emailModelMapper;
        private readonly IFileTypeModel[] _fileTypes;

        public DataService(IDataProvider dataProvider, TableDataModelMapper tableDataModelMapper, EmailModelMapper emailModelMapper)
        {
            _dataProvider = dataProvider;
            _tableDataModelMapper = tableDataModelMapper;
            _emailModelMapper = emailModelMapper;
            _fileTypes = new IFileTypeModel[]
            {
                new FileTypeModel {Filter = "*.csv", DisplayName = "CSV Files (*.csv)"}
            };
        }

        IEmailModel IDataService.CreateNewEmail()
        {
            return new EmailModel();
        }

        Task<ITableDataModel> IDataService.LoadData(IFileModel fileModel, CancellationToken ct)
        {
            return Task.Run(() => _tableDataModelMapper.MapToModel(_dataProvider.LoadData(fileModel.Name)), ct);
        }

        Task IDataService.SendEmail(IEmailModel emailModel, CancellationToken ct)
        {
            return Task.Run(() => _dataProvider.SendEmail(_emailModelMapper.MapToDto(emailModel)), ct);
        }

        IFileTypeModel[] IDataService.FileTypes => _fileTypes;
    }
}