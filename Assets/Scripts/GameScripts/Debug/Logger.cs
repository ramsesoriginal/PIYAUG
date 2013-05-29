using UnityEngine;
using System.Collections;

/// <summary>
/// Class to log events for debug purposes. Should output pretty HTML
/// </summary>
public static class Logger
{
    #region static Interface
    private static HTMLLogger _logger;
    private static HTMLLogger logger
    {
        get
        {
            if (_logger == null)
                _logger = new HTMLLogger();
            return _logger;
        }
    }

    public static TaggedLogger Tags
    {
        get
        {
            return new TaggedLogger();
        }
    }

    public class TaggedLogger
    {
        public string[] tags;

        public TaggedLogger()
        {
            tags = null;
        }

        public TaggedLogger this[params string[] Logtags]
        {
            get
            {
                return new TaggedLogger() { tags = Logtags };
            }
        }

        public  void Log(string message = null, params object[] logData)
        {
            logger.addToLog(message, null, tags, logData);
        }
        public void Log(Exception exception, params object[] logData)
        {
            logger.addToLog(null, exception, tags, logData);
        }

        public void Log(Exception exception, string message = null,  params object[] logData)
        {
            logger.addToLog(message, exception, tags, logData);
        }

        public void LogWithScreenshot(string message = null, params object[] logData)
        {
            logger.addScreenShotToLog(message, null, tags, logData);
        }

        public void LogWithScreenshot(Exception exception, params object[] logData)
        {
            logger.addScreenShotToLog(null, exception, tags, logData);
        }

        public void LogWithScreenshot( Exception exception, string message = null, params object[] logData)
        {
            logger.addScreenShotToLog(message, exception, tags, logData);
        }


    }

    public static void Log(params object[] logData)
    {
        logger.addToLog(null, null, null, logData);
    }

    public static void Log(this string message, params object[] logData)
    {
        logger.addToLog(message, null, null, logData);
    }

    public static void Log(this Exception exception, params object[] logData)
    {
        logger.addToLog(null, exception, null, logData);
    }

    public static void Log(this Exception exception, string message = null, params object[] logData)
    {
        logger.addToLog(message, exception, null, logData);
    }

    public static void LogWithScreenshot(string message = null, params object[] logData)
    {
        logger.addScreenShotToLog(message, null, null, logData);
    }

    public static void LogWithScreenshot(this Exception exception, params object[] logData)
    {
        logger.addScreenShotToLog(null, exception, null, logData);
    }

    public static void LogWithScreenshot(this Exception exception, string message = null, params object[] logData)
    {
        logger.addScreenShotToLog(message, exception, null, logData);
    }

    public static void HTMLTable(StringBuilder sb)
    {
        logger.ToHTMLTable(sb);
    }
    #endregion

    #region HTMLLogger
    class HTMLLogger {
        private List<LogEntry> entries;

        public HTMLLogger()
        {
            entries = new List<LogEntry>();
            if (_logger != null)
                throw new Exception("WTF are you doing? singleton, modafucka!");
        }

        public void addToLog(string message = null, Exception exception = null, IEnumerable<string> tags = null, IEnumerable<object> logData = null)
        {
            entries.Add(new LogEntry(message, null, exception, tags, logData));
        }

        public void addScreenShotToLog(string message = null, Exception exception = null, IEnumerable<string> tags = null, IEnumerable<object> logData = null)
        {
            entries.Add(new LogEntry(message, null, exception, tags, logData));
        }

        public IEnumerable<LogEntry> Entries
        {
            get
            {
                return entries;
            }
        }

        public string ToString()
        {
            var sb = new StringBuilder();
            ToHTMLTable(sb);
            return sb.ToString(); 
        }


        public void ToHTMLTable(StringBuilder sb)
        {
            sb.Append("<table>");

            sb.Append("<thead>");

            sb.Append("<tr>");

            sb.Append("<th>");
            sb.Append("Timestamp");
            sb.Append("</th>");

            sb.Append("<th>");
            sb.Append("Player Position");
            sb.Append("</th>");

            sb.Append("<th>");
            sb.Append("Message");
            sb.Append("</th>");

            sb.Append("<th>");
            sb.Append("Screenshot");
            sb.Append("</th>");

            sb.Append("<th>");
            sb.Append("Exception");
            sb.Append("</th>");

            sb.Append("<th>");
            sb.Append("Additional Debug Data");
            sb.Append("</th>");

            sb.Append("</tr>");

            sb.Append("</thead>");

            sb.Append("<tbody>");
            sb.Append("</tbody>");

            foreach (LogEntry entry in Entries)
            {
                entry.ToHTMLRow(sb);
            }

            sb.Append("</table>");

        }
    }

    #endregion

    class LogEntry
    {
        public class Coordinates
        {
            public double x;
            public double y;
            public double z;
        }

        private string message;
        public string Message {
            get
            {
                return message;
            }
        }

        private DateTime timestamp;
        public DateTime Timestamp {
            get
            {
                return timestamp;
            }
        }

        private Coordinates position;
        public Coordinates Position
        {
            get
            {
                return position;
            }
        }

        private List<string> tags;
        public IEnumerable<string> Tags
        {
            get
            {
                return tags;
            }
        }

        private string imageUrl;
        public string ImageUrl
        {
            get
            {
                return imageUrl;
            }
        }

        private Exception exception;
        public Exception Exception
        {
            get
            {
                return exception;
            }
        }

        private List<string> logData;
        public List<string> LogData
        {
            get
            {
                return logData;
            }
        }

