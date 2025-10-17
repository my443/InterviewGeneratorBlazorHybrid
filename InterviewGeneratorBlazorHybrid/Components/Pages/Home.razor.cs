
namespace InterviewGeneratorBlazorHybrid.Components.Pages
{
    public partial class Home
    {
        private void AddNewInterview()
        {
            ViewModel.AddNewInterview();
            Navigation.NavigateTo($"/interview/{ViewModel.Interview.Id}");
        }
    }
}
