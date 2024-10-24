using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookManager : MonoBehaviour
{
    public static BookManager Instance; // Синглтон для доступа к BookManager
    public GameObject bookPrefab; // Префаб книги, содержащий Image и кнопку
    public Transform bookContainer; // Контейнер для размещения книг
    public BookData[] bookDataArray; // Массив данных книг
    public Image externalPageDisplay; // Внешний Image для отображения страниц
    public Text selectedBookTitleText; // Текстовое поле для отображения названия выбранной книги
    public InputField nameInputField; // Поле ввода для имени книги
    public InputField yearInputField; // Поле ввода для года книги
    public Button searchButton; // Кнопка "Найти"
    public Button showFavoritesButton; // Кнопка "Показать избранное"
    public Sprite emptyHeartSprite; // Спрайт пустого сердца
    public Sprite filledHeartSprite; // Спрайт закрашенного сердца

    private List<string> favoriteBookTitles; // Список названий избранных книг

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadFavorites();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GenerateBooks();
        if (searchButton != null)
        {
            searchButton.onClick.AddListener(SearchBooks); // Привязываем метод поиска к кнопке
        }
        if (showFavoritesButton != null)
        {
            showFavoritesButton.onClick.AddListener(ShowFavorites); // Привязываем метод отображения избранного к кнопке
        }
    }

    public void GenerateBooks()
    {
        foreach (var bookData in bookDataArray)
        {
            GameObject newBook = Instantiate(bookPrefab, bookContainer);
            newBook.SetActive(true);
            BookLoader bookLoader = newBook.GetComponent<BookLoader>();

            // Найдем компоненты текста внутри префаба
            Text[] texts = newBook.GetComponentsInChildren<Text>();
            foreach (var text in texts)
            {
                if (text.name == "NameText")
                {
                    bookLoader.nameText = text;
                }
                else if (text.name == "YearText")
                {
                    bookLoader.yearText = text;
                }
            }

            // Устанавливаем данные книги и внешний Image
            bookLoader.SetBookData(bookData, externalPageDisplay);

            Button button = newBook.GetComponentInChildren<Button>();
            button.onClick.AddListener(() => OnBookButtonClicked(bookLoader));
        }
    }

    public void OnBookButtonClicked(BookLoader bookLoader)
    {
        bookLoader.LoadBook();
        if (selectedBookTitleText != null)
        {
            selectedBookTitleText.text = bookLoader.bookData.title;
        }
        Debug.Log("Загружена книга: " + bookLoader.bookData.title);
    }

    public void ToMenu()
    {
        foreach (Transform book in bookContainer)
        {
            bookContainer.gameObject.SetActive(true);
            book.gameObject.SetActive(true);
        }
    }
    public void SearchBooks()
    {
        if (nameInputField == null || yearInputField == null || bookContainer == null)
        {
            Debug.LogError("One or more input fields or book container are not assigned in the inspector.");
            return;
        }

        string searchName = nameInputField.text.ToLower();
        string searchYear = yearInputField.text;

        foreach (Transform book in bookContainer)
        {
            BookLoader bookLoader = book.GetComponent<BookLoader>();
            if (bookLoader == null || bookLoader.bookData == null)
            {
                continue;
            }

            bool nameMatches = string.IsNullOrEmpty(searchName) || bookLoader.bookData.title.ToLower().Contains(searchName);
            bool yearMatches = string.IsNullOrEmpty(searchYear) || bookLoader.bookData.yearOfRelease.ToString() == searchYear;

            book.gameObject.SetActive(nameMatches && yearMatches);
        }
    }

    public void AddToFavorites(BookData bookData)
    {
        if (bookData != null && !favoriteBookTitles.Contains(bookData.title))
        {
            favoriteBookTitles.Add(bookData.title);
            SaveFavorites();
        }
    }

    public void RemoveFromFavorites(BookData bookData)
    {
        if (bookData != null && favoriteBookTitles.Contains(bookData.title))
        {
            favoriteBookTitles.Remove(bookData.title);
            SaveFavorites();
        }
    }

    public bool IsFavorite(BookData bookData)
    {
        return bookData != null && favoriteBookTitles.Contains(bookData.title);
    }

    public void LoadFavorites()
    {
        favoriteBookTitles = new List<string>();

        if (PlayerPrefs.HasKey("Favorites"))
        {
            string[] savedFavorites = PlayerPrefs.GetString("Favorites").Split(';');
            foreach (var title in savedFavorites)
            {
                if (!string.IsNullOrEmpty(title))
                {
                    favoriteBookTitles.Add(title);
                }
            }
        }
    }

    public void SaveFavorites()
    {
        string favorites = string.Join(";", favoriteBookTitles);
        PlayerPrefs.SetString("Favorites", favorites);
        PlayerPrefs.Save();
    }

    public void ShowFavorites()
    {
        foreach (Transform book in bookContainer)
        {
            BookLoader bookLoader = book.GetComponent<BookLoader>();
            if (bookLoader != null && bookLoader.bookData != null)
            {
                book.gameObject.SetActive(favoriteBookTitles.Contains(bookLoader.bookData.title));
            }
            else
            {
                book.gameObject.SetActive(false);
            }
        }
    }
}
