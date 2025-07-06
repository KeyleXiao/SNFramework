using System;
using System.Text;
using UnityEngine;

namespace SNFramework
{
    /// <summary>
    /// SNKit错误追踪器
    /// </summary>
    internal class SNErrorTracker
    {
        private static readonly object _lock = new object();
        private static SNErrorTracker _instance;

        public static SNErrorTracker Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SNErrorTracker();
                        }
                    }
                }
                return _instance;
            }
        }

        // 私有构造函数，防止外部创建实例
        private SNErrorTracker() { }

        /// <summary>
        /// 追踪错误
        /// </summary>
        public void TrackError(Exception ex, string eventName, ISNContext context, object[] args = null)
        {
            if (!SNConfig.EnableErrorTracking)
                return;

            var errorInfo = new StringBuilder();
            errorInfo.AppendLine($"SNKit事件处理器异常");
            errorInfo.AppendLine($"事件名称: {eventName}");
            errorInfo.AppendLine($"上下文: {context?.ContextModel ?? "无上下文"}");
            errorInfo.AppendLine($"异常类型: {ex.GetType().Name}");
            errorInfo.AppendLine($"异常消息: {ex.Message}");

            if (args != null && args.Length > 0)
            {
                errorInfo.AppendLine("参数信息:");
                for (int i = 0; i < args.Length; i++)
                {
                    var arg = args[i];
                    errorInfo.AppendLine($"  参数[{i}]: {(arg?.ToString() ?? "null")} ({(arg?.GetType().Name ?? "null")})");
                }
            }

            if (SNConfig.CollectFullCallStack)
            {
                errorInfo.AppendLine($"调用堆栈: {ex.StackTrace}");
            }

            Debug.LogError(errorInfo.ToString());
            SNKit.Instance?.OnEventHandlerException(ex);
        }

        /// <summary>
        /// 包装委托，添加错误追踪
        /// </summary>
        public static Action WrapDelegate(Action action, string eventName, string context)
        {
            if (!SNConfig.EnableErrorTracking)
                return action;

            return () =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    Instance.TrackError(ex, eventName, null, null);
                    throw;
                }
            };
        }

        /// <summary>
        /// 包装泛型委托，添加错误追踪
        /// </summary>
        public static Action<T> WrapDelegate<T>(Action<T> action, string eventName, string context)
        {
            if (!SNConfig.EnableErrorTracking)
                return action;

            return (arg) =>
            {
                try
                {
                    action(arg);
                }
                catch (Exception ex)
                {
                    Instance.TrackError(ex, eventName, null, new object[] { arg });
                    throw;
                }
            };
        }

        /// <summary>
        /// 包装两个参数的泛型委托
        /// </summary>
        public static Action<T1, T2> WrapDelegate<T1, T2>(Action<T1, T2> action, string eventName, string context)
        {
            if (!SNConfig.EnableErrorTracking)
                return action;

            return (arg1, arg2) =>
            {
                try
                {
                    action(arg1, arg2);
                }
                catch (Exception ex)
                {
                    Instance.TrackError(ex, eventName, null, new object[] { arg1, arg2 });
                    throw;
                }
            };
        }

        /// <summary>
        /// 包装三个参数的泛型委托
        /// </summary>
        public static Action<T1, T2, T3> WrapDelegate<T1, T2, T3>(Action<T1, T2, T3> action, string eventName, string context)
        {
            if (!SNConfig.EnableErrorTracking)
                return action;

            return (arg1, arg2, arg3) =>
            {
                try
                {
                    action(arg1, arg2, arg3);
                }
                catch (Exception ex)
                {
                    Instance.TrackError(ex, eventName, null, new object[] { arg1, arg2, arg3 });
                    throw;
                }
            };
        }

        /// <summary>
        /// 包装四个参数的泛型委托
        /// </summary>
        public static Action<T1, T2, T3, T4> WrapDelegate<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, string eventName, string context)
        {
            if (!SNConfig.EnableErrorTracking)
                return action;

            return (arg1, arg2, arg3, arg4) =>
            {
                try
                {
                    action(arg1, arg2, arg3, arg4);
                }
                catch (Exception ex)
                {
                    Instance.TrackError(ex, eventName, null, new object[] { arg1, arg2, arg3, arg4 });
                    throw;
                }
            };
        }

        /// <summary>
        /// 包装五个参数的泛型委托
        /// </summary>
        public static Action<T1, T2, T3, T4, T5> WrapDelegate<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action, string eventName, string context)
        {
            if (!SNConfig.EnableErrorTracking)
                return action;

            return (arg1, arg2, arg3, arg4, arg5) =>
            {
                try
                {
                    action(arg1, arg2, arg3, arg4, arg5);
                }
                catch (Exception ex)
                {
                    Instance.TrackError(ex, eventName, null, new object[] { arg1, arg2, arg3, arg4, arg5 });
                    throw;
                }
            };
        }
    }
}