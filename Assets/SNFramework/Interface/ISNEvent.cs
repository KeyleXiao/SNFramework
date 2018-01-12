//if you hava any problem please email to keyle_xiao@hotmail.com
using System;
using System.Collections.Generic;

namespace SNFramework
{
    public interface ISNEvent : ISN
    {
        string SEventName { get; set; }
        Dictionary<Type, Dictionary<string, Delegate>> SNTable { get; set; }

        ISNEvent RegisterSNEvent(string name, Delegate handler);
        ISNEvent RegisterSNEvent<T>(string name, Action<T> handler);
        ISNEvent RegisterSNEvent<T, U>(string name, Action<T, U> handler);
        ISNEvent RegisterSNEvent<T, U, V>(string name, Action<T, U, V> handler);
        ISNEvent RegisterSNEvent<T, U, V, W>(string name, Action<T, U, V, W> handler);
        ISNEvent RegisterSNEvent<T, U, V, W, X>(string name, Action<T, U, V, W, X> handler);

        ISNEvent RegisterSNEvent<TResult>(string name, System.Func<TResult> handler);
        ISNEvent RegisterSNEvent<T, TResult>(string name, System.Func<T, TResult> handler);
        ISNEvent RegisterSNEvent<T, U, TResult>(string name, System.Func<T, U, TResult> handler);
        ISNEvent RegisterSNEvent<T, U, V, TResult>(string name, System.Func<T, U, V, TResult> handler);
        ISNEvent RegisterSNEvent<T, U, V, W, TResult>(string name, System.Func<T, U, V, W, TResult> handler);
        ISNEvent RegisterSNEvent<T, U, V, W, X, TResult>(string name, Func<T, U, V, W, X, TResult> handler);


        ISNEvent UnregisterSNEvent(string name, Delegate handler);

        ISNEvent SendSNEvent(string name);
        ISNEvent SendSNEvent<T>(string name, T arg1);
        ISNEvent SendSNEvent<T, U>(string name, T arg1, U arg2);
        ISNEvent SendSNEvent<T, U, V>(string name, T arg1, U arg2, V arg3);
        ISNEvent SendSNEvent<T, U, V, W>(string name, T arg1, U arg2, V arg3, W arg4);
        ISNEvent SendSNEvent<T, U, V, W, X>(string name, T arg1, U arg2, V arg3, W arg4, X arg5);



        TResult SendSNEventHasReturn<TResult>(string name);
        TResult SendSNEventHasReturn<T, TResult>(string name, T arg1);
        TResult SendSNEventHasReturn<T, U, TResult>(string name, T arg1, U arg2);
        TResult SendSNEventHasReturn<T, U, V, TResult>(string name, T arg1, U arg2, V arg3);
        TResult SendSNEventHasReturn<T, U, V, W, TResult>(string name, T arg1, U arg2, V arg3, W arg4);
        TResult SendSNEventHasReturn<T, U, V, W, X, TResult>(string name, T arg1, U arg2, V arg3, W arg4, X arg5);


    }
}
