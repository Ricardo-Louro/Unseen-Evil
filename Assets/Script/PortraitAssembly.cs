using UnityEngine;

public class PortraitAssembly : Interactive
{
    [SerializeField] private GameObject pagesVisual;

    public void Start()
    {
    }

    public override void Interact()
    {
        pagesVisual.SetActive(true);
        //FINISH GAME
        Debug.Log("Finish game");
    }
}
