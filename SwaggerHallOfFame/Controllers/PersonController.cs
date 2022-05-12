using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace SwaggerHallOfFame.Controllers
{
    /// <summary>  
    ///  Контроллер для сотрудников.  
    /// </summary>
    [Route("api/v1/")]
    [ApiController]
    public class PersonController : Controller
    {
        private readonly PersonDBContext _db;

        public PersonController(PersonDBContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Получение всех сотрудников
        /// </summary>
        [HttpGet("persons/")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            try
            {
                var allPersons = await _db.Persons.ToListAsync();

                if (allPersons == null)
                {
                    return NotFound("Сотрудники не существуют");
                }

                return Ok(allPersons);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    ex.ToString());
            }
        }

        /// <summary>
        /// Получение конкретного сотрудника
        /// </summary>
        [HttpGet("person/{id}")]
        public async Task<IActionResult> GetPerson(long? id)
        {
            try
            {
                var person = await _db.Persons.FindAsync(id);

                if (person == null)
                {
                    return NotFound("Неверный Id");
                }

                return Ok(person);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    ex.ToString());
            }
        }

        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     {
        ///        "name": "Petr Bikhteev",
        ///        "displayName": "petrbikhteev"
        ///     }
        ///
        /// </remarks>
        [HttpPost("person/")]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            try
            {
                _db.Persons.Add(person);
                await _db.SaveChangesAsync();

                return Ok(person);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    ex.ToString());
            }
        }

        /// <summary>
        /// Обновление сотрудника
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     {
        ///        "id": 1,
        ///        "name": "Petr Bikhteev",
        ///        "displayName": "petr2002"      
        ///     }
        ///
        /// </remarks>
        [HttpPut("person/{id}")]
        public async Task<ActionResult<Person>> PutPerson(long? id,Person updatePerson)
        {
            try
            {
                if (id != updatePerson.Id)
                {
                    return BadRequest("Не совпадают id");
                }

                var person = await _db.Persons.FindAsync(id);
                _db.Entry(person).CurrentValues.SetValues(updatePerson);
                await _db.SaveChangesAsync();

                return Ok(person);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    ex.ToString());
            }
        }

        /// <summary>
        /// Удаление конкретного сотрудника
        /// </summary>
        [HttpDelete("person/{id}")]
        public async Task<IActionResult> DeletePerson(long? id)
        {
            try
            {
                var person = await _db.Persons.FindAsync(id);

                if (person == null)
                {
                    return NotFound("Неверный id");
                }

                _db.Persons.Remove(person);
                await _db.SaveChangesAsync();

                return Ok(person);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    ex.ToString());
            }
        }
    }
}
