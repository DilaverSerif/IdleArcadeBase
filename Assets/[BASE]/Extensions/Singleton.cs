using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            instance = FindObjectOfType<T>();
            
            if (instance == null)
            {
                GameObject singletonObject = new GameObject(typeof(T).Name);
                instance = singletonObject.AddComponent<T>();
            }
            return instance;
        }
    }
}

public abstract class SingletonDontDestroy<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            instance = FindObjectOfType<T>();

            if (instance == null)
            {
                GameObject singletonObject = new GameObject(typeof(T).Name);
                instance = singletonObject.AddComponent<T>();

                DontDestroyOnLoad(singletonObject);
            }
            return instance;
        }
    }
}