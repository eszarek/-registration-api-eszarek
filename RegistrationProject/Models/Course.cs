using System;

namespace CourseRegistration.Models
{
    public class Course: IComparable<Course>
    {
        public string Name {get;set;}
        public string Title {get;set;}
        public double Credits {get;set;}
        public string Description {get;set;}
        public string Department {get; set;}

        public override String ToString() {
            return $"{Name} ({Department}): {Title} ({Credits})\n{Description}\n";

        }
        public int CompareTo(Course other) {
            return this.Name.CompareTo(other.Name);
        }
        public override bool Equals(object obj)
        {
            Course c = obj as Course;
            
            return !Object.ReferenceEquals(null, c)
            && String.Equals(Name, c.Name);

        }

        
        public override int GetHashCode()
      {
         return this.GetHashCode(); 
      }
    }
}
