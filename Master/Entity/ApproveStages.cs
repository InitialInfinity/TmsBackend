using Tokens;

namespace Master.Entity
{
    public class ApproveStages
    {
        public BaseModel? BaseModel { get; set; }
        public Guid? UserId { get; set; }
        public string? as_id { get; set; }
        public string? as_action { get; set; }
        public string? as_stage { get; set; }
        public string? as_level { get; set; }
        public string? as_display_name { get; set; }
   
        public string? as_role_id { get; set; }

    }
}
