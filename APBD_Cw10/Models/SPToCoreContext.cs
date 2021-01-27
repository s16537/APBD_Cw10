using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using APBD_Cw10.Models;

namespace APBD_Cw10.Model
{
    public partial class SPToCoreContext : s16537Context
    {

        public SPToCoreContext()
        {
        }

        public SPToCoreContext(DbContextOptions<s16537Context> options)
            : base(options)
        {
        }               

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            // No key            
            //Thanks Valecass!!!
            base.OnModelCreating(modelBuilder);
        }

        public void PromoteStudents(string Studies,int? Semester,ref int? ERROR_CODE,ref int? IdEnrollmentNew,ref int? IdStudies,ref DateTime? StartDate)
        {
            try
            {
                // Parameters
                SqlParameter p_Studies = new SqlParameter("@Studies", Studies ?? (object)DBNull.Value);
                p_Studies.Direction = ParameterDirection.Input;
                p_Studies.DbType = DbType.String;
                p_Studies.Size = 200;

                SqlParameter p_Semester = new SqlParameter("@Semester", Semester ?? (object)DBNull.Value);
                p_Semester.Direction = ParameterDirection.Input;
                p_Semester.DbType = DbType.Int32;
                p_Semester.Size = 4;

                SqlParameter p_ERROR_CODE = new SqlParameter("@ERROR_CODE", ERROR_CODE ?? (object)DBNull.Value);
                p_ERROR_CODE.Direction = ParameterDirection.Output;
                p_ERROR_CODE.DbType = DbType.Int32;
                p_ERROR_CODE.Size = 4;

                SqlParameter p_IdEnrollmentNew = new SqlParameter("@IdEnrollmentNew", IdEnrollmentNew ?? (object)DBNull.Value);
                p_IdEnrollmentNew.Direction = ParameterDirection.Output;
                p_IdEnrollmentNew.DbType = DbType.Int32;
                p_IdEnrollmentNew.Size = 4;

                SqlParameter p_IdStudies = new SqlParameter("@IdStudies", IdStudies ?? (object)DBNull.Value);
                p_IdStudies.Direction = ParameterDirection.Output;
                p_IdStudies.DbType = DbType.Int32;
                p_IdStudies.Size = 4;

                SqlParameter p_StartDate = new SqlParameter("@StartDate", StartDate ?? (object)DBNull.Value);
                p_StartDate.Direction = ParameterDirection.Output;
                p_StartDate.DbType = DbType.DateTime;
                p_StartDate.Size = 3;


                // Processing 
                string sqlQuery = $@"EXEC [dbo].[PromoteStudents] @Studies, @Semester, @ERROR_CODE OUTPUT, @IdEnrollmentNew OUTPUT, @IdStudies OUTPUT, @StartDate OUTPUT";
                //Execution
                this.Database.ExecuteSqlRaw(sqlQuery , p_Studies , p_Semester , p_ERROR_CODE , p_IdEnrollmentNew , p_IdStudies , p_StartDate );
                
                //Output Params
                ERROR_CODE = (int?)p_ERROR_CODE.Value;
                IdEnrollmentNew = (int?)p_IdEnrollmentNew.Value;
                IdStudies = (int?)p_IdStudies.Value;
                StartDate = (DateTime?)p_StartDate.Value;
            }
            catch (Exception ex){
                throw ex;
            }

            //Return
        }


    }
}