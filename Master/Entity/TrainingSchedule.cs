using Master.Entity;
using System.Data;
using System.Threading;
using Tokens;

namespace Master.Entity
{
    public class TrainingSchedule
    {
        public BaseModel? BaseModel { get; set; }
        public Guid? UserId { get; set; }

		public string? roleid { get; set; }
		//training schedule
		public Guid? ts_id { get; set; }
        public string? ts_training_no{ get; set; }
        public string? ts_trainer_name{ get; set; }
        public string? ts_training_dept { get; set; }
        public string? ts_req_by{ get; set; }
        public string? ts_topic { get; set; }
        public string? ts_topic_name { get; set; }
        public string? ts_no_que{ get; set; }
        public string? ts_training_agency{ get; set; }
        public string? ts_training_type{ get; set; }
        public string? ts_reoccurence{ get; set; }
        public DateTime? ts_dt_tm_fromtraining { get; set; }
        public DateTime? ts_dt_tm_totraining { get; set; }
        public string? ts_status { get; set; }

		public string? ts_remark { get; set; }

		public DateTime? ts_createddate { get; set; }
        public DateTime? ts_updateddate { get; set; }
        public string? ts_creadtedby { get; set; }
        public string? ts_updatedby { get; set; }
        public string? ts_isactive { get; set; }
        public string? ts_action { get; set; }
        public string? ts_empid { get; set; }
        public string? ts_tag { get; set; }

        public List<trainingsubschedule>? trainingsubschedule { get; set; }



        public DataTable? DataTable { get; set; }


    }

    public class trainingsubschedule
    {

        //add training schedule

       
        public Guid? tss_id { get; set; }
        public string? tss_emp_code { get; set; }
        public string? tss_emp_name { get; set; }
        public string? tss_traning_attend { get; set; }
        public string? tss_traning_des { get; set; }
        public string? tss_sch_hour { get; set; }

        public string? tss_actual_attend { get; set; }
        public string? tss_com_status { get; set; }
        public string? tss_to_marks { get; set; }
        public string? tss_marks_obt { get; set; }
       
        public string? tss_traning_status { get; set; }
        public string? tss_re_traning_req { get; set; }
        public Byte[]? tss_traning_cert { get; set; }
        public string? tss_status { get; set; }
        public string? tss_remark { get; set; }

        public string? ts_tss_id { get; set; }
        public string? tss_topic { get; set; }
        public string? tss_tag { get; set; }
      


    }
  }

                     
                     
                      
                    
                     
                     