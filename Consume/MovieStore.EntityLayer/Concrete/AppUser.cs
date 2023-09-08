using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.EntityLayer.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string FirtName { get; set; }
        public string LastName { get; set; }
    }
}
