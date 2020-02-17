using System.ComponentModel.DataAnnotations;
namespace DevicesSearch.RBACs.Roles.SearchObjects
{
    public class AuthenticateModel
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }
    }
}