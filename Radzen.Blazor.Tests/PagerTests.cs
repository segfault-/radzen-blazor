using Bunit;
using Bunit.JSInterop;
using System;
using System.Collections.Generic;
using Xunit;

namespace Radzen.Blazor.Tests
{
    public class PagerTests
    {
        [Fact]
        public void RadzenPager_AutoHide_If_Count_Is_Less_Than_PageSize()
        {
            using var ctx = new TestContext();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;
            ctx.JSInterop.SetupModule("_content/Radzen.Blazor/Radzen.Blazor.js");

            var component = ctx.RenderComponent<RadzenPager>(parameters =>
            {
                parameters.Add<int>(p => p.PageSize, 20);
                parameters.Add<int>(p => p.Count, 100);
            });

            component.Render();

            Assert.Contains(@$"rz-paginator", component.Markup);

            component.SetParametersAndRender(parameters =>
            {
                parameters.Add<int>(p => p.PageSize, 101);
                parameters.Add<int>(p => p.Count, 100);
            });
            Assert.DoesNotContain(@$"rz-paginator", component.Markup);
        }

        [Fact]
        public void RadzenPager_Dont_AutoHide_If_PageSizeOptions_Specified()
        {
            using var ctx = new TestContext();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;
            ctx.JSInterop.SetupModule("_content/Radzen.Blazor/Radzen.Blazor.js");

            var component = ctx.RenderComponent<RadzenPager>(parameters =>
            {
                parameters.Add<int>(p => p.PageSize, 101);
                parameters.Add<int>(p => p.Count, 100);
                parameters.Add<IEnumerable<int>>(p => p.PageSizeOptions, new int[] { 3, 7, 15 });
            });

            component.Render();

            Assert.Contains(@$"rz-paginator", component.Markup);
            Assert.Contains(@$"rz-dropdown-trigger", component.Markup);
        }

    }
}
