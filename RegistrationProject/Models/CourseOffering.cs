using System;

namespace CourseRegistration.Models
{
    public class CourseOffering: IComparable<CourseOffering>
    {
        public Course TheCourse {get;set;}
        public string Semester {get;set;}
        public string Section {get;set;}
        public string Department {get;set;}
   // public string Instructor {get;set;}

        public override String ToString() {
            return $"{TheCourse}{Department} Section {Section} offered ({Semester})\n";

        }
        public int CompareTo(CourseOffering other) {
            return this.Semester.CompareTo(other.Semester);
        }

        
    }
}
