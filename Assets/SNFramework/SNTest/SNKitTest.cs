//if you hava any problem please email to keyle_xiao@hotmail.com
using System;
using SNFramework;
using UnityEngine.TestTools;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace SNTest
{
  public class SNKitTest
  {
    [SNMethod (SNMsg.ExampleMsg)]
    public void Test ()
    {
      TestInt = 1;
    }

    [SNMethod (SNMsg.ExampleMsg)]
    public void Test1 (int a)
    {
      TestInt = a;
    }

    [SNMethod (SNMsg.ExampleMsg)]
    public void Test2 (int a, int b)
    {
      TestInt = a + b;
    }

    [SNMethod (SNMsg.ExampleMsg)]
    public void Test3 (int a, int b, int c)
    {
      TestInt = a + b + c;
    }

    [SNMethod (SNMsg.ExampleMsg)]
    public void Test4 (int a, int b, int c, int d)
    {
      TestInt = a + b + c + d;
    }

    [SNMethod (SNMsg.ExampleMsg)]
    public void Test5 (int a, int b, int c, int d, int e)
    {
      TestInt = a + b + c + d + e;
    }


    [SNMethod (SNMsg.ExampleMsg)]
    public int TestHasReturn ()
    {
      return TestInt = 1;
    }

    [SNMethod (SNMsg.ExampleMsg)]
    public int TestHasReturn1 (int a)
    {
      return TestInt = a;
    }

    [SNMethod (SNMsg.ExampleMsg)]
    public int TestHasReturn2 (int a, int b)
    {
      return TestInt = a + b;
    }

    [SNMethod (SNMsg.ExampleMsg)]
    public int TestHasReturn3 (int a, int b, int c)
    {
      return TestInt = a + b + c;
    }

    [SNMethod (SNMsg.ExampleMsg)]
    public int TestHasReturn4 (int a, int b, int c, int d)
    {
      return TestInt = a + b + c + d;
    }

    [SNMethod (SNMsg.ExampleMsg)]
    public int TestHasReturn5 (int a, int b, int c, int d, int e)
    {
      return TestInt = a + b + c + d + e;
    }


    public int TestInt { get; set; }

    [Test]
    public void AddTest ()
    {
      SNKit.Instance.Reset ();
      SNKit.Instance.Register (Test);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg);
      Assert.AreEqual (1, TestInt);
    }

    [Test]
    public void AddTest1 ()
    {
      SNKit.Instance.Reset ();
      SNKit.Instance.Register<int> (Test1);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1);
      Assert.AreEqual (1, TestInt);
    }

    [Test]
    public void AddTest2 ()
    {
      SNKit.Instance.Reset ();
      SNKit.Instance.Register<int, int> (Test2);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1);
      Assert.AreEqual (2, TestInt);
    }

    [Test]
    public void AddTest3 ()
    {
      SNKit.Instance.Reset ();
      SNKit.Instance.Register<int, int, int> (Test3);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1);
      Assert.AreEqual (3, TestInt);
    }

    [Test]
    public void AddTest4 ()
    {
      SNKit.Instance.Reset ();
      SNKit.Instance.Register<int, int, int, int> (Test4);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, 1);
      Assert.AreEqual (4, TestInt);
    }



    [Test]
    public void AddTest5 ()
    {
      SNKit.Instance.Reset ();
      SNKit.Instance.Register<int, int, int, int, int> (Test5);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, 1, 1);
      Assert.AreEqual (5, TestInt);
    }

    // --------------------------------------

    [Test]
    public void AddTestHasReturn ()
    {
      SNKit.Instance.Reset ();
      SNKit.Instance.Register<int> (TestHasReturn);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      TestInt = SNKit.Instance.DispatchHasReturn<int> (SNMsg.ExampleMsg);
      Assert.AreEqual (1, TestInt);
    }

    [Test]
    public void AddHasReturnTest1 ()
    {
      SNKit.Instance.Reset ();
      SNKit.Instance.Register<int,int> (TestHasReturn1);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      TestInt = SNKit.Instance.DispatchHasReturn<int,int> (SNMsg.ExampleMsg, 1);
      Assert.AreEqual (1, TestInt);
    }

    [Test]
    public void AddHasReturnTest2 ()
    {
      SNKit.Instance.Reset ();
      SNKit.Instance.Register<int, int,int> (TestHasReturn2);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      TestInt = SNKit.Instance.DispatchHasReturn<int, int,int> (SNMsg.ExampleMsg, 1, 1);
      Assert.AreEqual (2, TestInt);
    }

    [Test]
    public void AddHasReturnTest3 ()
    {
      SNKit.Instance.Reset ();
      SNKit.Instance.Register<int, int, int,int> (TestHasReturn3);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      TestInt = SNKit.Instance.DispatchHasReturn<int, int, int,int> (SNMsg.ExampleMsg, 1, 1, 1);
      Assert.AreEqual (3, TestInt);
    }

    [Test]
    public void AddHasReturnTest4 ()
    {
      SNKit.Instance.Reset ();
      SNKit.Instance.Register<int, int, int,int,int> (TestHasReturn4);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      TestInt = SNKit.Instance.DispatchHasReturn<int, int, int,int,int> (SNMsg.ExampleMsg, 1, 1, 1, 1);
      Assert.AreEqual (4, TestInt);
    }



    [Test]
    public void AddHasReturnTest5 ()
    {
      SNKit.Instance.Reset ();
      SNKit.Instance.Register<int, int, int, int,int, int> (TestHasReturn5);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      TestInt = SNKit.Instance.DispatchHasReturn<int, int, int,int,int,int> (SNMsg.ExampleMsg, 1, 1, 1, 1, 1);
      Assert.AreEqual (5, TestInt);
    }

    [Test]
    public void ResetContextTest ()
    {
      SNKit.Instance.Reset ();
      SNKit.Instance.Register (Test);

      Assert.AreEqual ((SNKit.Instance [SNContextLevel.DEFAULT].Context [0] as SN).ResetTimes, 0);

      SNKit.Instance.ResetContext ();
      Assert.AreEqual (SNKit.Instance [SNContextLevel.DEFAULT].Context.Count, 1);
      Assert.AreEqual ((SNKit.Instance [SNContextLevel.DEFAULT]as SN).ResetTimes, 1);
      Assert.AreEqual ((SNKit.Instance [SNContextLevel.DEFAULT].Context [0] as SN).ResetTimes, 1);
    }

  }
}
