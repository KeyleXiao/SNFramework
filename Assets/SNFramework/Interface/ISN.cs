//if you hava any problem please email to keyle_xiao@hotmail.com
namespace SNFramework
{
  public interface ISN
  {
    string IdentifiedSign { get; set; }

    ISN Reset ();

    ISN Log (string msg);
  }
}
