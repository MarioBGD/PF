namespace PF.App.Core.Models
{
    public class TimeCategory
    {
        public enum Categories
        {
            Today,
            Yesterday,
            ThisWeek,
            LastWeek,
            ThisMonth,
            XMonthsAho
        }

        public TimeCategory(Categories category, int number = 0)
        {
            Category = category;
            Number = number;
        }

        public Categories Category { get; }
        public int Number { get; }

        public bool Equals(TimeCategory timeCategory) => this.Category == timeCategory.Category && this.Number == timeCategory.Number;
    }
}