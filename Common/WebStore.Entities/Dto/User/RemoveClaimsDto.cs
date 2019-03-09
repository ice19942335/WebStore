using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace WebStore.Entities.Dto.User
{
    public class RemoveClaimsDto
    {
        public Entities.Identity.User User { get; set; }

        public IEnumerable<Claim> Claims { get; set; }
    }
}
