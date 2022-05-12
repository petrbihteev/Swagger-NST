using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace SwaggerHallOfFame.Controllers
{
    /// <summary>  
    ///  Контроллер для навыков сотрудника.  
    /// </summary>
    [Route("api/v1/")]
    [ApiController]
    public class PersonSkillController : Controller
    {
        private readonly PersonDBContext _db;

        public PersonSkillController(PersonDBContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Получение всех навыков для сотрудниокв
        /// </summary>
        [HttpGet("personskills/")]
        public async Task<ActionResult<IEnumerable<ConPersonSkill>>> GetPersonSkills()
        {
            try
            {
                var allPersonSkill = await _db.ConPersonSkills.ToListAsync();

                if (allPersonSkill == null)
                {
                    return NotFound("Записи не найдены");
                }

                return Ok(allPersonSkill);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    ex.ToString());
            }
        }

        /// <summary>
        /// Получение навыка(-ов) определенного сотрудника
        /// </summary>
        [HttpGet("personskill/{id}")]
        public async Task<IActionResult> GetPersonSkill(long? id)
        {
            try
            {
                var personSkill = await _db.ConPersonSkills
                    .Where(x => x.PersonId == id).ToListAsync();

                if (!personSkill.Any())
                {
                    return NotFound("Навыки сотрудника не найдены");
                }

                return Ok(personSkill);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    ex.ToString());
            }
        }

        /// <summary>
        /// Добавление навыка для сотрудника
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     {
        ///         "level": 10,
        ///         "personId": 4,
        ///         "skillId": 3
        ///     }
        ///
        /// </remarks>
        [HttpPost("personskill/")]
        public async Task<ActionResult<ConPersonSkill>> PostPersonSkill(ConPersonSkill postPersonSkill)
        {
            try
            {
                var personSkill = await _db.ConPersonSkills.FirstOrDefaultAsync(x => x.PersonId == 
                postPersonSkill.PersonId && x.SkillId == postPersonSkill.SkillId);

                if (personSkill != null)
                {
                    return NotFound("У сотрудника уже есть данный навык");
                }

                _db.ConPersonSkills.Add(postPersonSkill);
                await _db.SaveChangesAsync();

                return Ok(postPersonSkill);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, 
                    ex.ToString());
            }
        }

        /// <summary>
        /// Обновление навыка сотрудника
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     {
        ///         "idPersonSkill": 0,
        ///         "level": 10,
        ///         "personId": 0,
        ///         "skillId": 0
        ///     }
        ///
        /// </remarks>
        [HttpPut("personskill/{id}")]
        public async Task<ActionResult<ConPersonSkill>> PutPersonSkill(long? id, ConPersonSkill updatePersonSkill)
        {
            try
            {
                if (id != updatePersonSkill.IdPersonSkill)
                {
                    return BadRequest("Не совпадают id");
                }

                var personSkill = await _db.ConPersonSkills.FindAsync(id);
                _db.Entry(personSkill).CurrentValues.SetValues(updatePersonSkill);
                await _db.SaveChangesAsync();

                return Ok(personSkill);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    ex.ToString());
            }
        }

        /// <summary>
        /// Удаление конкретного навыка
        /// </summary>
        [HttpDelete("personskill/{id}")]
        public async Task<IActionResult> DeletePersonSkill(long? id)
        {
            try
            {
                var personSkill = await _db.ConPersonSkills.FindAsync(id);

                if (personSkill == null)
                {
                    return NotFound("Неверный id");
                }

                _db.ConPersonSkills.Remove(personSkill);
                await _db.SaveChangesAsync();

                return Ok(personSkill);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    ex.ToString());
            }
        }
    }
}
