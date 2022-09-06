using Frontend.ViewModels;

namespace Frontend.Views;

public partial class TabView : ContentView
{
	private TabViewModel _context;

	public TabView() {
		InitializeComponent();
		_context = BindingContext as TabViewModel;
	}

	private void SelectionChanged(object sender, SelectionChangedEventArgs e) {
		_context.SelectedTab = _context.Names.IndexOf(e.CurrentSelection[0] as string);
	}
}