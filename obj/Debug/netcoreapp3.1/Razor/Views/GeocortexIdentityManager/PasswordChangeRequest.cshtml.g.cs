#pragma checksum "C:\Users\alkarr\Source\Repos\UserLogOnSecurityTools\Views\GeocortexIdentityManager\PasswordChangeRequest.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5474638ce1d07a6fc2a1285c8d49c38d44b87a8e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_GeocortexIdentityManager_PasswordChangeRequest), @"mvc.1.0.view", @"/Views/GeocortexIdentityManager/PasswordChangeRequest.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\alkarr\Source\Repos\UserLogOnSecurityTools\Views\_ViewImports.cshtml"
using UserLogOnSecurityTools;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\alkarr\Source\Repos\UserLogOnSecurityTools\Views\_ViewImports.cshtml"
using UserLogOnSecurityTools.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5474638ce1d07a6fc2a1285c8d49c38d44b87a8e", @"/Views/GeocortexIdentityManager/PasswordChangeRequest.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5f0f7a9a6f2da399746d5d41af12d61ce22f1ae9", @"/Views/_ViewImports.cshtml")]
    public class Views_GeocortexIdentityManager_PasswordChangeRequest : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UserLogOnSecurityTools.Models.GeocortexIdentityManagerViewModels.PasswordChangeRequestViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<p class=\"pageHeader\">Change Password</p>\r\n<p>You will receive an e-mail with a link to change your password at the email address below. Please click \'Send\' below to receive the e-mail.</p>\r\n\r\n");
#nullable restore
#line 6 "C:\Users\alkarr\Source\Repos\UserLogOnSecurityTools\Views\GeocortexIdentityManager\PasswordChangeRequest.cshtml"
 using (Html.BeginForm("PasswordChangeRequest", "GeocortexIdentityManager", FormMethod.Post))
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\alkarr\Source\Repos\UserLogOnSecurityTools\Views\GeocortexIdentityManager\PasswordChangeRequest.cshtml"
Write(Html.EditorForModel("ChangeRequest"));

#line default
#line hidden
#nullable disable
            WriteLiteral("    <input type=\"submit\" value=\"Send\" />\r\n");
#nullable restore
#line 10 "C:\Users\alkarr\Source\Repos\UserLogOnSecurityTools\Views\GeocortexIdentityManager\PasswordChangeRequest.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<UserLogOnSecurityTools.Models.GeocortexIdentityManagerViewModels.PasswordChangeRequestViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
