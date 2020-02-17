using System.ComponentModel.DataAnnotations;
namespace CommandManagement.Domains.RBACs.ValueObjects
{
    public class AuthenticateInfo
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }
    }
}
