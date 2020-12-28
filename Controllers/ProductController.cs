using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace http_asp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        static List<Product> products = new List<Product>();

        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return products.ToArray();
        }

        [HttpPut]
        public Product Put(Product p)
        {
            p.Id = products.Count > 0 ? products[products.Count - 1].Id + 1 : 0;
            products.Add(p);
            return p;
        }

        [HttpPost]
        public ActionResult<Product> Post(Product p)
        {
            Product t = products.Find(i => i.Id == p.Id);
            if(t == null)
                return BadRequest();
            t.Name = p.Name;
            t.Count = p.Count;
            t.Cost = p.Cost;
            return p;
        }

        [HttpDelete]
        public ActionResult Post(IEnumerable<int> d)
        {
            List<Product> t = products.FindAll(i => d.Any(c => c == i.Id));
            foreach(var p in t)
                products.Remove(p);
            return Accepted();
        }

        [HttpGet("/sum")]
        public double Sum()
        {
            double s = 0;
            products.ForEach(p => s += p.Cost * p.Count);
            return s;
        }
    }
}
