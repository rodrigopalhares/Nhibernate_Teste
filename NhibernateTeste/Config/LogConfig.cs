using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Appender;
using log4net.Layout;

namespace NhibernateTeste.Config
{
	public class LogConfig
	{
		public static void Init()
		{
			//var file = new FileInfo("output.log");
			//var appender = new FileAppender
			//                {
			//                    AppendToFile = false,
			//                    File = file.FullName,
			//                    Layout = new SimpleLayout()
			//                };

			var appender = new DebugAppender
			{
				Layout = new SimpleLayout()
			};

			log4net.Config.BasicConfigurator.Configure(appender);
		}
	}
}
