﻿// <auto-generated/>
#pragma warning disable 1591
namespace Test
{
    #line default
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Components;
    #line default
    #line hidden
    #nullable restore
    public partial class TestComponent : global::Microsoft.AspNetCore.Components.ComponentBase
    #nullable disable
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<global::Test.TestComponent>(0);
            __builder.AddComponentParameter(1, "Value", "Hello");
            __builder.CloseComponent();
            __builder.AddMarkupContent(2, "\r\n\r\n");
            __builder.AddContent(3, 
#nullable restore
#line (3,2)-(3,2) "x:\dir\subdir\Test\TestComponent.cshtml"

#line default
#line hidden
#nullable disable
            );
        }
        #pragma warning restore 1998
#nullable restore
#line (5,8)-(7,1) "x:\dir\subdir\Test\TestComponent.cshtml"

    [Parameter] public int Param { get; set; }

#line default
#line hidden
#nullable disable

    }
}
#pragma warning restore 1591
