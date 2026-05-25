using System;
using System.IO;
using System.Diagnostics;
using log4net;
using log4net.Config;
using Microsoft.SqlServer.Management.HadrData;

namespace NSqlTools.BusinessLayer.Logging
{
	public static class Log
	{
		private static readonly object _sync = new object();
		private static bool _initialized;
		private static ILog _logger;

		static Log()
		{
			EnsureInitialized();
		}

		private static void EnsureInitialized()
		{
			if (_initialized) return;
			lock (_sync)
			{
				if (_initialized) return;
				try
				{
					var baseDir = AppDomain.CurrentDomain.BaseDirectory ?? string.Empty;
					var dataDir = NSqlTools.Types.Constants.LogsFolder;
					if (!Directory.Exists(dataDir))
						Directory.CreateDirectory(dataDir);

					// read configuration from app.config
					XmlConfigurator.Configure();
					_logger = LogManager.GetLogger("NSqlToolsLogger");
				}
				catch (Exception ex)
				{
					NSqlTools.BusinessLayer.Logging.Log.Error("EnsureInitialized Error", ex);

					// fall back to Debug/Trace if directory creation fails
					Debug.WriteLine(ex);
				}
				_initialized = true;
			}
		}

		public static void Error(object message, Exception ex = null)
		{
			EnsureInitialized();
			var text = (message ?? string.Empty).ToString();
			if (_logger != null)
			{
				if (ex != null)
					_logger.Error(text, ex);
				else
					_logger.Error(text);
			}
			else
			{
				// fallback
				if (ex != null)
					Trace.TraceError(text + " - " + ex);
				else
					Trace.TraceError(text);
			}
		}

		public static void Info(object message)
		{
			EnsureInitialized();
			var text = (message ?? string.Empty).ToString();
			if (_logger != null)
				_logger.Info(text);
			else
				Trace.TraceInformation(text);
		}
	}
}
