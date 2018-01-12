//if you hava any problem please email to keyle_xiao@hotmail.com
using System;
using System.Collections.Generic;

namespace SNFramework
{
  public interface ISNKit:ISN
  {
    Dictionary<int, ISNQueue> SNGroup { get; set; }

    ISNQueue GetGroup (int poolID = 0);

    SNKit ForceReset (int id = 0);

    SNKit RemoveQueue (int poolID);

    SNKit Add (Action m);

    SNKit Add<T> (Action<T> m);

    SNKit Add<T, U> (Action<T, U> m);

    SNKit Add<T, U, V> (Action<T, U, V> m);

    SNKit Add<T, U, V, W> (Action<T, U, V, W> m);

    SNKit Add<T, U, V, W, X> (Action<T, U, V, W, X> m);

    SNKit Add<TResult> (Func<TResult> m);

    SNKit Add<T, TResult> (Func<T, TResult> m);

    SNKit Add<T, U, TResult> (Func<T, U, TResult> m);

    SNKit Add<T, U, V, TResult> (Func<T, U, V, TResult> m);

    SNKit Add<T, U, V, W, TResult> (Func<T, U, V, W, TResult> m);

    SNKit Add<T, U, V, W, X, TResult> (Func<T, U, V, W, X, TResult> m);
  }
}