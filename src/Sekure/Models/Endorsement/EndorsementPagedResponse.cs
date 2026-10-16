using System.Collections.Generic;

namespace Sekure.Models.Endorsement
{
    public class EndorsementPagedResponse
    {
        public List<EndorsementResponse> Items { get; set; }
        public int Total { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
