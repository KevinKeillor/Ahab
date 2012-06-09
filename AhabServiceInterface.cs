using System.ServiceModel;
using System.IO;
using System;
using System.Collections.Generic;

namespace AhabRestService
{
    [ServiceContract]
    public interface IAhabService
    {


        // Movie information
        [OperationContract]
        Stream GetMovieThumb(string id);
        [OperationContract]
        List<MovieSumary> GetMovieInfoList();
        [OperationContract]
        List<Artist> GetMovieArtistList();

    }
}
