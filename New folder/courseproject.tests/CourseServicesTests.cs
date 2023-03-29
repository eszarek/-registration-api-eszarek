using System;
using Xunit;
using cs330_proj1;
using System.Collections.Generic;
using Moq;
using System.Linq;

namespace courseproject.tests
{
    public class CouseServicesTests
    {
        [Fact]
        public void GetOfferingsByGoalIdAndSemester_GoalNotFound_ExceptionThrown()
        {
            // Arrange
            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(GetTestCourses());
            mockRepository.Setup(m => m.Goals).Returns(new List<CoreGoal>(){
            new CoreGoal() {
                Courses = GetTestCourses(),
                Description = "test",
                Id = "CG1",
                Name = "English Literacy"
            }
            });

            mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = GetTestCourses().First()
                }
            });

            var courseServices = new CourseServices(mockRepository.Object);
            var goalId = "CG5";
            var semester = "Spring 2021";

            // Act/Assert
            Assert.Throws<Exception>(() => courseServices.GetOfferingsByGoalIdAndSemester(goalId, semester));
        }


        [Fact]
        public void GetOfferingsByGoalIdAndSemester_GoalIsFoundAndOneCourseOfferingIsInSemester_OfferingIsReturned()
        {
            // Arrange
            var course = new Course() {
                Name= "ARTD 201",
                Title="graphic design",
                Credits=3.0,
                Description="graphic design descr"
            };

            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(new List<Course> {
                course});

            mockRepository.Setup(m => m.Goals).Returns(new List<CoreGoal>(){
            new CoreGoal() {
                Courses = GetTestCourses(),
                Description = "test",
                Id = "CG1",
                Name = "English Literacy"
            }
            });

            mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = course
                }
            });

            
            var goalId = "CG1";
            var semester = "Spring 2021";
            var courseServices = new CourseServices(mockRepository.Object);

            //Act
            var result = courseServices.GetOfferingsByGoalIdAndSemester(goalId, semester);

            // Assert
            var itemInList = Assert.Single(result);
            // Assert.Equal(2, result.Count());
            Assert.Equal(semester, itemInList.Semester);
            Assert.Equal(course.Name, itemInList.TheCourse.Name);
            
           
        }
        [Fact]
        public void GetOfferingsByGoalIdAndSemester_GoalIsFoundAndMultipleCourseOfferingsAreInSemester_OfferingsAreReturned()
        {
             // Arrange
            var course = new Course() {
                Name= "ARTD 201",
                Title="graphic design",
                Credits=3.0,
                Description="graphic design descr"
            };

            var course2 = new Course() {
                Name= "ARTD 205",
                Title="Studio Art",
                Credits=3.0,
                Description="Studio art descr"
            };

            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(new List<Course> {
                course, course2});

            mockRepository.Setup(m => m.Goals).Returns(new List<CoreGoal>(){
            new CoreGoal() {
                Courses = GetTestCourses(),
                Description = "test",
                Id = "CG1",
                Name = "English Literacy"
            }
            });

            mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = course
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = course2
                }
            });

            
            var goalId = "CG1";
            var semester = "Spring 2021";
            var courseServices = new CourseServices(mockRepository.Object);

            //Act
            var result = courseServices.GetOfferingsByGoalIdAndSemester(goalId, semester);

            // Assert
            
            Assert.Equal(2, result.Count());
            
        }

        
        [Fact]
        public void GetOfferingsByGoalIdAndSemester_GoalIsFoundAndNoCourseOfferingIsInSemester_EmptyListIsReturned()
        {
            // Arrange
            var course = new Course() {
                Name= "ARTD 201",
                Title="graphic design",
                Credits=3.0,
                Description="graphic design descr"
            };

            var course2 = new Course() {
                Name= "ARTD 205",
                Title="Studio Art",
                Credits=3.0,
                Description="Studio art descr"
            };

            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(new List<Course> {
                course, course2});

            mockRepository.Setup(m => m.Goals).Returns(new List<CoreGoal>(){
            new CoreGoal() {
                Courses = GetTestCourses(),
                Description = "test",
                Id = "CG1",
                Name = "English Literacy"
            }
            });

            mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                
            });

            
            var goalId = "CG1";
            var semester = "Spring 2021";
            var courseServices = new CourseServices(mockRepository.Object);

            //Act
            var result = courseServices.GetOfferingsByGoalIdAndSemester(goalId, semester);

            // Assert
            Assert.Empty(result);
        }

        // user story 2 As a student, I want to see all available courses so that I know what my options are.//
        [Fact]
        public void GetCourses_NoCoursesAreFound_ReturnsEmptyList()
        {
            // Given
            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(GetTestCourses());
            var courseServices = new CourseServices(mockRepository.Object);
            // When
            var resultAllCourses= courseServices.GetCourses();
            // Then
            Assert.Equal(3, resultAllCourses.Count());
            Assert.Contains(resultAllCourses, item => item.Name is "ARTD 201");
            Assert.Contains(resultAllCourses, item => item.Name is "ARTD 205");
            Assert.Contains(resultAllCourses, item => item.Name is "ARTS 101");
        }
        [Fact]
        public void GetCourses_AllCoursesAreFound_returnsAListOfAllCourses()
        {
            // Given
             var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(new List<Course> {});
            var courseServices = new CourseServices(mockRepository.Object);
            // When
            var resultAllCourses= courseServices.GetCourses();
            // Then
            Assert.Empty(resultAllCourses);
        }
    

    // user story 3:
    //* As a student, I want to see all course offerings by semester, so that I can choose from what's
    //available to register for next semester */
    [Fact]
    public void GetCourseOfferingsBySemester_SemesterIsSpring2021_Returns3Courses()
    {
       var course = new Course() {
                Name= "ARTD 201",
                Title="graphic design",
                Credits=3.0,
                Description="graphic design descr"
            };

            var course2 = new Course() {
                Name= "ARTD 205",
                Title="Studio Art",
                Credits=3.0,
                Description="Studio art descr"
            };
            var course3 = new Course() {
                Name= "ARTD 203",
                Title="Art History",
                Credits=3.0,
                Description="Art History descr"
            };

            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(new List<Course> {
                course, course2, course3});

            mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = course
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = course2
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = course3
                }
            });
            var semester = "Spring 2021";
            var courseServices = new CourseServices(mockRepository.Object);

            //Act
            var semesterResult = courseServices.GetCourseOfferingsBySemester(semester);

            // Assert
            
            Assert.Equal(3, semesterResult.Count());
            Assert.Contains(semesterResult, item => item.TheCourse.Name is "ARTD 201");
            Assert.Contains(semesterResult, item => item.TheCourse.Name is "ARTD 205");
            Assert.Contains(semesterResult, item => item.TheCourse.Name is "ARTD 203");
    }

    [Fact]
    public void GetCourseOfferingsBySemester_SemesterIsSpring2022_ReturnsEmptyList()
    {
       var course = new Course() {
                Name= "ARTD 201",
                Title="graphic design",
                Credits=3.0,
                Description="graphic design descr"
            };

            var course2 = new Course() {
                Name= "ARTD 205",
                Title="Studio Art",
                Credits=3.0,
                Description="Studio art descr"
            };
            var course3 = new Course() {
                Name= "ARTD 203",
                Title="Art History",
                Credits=3.0,
                Description="Art History descr"
            };

            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(new List<Course> {
                course, course2, course3});

            mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = course
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = course2
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = course3
                }
            });
            var semester = "Spring 2022";
            var courseServices = new CourseServices(mockRepository.Object);

            //Act
            var semesterResult = courseServices.GetCourseOfferingsBySemester(semester);

            // Assert
            
            Assert.Empty(semesterResult);
        }
        /* user story 4: As a student I want to see all course offerings by semester and department so that I can 
        choose major courses to register for */
        [Fact]
        public void GetCourseOfferingsBySemesterAndDept_SemesterisSpring21andDepartmentIsArt_TwpCoursesReturned()
        {
             // Arrange
            var course = new Course() {
                Name= "ARTD 201",
                Title="graphic design",
                Credits=3.0,
                Description="graphic design descr",
                Department="Art"
            };

            var course2 = new Course() {
                Name= "ARTD 205",
                Title="Studio Art",
                Credits=3.0,
                Description="Studio art descr",
                Department="Art"
            };

            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(new List<Course> {
                course, course2});

            mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = course,
                    Department= "Art"
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = course2,
                    Department="Art"
                }
                });

            
            var department="Art";
            var semester = "Spring 2021";
            var courseServices = new CourseServices(mockRepository.Object);

            //Act
            var result = courseServices.GetCourseOfferingsBySemesterAndDept(semester, department);

            // Assert
            
            Assert.Equal(2, result.Count());
            Assert.Contains(result, item => item.TheCourse.Name is "ARTD 201");
            Assert.Contains(result, item => item.TheCourse.Name is "ARTD 205");
            
        }
        [Fact]

        public void GetCourseOfferingsBySemesterAndDept_SemesterisSpring22andDepartmentIsArt_EmptyListReturned()
        {
             // Arrange
            var course = new Course() {
                Name= "ARTD 201",
                Title="graphic design",
                Credits=3.0,
                Description="graphic design descr",
                Department="Art"
            };

            var course2 = new Course() {
                Name= "ARTD 205",
                Title="Studio Art",
                Credits=3.0,
                Description="Studio art descr",
                Department="Art"
            };

            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(new List<Course> {
                course, course2});

            mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = course,
                    Department= "Art"
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = course2,
                    Department="Art"
                }
                });

            
            var department="Art";
            var semester = "Spring 2022";
            var courseServices = new CourseServices(mockRepository.Object);

            //Act
            var result = courseServices.GetCourseOfferingsBySemesterAndDept(semester, department);

            // Assert
            
            Assert.Empty(result);
            
        }

        private List<Course> GetTestCourses()
        {
            return new List<Course>(){
            new Course() {
                Name="ARTD 201",
                Title="graphic design",
                Credits=3.0,
                Description="graphic design descr",
                Department = "Art"

            },
            new Course() {
                Name="ARTS 101",
                Title="art studio",
                Credits=3.0,
                Description="studio descr",
                Department = "Art"


            },
            new Course() {
                Name= "ARTD 205",
                Title="Studio Art",
                Credits=3.0,
                Description="Studio art descr",
                Department = "Art"

            }
            };
        }

    }
}

