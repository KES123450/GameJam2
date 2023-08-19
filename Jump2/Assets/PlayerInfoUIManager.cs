using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoUIManager : MonoBehaviour
{
    public GameObject UIPrefab;

    public void AddPrefab()
    {
        GameObject prefab = Instantiate(UIPrefab, transform.position, Quaternion.identity);
        prefab.transform.parent = transform;
    }

    public void DeletePrefab()
    {
        Destroy(transform.GetChild(0).gameObject);
    }

    public void InitInfo()
    {
        if (transform.childCount >= 2)
        {
            for(int i = 0; i < transform.childCount - 1; i++)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
        }

        if (transform.childCount <= 0)
        {
            AddPrefab();
        }
    }
}
