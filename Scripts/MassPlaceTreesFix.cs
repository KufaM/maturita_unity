using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Terrain))]

public class MassPlaceTreesFix : MonoBehaviour
{
    [ContextMenu("Extract")]
    public void Extract()
    {
        Debug.Log("ExtractTreeCollidersFromTerrain::Extract");
        Terrain terrain = GetComponent<Terrain>();
        Transform[] transforms = terrain.GetComponentsInChildren<Transform>();
        for (int i = 1; i < transforms.Length; i++)
        {
            DestroyImmediate(transforms[i].gameObject);
        }
        Debug.Log("Tree prototypes count: " + terrain.terrainData.treePrototypes.Length);
        for (int i = 0; i < terrain.terrainData.treePrototypes.Length; i++)
        {
            TreePrototype tree = terrain.terrainData.treePrototypes[i];
            TreeInstance[] instances = terrain.terrainData.treeInstances.Where(x => x.prototypeIndex == i).ToArray();
            Debug.Log("Tree prototypes[" + i + "] instance count: " + instances.Length);
            for (int j = 0; j < instances.Length; j++)
            {
                //Un-normalize positions so they're in world-space
                instances[j].position = Vector3.Scale(instances[j].position, terrain.terrainData.size);
                instances[j].position += terrain.GetPosition();
                NavMeshObstacle navMeshObstacle = tree.prefab.GetComponent<NavMeshObstacle>();
                if (!navMeshObstacle)
                {
                    Debug.LogWarning("Tree with prototype[" + i + "] instance[" + j + "] did not have a NavMeshObstacle component, skipping!");
                    continue;
                }

                Vector3 primitiveScale = navMeshObstacle.size;
                GameObject obj = Instantiate(tree.prefab, instances[j].position, Quaternion.identity);
                obj.name = tree.prefab.name + j;
                if (terrain.preserveTreePrototypeLayers)
                    obj.layer = tree.prefab.layer;
                else
                    obj.layer = terrain.gameObject.layer;
                obj.transform.SetParent(terrain.transform);
                obj.isStatic = true;
            }
        }
    }
}