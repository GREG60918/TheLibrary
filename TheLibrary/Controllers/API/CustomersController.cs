using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheLibrary.Data;
using TheLibrary.DTOs;
using TheLibrary.Models;

namespace TheLibrary.Controllers.API
{
	[Route("api/Customers")]
	[ApiController]
	public class CustomersController : ControllerBase
	{
		private TheLibraryDbContext _theLibraryDbContext;

		private readonly IMapper _mapper;

		public CustomersController(IMapper mapper)
		{
			_theLibraryDbContext = new TheLibraryDbContext();
			_mapper = mapper;
		}

		[HttpGet]
		public ActionResult<IEnumerable<CustomerDTO>> GetCustomers()
		{
			return _theLibraryDbContext.Customers
				.Include(c => c.MembershipType)
				.Select(_mapper.Map<Customer, CustomerDTO>)
				.ToList();
		}

		[HttpGet("{id}")]
		public ActionResult<CustomerDTO> GetCustomer(int id)
		{
			var customer = _theLibraryDbContext.Customers.SingleOrDefault(c => c.Id == id);

			if(customer == null)
			{
				return NotFound();
			}

			return Ok(_mapper.Map<Customer, CustomerDTO> (customer));
		}

		[HttpPost]
		public ActionResult<CustomerDTO> CreateCustomer(CustomerDTO customerDTO)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest();
			}

			var customer = _mapper.Map<CustomerDTO, Customer>(customerDTO);

			_theLibraryDbContext.Customers.Add(customer);

			_theLibraryDbContext.SaveChanges();

			customerDTO.Id = customer.Id;

			return Created(new Uri(Request.GetDisplayUrl() + "/" + customerDTO.Id), customerDTO);
		}

		[HttpPut("{id}")]
		public ActionResult UpdateCustomer(int id, CustomerDTO customerDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var customerInDb = _theLibraryDbContext.Customers.SingleOrDefault(c => c.Id == id);

			if (customerDTO == null)
			{
				return NotFound();
			}

			_mapper.Map(customerDTO, customerInDb);

			_theLibraryDbContext.SaveChanges();

			return Ok();
		}

		[HttpDelete("{id}")]
		public ActionResult DeleteCustomer(int id)
		{
			var customer = _theLibraryDbContext.Customers.SingleOrDefault(c => c.Id == id);

			if (customer == null)
			{
				return NotFound();
			}

			_theLibraryDbContext.Remove(customer);

			_theLibraryDbContext.SaveChanges();

			return Ok();
		}
	}
}
