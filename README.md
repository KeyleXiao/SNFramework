# SNFramework
SmartNotificationFramework/EventBus for Unity3d

# 起源
2015年 我在淘米写了一套 [消息传递机制](http://www.cnblogs.com/Keyle/p/4843934.html) 当然这一套在我的上家公司也得到验证。
2016年 我在世纪创意基于2015年一版的消息传递进行了封装但是当时赶项目也没有引入。我称之为 [SmartNotificationFramework](https://gitee.com/keyle/SmartNotificationFramework) 我希望它能达到足够的灵活，在我看来它工作的还不错，但是太简陋。
2018年 重构与完成了大部分的单元测试。SNFramework 是我正在使用的消息传递机制。它已经有了初步的理想功能，如消息上下分隔离，消息被阅读成功自动释放(即指执行一次)，广播消息等。

2021年 添加Await/Async异步调用支持，并且增加了相对于的单元测试。

# 用法
因为本项目我一直在维护，所以还是推荐你阅读测试用例位于 <code>SNFramework/SNTest</code> 文件夹下。

# Sample

节选自 SNKitTest.cs 测试用例

不带参数的消息订阅触发：

``` cs
[SNMethod (SNMsg.ExampleMsg, AutoRelease: true)]
public void Test2 (int a, int b)
{
  TestInt += (a + b);
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

  SNKit.Instance.Dispatch (SNMsg.ExampleMsg, 1, 1, dispatchLevel);//这里并不会被执行到 消息被执行一次会自动释放

  Assert.AreEqual (2, TestInt);
}

```


带参数的消息订阅触发：

``` cs
[SNMethod (SNMsg.ExampleMsg, AutoRelease: true)]
public int TestHasReturn5 (int a, int b, int c, int d, int e)
{
  return TestInt += (a + b + c + d + e);
}

[Test]
public void CrossContextAddHasReturnTest5 ()
{
  ResetEveryTest ();
  SNKit.Instance.Register<int, int, int, int, int, int> (TestHasReturn5); //如果是匿名函数则不需要指定类型
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

```


参数配置：
可以通过扩展<code> partial </code>类语法来扩展 <code> SNFramework/SNConfig.cs </code> 配置。

``` cs
/// <summary>
/// 定义不同类型的消息
/// </summary>
public partial class SNMsg
{
  public const string ExampleMsg = "0x0";
}

/// <summary>
/// 根据不同的级别定制不同的通信上下文
/// </summary>
public partial class SNContextLevel
{
  public const string DEFAULT = "0x0";
  public const string BOARDCAST = "0x1";
}
```



