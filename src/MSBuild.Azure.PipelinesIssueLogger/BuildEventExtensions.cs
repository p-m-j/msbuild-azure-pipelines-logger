using System.Collections.Generic;
using Microsoft.Build.Framework;

namespace MSBuild.Azure.PipelinesIssueLogger
{
    internal static class BuildEventExtensions
    {
        public static IEnumerable<KeyValuePair<string, string>> GetIssueProperties(this BuildWarningEventArgs eventArgs)
        {
            yield return new KeyValuePair<string, string>("type", "warning");
            yield return new KeyValuePair<string, string>("sourcepath", eventArgs.File);
            yield return new KeyValuePair<string, string>("linenumber", $"{eventArgs.LineNumber}");
            yield return new KeyValuePair<string, string>("columnnumber", $"{eventArgs.ColumnNumber}");
            yield return new KeyValuePair<string, string>("code", $"{eventArgs.Code}");
        }

        public static IEnumerable<KeyValuePair<string, string>> GetIssueProperties(this BuildErrorEventArgs eventArgs)
        {
            yield return new KeyValuePair<string, string>("type", "error");
            yield return new KeyValuePair<string, string>("sourcepath", eventArgs.File);
            yield return new KeyValuePair<string, string>("linenumber", $"{eventArgs.LineNumber}");
            yield return new KeyValuePair<string, string>("columnnumber", $"{eventArgs.ColumnNumber}");
            yield return new KeyValuePair<string, string>("code", $"{eventArgs.Code}");
        }
    }
}