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
        Load();
    }

    public void GatherPriorities()
    {
        priority.Clear();
        for (int i = 0; i < vCams.Length; i++)
        {
            priority.Add(vCams[i].m_Priority);
        }
    }

    void Load()
    {
        string saveString = SaveSystem.Load();
        if (saveString != null)
        {
            Savepoint.SaveData saveData = JsonUtility.FromJson<Savepoint.SaveData>(saveString);

            foreach (int number in saveData.camPriority)
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
