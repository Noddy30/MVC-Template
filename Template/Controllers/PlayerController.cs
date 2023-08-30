using System;
using Microsoft.AspNetCore.Mvc;
using Template.Repositories.Players;

namespace Template.Controllers
{
	public class PlayerController : Controller
	{
		private readonly IPlayerRepository _playerRepository;

		public PlayerController(IPlayerRepository playerRepository)
		{
			_playerRepository = playerRepository;

        }

		public IActionResult Index()
		{
			return View();
		}

	}
}

