using System.ServiceModel;

namespace ScaleSimulator
{
    [ServiceContract(Namespace = "https://scalesim.becampbell.com.au")]
    public interface IPCScaleSimContract
    {
        [OperationContract]
        int getWeight(out decimal grossWeight);
    }
}
