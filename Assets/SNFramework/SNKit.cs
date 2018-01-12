//if you hava any problem please email to keyle_xiao@hotmail.com
using System;
using System.Collections.Generic;

namespace SNFramework
{
  public class SNKit :SN,ISNKit
  {
    public Dictionary<string, ISNContext> SNContexts { get; set; }

    private SNKit () : base ()
    {
      SNContexts = new Dictionary<string, ISNContext> ();
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
          que.Context [i].Register (atr.SNEventName, g);
          return;
        }
      }

      ISNEvent e = new SNEvent ();
      e.IdentifiedSign = atr.SNEventName;
      e.Register (atr.SNEventName, g);
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



    public SNKit Dispatch (string name, string context = SNContextLevel.DEFAULT)
    {
      ISNEvent e = null;
      if (context == SNContextLevel.BOARDCAST) {
        foreach (var item in SNContexts.Values) {
          e = item.GetSNEvent (name);
          if (e != null) {
            e.Dispatch (name);
          }
        }
        return this;
      }

      e = GetContext (context).GetSNEvent (name);
      if (e != null) {
        e.Dispatch (name);
      }
      return this;
    }

    public SNKit Dispatch<T> (string name, T arg1, string context = SNContextLevel.DEFAULT)
    {
      ISNEvent e = null;
      if (context == SNContextLevel.BOARDCAST) {
        foreach (var item in SNContexts.Values) {
          e = item.GetSNEvent (name);
          if (e != null) {
            e.Dispatch (name, arg1);
          }
        }
        return this;
      }

      e = GetContext (context).GetSNEvent (name);
      if (e != null) {
        e.Dispatch (name, arg1);
      }
      return this;
    }

    public SNKit Dispatch<T, U> (string name, T arg1, U arg2, string context = SNContextLevel.DEFAULT)
    {
      ISNEvent e = null;
      if (context == SNContextLevel.BOARDCAST) {
        foreach (var item in SNContexts.Values) {
          e = item.GetSNEvent (name);
          if (e != null) {
            e.Dispatch (name, arg1, arg2);
          }
        }
        return this;
      }

      e = GetContext (context).GetSNEvent (name);
      if (e != null) {
        e.Dispatch (name, arg1, arg2);
      }
      return this;
    }

    public SNKit Dispatch<T, U, V> (string name, T arg1, U arg2, V arg3, string context = SNContextLevel.DEFAULT)
    {
      ISNEvent e = null;
      if (context == SNContextLevel.BOARDCAST) {
        foreach (var item in SNContexts.Values) {
          e = item.GetSNEvent (name);
          if (e != null) {
            e.Dispatch (name, arg1, arg2, arg3);
          }
        }
        return this;
      }

      e = GetContext (context).GetSNEvent (name);
      if (e != null) {
        e.Dispatch (name, arg1, arg2, arg3);
      }
      return this;
    }

    public SNKit Dispatch<T, U, V, W> (string name, T arg1, U arg2, V arg3, W arg4, string context = SNContextLevel.DEFAULT)
    {
      ISNEvent e = null;
      if (context == SNContextLevel.BOARDCAST) {
        foreach (var item in SNContexts.Values) {
          e = item.GetSNEvent (name);
          if (e != null) {
            e.Dispatch (name, arg1, arg2, arg3, arg4);
          }
        }
        return this;
      }

      e = GetContext (context).GetSNEvent (name);
      if (e != null) {
        e.Dispatch (name, arg1, arg2, arg3, arg4);
      }
      return this;
    }

    public SNKit Dispatch<T, U, V, W, X> (string name, T arg1, U arg2, V arg3, W arg4, X arg5, string context = SNContextLevel.DEFAULT)
    {
      ISNEvent e = null;
      if (context == SNContextLevel.BOARDCAST) {
        foreach (var item in SNContexts.Values) {
          e = item.GetSNEvent (name);
          if (e != null) {
            e.Dispatch (name, arg1, arg2, arg3, arg4, arg5);
          }
        }
        return this;
      }
      e = GetContext (context).GetSNEvent (name);
      if (e != null) {
        e.Dispatch (name, arg1, arg2, arg3, arg4, arg5);
      }
      return this;
    }

