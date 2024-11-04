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
    //此设定解决JsonResult中文被编码的问题
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
    //添加Api层注释（true表示显示控制器注释）
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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ScreenShare API"); //注意中间段v1要和上面SwaggerDoc定义的名字保持一致
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
        //codefirst 创建表
        var _repository = scope.ServiceProvider.GetRequiredService<IApp_Respositoriy>();
        _repository.GetDB().DbMaintenance.CreateDatabase();
        _repository.GetDB().CodeFirst.InitTables(typeof(App));
        //创建vector插件如果数据库没有则需要提供支持向量的数据库
        _repository.GetDB().Ado.ExecuteCommandAsync($"CREATE EXTENSION IF NOT EXISTS vector;");
    }
}
