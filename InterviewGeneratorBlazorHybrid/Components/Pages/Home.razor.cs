using InterviewGeneratorBlazorHybrid.Data;

namespace InterviewGeneratorBlazorHybrid.Components.Pages
{
    public partial class Home
    {
        protected override void OnInitialized() {
            ViewModel.IsDatabaseAvailable();

            if (!ViewModel.DatabaseIsAvailable)
                Navigation.NavigateTo("/database");
        }
        private void AddNewInterview()
        {
            ViewModel.AddNewInterview();
            Navigation.NavigateTo($"/interview/{ViewModel.Interview.Id}");
        }
    }
}
