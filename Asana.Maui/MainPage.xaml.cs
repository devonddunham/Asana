using Asana.Maui.ViewModels;

namespace Asana.Maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        private void AddNewClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ToDoDetails");
        }

        private void EditClicked(object sender, EventArgs e)
        {
            var selected = (BindingContext as MainPageViewModel)?.SelectedToDoId ?? 0;
            Shell.Current.GoToAsync($"//ToDoDetails?todoId={selected}");
        }

        private void DeleteClicked(object sender, EventArgs e)
        {
            (BindingContext as MainPageViewModel)?.DeleteToDo();
        }

        private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            (BindingContext as MainPageViewModel)?.RefreshPage();
        }

        private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
        {

        }

        private void InLineDeleteClicked(object sender, EventArgs e)
        {
            (BindingContext as MainPageViewModel)?.RefreshPage();
        }
        
        private void ProjectClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync($"//ProjectPage");
        }
    }

}
