//if you hava any problem please email to keyle_xiao@hotmail.com
using System;

namespace SNFramework
{
  [AttributeUsage (AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
  public sealed class SNMethodAttribute : Attribute
  {
    public readonly string SNEventName;
    public readonly bool AutoRelease;

    public SNMethodAttribute (string snEventName, bool autoRelease = false)
    {
      AutoRelease = autoRelease;
      SNEventName = snEventName;
    }
  }
}
