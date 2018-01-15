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

    [SNMethod (SNMsg.ExampleMsg, true, SNContextLevel.DEFAULT)]
    public void Test1 (int a)
    {
      TestInt = a;
    }

    [SNMethod (SNMsg.ExampleMsg, true, SNContextLevel.DEFAULT)]
    public void Test2 (int a, int b)
    {
      TestInt += (a + b);
    }

    [SNMethod (SNMsg.ExampleMsg, true, SNContextLevel.DEFAULT)]
    public void Test3 (int a, int b, int c)
    {
      TestInt += (a + b + c);
    }

    [SNMethod (SNMsg.ExampleMsg, true, SNContextLevel.DEFAULT)]
    public void Test4 (int a, int b, int c, int d)
    {
      TestInt += a + b + c + d;
    }

    [SNMethod (SNMsg.ExampleMsg, true, SNContextLevel.DEFAULT)]
    public void Test5 (int a, int b, int c, int d, int e)
    {
      TestInt += a + b + c + d + e;
    }


    [SNMethod (SNMsg.ExampleMsg, true, SNContextLevel.DEFAULT)]
    public int TestHasReturn ()
    {
      return TestInt = 1;
    }

    [SNMethod (SNMsg.ExampleMsg, true, SNContextLevel.DEFAULT)]
    public int TestHasReturn1 (int a)
    {
      return TestInt = a;
    }

    [SNMethod (SNMsg.ExampleMsg, true, SNContextLevel.DEFAULT)]
    public int TestHasReturn2 (int a, int b)
    {
      return TestInt += (a + b);
    }

    [SNMethod (SNMsg.ExampleMsg, true, SNContextLevel.DEFAULT)]
    public int TestHasReturn3 (int a, int b, int c)
    {
      return TestInt += (a + b + c);
    }

    [SNMethod (SNMsg.ExampleMsg, true, SNContextLevel.DEFAULT)]
    public int TestHasReturn4 (int a, int b, int c, int d)
    {
      return TestInt += (a + b + c + d);
    }

    [SNMethod (SNMsg.ExampleMsg, true, SNContextLevel.DEFAULT)]
    public int TestHasReturn5 (int a, int b, int c, int d, int e)
    {
      return TestInt += (a + b + c + d + e);
    }


    public int TestInt { get; set; }


    public void ResetEveryTest ()
    {
      SNKit.Instance.Reset ();
      TestInt = 0;
    }

    [Test]
    public void AddTest ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register (Test);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);
      SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease = false;
      Assert.IsTrue (!SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);


      for (int i = 0; i < 5; i++) {
        SNKit.Instance.Dispatch (SNMsg.ExampleMsg);
        Assert.AreEqual (1, TestInt);
        TestInt = 2;
        SNKit.Instance.Dispatch (SNMsg.ExampleMsg);
        Assert.AreEqual (1, TestInt);
      }
    }

    [Test]
    public void AddTest1Once ()
    {
      ResetEveryTest ();


      SNKit.Instance.Register<int> (Test1);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1);//此函数仅执行一次 所以第二次不会变更TestInt的值

      TestInt = 2;
      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1);//消息只会触发一次 所以现在testint不能再次被改变

      Assert.AreEqual (2, TestInt);
    }

    [Test]
    public void AddTest2 ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int> (Test2);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);
      SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease = false;
      Assert.IsTrue (!SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1);
      Assert.AreEqual (2, TestInt);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1);
      Assert.AreEqual (4, TestInt);
    }

    [Test]
    public void AddTest2Once ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int> (Test2);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);
      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1);
      Assert.AreEqual (2, TestInt);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1);//这里并不会被执行到

      Assert.AreEqual (2, TestInt);
    }

    [Test]
    public void AddTest3 ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int> (Test3);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease = false;
      Assert.IsTrue (!SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1);
      Assert.AreEqual (3, TestInt);


      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1);
      Assert.AreEqual (6, TestInt);
    }


    [Test]
    public void AddTest3Once ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int> (Test3);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1);
      Assert.AreEqual (3, TestInt);

      TestInt = -1;

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1);//方法不会再次被执行
      Assert.AreEqual (-1, TestInt);
    }


    [Test]
    public void AddTest4 ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int, int> (Test4);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease = false;
      Assert.IsTrue (!SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, 1);
      Assert.AreEqual (4, TestInt);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, 1);
      Assert.AreEqual (8, TestInt);
    }



    [Test]
    public void AddTest4Once ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int, int> (Test4);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, 1);
      Assert.AreEqual (4, TestInt);


      TestInt = 2;

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, 1);

      Assert.AreEqual (2, TestInt);
    }


    [Test]
    public void AddTest5 ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int, int, int> (Test5);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease = false;
      Assert.IsTrue (!SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, 1, 1);
      Assert.AreEqual (5, TestInt);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, 1, 1);
      Assert.AreEqual (10, TestInt);
    }

    [Test]
    public void AddTest5Once ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int, int, int> (Test5);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, 1, 1);
      Assert.AreEqual (5, TestInt);

      TestInt = 2;
      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, 1, 1);
      Assert.AreEqual (2, TestInt);
    }

    [Test]
    public void AddTestOnce ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int, int, int> (Test5);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, 1, 1);
      Assert.AreEqual (5, TestInt);

      TestInt = 1;
      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, 1, 1);
      Assert.AreEqual (1, TestInt);
    }

    // --------------------------------------

    [Test]
    public void AddTestHasReturn ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int> (TestHasReturn);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease = false;
      Assert.IsTrue (!SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      TestInt = SNKit.Instance.DispatchHasReturn<int> (SNMsg.ExampleMsg);
      Assert.AreEqual (1, TestInt);

      TestInt = 2;

      TestInt = SNKit.Instance.DispatchHasReturn<int> (SNMsg.ExampleMsg);
      Assert.AreEqual (1, TestInt);
    }

    [Test]
    public void AddTestHasReturnOnce ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int> (TestHasReturn);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      TestInt = SNKit.Instance.DispatchHasReturn<int> (SNMsg.ExampleMsg);
      Assert.AreEqual (1, TestInt);

      TestInt = SNKit.Instance.DispatchHasReturn<int> (SNMsg.ExampleMsg);

      Assert.AreEqual (0, TestInt);
    }

    [Test]
    public void AddHasReturnTest1 ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int> (TestHasReturn1);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease = false;
      Assert.IsTrue (!SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      TestInt = SNKit.Instance.DispatchHasReturn<int, int> (SNMsg.ExampleMsg, 1);
      Assert.AreEqual (1, TestInt);
      TestInt = 0;
      TestInt = SNKit.Instance.DispatchHasReturn<int, int> (SNMsg.ExampleMsg, 1);
      Assert.AreEqual (1, TestInt);
    }


    [Test]
    public void AddHasReturnTest1Once ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int> (TestHasReturn1);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      TestInt = SNKit.Instance.DispatchHasReturn<int, int> (SNMsg.ExampleMsg, 1);
      Assert.AreEqual (1, TestInt);

      TestInt = SNKit.Instance.DispatchHasReturn<int, int> (SNMsg.ExampleMsg, 1);
      Assert.AreEqual (0, TestInt);
    }



    [Test]
    public void AddHasReturnTest2 ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int> (TestHasReturn2);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease = false;
      Assert.IsTrue (!SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      TestInt = SNKit.Instance.DispatchHasReturn<int, int, int> (SNMsg.ExampleMsg, 1, 1);
      Assert.AreEqual (2, TestInt);
      TestInt = 10;
      TestInt = SNKit.Instance.DispatchHasReturn<int, int, int> (SNMsg.ExampleMsg, 1, 1);
      Assert.AreEqual (12, TestInt);
    }

    [Test]
    public void AddHasReturnTest2Once ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int> (TestHasReturn2);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      TestInt = SNKit.Instance.DispatchHasReturn<int, int, int> (SNMsg.ExampleMsg, 1, 1);
      Assert.AreEqual (2, TestInt);

      TestInt = SNKit.Instance.DispatchHasReturn<int, int, int> (SNMsg.ExampleMsg, 1, 1);
      Assert.AreEqual (0, TestInt);
    }

    [Test]
    public void AddHasReturnTest3 ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int, int> (TestHasReturn3);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease = false;
      Assert.IsTrue (!SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);
      TestInt = SNKit.Instance.DispatchHasReturn<int, int, int, int> (SNMsg.ExampleMsg, 1, 1, 1);
      Assert.AreEqual (3, TestInt);

      TestInt = 100;

      TestInt = SNKit.Instance.DispatchHasReturn<int, int, int, int> (SNMsg.ExampleMsg, 1, 1, 1);
      Assert.AreEqual (103, TestInt);
    }


    [Test]
    public void AddHasReturnTest3Once ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int, int> (TestHasReturn3);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);
      TestInt = SNKit.Instance.DispatchHasReturn<int, int, int, int> (SNMsg.ExampleMsg, 1, 1, 1);
      Assert.AreEqual (3, TestInt);

      TestInt = SNKit.Instance.DispatchHasReturn<int, int, int, int> (SNMsg.ExampleMsg, 1, 1, 1);
      Assert.AreEqual (0, TestInt);
    }


    [Test]
    public void AddHasReturnTest4 ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int, int, int> (TestHasReturn4);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease = false;
      Assert.IsTrue (!SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);


      TestInt = SNKit.Instance.DispatchHasReturn<int, int, int, int, int> (SNMsg.ExampleMsg, 1, 1, 1, 1);
      Assert.AreEqual (4, TestInt);

      TestInt = 100; 

      TestInt = SNKit.Instance.DispatchHasReturn<int, int, int, int, int> (SNMsg.ExampleMsg, 1, 1, 1, 1);
      Assert.AreEqual (104, TestInt);
    }

    [Test]
    public void AddHasReturnTest4Once ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int, int, int> (TestHasReturn4);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);


      TestInt = SNKit.Instance.DispatchHasReturn<int, int, int, int, int> (SNMsg.ExampleMsg, 1, 1, 1, 1);
      Assert.AreEqual (4, TestInt);

      TestInt = SNKit.Instance.DispatchHasReturn<int, int, int, int, int> (SNMsg.ExampleMsg, 1, 1, 1, 1);
      Assert.AreEqual (0, TestInt);
    }

    [Test]
    public void AddHasReturnTest5 ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int, int, int, int> (TestHasReturn5);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease = false;
      Assert.IsTrue (!SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      TestInt = SNKit.Instance.DispatchHasReturn<int, int, int, int, int, int> (SNMsg.ExampleMsg, 1, 1, 1, 1, 1);
      Assert.AreEqual (5, TestInt);

      TestInt = SNKit.Instance.DispatchHasReturn<int, int, int, int, int, int> (SNMsg.ExampleMsg, 1, 1, 1, 1, 1);
      Assert.AreEqual (10, TestInt);

      TestInt = SNKit.Instance.DispatchHasReturn<int, int, int, int, int, int> (SNMsg.ExampleMsg, 1, 1, 1, 1, 1);
      Assert.AreEqual (15, TestInt);
    }

    [Test]
    public void AddHasReturnTest5Once ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int, int, int, int> (TestHasReturn5);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      TestInt = SNKit.Instance.DispatchHasReturn<int, int, int, int, int, int> (SNMsg.ExampleMsg, 1, 1, 1, 1, 1);
      Assert.AreEqual (5, TestInt);

      TestInt = SNKit.Instance.DispatchHasReturn<int, int, int, int, int, int> (SNMsg.ExampleMsg, 1, 1, 1, 1, 1);
      Assert.AreEqual (0, TestInt);

      TestInt = SNKit.Instance.DispatchHasReturn<int, int, int, int, int, int> (SNMsg.ExampleMsg, 1, 1, 1, 1, 1);
      Assert.AreEqual (0, TestInt);
    }


    [Test]
    public void ResetContextTest ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register (Test);

      Assert.AreEqual ((SNKit.Instance [SNContextLevel.DEFAULT].Context [0] as SN).ResetTimes, 0);

      SNKit.Instance.ResetContext ();
      Assert.AreEqual (SNKit.Instance [SNContextLevel.DEFAULT].Context.Count, 1);
      Assert.AreEqual ((SNKit.Instance [SNContextLevel.DEFAULT] as SN).ResetTimes, 1);
      Assert.AreEqual ((SNKit.Instance [SNContextLevel.DEFAULT].Context [0] as SN).ResetTimes, 1);
    }

  }
}
