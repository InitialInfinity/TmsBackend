using Tokens;

namespace Master.Entity
{
    public class CompentencyMaster
    {
        public BaseModel? BaseModel { get; set; }
        public Guid? UserId { get; set; }
        public Guid? cp_id { get; set; }

        public string? cp_designation { get; set; }
        public string? cp_description { get; set; }
        public string? cp_qualification { get; set; }
        public string? cp_experiance { get; set; }
        public string? cp_skillreq { get; set; }
        public string? cp_training { get; set; }
        public string? cp_isactive { get; set; }
        public string? t_id { get; set; }
        public string? t_description { get; set; }
        public DateTime? cp_createddate { get; set; }
        public DateTime? cp_updateddate { get; set; }
    }
}
