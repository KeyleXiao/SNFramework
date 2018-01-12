//if you hava any problem please email to keyle_xiao@hotmail.com
namespace SNFramework
{
    public interface ISN
    {
        string IdentifiedSign { get;}
        
        ISN Reset();
        ISN WarningLog(string msg);
        ISN ErrorLog(string msg);

        ISN GetSNObject(string identifiedID);
        
    }
}
