using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserLogOnSecurityTools.Data;
using UserLogOnSecurityTools.Models;
using UserLogOnSecurityTools.Models.GeocortexIdentityManagerViewModels;

using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace UserLogOnSecurityTools.Controllers
{
    public class GeocortexIdentityManagerController : Controller
    {

        private ApplicationDbContext _context;
        public GeocortexIdentityManagerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();

        #region Test
        public IActionResult Test1()
        {
            return Test2();
        }

        private IActionResult Test2()
        {
            return Redirect("https://skyview.hornershifrin.com");
        }
        #endregion

        #region Create User

        //public IActionResult CreateUser(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext db, SignInManager<User> s)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //        app.UseBrowserLink();

        //        if (s.UserManager.FindByNameAsync("dev").Result == null)
        //        {
        //            var result = s.UserManager.CreateAsync(new User
        //            {
        //                UserName = "dev",
        //                Email = "dev@app.com"
        //            }, "Aut94L#G-a").Result;
        //        }
        //    }

        //    app.UseAuthentication();
        //    app.UseStaticFiles();
        //    app.UseMvc(routes =>
        //    {
        //        routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
        //        routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });
        //    });

        //    return Redirect("https://skyview.hornershifrin.com/utils/messages/OK.html");
        //}

        #endregion

        #region Password Reset
        [HttpGet]
        public IActionResult PasswordResetRequest()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PasswordResetRequest(PasswordResetRequestViewModel model)
        {
            var user = _context.GeocortexUsers.SingleOrDefault(x => x.UserName == model.UserName);
            if (user != null)
            {
                await PasswordResetEmail(user.UserId);
                return View("Messages", new Messages
                {
                    Title = "Success.",
                    Message = "An email has been sent to your account. Please check your inbox for your security code."
                });
            }
            return View("Messages", new Messages
            {
                Title = "Error.",
                Message = "The e-mail address you entered is not associated with the Skyview GIS system. Please click the back button on your browser to try again, or contact Horner & Shifrin IT staff if you believe this is an error."
            });

        }

        [HttpGet]
        public IActionResult PasswordReset(Guid id)
        {
            var action = _context.UserActions.Single(x => x.ActionId == id);
            var userId = action.UserId;
            var user = _context.GeocortexUsers.Include(x => x.Membership).SingleOrDefault(x => x.UserId == userId);
            var model = new PasswordResetViewModel
            {
                User = user,
                ActionId = action.ActionId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PasswordReset(PasswordResetViewModel model)
        {
            var userToUpdate = _context.GeocortexUsers.Include(x => x.Membership).FirstOrDefault(x => x.UserId == model.User.UserId);
            if (userToUpdate != null)
            {

                var newPasswordHash = EncodePassword(model.NewPassword, userToUpdate.Membership.PasswordFormat, userToUpdate.Membership.PasswordSalt);
                var verifySecurityCode = _context.UserActions.Single(x => x.ActionId == model.ActionId).SecurityCode;

                if (model.SecurityCode == verifySecurityCode)
                {
                    userToUpdate.Membership.Password = newPasswordHash;
                    _context.GeocortexUsers.Update(userToUpdate);
                    _context.SaveChanges();

                    await PasswordResetConfirmationEmail(model.User.UserId);

                    return View("Messages", new Messages
                    {
                        Title = "Success.",
                        Message = "Your account has been successfully updated."
                    });
                }

            }
            return View("Messages", new Messages
            {
                Title = "Error.",
                Message = "Something went wrong."
            });
        }

        private async Task<IActionResult> PasswordResetConfirmationEmail(Guid id)
        {
            // Insert action
            var user = _context.GeocortexUsers.Include(x => x.Membership).FirstOrDefault(x => x.UserId == id);
            if (user != null)
            {

                var subject = "Skyview GIS Password Confirmation";
                var message = "<html><span style=\"font-family: arial;\">This email is confirming that your password has recently been updated/changed. Please contact Horner & Shifrin IT staff if you did not make this change.</span></html>";
                await SendEmailAsync(user.UserName, subject, message);
                return Json(new { status = "sent" });

            }
            else return Json(new { status = "error" });

        }

        private async Task<IActionResult> PasswordResetEmail(Guid id)
        {
            // Insert action
            var user = _context.GeocortexUsers.Include(x => x.Membership).FirstOrDefault(x => x.UserId == id);
            if (user != null)
            {
                var actionId = Guid.NewGuid();
                var securityCode = GenerateSecurityCode();
                var changeRequestAction = new UserAction
                {
                    ActionId = actionId,
                    UserId = user.UserId,
                    ActionType = "Password reset request",
                    ActionDate = DateTime.Now,
                    SecurityCode = securityCode
                };
                _context.UserActions.Add(changeRequestAction);
                _context.SaveChanges();

                var subject = "Reset Your Skyview GIS Password";
                var message = "<html><span style=\"font-family: arial; font-weight: bold;\"><a href=\"https://skyview.hornershifrin.com/UserTools/GeocortexIdentityManager/PasswordReset/" + actionId + "\">Click here to reset password.</a></span><br/><br/><span style=\"font-family: arial; font-weight: bold;\">Your security code is: " + securityCode + "</span></html>";
                await SendEmailAsync(user.UserName, subject, message);
                return Json(new { status = "sent" });

            }
            else return Json(new { status = "error" });

        }
        #endregion

        #region Password Change
        [HttpGet]
        public IActionResult PasswordChangeRequest(Guid id)
        {

            var model = new PasswordChangeRequestViewModel
            {
                UserId = id,
                UserName = _context.GeocortexUsers.SingleOrDefault(x => x.UserId == id)?.UserName
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PasswordChangeRequest(PasswordChangeRequestViewModel model)
        {

            var user = _context.GeocortexUsers.SingleOrDefault(x => x.UserName == model.UserName);
            if (user != null)
            {
                await PasswordChangeEmail(user.UserId);
            }
            return View("Messages", new Messages
            {
                Title = "Success.",
                Message = "An email has been sent to your account. Please check your inbox for your security code."
            });
        }

        [HttpGet]
        public IActionResult PasswordChange(Guid id, bool incorrectPassword = false)
        {
            var action = _context.UserActions.Single(x => x.ActionId == id);
            var userId = action.UserId;
            var user = _context.GeocortexUsers.Include(x => x.Membership).SingleOrDefault(x => x.UserId == userId);
            var model = new PasswordChangeViewModel
            {
                User = user,
                ActionId = id,
                IncorrectPassword = incorrectPassword
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel model)
        {
            var userToUpdate = _context.GeocortexUsers.Include(x => x.Membership).FirstOrDefault(x => x.UserId == model.User.UserId);
            if (userToUpdate != null)
            {

                var dBPasswordHash = userToUpdate.Membership.Password;
                var currentPasswordHash = EncodePassword(model.CurrentPassword, userToUpdate.Membership.PasswordFormat, userToUpdate.Membership.PasswordSalt);
                var newPasswordHash = EncodePassword(model.NewPassword, userToUpdate.Membership.PasswordFormat, userToUpdate.Membership.PasswordSalt);

                if (dBPasswordHash == currentPasswordHash)
                {
                    userToUpdate.Membership.Password = newPasswordHash;
                    _context.GeocortexUsers.Update(userToUpdate);
                    _context.SaveChanges();
                    await PasswordResetConfirmationEmail(model.User.UserId);
                    return View("Messages", new Messages
                    {
                        Title = "Success.",
                        Message = "Your account was successfully updated."
                    });
                }
                return PasswordChange(model.ActionId, true);
            }
            return View("Messages", new Messages
            {
                Title = "Error.",
                Message = "Something went wrong."
            });
        }

        private async Task<IActionResult> PasswordChangeEmail(Guid id)
        {
            // Insert action
            var user = _context.GeocortexUsers.Include(x => x.Membership).FirstOrDefault(x => x.UserId == id);
            if (user != null)
            {
                var actionId = Guid.NewGuid();
                var changeRequestAction = new UserAction
                {
                    ActionId = actionId,
                    UserId = user.UserId,
                    ActionType = "Password change request",
                    ActionDate = DateTime.Now
                };
                _context.UserActions.Add(changeRequestAction);
                _context.SaveChanges();

                var subject = "Change Your Skyview GIS Password";
                var message = "<html><span style=\"font-family: arial; font-weight: bold;\"><a href=\"https://skyview.hornershifrin.com/UserTools/GeocortexIdentityManager/PasswordChange/" + actionId + "\">Click here to change password.</a></span></html>";
                await SendEmailAsync(user.UserName, subject, message);
                return Json(new { status = "sent" });

            }
            else return Json(new { status = "error" });

        }
        #endregion

        #region Password Set
        [HttpGet]
        public IActionResult PasswordSet(Guid id, string clientFolder)
        {
            var action = _context.UserActions.SingleOrDefault(x => x.ActionId == id);
            if (action != null)
            {
                var user = _context.GeocortexUsers.SingleOrDefault(x => x.UserId == action.UserId);
                var model = new PasswordSetViewModel
                {
                    ActionId = id,
                    User = user, 
                    UserId = action.UserId,
                    ClientFolder = clientFolder
                };
                return View(model);
            }
            return View("Messages", new Messages
            {
                Title = "Error.",
                Message = "Something went wrong."
            });
        }

        [HttpPost]
        public async Task<IActionResult> PasswordSet(PasswordSetViewModel model)
        {
            var userToUpdate = _context.GeocortexUsers.Include(x => x.Membership).FirstOrDefault(x => x.UserId == model.UserId);
            if (userToUpdate != null)
            {

                var dBPasswordHash = userToUpdate.Membership.Password;
                var currentPasswordHash = EncodePassword("GISMapping6834", userToUpdate.Membership.PasswordFormat, userToUpdate.Membership.PasswordSalt);
                var newPasswordHash = EncodePassword(model.NewPassword, userToUpdate.Membership.PasswordFormat, userToUpdate.Membership.PasswordSalt);

                if (dBPasswordHash == currentPasswordHash)
                {
                    userToUpdate.Membership.Password = newPasswordHash;
                    _context.GeocortexUsers.Update(userToUpdate);
                    _context.SaveChanges();
                    await PasswordResetConfirmationEmail(model.UserId);
                    return Redirect("https://skyview.hornershifrin.com/" + model.ClientFolder);
                }
                return PasswordSet(model.ActionId, model.ClientFolder);
            }
            return View("Messages", new Messages
            {
                Title = "Error.",
                Message = "Something went wrong."
            });
        }

        #endregion

        #region Multi-factor Authentication

        [HttpGet]
        public IActionResult MultiFactorAuth(string passthrough)
        {
            var model = new MultiFactorAuthViewModel
            {
                Passthrough = passthrough
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MultiFactorAuth(MultiFactorAuthViewModel model)
        {
            var user = _context.GeocortexUsers.SingleOrDefault(x => x.UserName == model.UserName);
            if (user != null)
            {
                await MultiFactorAuthEmail(user.UserId, model.Passthrough);
                return View("Messages", new Messages
                {
                    Title = "Success.",
                    Message = "An email has been sent to your account. Please check your inbox for your security code."
                });
            }
            return View("Messages", new Messages
            {
                Title = "Error.",
                Message = "Something went wrong."
            });
        }

        [HttpGet]
        public IActionResult MultiFactorAuthConfirm(Guid id, string passthrough)
        {
            var action = _context.UserActions.Single(x => x.ActionId == id);
            var userId = action.UserId;
            var user = _context.GeocortexUsers.Include(x => x.Membership).SingleOrDefault(x => x.UserId == userId);
            var model = new MultiFactorAuthConfirmViewModel
            {
                User = user,
                ActionId = action.ActionId,
                Passthrough = passthrough
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult MultiFactorAuthConfirm(MultiFactorAuthConfirmViewModel model)
        {
            var verifySecurityCode = _context.UserActions.Single(x => x.ActionId == model.ActionId).SecurityCode;
            if (verifySecurityCode == model.SecurityCode)
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = new DateTimeOffset(DateTime.Now.AddDays(7)),
                    HttpOnly= true,
                    Secure = true
                };

                HttpContext.Response.Cookies.Append("Skyview_verified", AppGuid(model.Passthrough), cookieOptions);
                return Redirect("https://skyview.hornershifrin.com/HSViewer/Index.html?viewer=" + model.Passthrough + ".HSViewer#");
            }
            return View("Messages", new Messages
            {
                Title = "Error.",
                Message = "Something went wrong."
            });
        }

        private async Task<IActionResult> MultiFactorAuthEmail(Guid id, string passthrough)
        {
            // Insert action
            var user = _context.GeocortexUsers.Include(x => x.Membership).FirstOrDefault(x => x.UserId == id);
            if (user != null)
            {
                var actionId = Guid.NewGuid();
                var securityCode = GenerateSecurityCode();
                var changeRequestAction = new UserAction
                {
                    ActionId = actionId,
                    UserId = user.UserId,
                    ActionType = "Multifactor auth request",
                    ActionDate = DateTime.Now,
                    SecurityCode = securityCode
                };
                _context.UserActions.Add(changeRequestAction);
                _context.SaveChanges();

                var subject = "Verify Your Skyview GIS Username";
                var message = "<html><span style=\"font-family: arial; font-weight: bold;\"><a href=\"https://skyview.hornershifrin.com/UserTools/GeocortexIdentityManager/MultiFactorAuthConfirm/" + actionId + "?passthrough=" + passthrough + "\">Click here to verify your account.</a></span><br/><br/><span style=\"font-family: arial; font-weight: bold;\">Your security code is: " + securityCode + "</span></html>";
                await SendEmailAsync(user.UserName, subject, message);
                return Json(new { status = "sent" });

            }
            else return Json(new { status = "error" });

        }

        #endregion

        #region Users
        public IActionResult Users()
        {
            //using (var context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>()))
            //{
            //    if (ModelState.IsValid)
            //    {
            //        var userNames = _context.GeocortexUsers.Select(u => u.UserName).ToList();
            //        var model = new UsersViewModel
            //        {
            //            Users = _context.GeocortexUsers.Include(x => x.Membership).Select(x => new UsersViewModel.ListItem
            //                {
            //                    UserId = x.UserId.ToString(),
            //                    UserName = x.UserName,
            //                    Email = x.Membership.Email,
            //                    LogOnDate = x.Membership.LastLoginDate.ToString(CultureInfo.InvariantCulture)
            //            })
            //                .ToList()
            //        };
            //        return View(model);
            //    }
            //}
            //return View();
            return NotFound();
        }

        [HttpGet]
        public IActionResult EditUser(Guid id)
        {
            var user = _context.GeocortexUsers.Include(x => x.Membership).SingleOrDefault(x => x.UserId == id);
            return View(new EditUserViewModel
            {
                User = user
            });
            return NotFound();
        }

        [HttpPost]
        public IActionResult EditUser(EditUserViewModel model)
        {
            //var userToUpdate = _context.GeocortexUsers.Include(x => x.Membership).FirstOrDefault(x => x.UserId == model.User.UserId);
            //if (userToUpdate != null)
            //{
            //    userToUpdate.UserName = model.User.UserName;
            //    userToUpdate.Membership.Email = model.User.Membership.Email;

            //    userToUpdate.Membership.Password = EncodePassword(model.NewPassword, userToUpdate.Membership.PasswordFormat, userToUpdate.Membership.PasswordSalt);

            //    _context.GeocortexUsers.Update(userToUpdate);
            //    _context.SaveChanges();
            //}
            //return View(new EditUserViewModel
            //{
            //    User = userToUpdate
            //});
            return NotFound();
        }
        #endregion

        #region Additional Functions
        private string EncodePassword(string pass, int passwordFormat, string salt)
        {
            byte[] numArray;
            byte[] numArray1;
            string base64String;

            if (passwordFormat == 1)
            {
                byte[] bytes = Encoding.Unicode.GetBytes(pass);
                byte[] numArray2 = Convert.FromBase64String(salt);
                byte[] numArray3;

                // Hash password
                HashAlgorithm hashAlgorithm = new HMACSHA256();

                KeyedHashAlgorithm keyedHashAlgorithm = (KeyedHashAlgorithm)hashAlgorithm;

                if (keyedHashAlgorithm.Key.Length != numArray2.Length)
                {

                    if (keyedHashAlgorithm.Key.Length >= numArray2.Length)
                    {
                        numArray = new byte[keyedHashAlgorithm.Key.Length];
                        int num = 0;
                        while (true)
                        {
                            if (!(num < numArray.Length))
                            {
                                break;
                            }
                            int num1 = Math.Min(numArray2.Length, numArray.Length - num);
                            Buffer.BlockCopy(numArray2, 0, numArray, num, num1);
                            num = num + num1;
                        }
                        keyedHashAlgorithm.Key = numArray;
                    }
                    else
                    {
                        numArray = new byte[keyedHashAlgorithm.Key.Length];
                        Buffer.BlockCopy(numArray2, 0, numArray, 0, numArray.Length);
                        keyedHashAlgorithm.Key = numArray;
                    }
                }
                else
                {
                    keyedHashAlgorithm.Key = numArray2;
                }
                numArray3 = keyedHashAlgorithm.ComputeHash(bytes);

                base64String = Convert.ToBase64String(numArray3);
            }
            else
            {
                base64String = pass;
            }

            return base64String;
        }

        private async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Horner & Shifrin", "do-not-reply@hornershifrin.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("html") { Text = message };

            using (var client = new SmtpClient())
            {
                client.LocalDomain = "hornershifrincom.mail.protection.outlook.com";
                await client.ConnectAsync("hornershifrincom.mail.protection.outlook.com").ConfigureAwait(false);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }

        private string GenerateSecurityCode()
        {
            return new Random().Next(1, 1000000).ToString();
        }

        private string AppGuid(string client)
        {
            var clientDict = new Dictionary<string, string>
            {
                ["UMSL"] = "cfd5223a-243d-4e3e-9b62-6acc4b3fb2e6-42f40a3f-f8e0-4c14-84e6-2068840ce31a",
                ["Bayer"] = "87845d06-040e-4bd3-8b11-058741ef6e9c-60655058-f156-49f3-a91d-06b87ad2767f"
            };
            return clientDict[client];
        } 

        #endregion
    }
}