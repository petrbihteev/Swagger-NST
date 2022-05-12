using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SwaggerHallOfFame
{
    /// <summary>  
    ///  Класс сотрудников.  
    /// </summary>  
    public partial class Person
    {
        public Person()
        {
            ConPersonSkills = new HashSet<ConPersonSkill>();
        }
        /// <summary>  
        ///  Идентификатор сотрудника.  
        /// </summary>  
        [Key]
        public long Id { get; set; }
        /// <summary>  
        ///  Наименование сотрудника.  
        /// </summary>  
        [Required(ErrorMessage = "Наименование работника пустое")]
        [StringLength(25,ErrorMessage = "Наименование работника больше 25 символов!")]
        public string Name { get; set; } = null!;
        /// <summary>  
        ///  Отображаемое имя сотрудника.  
        /// </summary>  
        [Required(ErrorMessage = "Отображаемое имя работника пустое")]
        [StringLength(25, ErrorMessage = "Отображаемое имя работника больше 25 символов!")]
        public string DisplayName { get; set; } = null!;

        public virtual ICollection<ConPersonSkill> ConPersonSkills { get; set; }
    }
}
