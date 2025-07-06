using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SNFramework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

/// <summary>
/// SNKit框架的单元测试类
/// 测试覆盖：
/// 1. 基本事件注册和分发
/// 2. 参数传递
/// 3. 返回值处理
/// 4. 事件注销
/// 5. 广播机制
/// 6. 异步分发
/// 7. 自动释放
/// 8. 上下文管理
/// </summary>
public class SNKitTests
{
    private SNKit snKit;
    private const string TEST_EVENT = "test_event";
    private const string TEST_CONTEXT = "test_context";
    private bool eventTriggered;
    private int eventValue;
    private string eventResult;

    /// <summary>
    /// 测试事件处理器类
    /// 包含各种类型的事件处理方法
    /// </summary>
    private class TestEventHandler
    {
        [SNMethod(TEST_EVENT)]
        public void HandleSimpleEvent()
        {
            SNKitTests.Instance.eventTriggered = true;
        }

        [SNMethod(TEST_EVENT)]
        public void HandleParameterEvent(int value)
        {
            SNKitTests.Instance.eventValue = value;
        }

        [SNMethod(TEST_EVENT)]
        public string HandleReturnEvent()
        {
            return "success";
        }

        [SNMethod("error_event")]
        public void HandleErrorEvent()
        {
            throw new Exception("Test exception");
        }
    }

