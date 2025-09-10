// ReSharper disable UnusedMember.Global

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace MSBuild.Azure.PipelinesIssueLogger
{
    public class PipelinesIssueLogger : Logger
    {
        private IEventSource _eventSource;
        private readonly TextWriter _writer;

        public PipelinesIssueLogger()
            : this(Console.Out)
        { }

        public PipelinesIssueLogger(TextWriter writer) =>
            _writer = writer;

        public override void Initialize(IEventSource eventSource)
        {
            eventSource.WarningRaised += EventSourceOnWarningRaised;
            eventSource.ErrorRaised += EventSourceOnErrorRaised;
            _eventSource = eventSource;
        }

        private void EventSourceOnErrorRaised(object _, BuildErrorEventArgs e) =>
            LogIssue(e.Message ?? string.Empty, e.GetIssueProperties());

        private void EventSourceOnWarningRaised(object _, BuildWarningEventArgs e) =>
            LogIssue(e.Message ?? string.Empty, e.GetIssueProperties());

        private void LogIssue(
            string message,
            IEnumerable<KeyValuePair<string,string>> properties)
        {
            _writer.Write("##vso[");
            _writer.Write($"task.logissue ");
            foreach (var prop in properties)
            {
                _writer.Write($"{prop.Key}={prop.Value};");
            }
            _writer.Write("]");
            _writer.Write($"{message}{Environment.NewLine}");
        }

        public override void Shutdown()
        {
            if (_eventSource == null)
            {
                return;
            }

            _eventSource.WarningRaised -= EventSourceOnWarningRaised;
            _eventSource.ErrorRaised -= EventSourceOnErrorRaised;
        }
    }
}
