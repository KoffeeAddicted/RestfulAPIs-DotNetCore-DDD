using Domain.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Base
{
    public abstract class AuditEntity<TKey> : DeleteEntity<TKey>, IAuditEntity<TKey>
    {
        public DateTime CreatedTime { get; set; }
        public string CreatedByName { get; set; }
        public long CreatedById { get; set; }
        public DateTime LastModifiedTime { get; set; }
        public string LastModifiedByName { get; set; }
        public long LastModifiedById { get; set; }
    }
}
