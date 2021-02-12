using System;

#nullable disable

namespace DataAccess.DataAccess
{
    public partial class Log
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime UpdateRecievedDate { get; set; }
        public string VersionNumber { get; set; }
        public bool IsStrategyChecked { get; set; }

        public virtual Product IdNavigation { get; set; }
    }
}
