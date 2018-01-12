//if you hava any problem please email to keyle_xiao@hotmail.com
using System;
using System.Collections.Generic;

namespace SNFramework
{
  public class SNEvent : SN,ISNEvent
  {
    public SNEvent () : base ()
    {
      SNTable = new Dictionary<Type, Dictionary<string, Delegate>> ();
    }

    public string SEventName { get; set; }

    public Dictionary<Type, Dictionary<string, Delegate>> SNTable { get; set; }

    public ISNEvent UnregisterSNEvent (string name, Delegate handler)
    {
      if (SNTable == null)
        return null;

      Dictionary<string, Delegate> dictionary;
      Delegate source;
      if (SNTable.TryGetValue (handler.GetType (), out dictionary) && dictionary.TryGetValue (name, out source)) {
        dictionary [name] = Delegate.Remove (source, handler);
      }

      return this;
    }

    protected Delegate GetDelegate (string name, Type type)
    {
      Dictionary<string, Delegate> dictionary;
      Delegate result;
      if (SNTable != null && SNTable.TryGetValue (type, out dictionary) && dictionary.TryGetValue (name, out result)) {
        return result;
      }
      return null;
    }

    public ISNEvent RegisterSNEvent (string name, Delegate handler)
    {
      if (SNTable == null) {
        SNTable = new Dictionary<Type, Dictionary<string, Delegate>> ();
      }
      Dictionary<string, Delegate> dictionary;
      if (!SNTable.TryGetValue (handler.GetType (), out dictionary)) {
        dictionary = new Dictionary<string, Delegate> ();
        SNTable.Add (handler.GetType (), dictionary);
      }
      Delegate a;
      if (dictionary.TryGetValue (name, out a)) {
        dictionary [name] = Delegate.Combine (a, handler);
      } else {
        dictionary.Add (name, handler);
      }
      return this;
    }

    public ISNEvent RegisterSNEvent<T> (string name, Action<T> handler)
    {
      return RegisterSNEvent (name, (Delegate)handler);
    }

    public ISNEvent RegisterSNEvent<T, U> (string name, Action<T, U> handler)
    {
      return RegisterSNEvent (name, (Delegate)handler);
    }

    public ISNEvent RegisterSNEvent<T, U, V> (string name, Action<T, U, V> handler)
    {
      return RegisterSNEvent (name, (Delegate)handler);
    }

    public ISNEvent RegisterSNEvent<T, U, V, W> (string name, Action<T, U, V, W> handler)
    {
      return RegisterSNEvent (name, (Delegate)handler);
    }

    public ISNEvent RegisterSNEvent<T, U, V, W, X> (string name, Action<T, U, V, W, X> handler)
    {
      return RegisterSNEvent (name, (Delegate)handler);
    }

    public ISNEvent RegisterSNEvent<TResult> (string name, System.Func<TResult> handler)
    {
      return RegisterSNEvent (name, (Delegate)handler);
    }

    public ISNEvent RegisterSNEvent<T, TResult> (string name, System.Func<T, TResult> handler)
    {
      return RegisterSNEvent (name, (Delegate)handler);
    }

    public ISNEvent RegisterSNEvent<T, U, TResult> (string name, System.Func<T, U, TResult> handler)
    {
      return RegisterSNEvent (name, (Delegate)handler);
    }

    public ISNEvent RegisterSNEvent<T, U, V, TResult> (string name, System.Func<T, U, V, TResult> handler)
    {
      return RegisterSNEvent (name, (Delegate)handler);
    }

    public ISNEvent RegisterSNEvent<T, U, V, W, TResult> (string name, System.Func<T, U, V, W, TResult> handler)
    {
      return RegisterSNEvent (name, (Delegate)handler);
    }

    public ISNEvent RegisterSNEvent<T, U, V, W, X, TResult> (string name, Func<T, U, V, W, X, TResult> handler)
    {
      return RegisterSNEvent (name, (Delegate)handler);
    }

    public ISNEvent SendSNEvent (string name)
    {
      System.Action action = GetDelegate (name, typeof(System.Action)) as System.Action;
      if (action != null) {
        action ();
      }
      return this;
    }

