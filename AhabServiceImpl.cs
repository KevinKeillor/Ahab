using System;
using System.IO;
using System.ServiceModel.Web;
using System.Drawing;
using System.Drawing.Imaging;


namespace AhabRestService
{
    public class AhabService : IAhabService
	{
       // string userPath = "C:\\Users\\user\\AppData\\Roaming\\XBMC\\userdata\\Database\\";
       // string VideoDatabase = "MyVideos60.db";


		[WebGet(ResponseFormat = WebMessageFormat.Json,
					UriTemplate = "movie/thumb/{id}")]
        public Stream GetMovieThumb(string id)
		{
            //Database bd = new Database(userPath + VideoDatabase);
			// lookup person with the requested id 
            

            System.Drawing.Image originalImg = System.Drawing.Image.FromFile(@"C:\Users\user\AppData\Roaming\XBMC\userdata\Thumbnails\9\9b979077.jpg");
            float imgRatio = 0.6F;//originalImg.Width / originalImg.Height;
            Image.GetThumbnailImageAbort thumbCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            System.Drawing.Image thumbNailImg = originalImg.GetThumbnailImage((int)(120 * imgRatio), 120, thumbCallback, IntPtr.Zero);
            originalImg.Dispose();
            thumbNailImg.Save(@"C:\img.png");            
            //FileStream stream = File.OpenRead(@"C:\img.png");
            WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
           // return stream as Stream;
            var stream = new System.IO.MemoryStream();
            thumbNailImg.Save(stream, ImageFormat.Png);
            thumbNailImg.Dispose();
            stream.Position = 0;
            return stream;

		}

        private bool ThumbnailCallback()
        {
            return false;
        }

	}
}