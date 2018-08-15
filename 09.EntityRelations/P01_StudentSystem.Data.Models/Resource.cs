using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using P01_StudentSystem.Data.Models.Enums;

namespace P01_StudentSystem.Data.Models
{
    public class Resource
    {
        //[Key]
        public int ResourceId { get; set; }

        //[Required]
       // [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

       // [Required]
        public string Url { get; set; }

        public ResourceType ResourceType  { get; set; }

       // [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
