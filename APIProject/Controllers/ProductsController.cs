using Microsoft.AspNetCore.Mvc;


namespace APIProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController: ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public ProductsController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public ActionResult<List<Product>> GetProducts()
        {
            var products = _context.Set<Product>().ToList();
            return Ok(products);
        }
        [HttpPost]
        [Route("")]
        
        public ActionResult<int> CreateProduct(Product product1)
        {
            product1.Id = 0; 
            _context.Set<Product>().Add(product1);
            _context.SaveChanges();
            return Ok(product1.Id);
        }
        [HttpPut]
        [Route("")]
        public ActionResult UpdateProduct(Product product1)
        {
            var existingProduct = _context.Set<Product>().Find(product1.Id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.Name = product1.Name;
            existingProduct.Sku = product1.Sku;
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _context.Set<Product>().Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpDelete]
        [Route("")]
        public ActionResult DeleteProduct(int id)
        {
            var existingProduct = _context.Set<Product>().Find(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            _context.Set<Product>().Remove(existingProduct);
            _context.SaveChanges();
            return Ok();
        }
       
    }
}
