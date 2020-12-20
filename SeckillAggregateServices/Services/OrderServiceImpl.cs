using Projects.Cores.DynamicMiddleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Services
{
    public class OrderServiceImpl
    {
        private readonly IDynamicMiddlewareService _dynamicMiddlewareService;

        private string scheme = "http";
        private string orderService = "OrderServices";
        public OrderServiceImpl(IDynamicMiddlewareService  dynamicMiddlewareService)
        {
            _dynamicMiddlewareService = dynamicMiddlewareService;
        }

        public IList<object> GetOrders(string serviceLink,IDictionary<string,object> paramsObject)
        {
            return _dynamicMiddlewareService.GetList<object>(scheme, orderService, serviceLink, paramsObject);
        }
    }
}
