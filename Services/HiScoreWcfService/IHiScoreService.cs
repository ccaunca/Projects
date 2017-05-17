using FallDown.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace HiScoreWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IHiScoreService
    {
        [OperationContract]
        [WebGet(UriTemplate="/n/{n}")]
        IEnumerable<FallDown_GetNHiScores_Result> GetNScores(string n);

        [OperationContract]
        [WebGet(UriTemplate = "/hiscore/{hiScore}/name/{name}")]
        bool InsertHiScore(string hiScore, string name);
    }
}
