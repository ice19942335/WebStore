using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Entities.ViewModels.Account
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "Имя пользователя является обязательным", AllowEmptyStrings = false), MaxLength(256, ErrorMessage = "Длина имени ограничена 256 символами")]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Отсутствует пароль"), DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }
}
