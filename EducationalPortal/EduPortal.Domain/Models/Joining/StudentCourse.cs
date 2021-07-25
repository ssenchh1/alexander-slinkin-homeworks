using System;
using EduPortal.Domain.Models.Users;

namespace EduPortal.Domain.Models.Joining
{
    public class StudentCourse
    {
        public DateTime EnrolledAt { get; set; }

        public Student Student { get; set; }
        public string StudentId { get; set; }

        public Course Course { get; set; }
        public int CourseId { get; set; }
    }
}