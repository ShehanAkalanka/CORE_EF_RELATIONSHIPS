using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NET_CORE_EF_RELATIONSHIPS.Data;
using NET_CORE_EF_RELATIONSHIPS.DTO;

namespace NET_CORE_EF_RELATIONSHIPS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly DataContext _Context;

        public CharacterController(DataContext Context)
        {
            _Context = Context;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<Character>>> Get(int userId)
        {
            var characters = await _Context.Characters
                .Where(c => c.UserId == userId)
                .Include(c=>c.Weapon)
                .Include(c=>c.Skills)
                .ToListAsync();
            return Ok(characters);
        }

        [HttpPost]
        public async Task<ActionResult<List<Character>>> CreateCharacter(Character character)
        {
            _Context.Characters.Add(character);
            await _Context.SaveChangesAsync();

            return await Get(character.UserId);
        }

        [HttpPost("Weapon")]
        public async Task<ActionResult<List<Character>>> AddWeapon(Weapon weapon)
        {
            var character = await _Context.Characters.FindAsync(weapon.CharacterId);
            if (character == null)
                return NotFound();
            var newWeapon = new Weapon
            {
                Name = weapon.Name,
                Damage=weapon.Damage,
                CharacterId=weapon.CharacterId
            };
            _Context.weapons .Add(newWeapon);
            await _Context.SaveChangesAsync();

            return Ok(character);
        }

        [HttpPost("skill")]
        public async Task<ActionResult<List<Character>>> AddCharacterSkill(CharacterSkillDTO request)
        {
            var character = await _Context.Characters
                .Where(c => c.Id == request.CharacterId)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync();

            if (character == null)
                return NotFound();

            var skill = await _Context.Skills.FindAsync(request.SkillId);
            if (skill == null)
                return NotFound();

            character.Skills.Add(skill);
            await _Context.SaveChangesAsync();

            return Ok(character);
        }
    }
}
