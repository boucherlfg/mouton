using System;
using System.Collections.Generic;

public class ServiceManager : Singleton<ServiceManager>
{
    private List<object> _services = new();

    public T Get<T>(Func<T> generator = null) where T : class 
    {
        generator ??= Ext.DefaultConstructor<T>;
        var srv = _services.Find(x => x is T);
        if(srv == null) {
            srv = generator();
            _services.Add(srv);
        }
        return srv as T;
    }
}