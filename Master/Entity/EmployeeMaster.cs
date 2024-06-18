using Tokens;

namespace Master.Entity
{
    public class EmployeeMaster
    {
        public BaseModel? BaseModel { get; set; }
        public string? UserId { get; set; }
        public string? emp_id { get; set; }
        public string? emp_code { get; set; }
        public string? emp_fname { get; set; }
        public string? emp_mname { get; set; }
        public string? emp_lname { get; set; }
        public string? emp_job_title { get; set; }
        public string? emp_des { get; set; }//id
        public string? emp_des_id { get; set; }
        public string? emp_dep { get; set; }//id
        public string? emp_dep_id { get; set; }
        public string? emp_hod { get; set; }
        public string? emp_hodToEmp { get; set; }
        public string? emp_add1 { get; set; }
        public string? emp_add2 { get; set; }
        public string? emp_city { get; set; }
        public string? emp_city_name { get; set; }
        public string? emp_state { get; set; }
        public string? emp_country { get; set; }
        public string? emp_pincode { get; set; }
        public string? emp_mob_no { get; set; }
        public string? emp_off_no { get; set; }
        public string? emp_email { get; set; }

        public DateTime? emp_joiningDate{ get; set; }
        public string? emp_isactive { get; set; }
        public DateTime? emp_createddate { get; set; }
        public DateTime? emp_updateddate { get; set; }

        public string? Server_Value { get; set; }
    }
}
