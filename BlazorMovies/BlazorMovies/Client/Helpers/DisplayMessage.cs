using Microsoft.JSInterop;

namespace BlazorMovies.Client.Helpers
{
    public class DisplayMessage : IDisplayMessage
    {
        //use js runtime to talk to 3rd party js library sweetalert2
        private readonly IJSRuntime js;

        public DisplayMessage(IJSRuntime js)
        {
            this.js = js;
        }

        public async ValueTask DisplayErrorMessage(string message)
        {
            await DoDisplayMessage("Error", message, "error");
        }

        public async ValueTask DisplaySuccessMessage(string message)
        {
            await DoDisplayMessage("Success", message, "success");
        }

        private async ValueTask DoDisplayMessage(string title, string message, string messageType)
        {
            //Swal.fire(title, message, type) is a sweetalert2 library method
            await js.InvokeVoidAsync("Swal.fire", title, message, messageType);
        }
    }
}
