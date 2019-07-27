using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHelperDrawer : MonoBehaviour
{

    private static DebugHelperDrawer _instance;
    public static DebugHelperDrawer Instance { get { return _instance; } }

    public GameObject vectorHelperPrefab;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void SpawnVectorHelperPrefab(Vector3 vector, Vector3 origin)
    {
        GameObject helper = Instantiate(vectorHelperPrefab);
        helper.transform.position = origin;
        helper.transform.rotation = Quaternion.LookRotation(vector, Vector3.up);
        helper.transform.localScale = new Vector3(1, 1 , vector.magnitude);

    }


}
