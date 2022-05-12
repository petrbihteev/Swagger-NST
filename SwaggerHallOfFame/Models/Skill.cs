using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SwaggerHallOfFame
{
    /// <summary>  
    ///  Класс навыков.  
    /// </summary>
    public partial class Skill
    {
        public Skill()
        {
            ConPersonSkills = new HashSet<ConPersonSkill>();
        }
        /// <summary>  
        ///  Идентификатор навыка.  
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>  
        ///  Наименование навыка.  
        /// </summary>
        [Required(ErrorMessage = "Наименование навыка пустое")]
        [StringLength(25, ErrorMessage = "Наименование навыка больше 25 символов!")]
        public string Name { get; set; } = null!;

        public virtual ICollection<ConPersonSkill> ConPersonSkills { get; set; }
    }
}
