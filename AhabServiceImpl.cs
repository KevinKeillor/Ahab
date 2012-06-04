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
                    UriTemplate = "movie/info/{id}")]
        public String GetMovieInfoList()
        {
            string userPath = "C:\\Users\\user\\AppData\\Roaming\\XBMC\\userdata\\Database\\";
            string VideoDatabase = "MyVideos60.db";

            MovieDatabase MovieDB = new MovieDatabase();
            MovieDB.SetDbSource(userPath + VideoDatabase);
            MovieDB.Open();
            List<MovieInfo> list = MovieDB.GetMovieInfoList();
            MovieDB.Close();
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(MovieInfo));

            MemoryStream ms = new MemoryStream();

            XmlDictionaryWriter writer = JsonReaderWriterFactory.CreateJsonWriter(ms);

            json.WriteObject(ms,list);

            writer.Flush(); 

            String jsonString = Encoding.Default.GetString(ms.GetBuffer());
            return jsonString;
        }

	}
}