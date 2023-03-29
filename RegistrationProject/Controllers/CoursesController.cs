using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CourseRegistration.Models;
using CourseRegistration.Services;
using CourseRegistration.Repository;

namespace CourseRegistration.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    [Route("courses")]
    public class CoursesController : ControllerBase
    {
        //Course service interface object
        private ICourseServices _courseServices;
        public CoursesController(ICourseServices courseServices)
        {
            //constructor set the value of private data member to be the value of the entered parameter in course services.
            _courseServices = courseServices;
            
            //dependency injection calls course controller. Course controller depends on a course services object inside its method. 
        }

    
    
        //Course service interface object
        //add repo methods for post delete, repo owns the list. adding in the repo file. functions to be called byc ourse services, which in turn gets called by course controller.
        

        /* private ICourseRepository _courseRepository;
        public RepositoryController(ICourseRepository CourseRepository)
        {
            //constructor set the value of private data member to be the value of the entered parameter in course services.
            _courseRepository = CourseRepository;
            
            //dependency injection calls course controller. Course controller depends on a course services object inside its method. 
        } */
        
    

        [HttpGet (Name="GetAllCourses")]
        public IActionResult GetAllCourses()
        {
            
                IEnumerable<Course> list =_courseServices.GetCourses();

                if (list !=null) return Ok(list);
                else return BadRequest();
            
        }


        [HttpGet("{name}", Name="GetCourse")]
        public IActionResult GetCourseByName(string name)
        {   
           
                try{
                    Course c = _courseServices.GetCourseByName(name);
                    if (c !=null) return Ok(c);
                    else return BadRequest();
                }
                catch (Exception){
                    return StatusCode(500, "Internal Server Error");
                }
                
        }

        [HttpGet("{name}/offerings")]
        public IActionResult GetCourseBySemester(string name,string semester){
            try{
                IEnumerable<CourseOffering> list=_courseServices.GetOneCourseOfferingsBySemester(name, semester);
               return Ok(list);
            } catch (NullReferenceException){
                return StatusCode(204);
            }
        
        }

        [HttpGet("search")]
        public IActionResult GetCourseByDept(string dept)
        {   
           
                IEnumerable<Course> list =_courseServices.GetCourses();
                
                if (list !=null) foreach (Course m in list){
                    if(m.Department.Equals(dept))
            
                    return Ok(m);
                  
                };  
                return StatusCode(404);
                
        }

        [HttpPost]
        public IActionResult AddCourse(Course c) {
            try{
                 
                Course returnedCourse = _courseServices.AddCourse(c);
                
                if (c !=null) return CreatedAtRoute("GetCourse", new {name=returnedCourse.Name}, returnedCourse);
                else return BadRequest();
                
            }catch(Exception){
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut ("{name}")]
        public IActionResult UpdateCourseByName(string name, Course update){
            try{
                _courseServices.UpdateCourseByName(name, update);
                return NoContent();

            }catch(Exception){
                return StatusCode(500,"Internal Server Error");
            }
        }

        [HttpDelete("{name}")]
        public IActionResult DeleteCourseByName (string name){
            try{
                _courseServices.DeleteCourseByName(name);
                return StatusCode(202);

            }catch(ArgumentException){
                return StatusCode(409);
            }
        }

        


    
    }
}
