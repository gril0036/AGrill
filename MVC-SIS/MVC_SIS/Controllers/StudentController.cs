﻿using Exercises.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exercises.Models.Data;
using Exercises.Models.ViewModels;

namespace Exercises.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = StudentRepository.GetAll();

            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new StudentVM();
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(StudentVM studentVM)
        {
            studentVM.Student.Courses = new List<Course>();
            studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);

            foreach (var id in studentVM.SelectedCourseIds)
                studentVM.Student.Courses.Add(CourseRepository.Get(id));                       
            
            if (ModelState.IsValid)
            {
                StudentRepository.Add(studentVM.Student);
                return RedirectToAction("List");
            }
            else
            {
                studentVM.SetCourseItems(CourseRepository.GetAll());
                studentVM.SetMajorItems(MajorRepository.GetAll());
                return View("Add", studentVM);
            }
        }

        [HttpGet]
        public ActionResult EditStudent(int id)
        {
            var student = StudentRepository.Get(id);

            var viewModel = new StudentVM();

            viewModel.Student = student;

            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            viewModel.SetStateItems(StateRepository.GetAll());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditStudent(StudentVM studentVM)
        {
            studentVM.Student.Courses = new List<Course>();
            studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);

            foreach (var id in studentVM.SelectedCourseIds)
                studentVM.Student.Courses.Add(CourseRepository.Get(id));
                        
            if (ModelState.IsValid)
            {
                StudentRepository.Edit(studentVM.Student);
                return RedirectToAction("List");
            }
            else
            {
                studentVM.SetCourseItems(CourseRepository.GetAll());
                studentVM.SetMajorItems(MajorRepository.GetAll());
                return View("EditStudent", studentVM);
            }
        }

        [HttpGet]
        public ActionResult DeleteStudent(int id)
        {
            var student = StudentRepository.Get(id);
            return View(student);
        }

        [HttpPost]
        public ActionResult DeleteStudent(Student student)
        {
            StudentRepository.Delete(student.StudentId);
            return RedirectToAction("List");
        }
    }
}