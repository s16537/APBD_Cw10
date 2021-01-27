using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using APBD_Cw10.DTOs.Requests;
using APBD_Cw10.Model;
using APBD_Cw10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APBD_Cw10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly s16537Context _context;
        //private readonly SPToCoreContext _functions;
        public EnrollmentsController(s16537Context context)
        {
            _context = context;
           // _functions = functions;
        }

        [HttpGet]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            var st = new Student();
            st.IndexNumber = request.IndexNumber;
            st.FirstName = request.FirstName;
            st.LastName = request.LastName;
            st.BirthDate = DateTime.Parse(request.BirthDate);
            var studies = request.Studies;

            var studiesId = _context.Studies.Where(stu => stu.Name == studies).Select(stu => stu.IdStudy).FirstOrDefault();
            if (studiesId == 0)
            {
                return NotFound("Nie znaleziono studiów o takiej nazwie.");
            }
            var enrollmentId = _context.Enrollments.Where(enr => enr.IdStudy == studiesId && enr.Semester == 1).Select(enr => enr.IdEnrollment).FirstOrDefault();
            if(enrollmentId == 0)
            {
                //stworz wpis enrollment
                int nextId = _context.Enrollments.Max(enr => enr.IdEnrollment) + 1;
                Enrollment enrollment = new Enrollment()
                {
                    IdEnrollment = nextId,
                    Semester = 1,
                    IdStudy = studiesId,
                    StartDate = DateTime.Now
                };
                _context.Enrollments.Add(enrollment);
                _context.SaveChanges();
                enrollmentId = nextId;
            }
            else
            {
                var startDate = _context.Enrollments.Where(enr => enr.IdStudy == studiesId && enr.Semester == 1).Select(enr => enr.StartDate).FirstOrDefault();

                //sprawdzamy czy podany index jest unikalny
                var res = _context.Students.Where(stu => stu.IndexNumber == request.IndexNumber).Select(stu => stu.IndexNumber).FirstOrDefault();
                if(res != null)
                {
                    return BadRequest("Student o podanym indeksie istnieje w bazie.");
                }

                //dodajemy studenta
                st.IdEnrollment = enrollmentId;
                _context.Students.Add(st);
                _context.SaveChanges();
            }
            return Ok(st);
        }

        /*
        [HttpPost]
        public IActionResult PromoteStudent(PromoteStudentsRequest request)
        {
            var studies = request.Studies;
            var semester = request.Semester;

           // List<SqlParameter> parameters = new List<SqlParameter>();
            var inStudies = new SqlParameter("@studies", studies);
           // parameters.Add(inStudies);
            var inSem = new SqlParameter("@semester", semester);
           // parameters.Add(inSem);
            //---------------------------------------------------------
            var outErrCode = new SqlParameter("@ERROR_CODE", SqlDbType.Int);
            outErrCode.Direction = ParameterDirection.Output;
            //parameters.Add(outErrCode);

            var outIdEnr= new SqlParameter("@IdEnrollmentNew", SqlDbType.Int);
            outIdEnr.Direction = ParameterDirection.Output;
           // parameters.Add(outIdEnr);

            var outIdStds = new SqlParameter("@IdStudies", SqlDbType.Int);
            outIdStds.Direction = ParameterDirection.Output;
            //parameters.Add(outIdStds);

            var outStartDate = new SqlParameter("@StartDate", SqlDbType.Date);
            outStartDate.Direction = ParameterDirection.Output;
            //parameters.Add(startDate);

            int resErrCode, resIdEnr, resIdStudies;
            DateTime resDate;

            _functions.PromoteStudents(studies, semester, ref resErrCode, resIdEnr, resIdStudies, resDate);




            return Ok(queryRes);
        }
        */
    }
}
