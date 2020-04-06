using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Web.ViewModels.Profiles
{
    public class ProfileViewModel
    {
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "Дата рождения")]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime BirthDate {
            get 
            {
                return this._birthDate.HasValue ? this._birthDate.Value : DateTime.Now.AddYears(-25);
            }
            set { this._birthDate = value; }
        }

        private DateTime? _birthDate = null;

        [Display(Name = "Обо мне")]
        public string AboutMe { get; set; }

        [Display(Name = "Тел.")]
        public string Phone { get; set; }
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        [Display(Name = "Сайт")]
        [Url]
        public string Site { get; set; }


        [Display(Name = "Новое фото профиля")]
        public IFormFile ProfilePicture { get; set; }
        public byte[] Avatar { get; set; }

        [Display(Name = "Полное имя")]
        public string FullName
        {
            get
            {
                return $"{LastName} {FirstName}";
            }
        }

    }
}
