using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quiz_Csharp.Models;
using System.Text.Json;

namespace Quiz_Csharp.Pages
{
    public class QuizModel : PageModel
    {
        [BindProperty]
        public List<Question> Questions { get; set; } = new();

        [BindProperty]
        public int CurrentQuestionIndex { get; set; } = 0;

        [BindProperty]
        public int Score { get; set; } = 0;

        public int TotalQuestions => Questions?.Count ?? 0;

        public Question CurrentQuestion => 
            Questions != null && CurrentQuestionIndex < Questions.Count ? Questions[CurrentQuestionIndex] : null;

        public void OnGet()
        {
            // Charger les questions depuis le fichier JSON
            string json = System.IO.File.ReadAllText("Data/questions.json");
            var questionList = JsonSerializer.Deserialize<QuestionList>(json);

            if (questionList != null && questionList.Questions != null)
            {
                Questions = questionList.Questions.ToList();
            }
        }

        public IActionResult OnPost(string selectedAnswer)
        {
            if (Questions == null || CurrentQuestionIndex >= Questions.Count)
            {
                return RedirectToPage("Error"); // Redirige vers une page d'erreur en cas de problème
            }

            if (selectedAnswer == CurrentQuestion?.CorrectAnswer)
            {
                Score++;
            }

            CurrentQuestionIndex++;

            if (CurrentQuestionIndex >= Questions.Count)
            {
                return RedirectToPage("Results");
            }

            return Page();
        }
    }
}
