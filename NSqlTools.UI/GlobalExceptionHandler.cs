using NSqlTools.Lib;
using NSqlTools.Types.Properties;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSqlTools.UI
{
    /// <summary>
    /// Global exception handler for the application to prevent crashes
    /// </summary>
    public static class GlobalExceptionHandler
    {
        private static bool _isHandlerInstalled = false;
        private static readonly object _lock = new object();

        /// <summary>
        /// Install global exception handlers for the application
        /// </summary>
        public static void Install()
        {
            lock (_lock)
            {
                if (_isHandlerInstalled)
                    return;

                // UI thread exceptions
                Application.ThreadException += Application_ThreadException;
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                // Non-UI thread exceptions
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                // Task exceptions
                TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

                _isHandlerInstalled = true;
                LogHelper.Info("Global exception handlers installed successfully");
            }
        }

        /// <summary>
        /// Uninstall global exception handlers
        /// </summary>
        public static void Uninstall()
        {
            lock (_lock)
            {
                if (!_isHandlerInstalled)
                    return;

                Application.ThreadException -= Application_ThreadException;
                AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
                TaskScheduler.UnobservedTaskException -= TaskScheduler_UnobservedTaskException;

                _isHandlerInstalled = false;
                LogHelper.Info("Global exception handlers uninstalled");
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception, "UI Thread Exception");
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                HandleException(ex, "AppDomain Unhandled Exception", e.IsTerminating);
            }
            else
            {
                LogHelper.Error($"Unknown exception object: {e.ExceptionObject}");
            }
        }

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
            HandleException(e.Exception, "Task Unobserved Exception");
        }

        private static void HandleException(Exception ex, string source, bool isTerminating = false)
        {
            try
            {
                if (IsKnownNonFatalGdiPaintException(ex, source, isTerminating))
                {
                    LogHelper.Info($"Ignored non-fatal paint exception: {ex.Message}");
                    return;
                }

                // Log the exception
                LogHelper.Error($"{source}: {ex.Message}", ex);

                // Build user-friendly message
                StringBuilder message = new StringBuilder();
                message.AppendLine("An unexpected error occurred. The error has been logged.");
                message.AppendLine();
                message.AppendLine($"{CommonResource.Error}: {ex.Message}");

                if (isTerminating)
                {
                    message.AppendLine();
                    message.AppendLine("The application will now close.");
                }

                // Show message on UI thread
                if (Application.OpenForms.Count > 0 && Application.OpenForms[0].InvokeRequired)
                {
                    Application.OpenForms[0].Invoke(new Action(() =>
                    {
                        MessageBox.Show(
                            message.ToString(),
                            CommonResource.Error,
                            MessageBoxButtons.OK,
                            isTerminating ? MessageBoxIcon.Error : MessageBoxIcon.Warning
                        );
                    }));
                }
                else
                {
                    MessageBox.Show(
                        message.ToString(),
                        CommonResource.Error,
                        MessageBoxButtons.OK,
                        isTerminating ? MessageBoxIcon.Error : MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception handlerEx)
            {
                // If exception handler itself fails, try to log it
                try
                {
                    LogHelper.Error("Exception handler failed", handlerEx);
                }
                catch
                {
                    // Last resort - ignore
                }
            }
        }

        private static bool IsKnownNonFatalGdiPaintException(Exception ex, string source, bool isTerminating)
        {
            if (isTerminating || !(ex is ExternalException))
                return false;

            if (!String.Equals(source, "UI Thread Exception", StringComparison.Ordinal))
                return false;

            if (String.IsNullOrEmpty(ex.StackTrace))
                return false;

            return ex.Message.IndexOf("A generic error occurred in GDI+", StringComparison.OrdinalIgnoreCase) >= 0
                && ex.StackTrace.IndexOf("System.Windows.Forms.ToolStrip.OnPaintBackground", StringComparison.Ordinal) >= 0;
        }
    }
}
