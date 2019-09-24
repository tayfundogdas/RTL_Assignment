using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shared.Data.Entities
{
    [Table("Actors")]
    public class ActorEntity
    {
        public long Id { get; set; }

        public long CorrelationId { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime? Birthday { get; set; }
    }
}
