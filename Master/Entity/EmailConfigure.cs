using Tokens;

namespace Master.Entity
{
	public class EmailConfigure
	{
		public BaseModel? BaseModel { get; set; }
		public Guid? id { get; set; }
		public string? email { get; set; }
		public string? password { get; set; }
		public string? smtp_server { get; set; }
		public int? smtp_port { get; set; }
	}
}
