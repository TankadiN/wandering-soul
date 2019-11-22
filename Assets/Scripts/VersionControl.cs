using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionControl : MonoBehaviour
{
    public TMP_Text appVersion;
    public TMP_Text unityVersion;
    public bool isTestBuild;

    void Start()
    {
        appVersion.text = "v" + Application.version;
        if(isTestBuild)
        {
            appVersion.text += " - TESTBUILD";
        }
        unityVersion.text = "Built on Unity Personal " + Application.unityVersion;
    }
}
