using System;

namespace Sekure.Models
{
    public class IntegrityTokenValue
    {
        public long Id { get; set; }
        public Guid SessionId { get; set; }
        public string ParameterName { get; set; }
        public int ItemIndex { get; set; }
        public string ValuePlain { get; set; }
        public string ValueEncrypted { get; set; }
        public bool IsPII { get; set; }
        public DateTime CreationDate { get; set; }

        public IntegrityTokenValue() { }
    }
}
