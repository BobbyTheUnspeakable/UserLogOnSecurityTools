#pragma checksum "C:\Users\alkarr\Source\Repos\UserLogOnSecurityTools\Views\GeocortexIdentityManager\PasswordReset.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d93743404b0838f6167ae2ecf88a59db159ebc6b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_GeocortexIdentityManager_PasswordReset), @"mvc.1.0.view", @"/Views/GeocortexIdentityManager/PasswordReset.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d93743404b0838f6167ae2ecf88a59db159ebc6b", @"/Views/GeocortexIdentityManager/PasswordReset.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5f0f7a9a6f2da399746d5d41af12d61ce22f1ae9", @"/Views/_ViewImports.cshtml")]
    public class Views_GeocortexIdentityManager_PasswordReset : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UserLogOnSecurityTools.Models.GeocortexIdentityManagerViewModels.PasswordResetViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<h4 class=\"pageHeader\">Change Password</h4>\r\n\r\n");
#nullable restore
#line 5 "C:\Users\alkarr\Source\Repos\UserLogOnSecurityTools\Views\GeocortexIdentityManager\PasswordReset.cshtml"
 using (Html.BeginForm("PasswordReset", "GeocortexIdentityManager", FormMethod.Post))
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\alkarr\Source\Repos\UserLogOnSecurityTools\Views\GeocortexIdentityManager\PasswordReset.cshtml"
Write(Html.EditorForModel("Reset"));

#line default
#line hidden
#nullable disable
            WriteLiteral("    <input type=\"submit\" value=\"Submit\" />\r\n");
#nullable restore
#line 9 "C:\Users\alkarr\Source\Repos\UserLogOnSecurityTools\Views\GeocortexIdentityManager\PasswordReset.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<UserLogOnSecurityTools.Models.GeocortexIdentityManagerViewModels.PasswordResetViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
