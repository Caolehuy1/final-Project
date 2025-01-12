using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    /*public class Singleton<T> : MonoBehaviour*/
{
    /* private static T instance;
     public static T Instance { get { return instance; } }

     protected virtual void Awake()
     {
         if (instance != null && this.gameObject != null)
         {
             Destroy(this.gameObject);
         }
         else
         {
             instance =  (T)(object)(this) ;
         }
         if (!gameObject.transform.parent)
         {
             DontDestroyOnLoad(gameObject);
         }

     }*/

    private static T instance;
    public static T Instance { get { return instance; } }

    protected virtual void Awake()
    {
        if (instance != null && this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = (T)this;
        }

        DontDestroyOnLoad(gameObject);
    }
}
