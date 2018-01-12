﻿//if you hava any problem please email to keyle_xiao@hotmail.com
using System;
using SNFramework;
using UnityEngine.TestTools;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace SNTest
{
  public class SNEventTest
  {
    [Test]
    public void RegisterSNEventTest0 ()
    {
      var testInsintace = new SNEvent ();
      ISNEvent e = null;

      Action voidAc = () => {
      };
      e = testInsintace.Register (SNEventName.Msg0, voidAc);
      Assert.IsNotNull (e);
    }

    [Test]
    public void RegisterSNEventTest1 ()
    {
      var testInsintace = new SNEvent ();
      ISNEvent e = null;

      Action<int> voidAc1 = (int i) => {
      };
      e = testInsintace.Register (SNEventName.Msg0, voidAc1);
      Assert.IsNotNull (e);
    }

    [Test]
    public void RegisterSNEventTest2 ()
    {
      var testInsintace = new SNEvent ();
      ISNEvent e = null;

      Action<string, int> voidAc2 = (string s, int i) => {
      };
      e = testInsintace.Register (SNEventName.Msg0, voidAc2);
      Assert.IsNotNull (e);
    }

    [Test]
    public void RegisterSNEventTest3 ()
    {
      var testInsintace = new SNEvent ();
      ISNEvent e = null;

      Action<string, int, int> voidAc3 = (string s, int i, int p3) => {
      };
      e = testInsintace.Register (SNEventName.Msg0, voidAc3);
      Assert.IsNotNull (e);
    }

    [Test]
    public void RegisterSNEventTest4 ()
    {
      var testInsintace = new SNEvent ();
      ISNEvent e = null;

      Action<string, int, int, int> voidAc4 = (string s, int i, int p3, int p4) => {
      };
      e = testInsintace.Register (SNEventName.Msg0, voidAc4);
      Assert.IsNotNull (e);
    }

    [Test]
    public void RegisterSNEventTest5 ()
    {
      var testInsintace = new SNEvent ();
      ISNEvent e = null;

      SNFramework.Action<string, int, int, int, int> voidAc5 = (string s, int i, int p3, int p4, int p5) => {
      };
      e = testInsintace.Register (SNEventName.Msg0, voidAc5);
      Assert.IsNotNull (e);
    }


    //SendSNEvent
    [Test]
    public void SendSNEventTest0 ()
    {
      var testInsintace = new SNEvent ();
      int param = 0;

      Action voidAc = () => {
        param = 1;
      };
      testInsintace.Register (SNEventName.Msg0, voidAc);
      testInsintace.Dispatch (SNEventName.Msg0);
      Assert.AreEqual (param, 1);
    }

    [Test]
    public void SendSNEventTest1 ()
    {
      var testInsintace = new SNEvent ();
      int param = 0;

      Action<int> voidAc1 = (int i) => {
        param = i;
      };

      testInsintace.Register (SNEventName.Msg0, voidAc1);
      testInsintace.Dispatch (SNEventName.Msg0, 1);
      Assert.AreEqual (param, 1);
    }

    [Test]
    public void SendSNEventTest2 ()
    {
      var testInsintace = new SNEvent ();
      string str = "";
      int param = 0;

      Action<string, int> voidAc2 = (string s, int i) => {
        str = "1";
        param = 1;
      };

      testInsintace.Register (SNEventName.Msg0, voidAc2);
      testInsintace.Dispatch (SNEventName.Msg0, "1", 1);
      Assert.AreEqual (param, 1);
      Assert.AreEqual (str, "1");
    }

    [Test]
    public void SendSNEventTest3 ()
    {
      var testInsintace = new SNEvent ();
      string str = "";
      int param = 0;
      int param2 = 0;
      Action<string, int, int> voidAc3 = (string s, int i, int p3) => {
        str = "1";
        param = 1;
        param2 = 1;
      };
      testInsintace.Register (SNEventName.Msg0, voidAc3);
      testInsintace.Dispatch (SNEventName.Msg0, "1", 1, 1);
      Assert.AreEqual (param, 1);
      Assert.AreEqual (str, "1");
      Assert.AreEqual (param2, 1);
    }

    [Test]
    public void SendSNEventTest4 ()
    {
      var testInsintace = new SNEvent ();
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
      testInsintace.Register (SNEventName.Msg0, voidAc4);
      testInsintace.Dispatch (SNEventName.Msg0, "1", 1, 1, 1);
      Assert.AreEqual (param, 1);
      Assert.AreEqual (str, "1");
      Assert.AreEqual (param2, 1);
      Assert.AreEqual (param3, 1);
    }

    [Test]
    public void SendSNEventTest5 ()
    {
      var testInsintace = new SNEvent ();
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
      testInsintace.Register (SNEventName.Msg0, voidAc5);
      testInsintace.Dispatch (SNEventName.Msg0, "1", 1, 1, 1, 1);
      Assert.AreEqual (param, 1);
      Assert.AreEqual (str, "1");
      Assert.AreEqual (param2, 1);
      Assert.AreEqual (param3, 1);
      Assert.AreEqual (param4, 1);
    }

    [Test]
    public void SendSNEventHasReturn ()
    {
      var testInsintace = new SNEvent ();

      Func<int> voidFc = () => {
        return 1;
      };
      testInsintace.Register (SNEventName.Msg0, voidFc);
      var item = testInsintace.DispatchHasReturn<int> (SNEventName.Msg0);
      Assert.AreEqual (item, 1);
    }

    [Test]
    public void SendSNEventHasReturn1 ()
    {
      var testInsintace = new SNEvent ();

      Func<int, int> voidFc1 = (int i) => {
        return i;
      };
      testInsintace.Register (SNEventName.Msg0, voidFc1);
      var item = testInsintace.DispatchHasReturn<int, int> (SNEventName.Msg0, 1);
      Assert.AreEqual (item, 1);
    }

    [Test]
    public void SendSNEventHasReturn2 ()
    {
      var testInsintace = new SNEvent ();

      Func<int, int, int> voidFc = (int i, int i2) => {
        return i + i2;
      };
      testInsintace.Register (SNEventName.Msg0, voidFc);
      var item = testInsintace.DispatchHasReturn<int, int, int> (SNEventName.Msg0, 1, 1);
      Assert.AreEqual (item, 2);
    }

    [Test]
    public void SendSNEventHasReturn3 ()
    {
      var testInsintace = new SNEvent ();

      Func<int, int, int, int> voidFc = (int i, int i2, int i3) => {
        return i + i2 + i3;
      };
      testInsintace.Register (SNEventName.Msg0, voidFc);
      var item = testInsintace.DispatchHasReturn<int, int, int, int> (SNEventName.Msg0, 1, 1, 1);
      Assert.AreEqual (item, 3);
    }

    [Test]
    public void SendSNEventHasReturn4 ()
    {
      var testInsintace = new SNEvent ();
      int value = 0;
      Func<int, int, int, int, int> voidFc = (int i, int i2, int i3, int i4) => {
        return (value += (i + i2 + i3 + i4));
      };
      testInsintace.Register (SNEventName.Msg0, voidFc);
      testInsintace.Register (SNEventName.Msg0, voidFc);

      testInsintace.DispatchHasReturn<int, int, int, int, int> (SNEventName.Msg0, 1, 1, 1, 1);
      Assert.AreEqual (value, 8);
    }

    [Test]
    public void SendSNEventHasReturn5 ()
    {
      var testInsintace = new SNEvent ();
      int value = 0;

      SNFramework.Func<int, int, int, int, int, int> voidFc = (int i, int i2, int i3, int i4, int i5) => {
        return value += i + i2 + i3 + i4 + i5;
      };


      Func<int, int, int, int, int> voidFc4 = (int i, int i2, int i3, int i4) => {
        return value += (i + i2 + i3 + i4);
      };

      testInsintace.Register (SNEventName.Msg0, voidFc4);
      testInsintace.Register (SNEventName.Msg0, voidFc);
      var item = testInsintace.DispatchHasReturn<int, int, int, int, int, int> (SNEventName.Msg0, 1, 1, 1, 1, 1);
      testInsintace.DispatchHasReturn<int, int, int, int,int> (SNEventName.Msg0, 1, 1, 1, 1);
      Assert.AreEqual (value, 9);
    }

    [Test]
    public void UnregisterSNEventTest ()
    {
      var testInsintace = new SNEvent ();

      Action voidAc = () => {
      };
      ISNEvent e = testInsintace.Register (SNEventName.Msg0, voidAc);
      e = testInsintace.Unregister (SNEventName.Msg0, voidAc);
      Assert.IsNotNull (e);
    }
  }
}
