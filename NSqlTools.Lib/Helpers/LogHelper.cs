using System;
using System.Diagnostics;
using System.IO;
using log4net;
using log4net.Config;
using NSqlTools.Types;

namespace NSqlTools.Lib
{
	public static class LogHelper
	{
		private static readonly object _sync;

		private static bool _initialized;

		private static ILog _logger;

		static LogHelper()
		{
			_sync = new object();
			EnsureInitialized();
		}

		private static void EnsureInitialized()
		{
			if (_initialized)
			{
				return;
			}
			lock (_sync)
			{
				if (_initialized)
				{
					return;
				}
				try
				{
					string dataDir = Constants.LogsFolder;
					if (!Directory.Exists(dataDir))
					{
						Directory.CreateDirectory(dataDir);
					}

					// App.config'deki log4net section'ını yükle
					XmlConfigurator.Configure();

					_logger = LogManager.GetLogger("NSqlToolsLogger");

					// Log sisteminin başarıyla başlatıldığını kaydet
					if (_logger != null)
					{
						_logger.Info("Log system initialized successfully");
					}
					else
					{
						Trace.TraceWarning("Logger could not be initialized - _logger is null");
					}
				}
				catch (Exception ex)
				{
					LogHelper.Error(ex);
					// İlk başlatmada logger yoksa Trace'e yaz
					System.Diagnostics.Debug.WriteLine("EnsureInitialized Error: " + ex);
					Trace.TraceError("EnsureInitialized Error: " + ex);
				}
				_initialized = true;
			}
		}

		public static void Error(object message, Exception ex = null)
		{
			EnsureInitialized();
			string text = (message ?? string.Empty).ToString();
			if (_logger != null)
			{
				if (ex != null)
				{
					_logger.Error(text, ex);
				}
				else
				{
					_logger.Error(text);
				}
			}
			else if (ex != null)
			{
				Trace.TraceError(text + " - " + ex);
			}
			else
			{
				Trace.TraceError(text);
			}
		}

		public static void Info(object message)
		{
			EnsureInitialized();
			string text = (message ?? string.Empty).ToString();
			if (_logger != null)
			{
				_logger.Info(text);
			}
			else
			{
				Trace.TraceInformation(text);
			}
		}
	}
}
