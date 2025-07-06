//if you hava any problem please email to keyle_xiao@hotmail.com
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SNFramework
{
  public class SNEvent : SN, ISNEvent
  {
    private static readonly Task<ISNEvent> CompletedEventTask = Task.FromResult<ISNEvent>(null);
    private static readonly Dictionary<Type, Dictionary<string, Delegate>> EmptyTable = new Dictionary<Type, Dictionary<string, Delegate>>();
    
    public SNEvent()
    {
      SNTable = new Dictionary<Type, Dictionary<string, Delegate>>(8); // 预分配初始容量
    }

    /// <summary>
    /// 是否触发一次后自动释放
    /// </summary>
    /// <value><c>true</c> if auto release; otherwise, <c>false</c>.</value>
    public bool AutoRelease { get; set; }

    protected Dictionary<Type, Dictionary<string, Delegate>> SNTable { get; set; }

    public ISNEvent Unregister(string name, Delegate handler)
    {
      if (SNTable == null)
      {
        Reset();
        return this;
      }

      if (SNTable.TryGetValue(handler.GetType(), out var dictionary) && 
          dictionary.TryGetValue(name, out var source))
      {
        dictionary[name] = Delegate.Remove(source, handler);
      }

      return this;
    }

    protected Delegate GetDelegate(string name, Type type)
    {
      if (SNTable != null && 
          SNTable.TryGetValue(type, out var dictionary) && 
          dictionary.TryGetValue(name, out var result))
      {
        return result;
      }
      return null;
    }

    public ISNEvent Register(string name, Delegate handler)
    {
      if (SNTable == null)
      {
        SNTable = new Dictionary<Type, Dictionary<string, Delegate>>(8);
      }
      
      if (!SNTable.TryGetValue(handler.GetType(), out var dictionary))
      {
        dictionary = new Dictionary<string, Delegate>(4);
        SNTable.Add(handler.GetType(), dictionary);
      }
      
      if (dictionary.TryGetValue(name, out var existingDelegate))
      {
        dictionary[name] = Delegate.Combine(existingDelegate, handler);
      }
      else
      {
        dictionary.Add(name, handler);
      }
      return this;
    }

    public ISNEvent Register<T> (string name, Action<T> handler)
    {
      return Register (name, (Delegate)handler);
    }

    public ISNEvent Register<T, U> (string name, Action<T, U> handler)
    {
      return Register (name, (Delegate)handler);
    }

    public ISNEvent Register<T, U, V> (string name, Action<T, U, V> handler)
    {
      return Register (name, (Delegate)handler);
    }

    public ISNEvent Register<T, U, V, W> (string name, Action<T, U, V, W> handler)
    {
      return Register (name, (Delegate)handler);
    }

    public ISNEvent Register<T, U, V, W, X> (string name, Action<T, U, V, W, X> handler)
    {
      return Register (name, (Delegate)handler);
    }

    public ISNEvent Register<TResult> (string name, System.Func<TResult> handler)
    {
      return Register (name, (Delegate)handler);
    }

    public ISNEvent Register<T, TResult> (string name, System.Func<T, TResult> handler)
    {
      return Register (name, (Delegate)handler);
    }
    
    public ISNEvent Register<T, U, TResult> (string name, System.Func<T, U, TResult> handler)
    {
      return Register (name, (Delegate)handler);
    }

    public ISNEvent Register<T, U, V, TResult> (string name, System.Func<T, U, V, TResult> handler)
    {
      return Register (name, (Delegate)handler);
    }

    public ISNEvent Register<T, U, V, W, TResult> (string name, System.Func<T, U, V, W, TResult> handler)
    {
      return Register (name, (Delegate)handler);
    }

    public ISNEvent Register<T, U, V, W, X, TResult> (string name, Func<T, U, V, W, X, TResult> handler)
    {
      return Register (name, (Delegate)handler);
    }

    public ISNEvent Dispatch (string name)
    {
      System.Action action = GetDelegate (name, typeof(System.Action)) as System.Action;
      if (action != null) {
        action ();
        if (AutoRelease)
          Reset ();
      }
      return this;
    }

    public ISNEvent Dispatch<T> (string name, T arg1)
    {
      Action<T> action = GetDelegate (name, typeof(Action<T>)) as Action<T>;
      if (action != null) {
        action (arg1);
        if (AutoRelease)
          Reset ();
      }
      return this;
    }

    public ISNEvent Dispatch<T, U> (string name, T arg1, U arg2)
    {
      Action<T, U> action = GetDelegate (name, typeof(Action<T, U>)) as Action<T, U>;
      if (action != null) {
        action (arg1, arg2);
        if (AutoRelease)
          Reset ();
      }
      return this;
    }

    public ISNEvent Dispatch<T, U, V> (string name, T arg1, U arg2, V arg3)
    {
      Action<T, U, V> action = GetDelegate (name, typeof(Action<T, U, V>)) as Action<T, U, V>;
      if (action != null) {
        action (arg1, arg2, arg3);
        if (AutoRelease)
          Reset ();
      }
      return this;
    }

    public ISNEvent Dispatch<T, U, V, W> (string name, T arg1, U arg2, V arg3, W arg4)
    {
      Action<T, U, V, W> action = GetDelegate (name, typeof(Action<T, U, V, W>)) as Action<T, U, V, W>;
      if (action != null) {
        action (arg1, arg2, arg3, arg4);
        if (AutoRelease)
          Reset ();
      }
      return this;
    }

    public ISNEvent Dispatch<T, U, V, W, X> (string name, T arg1, U arg2, V arg3, W arg4, X arg5)
    {
      Action<T, U, V, W, X> action = GetDelegate (name, typeof(Action<T, U, V, W, X>)) as Action<T, U, V, W, X>;
      if (action != null) {
        action (arg1, arg2, arg3, arg4, arg5);
        if (AutoRelease)
          Reset ();
      }
      return this;
    }

    public Task<ISNEvent> DispatchAsync(string name)
    {
      Dispatch(name);
      return CompletedEventTask;
    }

    public Task<ISNEvent> DispatchAsync<T>(string name, T arg1)
    {
      Dispatch(name, arg1);
      return CompletedEventTask;
    }

    public Task<ISNEvent> DispatchAsync<T, U>(string name, T arg1, U arg2)
    {
      Dispatch(name, arg1, arg2);
      return CompletedEventTask;
    }

    public Task<ISNEvent> DispatchAsync<T, U, V>(string name, T arg1, U arg2, V arg3)
    {
      Dispatch(name, arg1, arg2, arg3);
      return CompletedEventTask;
    }

    public Task<ISNEvent> DispatchAsync<T, U, V, W>(string name, T arg1, U arg2, V arg3, W arg4)
    {
      Dispatch(name, arg1, arg2, arg3, arg4);
      return CompletedEventTask;
    }

    public Task<ISNEvent> DispatchAsync<T, U, V, W, X>(string name, T arg1, U arg2, V arg3, W arg4, X arg5)
    {
      Dispatch(name, arg1, arg2, arg3, arg4, arg5);
      return CompletedEventTask;
    }

    public bool DispatchHasReturn<TResult> (string name, ref TResult result)
    {
      System.Func<TResult> func = GetDelegate (name, typeof(System.Func<TResult>)) as System.Func<TResult>;
      if (func != null) {
        if (AutoRelease)
          Reset ();

        result = func (); 
        return true;
      }
      return false;
    }

    public bool DispatchHasReturn<T, TResult> (string name, T arg1, ref TResult result)
    {
      System.Func<T, TResult> func = GetDelegate (name, typeof(System.Func<T, TResult>)) as System.Func<T, TResult>;
      if (func != null) {
        if (AutoRelease)
          Reset ();

        result = func (arg1); 
        return true;
      }
      return false;
    }

    public bool DispatchHasReturn<T, U, TResult> (string name, T arg1, U arg2, ref TResult result)
    {
      System.Func<T, U, TResult> func = GetDelegate (name, typeof(System.Func<T, U, TResult>)) as System.Func<T, U, TResult>;
      if (func != null) {
        if (AutoRelease)
          Reset ();

        result = func (arg1, arg2); 
        return true;
      }
      return false;
    }

    public bool DispatchHasReturn<T, U, V, TResult> (string name, T arg1, U arg2, V arg3, ref TResult result)
    {
      System.Func<T, U, V, TResult> func = GetDelegate (name, typeof(System.Func<T, U, V, TResult>)) as System.Func<T, U, V, TResult>;
      if (func != null) {
        if (AutoRelease)
          Reset ();

        result = func (arg1, arg2, arg3); 
        return true;
      }
      return false;
    }

    public bool DispatchHasReturn<T, U, V, W, TResult> (string name, T arg1, U arg2, V arg3, W arg4, ref TResult result)
    {
      System.Func<T, U, V, W, TResult> func = GetDelegate (name, typeof(System.Func<T, U, V, W, TResult>)) as System.Func<T, U, V, W, TResult>;
      if (func != null) {
        if (AutoRelease)
          Reset ();

        result = func (arg1, arg2, arg3, arg4); 
        return true;
      }
      return false;
    }

    public bool DispatchHasReturn<T, U, V, W, X, TResult> (string name, T arg1, U arg2, V arg3, W arg4, X arg5, ref TResult result)
    {
      Func<T, U, V, W, X, TResult> func = GetDelegate (name, typeof(Func<T, U, V, W, X, TResult>)) as Func<T, U, V, W, X, TResult>;

      if (func != null) {
        if (AutoRelease)
          Reset ();

        result = func (arg1, arg2, arg3, arg4, arg5); 
        return true;
      }
      return false;
    }
    
    public async Task<TResult> DispatchHasReturnAsync<TResult>(string name)
    {
      var result = default(TResult);
      DispatchHasReturn(name, ref result);
      return result;
    }

    public async Task<TResult> DispatchHasReturnAsync<T, TResult>(string name, T arg1)
    {
      var result = default(TResult);
      DispatchHasReturn(name,arg1, ref result);
      return result;
    }

    public async Task<TResult> DispatchHasReturnAsync<T, U, TResult>(string name, T arg1, U arg2)
    {
      var result = default(TResult);
      DispatchHasReturn(name, arg1, arg2, ref result);
      return result;
    }

    public async Task<TResult> DispatchHasReturnAsync<T, U, V, TResult>(string name, T arg1, U arg2, V arg3)
    {
      var result = default(TResult);
      DispatchHasReturn(name,arg1,arg2,arg3,ref result);
      return result;
    }

    public async Task<TResult> DispatchHasReturnAsync<T, U, V, W, TResult>(string name, T arg1, U arg2, V arg3, W arg4)
    {
      var result = default(TResult);
      DispatchHasReturn(name,arg1,arg2,arg3,arg4,ref result);
      return result;
    }

    public async Task<TResult> DispatchHasReturnAsync<T, U, V, W, X, TResult>(string name, T arg1, U arg2, V arg3, W arg4, X arg5)
    {
      var result = default(TResult);
      DispatchHasReturn(name,arg1,arg2,arg3,arg4,arg5,ref result);
      return result;
    }

    public override ISN Reset ()
    {
      AutoRelease = false; //设置基础属性的默认值

      base.Reset ();
      if (SNTable != null) {
        foreach(var dict in SNTable.Values)
        {
          dict.Clear();
        }
        SNTable.Clear();
      } else {
        SNTable = new Dictionary<Type, Dictionary<string, Delegate>>(8);
      }
      return this;
    }
  }
}
