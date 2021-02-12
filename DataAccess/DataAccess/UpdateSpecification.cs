using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.DataAccess
{
    public partial class UpdateSpecification
    {
        public UpdateSpecification()
        {
            Strategies = new HashSet<Strategy>();
        }

        public int Id { get; set; }
        public string VersionNumber { get; set; }
        public string FilePath { get; set; }

        public virtual ICollection<Strategy> Strategies { get; set; }
    }
}
