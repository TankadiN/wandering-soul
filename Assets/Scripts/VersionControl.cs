using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionControl : MonoBehaviour
{
    public TMP_Text appVersion;
    public TMP_Text unityVersion;

    void Start()
    {
        appVersion.text = "v" + Application.version;
        unityVersion.text = "Built on Unity Personal " + Application.unityVersion;
    }
}
