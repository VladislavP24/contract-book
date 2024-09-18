using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_notebook;

public class NoteBookModel : ObservableObject
{
    /// <summary>
    /// Id записной книжки
    /// </summary>
    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value, nameof(Id));
    }
    private int _id;

    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName
    {
        get => _lastName;
        set => SetProperty(ref _lastName, value, nameof(LastName));
    }
    private string _lastName;

    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName
    {
        get => _firstName;
        set => SetProperty(ref _firstName, value, nameof(FirstName));
    }
    private string _firstName;

    /// <summary>
    /// Отчество
    /// </summary>
    public string SurName
    {
        get => _surName;
        set => SetProperty(ref _surName, value, nameof(SurName));
    }
    private string _surName;

    /// <summary>
    /// Номер телефона
    /// </summary>
    public string PhoneNumber
    {
        get => _phoneNumber;
        set => SetProperty(ref _phoneNumber, value, nameof(PhoneNumber));
    }
    private string _phoneNumber;


    public NoteBookModel(int id, string lastName, string firstName, string surName, string phoneNumber)
    {
        Id = id;
        LastName = lastName;
        FirstName = firstName;
        SurName = surName;
        PhoneNumber = phoneNumber;
    }

    public NoteBookModel() { }
}
