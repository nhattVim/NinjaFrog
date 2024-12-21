using UnityEditor;
using UnityEngine;

public class SyncPrefabNames : MonoBehaviour
{
    [MenuItem("Tools/Sync Prefab Names")]
    public static void SyncNames()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        int count = 0;

        foreach (GameObject obj in allObjects)
        {
            GameObject prefab = PrefabUtility.GetCorrespondingObjectFromSource(obj);

            if (prefab != null)
            {
                if (obj.name != prefab.name)
                {
                    obj.name = prefab.name;
                    count++;
                }
            }
        }

        Debug.Log($"Đã đồng bộ {count} đối tượng với tên Prefab.");
    }
}
