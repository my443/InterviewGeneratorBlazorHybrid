using InterviewGeneratorBlazorHybrid.Models;

namespace InterviewGeneratorBlazorHybrid.Components.Pages
{
    public partial class InterviewList
    {
        protected override void OnInitialized()
        {
            if (Preferences.Get("DatabaseFilePath", "Not Set") == "Not Set")
            {
                Navigation.NavigateTo($"/settings");
            }
        }
        private void AddNewInterview()
        {
            ViewModel.IsAddMode = true;
            ViewModel.IsEditMode = true;
            ViewModel.AddNewInterview();
            //Navigation.NavigateTo($"/interview/{ViewModel.Interview.Id}");
            //StateHasChanged();
        }

        private async Task DeleteInterviewWithConfirm(Interview interview)
        {
            ViewModel.DeleteInterview(interview.Id);
        }
    }
}
