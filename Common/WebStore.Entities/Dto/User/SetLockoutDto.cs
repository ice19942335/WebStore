using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Entities.Dto.User
{
    public class SetLockoutDto
    {
        public Entities.Identity.User User { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
