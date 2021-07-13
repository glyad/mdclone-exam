namespace MdClone.Data.Contracts.Dto
{
    public class EmailDto
    {
        public EmailRecipientDto[] To { get; set; }
        public EmailRecipientDto[] Cc { get; set; }
        public string Subject { get; set; }
        public byte[] Message { get; set; }
    }
}