using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BaseEntity
    {
        // EF is convention based - it will automatically make "Id" the primary key 
        // It would not work if the prop was called something like TheId
        public int Id { get; set; }
    }
}