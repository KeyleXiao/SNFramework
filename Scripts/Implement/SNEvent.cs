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

        public ISNContext ParentContext { get; set; }

        public Dictionary<Type, Dictionary<string, Delegate>> SNTable { get; set; }

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

        public ISNEvent Register<T>(string name, Action<T> handler)
        {
            return Register(name, (Delegate)handler);
        }

        public ISNEvent Register<T, U>(string name, Action<T, U> handler)
        {
            return Register(name, (Delegate)handler);
        }

        public ISNEvent Register<T, U, V>(string name, Action<T, U, V> handler)
        {
            return Register(name, (Delegate)handler);
        }

        public ISNEvent Register<T, U, V, W>(string name, Action<T, U, V, W> handler)
        {
            return Register(name, (Delegate)handler);
        }

        public ISNEvent Register<T, U, V, W, X>(string name, Action<T, U, V, W, X> handler)
        {
            return Register(name, (Delegate)handler);
        }

        public ISNEvent Register<TResult>(string name, System.Func<TResult> handler)
        {
            return Register(name, (Delegate)handler);
        }

        public ISNEvent Register<T, TResult>(string name, System.Func<T, TResult> handler)
        {
            return Register(name, (Delegate)handler);
        }

        public ISNEvent Register<T, U, TResult>(string name, System.Func<T, U, TResult> handler)
        {
            return Register(name, (Delegate)handler);
        }

        public ISNEvent Register<T, U, V, TResult>(string name, System.Func<T, U, V, TResult> handler)
        {
            return Register(name, (Delegate)handler);
        }

        public ISNEvent Register<T, U, V, W, TResult>(string name, System.Func<T, U, V, W, TResult> handler)
        {
            return Register(name, (Delegate)handler);
        }

        public ISNEvent Register<T, U, V, W, X, TResult>(string name, Func<T, U, V, W, X, TResult> handler)
        {
            return Register(name, (Delegate)handler);
        }


        public ISNEvent Dispatch(string name)
        {
            System.Action action = GetDelegate(name, typeof(System.Action)) as System.Action;
            if (action != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        action();
                        HandleAutoRelease();
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext);
                        throw;
                    }
                }
                else
                {
                    action();
                    HandleAutoRelease();
                }
            }
            return this;
        }

        public ISNEvent Dispatch<T>(string name, T arg1)
        {
            Action<T> action = GetDelegate(name, typeof(Action<T>)) as Action<T>;
            if (action != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        action(arg1);
                        HandleAutoRelease();
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext, new object[] { arg1 });
                        throw;
                    }
                }
                else
                {
                    action(arg1);
                    HandleAutoRelease();
                }
            }
            return this;
        }

        public ISNEvent Dispatch<T, U>(string name, T arg1, U arg2)
        {
            Action<T, U> action = GetDelegate(name, typeof(Action<T, U>)) as Action<T, U>;
            if (action != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        action(arg1, arg2);
                        HandleAutoRelease();
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext, new object[] { arg1, arg2 });
                        throw;
                    }
                }
                else
                {
                    action(arg1, arg2);
                    HandleAutoRelease();
                }
            }
            return this;
        }

        public ISNEvent Dispatch<T, U, V>(string name, T arg1, U arg2, V arg3)
        {
            Action<T, U, V> action = GetDelegate(name, typeof(Action<T, U, V>)) as Action<T, U, V>;
            if (action != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        action(arg1, arg2, arg3);
                        HandleAutoRelease();
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext, new object[] { arg1, arg2, arg3 });
                        throw;
                    }
                }
                else
                {
                    action(arg1, arg2, arg3);
                    HandleAutoRelease();
                }
            }
            return this;
        }

        public ISNEvent Dispatch<T, U, V, W>(string name, T arg1, U arg2, V arg3, W arg4)
        {
            Action<T, U, V, W> action = GetDelegate(name, typeof(Action<T, U, V, W>)) as Action<T, U, V, W>;
            if (action != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        action(arg1, arg2, arg3, arg4);
                        HandleAutoRelease();
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext, new object[] { arg1, arg2, arg3, arg4 });
                        throw;
                    }
                }
                else
                {
                    action(arg1, arg2, arg3, arg4);
                    HandleAutoRelease();
                }
            }
            return this;
        }

        public ISNEvent Dispatch<T, U, V, W, X>(string name, T arg1, U arg2, V arg3, W arg4, X arg5)
        {
            Action<T, U, V, W, X> action = GetDelegate(name, typeof(Action<T, U, V, W, X>)) as Action<T, U, V, W, X>;
            if (action != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        action(arg1, arg2, arg3, arg4, arg5);
                        HandleAutoRelease();
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext, new object[] { arg1, arg2, arg3, arg4, arg5 });
                        throw;
                    }
                }
                else
                {
                    action(arg1, arg2, arg3, arg4, arg5);
                    HandleAutoRelease();
                }
            }
            return this;
        }

        public async Task<ISNEvent> DispatchAsync(string name)
        {
            // 首先尝试获取Func<Task>类型的委托（异步方法）
            var funcTask = GetDelegate(name, typeof(System.Func<Task>)) as System.Func<Task>;
            if (funcTask != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        await funcTask();
                        HandleAutoRelease();
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext);
                        throw;
                    }
                }
                else
                {
                    await funcTask();
                    HandleAutoRelease();
                }
                return this;
            }

            // 如果没有找到Func<Task>，尝试Action类型的委托（同步方法）
            System.Action action = GetDelegate(name, typeof(System.Action)) as System.Action;
            if (action != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        await Task.Run(action);
                        HandleAutoRelease();
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext);
                        throw;
                    }
                }
                else
                {
                    await Task.Run(action);
                    HandleAutoRelease();
                }
            }
            return this;
        }

        public async Task<ISNEvent> DispatchAsync<T>(string name, T arg1)
        {
            Action<T> action = GetDelegate(name, typeof(Action<T>)) as Action<T>;
            if (action != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        await Task.Run(() => action(arg1));
                        HandleAutoRelease();
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext, new object[] { arg1 });
                        throw;
                    }
                }
                else
                {
                    await Task.Run(() => action(arg1));
                    HandleAutoRelease();
                }
            }
            return this;
        }

        public async Task<ISNEvent> DispatchAsync<T, U>(string name, T arg1, U arg2)
        {
            Action<T, U> action = GetDelegate(name, typeof(Action<T, U>)) as Action<T, U>;
            if (action != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        await Task.Run(() => action(arg1, arg2));
                        HandleAutoRelease();
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext, new object[] { arg1, arg2 });
                        throw;
                    }
                }
                else
                {
                    await Task.Run(() => action(arg1, arg2));
                    HandleAutoRelease();
                }
            }
            return this;
        }

        public async Task<ISNEvent> DispatchAsync<T, U, V>(string name, T arg1, U arg2, V arg3)
        {
            Action<T, U, V> action = GetDelegate(name, typeof(Action<T, U, V>)) as Action<T, U, V>;
            if (action != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        await Task.Run(() => action(arg1, arg2, arg3));
                        HandleAutoRelease();
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext, new object[] { arg1, arg2, arg3 });
                        throw;
                    }
                }
                else
                {
                    await Task.Run(() => action(arg1, arg2, arg3));
                    HandleAutoRelease();
                }
            }
            return this;
        }

        public async Task<ISNEvent> DispatchAsync<T, U, V, W>(string name, T arg1, U arg2, V arg3, W arg4)
        {
            Action<T, U, V, W> action = GetDelegate(name, typeof(Action<T, U, V, W>)) as Action<T, U, V, W>;
            if (action != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        await Task.Run(() => action(arg1, arg2, arg3, arg4));
                        HandleAutoRelease();
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext, new object[] { arg1, arg2, arg3, arg4 });
                        throw;
                    }
                }
                else
                {
                    await Task.Run(() => action(arg1, arg2, arg3, arg4));
                    HandleAutoRelease();
                }
            }
            return this;
        }

        public async Task<ISNEvent> DispatchAsync<T, U, V, W, X>(string name, T arg1, U arg2, V arg3, W arg4, X arg5)
        {
            Action<T, U, V, W, X> action = GetDelegate(name, typeof(Action<T, U, V, W, X>)) as Action<T, U, V, W, X>;
            if (action != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        await Task.Run(() => action(arg1, arg2, arg3, arg4, arg5));
                        HandleAutoRelease();
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext, new object[] { arg1, arg2, arg3, arg4, arg5 });
                        throw;
                    }
                }
                else
                {
                    await Task.Run(() => action(arg1, arg2, arg3, arg4, arg5));
                    HandleAutoRelease();
                }
            }
            return this;
        }

        public bool DispatchHasReturn<TResult>(string name, ref TResult result)
        {
            System.Func<TResult> func = GetDelegate(name, typeof(System.Func<TResult>)) as System.Func<TResult>;
            if (func != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        result = func();
                        HandleAutoRelease();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext);
                        throw;
                    }
                }
                else
                {
                    result = func();
                    HandleAutoRelease();
                    return true;
                }
            }
            return false;
        }

        public bool DispatchHasReturn<T, TResult>(string name, T arg1, ref TResult result)
        {
            System.Func<T, TResult> func = GetDelegate(name, typeof(System.Func<T, TResult>)) as System.Func<T, TResult>;
            if (func != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        result = func(arg1);
                        HandleAutoRelease();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext, new object[] { arg1 });
                        throw;
                    }
                }
                else
                {
                    result = func(arg1);
                    HandleAutoRelease();
                    return true;
                }
            }
            return false;
        }

        public bool DispatchHasReturn<T, U, TResult>(string name, T arg1, U arg2, ref TResult result)
        {
            System.Func<T, U, TResult> func = GetDelegate(name, typeof(System.Func<T, U, TResult>)) as System.Func<T, U, TResult>;
            if (func != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        result = func(arg1, arg2);
                        HandleAutoRelease();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext, new object[] { arg1, arg2 });
                        throw;
                    }
                }
                else
                {
                    result = func(arg1, arg2);
                    HandleAutoRelease();
                    return true;
                }
            }
            return false;
        }

        public bool DispatchHasReturn<T, U, V, TResult>(string name, T arg1, U arg2, V arg3, ref TResult result)
        {
            System.Func<T, U, V, TResult> func = GetDelegate(name, typeof(System.Func<T, U, V, TResult>)) as System.Func<T, U, V, TResult>;
            if (func != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        result = func(arg1, arg2, arg3);
                        HandleAutoRelease();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext, new object[] { arg1, arg2, arg3 });
                        throw;
                    }
                }
                else
                {
                    result = func(arg1, arg2, arg3);
                    HandleAutoRelease();
                    return true;
                }
            }
            return false;
        }

        public bool DispatchHasReturn<T, U, V, W, TResult>(string name, T arg1, U arg2, V arg3, W arg4, ref TResult result)
        {
            System.Func<T, U, V, W, TResult> func = GetDelegate(name, typeof(System.Func<T, U, V, W, TResult>)) as System.Func<T, U, V, W, TResult>;
            if (func != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        result = func(arg1, arg2, arg3, arg4);
                        HandleAutoRelease();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext, new object[] { arg1, arg2, arg3, arg4 });
                        throw;
                    }
                }
                else
                {
                    result = func(arg1, arg2, arg3, arg4);
                    HandleAutoRelease();
                    return true;
                }
            }
            return false;
        }

        public bool DispatchHasReturn<T, U, V, W, X, TResult>(string name, T arg1, U arg2, V arg3, W arg4, X arg5, ref TResult result)
        {
            Func<T, U, V, W, X, TResult> func = GetDelegate(name, typeof(Func<T, U, V, W, X, TResult>)) as Func<T, U, V, W, X, TResult>;
            if (func != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        result = func(arg1, arg2, arg3, arg4, arg5);
                        HandleAutoRelease();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext, new object[] { arg1, arg2, arg3, arg4, arg5 });
                        throw;
                    }
                }
                else
                {
                    result = func(arg1, arg2, arg3, arg4, arg5);
                    HandleAutoRelease();
                    return true;
                }
            }
            return false;
        }

        public async Task<TResult> DispatchHasReturnAsync<TResult>(string name)
        {
            var func = GetDelegate(name, typeof(System.Func<TResult>)) as System.Func<TResult>;
            if (func != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        var result = await Task.Run(func);
                        HandleAutoRelease();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext);
                        throw;
                    }
                }
                else
                {
                    var result = await Task.Run(func);
                    HandleAutoRelease();
                    return result;
                }
            }
            return default(TResult);
        }

        public async Task<TResult> DispatchHasReturnAsync<T, TResult>(string name, T arg1)
        {
            var func = GetDelegate(name, typeof(System.Func<T, TResult>)) as System.Func<T, TResult>;
            if (func != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        var result = await Task.Run(() => func(arg1));
                        HandleAutoRelease();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext, new object[] { arg1 });
                        throw;
                    }
                }
                else
                {
                    var result = await Task.Run(() => func(arg1));
                    HandleAutoRelease();
                    return result;
                }
            }
            return default(TResult);
        }

        public async Task<TResult> DispatchHasReturnAsync<T, U, TResult>(string name, T arg1, U arg2)
        {
            var func = GetDelegate(name, typeof(System.Func<T, U, TResult>)) as System.Func<T, U, TResult>;
            if (func != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        var result = await Task.Run(() => func(arg1, arg2));
                        HandleAutoRelease();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext, new object[] { arg1, arg2 });
                        throw;
                    }
                }
                else
                {
                    var result = await Task.Run(() => func(arg1, arg2));
                    HandleAutoRelease();
                    return result;
                }
            }
            return default(TResult);
        }

        public async Task<TResult> DispatchHasReturnAsync<T, U, V, TResult>(string name, T arg1, U arg2, V arg3)
        {
            var func = GetDelegate(name, typeof(System.Func<T, U, V, TResult>)) as System.Func<T, U, V, TResult>;
            if (func != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        var result = await Task.Run(() => func(arg1, arg2, arg3));
                        HandleAutoRelease();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext, new object[] { arg1, arg2, arg3 });
                        throw;
                    }
                }
                else
                {
                    var result = await Task.Run(() => func(arg1, arg2, arg3));
                    HandleAutoRelease();
                    return result;
                }
            }
            return default(TResult);
        }

        public async Task<TResult> DispatchHasReturnAsync<T, U, V, W, TResult>(string name, T arg1, U arg2, V arg3, W arg4)
        {
            var func = GetDelegate(name, typeof(System.Func<T, U, V, W, TResult>)) as System.Func<T, U, V, W, TResult>;
            if (func != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        var result = await Task.Run(() => func(arg1, arg2, arg3, arg4));
                        HandleAutoRelease();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext, new object[] { arg1, arg2, arg3, arg4 });
                        throw;
                    }
                }
                else
                {
                    var result = await Task.Run(() => func(arg1, arg2, arg3, arg4));
                    HandleAutoRelease();
                    return result;
                }
            }
            return default(TResult);
        }

        public async Task<TResult> DispatchHasReturnAsync<T, U, V, W, X, TResult>(string name, T arg1, U arg2, V arg3, W arg4, X arg5)
        {
            var func = GetDelegate(name, typeof(Func<T, U, V, W, X, TResult>)) as Func<T, U, V, W, X, TResult>;
            if (func != null)
            {
                if (SNConfig.EnableErrorTracking)
                {
                    try
                    {
                        var result = await Task.Run(() => func(arg1, arg2, arg3, arg4, arg5));
                        HandleAutoRelease();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        SNErrorTracker.Instance.TrackError(ex, name, ParentContext, new object[] { arg1, arg2, arg3, arg4, arg5 });
                        throw;
                    }
                }
                else
                {
                    var result = await Task.Run(() => func(arg1, arg2, arg3, arg4, arg5));
                    HandleAutoRelease();
                    return result;
                }
            }
            return default(TResult);
        }

        public override ISN Reset()
        {
            AutoRelease = false;
            ParentContext = null;

            base.Reset();
            if (SNTable != null)
            {
                foreach (var dict in SNTable.Values)
                {
                    dict.Clear();
                }
                SNTable.Clear();
            }
            else
            {
                SNTable = new Dictionary<Type, Dictionary<string, Delegate>>(8);
            }
            return this;
        }

        private void HandleAutoRelease()
        {
            if (AutoRelease)
            {
                // 从上下文中注销并回收到对象池
                ParentContext?.Release(this);
            }
        }



        //ISNEvent ISNEvent.Release(string eventName, Delegate handler)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
