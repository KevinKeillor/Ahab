using System.ServiceModel;
using System.IO;

namespace AhabRestService
{
    [ServiceContract]
    public interface IAhabService
    {
        [OperationContract]
        Stream GetMovieThumb(string id);
    }
}
