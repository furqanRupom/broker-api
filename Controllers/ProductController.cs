using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


[Route("/api/products")]
[ApiController]

public class ProductController : ControllerBase
{
	private static List<Product> products = new List<Product>();

	// Retrieve all products
	[HttpGet]
	public ActionResult<List<Product>> GetAll() => products;

	// Retrive Product By ID

	[HttpGet("{id}")]
	public ActionResult<Product> GetById(int id)
	{
		var product = products.FirstOrDefault((product) => product.Id == id);
		return product != null ? Ok(product) : NotFound();	
	}

	// Create New Product
	[HttpPost]
	public ActionResult<Product> NewProduct(Product product)
	{
		product.Id = products.Count + 1;
		products.Add(product);
		return CreatedAtAction(nameof(GetById), new {Id = product.Id},product);
	}

	// Update Product
	[HttpPut("{id}")]
	public ActionResult<Product> UpdateProduct(int id,Product updateProduct)
	{
		var product = products.FirstOrDefault((product) => product.Id == id);
		if (product == null) return NotFound();
		product.Name = updateProduct.Name;
		product.Description = updateProduct.Description;
		product.Price = updateProduct.Price;
		return Ok(product);
	}
	// Delete Product
	[HttpDelete("{id}")]
	public ActionResult DeleteProduct(int id)
	{
		var product = products.FirstOrDefault((product) => product.Id == id);
		if(product == null) return NotFound();
		products.Remove(product);
		return NoContent();
	}
}
