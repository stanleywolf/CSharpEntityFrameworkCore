﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P01_StudentSystem.Data.Models
{
    public class Student
    {
        public Student()
        {
            this.HomeworkSubmissions = new HashSet<Homework>();
            this.CourseEnrollments = new HashSet<StudentCourse>();
        }
       // [Key]
        public int  StudentId { get; set; }

       // [Required]
       // [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

       // [Range(10,10)]
        public string PhoneNumber { get; set; }

        //[Required]
        public DateTime RegisteredOn { get; set; }

        public DateTime? Birthday { get; set; }

        public ICollection<Homework> HomeworkSubmissions { get; set; }
        public ICollection<StudentCourse> CourseEnrollments { get; set; }
    }
}
