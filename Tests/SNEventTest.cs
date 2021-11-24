//if you hava any problem please email to keyle_xiao@hotmail.com
using System;
using System.Threading.Tasks;
using SNFramework;
using NUnit.Framework;

namespace SNTest
{
  public class SNEventTest
  {

    [Test]
    public void RegisterSNEventTest0 ()
    {
      ISNEvent testInsintace = new SNEvent ();
      ISNEvent e = null;

      Action voidAc = () => {
      };
      e = testInsintace.Register (SNMsg.ExampleMsg, voidAc);
      Assert.IsNotNull (e);
    }

    [Test]
    public void RegisterSNEventTest1 ()
    {
      ISNEvent testInsintace = new SNEvent ();
      ISNEvent e = null;

      Action<int> voidAc1 = (int i) => {
      };
      e = testInsintace.Register (SNMsg.ExampleMsg, voidAc1);
      Assert.IsNotNull (e);
    }

    [Test]
    public void RegisterSNEventTest2 ()
    {
      ISNEvent testInsintace = new SNEvent ();
      ISNEvent e = null;

     Action<string,int> voidAc2 = (string s, int i) => {
      };
      e = testInsintace.Register (SNMsg.ExampleMsg, voidAc2);
      Assert.IsNotNull (e);
    }

    [Test]
    public void RegisterSNEventTest3 ()
    {
      ISNEvent testInsintace = new SNEvent ();
      ISNEvent e = null;

      Action<string, int, int> voidAc3 = (string s, int i, int p3) => {
      };
      e = testInsintace.Register (SNMsg.ExampleMsg, voidAc3);
      Assert.IsNotNull (e);
    }

    [Test]
    public void RegisterSNEventTest4 ()
    {
      ISNEvent testInsintace = new SNEvent ();
      ISNEvent e = null;

      Action<string, int, int, int> voidAc4 = (string s, int i, int p3, int p4) => {
      };
      e = testInsintace.Register (SNMsg.ExampleMsg, voidAc4);
      Assert.IsNotNull (e);
    }

    [Test]
    public void RegisterSNEventTest5 ()
    {
      ISNEvent testInsintace = new SNEvent ();
      ISNEvent e = null;

      SNFramework.Action<string, int, int, int, int> voidAc5 = (string s, int i, int p3, int p4, int p5) => {
      };
      e = testInsintace.Register (SNMsg.ExampleMsg, voidAc5);
      Assert.IsNotNull (e);
    }


    //SendSNEvent
    [Test]
    public void SendSNEventTest0 ()
    {
      ISNEvent testInsintace = new SNEvent ();
      int param = 0;

      Action voidAc = () => {
        param = 1;
      };
      testInsintace.Register (SNMsg.ExampleMsg, voidAc);
      testInsintace.Dispatch (SNMsg.ExampleMsg);
      Assert.AreEqual (param, 1);
    }
    
    //SendSNEvent
    [Test]
    public async void SendSNEventTest0Async ()
    {
      ISNEvent testInsintace = new SNEvent ();
      int param = 0;

      Action voidAc = () => {
        param = 1;
      };
      testInsintace.Register (SNMsg.ExampleMsg, voidAc);
      await testInsintace.DispatchAsync (SNMsg.ExampleMsg);
      Assert.AreEqual (param, 1);
    }


    [Test]
    public void SendSNEventTest1 ()
    {
      ISNEvent testInsintace = new SNEvent ();
      int param = 0;

      Action<int> voidAc1 = (int i) => {
        param = i;
      };

      testInsintace.Register (SNMsg.ExampleMsg, voidAc1);
      testInsintace.Dispatch (SNMsg.ExampleMsg, 1);
      Assert.AreEqual (param, 1);
    }
    
    [Test]
    public async void SendSNEventTest1Async ()
    {
      ISNEvent testInsintace = new SNEvent ();
      int param = 0;

      Action<int> voidAc1 = (int i) => {
        param = i;
      };

      testInsintace.Register (SNMsg.ExampleMsg, voidAc1);
      await testInsintace.DispatchAsync (SNMsg.ExampleMsg, 1);
      Assert.AreEqual (param, 1);
    }

