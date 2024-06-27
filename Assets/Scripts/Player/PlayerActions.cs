using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Temporary")]
    public GameObject destroyEffectPrefab;

    [SerializeField] private World world;
    private static bool canAct = true;

    void Update()
    {
        if (canAct)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                world.WorldManipulation();
            }
        }
    }

    public static void EnableActions(bool enable)
    {
        canAct = enable;
    }
}
