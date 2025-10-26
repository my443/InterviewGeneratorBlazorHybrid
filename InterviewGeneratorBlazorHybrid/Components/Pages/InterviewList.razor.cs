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
            ViewModel.OnChange += OnViewModelChanged;
        }
        private void AddNewInterview()
        {
            ViewModel.IsAddMode = true;
            ViewModel.IsEditMode = true;
            ViewModel.AddNewInterview();
        }

        private void EditInterview(Interview interview)
        {
            ViewModel.IsAddMode = false;
            ViewModel.IsEditMode = true;
            ViewModel.LoadInterviewById(interview.Id);
        }

        private async Task DeleteInterviewWithConfirm(Interview interview)
        {
            ViewModel.DeleteInterview(interview.Id);
        }

        private void OnViewModelChanged() => InvokeAsync(StateHasChanged);

        public void Dispose()
        {
            ViewModel.OnChange -= OnViewModelChanged;
        }
    }
}
