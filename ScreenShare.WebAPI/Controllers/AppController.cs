using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScreenShare.Common;
using ScreenShare.DBMain;
using ScreenShare.Entity;
using ScreenShare.Irepositiories;
using ScreenShare.Repositories;
using SqlSugar;

namespace ScreenShare.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly IApp_Respositoriy _repository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public AppController(IApp_Respositoriy repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetList(int pageIndex, int PageSize)
        {
            PageModel pageModel = new PageModel()
            {
                PageIndex = pageIndex,
                PageSize = PageSize
            };
            App_Respositoriy _Respositoriy = new App_Respositoriy();
            _Respositoriy.AddTest();

            PageList<App> page = _repository.GetPageList(x => x.Id > 0, pageModel);
            return Ok(new { code = 0, message = "获取成功", data = page });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddData()
        {
            _repository.Insert(new App() {Id=1, version="1.0.0"});
            return Ok(new { code = 0, message = "添加成功"});
        }
    }
}