    public ISNEvent SendSNEvent<T> (string name, T arg1)
    {
      Action<T> action = GetDelegate (name, typeof(Action<T>)) as Action<T>;
      if (action != null) {
        action (arg1);
      }
      return this;
    }

    public ISNEvent SendSNEvent<T, U> (string name, T arg1, U arg2)
    {
      Action<T, U> action = GetDelegate (name, typeof(Action<T, U>)) as Action<T, U>;
      if (action != null) {
        action (arg1, arg2);
      }
      return this;
    }

    public ISNEvent SendSNEvent<T, U, V> (string name, T arg1, U arg2, V arg3)
    {
      Action<T, U, V> action = GetDelegate (name, typeof(Action<T, U, V>)) as Action<T, U, V>;
      if (action != null) {
        action (arg1, arg2, arg3);
      }
      return this;
    }

    public ISNEvent SendSNEvent<T, U, V, W> (string name, T arg1, U arg2, V arg3, W arg4)
    {
      Action<T, U, V, W> action = GetDelegate (name, typeof(Action<T, U, V, W>)) as Action<T, U, V, W>;
      if (action != null) {
        action (arg1, arg2, arg3, arg4);
      }
      return this;
    }

    public ISNEvent SendSNEvent<T, U, V, W, X> (string name, T arg1, U arg2, V arg3, W arg4, X arg5)
    {
      Action<T, U, V, W, X> action = GetDelegate (name, typeof(Action<T, U, V, W, X>)) as Action<T, U, V, W, X>;
      if (action != null) {
        action (arg1, arg2, arg3, arg4, arg5);
      }
      return this;
    }

    public TResult SendSNEventHasReturn<TResult> (string name)
    {
      System.Func<TResult> func = GetDelegate (name, typeof(System.Func<TResult>)) as System.Func<TResult>;
      if (func != null) {
        return func ();
      }
      return default(TResult);
    }

    public TResult SendSNEventHasReturn<T, TResult> (string name, T arg1)
    {
      System.Func<T, TResult> func = GetDelegate (name, typeof(System.Func<T, TResult>)) as System.Func<T, TResult>;
      if (func != null) {
        return func (arg1);
      }
      return default(TResult);
    }

    public TResult SendSNEventHasReturn<T, U, TResult> (string name, T arg1, U arg2)
    {
      System.Func<T, U, TResult> func = GetDelegate (name, typeof(System.Func<T, U, TResult>)) as System.Func<T, U, TResult>;
      if (func != null) {
        return func (arg1, arg2);
      }
      return default(TResult);
    }

    public TResult SendSNEventHasReturn<T, U, V, TResult> (string name, T arg1, U arg2, V arg3)
    {
      System.Func<T, U, V, TResult> func = GetDelegate (name, typeof(System.Func<T, U, V, TResult>)) as System.Func<T, U, V, TResult>;
      if (func != null) {
        return func (arg1, arg2, arg3);
      }
      return default(TResult);
    }

    public TResult SendSNEventHasReturn<T, U, V, W, TResult> (string name, T arg1, U arg2, V arg3, W arg4)
    {
      System.Func<T, U, V, W, TResult> func = GetDelegate (name, typeof(System.Func<T, U, V, W, TResult>)) as System.Func<T, U, V, W, TResult>;
      if (func != null) {
        return func (arg1, arg2, arg3, arg4);
      }
      return default(TResult);
    }

    public TResult SendSNEventHasReturn<T, U, V, W, X, TResult> (string name, T arg1, U arg2, V arg3, W arg4, X arg5)
    {
      Func<T, U, V, W, X, TResult> func = GetDelegate (name, typeof(Func<T, U, V, W, X, TResult>)) as Func<T, U, V, W, X, TResult>;

      if (func != null) {
        return func (arg1, arg2, arg3, arg4, arg5);
      }
      return default(TResult);
    }

    public override ISN Reset ()
    {
      if (SNTable != null) {
        SNTable.Clear ();
      } else {
        SNTable = new Dictionary<Type, Dictionary<string, Delegate>> ();
      }
      return this;
    }
  }
}
