﻿using Microsoft.JSInterop;

namespace PizzaPlace.Client.Services
{
    // encapsulating the JSRuntime code in a service
    public class LocalStorage : ILocalStorage
    {
        private readonly IJSRuntime jsRuntime;

        public LocalStorage(IJSRuntime jsRuntime) => this.jsRuntime = jsRuntime;

        public async Task<T> GetProperty<T>(string propName) => await this.jsRuntime.InvokeAsync<T>("blazorLocalStorage.get", propName);

        public async Task<object> SetProperty<T>(string propName, T value) => await this.jsRuntime.InvokeAsync<object>("blazorLocalStorage.set", propName, value);

        public async Task<object> WatchAsync<T>(T instance) where T : class => 
            await this.jsRuntime.InvokeAsync<object>("blazorLocalStorage.watch", DotNetObjectReference.Create<T>(instance));
    }
}
