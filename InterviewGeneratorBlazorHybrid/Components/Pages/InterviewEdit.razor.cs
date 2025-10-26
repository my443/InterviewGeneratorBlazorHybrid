﻿using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewGeneratorBlazorHybrid.Components.Pages
{
    public partial class InterviewEdit
    {
        // ensure the component re-renders when the viewmodel state changes
        protected override void OnInitialized()
        {
            ViewModel.OnChange += OnViewModelChanged;
        }

        private void OnViewModelChanged() => InvokeAsync(StateHasChanged);

        public void Dispose()
        {
            ViewModel.OnChange -= OnViewModelChanged;
        }
    }
}