    private static SNKitTests Instance { get; set; }
    private TestEventHandler eventHandler;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        Instance = this;
    }

    [SetUp]
    public void Setup()
    {
        snKit = SNKit.Instance;
        snKit.Reset();
        eventTriggered = false;
        eventValue = 0;
        eventResult = string.Empty;
        eventHandler = new TestEventHandler();
    }

    [TearDown]
    public void Teardown()
    {
        snKit.Reset();
    }

    /// <summary>
    /// 测试基本事件注册和分发功能
    /// </summary>
    [UnityTest]
    public IEnumerator SNKit_RegisterAndDispatch_SimpleEvent_Success()
    {
        // Arrange - 注册一个简单的事件处理器
        snKit.Register(eventHandler.HandleSimpleEvent, TEST_CONTEXT);

        // Act - 分发事件
        snKit.Dispatch(TEST_EVENT, TEST_CONTEXT);
        yield return null;

        // Assert - 验证事件是否被触发
        Assert.IsTrue(eventTriggered, "Event should have been triggered");
    }

    /// <summary>
    /// 测试事件注销功能
    /// </summary>
    [UnityTest]
    public IEnumerator SNKit_UnRegister_Event_NotTriggered()
    {
        // Arrange - 注册事件处理器
        snKit.Register(eventHandler.HandleSimpleEvent, TEST_CONTEXT);

        // Act - 注销事件处理器并尝试触发事件
        snKit.UnRegister(eventHandler.HandleSimpleEvent, TEST_CONTEXT);
        snKit.Dispatch(TEST_EVENT, TEST_CONTEXT);
        yield return null;

        // Assert - 验证事件未被触发
        Assert.IsFalse(eventTriggered, "Event should not have been triggered after unregistering");
    }

    /// <summary>
    /// 测试事件广播功能
    /// 验证事件是否能正确广播到所有上下文
    /// </summary>
    [UnityTest]
    public IEnumerator SNKit_Broadcast_TriggersAllContexts()
    {
        // Arrange - 在不同上下文注册事件处理器
        var handler1 = new TestEventHandler();
        var handler2 = new TestEventHandler();

        snKit.Register(handler1.HandleSimpleEvent, "context1");
        snKit.Register(handler2.HandleSimpleEvent, "context2");

        // Act - 广播事件
        snKit.Dispatch(TEST_EVENT, SNContextLevel.BOARDCAST);
        yield return null;

        // Assert - 验证事件在所有上下文中都被触发
        Assert.IsTrue(eventTriggered, "Events should have been triggered in broadcast mode");
    }

    /// <summary>
    /// 测试多个处理器的触发
    /// </summary>
    [UnityTest]
    public IEnumerator SNKit_MultipleHandlers_AllTriggered()
    {
        // Arrange - 注册多个事件处理器
        var handler1 = new TestEventHandler();
        var handler2 = new TestEventHandler();

        snKit.Register(handler1.HandleSimpleEvent, "context1");
        snKit.Register(handler2.HandleSimpleEvent, TEST_CONTEXT);

        // Act - 触发事件
        snKit.Dispatch(TEST_EVENT);
        yield return null;

        // Assert - 验证所有处理器都被触发
        Assert.IsTrue(eventTriggered, "All handlers should have been triggered");
    }

    /// <summary>
    /// 测试异步事件分发
    /// </summary>
    [UnityTest]
    public IEnumerator SNKit_AsyncDispatch_Success()
    {
        // Arrange - 注册事件处理器
        snKit.Register(eventHandler.HandleSimpleEvent, TEST_CONTEXT);

        // Act - 异步分发事件
        var task = snKit.GetContext(TEST_CONTEXT)
                       .GetSNEvent(TEST_EVENT)
                       .DispatchAsync(TEST_EVENT);

        while (!task.IsCompleted)
        {
            yield return null;
        }

        // Assert - 验证异步事件是否完成
        Assert.IsTrue(eventTriggered, "Async dispatch should have completed");
    }

    /// <summary>
    /// 测试自动释放事件功能 - 详细过程
    /// </summary>
    [UnityTest]
    public IEnumerator SNKit_AutoRelease_DetailedProcess()
    {
        // Arrange
        eventTriggered = false;
        var eventName = "TestAutoReleaseEvent";
        var context = snKit.GetContext(TEST_CONTEXT);
        var autoReleaseHandler = new AutoReleaseEventHandler();

        // 注册事件
        snKit.Register(autoReleaseHandler.HandleAutoReleaseEvent, TEST_CONTEXT);
        var evt = context.GetSNEvent(TEST_EVENT);

        // 确认事件已正确注册
        Assert.IsTrue(evt.AutoRelease, "Event should be marked for auto-release");
        Assert.AreEqual(context, evt.ParentContext, "Event should have correct parent context");

        // Act - 触发事件
        snKit.Dispatch(TEST_EVENT, TEST_CONTEXT);
        yield return null;

        // Assert
        Assert.IsTrue(eventTriggered, "Event should have been triggered");

        // 验证第二次触发时事件已被自动释放（不会再次触发）
        eventTriggered = false;
        snKit.Dispatch(TEST_EVENT, TEST_CONTEXT);
        yield return null;
        Assert.IsFalse(eventTriggered, "Event should not trigger after auto-release");
    }

    /// <summary>
    /// 测试自动释放在异步事件中的行为
    /// </summary>
    [UnityTest]
    public IEnumerator SNKit_AutoRelease_AsyncEvent()
    {
        // Arrange
        eventTriggered = false;
        var context = snKit.GetContext(TEST_CONTEXT);
        var autoReleaseHandler = new AsyncAutoReleaseEventHandler();

        // 注册异步事件
        snKit.Register(autoReleaseHandler.HandleAsyncAutoReleaseEvent, TEST_CONTEXT);
        var evt = context.GetSNEvent(TEST_EVENT);
        Assert.IsTrue(evt.AutoRelease, "Event should be marked for auto-release");

        // Act - 异步触发事件
        var task = evt.DispatchAsync(TEST_EVENT);

        // 等待异步操作完成
        yield return new WaitUntil(() => task.IsCompleted);

        // Assert
        Assert.IsTrue(eventTriggered, "Async event should have been triggered");

        // 验证第二次触发不会执行（因为事件已被自动释放）
        eventTriggered = false;

        // 创建一个新的事件实例来测试
        var newEvt = context.GetSNEvent(TEST_EVENT);
        task = newEvt.DispatchAsync(TEST_EVENT);

        yield return new WaitUntil(() => task.IsCompleted);

        Assert.IsFalse(eventTriggered, "Event should not trigger after async auto-release");
    }

    private class AutoReleaseEventHandler
    {
        [SNMethod(TEST_EVENT, autoRelease: true)]
        public void HandleAutoReleaseEvent()
        {
            SNKitTests.Instance.eventTriggered = true;
        }
    }

    private class AsyncAutoReleaseEventHandler
    {
        [SNMethod(TEST_EVENT, autoRelease: true)]
        public async Task HandleAsyncAutoReleaseEvent()
        {
            await Task.Delay(100);  // 模拟异步操作
            SNKitTests.Instance.eventTriggered = true;
        }
    }

    /// <summary>
    /// 测试上下文重置功能
    /// </summary>
    [UnityTest]
    public IEnumerator SNKit_ContextReset_EventsCleared()
    {
        // Arrange - 注册事件处理器
        snKit.Register(eventHandler.HandleSimpleEvent, TEST_CONTEXT);

        // Act - 重置上下文并尝试触发事件
        snKit.ResetContext(TEST_CONTEXT);
        snKit.Dispatch(TEST_EVENT, TEST_CONTEXT);
        yield return null;

        // Assert - 验证事件未被触发
        Assert.IsFalse(eventTriggered, "Event should not trigger after context reset");
    }

    /// <summary>
    /// 测试多参数事件处理
    /// </summary>
    [UnityTest]
    public IEnumerator SNKit_MultipleParameters_CorrectlyHandled()
    {
        // Arrange - 准备多参数事件处理器
        string param1Expected = "test";
        int param2Expected = 42;
        bool parametersMatched = false;

        var multiParamHandler = new MultiParamEventHandler(param1Expected, param2Expected, matched => parametersMatched = matched);
        snKit.Register<string, int>(multiParamHandler.HandleMultiParamEvent, TEST_CONTEXT);

        // Act - 触发带多个参数的事件
        snKit.Dispatch(TEST_EVENT, param1Expected, param2Expected, TEST_CONTEXT);
        yield return null;

        // Assert - 验证参数是否正确传递
        Assert.IsTrue(parametersMatched, "Multi-parameter handler should have been called with correct parameters");
    }

    private class MultiParamEventHandler
    {
        private readonly string expectedParam1;
        private readonly int expectedParam2;
        private readonly Action<bool> onParametersMatched;

        public MultiParamEventHandler(string param1, int param2, Action<bool> callback)
        {
            expectedParam1 = param1;
            expectedParam2 = param2;
            onParametersMatched = callback;
        }

        [SNMethod(TEST_EVENT)]
        public void HandleMultiParamEvent(string param1, int param2)
        {
            onParametersMatched(param1 == expectedParam1 && param2 == expectedParam2);
        }
    }

    /// <summary>
    /// 测试事件优先级 先注册的先触发
    /// </summary>
    [UnityTest]
    public IEnumerator SNKit_EventPriority_OrderedExecution()
    {
        // Arrange - 注册不同优先级的事件处理器
        var executionOrder = new List<int>();

        var handler1 = new PriorityEventHandler(1, executionOrder);
        var handler2 = new PriorityEventHandler(2, executionOrder);
        var handler3 = new PriorityEventHandler(3, executionOrder);

        snKit.Register(handler2.HandleEvent, TEST_CONTEXT);
        snKit.Register(handler1.HandleEvent, TEST_CONTEXT);
        snKit.Register(handler3.HandleEvent, TEST_CONTEXT);

        // Act - 触发事件
        snKit.Dispatch(TEST_EVENT, TEST_CONTEXT);
        yield return null;

        // Assert - 验证执行顺序是否符合优先级
        CollectionAssert.AreEqual(new[] { 2, 1, 3 }, executionOrder, "Events should be executed in priority order");
    }

    private class PriorityEventHandler
    {
        private readonly int priority;
        private readonly List<int> executionOrder;

        public PriorityEventHandler(int priority, List<int> executionOrder)
        {
            this.priority = priority;
            this.executionOrder = executionOrder;
        }

        [SNMethod(TEST_EVENT)]
        public void HandleEvent()
        {
            executionOrder.Add(priority);
        }
    }

    [UnityTest]
    public IEnumerator SNKit_AutoRelease_SpecificDelegate_Test()
    {
        const string TEST_CONTEXT = "TestContext";
        const string TEST_EVENT = "TestEvent";
        
        bool firstHandlerCalled = false;
        bool secondHandlerCalled = false;
        
        // 创建两个不同的处理器
        Action firstHandler = () => { firstHandlerCalled = true; };
        Action secondHandler = () => { secondHandlerCalled = true; };
        
        var context = snKit.GetContext(TEST_CONTEXT);
        var evt = context.GetSNEvent(TEST_EVENT);
        
        // 注册两个处理器
        evt.Register(TEST_EVENT, firstHandler);
        evt.Register(TEST_EVENT, secondHandler);
        
        // 设置自动释放
        evt.AutoRelease = true;
        
        // 调用第一个处理器
        firstHandlerCalled = false;
        secondHandlerCalled = false;
        evt.Dispatch(TEST_EVENT);
        
        // 验证两个处理器都被调用了
        Assert.IsTrue(firstHandlerCalled, "第一个处理器应该被调用");
        Assert.IsTrue(secondHandlerCalled, "第二个处理器应该被调用");
        
        // 由于设置了自动释放，事件应该被完全移除
        // 再次尝试获取事件应该得到新的实例
        var newEvt = context.GetSNEvent(TEST_EVENT);
        
        // 验证事件已经被重新创建（没有任何处理器）
        firstHandlerCalled = false;
        secondHandlerCalled = false;
        newEvt.Dispatch(TEST_EVENT);
        
        Assert.IsFalse(firstHandlerCalled, "第一个处理器不应该被调用（事件已被释放）");
        Assert.IsFalse(secondHandlerCalled, "第二个处理器不应该被调用（事件已被释放）");
        
        yield return null;
    }

    [UnityTest]
    public IEnumerator SNKit_AutoRelease_SpecificDelegate_Partial_Test()
    {
        const string TEST_CONTEXT = "TestContext";
        const string TEST_EVENT = "TestEvent";
        
        bool firstHandlerCalled = false;
        bool secondHandlerCalled = false;
        
        // 创建两个不同的处理器
        Action firstHandler = () => { firstHandlerCalled = true; };
        Action secondHandler = () => { secondHandlerCalled = true; };
        
        var context = snKit.GetContext(TEST_CONTEXT);
        var evt = context.GetSNEvent(TEST_EVENT);
        
        // 注册两个处理器
        evt.Register(TEST_EVENT, firstHandler);
        evt.Register(TEST_EVENT, secondHandler);
        
        // 不设置自动释放，手动使用细化释放
        evt.AutoRelease = false;
        
        // 调用事件
        firstHandlerCalled = false;
        secondHandlerCalled = false;
        evt.Dispatch(TEST_EVENT);
        
        // 验证两个处理器都被调用了
        Assert.IsTrue(firstHandlerCalled, "第一个处理器应该被调用");
        Assert.IsTrue(secondHandlerCalled, "第二个处理器应该被调用");
        
        // 手动移除第一个处理器
        context.Release(evt, TEST_EVENT, firstHandler);
        
        // 再次调用事件
        firstHandlerCalled = false;
        secondHandlerCalled = false;
        evt.Dispatch(TEST_EVENT);
        
        // 验证只有第二个处理器被调用
        Assert.IsFalse(firstHandlerCalled, "第一个处理器不应该被调用（已被移除）");
        Assert.IsTrue(secondHandlerCalled, "第二个处理器应该被调用");
        
        // 手动移除第二个处理器
        context.Release(evt, TEST_EVENT, secondHandler);
        
        // 再次调用事件
        firstHandlerCalled = false;
        secondHandlerCalled = false;
        evt.Dispatch(TEST_EVENT);
        
        // 验证都没有被调用，但事件对象应该被完全移除
        Assert.IsFalse(firstHandlerCalled, "第一个处理器不应该被调用");
        Assert.IsFalse(secondHandlerCalled, "第二个处理器不应该被调用");
        
        yield return null;
    }
}





