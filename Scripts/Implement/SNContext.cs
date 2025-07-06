//if you hava any problem please email to keyle_xiao@hotmail.com
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace SNFramework
{
    public class SNContext : SN, ISNContext
    {
        private static readonly Stack<SNEvent> eventPool = new Stack<SNEvent>();
        private static readonly object poolLock = new object();
        private const string CANT_FIND_MESSAGE = " CAN'T FIND";
        private static readonly StringBuilder logBuilder = new StringBuilder(128);

        private static SNEvent GetEventFromPool()
        {
            //保证线程安全
            lock (poolLock)
            {
                if (eventPool.Count > 0)
                {
                    var evt = eventPool.Pop();
                    //evt.Reset();
                    return evt;
                }
                return new SNEvent();
            }
        }

        private static void ReturnEventToPool(SNEvent evt)
        {
            if (evt == null) return;

            evt.Reset();//keyle：回池重置一次就行
            lock (poolLock)
            {
                eventPool.Push(evt);
            }
        }

        private ISNEvent FindEventBySign(string sign)
        {
            foreach (var evt in Context)
            {
                if (evt.IdentifiedSign == sign)
                {
                    return evt;
                }
            }
            return null;
        }

        public SNContext()
          : base()
        {
            Context = new HashSet<ISNEvent>(16); // 预分配初始容量
            ContextModel = SNContextLevel.DEFAULT;
        }

        public string ContextModel { get; set; }

        protected HashSet<ISNEvent> Context { get; set; }

        public ISNContext RemoveSNEvent(ISNEvent sn)
        {
            if (Context.Remove(sn))
            {
                if (sn is SNEvent snEvent)
                {
                    ReturnEventToPool(snEvent);
                }
            }
            else
            {
                logBuilder.Length = 0;
                logBuilder.Append(sn.IdentifiedSign).Append(CANT_FIND_MESSAGE);
                Log(logBuilder.ToString());
            }

            return this;
        }

        public ISNContext RemoveSNEventByIdentifiedID(string identifiedID)
        {
            var eventToRemove = FindEventBySign(identifiedID);
            if (eventToRemove != null)
            {
                Context.Remove(eventToRemove);
                if (eventToRemove is SNEvent snEvent)
                {
                    ReturnEventToPool(snEvent);
                }
            }
            return this;
        }

        public override ISN Reset()
        {
            base.Reset();
            if (Context != null)
            {
                var events = new ISNEvent[Context.Count];
                Context.CopyTo(events);
                foreach (var evt in events)
                {
                    if (evt is SNEvent snEvent)
                    {
                        ReturnEventToPool(snEvent);
                    }
                    //evt.Reset();keyle：ReturnEventToPool回池会执行重置
                }
                Context.Clear();
            }
            else
            {
                Context = new HashSet<ISNEvent>(16);
            }
            ContextModel = SNContextLevel.DEFAULT;
            return this;
        }

        public ISNEvent UpdateSNEvent(ISNEvent sn)
        {
            var existingEvent = FindEventBySign(sn.IdentifiedSign);
            if (existingEvent != null)
            {
                Context.Remove(existingEvent);
                if (existingEvent is SNEvent oldEvent)
                {
                    ReturnEventToPool(oldEvent);
                }
            }
            Context.Add(sn);
            return sn;
        }

        public ISNEvent GetSNEvent(string identifiedOrEventName)
        {
            if (Context == null)
            {
                Context = new HashSet<ISNEvent>(16);
                return CreateSNEvent(identifiedOrEventName);
            }

            var existingEvent = FindEventBySign(identifiedOrEventName);
            if (existingEvent != null)
            {
                return existingEvent;
            }

            return CreateSNEvent(identifiedOrEventName);
        }

        public ISNEvent SetSNEvent(string identifiedOrEventName, ISNEvent value)
        {
            var existingEvent = FindEventBySign(value.IdentifiedSign);
            if (existingEvent != null)
            {
                Context.Remove(existingEvent);
                if (existingEvent is SNEvent oldEvent)
                {
                    ReturnEventToPool(oldEvent);
                }
                Context.Add(value);
                return value;
            }

            CreateSNEvent(identifiedOrEventName);
            return SetSNEvent(identifiedOrEventName, value);
        }

        public ISNEvent CreateSNEvent(string identifiedEventName)
        {
            ISNEvent e = GetEventFromPool();
            e.AutoRelease = false;
            e.IdentifiedSign = identifiedEventName;
            e.ParentContext = this;
            Context.Add(e);
            return e;
        }

        public ISNEvent CreateSNEvent(Delegate g)
        {
            // 获取方法上的所有特性
            var attributes = g.Method.GetCustomAttributes(false);
            SNMethodAttribute atr = null;

            // 查找SNMethodAttribute
            foreach (var attr in attributes)
            {
                if (attr is SNMethodAttribute methodAttr)
                {
                    atr = methodAttr;
                    break;
                }
            }

            // 如果没有找到SNMethodAttribute，检查是否是异步方法
            if (atr == null)
            {
                var returnType = g.Method.ReturnType;
                if (returnType == typeof(Task) ||
                    (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>)))
                {
                    // 对于异步方法，我们创建一个默认的SNMethodAttribute
                    atr = new SNMethodAttribute(g.Method.Name, true);  // 默认设置autoRelease为true
                }
                else
                {
                    // 如果既不是异步方法也没有SNMethodAttribute，抛出异常
                    throw new InvalidOperationException($"Method {g.Method.Name} must be marked with SNMethodAttribute or be an async method.");
                }
            }

            var existingEvent = FindEventBySign(atr.SNEventName);

            if (existingEvent != null)
            {
                return existingEvent.Register(atr.SNEventName, g);
            }

            ISNEvent e = GetEventFromPool();
            e.AutoRelease = atr.AutoRelease;
            e.IdentifiedSign = atr.SNEventName;
            e.ParentContext = this;
            e.Register(atr.SNEventName, g);
            return UpdateSNEvent(e);
        }

        public ISNEvent this[string identifiedSign]
        {
            get { return GetSNEvent(identifiedSign); }
            set { SetSNEvent(identifiedSign, value); }
        }

        public ISNContext Release(ISNEvent sn)
        {
            return RemoveSNEvent(sn);
        }

        public ISNContext Release(ISNEvent sn, string sign, Delegate handler)
        {
            // 细化release，精确移除特定的委托处理器
            if (sn == null || handler == null) return this;

            // 从事件中移除特定的委托处理器
            sn.Unregister(sign, handler);

            return this;
        }
    }
}
