using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShare.Model
{
    [SugarTable("UserTable")]
    public class UserTable
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWordEncry{ get; set; }
    }
}
