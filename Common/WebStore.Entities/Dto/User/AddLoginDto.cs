using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace WebStore.Entities.Dto.User
{
    public class AddLoginDto
    {
        public Entities.Identity.User User { get; set; }

        public  UserLoginInfo UserLoginInfo { get; set; }
    }
}
