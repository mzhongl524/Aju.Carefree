using Microsoft.AspNetCore.Mvc;
using Nest;
using System.Collections.Generic;

namespace Aju.Carefree.Api.Controllers
{
    /*
     * 更多方法参考：
     *  GitHub：elastic/elasticsearch-net（https://github.com/elastic/elasticsearch-net）
     *  Elasticsearch.Net and NEST: the .NET clients（ https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/index.html）
     */

    [Route("api/[controller]")]
    [ApiController]
    public class ElasticsearchController : ControllerBase
    {
        private readonly ElasticClient _client;
        public ElasticsearchController(IEsClientProvider esClientProvider) => _client = esClientProvider.GetClient();

        [HttpPost]
        [Route("elasticsearch/index")]
        public IIndexResponse Index(Post post)
        {
            return _client.IndexDocument(post);
        }

        [HttpPost]
        [Route("elasticsearch/search")]
        public IReadOnlyCollection<Post> Search(string type)
        {
            return _client.Search<Post>(s => s
                .From(0)
                .Size(10)
                .Query(q => q.Match(m => m.Field(f => f.Type).Query(type)))).Documents;
        }
    }

    public class Post
    {
        public int Id { get; set; }
        public string Index { get; set; }
        public string Type { get; set; }
    }
}