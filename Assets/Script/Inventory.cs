using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pageCounterUI;

    private Dictionary<PortraitPiece, bool> pages = new Dictionary<PortraitPiece, bool>();
    private int currentPageCount = 0;
    private int totalPageCount;

    [SerializeField] private GameObject secondScarecrow;

    public void ObtainPage(PortraitPiece piece)
    {
        pages[piece] = true;
        currentPageCount += 1;
        UpdateUI();
        CheckForScarecrowActivation();
        CheckCompletion();
    }

    private void CheckCompletion()
    {
        if(currentPageCount >= totalPageCount)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

    private void CheckForScarecrowActivation()
    {
        if(currentPageCount == 3)
        {
            secondScarecrow.SetActive(true);
        }
    }
}
