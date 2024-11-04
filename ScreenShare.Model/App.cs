using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace ScreenShare.Entity
{
    [SugarTable("App")]
    public class App
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int Id { get; set; }
        
        public string version { get; set; }
    }
}
