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
        /// Получение навыков сотрудника
        /// </summary>
        //Метод GET: api/v1/personskill/id Получение навыков сотрудника по Id
        [HttpGet("v1/personskill/{id}")]
        public async Task<IActionResult> GetPersonSkills(long? id)
        {
            try
            {
                var personSkill = await _db.ConPersonSkills
                    .Where(x => x.PersonId == id).ToListAsync();

                if (personSkill.Count == 0)
                {
                    return NotFound("Навыки не найдены");
                }

                return Ok(personSkill);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Добавление навыка для сотрудника
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     {
        ///        "name": "Знание ПК"
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
                    return NotFound("У сотрудника уже есть данный навык!");
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
