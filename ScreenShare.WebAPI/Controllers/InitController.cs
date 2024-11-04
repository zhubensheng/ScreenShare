using Microsoft.AspNetCore.Mvc;
using ScreenShare.Entity;
using ScreenShare.Irepositiories;

namespace ScreenShare.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InitController : ControllerBase
    {
        private readonly IApp_Respositoriy _repository;

        public InitController(IApp_Respositoriy repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// 初始化DB 和表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InitTable()
        {
            _repository.GetDB().DbMaintenance.CreateDatabase();
            _repository.GetDB().CodeFirst.InitTables(typeof(App));
            //初始化数据

            //创建vector插件如果数据库没有则需要提供支持向量的数据库
            _repository.GetDB().Ado.ExecuteCommandAsync($"CREATE EXTENSION IF NOT EXISTS vector;");
            return Ok();
        }
    }
}
