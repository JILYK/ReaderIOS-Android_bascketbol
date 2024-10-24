using UnityEngine;
using UnityEngine.UI;

public class BookLoader : MonoBehaviour
{
    public BookData bookData;
    public Image pageDisplay;
    public Image externalPageDisplay; // Новый Image для отображения страниц
    public Text nameText; // Текст для отображения названия
    public Text yearText; // Текст для отображения года выпуска
    public Button favoriteButton; // Кнопка "Добавить в избранное"
    public Sprite emptyHeartSprite; // Спрайт пустого сердца
    public Sprite filledHeartSprite; // Спрайт закрашенного сердца

    void Start()
    {
        if (bookData != null)
        {
            UpdatePageDisplay();
            UpdateBookInfo();
        }

        if (favoriteButton != null)
        {
            favoriteButton.onClick.AddListener(ToggleFavorite);
            UpdateFavoriteButtonState();
        }
    }

    public void SetBookData(BookData newBookData, Image externalImage)
    {
        bookData = newBookData;
        externalPageDisplay = externalImage;
        UpdatePageDisplay();
        UpdateBookInfo();
        UpdateFavoriteButtonState();
    }

    public void LoadBook()
    {
        if (bookData != null && bookData.pages.Count > 0)
        {
            if (externalPageDisplay != null)
            {
                BookController.bookData = bookData;
                externalPageDisplay.sprite = bookData.pages[0];
            }
        }
    }

    private void UpdatePageDisplay()
    {
        if (bookData != null && bookData.pages.Count > 0)
        {
            pageDisplay.sprite = bookData.pages[0];
        }
    }

    private void UpdateBookInfo()
    {
        if (nameText != null)
        {
            nameText.text = bookData.title;
        }
        if (yearText != null)
        {
            yearText.text = bookData.yearOfRelease.ToString();
        }
    }

    public void ToggleFavorite()
    {
        if (bookData != null)
        {
            if (BookManager.Instance.IsFavorite(bookData))
            {
                BookManager.Instance.RemoveFromFavorites(bookData);
            }
            else
            {
                BookManager.Instance.AddToFavorites(bookData);
            }
            UpdateFavoriteButtonState();
        }
    }

    public void UpdateFavoriteButtonState()
    {
        if (favoriteButton != null && bookData != null)
        {
            bool isFavorite = BookManager.Instance.IsFavorite(bookData);
            favoriteButton.image.sprite = isFavorite ? filledHeartSprite : emptyHeartSprite;
        }
    }
}
