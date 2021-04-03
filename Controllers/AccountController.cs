using Login.Models;
using Login.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Drawing;
using Newtonsoft.Json;


// exo : creer un controller account avec quatre action qui mene a quatre vues differentt
//1 er : login 
// 2eme : register
// 3eme : forgot password
// 4 : terms and condition
// les contres vues sont interconnectes par des liens sur les pages
namespace Login.Controllers
{
    public class AccountController : Controller
    {
        private const string V = "<br/><br/> we are excited to tell you that your dotnet awesome account is" +
                            "successfully created. please click on the below link to verify your account" +
                            " <br/><br/><a href=";

        public object Email { get; private set; }

        // GET: account


        public ActionResult LOGIN(string returnUrl)
        {
            return View(new LoginModel { ReturnUrl = returnUrl});
        }
        [HttpPost]
        public ActionResult LOGIN(LoginModel model)
        {
            Authenticate useCase = new Authenticate(new Authenticatecommande(model.Email, model.Password));
            var user = useCase.Execute();

            if (user == null)
            {

                model.IsError = true;
                model.Message = "Email or password is invalid";

                return View(model);
            }
            if(user.Status == false)
            {
                model.IsError = true;
                model.Message = "this account has be been disabled !";
                return View(model);
            }

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
                (
                1,
                user.Email,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                false,
                JsonConvert.SerializeObject
                (
                    new RegisterModel
                    (
                        user.Email,
                        user.Password,
                        user.Password,
                        user.Name
                    )
                ),
                FormsAuthentication.FormsCookiePath

                );
            Response.Cookies.Add(
                new HttpCookie(
                    FormsAuthentication.FormsCookieName,
                    FormsAuthentication.Encrypt(ticket)
                    )
                );


            if (!string.IsNullOrEmpty(model.ReturnUrl))
                

                return Redirect(model.ReturnUrl);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session[nameof(User)] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult REGISTER()
        {
            return View();
        }
        [HttpPost]
        public ActionResult REGISTER(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Register useCase = new Register
                    (new Registercommande(
                        model.Email,
                        model.Password,
                        model.Nom,
                         true,
                        Services.entities.User.Roleoptions.Customer,
                       
                        DateTime.Now
                        
                        ));
                var User = useCase.Execute();

                if (User == null)
                {

                    model.IsError = true;
                    model.Message = "an error occured please try again later!";

                    return View(model);
                }
               

                return RedirectToAction("LOGIN");
            }
            else
            {

                return View(model);
            }
        }

        /* [NonAction]
         public void SenverificationLinkEmail(string emailID , string activationCode, String emailFor = "verifyAccount")
         {
             var verifyUrl = "/User/" + emailFor + "/" + activationCode;
             var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
             var fromEmail = new MailAddress  ("dotnetawesome@gmail.com", "Dotnet Awesome");
             var toEmail = new MailAddress(emailID);
             var fromEmailpassword = "********";// replace with actual password

             string subject = "";
             string body = "";
             if (emailFor == "verifyAccount")
             {
                 subject = "your account is successfully created";
                 body = V + link + "" > " + link + " </ a > " ;

             }
             else if(emailFor="ResetPassword")
             {

                 subject = "reset pssword";
                 body = "Hi,<br/><br/> we go request for reset your account password please click on" +
                     "the below link to reset your password"+
                     "<br/><br/><a href="+ link+">reset password link <a/>";
             }
             var smtp = new SmtClient
             {
                 Host = "smtp.gmail.com",
                 Port = 587,
                 Enabless1 = true,
                 DeliveryMethod = SmtpDeliveryMethod.Network,
                 UseDefaultCredentials = false,
                 Credentials = new NetworkCredential(fromEmail.Address, fromEmailpassword)
             };
             using (var message = new MailMessage(fromEmail, toEmail) {

                 subject = subject,
                 body = body,
                 IsBodyHtml = true
             })
                 smtp.Send(message);
         }
         */
        public ActionResult FORGOT_PASSWORD()
        {
            return View();
        }
            [HttpPost]
        public ActionResult FORGOT_PASSWORD(ForgotModel model)
        {
            /*//verify Email
            //generate reset password link
            //send email
            string message = " ";
            bool Status = false;*/
           
            string MailCon = ConfigurationManager.ConnectionStrings["SqlServer"].ConnectionString;
            SqlConnection Sqlcon = new SqlConnection(MailCon);
            string Sqlquery = "select Email, Password from [dbo].[User] where Email = @Email";
            SqlCommand Sqlcommand = new SqlCommand(Sqlquery, Sqlcon);
           Sqlcommand.Parameters.AddWithValue("@Email", SqlNotificationType.Subscribe);
            Sqlcon.Open();
            SqlDataReader sdr = Sqlcommand.ExecuteReader();
            if (sdr.Read())
            {
                string username = sdr["Email"].ToString();
                string password = sdr["Password"].ToString();

                MailMessage mm = new MailMessage("guylainboss2@gmail.com"," chimene.com237");
                mm.Subject = "your password";
                mm.Body = string.Format("hello: <h1>{0}</h1> is your email id <br/> your password is <h1>{1}</h1>", username, password);
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                NetworkCredential nc = new NetworkCredential();
                nc.UserName = "guylainboss2@gmail.com";
                nc.Password = "chimene.com237";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = nc;
                smtp.Port = 587;
                smtp.Send(mm);
               // string msg = " ";

                
              
                
            }



            /*
            using (Authenticate dc = new Authenticate()) 
            {
                var account = dc.User.where(XmlSiteMapProvider => XmlSiteMapProvider.Email == Email).FirstOrDefaut();

                if (account != null)
                {
                    // send email for reset password
                    string resetcode = Guid.NewGuid().ToString();
                    sendVerificationLinkEmail(account.Email, resetcode,"resetpassword");
                    account.ResetPasswordCode = resetcode;
                    // this line i have added here to avoid confirm password not match
                    //issu, as we had added a confirm password property
                    //in cur model class in part 
                    dc.Configuration.ValidateOnSaveEnabled = false;

                    dc.SaveChanges();
                    message="reset password link has been send to your email id"

                }
                else
                {
                    message = " Account not found";
                }
            }*/
            return View();
        }

       /* public ActionResult ResetPassword( string id)
        {
            //verify the reset password link 
            //find account associated with this link 
            //redirect to reset password page
            using (MyDatasetEntity dc = new MyDatasetEntity())
            {
                var user = dc.User.where(x => x.ResetPassworCode == id ).FirstOrDefault();
                if(user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);

                }
                else
                {
                    return HttpNotFound();
                }
            }
        }*/

       /* [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = " ";
            if (ModelState.IsValid)
            {
                using (MyDatasetEntity dc = new MyDatasetEntity())
                {
                    var user = dc.User.where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        user.Password = Crypto.Hash(model.NewPassword);
                        user.ResetPasswordCode = " ";
                        dc.Configuration.ValidateOnSaveEnabled = false;
                        dc.SaveChange();
                        message = "new password update successfully";
                    }
                    else
                    {
                        message = " something invalid";

                    }
                    ViewBag.Message = message;
                    return View(model);
                }
            }
        }*/
        public ActionResult TERMS()
        {
           return View();
        }


    }
} 