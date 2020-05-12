﻿using Microsoft.AspNetCore.Identity;

namespace TaskManager.Entities.Tables.Identity
{
    public class Role: IdentityRole<int>
    {
        public string Description { get; set; }
    }
}
