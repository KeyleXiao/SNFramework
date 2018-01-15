//if you hava any problem please email to keyle_xiao@hotmail.com
using System;
using System.Collections.Generic;

namespace SNFramework
{
  public interface ISNContext : ISN
  {
    //    List<ISNEvent> Context { get; set; }

    string ContextModel { get; set; }

    ISNEvent GetSNEvent (string sign);

    ISNEvent CreateSNEvent (string identifiedEventName);

    ISNEvent CreateSNEvent (Delegate g);

    ISNEvent SetSNEvent (string identifiedOrEventName, ISNEvent value);

    ISNContext RemoveSNEvent (ISNEvent sn);

    ISNEvent UpdateSNEvent (ISNEvent sn);
  }
}
