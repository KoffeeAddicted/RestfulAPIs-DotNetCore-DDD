using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Base.Interfaces
{
    public interface IAuditEntity
    {
        DateTime CreatedTime { get; set; }
        string CreatedByName { get; set; }
        long CreatedById { get; set; }
        DateTime LastModifiedTime { get; set; }
        string LastModifiedByName { get; set; }
        long LastModifiedById { get; set; }
    }

    public interface IAuditEntity<TKey> : IAuditEntity, IDeleteEntity<TKey>
    {

    }    
}
