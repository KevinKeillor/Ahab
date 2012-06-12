using System;
using System.IO;
using System.ServiceModel.Web;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Text;
using System.Collections.Generic;


namespace AhabRestService
{
    public class AhabService : IAhabService
	{


		[WebGet(ResponseFormat = WebMessageFormat.Json,
					UriTemplate = "movie/thumb/{id}")]
        public Stream GetMovieThumb(string id)
		{
            string userPath = "C:\\Users\\user\\AppData\\Roaming\\XBMC\\userdata\\Database\\";
            string VideoDatabase = "MyVideos60.db";

            MovieDatabase MovieDB = new MovieDatabase();
            MovieDB.SetDbSource(userPath + VideoDatabase);
            MovieDB.SetThumbsPath("C:\\Users\\user\\AppData\\Roaming\\XBMC\\userdata\\Thumbnails\\Video\\");
            MovieDB.Open();
            Stream movieThumb = MovieDB.GetMovieThumb(Int32.Parse(id));
            MovieDB.Close();
            WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
            return movieThumb;

		}

        [WebGet(ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "movie/info")]
        public List<MovieSumary> GetMovieInfoList()
        {
            string userPath = "C:\\Users\\user\\AppData\\Roaming\\XBMC\\userdata\\Database\\";
            string VideoDatabase = "MyVideos60.db";

            MovieDatabase MovieDB = new MovieDatabase();
            MovieDB.SetDbSource(userPath + VideoDatabase);
            MovieDB.Open();
            List<MovieSumary> list = MovieDB.GetMovieSummaryList();
            MovieDB.Close();

            return list;
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "movie/artist")]
        public List<Artist> GetMovieArtistList()
        {
            string userPath = "C:\\Users\\user\\AppData\\Roaming\\XBMC\\userdata\\Database\\";
            string VideoDatabase = "MyVideos60.db";

            MovieDatabase MovieDB = new MovieDatabase();
            MovieDB.SetDbSource(userPath + VideoDatabase);
            MovieDB.Open();
            List<Artist> list = MovieDB.GetMovieArtistList();
            MovieDB.Close();

            return list;
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "movie/info/{Id}")]
        public List<MovieSumary> UpdateMovieInfoList(String Id)
        {
            string userPath = "C:\\Users\\user\\AppData\\Roaming\\XBMC\\userdata\\Database\\";
            string VideoDatabase = "MyVideos60.db";

            MovieDatabase MovieDB = new MovieDatabase();
            MovieDB.SetDbSource(userPath + VideoDatabase);
            MovieDB.Open();
            List<MovieSumary> list = MovieDB.GetMovieSummaryList();
            MovieDB.Close();

            return list;
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "movie/artist/{Id}")]
        public List<Artist> UpdateMovieArtistList(String Id)
        {
            string userPath = "C:\\Users\\user\\AppData\\Roaming\\XBMC\\userdata\\Database\\";
            string VideoDatabase = "MyVideos60.db";

            MovieDatabase MovieDB = new MovieDatabase();
            MovieDB.SetDbSource(userPath + VideoDatabase);
            MovieDB.Open();
            List<Artist> list = MovieDB.GetMovieArtistList();
            MovieDB.Close();

            return list;
        }

	}
}