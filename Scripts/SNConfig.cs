//if you hava any problem please email to keyle_xiao@hotmail.com
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
}