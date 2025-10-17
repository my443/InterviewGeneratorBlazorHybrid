using InterviewGeneratorBlazorHybrid.Models;

namespace InterviewGeneratorBlazorHybrid.Components.Pages
{
    public partial class InterviewList
    {
        private void AddNewInterview()
        {
            ViewModel.AddNewInterview();
            Navigation.NavigateTo($"/interview/{ViewModel.Interview.Id}");
        }

        private async Task DeleteInterviewWithConfirm(Interview interview)
        {
            ViewModel.DeleteInterview(interview.Id);
        }
    }
}
