using System.ServiceModel;

namespace WcfJsonRestService
{
	[ServiceContract]
	public interface IService1
	{
		[OperationContract]
		Person GetData(string id);
	}
}
