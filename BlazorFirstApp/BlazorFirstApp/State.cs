using System.ComponentModel.DataAnnotations;

namespace BlazorFirstApp
{
    public class State
    {
        public string Role { get; set; } = "Admin";

        [MaxLength(5)]
        public string Author { get; set; } = "Pesho";
    }
}