    [Test]
    public void SendSNEventTest2 ()
    {
      ISNEvent testInsintace = new SNEvent ();
      string str = "";
      int param = 0;

      Action<string, int> voidAc2 = (string s, int i) => {
        str = "1";
        param = 1;
      };

      testInsintace.Register (SNMsg.ExampleMsg, voidAc2);
      testInsintace.Dispatch (SNMsg.ExampleMsg, "1", 1);
      Assert.AreEqual (param, 1);
      Assert.AreEqual (str, "1");
    }
    
    [Test]
    public async void SendSNEventTest2Async ()
    {
      ISNEvent testInsintace = new SNEvent ();
      string str = "";
      int param = 0;

      Action<string, int> voidAc2 = (string s, int i) => {
        str = "1";
        param = 1;
      };

      testInsintace.Register (SNMsg.ExampleMsg, voidAc2);
      await testInsintace.DispatchAsync (SNMsg.ExampleMsg, "1", 1);
      Assert.AreEqual (param, 1);
      Assert.AreEqual (str, "1");
    }

    [Test]
    public void SendSNEventTest3 ()
    {
      ISNEvent testInsintace = new SNEvent ();
      string str = "";
      int param = 0;
      int param2 = 0;
      Action<string, int, int> voidAc3 = (string s, int i, int p3) => {
        str = "1";
        param = 1;
        param2 = 1;
      };
      testInsintace.Register (SNMsg.ExampleMsg, voidAc3);
      testInsintace.Dispatch (SNMsg.ExampleMsg, "1", 1, 1);
      Assert.AreEqual (param, 1);
      Assert.AreEqual (str, "1");
      Assert.AreEqual (param2, 1);
    }
    
    [Test]
    public async void SendSNEventTest3Async ()
    {
      ISNEvent testInsintace = new SNEvent ();
      string str = "";
      int param = 0;
      int param2 = 0;
      Action<string, int, int> voidAc3 = (string s, int i, int p3) => {
        str = "1";
        param = 1;
        param2 = 1;
      };
      testInsintace.Register (SNMsg.ExampleMsg, voidAc3);
      await testInsintace.DispatchAsync (SNMsg.ExampleMsg, "1", 1, 1);
      Assert.AreEqual (param, 1);
      Assert.AreEqual (str, "1");
      Assert.AreEqual (param2, 1);
    }

    [Test]
    public void SendSNEventTest4 ()
    {
      ISNEvent testInsintace = new SNEvent ();
      string str = "";
      int param = 0;
      int param2 = 0;
      int param3 = 0;

      Action<string, int, int, int> voidAc4 = (string s, int i, int p3, int p4) => {
        str = "1";
        param = 1;
        param2 = 1;
        param3 = 1;
      };
      testInsintace.Register (SNMsg.ExampleMsg, voidAc4);
      testInsintace.Dispatch (SNMsg.ExampleMsg, "1", 1, 1, 1);
      Assert.AreEqual (param, 1);
      Assert.AreEqual (str, "1");
      Assert.AreEqual (param2, 1);
      Assert.AreEqual (param3, 1);
    }
    
    [Test]
    public async void SendSNEventTest4Async ()
    {
      ISNEvent testInsintace = new SNEvent ();
      string str = "";
      int param = 0;
      int param2 = 0;
      int param3 = 0;

      Action<string, int, int, int> voidAc4 = (string s, int i, int p3, int p4) => {
        str = "1";
        param = 1;
        param2 = 1;
        param3 = 1;
      };
      testInsintace.Register (SNMsg.ExampleMsg, voidAc4);
      await testInsintace.DispatchAsync (SNMsg.ExampleMsg, "1", 1, 1, 1);
      Assert.AreEqual (param, 1);
      Assert.AreEqual (str, "1");
      Assert.AreEqual (param2, 1);
      Assert.AreEqual (param3, 1);
    }

