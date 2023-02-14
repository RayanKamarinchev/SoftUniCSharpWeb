using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Grpc_01.Services
{
    public class ProductGrpcService : Product.ProductBase
    {
        public override Task<ProductList> GetAll(Empty request, ServerCallContext context)
        {
            return base.GetAll(request, context);
        }
    }
}
