//if you hava any problem please email to keyle_xiao@hotmail.com
using System;
using System.Collections.Generic;

namespace SNFramework
{
  public class SNQueue : SN, ISNQueue
  {
    public SNQueue ()
      : base ()
    {
      Queue = new List<ISNEvent> ();
      QueueModel = SNQueueType.DEFAULT;
      Expression = null;
    }


    public ISNEvent Expression { get; set; }

    public SNQueueType QueueModel { get; set; }

    public List<ISNEvent> Queue { get; set; }


    public ISNQueue RemoveSNEvent (ISNEvent sn)
    {
      if (Queue.Contains (sn)) {
        Queue.Remove (sn);
      } else {
        ErrorLog (string.Format ("{0} CAN'T FIND", sn.IdentifiedSign));
      }

      return this;
    }

    public ISNQueue RemoveSNEventByIdentifiedID (string identifiedID)
    {
      for (int i = 0; i < Queue.Count; i++) {
        if (Queue [i].IdentifiedSign == identifiedID) {
          Queue.RemoveAt (i);
        }
      }
      return this;
    }

    public override ISN Reset ()
    {
      if (Queue != null) {
        Queue.Clear ();
      } else {
        Queue = new List<ISNEvent> ();
      }
      QueueModel = SNQueueType.DEFAULT;
      Expression = null;
      return this;
    }

    [Obsolete ("No implement ...", true)]
    public ISNQueue SetExpression (ISNEvent sn)
    {
      Expression = sn;
      return this;
    }

    [Obsolete ("No implement ...", true)]
    public ISNQueue SetQueueModel (SNQueueType t)
    {
      QueueModel = t;
      return this;
    }

    public ISNQueue UpdateSNEvent (ISNEvent sn)
    {
      for (int i = 0; i < Queue.Count; i++) {
        if (Queue [i].IdentifiedSign == sn.IdentifiedSign) {
          Queue [i] = sn;
          return this;
        }
      }
      Queue.Add (sn);
      return this;
    }

    public ISNEvent GetSNEvent (string sign)
    {
      if (Queue == null) {
        ErrorLog ("Queue IS NULL");
        return null;
      }
      for (int i = 0; i < Queue.Count; i++) {
        if (Queue [i].IdentifiedSign == sign) {
          return Queue [i];
        }
      }
      for (int i = 0; i < Queue.Count; i++) {
        if (Queue [i].SEventName == sign) {
          return Queue [i];
        }
      }
      return null;
    }

    public ISNEvent this [string identifiedSign] {
      get {
        for (int i = 0; i < Queue.Count; i++) {
          if (Queue [i].IdentifiedSign == identifiedSign) {
            return Queue [i];
          }
        }
        return null;
      }
      set {
        for (int i = 0; i < Queue.Count; i++) {
          if (Queue [i].IdentifiedSign == identifiedSign) {
            Queue [i] = value;
          }
        }
      }
    }
  }
}