    [Test]
    public void SendSNEventTest5 ()
    {
      ISNEvent testInsintace = new SNEvent ();
      string str = "";
      int param = 0;
      int param2 = 0;
      int param3 = 0;
      int param4 = 0;

      SNFramework.Action<string, int, int, int, int> voidAc5 = (string s, int i, int p3, int p4, int p5) => {
        str = "1";
        param = 1;
        param2 = 1;
        param3 = 1;
        param4 = 1;
      };
      testInsintace.Register (SNMsg.ExampleMsg, voidAc5);
      testInsintace.Dispatch (SNMsg.ExampleMsg, "1", 1, 1, 1, 1);
      Assert.AreEqual (param, 1);
      Assert.AreEqual (str, "1");
      Assert.AreEqual (param2, 1);
      Assert.AreEqual (param3, 1);
      Assert.AreEqual (param4, 1);
    }
    [Test]
    public async void SendSNEventTest5Async ()
    {
      ISNEvent testInsintace = new SNEvent ();
      string str = "";
      int param = 0;
      int param2 = 0;
      int param3 = 0;
      int param4 = 0;

      SNFramework.Action<string, int, int, int, int> voidAc5 = (string s, int i, int p3, int p4, int p5) => {
        str = "1";
        param = 1;
        param2 = 1;
        param3 = 1;
        param4 = 1;
      };
      testInsintace.Register (SNMsg.ExampleMsg, voidAc5);
      await testInsintace.DispatchAsync (SNMsg.ExampleMsg, "1", 1, 1, 1, 1);
      Assert.AreEqual (param, 1);
      Assert.AreEqual (str, "1");
      Assert.AreEqual (param2, 1);
      Assert.AreEqual (param3, 1);
      Assert.AreEqual (param4, 1);
    }

    public void ResetEveryTest ()
    {
      item = 0;
    }

    int item = 0;

    [Test]
    public void SendSNEventHasReturn ()
    {
      ISNEvent testInsintace = new SNEvent ();

      Func<int> voidFc = () => {
        return 1;
      };
      testInsintace.Register (SNMsg.ExampleMsg, voidFc);

      testInsintace.DispatchHasReturn<int> (SNMsg.ExampleMsg, ref item);
      Assert.AreEqual (item, 1);
    }

    [Test]
    public void SendSNEventHasReturn1 ()
    {
      ResetEveryTest ();
      ISNEvent testInsintace = new SNEvent ();

      Func<int, int> voidFc1 = (int i) => {
        return i;
      };
      testInsintace.Register (SNMsg.ExampleMsg, voidFc1);
      testInsintace.DispatchHasReturn<int, int> (SNMsg.ExampleMsg, 100, ref item);
      Assert.AreEqual (item, 100);
    }
    
    

    
    [Test]
    public async void SendSNEventHasReturn1Async()
    {
      ResetEveryTest ();
      ISNEvent testInsintace = new SNEvent ();
      
      Func<int, int> voidFc1Async = (int i) => {
        return i;
      };

      testInsintace.Register (SNMsg.ExampleMsg, voidFc1Async);
      item = await testInsintace.DispatchHasReturnAsync<int, int> (SNMsg.ExampleMsg, 199999);
      Assert.AreEqual (item, 199999);
    }

    [Test]
    public void SendSNEventHasReturn2 ()
    {
      ResetEveryTest ();
      ISNEvent testInsintace = new SNEvent ();

      Func<int, int, int> voidFc = (int i, int i2) => {
        return i + i2;
      };
      testInsintace.Register (SNMsg.ExampleMsg, voidFc);
      testInsintace.DispatchHasReturn<int, int, int> (SNMsg.ExampleMsg, 1, 1, ref item);
      Assert.AreEqual (item, 2);
    }
    
    [Test]
    public async void SendSNEventHasReturn2Async ()
    {
      ResetEveryTest ();
      ISNEvent testInsintace = new SNEvent ();

      Func<int, int, int> voidFc = (int i, int i2) => {
        return i + i2;
      };
      testInsintace.Register (SNMsg.ExampleMsg, voidFc);
      item = await testInsintace.DispatchHasReturnAsync<int, int, int> (SNMsg.ExampleMsg, 1, 1);
      Assert.AreEqual (item, 2);
    }


    [Test]
    public void SendSNEventHasReturn3 ()
    {
      ResetEveryTest ();
      ISNEvent testInsintace = new SNEvent ();

      Func<int, int, int, int> voidFc = (int i, int i2, int i3) => {
        return i + i2 + i3;
      };
      testInsintace.Register (SNMsg.ExampleMsg, voidFc);
      testInsintace.DispatchHasReturn<int, int, int, int> (SNMsg.ExampleMsg, 1, 1, 1, ref item);
      Assert.AreEqual (item, 3);
    }
    
