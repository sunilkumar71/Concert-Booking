using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Identity:membership program : Authentication-Credentials(UserName & Password)
// & Authorization (Access Rights)
//Authentication:
//Register: IdentityUser Class -Id(Guid),UserName,Password,Email,Phone
          //SignInManager-Check User Signin, User SignIn
          //UserManager: Store user data in database ,get user information form database,add role to user
          //IdentityRole: ID,Name
          //Claims:Piece of information about user ,aadhar card
          // ClaimsIdentity=List<Claim>
namespace ConcertBooking.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public string? FirstName {  get; set; }
        public string? Address{ get; set; }
        public string? Pincode{ get; set; }
    }
}
