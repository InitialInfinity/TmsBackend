using System.Data;
using Tokens;

namespace Master.Entity
{
    public class FeedbackForm
    {
        public BaseModel? BaseModel { get; set; }
        public Guid? UserId { get; set; }
        public Guid? fb_id { get; set; }
        public string? fb_name { get; set; }
        public string? fb_no { get; set; }
        public string? fb_title { get; set; }
        public DateTime? fb_date { get; set; }
        public string? fb_givenBy { get; set; }

        public DateTime? fb_createddate { get; set; }
        public DateTime? fb_updateddate { get; set; }
        public string? fb_creadtedby { get; set; }
        public string? fb_updatedby { get; set; }
        public string? fb_isactive { get; set; }
        public DataTable? DataTable { get; set; }

        public List<Feedback>? Feedback { get; set; }
        public string? fb_empCode { get; set; }
        public string? fb_trnNo { get; set; }
    }
    public class Feedback
    {
        public string? f_trnNo { get; set; }
        public string? f_trnType { get; set; }
        public string? f_trnReqBy { get; set; }
        public string? f_dateTrnForm { get; set; }
        public string? f_TimeTrnForm { get; set; }

        public string? f_dateTrnTo { get; set; }
        public string? f_TimeTrnTo { get; set; }
        public string? f_empCode { get; set; }
        public string? f_empName { get; set; }
        public string? f_trnAttend { get; set; }
        public string? f_des { get; set; }
        public string? f_desId { get; set; }
        public string? f_feedback { get; set; }
        public Byte[]? f_Trainerfeedback { get; set; }
        public Byte[]? f_Trainingfeedback { get; set; }
       
        public string? f_fb_id { get; set; }
        public string? f_depid { get; set; }
        public string? f_dep { get; set; }
        public string? f_trainingRel { get; set; }
        public string? f_contentWell { get; set; }
        public string? f_example { get; set; }
        public string? f_duration { get; set; }
        public string? f_participation { get; set; }
        public string? f_question { get; set; }
    }
}
