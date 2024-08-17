using System;

public class BaseEvent {
    private event Action Change;
    public void Invoke() => Change?.Invoke();
    public void Subscribe(Action action) => Change += action;
    public void Unsubscribe(Action action) => Change -= action;
}

public class BaseEvent<T> {
    private event Action<T> Change;
    public void Invoke(T value) => Change?.Invoke(value);
    public void Subscribe(Action<T> action) => Change += action;
    public void Unsubscribe(Action<T> action) => Change -= action;
}
