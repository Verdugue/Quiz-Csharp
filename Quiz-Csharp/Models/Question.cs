namespace Quiz_Csharp.Models
{
    public class Question
    {
        public required string QuestionText { get; set; }
        public required string CorrectAnswer { get; set; }
        public required Answer[] Answers { get; set; }
        public required string Difficulty { get; set; }
    }

    public class Answer
    {
        public required string Text { get; set; }
    }

    public class QuestionList
    {
        public required Question[] Questions { get; set; }
    }
}
