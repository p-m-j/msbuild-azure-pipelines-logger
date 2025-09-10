#pragma warning disable CS0067

using Microsoft.Build.Framework;

namespace MSBuild.Azure.PipelinesIssueLogger;

public class TestEventSource : IEventSource
{
    public event BuildMessageEventHandler? MessageRaised;
    public event BuildErrorEventHandler? ErrorRaised;
    public event BuildWarningEventHandler? WarningRaised;
    public event BuildStartedEventHandler? BuildStarted;
    public event BuildFinishedEventHandler? BuildFinished;
    public event ProjectStartedEventHandler? ProjectStarted;
    public event ProjectFinishedEventHandler? ProjectFinished;
    public event TargetStartedEventHandler? TargetStarted;
    public event TargetFinishedEventHandler? TargetFinished;
    public event TaskStartedEventHandler? TaskStarted;
    public event TaskFinishedEventHandler? TaskFinished;
    public event CustomBuildEventHandler? CustomEventRaised;
    public event BuildStatusEventHandler? StatusEventRaised;
    public event AnyEventHandler? AnyEventRaised;

    public void Warn(BuildWarningEventArgs warning) =>
        WarningRaised?.Invoke(this, warning);

    public void Error(BuildErrorEventArgs error) =>
        ErrorRaised?.Invoke(this, error);
}