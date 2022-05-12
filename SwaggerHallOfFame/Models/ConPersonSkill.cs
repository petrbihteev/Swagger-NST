using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwaggerHallOfFame
{
    /// <summary>  
    ///  Класс навыки сотрудников.  
    /// </summary>  
    public partial class ConPersonSkill
    {
        /// <summary>  
        ///  Идентификатор навыка сотрудника.  
        /// </summary>  
        [Key]
        public long IdPersonSkill { get; set; }
        /// <summary>  
        ///  Уровень навыка.  
        /// </summary>  
        [Required(ErrorMessage = "Уровень пустой")]
        [Range(1, 10, ErrorMessage = "Уровень навыка может быть в диапазоне от 1 до 10")]
        public byte Level { get; set; }
        /// <summary>  
        ///  Внешний ключ сотрудника.  
        /// </summary>  
        [ForeignKey(nameof(Person))]
        public long PersonId { get; set; }
        /// <summary>  
        ///  Внешний ключ навыка.  
        /// </summary>  
        [ForeignKey(nameof(Skill))]
        public long SkillId { get; set; }
    }
}
