using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly PlayerContext _context;
        private readonly GameCenter _gamecenter;

        public RoomsController(PlayerContext context, GameCenter gameCenter)
        {
            _context = context;
            _gamecenter = gameCenter;
        }

        // GET: api/<RoomsController>
        [HttpGet]
        public IEnumerable<Room> Get()
        {
            return _gamecenter.GetRooms();
        }

        // GET: api/<HomeController> [FromQuery(Name = "playerid")]
        // can just parameter as "[FromBody] Player fakePlayer" ( can skip [FromBody])
        // then send { id : 123}
        // or "[FromBody] long playerid" , then just send 5 (without quoto)
        [HttpPost("Join/{roomid}")]
        public ActionResult<Room> Join(int roomid, Player fakePlayer)
        {
            var player = _context.Players.Find(fakePlayer.Id);
            if (player == null) return NotFound("Cannot find this user.");
            var room = _gamecenter.JoinRoom(roomid, player);
            if (room == null) return NotFound("Cannot find room.");
            return room;
        }

        // GET: api/<HomeController>
        [HttpPost("Create")]
        public ActionResult<Room> Create(Player fakePlayer)
        {
            var player = _context.Players.Find(fakePlayer.Id);
            if (player == null) return NotFound("Cannot find this user.");
            var room = _gamecenter.CreateRoom(player);
            return room;
        }
    }
}
