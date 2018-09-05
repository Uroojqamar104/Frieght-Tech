using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace FreightTech.App.Helpers
{
    public interface IPushNotification
    {
        bool QueueMessage(string to, string title, string message, string urlNotificationClick);
    }
    public class GoogleFirebaseNotification : IPushNotification
    {
        public bool QueueMessage(string to, string title, string message, string urlNotificationClick)
        {
            if (string.IsNullOrEmpty(to))
            {
                return false;
            }
            var serverApiKey = "AAAAqWizcQ4:APA91bFFMmK2y_rGMXpLrUtNYkUI62Q01XAufeuz0idvTWMccLVlQRgEBSqUx-e16PyD03p40uVH_UNPcivofxRMmSoQmkpv1ZXxTwo85wCRQuBiWIsQfhjRV9ILfK7_kuDnRlVDbeKbY4ZquoKIfE4vC9ikmURIAQ";
            var firebaseGoogleUrl = "https://fcm.googleapis.com/fcm/send";

            var httpClient = new WebClient();
            httpClient.Headers.Add("Content-Type", "application/json");
            httpClient.Headers.Add(HttpRequestHeader.Authorization, "key=" + serverApiKey);
            var timeToLiveInSecond = 24 * 60; // 1 day
            var data = new
            {
                to = to,
                data = new
                {
                    notification = new
                    {
                        body = message,
                        title = title,
                        icon = "/Content/Images/truck1_24x24.png",
                        url = urlNotificationClick
                    }
                },
                time_to_live = timeToLiveInSecond
            };

            var json = JsonConvert.SerializeObject(data);
            Byte[] byteArray = Encoding.UTF8.GetBytes(json);
            var responsebytes = httpClient.UploadData(firebaseGoogleUrl, "POST", byteArray);
            string responsebody = Encoding.UTF8.GetString(responsebytes);
            dynamic responseObject = JsonConvert.DeserializeObject(responsebody);

            return responseObject.success == "1";
        }
    }
}