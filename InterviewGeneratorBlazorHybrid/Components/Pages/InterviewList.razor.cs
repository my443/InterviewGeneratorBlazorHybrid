using CommunityToolkit.Maui.Storage;
using InterviewGeneratorBlazorHybrid.Models;
using System.Text;
using System.Threading;


namespace InterviewGeneratorBlazorHybrid.Components.Pages
{
    public partial class InterviewList
    {
        string ErrorMessage = string.Empty;
        string SuccessMessage = string.Empty;

        bool _isPicking = false;
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
            Navigation.NavigateTo($"/interview");
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

        private async Task GenerateInterview(Interview interview)
        {
            SuccessMessage = string.Empty;
            try
            {
                string todaysdate = DateTime.Now.ToString("yyyy-MM-dd");

                var docxTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        // Windows: extension works
                        { DevicePlatform.WinUI, new[] { ".docx" } },

                    });
                //using MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes("Hello from the Community Toolkit!"));
                using MemoryStream stream = ViewModel.GenerateInterviewDoc(interview.Id);
                var result = await FileSaver.Default.SaveAsync($"{todaysdate}-Output Document.docx", stream, CancellationToken.None);

                if (result != null && (result.IsSuccessful))
                {
                    DisplaySuccessMessage(result.FilePath);
                }
            }
            catch (Exception ex)
            {
                DisplayErrorMessage();
            }
            //finally
            //{
            //    _isPicking = false;
            //}
        }

        private void DisplayErrorMessage()
        {
            ErrorMessage = "<p class=\"alert-danger\">The file couldn't be saved. There was an error. Please try again." +
                            "<br/>If the problem persists, go to <u>Settings</u> to select a different template file.</p>";
        }

        private void DisplaySuccessMessage(string savedPath)
        {
            SuccessMessage = $"The file was successfully saved to: {savedPath}";
        }
    }
}
