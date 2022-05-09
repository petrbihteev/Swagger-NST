using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace SwaggerHallOfFame.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/")]
    [ApiController]
    public class SkillController : Controller
    {
        private readonly PersonDBContext _db;

        public SkillController(PersonDBContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Получение всех навыков
        /// </summary>
        //Метод GET: api/v1/skills Получение всех сотрудников
        [HttpGet("v1/skills")]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkills()
        {
            try
            {
                var allSkills = await _db.Skills.ToListAsync();

                if (allSkills == null)
                {
                    return NotFound("Навыки не существуют");
                }

                return Ok(allSkills);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    ex.InnerException.Message.ToString());
            }
        }

        /// <summary>
        /// Получение конкретного навыка
        /// </summary>
        //Метод GET: api/v1/skill/id Получение конкретного навыка по Id
        [HttpGet("v1/skill/{id}")]
        public async Task<IActionResult> GetSkill(long? id)
        {
            try
            {
                var skill = await _db.Skills.FindAsync(id);

                if (skill == null)
                {
                    return NotFound("Неверный Id");
                }

                return Ok(skill);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    ex.InnerException.Message.ToString());
            }
        }

        /// <summary>
        /// Добавление навыка
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     {
        ///        "name": "Знание ПК"
        ///     }
        ///
        /// </remarks>
        //Метод Post: api/v1/skill Добавление навыка
        [HttpPost("v1/skill/")]
        public async Task<ActionResult<Skill>> PostSkill(Skill skill)
        {
            try
            {
                _db.Skills.Add(skill);
                await _db.SaveChangesAsync();

                return Ok(skill);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    ex.InnerException.Message.ToString());
            }
        }

        /// <summary>
        /// Обновление навыка
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     {
        ///        "id": 1,
        ///        "name": "Знание PC"
        ///     }
        ///
        /// </remarks>
        //Метод Put: api/v1/skill Обновление данных конретного навыка
        [HttpPut("v1/skill/{id}")]
        public async Task<ActionResult<Skill>> PutSkill(long? id, Skill updateSkill)
        {
            try
            {
                if (id != updateSkill.Id)
                {
                    return BadRequest("Не совпадают id");
                }

                var skill = await _db.Skills.FindAsync(id);
                _db.Entry(skill).CurrentValues.SetValues(updateSkill);
                await _db.SaveChangesAsync();

                return Ok(skill);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    ex.InnerException.Message.ToString());
            }
        }

        /// <summary>
        /// Удаление конкретного навыка
        /// </summary>
        //Метод Delete: api/v1/skill/id Удаление конкретного навыка по Id
        [HttpDelete("v1/skill/{id}")]
        public async Task<IActionResult> DeleteSkill(long? id)
        {
            try
            {
                var skill = await _db.Skills.FindAsync(id);

                if (skill == null)
                {
                    return NotFound("Неверный id");
                }

                _db.Skills.Remove(skill);
                await _db.SaveChangesAsync();

                return Ok(skill);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                     ex.InnerException.Message.ToString());
            }
        }
    }
}
