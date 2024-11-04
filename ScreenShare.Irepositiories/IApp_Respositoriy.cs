using ScreenShare.DBMain;
using ScreenShare.Entity;

namespace ScreenShare.Irepositiories
{
    public interface IApp_Respositoriy : IRepository<App>
    {
        int AddTest();
    }
}
