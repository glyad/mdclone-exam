namespace MdClone.Data.Contracts.Dto
{
    public class EmailDto
    {
        public EmailRecipientsDto To { get; set; }
        public EmailRecipientsDto Cc { get; set; }
        public string Subject { get; set; }
        public byte[] Message { get; set; }
    }
}