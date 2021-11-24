//if you hava any problem please email to keyle_xiao@hotmail.com
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SNFramework
{
  public interface ISNEvent : ISN
  {
    bool AutoRelease { get; set; }

    ISNEvent Register (string name, Delegate handler);

    ISNEvent Register<T> (string name, Action<T> handler);

    ISNEvent Register<T, U> (string name, Action<T, U> handler);

    ISNEvent Register<T, U, V> (string name, Action<T, U, V> handler);

    ISNEvent Register<T, U, V, W> (string name, Action<T, U, V, W> handler);

    ISNEvent Register<T, U, V, W, X> (string name, Action<T, U, V, W, X> handler);

    ISNEvent Register<TResult> (string name, System.Func<TResult> handler);

    ISNEvent Register<T, TResult> (string name, System.Func<T, TResult> handler);

    ISNEvent Register<T, U, TResult> (string name, System.Func<T, U, TResult> handler);

    ISNEvent Register<T, U, V, TResult> (string name, System.Func<T, U, V, TResult> handler);

    ISNEvent Register<T, U, V, W, TResult> (string name, System.Func<T, U, V, W, TResult> handler);

    ISNEvent Register<T, U, V, W, X, TResult> (string name, Func<T, U, V, W, X, TResult> handler);


    ISNEvent Unregister (string name, Delegate handler);

    ISNEvent Dispatch (string name);

    ISNEvent Dispatch<T> (string name, T arg1);

    ISNEvent Dispatch<T, U> (string name, T arg1, U arg2);

    ISNEvent Dispatch<T, U, V> (string name, T arg1, U arg2, V arg3);

    ISNEvent Dispatch<T, U, V, W> (string name, T arg1, U arg2, V arg3, W arg4);

    ISNEvent Dispatch<T, U, V, W, X> (string name, T arg1, U arg2, V arg3, W arg4, X arg5);
    
    Task<ISNEvent> DispatchAsync (string name);

    Task<ISNEvent> DispatchAsync<T> (string name, T arg1);

    Task<ISNEvent> DispatchAsync<T, U> (string name, T arg1, U arg2);

    Task<ISNEvent> DispatchAsync<T, U, V> (string name, T arg1, U arg2, V arg3);

    Task<ISNEvent> DispatchAsync<T, U, V, W> (string name, T arg1, U arg2, V arg3, W arg4);

    Task<ISNEvent> DispatchAsync<T, U, V, W, X> (string name, T arg1, U arg2, V arg3, W arg4, X arg5);



    bool DispatchHasReturn<TResult> (string name, ref TResult result);

    bool DispatchHasReturn<T, TResult> (string name, T arg1, ref TResult result);

    bool DispatchHasReturn<T, U, TResult> (string name, T arg1, U arg2, ref TResult result);

    bool DispatchHasReturn<T, U, V, TResult> (string name, T arg1, U arg2, V arg3, ref TResult result);

    bool DispatchHasReturn<T, U, V, W, TResult> (string name, T arg1, U arg2, V arg3, W arg4, ref TResult result);

    bool DispatchHasReturn<T, U, V, W, X, TResult> (string name, T arg1, U arg2, V arg3, W arg4, X arg5, ref TResult result);
    
    
    Task<TResult> DispatchHasReturnAsync<TResult> (string name);

    Task<TResult> DispatchHasReturnAsync<T, TResult> (string name, T arg1);

    Task<TResult> DispatchHasReturnAsync<T, U, TResult> (string name, T arg1, U arg2);

    Task<TResult> DispatchHasReturnAsync<T, U, V, TResult> (string name, T arg1, U arg2, V arg3);

    Task<TResult> DispatchHasReturnAsync<T, U, V, W, TResult> (string name, T arg1, U arg2, V arg3, W arg4);

    Task<TResult> DispatchHasReturnAsync<T, U, V, W, X, TResult> (string name, T arg1, U arg2, V arg3, W arg4, X arg5);
  }
}
