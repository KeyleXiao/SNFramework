//if you hava any problem please email to keyle_xiao@hotmail.com
using System;
using System.Collections.Generic;

namespace SNFramework
{
  public interface ISNKit:ISN
  {
    Dictionary<string, ISNContext> SNContexts { get; set; }

    ISNContext GetContext (string context);

    SNKit ResetContext (string context = SNContextLevel.DEFAULT);

    SNKit ResetContext ();

    SNKit Register (Action m);

    SNKit Register<T> (Action<T> m);

    SNKit Register<T, U> (Action<T, U> m);

    SNKit Register<T, U, V> (Action<T, U, V> m);

    SNKit Register<T, U, V, W> (Action<T, U, V, W> m);

    SNKit Register<T, U, V, W, X> (Action<T, U, V, W, X> m);

    SNKit Register<TResult> (Func<TResult> m);

    SNKit Register<T, TResult> (Func<T, TResult> m);

    SNKit Register<T, U, TResult> (Func<T, U, TResult> m);

    SNKit Register<T, U, V, TResult> (Func<T, U, V, TResult> m);

    SNKit Register<T, U, V, W, TResult> (Func<T, U, V, W, TResult> m);

    SNKit Register<T, U, V, W, X, TResult> (Func<T, U, V, W, X, TResult> m);
  }
}