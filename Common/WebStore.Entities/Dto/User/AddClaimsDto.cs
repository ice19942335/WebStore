using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace WebStore.Entities.Dto.User
{
    public class AddClaimsDto
    {
        public Entities.Identity.User User { get; set; }

        public IEnumerable<Claim> Claims { get; set; }
    }
}
