#pragma checksum "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "afe67460bbf80ae29e51c18cfcdba3db147093b5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Propuestas_Details), @"mvc.1.0.view", @"/Views/Propuestas/Details.cshtml")]
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
#line 1 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\_ViewImports.cshtml"
using FrontEnd;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\_ViewImports.cshtml"
using FrontEnd.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"afe67460bbf80ae29e51c18cfcdba3db147093b5", @"/Views/Propuestas/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3ea719869d0121793e2abdd0e78c4bddb249e5ed", @"/Views/_ViewImports.cshtml")]
    public class Views_Propuestas_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<FrontEnd.Models.Propuesta>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/img/Business.jpg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("opacity: .8; width:100%"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
  
    ViewData["Title"] = "Detalles de Propuesta";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Detalles Propuesta</h1>\r\n<hr />\r\n\r\n<!-- Main content -->\r\n<section class=\"content\">\r\n    <div class=\"container-index\">\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "afe67460bbf80ae29e51c18cfcdba3db147093b54966", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
        <div class=""container-fluid2"">
            <!-- SECTION TITLE  -->
            <div class=""card col-md-6"">
                <br />
                <div class=""card-body p-0"" style=""overflow-x:auto;"">
                    <table class=""table table-striped projects"">
                        <tbody>
                            <tr>
                                <th>
                                    ");
#nullable restore
#line 23 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                               Write(Html.DisplayNameFor(model => model.Titulo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 26 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                               Write(Html.DisplayFor(model => model.Titulo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 31 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                               Write(Html.DisplayNameFor(model => model.Descripcion));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 34 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                               Write(Html.DisplayFor(model => model.Descripcion));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 39 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                               Write(Html.DisplayNameFor(model => model.Pendiente));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 42 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                               Write(Html.DisplayFor(model => model.Pendiente));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 47 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                               Write(Html.DisplayNameFor(model => model.Tipo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 50 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                               Write(Html.DisplayFor(model => model.Tipo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 55 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                               Write(Html.DisplayNameFor(model => model.FechaPublicacion));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 58 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                               Write(Html.DisplayFor(model => model.FechaPublicacion));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 63 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                               Write(Html.DisplayNameFor(model => model.FechaFinalizacion));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 66 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                               Write(Html.DisplayFor(model => model.FechaFinalizacion));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 71 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                               Write(Html.DisplayNameFor(model => model.Problema));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 74 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                               Write(Html.DisplayFor(model => model.Problema));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 79 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                               Write(Html.DisplayNameFor(model => model.ResultadoEsperado));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 82 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                               Write(Html.DisplayFor(model => model.ResultadoEsperado));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 87 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                               Write(Html.DisplayNameFor(model => model.Riesgos));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 90 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                               Write(Html.DisplayFor(model => model.Riesgos));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 95 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                               Write(Html.DisplayNameFor(model => model.Beneficios));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 98 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                               Write(Html.DisplayFor(model => model.Beneficios));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 103 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                               Write(Html.DisplayNameFor(model => model.Usuario));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 106 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                               Write(Html.DisplayFor(model => model.Usuario.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <br />
            </div>

            <!-- /.card -->
        </div>
        <!-- /.container-fluid -->
    </div>
</section>
<!-- /.content -->
<br />


<section class=""content"">
    <div class=""container-fluid"">
        <div>
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "afe67460bbf80ae29e51c18cfcdba3db147093b516301", async() => {
                WriteLiteral("Editar Propuesta");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 127 "C:\Users\User\Documents\PAW-ProyectoU\FrontEnd\Views\Propuestas\Details.cshtml"
                                   WriteLiteral(Model.PropuestaId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" |\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "afe67460bbf80ae29e51c18cfcdba3db147093b518481", async() => {
                WriteLiteral("Volver a Propuestas");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n        </div>\r\n        <div>\r\n        </div>\r\n    </div>\r\n</section>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FrontEnd.Models.Propuesta> Html { get; private set; }
    }
}
#pragma warning restore 1591
