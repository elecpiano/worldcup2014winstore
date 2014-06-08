using NotificationsExtensions.ToastContent;
using System;
using System.Linq;
using Windows.UI.Notifications;

namespace WorldCup2014WinStore.Utility
{
    public class NotificationHelper
    {
        private static ToastNotifier _toastNotifier = null;
        private static ToastNotifier toastNotifier
        {
            get
            {
                if (_toastNotifier == null)
                {
                    _toastNotifier = Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier();
                }
                return _toastNotifier;
            }
        }
        public static bool AddAlarm(string id, string title, string description, DateTime dueTime)
        {
            var toasts = toastNotifier.GetScheduledToastNotifications();
            var toast = toasts.FirstOrDefault(x => x.Id == id);
            if (toast != null)
            {
                return false;
            }

            try
            {
                // Create the toast content by direct string manipulation.
                string toastXmlString =
                    "<toast duration=\"long\">\n" +
                        "<visual>\n" +
                            "<binding template=\"ToastText02\">\n" +
                                "<text id=\"1\">" + title + "</text>\n" +
                                "<text id=\"2\">" + description + "</text>\n" +
                            "</binding>\n" +
                        "</visual>\n" +
                        "<commands scenario=\"alarm\">\n" +
                            "<command id=\"snooze\"/>\n" +
                            "<command id=\"dismiss\"/>\n" +
                        "</commands>\n" +
                        "<audio src=\"ms-winsoundevent:Notification.Looping.Alarm2\" loop=\"true\" />\n" +
                    "</toast>\n";

                // Create an XML document from the XML.
                var toastDOM = new Windows.Data.Xml.Dom.XmlDocument();
                toastDOM.LoadXml(toastXmlString);

                //Schedule the alarm with the custom snooze interval
                TimeSpan snoozeInterval = TimeSpan.FromSeconds(300);
                var customAlarmScheduledToast = new Windows.UI.Notifications.ScheduledToastNotification(toastDOM, dueTime, snoozeInterval, 5);
                customAlarmScheduledToast.Id = id;
                toastNotifier.AddToSchedule(customAlarmScheduledToast);
                return true;
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        public static void RemoveAlarm(string id)
        {
            var toasts = toastNotifier.GetScheduledToastNotifications();
            var toast = toasts.FirstOrDefault(x => x.Id == id);
            if (toast != null)
            {
                toastNotifier.RemoveFromSchedule(toast);
            }
        }

        public static void ClearAlarms()
        {
            var toasts = toastNotifier.GetScheduledToastNotifications();
            foreach (var toast in toasts)
            {
                toastNotifier.RemoveFromSchedule(toast);
            }
        }

        public static void ShowToastMessage(string message)
        {
            IToastNotificationContent toastContent = null;

            IToastText01 templateContent = ToastContentFactory.CreateToastText01();
            templateContent.TextBodyWrap.Text = message;
            toastContent = templateContent;
            ToastNotification toast = toastContent.CreateNotification();
            toastNotifier.Show(toast);
        }

    }
}
