using MdClone.Model.Contracts;

namespace MdClone.Model
{
    internal class DataService : IDataService
    {
        private IFileTypeModel[] _fileTypes;

        public DataService()
        {
            _fileTypes = new IFileTypeModel[]
            {
                new FileTypeModel {Filter = "*.json", DisplayName = "JSON Files (*.json)"}
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