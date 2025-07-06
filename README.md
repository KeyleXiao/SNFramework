# SNKit Framework

一个为Unity3D设计的高性能、轻量级事件通信框架。

## 特性

- **上下文隔离**: 支持多个上下文环境，避免事件污染
- **自动释放**: 支持一次性事件自动注销，避免内存泄漏
- **异步支持**: 完整的async/await异步事件处理
- **类型安全**: 强类型的事件参数和返回值
- **优先级控制**: 支持事件处理器优先级
- **广播机制**: 支持跨上下文的事件广播
- **对象池优化**: 高效的事件对象复用
- **线程安全**: 支持多线程环境
- **错误追踪**: 零开销的事件处理器异常追踪系统

## 安装

1. 将SNKit文件夹复制到你的Unity项目中
2. 确保以下文件结构完整：
   ```
   Assets/
   └── SNKit/
       ├── Implement/
       │   ├── SN.cs
       │   └── SNContext.cs
       ├── Interface/
       │   ├── ISN.cs
       │   └── ISNContext.cs
       └── SNConfig.cs
   ```

## 基本用法

### 1. 事件注册

```csharp
// 使用SNMethod特性
public class MyClass
{
    [SNMethod("my_event")]
    public void HandleEvent(int value)
    {
        Debug.Log($"收到事件: {value}");
    }
}

// 注册事件处理器
var handler = new MyClass();
SNKit.Instance.Register(handler.HandleEvent, "my_context");
```

### 2. 事件触发

```csharp
// 简单触发
SNKit.Instance.Dispatch("my_event", 42, "my_context");

// 带返回值的触发
int result = 0;
SNKit.Instance.DispatchHasReturn("my_event", ref result, "my_context");

// 异步触发
await SNKit.Instance.DispatchAsync("my_event", "my_context");
```

### 3. 自动释放事件

```csharp
[SNMethod("one_time_event", autoRelease: true)]
public void HandleOneTimeEvent()
{
    // 此事件处理器只会执行一次，之后自动注销
}
```

### 4. 上下文管理

```csharp
// 定义上下文级别
public static class MyContexts
{
    public const string UI = "ui_context";
    public const string GAME = "game_context";
    public const string NETWORK = "network_context";
}

// 在特定上下文中注册
SNKit.Instance.Register(handler, MyContexts.UI);

// 广播到所有上下文
SNKit.Instance.Dispatch("global_event", SNContextLevel.BOARDCAST);
```

### 5. 异步事件处理

```csharp
// 异步事件处理器
[SNMethod("async_event")]
public async Task HandleAsyncEvent()
{
    await SomeAsyncOperation();
}

// 异步触发并等待结果
await SNKit.Instance.DispatchAsync("async_event");

// 带返回值的异步触发
int result = await SNKit.Instance.DispatchHasReturnAsync<int>("async_event");
```

## 高级特性

### 1. 优先级控制

```csharp
// 注册带优先级的事件处理器
SNKit.Instance.Register(handler.HandleEvent, "my_context", priority: 1);
```

### 2. 事件过滤

```csharp
// 注册带条件的事件处理器
SNKit.Instance.Register(handler.HandleEvent, "my_context", 
    condition: (args) => args.Count > 0);
```

### 3. 错误处理

```csharp
// 全局错误处理
SNKit.Instance.OnEventHandlerException += (exception) =>
{
    Debug.LogError($"事件处理异常: {exception.Message}");
};
```

## 性能优化建议

1. 使用对象池减少GC压力
2. 合理使用自动释放功能
3. 及时注销不需要的事件处理器
4. 避免过度使用广播机制
5. 合理划分上下文，避免事件泛滥

## 调试支持

- 详细的日志系统
- 运行时事件监控
- 内存泄漏检测
- 性能分析工具

## 注意事项

1. 避免在事件处理器中修改事件队列
2. 注意处理异步事件的生命周期
3. 合理使用上下文隔离
4. 避免循环依赖的事件链
5. 注意事件参数的类型安全

## 单元测试

框架提供完整的单元测试覆盖：

- 基本事件注册和触发
- 异步事件处理
- 上下文隔离
- 自动释放机制
- 错误处理
- 性能测试

## 许可证

MIT License

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

## 错误追踪系统

