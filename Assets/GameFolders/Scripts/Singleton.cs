using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if ((Object)_instance == (Object)null)
            {
                _instance = Object.FindObjectOfType<T>();
                if ((Object)_instance == (Object)null)
                {
                    Debug.LogError("An instance of " + typeof(T)?.ToString() + " is needed in the scene, but there is none.");
                }
            }

            return _instance;
        }
    }
}
