public abstract class Peril
{
    protected int _minutesSinceTaskStart = 0;
    public InGameTime Time { get; set; }

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

    private void OnMinute(int minute)
    {
        _minutesSinceTaskStart++;
    }
}
