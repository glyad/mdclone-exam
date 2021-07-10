using MdClone.Model.Contracts;

namespace MdClone.Model
{
    internal class DataService : IDataService
    {
        private readonly IFileTypeModel[] _fileTypes;

        public DataService()
        {
            _fileTypes = new IFileTypeModel[]
            {
                new FileTypeModel {Filter = "*.csv", DisplayName = "CSV Files (*.csv)"}
            };
        }

        ITableDataModel IDataService.CreateNewTable()
        {
            return new TableDataModel();
        }

        IEmailModel IDataService.CreateNewEmail()
        {
            return new EmailModel();
        }

        IFileTypeModel[] IDataService.FileTypes => _fileTypes;
    }
}