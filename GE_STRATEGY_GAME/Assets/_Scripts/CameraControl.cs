using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public LayerMask baseArea;
    public BuildingManager draggedBuilding;
    GameManager gm;

    void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
    }

    private void Update()
    {
        if(draggedBuilding != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, baseArea))
            {
                draggedBuilding.transform.position = hit.point;
            }


            // dragged building null means its been placed successfully
            if(Input.GetMouseButtonDown(0))
            {
                Bounds draggedBuildingBounds
                    = draggedBuilding.GetComponent<BuildingManager>().basePlate.bounds;

                Vector3 boundsArea = new Vector3(draggedBuildingBounds.extents.x, draggedBuildingBounds.extents.y, draggedBuildingBounds.extents.z);

                if (Physics.CheckBox(draggedBuilding.transform.position, boundsArea, Quaternion.identity, baseArea))
                {
                    //draggedBuilding.GetComponent<BuildingManager>().sourceButton.GetComponent<BuildBuilding>().ResetBuildingValues();
                    draggedBuilding.basePlate.gameObject.layer = LayerMask.NameToLayer("Building"); // Set layer to "Building"
                    draggedBuilding = null;
                }
                else
                {
                    print("Terrain preventing building placement");
                }
            }
            
        }
    }
}
