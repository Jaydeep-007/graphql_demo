using graphql_demo.Entities;
using graphql_demo.Repositories;

namespace graphql_demo.GraphQL.Types
{
    public class ProductQueryTypes
    {
        public async Task<List<ProductDetails>> GetProductListAsync([Service] IProductService productService)
        {
            return await productService.ProductListAsync();
        }

        public async Task<ProductDetails> GetProductDetailsByIdAsync([Service] IProductService productService, Guid productId)
        {
            return await productService.GetProductDetailByIdAsync(productId);
        }
    }
}
