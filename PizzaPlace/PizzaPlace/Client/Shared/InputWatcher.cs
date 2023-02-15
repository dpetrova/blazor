using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace PizzaPlace.Client.Shared
{
    public class InputWatcher : ComponentBase
    {
        private EditContext editContext;

        // EditForm provides a cascading value of type EditContext, and the InputText components use this EditContext for things like validation
        // Whenever one of the Input components change, it calls the EditContext.NotifyFieldChanged method
        // EditContext has an OnFieldChanged event, which triggers every time a model’s property changes
        [CascadingParameter]
        public EditContext EditContext
        {
            get => this.editContext;
            // when the EditContext property gets set, the InputWatcher simply registers for the OnFieldChanged event and calls its own FieldChanged event callback
            set
            {
                this.editContext = value;
                // subscribing to the EditContext’s OnFieldChanged event
                EditContext.OnFieldChanged += async (sender, e) =>
                {
                    await FieldChanged.InvokeAsync(e.FieldIdentifier.FieldName);
                };
            }
        }

        [Parameter]
        public EventCallback<string> FieldChanged { get; set; }

        public bool Validate() => EditContext?.Validate() ?? false;
    }
}
