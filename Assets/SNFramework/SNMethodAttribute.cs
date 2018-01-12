//if you hava any problem please email to keyle_xiao@hotmail.com
using System;

namespace SNFramework
{
  [AttributeUsage (AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
  public sealed class SNMethodAttribute : Attribute
  {
    public readonly string SNEventName;
    public readonly bool AutoRelease;
    public readonly SNContextType context;

    public SNMethodAttribute (string snEventName, SNContextType contextType = SNContextType.DEFAULT, bool autoRelease = true)
    {
      AutoRelease = autoRelease;
      context = contextType;
      SNEventName = snEventName;
    }
  }
}
