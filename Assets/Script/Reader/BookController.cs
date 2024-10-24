using UnityEngine;
using UnityEngine.UI;

public class BookController : MonoBehaviour
{
    public static BookData bookData;
    public Image pageDisplay;
    public Button firstPageButton;
    public Button previousPageButton;
    public Button nextPageButton;

    private int currentPageIndex = 0;

    void Start()
    {
        
    }

    public  void ShowFirstPage()
    {
        currentPageIndex = 0;
        ShowPage(currentPageIndex);
    }

    public void ShowPreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            ShowPage(currentPageIndex);
        }
    }

    public void ShowNextPage()
    {
        print(bookData);
        print(bookData.pages.Count + " !!!!!!----------page");
        if (currentPageIndex < bookData.pages.Count - 1)
        {
            currentPageIndex++;
            ShowPage(currentPageIndex);
        }
    }

    public  void ShowPage(int pageIndex)
    {
        if (bookData.pages.Count > 0 && pageIndex >= 0 && pageIndex < bookData.pages.Count)
        {
            pageDisplay.sprite = bookData.pages[pageIndex];
            print(pageIndex + "      !!!!STR");
        }
    }
}