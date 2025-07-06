//if you hava any problem please email to keyle_xiao@hotmail.com
using System;

namespace SNFramework
{
  public class SN : ISN
  {
    private const string LOG_PREFIX = "SNFramework Log [";
    private const string LOG_SUFFIX = "]";
    
    public string IdentifiedSign { get; set; }

    /// <summary>
    /// 用来检测当前代码段是否被重置
    /// </summary>
    /// <value>The reset times.</value>
    public int ResetTimes { get; set; }

    public SN ()
    {
      IdentifiedSign = Guid.NewGuid().ToString();
    }

    public virtual ISN Log (string msg)
    {
      throw new System.Exception(LOG_PREFIX + msg + LOG_SUFFIX);
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
