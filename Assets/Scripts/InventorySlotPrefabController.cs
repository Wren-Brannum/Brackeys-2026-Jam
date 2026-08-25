using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventorySlotPrefabController : MonoBehaviour
{
    public bool isEnabled = true;

    // Update is called once per frame
    void Update()
    {
        if(isEnabled && Mouse.current.leftButton.wasPressedThisFrame && GetComponent<RectTransform>().rect.Contains(Input.mousePosition))
        {
            onClick();
            print("Inventory clicked");
        }
    }

    private void onClick()
    {
        
    }
}
