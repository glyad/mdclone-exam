using System.Linq;
using MdClone.Data.Contracts.Dto;
using MdClone.Data.Contracts.Providers;

namespace MdClone.Data.Real.Providers
{
	internal class DataProvider : IDataProvider
    {
        public TableDataDto LoadData(string filename)
        {
            var csv = new CsvReader(filename);

            var result = new TableDataDto
            {
                Header = csv.Header,
                Rows = csv.Rows.Select(row => new RowDataDto
                {
                    Items = row.Values
                        .Select((value, i) => new ItemDataDto
                        {
                            Header = csv.Header[i], 
                            Value = value
                        })
                        .ToArray()
                }).ToArray()
            };

            return result;
        }
    }
}