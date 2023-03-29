using System;
using System.Collections.Generic;
using CourseRegistration.Repository;
using CourseRegistration.Models;

namespace CourseRegistration.Services{
    public interface ICourseServices {
      
            public List<CourseOffering> GetOfferingsByGoalIdAndSemester(String theGoalId, String semester);

            public List<Course> GetCourses ();

            public Course GetCourseByName(string name);

            /* As a student, I want to see all course offerings by semester, so that I can choose from what's
            available to register for next semester */
            public List<CourseOffering> GetCourseOfferingsBySemester(string semester);
            
            /*As a student I want to see all course offerings by semester and department so that I can 
            choose major courses to register for */
            public List<CourseOffering> GetCourseOfferingsBySemesterAndDept(string semester, string department);
            public List<CourseOffering> GetOneCourseOfferingsBySemester(string name, string semester);

            public Course AddCourse (Course newCourse);

             public void DeleteCourseByName (string c);

            public void UpdateCourseByName (string name, Course update);

            public List<CoreGoal> GetAllCoreGoals ();
            public List<CoreGoalCourses> AllGoalCourses (string GoalId);
            public CoreGoal GetCoreGoalById(string Id);
            public CoreGoal GetCoreGoalWithCoursesById(string Id);
            public CoreGoal InsertCoreGoal(CoreGoal newGoal);
            public bool UpdateCoreGoal(string Id, CoreGoal newGoal);

            public bool AddCourseToCoreGoal(string id, CoreGoalCourses newCourse);

            public bool DeleteCoreGoal(String id);
        }
}