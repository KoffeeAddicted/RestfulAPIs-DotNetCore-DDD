using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comment : AuditEntity<Int64>
    {
        public String ProviderToken { get; set; }

        public Int64 StoryId { get; set; }

        public String Message { get; set; }

        public Int64 Rating { get; set; }

        public virtual Story Story { get; set; }
        public virtual User User { get; set; }
    }
}