    [Test]
    public async void SendSNEventHasReturn3Async ()
    {
      ResetEveryTest ();
      ISNEvent testInsintace = new SNEvent ();

      Func<int, int, int, int> voidFc = (int i, int i2, int i3) => {
        return i + i2 + i3;
      };
      testInsintace.Register (SNMsg.ExampleMsg, voidFc); 
      item = await testInsintace.DispatchHasReturnAsync<int, int, int, int> (SNMsg.ExampleMsg, 1, 1, 1);
      Assert.AreEqual (item, 3);
    }

    [Test]
    public void SendSNEventHasReturn4 ()
    {
      ResetEveryTest ();
      ISNEvent testInsintace = new SNEvent ();
      int value = 0;
      Func<int, int, int, int, int> voidFc = (int i, int i2, int i3, int i4) => {
        return (value += (i + i2 + i3 + i4));
      };
      testInsintace.Register (SNMsg.ExampleMsg, voidFc);
      testInsintace.Register (SNMsg.ExampleMsg, voidFc);

      testInsintace.DispatchHasReturn<int, int, int, int, int> (SNMsg.ExampleMsg, 1, 1, 1, 1, ref item);
      Assert.AreEqual (value, 8);
    }
    
    [Test]
    public async void SendSNEventHasReturn4Async ()
    {
      ResetEveryTest ();
      ISNEvent testInsintace = new SNEvent ();
      int value = 0;
      Func<int, int, int, int, int> voidFc = (int i, int i2, int i3, int i4) => {
        return (value += (i + i2 + i3 + i4));
      };
      testInsintace.Register (SNMsg.ExampleMsg, voidFc);
      testInsintace.Register (SNMsg.ExampleMsg, voidFc);

      item = await testInsintace.DispatchHasReturnAsync<int, int, int, int, int> (SNMsg.ExampleMsg, 1, 1, 1, 1);
      Assert.AreEqual (value, 8);
    }
    
    [Test]
    public void SendSNEventHasReturn5 ()
    {
      ResetEveryTest ();
      ISNEvent testInsintace = new SNEvent ();
      int value = 0;

      SNFramework.Func<int, int, int, int, int, int> voidFc = (int i, int i2, int i3, int i4, int i5) => {
        return value += i + i2 + i3 + i4 + i5;
      };


      Func<int, int, int, int, int> voidFc4 = (int i, int i2, int i3, int i4) => {
        return value += (i + i2 + i3 + i4);
      };

      testInsintace.Register (SNMsg.ExampleMsg, voidFc4);
      testInsintace.Register (SNMsg.ExampleMsg, voidFc);
      testInsintace.DispatchHasReturn<int, int, int, int, int, int> (SNMsg.ExampleMsg, 1, 1, 1, 1, 1,ref item);
      testInsintace.DispatchHasReturn<int, int, int, int,int> (SNMsg.ExampleMsg, 1, 1, 1, 1,ref item);
      Assert.AreEqual (value, 9);
    }

    [Test]
    public async void SendSNEventHasReturn5Async ()
    {
      ResetEveryTest ();
      ISNEvent testInsintace = new SNEvent ();
      int value = 0;

      SNFramework.Func<int, int, int, int, int, int> voidFc = (int i, int i2, int i3, int i4, int i5) => {
        return value += i + i2 + i3 + i4 + i5;
      };


      Func<int, int, int, int, int> voidFc4 = (int i, int i2, int i3, int i4) => {
        return value += (i + i2 + i3 + i4);
      };

      testInsintace.Register (SNMsg.ExampleMsg, voidFc4);
      testInsintace.Register (SNMsg.ExampleMsg, voidFc);
      item = await testInsintace.DispatchHasReturnAsync<int, int, int, int, int, int> (SNMsg.ExampleMsg, 1, 1, 1, 1, 1);
      item = await testInsintace.DispatchHasReturnAsync<int, int, int, int,int> (SNMsg.ExampleMsg, 1, 1, 1, 1);
      Assert.AreEqual (value, 9);
    }

    [Test]
    public void UnregisterSNEventTest ()
    {
      ISNEvent testInsintace = new SNEvent ();

      Action voidAc = () => {
      };
      ISNEvent e = testInsintace.Register (SNMsg.ExampleMsg, voidAc);
      e = testInsintace.Unregister (SNMsg.ExampleMsg, voidAc);
      Assert.IsNotNull (e);
    }
    
    
  }
}
