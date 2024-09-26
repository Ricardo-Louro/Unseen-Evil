using UnityEngine;

public class PortraitPiece : Interactive
{
    private Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        inventory.AddTotalPage();
    }

    public override void Interact()
    {
        inventory.ObtainPage(this);
        Destroy(gameObject);
    }
}
