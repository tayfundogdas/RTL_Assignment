using Shared.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Api.Dto
{
    public class ShowDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public List<ActorDto> cast { get; set; }

        public ShowDto(ShowEntity showEntity)
        {
            Id = showEntity.CorrelationId;
            Name = showEntity.Name;
            cast = showEntity.Actors
                .OrderByDescending(actor => actor.Birthday)
                .Select(actor => new ActorDto(actor)).ToList();
        }
    }
}
