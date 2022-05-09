using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace SwaggerHallOfFame.Controllers
{
    [Route("api/")]
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
        //Метод GET: api/v1/personskills Получение всех навыков для сотрудников
        [HttpGet("v1/personskills")]
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
                    ex.InnerException.Message.ToString());
            }
        }

        /// <summary>
        /// Получение навыка(-ов) определенного сотрудника
        /// </summary>
        //Метод GET: api/v1/personskill/id Получение навыков сотрудника по Id
        [HttpGet("v1/personskill/{id}")]
        public async Task<IActionResult> GetPersonSkill(long? id)
        {
            try
            {
                var personSkill = await _db.ConPersonSkills
                    .Where(x => x.PersonId == id).ToListAsync();

                if (personSkill.Count == 0)
                {
                    return NotFound("Навыки сотрудника не найдены");
                }

                return Ok(personSkill);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    ex.InnerException.Message.ToString());
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
        //Метод Post: api/v1/personskill Добавление навыка для сотрудника
        [HttpPost("v1/personskill/")]
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
                    ex.InnerException.Message.ToString());
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
        //Метод Put: api/v1/personskill Обновление навыка у сотрудника
        [HttpPut("v1/personskill/{id}")]
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
                    ex.InnerException.Message.ToString());
            }
        }

        /// <summary>
        /// Удаление конкретного навыка
        /// </summary>
        //Метод Delete: api/v1/personskill/id Удаление конкретного навыка по Id
        [HttpDelete("v1/personskill/{id}")]
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
                    ex.InnerException.Message.ToString());
            }
        }
    }
}
