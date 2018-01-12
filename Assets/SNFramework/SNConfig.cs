//if you hava any problem please email to keyle_xiao@hotmail.com
namespace SNFramework
{
  public delegate void Action<T,U,V,W,X> (T arg1,U arg2,V arg3,W arg4,X arg5);

  public delegate TResult Func<in T,in U,in V,in W,in X,out TResult> (T arg1,U arg2,V arg3,W arg4,X arg5);



  public static partial class SNEventName
  {
    /// <summary>
    /// Default Queue
    /// </summary>
    public const string Msg0 = "0x0";
  }

  public enum SNContextType
  {
    DEFAULT = 0,
    BOARDCAST = 2,
  }
}
