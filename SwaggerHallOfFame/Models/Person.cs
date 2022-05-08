﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwaggerHallOfFame
{
    public partial class Person
    {
        public Person()
        {
            ConPersonSkills = new HashSet<ConPersonSkill>();
        }
        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Наименование работника пустое")]
        [StringLength(25,ErrorMessage = "Наименование работника больше 25 символов!")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Отображаемое имя работника пустое")]
        [StringLength(25, ErrorMessage = "Отображаемое имя работника больше 25 символов!")]
        public string DisplayName { get; set; } = null!;

        public virtual ICollection<ConPersonSkill> ConPersonSkills { get; set; }
    }
}
