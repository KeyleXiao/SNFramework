//if you hava any problem please email to keyle_xiao@hotmail.com
using UnityEngine;

namespace SNFramework
{
    public delegate void Action<T, U, V, W, X>(T arg1, U arg2, V arg3, W arg4, X arg5);

    public delegate TResult Func<in T, in U, in V, in W, in X, out TResult>(T arg1, U arg2, V arg3, W arg4, X arg5);


    /// <summary>
    /// 定义不同类型的消息
    /// </summary>
    public partial class SNMsg
    {
        public const string ExampleMsg = "0x0";
        public const string RefreshInfomation = "RefreshInfomation";
        public const string MapIDOnChange = "ShowInfo";
        public const string BrushTypeChange = "BrushTypeChange";
        public const string CancelBrush = "CancelBrush";
        public const string RenderMapIcon = "RenderMapIcon";
        public const string PlayerClickCarPartial = "PlayerClickCarPartial";

    }

    /// <summary>
    /// 根据不同的级别定制不同的通信上下文
    /// </summary>
    public partial class SNContextLevel
    {
        public const string DEFAULT = "0x0";
        public const string BOARDCAST = "0x1";
    }

    /// <summary>
    /// SNKit配置类
    /// </summary>
    public static class SNConfig
    {
        /// <summary>
        /// 是否启用错误追踪
        /// 默认在编辑器环境下启用，其他环境关闭
        /// </summary>
        public static bool EnableErrorTracking
        {
            get
            {
#if UNITY_EDITOR
                return _enableErrorTracking ?? true;
#else
                return _enableErrorTracking ?? false;
#endif
            }
            set => _enableErrorTracking = value;
        }
        private static bool? _enableErrorTracking;

        /// <summary>
        /// 错误追踪的最大堆栈深度
        /// </summary>
        public static int MaxStackTraceDepth { get; set; } = 20;

        /// <summary>
        /// 是否在控制台输出错误信息
        /// </summary>
        public static bool LogErrorsToConsole { get; set; } = true;

        /// <summary>
        /// 是否收集完整的调用堆栈
        /// </summary>
        public static bool CollectFullCallStack { get; set; } = true;

        /// <summary>
        /// 错误追踪的日志级别
        /// </summary>
        public enum ErrorTrackingLevel
        {
            /// <summary>
            /// 只追踪异常
            /// </summary>
            ErrorOnly,
            /// <summary>
            /// 追踪异常和警告
            /// </summary>
            Warning,
            /// <summary>
            /// 追踪所有，包括调试信息
            /// </summary>
            All
        }

        /// <summary>
        /// 设置错误追踪的日志级别
        /// </summary>
        public static ErrorTrackingLevel TrackingLevel { get; set; } = ErrorTrackingLevel.ErrorOnly;
    }
}