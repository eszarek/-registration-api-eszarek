using System;
using System.Collections.Generic;
using CourseRegistration.Models;
using CourseRegistration.Controllers;
using CourseRegistration.Services;
using MySql.Data.MySqlClient;
using System.Linq;


namespace CourseRegistration.Repository{
    public interface ICourseRepository{
        //obsolete
        public List<Course> Courses {get;set;}
        public List<CoreGoal> Goals {get;set;}
        public List<CourseOffering> Offerings {get;set;}

        //new

        public IEnumerable<Course> GetAllCourses();

        public Course GetCourseByName(string name);

        public Course InsertCourse(Course newCourse);

        public void DeleteCourseByName(String name);
        public void UpdateCourseByName(string name, Course update);
        //------------------------------------------------------------------------------------------//
        public IEnumerable<CoreGoal> GetAllCoreGoals();
        public IEnumerable<CoreGoalCourses> GetCoursesForCoreGoalById(string GoalId);
        public CoreGoal GetCoreGoalById(string Id);
        public CoreGoal GetCoreGoalWithCoursesById(string Id);

        public CoreGoal InsertCoreGoal(CoreGoal newGoal);
        public bool UpdateCoreGoal(string Id, CoreGoal newGoal);

        public bool AddCourseToCoreGoal(string id, CoreGoalCourses newCourse);

        public bool DeleteCoreGoal(String id);
        
    }
     public class CourseRepository: ICourseRepository {
        public List<Course> Courses {get;set;}
        public List<CoreGoal> Goals {get;set;}
        public List<CourseOffering> Offerings {get;set;}

        private MySqlConnection _connection;

        
        

        //Add more data as needed 
        public CourseRepository() {
            string connectionString = "server=localhost;userid=csci330user;password=csci330pass;database=courseregistration";
            _connection = new MySqlConnection(connectionString);
            _connection.Open();

        }//end constructor

        ~CourseRepository(){
            _connection.Close();
        }

        public IEnumerable<Course> GetAllCourses(){
            var statement = "SELECT * FROM Courses";
            var command = new MySqlCommand (statement, _connection);
            var results = command.ExecuteReader();

            List<Course> newList = new List<Course>(25);

            while (results.Read()){
                Course c = new Course{
                    Name = (String)results[0],
                    Title = (string)results[1],
                    Credits = (Double)results[2],
                    Description = (string)results[3],
                    Department = (string)results[4]
                };
                newList.Add(c);
            }
            results.Close();
            return newList;
        }

        public Course GetCourseByName(string name){
            var statement = "SELECT * FROM Courses where Name=@newName";
            var command = new MySqlCommand (statement, _connection);
            command.Parameters.AddWithValue("@newname",name);
            var results = command.ExecuteReader();

            

            while (results.Read()){
                Course c = new Course{
                    Name = (String)results[0],
                    Title = (string)results[1],
                    Credits = (Double)results[2],
                    Description = (string)results[3],
                    Department = (string)results[4]
                };
                results.Close();
                return c;
            }
            results.Close();
            return null;
            
        }

        public Course InsertCourse(Course newCourse){
            
            var statement = "INSERT into Courses (Name, Title, Credits, Description, Department) VALUES (@newName, @newTitle, @newCredits, @newDesc, @newDept)";
            var command = new MySqlCommand (statement, _connection);
            command.Parameters.AddWithValue("@newName",newCourse.Name);
            command.Parameters.AddWithValue("@newTitle",newCourse.Title);
            command.Parameters.AddWithValue("@newCredits",newCourse.Credits);
            command.Parameters.AddWithValue("@newDesc",newCourse.Description);
            command.Parameters.AddWithValue("@newDEpt",newCourse.Department);
           
            int result = command.ExecuteNonQuery();
            if (result ==1){
                return newCourse;
            }
            else{
                return null;
                
            }
        }

         public void DeleteCourseByName(String name){
            try{
                var statement = "DELETE FROM courses WHERE Name=@deleteName";
                var command = new MySqlCommand (statement, _connection);
                command.Parameters.AddWithValue("@deleteName",name);

                int result = command.ExecuteNonQuery();
                Console.WriteLine(result);
                 
            } catch (ArgumentException){
                throw new ArgumentException ("Course not found");
            }           
        }

        public void UpdateCourseByName(string name, Course update){
            
            var statement = "UPDATE courses SET Name=@newName, Title=@newTitle, Credits=@newCredits, Description=@newDesc, Department=@newDept WHERE Name=@newName";
            var command = new MySqlCommand (statement, _connection);
            command.Parameters.AddWithValue("@newName",update.Name);
            command.Parameters.AddWithValue("@newTitle",update.Title);
            command.Parameters.AddWithValue("@newCredits",update.Credits);
            command.Parameters.AddWithValue("@newDesc",update.Description);
            command.Parameters.AddWithValue("@newDEpt",update.Department);
           
            int result = command.ExecuteNonQuery();
            Console.WriteLine(result);
            
            
        }

