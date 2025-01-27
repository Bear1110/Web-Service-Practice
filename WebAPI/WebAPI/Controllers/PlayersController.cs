﻿
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebAPI.Hubs;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly GameDBContext _context;
        private readonly IActionContextAccessor _accessor;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly GameCenter _gamecenter;

        public PlayersController(
            GameDBContext context,
            IActionContextAccessor aca,
            IHubContext<ChatHub> hubContext,
            GameCenter gameCenter)
        {
            _context = context;
            _accessor = aca;
            _hubContext = hubContext;
            _gamecenter = gameCenter;
        }

        // GET: api/Players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return await _context.Players.ToListAsync();
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(long id)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        [HttpGet("online")]
        public ActionResult<List<Player>> GetOnlinePlayer() => _gamecenter.ListOnlinePlayer();

        // PUT: api/Players/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(long id, Player player)
        {
            if (id != player.Id)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Players
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player onlyName)
        {
            string inputName = onlyName.Name;
            var player = await _context.Players.SingleOrDefaultAsync(e => e.Name == inputName);
            if (player != null) {
                _context.Entry(player).State = EntityState.Modified;
            }
            else
            {
                player = new Player(inputName);
                _context.Players.Add(player);
            }
            player.Ip = _accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            await _context.SaveChangesAsync();
            BroadcastHelper.Online(_hubContext, player);
            _gamecenter.AddOnlinePlayer(player);
            return CreatedAtAction(nameof(GetPlayer), new { id = player.Id }, player);
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Player>> DeletePlayer(long id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return player;
        }

        private bool PlayerExists(long id) => _context.Players.Any(e => e.Id == id);
    }
}