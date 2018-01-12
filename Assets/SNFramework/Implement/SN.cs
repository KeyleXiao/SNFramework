//if you hava any problem please email to keyle_xiao@hotmail.com
using System;

namespace SNFramework
{
  public class SN : ISN
  {
    public string IdentifiedSign { get; set; }

    #if UNITY_EDITOR
    /// <summary>
    /// 用来检测当前代码段是否被重置
    /// </summary>
    /// <value>The reset times.</value>
    public int ResetTimes { get; set; }
    #endif

    public SN ()
    {
      IdentifiedSign = (Guid.NewGuid ()).ToString ();
    }

    public virtual ISN Log (string msg)
    {
      throw new System.Exception (string.Format ("SNFramework Log [{0}]", msg));
    }

    public virtual ISN Reset ()
    {
      #if UNITY_EDITOR
      ResetTimes += 1;
      #endif
      return this;
    }
  }
}
