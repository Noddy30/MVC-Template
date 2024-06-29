﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Template.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AuthUser class
public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ProfilePicture { get; set; }
    public string? IdNumber { get; set; }
    public float? Handicap { get; set; }

    public bool? IsDeleted { get; set; }
}

