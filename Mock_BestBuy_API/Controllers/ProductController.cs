using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Mock_BestBuy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepository _repo;

        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var products = _repo.GetProducts();
            return Ok(JsonConvert.SerializeObject(products));
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _repo.GetProduct(id);
            return Ok(JsonConvert.SerializeObject(product));
        }

        [HttpPost]
        public void Post(Product product)
        {
            product.ProductID = ++_repo.GetProducts().LastOrDefault().ProductID;
            _repo.InsertProduct(product);
        }

        [HttpPut("{id}")]
        public void Put(int id, Product product)
        {
            product.ProductID = id;
            _repo.UpdateProduct(product);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var productToDelete = _repo.GetProduct(id);
            _repo.DeleteProduct(productToDelete);
        }
    }
}
