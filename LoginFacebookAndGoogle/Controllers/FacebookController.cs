using Entity;
using Facebook;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LoginFacebookAndGoogle.Controllers
{
    public class FacebookController : Controller
    {
        private const string FacebookAccessToken = "FacebookAccessToken";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FacebookLogin()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public void ExternalLogin(string provider, string returnUrl = null)
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("ExternalLoginCallback", "Facebook") };
            ControllerContext.HttpContext.GetOwinContext().Authentication.Challenge(properties, provider);

        }

        public async Task<ActionResult> ExternalLoginCallback()
        {
            Account account = null;
            var info = await AuthenticationManager.GetExternalLoginInfoAsync();

            if (info == null) {
                return RedirectToAction("Index", "Home"); 
            }
            if (info.Login.LoginProvider == "Facebook")
            {
                FacebookClient fb = new FacebookClient();
                var identity = AuthenticationManager.GetExternalIdentity(DefaultAuthenticationTypes.ExternalCookie);
                var accessToken = identity.FindFirstValue(FacebookAccessToken);
                fb.AccessToken = accessToken;
                var facebookInfo = fb.Get(ConfigurationManager.AppSettings["FacebookUri"]);
                FacebookEntity facebookEntity = JsonConvert.DeserializeObject<FacebookEntity>(facebookInfo.ToString());
                account = new Account();
                account.Id = facebookEntity.Id;
                account.Name = facebookEntity.Name;
                account.Email = facebookEntity.Email;
                account.FirstName = facebookEntity.first_name;
                account.LastName = facebookEntity.last_name;
            }
            else if (info.Login.LoginProvider == "Google")
            {
                string googleInfo = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "GoogleInfo").Value;
                GoogleEntity ObjectGoogle = JsonConvert.DeserializeObject<GoogleEntity>(googleInfo);
                account = new Account();
                account.Id = ObjectGoogle.id;
                account.Name = ObjectGoogle.name;
                account.Email = ObjectGoogle.email;
                account.FirstName = ObjectGoogle.given_name;
                account.LastName = ObjectGoogle.family_name;
            }

            return View("ExternalLoginConfirmation", account);
        }
        [HttpPost]
        public ActionResult ExternalLoginConfirmation(Account model, string returnUrl = null)
        {
            //ToDo
            return View("Index");
        }
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }


    }
}