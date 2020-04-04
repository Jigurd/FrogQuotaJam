public abstract class Task
{
    protected int _minutesSinceTaskStart = 0;
    public InGameTime Time { get; set; }
    public abstract string Name { get; protected set; }

    public virtual void OnStart()
    {
        _minutesSinceTaskStart = 0;
        InGameTimeManager.OnMinute += OnMinute;
    }
    public virtual void OnFinished()
    {
        InGameTimeManager.OnMinute -= OnMinute;
    }
    public abstract void OnUpdate();
    public abstract bool IsComplete();

    private void OnMinute(int minute)
    {
        _minutesSinceTaskStart++;
    }
}
