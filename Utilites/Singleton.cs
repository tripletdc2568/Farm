using UnityEngine;


// 该类是一个泛型单例，T 是继承自 Singleton<T> 的类型
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;  // 存储单例实例
    
    public static T Instance  // 外部访问单例实例
    {
        get => instance;
    }

    protected virtual void Awake()
    {
        // 如果已经有实例存在，销毁新的对象
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            // 否则将当前对象设为单例实例
            instance = (T)this;
        }
    }

    protected virtual void OnDestroy()
    {
        // 当对象销毁时，如果当前实例是该对象，则将实例设为 null
        if (instance == this)
        {
            instance = null;
        }
    }
}

