using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // DontDestroyOnLoad allows the GameManager to be preserved even if different scenes are loaded, in which it is not declared as an GamObject.
                                       // Previously this was implemented in each script, where it was needed. After moving it in its own script it can be used for all gameObjects by attaching this script.
    }

}
