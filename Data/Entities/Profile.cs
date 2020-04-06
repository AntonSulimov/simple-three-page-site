using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entities
{
    public class Profile
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }


        public string AboutMe { get; set; }
        

        public string Phone { get; set; }
        
        public string Address { get; set; }
        
        public string Site { get; set; }
        

        
        public byte[] ProfilePicture { get; set; }
    }
}
