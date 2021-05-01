using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentControllersManager : MonoBehaviour
{
    public static PersistentControllersManager instance;

    [RuntimeInitializeOnLoadMethod]
    public static void Initialize()
    {
        string prefabName = "M - PersistentManagers";
        GameObject effectsController = Resources.Load(prefabName) as GameObject;
        GameObject instantiatedDevPanel = Instantiate(effectsController);
        DontDestroyOnLoad(instantiatedDevPanel);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this);
        }
        else if (instance != this)
            Destroy(gameObject);
    }
}
