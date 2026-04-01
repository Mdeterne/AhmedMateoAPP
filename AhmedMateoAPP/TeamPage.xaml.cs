using AhmedMateoAPP.ViewModels;

namespace AhmedMateoAPP;

public partial class TeamPage : ContentPage
{
    public TeamPage(TeamViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
