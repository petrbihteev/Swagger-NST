using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace SwaggerHallOfFame.Controllers
{
    [Route("api/")]
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
        //Метод GET: api/v1/persons Получение всех сотрудников
        [HttpGet("v1/persons")]
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
                    ex.InnerException.Message.ToString());
            }
        }

        /// <summary>
        /// Получение конкретного сотрудника
        /// </summary>
        //Метод GET: api/v1/person/id Получение конкретного сотрудника по Id
        [HttpGet("v1/person/{id}")]
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
                    ex.InnerException.Message.ToString());
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
        //Метод Post: api/v1/person Добавление сотрудника
        [HttpPost("v1/person/")]
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
                    ex.InnerException.Message.ToString());
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
        //Метод Put: api/v1/person Обновление данных конретного сотрудника
        [HttpPut("v1/person/{id}")]
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
                    ex.InnerException.Message.ToString());
            }
        }

        /// <summary>
        /// Удаление конкретного сотрудника
        /// </summary>
        //Метод Delete: api/v1/person/id Удаление конкретного сотрудника по Id
        [HttpDelete("v1/person/{id}")]
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
                    ex.InnerException.Message.ToString());
            }
        }
    }
}