        //---------------------------------i-------------------------------------------//
         public IEnumerable<CoreGoal> GetAllCoreGoals(){
            var statement = "SELECT * FROM CoreGoals";
            var command = new MySqlCommand (statement, _connection);
            var results = command.ExecuteReader();

            List<CoreGoal> newList = new List<CoreGoal>(25);

            while (results.Read()){
                CoreGoal c = new CoreGoal{
                    Id = (String)results[0],
                    Name = (string)results[1],
                    Description = (string)results[2]
                    
                };
                newList.Add(c);
            }
            results.Close();
            return newList;
        }
        //---------------------------------ii-------------------------------------------//
          public CoreGoal GetCoreGoalById(string Id){
            var statement = "SELECT * FROM coregoals WHERE Id=@findId";
            var command = new MySqlCommand (statement, _connection);
            command.Parameters.AddWithValue("@findId",Id);
            var results = command.ExecuteReader();

            while (results.Read()){
                CoreGoal c = new CoreGoal{
                    Id = (string)results[0],
                    Name = (string)results[1],
                    Description = (string)results[2]
                };
                results.Close();
                return c;
            }
            results.Close();
            return null;
        }
        //---------------------------------iii-------------------------------------------//
         public CoreGoal GetCoreGoalWithCoursesById(string Id){
            List<CoreGoalCourses> GClist= new List<CoreGoalCourses> (GetCoursesForCoreGoalById(Id));
            var statement = "SELECT GoalId,  Coregoals.Name as `Goal Name`, Coregoals.Description, CourseName FROM coregoalcourses INNER JOIN coregoals ON coregoalcourses.GoalId = coregoals.Id WHERE GoalId = @findID";
            var command = new MySqlCommand (statement, _connection);
            command.Parameters.AddWithValue("@findId",Id);
            var results = command.ExecuteReader();

            List<CoreGoalCourses> newList = new List<CoreGoalCourses>(25);

            while (results.Read()){
                CoreGoalCourses c = new CoreGoalCourses{
                    GoalId = (String)results[0],
                    CourseName = (string)results[1]                                  
                };
                newList.Add(c);
                CoreGoal g = new CoreGoal{
                    Id = (String)results[0],
                    Name = (string)results[1],
                    Description = (string)results[2],
                    GoalCourses=GClist
                };
                results.Close();
                return g;

            }
            results.Close();
            return null;
        }
        //---------------------------------iv-------------------------------------------//

         public IEnumerable<CoreGoalCourses> GetCoursesForCoreGoalById(String GoalId){
            var statement = "SELECT * FROM CoreGoalCourses WHERE GoalId=@findId";
            var command = new MySqlCommand (statement, _connection);
            command.Parameters.AddWithValue("@findId",GoalId);
            var results = command.ExecuteReader();

            List<CoreGoalCourses> newList = new List<CoreGoalCourses>(25);

            while (results.Read()){
                CoreGoalCourses c = new CoreGoalCourses{
                    GoalId = (String)results[0],
                    CourseName = (string)results[1],
                    
                    
                };
                newList.Add(c);
            }
            results.Close();
            return newList;
        }

        //---------------------------------v-------------------------------------------//
        public CoreGoal InsertCoreGoal(CoreGoal newGoal){
            
            var statement = "INSERT INTO coregoals (Id, Name, Description) VALUES (@goalName, @GoalCourseName, @GDescript)";

            var command = new MySqlCommand (statement, _connection);
            command.Parameters.AddWithValue("@goalName",newGoal.Id);
            command.Parameters.AddWithValue("@GoalCourseName",newGoal.Name);
            command.Parameters.AddWithValue("@GDescript",newGoal.Description);
            
            
           
            int result = command.ExecuteNonQuery();
            if (result ==1){
                return newGoal;
            }
            else{
                return null;
                
            }
        }
        //---------------------------------vi-------------------------------------------//
        public bool UpdateCoreGoal(string Id, CoreGoal newGoal){
            
            var statement = "UPDATE coregoals SET Name=@GoalName, Description=@GDescript WHERE id=@goalId";
            var command = new MySqlCommand (statement, _connection);
            command.Parameters.AddWithValue("@goalId",Id);
            command.Parameters.AddWithValue("@GoalName",newGoal.Name);
            command.Parameters.AddWithValue("@GDescript",newGoal.Description);
           
            int result = command.ExecuteNonQuery();
            if (result <= 0){
                return false;
            }
            else {
                return true;}
            
            
        }
        //---------------------------------vii-------------------------------------------//
        public bool AddCourseToCoreGoal(string id, CoreGoalCourses newCourse){
            
            var statement = "INSERT INTO coregoalcourses (goalId, CourseName) VALUES (@goalId, @NewCourseName)";

            var command = new MySqlCommand (statement, _connection);
            command.Parameters.AddWithValue("@goalId",id);
            command.Parameters.AddWithValue("@newCourseName",newCourse.CourseName);
            
            
            
           
            int result = command.ExecuteNonQuery();
            if (result <= 0){
                return false;
            }
            else {
                return true;}
        }

         public bool DeleteCoreGoal(String id){
            
            var statement = "DELETE FROM coregoals WHERE  id=@deleteGoal";
            var command = new MySqlCommand (statement, _connection);
            command.Parameters.AddWithValue("@deleteGoal",id);

                int result = command.ExecuteNonQuery();
            if (result <= 0){
                return false;
            }
            else {
                return true;}   
        }





     }
}