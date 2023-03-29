using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CourseRegistration.Models;
using CourseRegistration.Services;

namespace CourseRegistration.Controllers
{
    [ApiController]
    
    [Route("coregoals")]
    public class CoreGoalController : Controller
    {
        private readonly ICourseServices _courseServices;
        public CoreGoalController(ICourseServices courseServices){
            _courseServices = courseServices;
        }
        
    

        [HttpGet (Name="GetAllCoreGoals")]
        public IActionResult GetAllCoreGoals()
        {
            
                IEnumerable<CoreGoal> list =_courseServices.GetAllCoreGoals();

                if (list !=null) return Ok(list);
                else return BadRequest();
            
        }

        [HttpGet("{GoalId}/courses", Name="CoreCourses")]
        public IActionResult AllCoreGoalCourses(string GoalId)
        {
            
                IEnumerable<CoreGoalCourses> list =_courseServices.AllGoalCourses(GoalId);

                if (list !=null) return Ok(list);
                else return BadRequest();
            
        }

        

    [HttpGet("{Id}", Name="GetGoalWithCourses")]
        public IActionResult GetCoreGoalWithCoursesById(string Id)
        {   
           
                try{
                    CoreGoal c = _courseServices.GetCoreGoalWithCoursesById(Id);
                    if (c !=null) return Ok(c);
                    else return BadRequest();
                }
                catch (Exception){
                    return StatusCode(500, "Internal Server Error");
                }
                
        }
    [HttpPost]
    public IActionResult AddNewCoreGoal(CoreGoal g) {
            try{
                 
                CoreGoal newGoal = _courseServices.InsertCoreGoal(g);
                
                if (g !=null) return CreatedAtRoute("GetCourse", new {name=newGoal.Name}, newGoal);
                else return BadRequest();
                
            }catch(Exception ex){
                return BadRequest(ex);
            }

        }

    

    [HttpPut ("{Id}")]
        public IActionResult UpdateGoal(string Id, CoreGoal newGoal){
            try{
                bool outcome= _courseServices.UpdateCoreGoal(Id, newGoal);
                if (!outcome){
                    return StatusCode(404, "No Value to update");
                }
                else{
                    return NoContent();
                }
                
            }catch(Exception ex){
                return BadRequest(ex);
            }
        }
    
    [HttpPost("{Id}/courses")]
    public IActionResult AddCourseToCoreGoal(string id, CoreGoalCourses newCourse) {
            try{
                 
                 bool outcome= _courseServices.AddCourseToCoreGoal(id, newCourse);
                
                if (!outcome){
                    return StatusCode(404, "Did not match an established goal");
                }
                else{
                    return NoContent();
                }
                
            }catch(Exception){
                return StatusCode(404, "Did not match an established goal");
            }

        }

        [HttpDelete("{Id}")]
    public IActionResult DeleteCoreGoal(String id) {
            try{
                 
                 bool outcome= _courseServices.DeleteCoreGoal(id);
                
                if (!outcome){
                    return StatusCode(404, "Did not match an established goal");
                }
                else{
                    return NoContent();
                }
                
            }catch(Exception ex){
                return BadRequest(ex);
            }

        }
    
    
    
    
    }
    
}
