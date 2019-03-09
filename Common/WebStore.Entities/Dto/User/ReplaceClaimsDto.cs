using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace WebStore.Entities.Dto.User
{
    public class ReplaceClaimsDto
    {
        public Entities.Identity.User User { get; set; }

        public Claim Claim { get; set; }

        public Claim NewClaim { get; set; }
    }
}