    public TResult DispatchHasReturn<TResult> (string name, string context = SNContextLevel.DEFAULT)
    {
      ISNEvent e = null;
      if (context == SNContextLevel.BOARDCAST) {
          
        foreach (var item in SNContexts.Values) {
          e = item.GetSNEvent (name);
          if (e != null) {
            return e.DispatchHasReturn <TResult> (name);
          }
        }
        return default(TResult);
      }

      e = GetContext (context).GetSNEvent (name);
      if (e != null) {
        return   e.DispatchHasReturn <TResult> (name);
      }
      return default(TResult);
    }

    public TResult DispatchHasReturn<T, TResult> (string name, T arg1, string context = SNContextLevel.DEFAULT)
    {
      ISNEvent e = null;
      if (context == SNContextLevel.BOARDCAST) {
        foreach (var item in SNContexts.Values) {
          e = item.GetSNEvent (name);
          if (e != null) {
            return  e.DispatchHasReturn<T, TResult> (name, arg1);
          }
        }
        return default(TResult);
      }

      e = GetContext (context).GetSNEvent (name);
      if (e != null) {
        return  e.DispatchHasReturn<T, TResult> (name, arg1);
      }
      return default(TResult);
    }

    public TResult DispatchHasReturn<T, U, TResult> (string name, T arg1, U arg2, string context = SNContextLevel.DEFAULT)
    {
      ISNEvent e = null;
      if (context == SNContextLevel.BOARDCAST) {
        foreach (var item in SNContexts.Values) {
          e = item.GetSNEvent (name);
          if (e != null) {
            return  e.DispatchHasReturn<T, U, TResult> (name, arg1, arg2);
          }
        }
        return default(TResult);
      }

      e = GetContext (context).GetSNEvent (name);
      if (e != null) {
        return  e.DispatchHasReturn<T, U, TResult> (name, arg1, arg2);
      }
      return default(TResult);
    }

    public TResult DispatchHasReturn<T, U, V, TResult> (string name, T arg1, U arg2, V arg3, string context = SNContextLevel.DEFAULT)
    {
      ISNEvent e = null;
      if (context == SNContextLevel.BOARDCAST) {
        foreach (var item in SNContexts.Values) {
          e = item.GetSNEvent (name);
          if (e != null) {
            return  e.DispatchHasReturn <T, U, V, TResult> (name, arg1, arg2, arg3);
          }
        }
        return default(TResult);
      }

      e = GetContext (context).GetSNEvent (name);
      if (e != null) {
        return  e.DispatchHasReturn <T, U, V, TResult> (name, arg1, arg2, arg3);
      }
      return default(TResult);
    }

    public TResult DispatchHasReturn<T, U, V, W, TResult> (string name, T arg1, U arg2, V arg3, W arg4, string context = SNContextLevel.DEFAULT)
    {
      ISNEvent e = null;
      if (context == SNContextLevel.BOARDCAST) {
        foreach (var item in SNContexts.Values) {
          e = item.GetSNEvent (name);
          if (e != null) {
            return  e.DispatchHasReturn<T, U, V, W, TResult> (name, arg1, arg2, arg3, arg4);
          }
        }
        return default(TResult);
      }

      e = GetContext (context).GetSNEvent (name);
      if (e != null) {
        return  e.DispatchHasReturn<T, U, V, W, TResult> (name, arg1, arg2, arg3, arg4);
      }
      return default(TResult);
    }

    public TResult DispatchHasReturn<T, U, V, W, X, TResult> (string name, T arg1, U arg2, V arg3, W arg4, X arg5, string context = SNContextLevel.DEFAULT)
    {
      ISNEvent e = null;
      if (context == SNContextLevel.BOARDCAST) {
        foreach (var item in SNContexts.Values) {
          e = item.GetSNEvent (name);
          if (e != null) {
            return e.DispatchHasReturn <T, U, V, W, X, TResult> (name, arg1, arg2, arg3, arg4, arg5);
          }
        }
        return default(TResult);
      }

      e = GetContext (context).GetSNEvent (name);
      if (e != null) {
        return e.DispatchHasReturn <T, U, V, W, X, TResult> (name, arg1, arg2, arg3, arg4, arg5);
      }
      return default(TResult);
    }


    public SNKit ResetContext (string context = SNContextLevel.DEFAULT)
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

    public ISNContext GetContext (string context = SNContextLevel.DEFAULT)
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
        SNContexts = new Dictionary<string, ISNContext> ();
      }
      return this;
    }


    public ISNContext this [string context] {
      get { return GetContext (context); }
    }


  }
}
