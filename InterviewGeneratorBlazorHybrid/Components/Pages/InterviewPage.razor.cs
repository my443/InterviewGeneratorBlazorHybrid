using InterviewGeneratorBlazorHybrid.ViewModels;
using Microsoft.AspNetCore.Components;

namespace InterviewGeneratorBlazorHybrid.Components.Pages
{
    public partial class InterviewPage
    {
        [Parameter]
        public int? InterviewId { get; set; }

        private InterviewViewModel ViewModel;

        protected override void OnInitialized()
        {
            ViewModel = new InterviewViewModel(ContextFactory);
            if (InterviewId.HasValue)
            {
                ViewModel.LoadInterviewById(InterviewId.Value);
            }
        }

        private void OnCategoryChanged(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value?.ToString(), out var catId))
            {
                ViewModel.SelectedCategoryId = catId;
                ViewModel.LoadQuestionsForCategory();
                ViewModel.SelectedQuestionId = null;
            }
        }

        private void OnQuestionChanged(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value?.ToString(), out var qId))
            {
                ViewModel.SelectedQuestionId = qId;
            }
        }

        private void AddQuestion()
        {
            ViewModel.AddQuestionToInterview();
            ViewModel.SaveInterview();
        }
        private void SaveInterview()
        {
            ViewModel.SaveInterview();
            // Optionally, clear the form or show a message here
        }

        private void DeleteQuestion(int questionId)
        {
            ViewModel.RemoveQuestionFromInterview(questionId);
            ViewModel.SaveInterview();
        }

        private void GenerateInterviewDoc()
        {
            int interviewId = InterviewId ?? 0;
            ViewModel.GenerateInterviewDoc(interviewId);
        }
    }
}
