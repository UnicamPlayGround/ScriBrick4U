using Frontend.ViewModels;

namespace Frontend.Helpers
{
    public class Mediator
    {
        private static Mediator instance;
        private readonly IDictionary<string, Action<object>> components = new Dictionary<string, Action<object>>();

        private Mediator() { }

        public void Register(string actionKey, Action<object> action)
        {
            if (actionKey == null || actionKey.Length == 0) throw new ArgumentException("Key can't be null or empty.");
            if (action is null) throw new ArgumentNullException("Action to be executed can't be null.");
            if (components.ContainsKey(actionKey) || components.Contains(new(actionKey, action))) throw new Exception("Parameters passed are already in dictionary.");

            components.Add(actionKey, action);
        }

        public void Execute(object sender, string actionKey)
        {
            if (sender is null || actionKey is null) throw new ArgumentNullException("Both parameters can't be null.");

            if (components.ContainsKey(actionKey) && sender is BlockTypeViewModel)
                components[actionKey].Invoke((sender as BlockTypeViewModel).SelectedType);
        }

        public static Mediator GetInstance()
        {
            if (instance == null)
                instance = new();

            return instance;
        }
    }
}
