using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly PlayerContext _context;
        private readonly GameCenter _gamecenter;
        private readonly IActionContextAccessor _accessor;

        public HomeController(PlayerContext context, GameCenter gameCenter, IActionContextAccessor aca)
        {
            _context = context;
            _gamecenter = gameCenter;
            _accessor = aca;
        }

        [HttpGet]
        public IEnumerable<string> Get() => new string[] { "Login", "Create", "Join" };

        [HttpPost("Login")]
        public async Task<ActionResult<Player>> Login(Player player)
        {
            player.IP = _accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return player;
        }

        // GET: api/<HomeController>
        [HttpGet("Create/{playerid}")]
        public ActionResult<Room> Create(long playerid)
        {
            var player = _context.Players.Find(playerid);
            if (player == null) return NotFound("Cannot find this user.");
            var room = _gamecenter.CreateRoom(player);
            return room;
        }

        // GET: api/<HomeController> [FromQuery(Name = "playerid")]
        [HttpGet("Join")]
        public ActionResult<Room> Join(int roomid, long playerid)
        {
            var player = _context.Players.Find(playerid);
            if (player == null) return NotFound("Cannot find this user.");
            var room = _gamecenter.JoinRoom(roomid, player);
            if (room == null) return NotFound("Cannot find room.");
            return room;
        }

        [HttpGet("LookUp")]
        public IEnumerable<Room> LookUp()
        {
            return _gamecenter.GetRooms();
        }
    }
}
