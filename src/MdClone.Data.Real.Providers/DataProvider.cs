using System.Collections.Generic;
using System.Linq;
using System.Threading;
using JetBrains.Annotations;
using MdClone.Data.Contracts.Dto;
using MdClone.Data.Contracts.Providers;

namespace MdClone.Data.Real.Providers
{
	[UsedImplicitly]
	internal class DataProvider : IDataProvider
    {
	    public IEnumerable<ISupportedFormatInfo> GetSupportedFormats()
	    {
            //Assembly.GetAssembly(GetType()).GetTypes().Where(type => type.FindInterfaces((filtered, criteria) => ))
		    throw new System.NotImplementedException();
	    }

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

        public void SendEmail(EmailDto emailDto)
        {
            Thread.Sleep(4000);
        }
    }
}