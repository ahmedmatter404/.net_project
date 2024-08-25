using AspTask.Models;
using AspTask.ViewModels;
using AspTask.Models;
using AspTask.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspTask.Controllers
{
    public class InstructorController : Controller
    {
        private readonly ItiContext itiContext;
        public InstructorController(ItiContext context)
        {
            itiContext = context;
        }

        public IActionResult Index()
        {
            ICollection<Instructor> Instructors = itiContext.Instructors.Take(9).ToList();
            return View(Instructors);
        }
        public IActionResult start()
        {
           
            return View();
        }
        public IActionResult std_Index()
        {
            ICollection<Student> Students = itiContext.Students.Take(9).ToList();
            return View(Students);
        }
        public IActionResult Details(int id)
        {
            var instructor = itiContext.Instructors.Where(s => s.InsId == id).Include(s => s.Dept).FirstOrDefault();

            InstructorVM insVM = new InstructorVM()
            {

                ID = instructor.InsId,
                Name = instructor.InsName,
                Degree = instructor.InsDegree,

            };
            return View(insVM);
        }
        public IActionResult Create()
        {
            // return the create view to enter data
            return View();
        }
        public IActionResult Save(int id, string name, string degree)
        {
            // create new instructor using context
            Instructor ins = new Instructor();
            {
                ins.InsId = id;
                ins.InsName = name;
                ins.InsDegree = degree;
                
            }
            itiContext.Instructors.Add(ins);
            itiContext.SaveChanges();
            TempData["SuccessMessage"] = "Instructor created successfully!";
            return RedirectToAction("index");
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            // 1- get student by id
            var instructor = itiContext.Instructors.Where(s => s.InsId == id).FirstOrDefault();
            return View(instructor);
        }
        [HttpPost]
        public IActionResult Edit(Instructor instructor)
        {
            var currentInstructor = itiContext.Instructors.Where(s => s.InsId == instructor.InsId).FirstOrDefault();
            currentInstructor.InsId = instructor.InsId;
            currentInstructor.InsName = instructor.InsName;
            currentInstructor.InsDegree = instructor.InsDegree;
           
            //itiContext.Students.Update(student);
            itiContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var currentInstructor = itiContext.Instructors.Where(s => s.InsId == id).FirstOrDefault();

            if (currentInstructor != null)
            {
                // get stud_courses for this student 
                var instructorCourses = itiContext.InsCourses.Where(c => c.InsId == id).ToList();

                // remove stud_courses for this student
                itiContext.InsCourses.RemoveRange(instructorCourses);
                itiContext.SaveChanges();

                itiContext.Instructors.Remove(currentInstructor);

                itiContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
       
        public IActionResult CreateUser(user user1)
        {
            if (ModelState.IsValid)
            {
                // create new user
            }
            return View();
        }


    }
}
