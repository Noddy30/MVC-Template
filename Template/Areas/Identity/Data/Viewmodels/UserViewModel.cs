using System;
namespace Template.Areas.Identity.Data.Viewmodels
{
	public class UserViewModel
	{
        public string Id = Guid.NewGuid().ToString();
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string? Password { get; set; }


    }
}

