using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSave : MonoBehaviour
{
    public GameObject managerScript;

    public void Save()
    {
        EasySaveManager script = managerScript.GetComponent<EasySaveManager>();
        ES2.Save(script.ammo, "ammo");
        ES2.Save(script.magazines, "magazines");
        ES2.Save(script.brokenArm, "brokenArm");
    }
}