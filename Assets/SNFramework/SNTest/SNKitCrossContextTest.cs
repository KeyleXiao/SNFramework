//if you hava any problem please email to keyle_xiao@hotmail.com
using System;
using SNFramework;
using NUnit.Framework;

namespace SNTest
{
  public class SNKitCrossContextTest
  {
    public string dispatchLevel = SNContextLevel.BOARDCAST;

    [SNMethod (SNMsg.ExampleMsg)]
    public void Test ()
    {
      TestInt = 1;
    }

    [SNMethod (SNMsg.ExampleMsg, autoRelease: true)]
    public void Test1 (int a)
    {
      TestInt = a;
    }

    [SNMethod (SNMsg.ExampleMsg, autoRelease: true)]
    public void Test2 (int a, int b)
    {
      TestInt += (a + b);
    }

    [SNMethod (SNMsg.ExampleMsg, autoRelease: true)]
    public void Test3 (int a, int b, int c)
    {
      TestInt += (a + b + c);
    }

    [SNMethod (SNMsg.ExampleMsg, autoRelease: true)]
    public void Test4 (int a, int b, int c, int d)
    {
      TestInt += a + b + c + d;
    }

    [SNMethod (SNMsg.ExampleMsg, autoRelease: true)]
    public void Test5 (int a, int b, int c, int d, int e)
    {
      TestInt += a + b + c + d + e;
    }


    [SNMethod (SNMsg.ExampleMsg, autoRelease: true)]
    public int TestHasReturn ()
    {
      return TestInt = 1;
    }

    [SNMethod (SNMsg.ExampleMsg, autoRelease: true)]
    public int TestHasReturn1 (int a)
    {
      return TestInt = a;
    }

    [SNMethod (SNMsg.ExampleMsg, autoRelease: true)]
    public int TestHasReturn2 (int a, int b)
    {
      return TestInt += (a + b);
    }

    [SNMethod (SNMsg.ExampleMsg, autoRelease: true)]
    public int TestHasReturn3 (int a, int b, int c)
    {
      return TestInt += (a + b + c);
    }

    [SNMethod (SNMsg.ExampleMsg, autoRelease: true)]
    public int TestHasReturn4 (int a, int b, int c, int d)
    {
      return TestInt += (a + b + c + d);
    }

    
    [SNMethod (SNMsg.ExampleMsg, autoRelease: true)]
    public int TestHasReturn5 (int a, int b, int c, int d, int e)
    {
      return TestInt += (a + b + c + d + e);
    }


    public int TestInt = 0;


    public void ResetEveryTest ()
    {
      SNKit.Instance.Reset ();
      TestInt = 0;
    }
    

    [Test]
    public void CrossContextAddTest ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register (Test);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);
      SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease = false;
      Assert.IsTrue (!SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);


