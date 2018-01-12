//if you hava any problem please email to keyle_xiao@hotmail.com
using System;
using System.Collections.Generic;

namespace SNFramework
{
  public class SNKit :SN,ISNKit
  {
    public Dictionary<int, ISNQueue> SNGroup { get; set; }

    private SNKit () : base ()
    {
      SNGroup = new Dictionary<int, ISNQueue> ();
    }

    private static SNKit instance;

    public static SNKit Instance {
      get { return instance ?? (instance = new SNKit ()); }
    }

    protected void GetAttribute (Delegate g)
    {
      var atr = g.Method.GetCustomAttributes (false) [0] as SNMethodAttribute;
      var que = GetGroup (atr.GroupID);

      for (int i = 0; i < que.Queue.Count; i++) {
        if (que.Queue [i].SEventName == atr.SNEventName) {
          que.Queue [i].RegisterSNEvent (atr.MsgName, g);
          return;
        }
      }

      ISNEvent e = new SNEvent ();
      e.SEventName = atr.SNEventName;
      e.RegisterSNEvent (atr.MsgName, g);
      que.UpdateSNEvent (e);
    }

    public SNKit Add (Action m)
    {
      GetAttribute (m);
      return this;
    }

    public SNKit Add<T> (Action<T> m)
    {
      GetAttribute (m);
      return this;
    }

    public SNKit Add<T, U> (Action<T, U> m)
    {
      GetAttribute (m);
      return this;
    }

    public SNKit Add<T, U, V> (Action<T, U, V> m)
    {
      GetAttribute (m);
      return this;
    }

    public SNKit Add<T, U, V, W> (Action<T, U, V, W> m)
    {
      GetAttribute (m);
      return this;
    }

    public SNKit Add<T, U, V, W, X> (Action<T, U, V, W, X> m)
    {
      GetAttribute (m);
      return this;
    }

    public SNKit Add<TResult> (Func<TResult> m)
    {
      GetAttribute (m);
      return this;
    }

    public SNKit Add<T, TResult> (Func<T, TResult> m)
    {
      GetAttribute (m);
      return this;
    }

    public SNKit Add<T, U, TResult> (Func<T, U, TResult> m)
    {
      GetAttribute (m);
      return this;
    }

    public SNKit Add<T, U, V, TResult> (Func<T, U, V, TResult> m)
    {
      GetAttribute (m);
      return this;
    }

    public SNKit Add<T, U, V, W, TResult> (Func<T, U, V, W, TResult> m)
    {
      GetAttribute (m);
      return this;
    }

    public SNKit Add<T, U, V, W, X, TResult> (Func<T, U, V, W, X, TResult> m)
    {
      GetAttribute (m);
      return this;
    }


    public SNKit ForceReset (int id = 0)
    {
      if (SNGroup.ContainsKey (id)) {
        if (SNGroup [id] == null) {
          ErrorLog (string.Format ("SNPool[{0}] IS NULL", id));
          return this;
        } else {
          SNGroup [id].Reset ();
          return this;
        }
      } else {
        WarningLog (string.Format ("SNPool[{0}] CAN'T FOUND", id));
        return this;
      }
    }

    public ISNQueue GetGroup (int groupID = 0)
    {
      if (SNGroup.ContainsKey (groupID)) {
        if (SNGroup [groupID] == null) {
          SNGroup [groupID] = new SNQueue ();
          return SNGroup [groupID];
        } else {
          return SNGroup [groupID];
        }
      } else {
        SNGroup.Add (groupID, new SNQueue ());
        return SNGroup [groupID];
      }
    }


    public override ISN Reset ()
    {
      if (SNGroup != null) {
        SNGroup.Clear ();
      } else {
        SNGroup = new Dictionary<int, ISNQueue> ();
      }
      return this;
    }

    public SNKit RemoveQueue (int groupID)
    {
      if (SNGroup.ContainsKey (groupID)) {
        SNGroup.Remove (groupID);
        return this;
      } else {
        WarningLog (string.Format ("SNPool[{0}] CAN'T FOUND", groupID));
        return this;
      }
    }

    public ISNQueue this [int groupID] {
      get { return GetGroup (groupID); }
    }
  }
}
