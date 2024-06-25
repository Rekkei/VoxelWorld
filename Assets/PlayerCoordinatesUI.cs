using UnityEngine;
using TMPro;

public class PlayerCoordinatesUI : MonoBehaviour
{
    public Transform player; 
    public TextMeshProUGUI coordinatesText; 
    public LayerMask cubeLayer; 
    public float raycastDistance = 1f; 

    void Update()
    {
        if (player != null && coordinatesText != null)
        {
            
            Vector3 playerPosition = player.position;

            
            int playerX = Mathf.RoundToInt(playerPosition.x);
            int playerY = Mathf.RoundToInt(playerPosition.y);
            int playerZ = Mathf.RoundToInt(playerPosition.z);

            RaycastHit hit;
            if (Physics.Raycast(player.position, Vector3.down, out hit))
            {

                int cubeX = Mathf.RoundToInt(hit.point.x);
                int cubeY = Mathf.RoundToInt(hit.point.y);
                int cubeZ = Mathf.RoundToInt(hit.point.z);

               
                coordinatesText.text = $"<color=red>Player: ({playerX}, {playerY}, {playerZ})\nCube Index: ({cubeX}, {cubeY}, {cubeZ})</color>";
            }
            else
            {
                coordinatesText.text = $"<color=red>Player: ({playerX}, {playerY}, {playerZ})\nNo cube found beneath the player</color>";
            }
            
        }
    }
}
