using UnityEngine;
public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = Resources.Load<T>(typeof(T).Name);
            }
            return _instance;
        }
    }
}