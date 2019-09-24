using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shared.Data.Entities
{
    [Table("Shows")]
    public class ShowEntity
    {
        public long Id { get; set; }

        public long CorrelationId { get; set; }

        public string Name { get; set; }

        public List<ActorEntity> Actors { get; set; }
    }
}
