namespace ThyroCareX.Data.Healpers
{
    public class PayMobSettings
    {
        public string ApiKey { get; set; } = string.Empty;
        public string IframeId { get; set; } = string.Empty;
        public string CardIntegrationId { get; set; } = string.Empty;
        public string KioskIntegrationId { get; set; } = string.Empty;
        public string HmacSecret { get; set; } = string.Empty;
        public string NotificationUrl { get; set; } = string.Empty;
    }
}
