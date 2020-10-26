using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly GameDBContext _context;
        private readonly GameCenter _gamecenter;

        public RoomsController(GameDBContext context, GameCenter gameCenter)
        {
            _context = context;
            _gamecenter = gameCenter;
        }

        // GET: api/<RoomsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        // GET: api/<HomeController> [FromQuery(Name = "playerid")]
        // can just parameter as "[FromBody] Player fakePlayer" ( can skip [FromBody])
        // then send { id : 123}
        // or "[FromBody] long playerid" , then just send 5 (without quoto)
        [HttpPost("Join/{roomid}")]
        public async Task<ActionResult<Room>> Join(int roomid, Player fakePlayer)
        {
            var player = await _context.Players.FindAsync(fakePlayer.Id);
            if (player == null) return NotFound("Cannot find this user.");
            var room = await _context.Rooms.FindAsync(roomid);
            if (room == null) return NotFound("Cannot find room.");
            if (room.isFull()) return BadRequest("Room is full");
            return await _gamecenter.JoinRoom(room, player);
        }

        // GET: api/<HomeController>
        [HttpPost("Create")]
        public async Task<ActionResult<Room>> Create(Player fakePlayer)
        {
            var player = _context.Players.Find(fakePlayer.Id);
            if (player == null) return NotFound("Cannot find this user.");
            return await _gamecenter.CreateRoom(player);
        }

        [HttpPost("Start/{roomid}")]
        public async Task<ActionResult> StartGameByRoom(int roomid, string[] shipMap)
        {
            var room = await _context.Rooms.FindAsync(roomid);
            if (room == null) return NotFound("Cannot find room.");
            if (!room.isFull()) return BadRequest("Room is not full.");
            _gamecenter.StartGame(room, shipMap);
            return Ok();
        }
    }
}
