using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static bool _isQuit;

    public static bool IsCreated => _instance != null;

    public static T Instance
    {
        get
        {
            if (_instance == null && !_isQuit)
            {
                _instance = FindObjectOfType<T>();

                if (_instance != null)
                {
                    (_instance as SingletonMonoBehaviour<T>)?.InitializeSingleton();
                }
            }

            return _instance;
        }
    }


    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (!IsCreated)
        {
            _isQuit = false;
            _instance = Instance;
        }
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _isQuit = true;
            _instance = null;
        }
    }

    protected virtual void OnApplicationQuit()
    {
        _isQuit = true;
    }
    protected virtual void InitializeSingleton()
    {
        DontDestroyOnLoad(gameObject);
    }
}
