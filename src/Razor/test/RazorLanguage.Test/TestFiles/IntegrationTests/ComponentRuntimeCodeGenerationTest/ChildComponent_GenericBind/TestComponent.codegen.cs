// <auto-generated/>
#pragma warning disable 1591
namespace Test
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    public class TestComponent : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.RenderTree.RenderTreeBuilder builder)
        {
            builder.OpenComponent<Test.MyComponent<string>>(0);
            builder.AddAttribute(1, "Item", Microsoft.AspNetCore.Components.RuntimeHelpers.TypeCheck<string>(Microsoft.AspNetCore.Components.BindMethods.GetValue(
#nullable restore
#line 1 "x:\dir\subdir\Test\TestComponent.cshtml"
                                    Value

#line default
#line hidden
#nullable disable
            )));
            builder.AddAttribute(2, "ItemChanged", new System.Action<string>(__value => Value = __value));
            builder.CloseComponent();
        }
        #pragma warning restore 1998
#nullable restore
#line 2 "x:\dir\subdir\Test\TestComponent.cshtml"
       
    string Value;

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