### 工作原理

SNKit的错误追踪系统采用"延迟检测"策略，只在事件实际触发时进行错误追踪，具有以下特点：

1. **零注册开销**
```csharp
// 注册时不会产生任何额外开销
eventSystem.Register("my_event", MyEventHandler);
```

2. **延迟检测机制**
```csharp
// 错误追踪只在事件触发时执行
public void Trigger(T arg)
{
    foreach (var handler in handlers)
    {
        if (SNConfig.EnableErrorTracking)
        {
            try
            {
                handler.Invoke(arg);
            }
            catch (Exception ex)
            {
                SNErrorTracker.Instance.TrackError(ex, eventName, context, new object[] { arg });
                throw; // 保持原有异常抛出行为
            }
        }
        else
        {
            handler.Invoke(arg);
        }
    }
}
```

3. **配置系统**
```csharp
// 在编辑器中默认启用
SNConfig.EnableErrorTracking = true;     // 启用错误追踪
SNConfig.LogErrorsToConsole = true;      // 输出到控制台
SNConfig.CollectFullCallStack = true;    // 收集完整堆栈
SNConfig.MaxStackTraceDepth = 20;        // 设置堆栈深度
```

4. **错误信息收集**
- 事件名称和上下文
- 异常类型和消息
- 参数值和类型
- 调用堆栈
- 源代码位置（文件名和行号）

### 性能特点

1. **零注册开销**
- 注册事件处理器时没有任何额外开销
- 不会包装或修改原始处理器

2. **按需检测**
- 仅在事件触发时才进行错误检测
- 未启用追踪时完全没有性能损失

3. **智能触发**
- 只在实际发生异常时收集信息
- 正常执行路径零开销

4. **可配置粒度**
- 支持全局开关控制
- 可按环境配置追踪级别
- 灵活的堆栈追踪深度设置

### 使用示例

1. **基本用法**
```csharp
// 注册事件处理器 - 零开销注册
[SNMethod("my_event")]
public void HandleEvent(int value)
{
    if (value < 0)
        throw new ArgumentException("值不能为负数");
}

// 注册异常处理回调
SNKit.Instance.OnEventHandlerException += (ex) => {
    Debug.LogError($"捕获到异常: {ex.Message}");
};
```

2. **自定义错误处理**
```csharp
// 注册全局异常处理器
SNKit.Instance.OnEventHandlerException += (ex) => {
    // 发送错误报告
    Analytics.SendErrorReport(ex);
    
    // 保存错误日志
    Logger.LogError(ex);
    
    // 显示用户提示
    UI.ShowErrorMessage(ex.Message);
};
```

3. **环境配置**
```csharp
#if UNITY_EDITOR
// 编辑器环境：完整追踪
SNConfig.EnableErrorTracking = true;
SNConfig.CollectFullCallStack = true;
SNConfig.LogErrorsToConsole = true;
#else
// 发布环境：精简追踪
SNConfig.EnableErrorTracking = true;
SNConfig.CollectFullCallStack = false;
SNConfig.LogErrorsToConsole = false;
#endif
```

### 错误输出示例

```
SNKit事件处理器异常
事件名称: player_damage
上下文: game_context
异常类型: ArgumentException
异常消息: 伤害值不能为负数
参数信息:
  参数[0]: -10 (Int32)
  参数[1]: Player_01 (String)
调用堆栈:
  在 Game.Combat.DamageHandler.HandleDamage
    文件: DamageHandler.cs:行 42
  在 Game.Combat.CombatSystem.ProcessDamage
    文件: CombatSystem.cs:行 128
```

### 最佳实践

1. **开发阶段配置**
- 启用完整错误追踪
- 开启控制台输出
- 收集完整堆栈信息
- 设置合理的堆栈深度

2. **发布阶段配置**
- 使用精简错误追踪
- 关闭控制台输出
- 限制堆栈收集深度
- 只保留关键信息

3. **异常处理建议**
- 不要吞掉异常
- 保持异常追踪链
- 合理使用try-catch
- 记录关键业务异常

4. **性能优化建议**
- 根据实际需求配置追踪级别
- 在性能敏感场景可临时关闭
- 合理设置堆栈深度
- 避免在高频事件中抛出异常

[原有的其他内容...]



