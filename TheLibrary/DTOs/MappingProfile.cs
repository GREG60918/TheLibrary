using AutoMapper;
using TheLibrary.Models;

namespace TheLibrary.DTOs
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Customer, CustomerDTO>().ForMember(m => m.Id, opt => opt.Ignore()); ;
			CreateMap<CustomerDTO, Customer>();
			
			CreateMap<Book, BookDTO>().ForMember(m => m.Id, opt => opt.Ignore()); ;
			CreateMap<BookDTO, Book>();


			CreateMap<MembershipType, MembershipTypeDTO>();

			CreateMap<Genre, GenreDTO>();
		}
	}
}
