//if you hava any problem please email to keyle_xiao@hotmail.com
using System;
using System.Collections.Generic;

namespace SNFramework
{
  public interface ISNContext : ISN
  {
    List<ISNEvent> Context { get; set; }

    SNContextType ContextModel { get; set; }

    ISNEvent GetSNEvent (string sign);

    ISNContext RemoveSNEvent (ISNEvent sn);

    ISNContext UpdateSNEvent (ISNEvent sn);
  }
}
