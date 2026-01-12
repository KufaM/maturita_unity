using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public Terrain terrain;

    void Start()
    {
        // Get terrain size and position
        Vector3 terrainSize = terrain.terrainData.size;
        Vector3 terrainPos = terrain.transform.position;

        // Generate random position within terrain bounds
        float randomX = Random.Range(terrainPos.x, terrainPos.x + terrainSize.x);
        float randomZ = Random.Range(terrainPos.z, terrainPos.z + terrainSize.z);

        // Get height at the random position
        float height = terrain.SampleHeight(new Vector3(randomX, 0, randomZ)) + terrainPos.y;
        // Offset
        float heightoffset = 1;
        height += heightoffset;

        // Set player position
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.MovePosition(new Vector3(randomX, height, randomZ));
        }
        else
        {
            transform.position = new Vector3(randomX, height, randomZ);
        }
    }
}
