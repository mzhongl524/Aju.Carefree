using Nest;

namespace Aju.Carefree.WebManager
{
    public interface IEsClientProvider
    {
        ElasticClient GetClient();
    }
}
