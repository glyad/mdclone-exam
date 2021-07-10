namespace MdClone.Data.Contracts.Dto
{
    public class TableDataDto
    {
        public string[] Header { get; set; }

        public RowDataDto[] Rows { get; set; }
    }
}