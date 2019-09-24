using Api.Dto;
using Shared.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class ShowsRepository
    {
        private static int PAGE_ITEM_COUNT = 10;

        public static double getTotalpageCount(RtlDbContext showContext)
        {
            return Math.Ceiling((double)showContext.Shows.LongCount() / PAGE_ITEM_COUNT);
        }

        public static List<ShowDto> getPagedShows(RtlDbContext showContext, int page)
        {
            if (page >= 1)
            {
                return showContext.Shows
                .Skip((page - 1) * PAGE_ITEM_COUNT)
                .Take(PAGE_ITEM_COUNT)
                .Include(show => show.Actors)
                .Select(show => new ShowDto(show))
                .ToList();
            }
            else
            {
                return null;
            }
        }
    }
}
