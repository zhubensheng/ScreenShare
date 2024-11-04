using ScreenShare.Common;
using ScreenShare.DBMain;
using ScreenShare.IRepositories;
using ScreenShare.Model;

namespace ScreenShare.Repositories
{
    [ServiceDescription(typeof(IWxPerson_Respositoriy), ServiceLifetime.Scoped)]
    public class WxPerson_Respositoriy : Repository<WxPersonTable>, IWxPerson_Respositoriy
    {
    }
}
