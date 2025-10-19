using InterviewGeneratorBlazorHybrid.Data;
using InterviewGeneratorBlazorHybrid.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace InterviewGeneratorBlazorHybrid.ViewModels
{
    public class InterviewViewModel
    {
        private readonly AppDbContextFactory _contextFactory;
        public AppDbContext _context { get; set; }

        // For the list
        public List<Interview> Interviews { get; set; } = new();
        // For the single 'Interview' View
        public Interview Interview { get; set; } = new();

        public List<Category> Categories { get; set; } = new();
        public List<Question> AvailableQuestions { get; set; } = new();
        
        public string? ErrorMessage { get; set; }
        public int? SelectedCategoryId { get; set; }
        public int? SelectedQuestionId { get; set; }
        public int SelectedInterviewId { get; set; }
        public string InterviewName { get; set; } = string.Empty;
        public DateTime InterviewDate { get; set; } = DateTime.Today;
        public bool DatabaseIsAvailable { get; set; } = false; 

        public InterviewViewModel(AppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;

            if (!IsDatabaseAvailable())
            {
                DatabaseIsAvailable = false;
            }
            else
            {
                DatabaseIsAvailable = true;
                _context = _contextFactory.CreateDbContext();
                LoadCategories();
                LoadInterviews();
            }
        }

        public void LoadCategories()
        {
            using var db = _contextFactory.CreateDbContext();
            Categories = db.Categories.ToList();
        }

        public void LoadQuestionsForCategory()
        {
            if (SelectedCategoryId == null) return;
            using var db = _contextFactory.CreateDbContext();
            AvailableQuestions = db.Questions
                .Include(q => q.Category)
                .Where(q => q.CategoryId == SelectedCategoryId)
                .ToList();
        }

        public void LoadInterviews()
        {
            using var db = _contextFactory.CreateDbContext();

            Interviews = db.Interviews
                .Include(i => i.Questions)
                .OrderByDescending(i => i.DateCreated)
                .ToList();
        }

        public void AddNewInterview()
        {
            AppDbContext context = _contextFactory.CreateDbContext();    
            InterviewName = "<<New Interview>>";
            InterviewDate = DateTime.Today;

            var interview = new Interview
            {
                InterviewName = InterviewName,
                DateCreated = InterviewDate,
                Questions = new List<Question>()
            };

            context.Interviews.Add(interview);
            context.SaveChanges();
            
            Interview = interview;
        }

        public void SaveInterview()
        {
            //using var db = _contextFactory.CreateDbContext();           

            _context.SaveChanges();
        }
        public void LoadInterviewById(int interviewId)
        {
            Interview = _context.Interviews
                .Include(i => i.Questions)
                .ThenInclude(q => q.Category)
                .FirstOrDefault(i => i.Id == interviewId);

            if (Interview != null)
            {
                InterviewName = Interview.InterviewName;
                InterviewDate = Interview.DateCreated;
                // Optionally, set other properties as needed
            }
        }
        public void DeleteInterview(int id)
        {
            try
            {
                using var db = _contextFactory.CreateDbContext();
                var interview = db.Interviews.Find(id);
                if (interview != null)
                {
                    db.Interviews.Remove(interview);
                    db.SaveChanges();
                    LoadInterviews();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error deleting interview: {ex.Message}";
            }
        }

        public void AddQuestionToInterview()
        {
            if (SelectedQuestionId == null) return;

            Question? question = AvailableQuestions.FirstOrDefault(q => q.Id == SelectedQuestionId);
            
            if (question != null && !Interview.Questions.Any(q => q.Id == question.Id))
            {
                Interview.Questions.Add(question);
            }
            SaveInterview();
        }

        public void RemoveQuestionFromInterview(int questionId)
        {
            var question = Interview.Questions.FirstOrDefault(q => q.Id == questionId);
            if (question != null)
            {
                Interview.Questions.Remove(question);
                SaveInterview();
            }
        }

        public void GenerateInterviewDoc(int interviewId)
        {
            var wordHelper = new Helpers.MSWordHelper(_contextFactory);
            wordHelper.GenerateInterviewDoc(interviewId);
        }

        public bool IsDatabaseAvailable()
        {
            var integrityCheck = new AppDbIntegrityCheck(_contextFactory);
            if (Preferences.Get("DatabaseFilePath", "Not Set") == "Not Set")
            {
                DatabaseIsAvailable = false;
            }
            else { 
                DatabaseIsAvailable = integrityCheck.IsValidDatabase();
            }
            return DatabaseIsAvailable;
        }
    }
}