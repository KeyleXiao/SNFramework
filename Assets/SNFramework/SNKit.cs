//if you hava any problem please email to keyle_xiao@hotmail.com
using System;
using System.Collections.Generic;

namespace SNFramework
{
  public class SNKit :SN,ISNKit
  {
    public Dictionary<SNContextType, ISNContext> SNContexts { get; set; }

    private SNKit () : base ()
    {
      SNContexts = new Dictionary<SNContextType, ISNContext> ();
    }

    private static SNKit instance;

    public static SNKit Instance {
      get { return instance ?? (instance = new SNKit ()); }
    }

    protected void CreateSNEvent (Delegate g)
    {
      var atr = g.Method.GetCustomAttributes (false) [0] as SNMethodAttribute;
      var que = GetContext (atr.context);

      for (int i = 0; i < que.Context.Count; i++) {
        if (que.Context [i].IdentifiedSign == atr.SNEventName) {
          que.Context [i].Register (atr.MsgName, g);
          return;
        }
      }

      ISNEvent e = new SNEvent ();
      e.IdentifiedSign = atr.SNEventName;
      e.Register (atr.MsgName, g);
      que.UpdateSNEvent (e);
    }

    public SNKit Register (Action m)
    {
      CreateSNEvent (m);
      return this;
    }

    public SNKit Register<T> (Action<T> m)
    {
      CreateSNEvent (m);
      return this;
    }

    public SNKit Register<T, U> (Action<T, U> m)
    {
      CreateSNEvent (m);
      return this;
    }

    public SNKit Register<T, U, V> (Action<T, U, V> m)
    {
      CreateSNEvent (m);
      return this;
    }

    public SNKit Register<T, U, V, W> (Action<T, U, V, W> m)
    {
      CreateSNEvent (m);
      return this;
    }

    public SNKit Register<T, U, V, W, X> (Action<T, U, V, W, X> m)
    {
      CreateSNEvent (m);
      return this;
    }

    public SNKit Register<TResult> (Func<TResult> m)
    {
      CreateSNEvent (m);
      return this;
    }

    public SNKit Register<T, TResult> (Func<T, TResult> m)
    {
      CreateSNEvent (m);
      return this;
    }

    public SNKit Register<T, U, TResult> (Func<T, U, TResult> m)
    {
      CreateSNEvent (m);
      return this;
    }

    public SNKit Register<T, U, V, TResult> (Func<T, U, V, TResult> m)
    {
      CreateSNEvent (m);
      return this;
    }

    public SNKit Register<T, U, V, W, TResult> (Func<T, U, V, W, TResult> m)
    {
      CreateSNEvent (m);
      return this;
    }

    public SNKit Register<T, U, V, W, X, TResult> (Func<T, U, V, W, X, TResult> m)
    {
      CreateSNEvent (m);
      return this;
    }


    public SNKit ResetContext (SNContextType context = SNContextType.DEFAULT)
    {
      if (SNContexts.ContainsKey (context)) {
        if (SNContexts [context] == null) {
          Log (string.Format ("SNContexts[{0}] IS NULL", context));
          return this;
        } else {
          SNContexts [context].Reset ();
          return this;
        }
      } else {
        Log (string.Format ("SNContexts[{0}] CAN'T FOUND", context));
        return this;
      }
    }

    public SNKit ResetContext ()
    {
      base.Reset ();
      foreach (var item in SNContexts.Values) {
        item.Reset ();
      }
      return this;
    }

    public ISNContext GetContext (SNContextType context = SNContextType.DEFAULT)
    {
      if (SNContexts.ContainsKey (context)) {
        if (SNContexts [context] == null) {
          SNContexts [context] = new SNContext ();
          return SNContexts [context];
        } else {
          return SNContexts [context];
        }
      } else {
        SNContexts.Add (context, new SNContext ());
        return SNContexts [context];
      }
    }


    public override ISN Reset ()
    {
      if (SNContexts != null) {
        SNContexts.Clear ();
      } else {
        SNContexts = new Dictionary<SNContextType, ISNContext> ();
      }
      return this;
    }


    public ISNContext this [SNContextType context] {
      get { return GetContext (context); }
    }


  }
}
