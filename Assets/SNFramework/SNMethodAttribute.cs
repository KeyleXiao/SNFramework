//if you hava any problem please email to keyle_xiao@hotmail.com
using System;
namespace SNFramework
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class SNMethodAttribute : Attribute
    {
        public readonly string MsgName;
        public readonly string SNEventName;
        public readonly bool AutoRelease;
        public readonly int GroupID;

        public SNMethodAttribute(string sNEventName,string msgName,int groupID = 0,bool autoRelease = true)
        {
            MsgName = msgName;
            AutoRelease = autoRelease;
            GroupID = groupID;
            SNEventName = sNEventName;
        }
    }
}
