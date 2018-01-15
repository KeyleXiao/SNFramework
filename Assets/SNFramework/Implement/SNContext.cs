//if you hava any problem please email to keyle_xiao@hotmail.com
using System;
using System.Collections.Generic;

namespace SNFramework
{
  public class SNContext : SN, ISNContext
  {
    public SNContext ()
      : base ()
    {
      Context = new List<ISNEvent> ();
      ContextModel = SNContextLevel.DEFAULT;
    }

    public string ContextModel { get; set; }

    public List<ISNEvent> Context { get; set; }

    public ISNContext RemoveSNEvent (ISNEvent sn)
    {
      if (Context.Contains (sn)) {
        Context.Remove (sn);
      } else {
        Log (string.Format ("{0} CAN'T FIND", sn.IdentifiedSign));
      }

      return this;
    }

    public ISNContext RemoveSNEventByIdentifiedID (string identifiedID)
    {
      for (int i = 0; i < Context.Count; i++) {
        if (Context [i].IdentifiedSign == identifiedID) {
          Context.RemoveAt (i);
        }
      }
      return this;
    }

    public override ISN Reset ()
    {
      base.Reset ();
      if (Context != null) {
        for (int i = 0; i < Context.Count; i++) {
          Context [i].Reset ();
        }
      } else {
        Context = new List<ISNEvent> ();
      }
      ContextModel = SNContextLevel.DEFAULT;
      return this;
    }



    public ISNContext UpdateSNEvent (ISNEvent sn)
    {
      for (int i = 0; i < Context.Count; i++) {
        if (Context [i].IdentifiedSign == sn.IdentifiedSign) {
          Context [i] = sn;
          return this;
        }
      }
      Context.Add (sn);
      return this;
    }


    public ISNEvent GetSNEvent (string identifiedOrEventName)
    {
      if (Context == null) {
        Context = new List<ISNEvent> ();
        CreateSNEvent (identifiedOrEventName);
        return null;
      }
      for (int i = 0; i < Context.Count; i++) {
        if (Context [i].IdentifiedSign == identifiedOrEventName) {
          return Context [i];
        }
      }

      return CreateSNEvent (identifiedOrEventName);
    }


    public ISNEvent SetSNEvent (string identifiedOrEventName, ISNEvent value)
    {
      for (int i = 0; i < Context.Count; i++) {
        if (Context [i].IdentifiedSign == value.IdentifiedSign) {
          Context [i] = value;
          return Context [i];
        }
      } 

      CreateSNEvent (identifiedOrEventName);
      return SetSNEvent (identifiedOrEventName, value);
    }

    public ISNEvent CreateSNEvent (string identifiedEventName)
    {
      ISNEvent e = new SNEvent ();
      e.AutoRelease = false;
      e.IdentifiedSign = identifiedEventName;
      Context.Add (e);
      return e;
    }



    public ISNEvent this [string identifiedSign] {
      get {
        GetSNEvent (identifiedSign);
        return null;
      }
      set {
        SetSNEvent (identifiedSign, value);
      }
    }
  }
}
