//if you hava any problem please email to keyle_xiao@hotmail.com
using System;

namespace SNFramework
{
  public abstract class SN : ISN
  {
    public string IdentifiedSign { get; set; }

    public SN ()
    {
      IdentifiedSign = (Guid.NewGuid ()).ToString ();
    }

    public virtual ISN ErrorLog (string msg)
    {
      throw new System.Exception ("ErrorLog:" + msg);
    }

    public virtual ISN WarningLog (string msg)
    {
      throw new System.Exception ("WarningLog:" + msg);
    }

    public abstract ISN Reset ();

    public virtual ISN GetSNObject (string identifiedID = "")
    {
      return this;
    }
  }
}
