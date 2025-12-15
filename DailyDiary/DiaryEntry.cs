using System;

namespace DailyDiary
{
    public class DiaryEntry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }

        public DiaryEntry()
        {
            Date = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{Id}] {Date.ToString("yyyy-MM-dd HH:mm")}\n{Content}\n----------------------";
        }
    }
}
