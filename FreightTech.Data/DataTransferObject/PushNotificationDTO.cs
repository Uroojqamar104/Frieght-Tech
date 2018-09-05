namespace FreightTech.Data.DataTransferObject
{
    public class PushNotificationDTO
    {
        public string TagKey { get; set; }
        public string TagValue { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
    }
}
