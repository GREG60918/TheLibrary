using System.ComponentModel.DataAnnotations;
using TheLibrary.Models;

namespace TheLibrary.DTOs
{
	public class CustomerDTO
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter customers name")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }
        
        public MembershipTypeDTO MembershipType { get; set; }

        public int MembershipTypeId { get; set; }

        //[Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }
    }
}
