﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShare.Option
{
    public class DBConnectionOption
    {
        /// <summary>
        /// sqlite连接字符串
        /// </summary>
        public static string DbType { get; set; }
        /// <summary>
        /// pg链接字符串
        /// </summary>
        public static string ConnectionStrings { get; set; }
    }
}
