//partial class with code of Dismissablealert component

using Microsoft.AspNetCore.Components;
using System.Diagnostics.Metrics;

namespace BlazorFirstApp.Pages
{
    public partial class DismissableAlert
    {
        private bool show;

        [Parameter]
        public bool Show
        {
            get => this.show;
            set
            {
                if (value != this.show)
                {
                    this.show = value;
                    //when changes the Show property’s value, the property’s setter triggers the ShowChanged event callback
                    ShowChanged.InvokeAsync(this.show);
                }
            }
        }

        [Parameter]
        public EventCallback<bool> ShowChanged { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public void Dismiss() => Show = false;
    }
}
