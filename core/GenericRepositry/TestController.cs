using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Vega.Models;

namespace Vega.core.GenericRepositry
{
    [Route("/api/Test")]
    public class TestController : Controller
    {
        public TestController(IRepository <Vehicle> vehichleRepsoitory)
        {
            _vehichleRepsoitory = vehichleRepsoitory;
        }

        public IRepository<Vehicle> _vehichleRepsoitory { get; }

        [HttpGet]
            public  IEnumerable<Vehicle> GetAllVehichles()
            {
               return _vehichleRepsoitory.GetAll(); 
            }
    }
}