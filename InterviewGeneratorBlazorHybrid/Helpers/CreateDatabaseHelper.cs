using InterviewGeneratorBlazorHybrid.Data;
using InterviewGeneratorBlazorHybrid.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using InterviewGeneratorBlazorHybrid.ViewModels;


namespace InterviewGeneratorBlazorHybrid.Helpers
{
    internal class CreateDatabaseHelper
    {
        private readonly AppDbContextFactory _contextFactory;


        public CreateDatabaseHelper(AppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void CreateDatabase(string databaseFilePath)
        {
            Preferences.Set("DatabaseFilePath", databaseFilePath);

            using var context = _contextFactory.CreateDbContext();
            // Ensure the database is deleted, and then is created if needed.
            context.Database.EnsureDeletedAsync();
            context.Database.EnsureCreated();

            //_viewModelStore.CategoryViewModel.ResetViewModel();
            //_viewModelStore.QuestionViewModel.ResetViewModel();
            //_viewModelStore.InterviewViewModel.ResetViewModel();
        }
    }
}
