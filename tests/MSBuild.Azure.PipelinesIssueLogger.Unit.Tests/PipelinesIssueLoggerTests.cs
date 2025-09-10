using Microsoft.Build.Framework;

namespace MSBuild.Azure.PipelinesIssueLogger;

public class PipelinesIssueLoggerTests
{
    [Fact]
    public void Warnings_logged_with_correct_format()
    {
        var warning = new BuildWarningEventArgs(
            subcategory: "subcategory",
            code: "42",
            file: "~/foo.cs",
            lineNumber: 12,
            columnNumber: 34,
            endLineNumber: 56,
            endColumnNumber: 78,
            message: "Misc error message",
            helpKeyword: "",
            senderName: ""
        );
        var writer = new StringWriter();
        var sut = new PipelinesIssueLogger(writer);
        var eventSource = new TestEventSource();
        sut.Initialize(eventSource);
        eventSource.Warn(warning);
        sut.Shutdown();

        var expected = "##vso[task.logissue type=warning;sourcepath=~/foo.cs;linenumber=12;columnnumber=34;code=42;]Misc error message" + Environment.NewLine;
        Assert.Equal(expected, writer.ToString());
    }

    [Fact]
    public void Errors_logged_with_correct_format()
    {
        var error = new BuildErrorEventArgs(
            subcategory: "subcategory",
            code: "42",
            file: "~/foo.cs",
            lineNumber: 12,
            columnNumber: 34,
            endLineNumber: 56,
            endColumnNumber: 78,
            message: "Misc error message",
            helpKeyword: "",
            senderName: ""
        );
        var writer = new StringWriter();
        var sut = new PipelinesIssueLogger(writer);
        var eventSource = new TestEventSource();
        sut.Initialize(eventSource);
        eventSource.Error(error);
        sut.Shutdown();

        var expected = "##vso[task.logissue type=error;sourcepath=~/foo.cs;linenumber=12;columnnumber=34;code=42;]Misc error message" + Environment.NewLine;
        Assert.Equal(expected, writer.ToString());
    }
}