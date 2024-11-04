using ScreenShare.Common;
using ScreenShare.DBMain;
using ScreenShare.Entity;
using ScreenShare.Irepositiories;

namespace ScreenShare.Repositories
{
    [ServiceDescription(typeof(IApp_Respositoriy), ServiceLifetime.Scoped)]
    public class App_Respositoriy : Repository<App>, IApp_Respositoriy
    {
        public int AddTest()
        {
            
            return 0;
        }
    }
}
