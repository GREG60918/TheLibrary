using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheLibrary.Data;
using TheLibrary.DTOs;
using TheLibrary.Models;

namespace TheLibrary.Controllers.API
{
	[Route("api/Books")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		private TheLibraryDbContext _theLibraryDbContext;

		private readonly IMapper _mapper;

		public BooksController(IMapper mapper)
		{
			_theLibraryDbContext = new TheLibraryDbContext();
			_mapper = mapper;
		}

		[HttpGet]
		public ActionResult<IEnumerable<BookDTO>> GetBooks()
		{
			return _theLibraryDbContext.Books
				.Include(b => b.Genre)
				.Select(_mapper.Map<Book, BookDTO>)
				.ToList();
		}

		[HttpGet("{id}")]
		public ActionResult<BookDTO> GetBook(int id)
		{
			var book = _theLibraryDbContext.Books.SingleOrDefault(c => c.Id == id);

			if (book == null)
			{
				return NotFound();
			}

			return Ok(_mapper.Map<Book, BookDTO>(book));
		}

		[HttpPost]
		public ActionResult<BookDTO> CreateBook(BookDTO bookDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var book = _mapper.Map<BookDTO, Book>(bookDTO);

			_theLibraryDbContext.Books.Add(book);

			_theLibraryDbContext.SaveChanges();

			bookDTO.Id = book.Id;

			return Created(new Uri(Request.GetDisplayUrl() + "/" + bookDTO.Id), bookDTO);
		}

		[HttpPut("{id}")]
		public ActionResult UpdateBook(int id, BookDTO bookDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var bookInDb = _theLibraryDbContext.Books.SingleOrDefault(c => c.Id == id);

			if (bookDTO == null)
			{
				return NotFound();
			}

			_mapper.Map(bookDTO, bookInDb);

			_theLibraryDbContext.SaveChanges();

			return Ok();
		}

		[HttpDelete("{id}")]
		public ActionResult DeleteBook(int id)
		{
			var book = _theLibraryDbContext.Books.SingleOrDefault(c => c.Id == id);

			if (book == null)
			{
				return NotFound();
			}

			_theLibraryDbContext.Remove(book);

			_theLibraryDbContext.SaveChanges();

			return Ok();
		}
	}
}
