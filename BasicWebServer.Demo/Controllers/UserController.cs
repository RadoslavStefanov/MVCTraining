using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Demo.Controllers
{
    public class UserController : Controller
    {

        private const string LoginForm = @"<form action='/Login' method='POST'>
                Username: <input type='text' name='Username'/>
                Password: <input type='text' name='Password'/>
                <input type='submit' value ='Log In' /> 
            </form>";

        private const string Username = "user";
         
        private const string Password = "123";

        public UserController(Request request) : base(request)
        {}

        public Response Login() =>View();

        public Response LogInUser()
        {
            Request.Session.Clear();

            var usernameMatches =
                this.Request.Form["Username"] == UserController.Username;
            var passwordMatches =
                this.Request.Form["Password"] == UserController.Password;

            if (usernameMatches && passwordMatches)
            {
                if (!this.Request.Session.ContainsKey(Session.SessionUserKey))
                {
                    this.Request.Session[Session.SessionUserKey] = "MyUserId";

                    var cookies = new CookieCollection();
                    cookies.Add(Session.SessionCookieName, Request.Session.Id);

                    return Html("<h3>Logged successfully</h3>", cookies);
                }

                return Html("<h3>Logged successfully</h3>");
            }
            return Redirect("/login");
        }

        public Response LogOut()
        {
            Request.Session.Clear();
            return Html("<h3>Logged out!</h3>");
        }

        public Response GetUserData()
        {
            if (Request.Session.ContainsKey(Session.SessionUserKey))
            {
                return Html($"<h3>Currently logged-in user is '{Username}'</h3>");
            }
            return Redirect("/login");

        }
    }
}
