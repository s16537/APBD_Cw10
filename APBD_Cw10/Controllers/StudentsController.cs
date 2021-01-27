using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD_Cw10.DTOs.Requests;
using APBD_Cw10.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Cw10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly s16537Context _context;
        public StudentsController(s16537Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var res = _context.Students.ToList();
            return Ok(res);
        }

        [HttpPost]
        public IActionResult UpdateStudent(UpdateStudentRequest request)
        {
            var index = request.IndexNumber;
            if (index == null)
            {
                return BadRequest("Podaj nr indeksu (PK) studenta, którego dane chcesz zaktualizować.");
            }
            var firstName = request.FirstName;
            var lastName = request.LastName;
            var birth = request.BirthDate;
            //var providedFields = request.GetType().GetProperties();

            try
            {
                //Pobieramy z bazy "obiekt" studenta, który zostanie zaktualizowany
                var studentToUpdate = _context.Students.Where(st => st.IndexNumber == index).First();
                if (firstName != null)
                {
                    studentToUpdate.FirstName = firstName;
                }
                if (lastName != null)
                {
                    studentToUpdate.LastName = lastName;
                }
                if (birth != null)
                {
                    studentToUpdate.BirthDate = birth;
                }

                _context.SaveChanges();
                return Ok("Zaktualizowano dane studenta.");
            }
            catch (Exception e)
            {
                return NotFound("Nie znaleziono studenta o podanym indeksie.");
            }
        }

        [HttpDelete]
        public IActionResult DeleteStudent(string index)
        {
            try
            {
                var studentToDelete = _context.Students.Where(st => st.IndexNumber == index).First();
                _context.Students.Remove(studentToDelete);
                _context.SaveChanges();
                return Ok("Usunięto studenta.");
            }
            catch (Exception e)
            {
                return NotFound("Nie znaleziono studenta o podanym indeksie.");
            }
        }
    }
}
