//if you hava any problem please email to keyle_xiao@hotmail.com
using System;
using System.Collections.Generic;

namespace SNFramework
{
    public interface ISNContext : ISN
    {
        //    List<ISNEvent> Context { get; set; }

        string ContextModel { get; set; }

        ISNEvent GetSNEvent(string sign);

        ISNEvent CreateSNEvent(string identifiedEventName);

        ISNEvent CreateSNEvent(Delegate g);

        ISNEvent SetSNEvent(string identifiedOrEventName, ISNEvent value);

        ISNContext RemoveSNEvent(ISNEvent sn);

        ISNEvent UpdateSNEvent(ISNEvent sn);

        ISNContext Release(ISNEvent sn);

        /// <summary>
        /// 细化的释放方法，可以精确移除特定的委托处理器
        /// </summary>
        /// <param name="sn">事件对象</param>
        /// <param name="sign">事件标识</param>
        /// <param name="handler">要移除的委托处理器</param>
        /// <returns></returns>
        ISNContext Release(ISNEvent sn, string sign, Delegate handler);
    }
}
