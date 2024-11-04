using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShare.Model
{
    [SugarTable("WxPersonTable")]
    public class WxPersonTable
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int Id { get; set; }
        public string OpenId { get; set; }
        public string WxName { get; set; }
        public string WxPhone { get; set; }
        public string PassWord { get; set; }
    }
}
