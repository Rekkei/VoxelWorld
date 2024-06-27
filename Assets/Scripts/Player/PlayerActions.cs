using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Temporary")]
    public GameObject destroyEffectPrefab;
    public GameObject selectedBlock = null;

    [SerializeField] private World world;
    private static bool canAct = true;

    private void Start()
    {
        selectedBlock = null;
    }

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
