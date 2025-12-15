using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DailyDiary
{
    public class DiaryManager
    {
        private List<DiaryEntry> _entries;
        private const string FileName = "diary.json";

        public DiaryManager()
        {
            _entries = new List<DiaryEntry>();
            LoadEntries();
        }

        public void AddEntry(string content)
        {
            int nextId = _entries.Any() ? _entries.Max(e => e.Id) + 1 : 1;
            var entry = new DiaryEntry
            {
                Id = nextId,
                Content = content,
                Date = DateTime.Now
            };
            _entries.Add(entry);
            SaveEntries();
        }

        public List<DiaryEntry> GetEntries()
        {
            return _entries.OrderByDescending(e => e.Date).ToList();
        }

        public bool DeleteEntry(int id)
        {
            var entry = _entries.FirstOrDefault(e => e.Id == id);
            if (entry != null)
            {
                _entries.Remove(entry);
                SaveEntries();
                return true;
            }
            return false;
        }

        private void SaveEntries()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_entries, options);
            File.WriteAllText(FileName, jsonString);
        }

        private void LoadEntries()
        {
            if (File.Exists(FileName))
            {
                string jsonString = File.ReadAllText(FileName);
                try
                {
                    _entries = JsonSerializer.Deserialize<List<DiaryEntry>>(jsonString) ?? new List<DiaryEntry>();
                }
                catch (JsonException)
                {
                    _entries = new List<DiaryEntry>();
                }
            }
        }
    }
}
