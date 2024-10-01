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
        UpdateUI();
        CheckCompletion();
    }

    private void CheckCompletion()
    {
        if(currentPageCount >= totalPageCount)
        {
            pageCounterUI.text = "All pages obtained...";
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
