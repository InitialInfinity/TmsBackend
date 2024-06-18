using Tokens;

namespace Master.Entity
{
    public class KPIMaster
    {
        public BaseModel? BaseModel { get; set; }
        public Guid? UserId { get; set; }
        public Guid? k_id { get; set; }
        public string? k_emp_code { get; set; }
        public string? k_emp_name { get; set; }
        public string? k_kpi_code { get; set; }
        public string? k_kpi_des { get; set; }
        public string? k_department { get; set; }
        public string? k_designation { get; set; }
        public string? k_uom { get; set; }
        public string? k_isactive { get; set; }

        public DateTime? k_targetdate { get; set; }
        public string? k_occurance{ get; set; }
        public DateTime? k_createddate { get; set; }
        public DateTime? k_updateddate { get; set; }

    }
}
