using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace contact_notebook;

public class NoteBookViewModel : ObservableObject
{
    public NoteBookViewModel()
    {
        _dbContext = new NoteBookDBContext();
        AddNoteBook = new RelayCommand(AddNoteBookCommand);
        DeleteNoteBook = new RelayCommand<NoteBookModel>(DeleteNoteBookCommand);
        ClearAllNoteBook = new RelayCommand(ClearAllNoteBookCommand);
        SearchNoteBook = new RelayCommand(SearchNoteBookCommand);
        SaveChangesToDB = new RelayCommand(SaveChangesToDBCommand);
        LoadNoteBooks();
    }

    private readonly NoteBookDBContext _dbContext;
    public ICommand AddNoteBook { get; set; }
    public ICommand DeleteNoteBook { get; set; }
    public ICommand ClearAllNoteBook { get; set; }
    public ICommand SearchNoteBook { get; set; }
    public ICommand SaveChangesToDB { get; set; }

    /// <summary>
    /// Коллекция записной книжки
    /// </summary>
    public ObservableCollection<NoteBookModel> DynamicNoteBookModelCollection
    {
        get => _dynamicNoteBookModelCollection;
        set => SetProperty(ref _dynamicNoteBookModelCollection, value, nameof(DynamicNoteBookModelCollection));
    }
    private ObservableCollection<NoteBookModel> _dynamicNoteBookModelCollection = new();

    /// <summary>
    /// Выбранная строчка в записной книжке
    /// </summary>
    public NoteBookModel CurSelectedNoteBook
    {
        get => _curSelectedNoteBook;
        set => SetProperty(ref _curSelectedNoteBook, value, nameof(CurSelectedNoteBook));
    }
    private NoteBookModel _curSelectedNoteBook;

    /// <summary>
    /// Введенный текст в строку поиска
    /// </summary>
    public string EnteredText
    {
        get => _enteredText;
        set => SetProperty(ref _enteredText, value, nameof(EnteredText));
    }
    private string _enteredText;

    /// <summary>
    /// Добавление строки в записную книжку
    /// </summary>
    private async void AddNoteBookCommand()
    {
        var newNoteBook = new NoteBookModel
        {
            LastName = "",
            FirstName = "",
            SurName = "",
            PhoneNumber = ""
        };

        try
        {
            await _dbContext.notebook.AddAsync(newNoteBook);
            await _dbContext.SaveChangesAsync();
            DynamicNoteBookModelCollection.Add(newNoteBook);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка добавления записи: {ex.Message}");
        }
    }


    /// <summary>
    /// Удаление из строки записной книжки
    /// </summary>
    private async void DeleteNoteBookCommand(NoteBookModel? noteBookModel)
    {
        if (noteBookModel == null)
            return;

        try
        {
            _dbContext.notebook.Remove(noteBookModel);
            await _dbContext.SaveChangesAsync();
            DynamicNoteBookModelCollection.Remove(noteBookModel);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка удаления записи: {ex.Message}");
        }
    }


    /// <summary>
    /// Очищение всей таблицы
    /// </summary>
    private async void ClearAllNoteBookCommand()
    {
        try
        {
            _dbContext.notebook.RemoveRange(_dbContext.notebook);
            await _dbContext.SaveChangesAsync();
            DynamicNoteBookModelCollection.Clear();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка очищения всей таблицы: {ex.Message}");
        }
    }

    /// <summary>
    /// Поиск по таблице 
    /// </summary>
    private async void SearchNoteBookCommand()
    {
        try
        {
            var noteBooks = await _dbContext.notebook.ToListAsync();
            var filteredNoteBooks = noteBooks.Where(n => string.IsNullOrEmpty(EnteredText) ||
                                                         n.FirstName.Contains(EnteredText) ||
                                                         n.LastName.Contains(EnteredText) ||
                                                         n.SurName.Contains(EnteredText) ||
                                                         n.PhoneNumber.Contains(EnteredText)).ToList();

            DynamicNoteBookModelCollection.Clear();
            foreach (var noteBook in filteredNoteBooks)
            {
                DynamicNoteBookModelCollection.Add(noteBook);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка ввода записи: {ex.Message}");
        }
    }

    private async void SaveChangesToDBCommand()
    {
        await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Загрузка данных из базы данных
    /// </summary>
    private async void LoadNoteBooks()
    {
        try
        {
            var noteBooks = await _dbContext.notebook.ToListAsync();
            DynamicNoteBookModelCollection = new ObservableCollection<NoteBookModel>(noteBooks);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки данных: {ex.Message}");
        }
    }
}
