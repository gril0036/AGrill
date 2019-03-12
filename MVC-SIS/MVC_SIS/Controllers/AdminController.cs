﻿using Exercises.Models.Data;
using Exercises.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exercises.Controllers
{
    public class AdminController : Controller
    {

        [HttpGet]
        public ActionResult Majors()
        {
            var model = MajorRepository.GetAll();
            return View(model.ToList());
        }

        [HttpGet]
        public ActionResult AddMajor()
        {
            return View(new Major());
        }

        [HttpPost]
        public ActionResult AddMajor(Major major)
        {
            if (string.IsNullOrEmpty(major.MajorName))
            {
                ModelState.AddModelError("MajorName", "Please enter a major name");
                return View("AddMajor", major);
            }

            MajorRepository.Add(major.MajorName);
            return RedirectToAction("Majors");
        }

        [HttpGet]
        public ActionResult EditMajor(int id)
        {
            var major = MajorRepository.Get(id);
            return View(major);
        }

        [HttpPost]
        public ActionResult EditMajor(Major major)
        {
            if (string.IsNullOrEmpty(major.MajorName))
            {
                ModelState.AddModelError("MajorName", "Please enter a major name");
                return View("EditMajor", major);
            }

            MajorRepository.Edit(major);
            return RedirectToAction("Majors");
        }

        [HttpGet]
        public ActionResult DeleteMajor(int id)
        {
            var major = MajorRepository.Get(id);
            return View(major);
        }

        [HttpPost]
        public ActionResult DeleteMajor(Major major)
        {
            MajorRepository.Delete(major.MajorId);
            return RedirectToAction("Majors");
        }

        [HttpGet]
        public ActionResult States()
        {
            var model = StateRepository.GetAll();
            return View(model.ToList());
        }

        [HttpGet]
        public ActionResult AddState()
        {
            return View(new State());
        }

        [HttpPost]
        public ActionResult AddState(State state)
        {                               
            if (string.IsNullOrEmpty(state.StateAbbreviation) || state.StateAbbreviation.Length != 2)
            {
                ModelState.AddModelError("StateAbbreviation", "Please enter a 2 letter state abbreviation");

            }
            else
            {
                IEnumerable<State> states = StateRepository.GetAll();
                if (states.Any(s => s.StateAbbreviation == state.StateAbbreviation.ToUpper()))
                {
                    ModelState.AddModelError("StateAbbreviation", "State abbreviation already exists, please enter another abbreviation");
                }
            }
            
            if (string.IsNullOrEmpty(state.StateName))
            {
                ModelState.AddModelError("StateName", "Please enter a state name");                
            }

            if (ModelState.IsValid)
            {
                state.StateAbbreviation = state.StateAbbreviation.ToUpper();
                StateRepository.Add(state);
                return RedirectToAction("States");
            }
            else
            {
                return View("AddState", state);
            }            
        }

        [HttpGet]
        public ActionResult EditState(string id)
        {
            var state = StateRepository.Get(id);
            return View(state);
        }

        [HttpPost]
        public ActionResult EditState(State state)
        {
            if (string.IsNullOrEmpty(state.StateName))
            {
                ModelState.AddModelError("StateName", "Please enter a state name");                
            }

            if (ModelState.IsValid)
            {
                StateRepository.Edit(state);
                return RedirectToAction("States");
            }
            else
            {
                return View("EditState", state);
            }            
        }

        [HttpGet]
        public ActionResult DeleteState(string id)
        {
            var state = StateRepository.Get(id);
            return View(state);
        }

        [HttpPost]
        public ActionResult DeleteState(State state)
        {
            StateRepository.Delete(state.StateAbbreviation);
            return RedirectToAction("States");
        }

        [HttpGet]
        public ActionResult Courses()
        {
            var model = CourseRepository.GetAll();
            return View(model.ToList());
        }

        [HttpGet]
        public ActionResult AddCourse()
        {
            return View(new Course());
        }

        [HttpPost]
        public ActionResult AddCourse(Course course)
        {
            if (string.IsNullOrEmpty(course.CourseName))
            {
                ModelState.AddModelError("CourseName", "Please enter a course name");
                return View("AddCourse", course);
            }

            CourseRepository.Add(course.CourseName);
            return RedirectToAction("Courses");
        }

        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            var course = CourseRepository.Get(id);
            return View(course);
        }

        [HttpPost]
        public ActionResult EditCourse(Course course)
        {
            if (string.IsNullOrEmpty(course.CourseName))
            {
                ModelState.AddModelError("CourseName", "Please enter a course name");
                return View("EditCourse", course);
            }

            CourseRepository.Edit(course);
            return RedirectToAction("Courses");
        }

        [HttpGet]
        public ActionResult DeleteCourse(int id)
        {
            var course = CourseRepository.Get(id);
            return View(course);
        }

        [HttpPost]
        public ActionResult DeleteCourse(Course course)
        {
            CourseRepository.Delete(course.CourseId);
            return RedirectToAction("Courses");
        }
    }
}