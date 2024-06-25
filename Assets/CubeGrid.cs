using UnityEngine;

public class CubeGrid : MonoBehaviour
{
    public int gridSizeX = 5; 
    public int gridSizeZ = 5; 
    public GameObject cubePrefab; 

    private Transform player; 

    void Start()
    {
       
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                Vector3 position = new Vector3(x, 0f, z);
                Instantiate(cubePrefab, position, Quaternion.identity, transform);
            }
        }

        
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(player.position, Vector3.down, out hit))
        {
            
            int xIndex = Mathf.RoundToInt(hit.point.x);
            int yIndex = Mathf.RoundToInt(hit.point.y);
            int zIndex = Mathf.RoundToInt(hit.point.z);

            
            Debug.Log("Player is on cube at index: " + xIndex + ", " + yIndex + ", " + zIndex);
        }
    }
}
