using TheLibrary.Models;

namespace TheLibrary.ViewModels
{
    public class RandomBookViewModel
    {
        public Book Book { get; set; }

        public List<Customer> Customers { get; set; }
    }
}
