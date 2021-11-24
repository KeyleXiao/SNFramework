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

    SNKit Register (Delegate m, string context = SNContextLevel.DEFAULT);
    
    SNKit Register (Action m, string context = SNContextLevel.DEFAULT);

    SNKit Register<T> (Action<T> m, string context = SNContextLevel.DEFAULT);

    SNKit Register<T, U> (Action<T, U> m, string context = SNContextLevel.DEFAULT);

    SNKit Register<T, U, V> (Action<T, U, V> m, string context = SNContextLevel.DEFAULT);

    SNKit Register<T, U, V, W> (Action<T, U, V, W> m, string context = SNContextLevel.DEFAULT);

    SNKit Register<T, U, V, W, X> (Action<T, U, V, W, X> m, string context = SNContextLevel.DEFAULT);

    SNKit Register<TResult> (Func<TResult> m, string context = SNContextLevel.DEFAULT);

    SNKit Register<T, TResult> (Func<T, TResult> m, string context = SNContextLevel.DEFAULT);

    SNKit Register<T, U, TResult> (Func<T, U, TResult> m, string context = SNContextLevel.DEFAULT);

    SNKit Register<T, U, V, TResult> (Func<T, U, V, TResult> m, string context = SNContextLevel.DEFAULT);

    SNKit Register<T, U, V, W, TResult> (Func<T, U, V, W, TResult> m, string context = SNContextLevel.DEFAULT);

    SNKit Register<T, U, V, W, X, TResult> (Func<T, U, V, W, X, TResult> m, string context = SNContextLevel.DEFAULT);
  }
}