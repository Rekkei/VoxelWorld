using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Keybinds")]
    [SerializeField] KeyCode attackKey = KeyCode.Mouse0;
    [SerializeField] KeyCode placeKey = KeyCode.Mouse1;

    [Header("Highlight")]
    [SerializeField] private Material highlightMaterial;
    private GameObject highlightedObject;
    private Material originalMaterial;

    [Header("Temporary")]
    public GameObject destroyEffectPrefab;
    public GameObject selectedBlock = null;

    private static bool canAct = true;

    private void Start()
    {
        selectedBlock = null; // Initially, no block is selected
    }

    void Update()
    {
        if (canAct)
        {
            HandleGameplay();
        }
    }

    public static void EnableActions(bool enable)
    {
        canAct = enable;
    }

    private void HandleGameplay()
    {
        Camera mainCamera = Camera.main;
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        HandleHighlighting();

        if (Physics.Raycast(ray, out hit, 100.0f) && Input.GetKeyDown(attackKey))
        {
            DestroyObject(hit.transform.gameObject);
        }
        if (Physics.Raycast(ray, out hit, 100.0f) && Input.GetKeyDown(placeKey))
        {
            PlaceObject(hit.point, hit.normal);
        }
    }

    private void DestroyObject(GameObject obj)
    {
        if (destroyEffectPrefab != null)
        {
            GameObject effect = Instantiate(destroyEffectPrefab, obj.transform.position, obj.transform.rotation);
            ParticleSystem particleSystem = effect.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.Play();
            }
            Destroy(effect, particleSystem.main.duration);
        }
        Destroy(obj);
    }

    private void PlaceObject(Vector3 position, Vector3 normal)
    {
        if (selectedBlock == null) return;

        Vector3 placePosition = position + normal * 0.5f;
        placePosition = new Vector3(
                Mathf.Round(placePosition.x),
                Mathf.Round(placePosition.y),
                Mathf.Round(placePosition.z)
            );
        Instantiate(selectedBlock, placePosition, Quaternion.identity);
    }

    private void HandleHighlighting()
    {
        Camera mainCamera = Camera.main;
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            HighlightObject(hit.collider.gameObject);
            return;
        }
        ClearHighlight();
    }

    private void HighlightObject(GameObject obj)
    {
        if (highlightedObject != obj)
        {
            ClearHighlight();
            highlightedObject = obj;
            Renderer renderer = highlightedObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                originalMaterial = renderer.material;
                renderer.material = highlightMaterial;
            }
        }
    }

    private void ClearHighlight()
    {
        if (highlightedObject != null)
        {
            Renderer renderer = highlightedObject.GetComponent<Renderer>();
            if (renderer != null && originalMaterial != null)
            {
                renderer.material = originalMaterial;
            }
            highlightedObject = null;
            originalMaterial = null;
        }
    }
}
