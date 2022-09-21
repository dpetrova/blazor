function myCustomLog(message) {
    console.log(message)
}

// invoke static C# method
function dotnetStaticInvokation() {    
    // params: 1-> name of the project that host the method that will be invoked; 2-> name of the static method
    DotNet.invokeMethodAsync("BlazorMovies.Client", "GetStaticCounter").then(result => {
        console.log(`counter of invoked C# static method into JS function is: ${result}`)
        return result
    })
}

// invoke instance C# method (param dotnetHelper is a reference to a C# object, so to know method of which class to invoke)
function dotnetInstanceInvocation(dotnetHelper) {    
    // params: 1-> name of the static method
    dotnetHelper.invokeMethodAsync("GetInstanceCounter")
}