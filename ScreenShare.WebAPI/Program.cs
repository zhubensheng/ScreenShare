using Microsoft.AspNetCore.Mvc;
using ScreenShare.Option;
using ScreenShare.Utils;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using ScreenShare.Irepositiories;
using ScreenShare.Repositories;
using ScreenShare.Entity;
using Microsoft.AspNetCore.Components.Authorization;
using ScreenShare.WebAPI.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(config =>
{
    //���趨���JsonResult���ı����������
    config.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);

    config.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
    config.JsonSerializerOptions.Converters.Add(new DateTimeNullableConvert());
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ScreenShare.WebAPI", Version = "v1" });
    //���Api��ע�ͣ�true��ʾ��ʾ������ע�ͣ�
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, true);
});
builder.Services.AddLocalization();
{
    builder.Configuration.GetSection("DBConnection").Get<DBConnectionOption>();
}
builder.Services.AddScoped<AuthenticationStateProvider, WxPersonAuthProvider>();
builder.Services.AddScoped<IApp_Respositoriy, App_Respositoriy>();
var app = builder.Build();


//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ScreenShare API"); //ע���м��v1Ҫ������SwaggerDoc��������ֱ���һ��
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
InitDB(app);
app.Run();


void InitDB(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        //codefirst ������
        var _repository = scope.ServiceProvider.GetRequiredService<IApp_Respositoriy>();
        _repository.GetDB().DbMaintenance.CreateDatabase();
        _repository.GetDB().CodeFirst.InitTables(typeof(App));
        //����vector���������ݿ�û������Ҫ�ṩ֧�����������ݿ�
        _repository.GetDB().Ado.ExecuteCommandAsync($"CREATE EXTENSION IF NOT EXISTS vector;");
    }
}
