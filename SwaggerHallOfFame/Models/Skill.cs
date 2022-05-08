using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwaggerHallOfFame
{
    public partial class Skill
    {
        public Skill()
        {
            ConPersonSkills = new HashSet<ConPersonSkill>();
        }

        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Наименование навыка пустое")]
        [StringLength(25, ErrorMessage = "Наименование навыка больше 25 символов!")]
        public string Name { get; set; } = null!;

        public virtual ICollection<ConPersonSkill> ConPersonSkills { get; set; }
    }
}
