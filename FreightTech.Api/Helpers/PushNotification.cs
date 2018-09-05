using FreightTech.Data.DataTransferObject;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace FreightTech.Api.Helpers
{
    public interface IPushNotification
    {
        Task<bool> NotifyDriversAboutNewOrders();
        Task<bool> NotifyCustomerAboutOrderAccepted(int customerId, string driverName);
    }

    public class PushNotification : IPushNotification
    {
        string TagKey = "topic";
        string DriverTag = "driver";
        string CustomerTag = "customer";
        string DriverTagPrefix = "driver_";
        string CustomerTagPrefix = "customer_";

        public async Task<bool> NotifyDriversAboutNewOrders()
        {
            try
            {
                PushNotificationDTO model = new PushNotificationDTO
                {
                    TagKey = TagKey,
                    TagValue = DriverTag,
                    Title = "New Orders",
                    Message = "New Orders you might interested in."
                };
                return await QueueMessage(model);
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> NotifyCustomerAboutOrderAccepted(int customerId, string driverName)
        {
            PushNotificationDTO model = new PushNotificationDTO
            {
                TagKey = TagKey,
                TagValue = CustomerTagPrefix + customerId,
                Title = "Order accepted",
                Message = string.Format("Your order is accepted by {0}.", driverName)
            };
            return await QueueMessage(model);
        }

        #region QueueMessage
        async Task<bool> QueueMessage(PushNotificationDTO model)
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Basic " + AppKeys.OneSignalRestAppKey);

            var serializer = new JavaScriptSerializer();
            var obj = new
            {
                app_id = AppKeys.OneSignalAppId,
                contents = new { en = model.Message },
                title = new { en = model.Title },
                filters = new object[] { new { field = "tag", key = model.TagKey, value = model.TagValue } }
            };

            var param = serializer.Serialize(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            string responseContent = null;

            try
            {
                using (var writer = await request.GetRequestStreamAsync())
                {
                    await writer.WriteAsync(byteArray, 0, byteArray.Length);
                }

                using (var response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = await reader.ReadToEndAsync();
                    }
                }
            }
            catch (WebException ex)
            {
                //System.Diagnostics.Debug.WriteLine(ex.Message);
                //System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
                return false;
            }
            return true;
            //System.Diagnostics.Debug.WriteLine(responseContent);
        }
        #endregion
    }

}
