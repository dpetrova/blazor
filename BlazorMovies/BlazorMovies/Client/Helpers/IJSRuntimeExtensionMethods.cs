using BlazorMovies.Shared.Entities;
using Microsoft.JSInterop;

namespace BlazorMovies.Client.Helpers
{
    // create reusable extension methods for IJSRuntime service
    public static class IJSRuntimeExtensionMethods
    {
        // ValueTask provides an awaitable result from asynchronous operation
        public static async ValueTask<bool> Confirm(this IJSRuntime js, string message)
        {
            return await js.InvokeAsync<bool>("confirm", message);
        }

        public static void Log(this IJSRuntime js, string message)
        {
            js.InvokeVoidAsync("console.log", message);
        }

        public static async ValueTask LogFromCustomJsFunction(this IJSRuntime js, string message)
        {
            // invoke custom created js function in wwwroot/js/Utilities.js
            await js.InvokeVoidAsync("myCustomLog", message);
        }
    }
}
