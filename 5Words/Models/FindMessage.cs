namespace _5Words.Models
{
    public class FindMessage
    {
        public int Length { get; set; }

        public string Contains { get; set; }

        public string NonContains { get; set; }

        public string Template { get; set; }

        public string AntiTemplate { get; set; }
    }
}
