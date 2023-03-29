using System;
using System.Collections.Generic;
using System.Text;

namespace CourseRegistration.Models
{
    public class CoreGoal: IComparable<CoreGoal>
    {   //old
        public string Id {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        
        public List<Course> Courses {get;set;}

        public List<CoreGoalCourses> GoalCourses {get;set;}


        /* public override String ToString() {
            StringBuilder courseList = new StringBuilder();
            foreach(Course c in Courses) {
                courseList.Append(c.ToString()+",");
            }
            return $"{Id}-{Name}: {Description} ()\n{courseList.ToString()}\n";

        } */
        public int CompareTo(CoreGoal other) {
            return this.Name.CompareTo(other.Name);
        }

        
    }

    public class CoreGoalCourses
    {   //old
        public string GoalId {get;set;}
        public string CourseName {get;set;}
            

        
    }
}
