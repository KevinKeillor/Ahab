using System;
using System.ServiceModel.Web;


namespace WcfJsonRestService
{
	public class Service1 : IService1
	{
        string userPath = "C:\\Users\\user\\AppData\\Roaming\\XBMC\\userdata\\Database\\";
        string VideoDatabase = "MyVideos60.db";


		[WebInvoke(Method = "GET",
					ResponseFormat = WebMessageFormat.Json,
					UriTemplate = "data/{id}")]
		public Person GetData(string id)
		{
            Database bd = new Database(userPath + VideoDatabase);
			// lookup person with the requested id 
			return new Person()
			{
				Id = Convert.ToInt32(id),
				Name = "Leo Messi"
			};
		}

	}

	public class Person
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}