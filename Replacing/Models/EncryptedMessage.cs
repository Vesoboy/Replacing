using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Replacing.Models
{
    public class EncryptedMessage
    {
        [Key]
        public Guid MessageId { get; set; }

        public string? OriginalMessage { get; set; }

        public string? EncryptedMessageText { get; set; }

        public DateTime ReceivedTime { get; set; }

    }
}
