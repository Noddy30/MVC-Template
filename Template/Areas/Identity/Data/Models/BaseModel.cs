using System;
namespace Template.Areas.Identity.Data.Models
{
	public class BaseModel
	{
        public string PublicId { get; set; } = Guid.NewGuid().ToString();
        public bool IsDeleted { get; set; } = false;
        public DateTime Created { get; set; } = DateTime.Now;
    }
}

