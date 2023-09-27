using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Template.Repositories.TeeBoxes;

namespace Template.Controllers
{
	public class TeeBoxController : Controller
	{
        private readonly ILogger<ScoreCardController> _logger;
        private readonly ITeeBoxRepository _teeBoxRepository;

        public TeeBoxController(ILogger<ScoreCardController> logger,
            ITeeBoxRepository teeBoxRepository)
		{
            _logger = logger;
            _teeBoxRepository = teeBoxRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Paginate()
        {
            var data = await _teeBoxRepository.GetAllTeeBoxesAsync();
            return Json(data);
        }
    }
}

