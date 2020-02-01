using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSave : MonoBehaviour
{

    public GameObject cameraContainer;

    public CinemachineVirtualCamera[] vCams;

    public List<int> priority;

    void Start()
    {
        vCams = cameraContainer.GetComponentsInChildren<CinemachineVirtualCamera>();
        GameEvents.SaveInitiated += Save;
        Load();
    }

    void Save()
    {
        priority.Clear();
        for (int i = 0; i < vCams.Length; i++)
        {
            priority.Add(vCams[i].m_Priority);
        }

        SaveLoad.Save<List<int>>(priority, "CameraPriorities");
    }

    void Load()
    {
        if(SaveChecker.CheckSaveFile())
        {
            foreach (int number in SaveLoad.Load<List<int>>("CameraPriorities"))
            {
                priority.Add(number);
            }

            for (int i = 0; i < vCams.Length; i++)
            {
                vCams[i].m_Priority = priority[i];
            }
        }
    }
}
