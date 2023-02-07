using Microsoft.AspNetCore.Components;

namespace BlazorFirstApp.Pages
{
    public class TimerComponent : ComponentBase
    {
        [Parameter]
        public double TimeInSeconds { get; set; }

        [Parameter]
        public EventCallback Tick { get; set; }

        protected override void OnInitialized()
        {
            var timer = new Timer(
                callback: (_) => InvokeAsync(() => Tick.InvokeAsync(null)),
                state: null,
                dueTime: TimeSpan.FromSeconds(TimeInSeconds),
                period: Timeout.InfiniteTimeSpan
            );

        }
    }
}
