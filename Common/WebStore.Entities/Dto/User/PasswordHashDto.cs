using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Entities.Dto.User
{
    public class PasswordHashDto
    {
        public Entities.Identity.User User { get; set; }

        public string Hash { get; set; }
    }
}
