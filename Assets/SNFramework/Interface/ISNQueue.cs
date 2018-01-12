//if you hava any problem please email to keyle_xiao@hotmail.com
using System;
using System.Collections.Generic;

namespace SNFramework
{
  public interface ISNQueue : ISN
  {

    SNQueueType QueueModel { get; set; }

    ISNEvent Expression { get; set; }

    List<ISNEvent> Queue { get; set; }

    ISNEvent GetSNEvent (string sign);

    [Obsolete ("No implement ...", true)]
    ISNQueue SetQueueModel (SNQueueType t);

    [Obsolete ("No implement ...", true)]
    ISNQueue SetExpression (ISNEvent sn);



    ISNQueue RemoveSNEvent (ISNEvent sn);

    ISNQueue RemoveSNEventByIdentifiedID (string identifiedID);

    ISNQueue UpdateSNEvent (ISNEvent sn);
  }
}
