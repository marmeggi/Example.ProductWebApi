using Example.WebApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using System.Xml.Linq;

namespace Example.WebApi.Controllers
{
    public class ProductController : ApiController
    {
        public List<Product> listOfProducts;

        public ProductController()
        {
            listOfProducts = new List<Product>()
            {
                new Product { Id = 1, Name ="Milk", Price = 5 },
                new Product { Id = 2, Name = "Bread", Price = 10 },
                new Product { Id = 3, Name = "Apple", Price = 2 },
            };
        }

        // GET api/product
        public List<Product> Get()
        {
            return listOfProducts;
        }

        // GET api/product/5
        public HttpResponseMessage Get(int id)
        {
            Product product = listOfProducts.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, product);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Product not found.");
            }
        }

        // POST api/product
        /* public HttpResponseMessage Post([FromBody]  Product productToCreate)
         {
             listOfProducts.Add(productToCreate);

             return Request.CreateResponse(HttpStatusCode.Created, listOfProducts);

         }
        */
        public HttpResponseMessage Post([FromUri] Product productToCreate)
        {
            listOfProducts.Add(productToCreate);

            return Request.CreateResponse(HttpStatusCode.Created, listOfProducts);

        }

        // PUT api/product/5
        public HttpResponseMessage Put(int id, [FromBody] Product updatedProduct)
        {
            Product productToUpdate = listOfProducts.FirstOrDefault(p => p.Id == id);

            if (productToUpdate != null)
            {
                productToUpdate.Name = updatedProduct.Name;
                productToUpdate.Price = updatedProduct.Price;

                return Request.CreateResponse(HttpStatusCode.OK, productToUpdate);
            }
            else return Request.CreateResponse(HttpStatusCode.NotFound, "Product not found.");
        }

        // DELETE api/product/5
        public HttpResponseMessage Delete(int id)
        {
            Product productToDelete = listOfProducts.FirstOrDefault(p => p.Id == id);

            if (productToDelete != null)
            {
                listOfProducts.Remove(productToDelete);
                return Request.CreateResponse(HttpStatusCode.OK, "Product deleted successfully.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Product not found.");
            }
        }
    }
}