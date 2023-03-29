using System;
using System.Collections.Generic;
using CourseRegistration.Repository;
using CourseRegistration.Models;
using System.Linq;

namespace CourseRegistration.Services
{
    public class CourseServices : ICourseServices
    {
       // private CourseRepository repo = new CourseRepository();
        private readonly ICourseRepository _repo;

        public CourseServices(ICourseRepository courseRepo) {
         _repo=courseRepo;
        }

       public List<CourseOffering> GetOfferingsByGoalIdAndSemester(String theGoalId, String semester) {
          //finish this method during the tutorial 
         List<CoreGoal> theGoals = _repo.Goals;
         List <CourseOffering> theOfferings = _repo.Offerings;
         CoreGoal theGoal=null;
         foreach (CoreGoal cg in theGoals) {
            if(cg.Id.Equals(theGoalId)){
               theGoal=cg;
               break;
            }
            if (theGoal==null) throw new Exception ("Didn't find the goal");}
      
       //search list of courses, then for each course, search offerings
      List<CourseOffering> courseOfferingsThatMeetGoal = new List<CourseOffering>();
                  
      foreach(CourseOffering c in theOfferings) {
         if(c.Semester.Equals(semester) 
            && theGoal.Courses.Contains(c.TheCourse) ) 
         {
            courseOfferingsThatMeetGoal.Add(c);
         }
      
      }
      return courseOfferingsThatMeetGoal;}

        
        //Add more service functions here, as needed, for the project

        /* As a student, I want to see all available courses so that I know what my options are  */
      public List<Course> GetCourses () {
         List<Course> allCourses = _repo.GetAllCourses().ToList<Course>();
         return allCourses;
        }
           
          

        /* As a student, I want to see all course offerings by semester, so that I can choose from what's
           available to register for next semester */
         public List<CourseOffering> GetCourseOfferingsBySemester(string semester){
            List<CourseOffering> theOfferings = _repo.Offerings;
          
            List<CourseOffering> semesterOffering = new List<CourseOffering>();
                        
            foreach(CourseOffering c in theOfferings) {
               if(c.Semester.Equals(semester)) {
                  semesterOffering.Add(c);
               }
            }
            return semesterOffering;}

         public List<CourseOffering> GetOneCourseOfferingsBySemester(string name, string semester){
            List<CourseOffering> theOfferings = _repo.Offerings;
          
            List<CourseOffering> courseSemesterOffering = new List<CourseOffering>();
                        
            foreach(CourseOffering c in theOfferings) {
               if(c.Semester.Equals(semester) && c.TheCourse.Name.Equals(name)) {
                  courseSemesterOffering.Add(c);
               }
            }
            return courseSemesterOffering;}
         /*As a student I want to see all course offerings by semester and department so that I can 
        choose major courses to register for */
         public List<CourseOffering> GetCourseOfferingsBySemesterAndDept(string semester, string department){
            List<CourseOffering> theOfferings = _repo.Offerings;
          
            List<CourseOffering> semesterAndDeptOffering = new List<CourseOffering>();
            List<CourseOffering> semesterOnly = new List<CourseOffering>();       
            foreach(CourseOffering a in theOfferings) {
               if(a.Semester.Equals(semester)){
                     semesterOnly.Add(a);}
               }
            foreach(CourseOffering b in semesterOnly){
               if(b.Department.Equals(department)){
                  semesterAndDeptOffering.Add(b);}
            }
            return semesterAndDeptOffering;
            }

         public Course GetCourseByName(string name){
            Course c = _repo.GetCourseByName(name);
            return c;
         }
       

         //Course Repo updates
         public Course AddCourse (Course newCourse){
            return _repo.InsertCourse(newCourse);

         }

         public void DeleteCourseByName (string c){
            _repo.DeleteCourseByName(c);
         }

         public void UpdateCourseByName (string name, Course update){
            _repo.UpdateCourseByName(name, update);
         }


         //Core goals CRUD ------------

         public List<CoreGoal> GetAllCoreGoals () {
         List<CoreGoal> allGoals = _repo.GetAllCoreGoals().ToList<CoreGoal>();
         return allGoals;
        }
        public List<CoreGoalCourses> AllGoalCourses(String GoalId){
         List<CoreGoalCourses> allGoals = _repo.GetCoursesForCoreGoalById(GoalId).ToList<CoreGoalCourses>();
         return allGoals;
        }

        public CoreGoal GetCoreGoalById(string Id){
            CoreGoal g = _repo.GetCoreGoalById(Id);
            return g;
         }

         public CoreGoal GetCoreGoalWithCoursesById(string Id){
            CoreGoal g = _repo.GetCoreGoalWithCoursesById(Id);
            return g;
         }
         public CoreGoal InsertCoreGoal(CoreGoal newGoal){
            return _repo.InsertCoreGoal(newGoal);

         }

         public bool UpdateCoreGoal(string Id, CoreGoal newGoal){
           return _repo.UpdateCoreGoal(Id, newGoal);
         }

         public bool AddCourseToCoreGoal(string id, CoreGoalCourses newCourse){
           return _repo.AddCourseToCoreGoal(id, newCourse);
         }

         public bool DeleteCoreGoal(String id){
           return _repo.DeleteCoreGoal(id);
         }
    }
}