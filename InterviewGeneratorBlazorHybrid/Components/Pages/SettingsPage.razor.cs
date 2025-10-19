using InterviewGeneratorBlazorHybrid.Data;

namespace InterviewGeneratorBlazorHybrid.Components.Pages;

public partial class SettingsPage
{
    private bool _isPicking;
    private string GeneralMessage = string.Empty;
    private string ErrorMessage = string.Empty;
    private string SuccessMessage = string.Empty;
    private string DisplayPath = "Not Set";       
    private AppDbContextFactory _appDbFactory = new AppDbContextFactory();
    private AppDbIntegrityCheck _appDbIntegrityCheck;

    protected override void OnInitialized()
    {
        if (Preferences.Get("DatabaseFilePath", "Not Set") == "Not Set")
        {
            GeneralMessage = "You haven't selected a valid database file yet. Please select one to proceed.";
        }
        DisplayPath = Preferences.Get("DatabaseFilePath", "Not set");            
        _appDbIntegrityCheck = new AppDbIntegrityCheck(_appDbFactory);
    }

    private async Task SelectDifferentDatabase()
    {
        if (_isPicking)
            return;

        _isPicking = true;
        try
        {
            var result = await FilePicker.Default.PickAsync();
            if (result != null)
            {
                // FullPath may be null on some platforms — fall back to FileName if needed.
                var selectedPath = result.FullPath ?? result.FileName;
                // Set the preferences, then validate the database
                Preferences.Set("DatabaseFilePath", selectedPath);

                if (!_appDbIntegrityCheck.IsValidDatabase())
                {
                    DisplayErrorMessage();
                }
                else
                {
                    DisplaySuccessMessage(selectedPath);
                }


                // DisplayPath = selectedPath;
                // StateHasChanged();
                // NavigationManager.NavigateTo("/");
            }
        }
        catch (Exception ex)
        {
            DisplayErrorMessage();
            StateHasChanged();
        }
        finally
        {
            _isPicking = false;
        }
    }

    private void DisplaySuccessMessage(string selectedPath)
    {
        SuccessMessage = "Database file selected successfully.";
        GeneralMessage = string.Empty;
        ErrorMessage = string.Empty;
        DisplayPath = selectedPath;
    }

    private void DisplayErrorMessage()
    {
        ErrorMessage = "The selected file is not a valid database. Please select a different file.";
        DisplayPath = "Not Set";
        Preferences.Set("DatabaseFilePath", "Not Set");
    }
}
