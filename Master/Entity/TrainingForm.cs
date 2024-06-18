using System.Data;
using Tokens;

namespace Master.Entity
{
    public class TrainingForm
    {
        public BaseModel? BaseModel { get; set; }
        public Guid? UserId { get; set; }
        public string? roleid { get; set; }
        public Guid? tr_id { get; set; }

        public string? tr_nature { get; set; }
        public string? tr_type { get; set; }
        public string? tr_req_no { get; set; }
        public DateTime? tr_req_date { get; set; }
        public string? tr_hours { get; set; }
        public string? tr_days { get; set; }
        public string? tr_action { get; set; }
        public string? tr_remark { get; set; }
        public DateTime? tr_createddate { get; set; }
        public DateTime? tr_updateddate { get; set; }
        public string? tr_creadtedby { get; set; }
        public string? tr_updatedby { get; set; }
        public string? tr_isactive { get; set; }
        public string? IDMail { get; set; }

        public List<training>? training { get; set; }



        public DataTable? DataTable { get; set; }
        //public Guid? td_id { get; set; }
        public string? td_dept { get; set; }
        public string? td_emp_code { get; set; }

       public string? td_emp_name { get; set; }
       public string? td_emp_des { get; set; }
        //public string? td_emp_code { get; set; }
        //public string? td_emp_name { get; set; }
        //public string? td_req_dept { get; set; }
        //public DateTime? td_date_training { get; set; }
        public string? tr_topic_training { get; set; }
        //public DateTime? td_createddate { get; set; }
        //public DateTime? td_updateddate { get; set; }
        //public string? td_creadtedby { get; set; }
        //public string? td_updatedby { get; set; }
        //public string? td_isactive { get; set; }
        //public string? tr_td_id { get; set; }
    }

    public class training
    {
        //public Guid? td_id { get; set; }
        public string? td_dept { get; set; }
        public string? td_des { get; set; }
        public string? td_emp_code { get; set; }
        public string? td_emp_name { get; set; }
        
        public string? td_date_training { get; set; }


		public string? td_req_dept { get; set; }
		public string? td_topic_training { get; set; }
		public string? td_topic_training_name { get; set; }
		//public DateTime? td_createddate { get; set; }
		//public DateTime? td_updateddate { get; set; }
		//public string? td_creadtedby { get; set; }
		//public string? td_updatedby { get; set; }
		//public string? td_isactive { get; set; }

		public string? td_emp_des { get; set; }
		public string? tr_td_id { get; set; }

    }
}
