using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Shared.Data.Entities;
using Api.Repository;
using Api.Dto;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {

        private RtlDbContext showContext;
        public ShowsController(RtlDbContext sc)
        {
            showContext = sc;
        }

        // GET api/shows
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "pageCount",
                ShowsRepository.getTotalpageCount(showContext).ToString() };
        }

        // GET api/shows/5
        [HttpGet("{page}")]
        public ActionResult<IEnumerable<ShowDto>> Get(int page)
        {
            return ShowsRepository.getPagedShows(showContext, page);
        }
    }
}
