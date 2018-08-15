using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetClinic.Models
{
    public class ProcedureAnimalAid
    {
        //-	ProcedureId – integer, Primary Key
        //-	Procedure – the animal aid’s procedure(required)
        //-	AnimalAidId – integer, Primary Key
        //-	AnimalAid – the procedure’s animal aid(required)

        public int ProcedureId { get; set; }
        [Required]
        public Procedure Procedure { get; set; }

        public int AnimalAidId { get; set; }
        [Required]
        public AnimalAid AnimalAid { get; set; }
    }
}
