using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using PetClinic.Models;

namespace PetClinic.Dto.ImportDtos
{
    public class AnimalDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Type { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int Age { get; set; }
                
        public PassportDto Passport { get; set; }
    }
}
