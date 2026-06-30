using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public GameObject roadPrefab;
    public Transform[] points;

    void Start()
    {
        for (int i = 0; i < points.Length - 1; i++)
        {
            Vector3 start = points[i].position;
            Vector3 end = points[i + 1].position;

            Vector3 direction = end - start;
            float distance = direction.magnitude;

            GameObject road = Instantiate(roadPrefab, start, Quaternion.identity);

            road.transform.LookAt(end);
            road.transform.localScale = new Vector3(1, 1, distance);
        }
    }
}