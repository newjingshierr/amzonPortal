
using System.Net.Http;
using System.Web.Http;
using RestAPI.Models;



namespace RestAPI.Controllers
{

    public class ProductRequest
    {
        public string Name;
        public float Price;
        public string Category;
    }

    public class ProductsController : ApiController
    {
       
        [HttpPost]
        public object CreateProduct(ProductRequest productRequest)
        {
            using (RestAPIContext content = new RestAPIContext())
            {
                Product product = new Product();
                product.Key = System.Guid.NewGuid();
                product.Name = productRequest.Name;
                product.Price = productRequest.Price;
                product.Category = productRequest.Category;
                content.Products.Add(product);
                return content.SaveChanges();   
                
             
            }
              
        }

    }
}
