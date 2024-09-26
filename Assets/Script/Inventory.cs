using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pageCounterUI;

    private Dictionary<PortraitPiece, bool> pages = new Dictionary<PortraitPiece, bool>();
    private int currentPageCount = 0;
    private int totalPageCount;

    public void ObtainPage(PortraitPiece piece)
    {
        pages[piece] = true;
        currentPageCount += 1;
        CheckCompletion();
        UpdateUI();
    }

    private void CheckCompletion()
    {
        if(currentPageCount >= totalPageCount)
        {
            Debug.Log("All pages obtained");
            FindObjectOfType<PortraitAssembly>().enabled = true;
        }
    }
    
    private void UpdateUI()
    {
        pageCounterUI.text = "Pages: " + currentPageCount + "/" + totalPageCount;
    }

    public void AddTotalPage()
    {
        totalPageCount += 1;
        UpdateUI();
    }
}
