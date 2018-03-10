using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;

namespace Security.WebApi.Helpers
{
    public static class LogHelper
    {
        public static void ErrorEx(this ILogger logger, Exception ex, object objectParams = null)
        {
            var data = new Dictionary<object, object>();
            foreach (DictionaryEntry dictionaryEntry in ex.Data)
            {
                data.Add(dictionaryEntry.Key, dictionaryEntry.Value);
            }

            if (objectParams != null)
                foreach (var propertyInfo in objectParams.GetType().GetProperties())
                {
                    data[propertyInfo.Name] = propertyInfo.GetValue(objectParams);
                }

            var exceptionMassage = ex.GetFullMessage();
            var dataMessage = data.DictionaryToString();
            var stackTrace = ex.StackTrace;
            logger.Error(ex, $"\"{ex.GetType().FullName}: {exceptionMassage}\"{stackTrace}\r\n{dataMessage}");
        }

        public static string DictionaryToString(this IDictionary dictionary)
        {
            var data = new Dictionary<object, object>();
            foreach (DictionaryEntry dictionaryEntry in dictionary)
            {
                data.Add(dictionaryEntry.Key, dictionaryEntry.Value);
            }

            var s = data.Aggregate("Dictionary Data:\r\n", (c, n) => { return $"{c}key=[{n.Key}] : value=[{n.Value}]\r\n"; });

            return s;
        }

        public static string GetFullMessage(this Exception e)
        {
            if (e == null)
                return string.Empty;

            return string.Format("{0}. Внутреннее сообщение: {1}", e.Message, GetFullMessage(e.InnerException));
        }
    }
}