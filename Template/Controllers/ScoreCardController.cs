using System;
using Microsoft.AspNetCore.Mvc;
using Template.Repositories.ScoreCards;

namespace Template.Controllers
{
	public class ScoreCardController : Controller
    {
        private readonly ILogger<ScoreCardController> _logger;
        private readonly IScoreCardRepository _scoreCardRepository;

        public ScoreCardController(ILogger<ScoreCardController> logger,
            IScoreCardRepository scoreCardRepository)
		{
            _logger = logger;
            _scoreCardRepository = scoreCardRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Paginate()
        {
            var data = await _scoreCardRepository.GetAllScoreCardsAsync();
            return Json(data);
        }
    }
}

