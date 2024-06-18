using Master.Repository;
using Context;
using Master.Repository;
using Master.Repository.Interface;
using Master.Entity;
using Master.API.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddControllers();


builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IDesignationMasterRepository, DesignationMasterRepository>();
builder.Services.AddScoped<IDepartmentMasterRepository, DepartmentMasterRepository>();
builder.Services.AddScoped<ILoginDetailsRepository, LoginDetailsRepository>();
builder.Services.AddScoped<IRoleMasterRepository, RoleMasterRepository>();
builder.Services.AddScoped<ITopicMasterRepository, TopicMasterRepository>();
builder.Services.AddScoped<IKPIMasterRepository, KPIMasterRepository>();
builder.Services.AddScoped<IEmployeeMasterRepository, EmployeeMasterRepository>();
builder.Services.AddScoped<ICompentencyMasterRepository, CompentencyMasterRepository>();
builder.Services.AddScoped<ITrainingFormRepository, TrainingFormRepository>();
builder.Services.AddScoped<IGetWebMenuRepository, GetWebMenuRepository>();
builder.Services.AddScoped<IUserMasterRepository, UserMasterRepository>();
builder.Services.AddScoped<IApproveStagesRepositorycs, ApproveStagesRepository>();
builder.Services.AddScoped<IParameterValueMasterRepository, ParameterValueMasterRepository>();
builder.Services.AddScoped<ICityMasterRepository, CityMasterRepository>();
builder.Services.AddScoped<ICountryMasterRepository, CountryMasterRepository>();
builder.Services.AddScoped<IStateMasterRepository, StateMasterRepository>();
builder.Services.AddScoped<ITrainingScheduleRepository, TrainingScheduleRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<ICompentencyChartReportRepository, CompentencyChartReportRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IParameterMasterRepository, ParameterMasterRepository>();
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(
		builder =>
		{
			builder.WithOrigins().AllowAnyOrigin()
								.AllowAnyHeader()
								.AllowAnyMethod();
		});
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
