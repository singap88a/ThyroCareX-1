using System;

namespace ThyroCareX.Core.Feature.Contact.Queries.Result
{
    public class ContactMessageResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string? AttachmentUrl { get; set; }
        public bool IsReplied { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