      for (int i = 0; i < 5; i++) {
        SNKit.Instance.Dispatch (SNMsg.ExampleMsg, dispatchLevel);
        Assert.AreEqual (1, TestInt);
        TestInt = 2;
        SNKit.Instance.Dispatch (SNMsg.ExampleMsg, dispatchLevel);
        Assert.AreEqual (1, TestInt);
      }
    }

    [Test]
    public void CrossContextAddTest1Once ()
    {
      ResetEveryTest ();


      SNKit.Instance.Register <int>(Test1);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, dispatchLevel);//此函数仅执行一次 所以第二次不会变更TestInt的值

      TestInt = 2;
      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, dispatchLevel);//消息只会触发一次 所以现在testint不能再次被改变

      Assert.AreEqual (2, TestInt);
    }

    [Test]
    public void CrossContextAddTest2 ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int> (Test2);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);
      SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease = false;
      Assert.IsTrue (!SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, dispatchLevel);
      Assert.AreEqual (2, TestInt);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, dispatchLevel);
      Assert.AreEqual (4, TestInt);
    }

    [Test]
    public void CrossContextAddTest2Once ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int> (Test2);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);
      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, dispatchLevel);
      Assert.AreEqual (2, TestInt);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, dispatchLevel);//这里并不会被执行到

      Assert.AreEqual (2, TestInt);
    }

    [Test]
    public void CrossContextAddTest3 ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int> (Test3);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease = false;
      Assert.IsTrue (!SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, dispatchLevel);
      Assert.AreEqual (3, TestInt);


      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, dispatchLevel);
      Assert.AreEqual (6, TestInt);
    }


    [Test]
    public void CrossContextAddTest3Once ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int> (Test3);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, dispatchLevel);
      Assert.AreEqual (3, TestInt);

      TestInt = -1;

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, dispatchLevel);//方法不会再次被执行
      Assert.AreEqual (-1, TestInt);
    }


    [Test]
    public void CrossContextAddTest4 ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int, int> (Test4);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease = false;
      Assert.IsTrue (!SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, 1, dispatchLevel);
      Assert.AreEqual (4, TestInt);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, 1, dispatchLevel);
      Assert.AreEqual (8, TestInt);
    }



    [Test]
    public void CrossContextAddTest4Once ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int, int> (Test4);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, 1, dispatchLevel);
      Assert.AreEqual (4, TestInt);


      TestInt = 2;

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, 1, dispatchLevel);

      Assert.AreEqual (2, TestInt);
    }


    [Test]
    public void CrossContextAddTest5 ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int, int, int> (Test5);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease = false;
      Assert.IsTrue (!SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, 1, 1, dispatchLevel);
      Assert.AreEqual (5, TestInt);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, 1, 1, dispatchLevel);
      Assert.AreEqual (10, TestInt);
    }

    [Test]
    public void CrossContextAddTest5Once ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int, int, int> (Test5);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, 1, 1, dispatchLevel);
      Assert.AreEqual (5, TestInt);

      TestInt = 2;
      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, 1, 1, dispatchLevel);
      Assert.AreEqual (2, TestInt);
    }

    [Test]
    public void CrossContextAddTestOnce ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int, int, int> (Test5);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, 1, 1, dispatchLevel);
      Assert.AreEqual (5, TestInt);

      TestInt = 1;
      SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, 1, 1, 1, dispatchLevel);
      Assert.AreEqual (1, TestInt);
    }

    // --------------------------------------

    [Test]
    public void CrossContextAddTestHasReturn ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int> (TestHasReturn);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease = false;
      Assert.IsTrue (!SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      Assert.IsTrue (SNKit.Instance.DispatchHasReturn(SNMsg.ExampleMsg, ref TestInt, dispatchLevel));
      Assert.AreEqual (1, TestInt);

      TestInt = 2;

      Assert.IsTrue (SNKit.Instance.DispatchHasReturn(SNMsg.ExampleMsg, ref TestInt, dispatchLevel));
      Assert.AreEqual (1, TestInt);
    }

    [Test]
    public void CrossContextAddTestHasReturnOnce ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int> (TestHasReturn);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      Assert.IsTrue (SNKit.Instance.DispatchHasReturn(SNMsg.ExampleMsg, ref TestInt, dispatchLevel));
      Assert.AreEqual (1, TestInt);

      TestInt = 100;
      Assert.IsFalse (SNKit.Instance.DispatchHasReturn(SNMsg.ExampleMsg, ref TestInt, dispatchLevel));

      Assert.AreEqual (100, TestInt);
    }

    [Test]
    public void CrossContextAddHasReturnTest1 ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int> (TestHasReturn1);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease = false;
      Assert.IsTrue (!SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      Assert.IsTrue (SNKit.Instance.DispatchHasReturn(SNMsg.ExampleMsg, 1, ref TestInt, dispatchLevel));
      Assert.AreEqual (1, TestInt);
      TestInt = 0;

      Assert.IsTrue (SNKit.Instance.DispatchHasReturn (SNMsg.ExampleMsg, 1, ref TestInt, dispatchLevel));
      Assert.AreEqual (1, TestInt);
    }


    [Test]
    public void CrossContextAddHasReturnTest1Once ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int> (TestHasReturn1);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      Assert.IsTrue (SNKit.Instance.DispatchHasReturn(SNMsg.ExampleMsg, 1, ref TestInt, dispatchLevel));
      Assert.AreEqual (1, TestInt);

      TestInt = 2;
      Assert.IsFalse (SNKit.Instance.DispatchHasReturn (SNMsg.ExampleMsg, 1, ref TestInt, dispatchLevel));
      Assert.AreEqual (2, TestInt);
    }



    [Test]
    public void CrossContextAddHasReturnTest2 ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int> (TestHasReturn2);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease = false;
      Assert.IsTrue (!SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      Assert.IsTrue (SNKit.Instance.DispatchHasReturn(SNMsg.ExampleMsg, 1, 1, ref TestInt, dispatchLevel));
      Assert.AreEqual (2, TestInt);
      TestInt = 10;
      Assert.IsTrue (SNKit.Instance.DispatchHasReturn(SNMsg.ExampleMsg, 1, 1, ref TestInt, dispatchLevel));
      Assert.AreEqual (12, TestInt);
    }

    [Test]
    public void CrossContextAddHasReturnTest2Once ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int> (TestHasReturn2);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      Assert.IsTrue (SNKit.Instance.DispatchHasReturn(SNMsg.ExampleMsg, 1, 1, ref TestInt, dispatchLevel));
      Assert.AreEqual (2, TestInt);

      TestInt = 3;
      Assert.False (SNKit.Instance.DispatchHasReturn(SNMsg.ExampleMsg, 1, 1, ref TestInt, dispatchLevel));
      Assert.AreEqual (3, TestInt);
    }

    [Test]
    public void CrossContextAddHasReturnTest3 ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int, int> (TestHasReturn3);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease = false;
      Assert.IsTrue (!SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);
      Assert.IsTrue (SNKit.Instance.DispatchHasReturn (SNMsg.ExampleMsg, 1, 1, 1, ref TestInt, dispatchLevel));
      Assert.AreEqual (3, TestInt);

      TestInt = 100;

      Assert.IsTrue (SNKit.Instance.DispatchHasReturn (SNMsg.ExampleMsg, 1, 1, 1, ref TestInt, dispatchLevel));
      Assert.AreEqual (103, TestInt);
    }


    [Test]
    public void CrossContextAddHasReturnTest3Once ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int, int> (TestHasReturn3);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);
      Assert.IsTrue (SNKit.Instance.DispatchHasReturn (SNMsg.ExampleMsg, 1, 1, 1, ref TestInt, dispatchLevel));
      Assert.AreEqual (3, TestInt);

      TestInt = 100;

      Assert.IsFalse (SNKit.Instance.DispatchHasReturn (SNMsg.ExampleMsg, 1, 1, 1, ref TestInt, dispatchLevel));
      Assert.AreEqual (100, TestInt);
    }


    [Test]
    public void CrossContextAddHasReturnTest4 ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int, int, int> (TestHasReturn4);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease = false;
      Assert.IsTrue (!SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);


      Assert.IsTrue (SNKit.Instance.DispatchHasReturn (SNMsg.ExampleMsg, 1, 1, 1, 1, ref TestInt, dispatchLevel));
      Assert.AreEqual (4, TestInt);

      TestInt = 100; 

      Assert.IsTrue (SNKit.Instance.DispatchHasReturn (SNMsg.ExampleMsg, 1, 1, 1, 1, ref TestInt, dispatchLevel));
      Assert.AreEqual (104, TestInt);
    }

    [Test]
    public void CrossContextAddHasReturnTest4Once ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int, int, int> (TestHasReturn4);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);


      Assert.IsTrue (SNKit.Instance.DispatchHasReturn (SNMsg.ExampleMsg, 1, 1, 1, 1, ref TestInt, dispatchLevel));
      Assert.AreEqual (4, TestInt);

      TestInt = 100;

      Assert.IsFalse (SNKit.Instance.DispatchHasReturn (SNMsg.ExampleMsg, 1, 1, 1, 1, ref TestInt, dispatchLevel));
      Assert.AreEqual (100, TestInt);
    }

    [Test]
    public void CrossContextAddHasReturnTest5 ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int, int, int, int> (TestHasReturn5);//如果是匿名函数则不需要指定类型
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease = false;
      Assert.IsTrue (!SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      Assert.IsTrue (SNKit.Instance.DispatchHasReturn (SNMsg.ExampleMsg, 1, 1, 1, 1, 1, ref TestInt, dispatchLevel));
      Assert.AreEqual (5, TestInt);

      Assert.IsTrue (SNKit.Instance.DispatchHasReturn(SNMsg.ExampleMsg, 1, 1, 1, 1, 1, ref TestInt, dispatchLevel));
      Assert.AreEqual (10, TestInt);

      Assert.IsTrue (SNKit.Instance.DispatchHasReturn (SNMsg.ExampleMsg, 1, 1, 1, 1, 1, ref TestInt, dispatchLevel));
      Assert.AreEqual (15, TestInt);
    }

    [Test]
    public void CrossContextAddHasReturnTest5Once ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register<int, int, int, int, int, int> (TestHasReturn5);
      Assert.AreEqual (SNKit.Instance.SNContexts.Count, 1);

      Assert.IsTrue (SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg).AutoRelease);

      Assert.IsTrue (SNKit.Instance.DispatchHasReturn(SNMsg.ExampleMsg, 1, 1, 1, 1, 1, ref TestInt, dispatchLevel));
      Assert.AreEqual (5, TestInt);

      TestInt = 100;

      Assert.IsFalse (SNKit.Instance.DispatchHasReturn(SNMsg.ExampleMsg, 1, 1, 1, 1, 1, ref TestInt, dispatchLevel));
      Assert.AreEqual (100, TestInt);

    }


    [Test]
    public void ResetContextTest ()
    {
      ResetEveryTest ();
      SNKit.Instance.Register (Test);

      Assert.AreEqual ((SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg) as SN).ResetTimes, 0);

      SNKit.Instance.ResetContext ();

      Assert.AreEqual ((SNKit.Instance [SNContextLevel.DEFAULT] as SN).ResetTimes, 1);
      Assert.AreEqual ((SNKit.Instance [SNContextLevel.DEFAULT].GetSNEvent (SNMsg.ExampleMsg) as SN).ResetTimes, 1);
    }

  }
}
