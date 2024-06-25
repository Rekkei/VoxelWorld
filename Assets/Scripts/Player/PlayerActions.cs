using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
    public GameObject[] blockPrefabs;

    public GameObject selectedBlock;

    void Update()
    {

        HandleHighlighting();

        if (Input.GetKeyDown(attackKey))
        {
            DestroyObject();
        }
        if (Input.GetKeyDown(placeKey))
        {
            PlaceObject();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectActionBar(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectActionBar(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectActionBar(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectActionBar(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectActionBar(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectActionBar(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectActionBar(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SelectActionBar(7);
        }

    }

    private void SelectActionBar(int i)
    {
        selectedBlock = blockPrefabs[i];
        Debug.Log(selectedBlock.name);
    }

    private void DestroyObject()
    {
        Camera mainCamera = Camera.main;

        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            Destroy(hit.transform.gameObject);
        }
    }

    private void PlaceObject()
    {
        Camera mainCamera = Camera.main;

        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            Vector3 placePosition = hit.point + hit.normal * 0.5f;

            placePosition = new Vector3(
                Mathf.Round(placePosition.x),
                Mathf.Round(placePosition.y),
                Mathf.Round(placePosition.z)
            );

            Instantiate(selectedBlock, placePosition, Quaternion.identity);
        }
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
