using Microsoft.AspNetCore.Components;
using System;

namespace BlazorMovies.Client.Pages
{
    public partial class TryPartialClass
    {
        private int currentCount = 0;        

        //dependency injection of configured (in Program.cs) services
        [Inject]
        SingletonService singletonService { get; set; }

        [Inject]
        TransientService transientService { get; set; }

        private void IncrementCount()
        {
            currentCount++;
            singletonService.Value = currentCount;
            transientService.Value = currentCount;
        }
    }
}
