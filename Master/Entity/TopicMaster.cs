using System.Data;
using Tokens;

namespace Master.Entity
{
    public class TopicMaster
    {
        public BaseModel? BaseModel { get; set; }
        public Guid? UserId { get; set; }
        public Guid? t_id { get; set; }
        public string? t_code { get; set; }
        public string? t_description { get; set; }
        public string? t_department { get; set; }
        public string? t_trainingtype { get; set; }
        public string? t_duration { get; set; }
        public string? t_creadtedby { get; set; }
        public string? t_updatedby { get; set; }
        public DateTime? t_createddate { get; set; }
        public DateTime? t_updateddate { get; set; }
        public string? t_isactive { get; set; }

        public List<subject>? subject { get; set; }



        public DataTable? DataTable { get; set; }

    }


    public  class subject
    {


        //public Guid? s_id { get; set; }
        public string? s_subject { get; set; }
        public string? s_content { get; set; }
        public string? s_subcontent { get; set; }
        //public string? s_creadtedby { get; set; }
        //public string? s_updatedby { get; set; }
       
       // public string? s_isactive { get; set; }
        public string? s_t_id { get; set; }
    }
}
