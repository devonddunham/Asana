using System.Threading.Tasks;
using Asana.Library.Models;
using Asana.Maui.ViewModels;

namespace Asana.Maui.Views;

[QueryProperty(nameof(ToDoId), "todoId")]
public partial class ToDoDetailView : ContentPage
{
    public ToDoDetailView()
    {
        InitializeComponent();
    }

    public int ToDoId { get; set; }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private async void OkClicked(object sender, EventArgs e)
    {
        try
        {
            await (BindingContext as ToDoDetailViewModel)?.AddOrUpdateToDoAsync();
            await Shell.Current.GoToAsync("//MainPage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ToDoDetailViewModel(ToDoId);
    }
}