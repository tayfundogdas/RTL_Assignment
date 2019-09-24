using System;
using Shared.Data.Entities;

namespace Api.Dto
{
    public class ActorDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Birthday { get; set; }

        public ActorDto(ActorEntity actorEntity)
        {
            Id = actorEntity.CorrelationId;
            Name = actorEntity.Name;
            if (actorEntity.Birthday.HasValue)
            {
                Birthday = actorEntity.Birthday.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                Birthday = null;
            }

        }
    }
}
