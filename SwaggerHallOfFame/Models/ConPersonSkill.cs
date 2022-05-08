using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwaggerHallOfFame
{
    public partial class ConPersonSkill
    {
        [Key]
        public long IdPersonSkill { get; set; }
        [Required(ErrorMessage = "Уровень пустой")]
        [Range(1, 10, ErrorMessage = "Уровень навыка может быть в диапазоне от 1 до 10")]
        public byte Level { get; set; }
        [ForeignKey(nameof(Person))]
        public long PersonId { get; set; }
        [ForeignKey(nameof(Skill))]
        public long SkillId { get; set; }
    }
}
