using Nest;

namespace Aju.Carefree.Api
{
    public interface IEsClientProvider
    {
        ElasticClient GetClient();
    }
}
