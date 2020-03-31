using System.ComponentModel.DataAnnotations;

namespace Project1.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}