        public LogEntry(string message = null, string imageUrl = null, Exception exception = null,  IEnumerable<string> tags = null, IEnumerable<object> logData = null)
        {
            this.message = "";
            this.imageUrl = "";
            this.timestamp = DateTime.Now;
            this.position = null; //Position
            this.tags = new List<string>();
            this.logData = new List<string>();
            if (logData!=null)
                foreach (object item in logData)
                {
                    this.logData.Add(item.ToLogData());
                }
            this.exception = null;

            if (imageUrl!=null)
                addUrl(imageUrl);
            if (message!=null)
                addMessage(message);
            if (Exception!=null)
                addException(exception);
            if (tags != null)
                addTags(tags);
        }

        private void addMessage(string message)
        {
            this.message = message;
            tags.Add("message");
        }

        private void addTags(IEnumerable<string> tags)
        {
            this.tags.AddRange(tags);
        }


        private void addUrl(string imageUrl)
        {
            this.imageUrl = imageUrl;
            tags.Add("image");
        }

        private void addException(Exception exception)
        {
            this.exception = exception;
            tags.Add("exception");
        }


        public override string ToString()
        {
            var sb = new StringBuilder();
            ToHTMLRow(sb);
            return sb.ToString();
        }

        public void ToHTMLRow(StringBuilder sb)
        {
            sb.Append("<tr");
            if (Tags.Count() > 0)
            {
                sb.Append(" class=\"");
                foreach (string tag in Tags)
                {
                    sb.Append(tag.Replace(' ', '_').Replace('\'', '_').Replace('"', '_'));
                }
                sb.Append("\" ");
            }
            sb.Append(">");
            AppendTimestampcell(sb);
            AppendPosition(sb);
            AppendMessage(sb);
            AppendImage(sb);
            AppendException(sb);
            AppendAdditionalDebugdata(sb);
            sb.Append("</tr>");
        }

        private void AppendTimestampcell(StringBuilder sb)
        {
            sb.Append("<td class=\"timestamp\">");
            {
                sb.AppendFormat("<time datetime=\"{0}-{1}-{2} {3}:{4}\">", timestamp.Year, timestamp.Month, timestamp.Day, timestamp.Hour, timestamp.Minute);
                {
                    sb.Append(timestamp.ToShortDateString());
                    sb.Append(" ");
                    sb.AppendFormat("{0}:{1}:{2}+{3}", timestamp.Hour, timestamp.Minute, timestamp.Second, timestamp.Millisecond);
                }
                sb.Append("</time>");
            }
            sb.Append("</td>");
        }

        private void AppendPosition(StringBuilder sb)
        {
            sb.Append("<td class=\"position\">");
            {
                if (Position!=null)
                {
                    sb.AppendFormat(" X: \"{0}\"", Position.x);
                    sb.AppendFormat(" Y: \"{0}\"", Position.y);
                    sb.AppendFormat(" Z: \"{0}\"", Position.z);
                }
            }
            sb.Append("</td>");
        }

        private void AppendMessage(StringBuilder sb)
        {
            sb.Append("<td class=\"message\">");
            {
                sb.Append(Message);
            }
            sb.Append("</td>");
        }

        private void AppendImage(StringBuilder sb)
        {
            sb.Append("<td class=\"image\">");
            {
                sb.AppendFormat("<a href=\"{0}\"><img src=\"{0}\" /></a>", ImageUrl);
            }
            sb.Append("</td>");
        }

        private void AppendException(StringBuilder sb)
        {
            sb.Append("<td class=\"exception\">");
            {
                AppendSingleException(sb, this.Exception);
            }
            sb.Append("</td>");
        }
        private void AppendSingleException(StringBuilder sb, Exception ex)
        {
            sb.Append("<div class=\"Exception\">");
            if (ex != null)
            {
                sb.AppendFormat("<div class=\"mesage\">{0}</div>", ex.Message ?? "");
                sb.AppendFormat("<div class=\"source\">{0}</div>", ex.Source ?? "");
                sb.AppendFormat("<div class=\"stackTrace\">{0}</div>", ex.StackTrace ?? "");
                //sb.AppendFormat("<div class=\"targetSite\">{0}</div>", ex.TargetSite ?? "");
                sb.AppendFormat("<div class=\"helpLink\">{0}</div>", ex.HelpLink ?? "");
                //sb.AppendFormat("<div class=\"data\">{0}</div>", ex.Data ?? "");
                if (ex.InnerException != null)
                {
                    sb.Append("<div class=\"innerException\">");
                    AppendSingleException(sb, ex.InnerException);
                    sb.Append("</di>");
                }
            }
            sb.Append("</div>");
        }

        private void  AppendAdditionalDebugdata(StringBuilder sb) {
            sb.Append("<td class=\"additionalData\">");
            {
                foreach (string item in LogData)
                {
                    sb.Append("<script type=\"text/xml\">");
                    sb.Append(item);
                    sb.Append("</script>");
                }
            }
            sb.Append("</td>");
        }
    }

}

public interface DebugData
{
    string ToLogData();
}

public static class LogExtensions
{
    public static string ToLogData(this object self)
    {
        if (self is DebugData)
            return (self as DebugData).ToLogData();
        Type type = self.GetType();
        XmlSerializer serializer = new XmlSerializer(type);
        var retval = "";
        using (var writer = new StringWriter())
        {
            serializer.Serialize(writer, self);
            retval = writer.ToString();
        }

        return retval;
    }
}
