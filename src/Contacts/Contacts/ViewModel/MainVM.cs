﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Contacts.Model;
using Contacts.Model.Services;

namespace Contacts.ViewModel
{
    /// <summary>
    ///     ViewModel для окна MainWindow.
    /// </summary>
    public class MainVM : INotifyPropertyChanged
    {
        /// <summary>
        ///     Команда добавления контакта.
        /// </summary>
        private readonly RelayCommand _addCommand;

        /// <summary>
        ///     Команда принятия изменений.
        /// </summary>
        private readonly RelayCommand _applyCommand;

        /// <summary>
        ///     Команда редактирования контакта.
        /// </summary>
        private readonly RelayCommand _editCommand;

        /// <summary>
        ///     Команда генерации случайного контакта.
        /// </summary>
        private readonly RelayCommand _randomizeCommand;

        /// <summary>
        ///     Команда удаления контакта.
        /// </summary>
        private readonly RelayCommand _removeCommand;

        /// <summary>
        ///     Хранит булевое значение доступности кнопки добавления.
        /// </summary>
        private bool _isEnabledAddButton;

        /// <summary>
        ///     Хранит булевое значение доступности кнопки редактирования.
        /// </summary>
        private bool _isEnabledEditButton;

        /// <summary>
        ///     Хранит булевое значение доступности кнопки генерации контакта.
        /// </summary>
        private bool _isEnabledRandomizeButton;

        /// <summary>
        ///     Хранит булевое значение доступности кнопки удаления.
        /// </summary>
        private bool _isEnabledRemoveButton;

        /// <summary>
        ///     Хранит булевое значение доступности редактирования текстовых полей.
        /// </summary>
        private bool _isReadOnlyTextBoxes;

        /// <summary>
        ///     Хранит булевое значение видимости кнопки принятия изменений.
        /// </summary>
        private bool _isVisibilityApplyButton;

        /// <summary>
        ///     Текущий контакт.
        /// </summary>
        private ContactVM _selectedContact;

        /// <summary>
        ///     Создаёт экземпляр класса <see cref="MainVM" />.
        /// </summary>
        public MainVM()
        {
            Contacts = ContactSerializer.Deserialize(Path);
            _editCommand = new RelayCommand(EditContact);
            _addCommand = new RelayCommand(AddContact);
            _removeCommand = new RelayCommand(RemoveContact);
            _randomizeCommand = new RelayCommand(RandomizeContact);
            _applyCommand = new RelayCommand(ApplyChangesContact);
            IsReadOnlyTextBoxes = true;
            IsVisibilityApplyButton = false;
            SetEnabled(true, false, false, true);
        }

        /// <summary>
        ///     Возвращает и задаёт путь сериализации. По умолчанию - папка "Мои документы".
        /// </summary>
        public string Path { get; set; } =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            + @"\Contacts\contacts.json";

        /// <summary>
        ///     Возвращает и задаёт коллекцию контактов.
        /// </summary>
        public ObservableCollection<ContactVM> Contacts { get; set; }

        /// <summary>
        ///     Возвращает и задает исходную версию редактируемого контакта.
        /// </summary>
        public ContactVM Buffer { get; set; }

        /// <summary>
        ///     Возвращает и задает текущий контакт.
        /// </summary>
        public ContactVM SelectedContact
        {
            get => _selectedContact;
            set
            {
                if (Buffer != null && Contacts.IndexOf(SelectedContact) != -1)
                {
                    Contacts[Contacts.IndexOf(SelectedContact)] = Buffer;
                    Buffer = null;
                }

                _selectedContact = value;
                IsVisibilityApplyButton = false;
                IsReadOnlyTextBoxes = true;
                if (_selectedContact == null)
                    SetEnabled(true, false, false, true);
                else
                    SetEnabled(true, true, true, true);
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Возвращает команду генерации контакта.
        /// </summary>
        public ICommand RandomizeCommand => _randomizeCommand;

        /// <summary>
        ///     Возвращает команду добавления контакта.
        /// </summary>
        public ICommand AddCommand => _addCommand;

        /// <summary>
        ///     Возвращает команду принятия изменений.
        /// </summary>
        public ICommand ApplyCommand => _applyCommand;

        /// <summary>
        ///     Возвращает команду редактирования контакта.
        /// </summary>
        public ICommand EditCommand => _editCommand;

        /// <summary>
        ///     Возвращает команду удаления контакта.
        /// </summary>
        public ICommand RemoveCommand => _removeCommand;

        /// <summary>
        ///     Возвращает и задаёт значение доступности редактирования текстовых полей.
        /// </summary>
        public bool IsReadOnlyTextBoxes
        {
            get => _isReadOnlyTextBoxes;
            set
            {
                _isReadOnlyTextBoxes = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Возвращает и задаёт значение доступности кнопки добавления.
        /// </summary>
        public bool IsEnabledAddButton
        {
            get => _isEnabledAddButton;
            set
            {
                _isEnabledAddButton = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Возвращает и задаёт значение доступности кнопки удаления.
        /// </summary>
        public bool IsEnabledRemoveButton
        {
            get => _isEnabledRemoveButton;
            set
            {
                _isEnabledRemoveButton = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Возвращает и задаёт значение доступности кнопки редактирования.
        /// </summary>
        public bool IsEnabledEditButton
        {
            get => _isEnabledEditButton;
            set
            {
                _isEnabledEditButton = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Возвращает и задаёт значение доступности кнопки генерации контакта.
        /// </summary>
        public bool IsEnabledRandomizeButton
        {
            get => _isEnabledRandomizeButton;
            set
            {
                _isEnabledRandomizeButton = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Возвращает и задаёт значение видимости кнопки принятия изменений.
        /// </summary>
        public bool IsVisibilityApplyButton
        {
            get => _isVisibilityApplyButton;
            set
            {
                _isVisibilityApplyButton = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Событие изменения свойства.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Вызывает редактирование нового экземпляра класса <see cref="ContactVM" />.
        /// </summary>
        private void AddContact()
        {
            SelectedContact = null;
            SelectedContact = new ContactVM(new Contact());
            IsVisibilityApplyButton = true;
            IsReadOnlyTextBoxes = false;
            SetEnabled(false, false, false, false);
        }

        /// <summary>
        ///     Генерирует случайный контакт.
        /// </summary>
        private void RandomizeContact()
        {
            var contact = ContactFactory.Randomize();
            Contacts.Add(new ContactVM(contact));
            ContactSerializer.Serialize(Contacts, Path);
        }

        /// <summary>
        ///     Вызывает редактирования текущего контакта.
        /// </summary>
        private void EditContact()
        {
            Buffer = (ContactVM)SelectedContact.Clone();
            IsReadOnlyTextBoxes = false;
            IsVisibilityApplyButton = true;
            SetEnabled(false, false, false, false);
        }

        /// <summary>
        ///     Удаляет текущий контакт.
        /// </summary>
        private void RemoveContact()
        {
            if (SelectedContact == null) return;
            var index = Contacts.IndexOf(SelectedContact);
            Contacts.RemoveAt(index);
            if (Contacts.Count == 0)
                SelectedContact = null;
            else if (index == Contacts.Count)
                SelectedContact = Contacts[index - 1];
            else
                SelectedContact = Contacts[index];
            ContactSerializer.Serialize(Contacts, Path);
        }

        /// <summary>
        ///     Принимает изменения редактирования контакта.
        /// </summary>
        private void ApplyChangesContact()
        {
            if (!Contacts.Contains(SelectedContact)) Contacts.Add(SelectedContact);
            IsVisibilityApplyButton = false;
            IsReadOnlyTextBoxes = true;
            Buffer = null;
            SetEnabled(true, true, true, true);
            ContactSerializer.Serialize(Contacts, Path);
        }

        /// <summary>
        ///     Устанавливает значения доступности кнопок.
        /// </summary>
        /// <param name="addButton">Кнопка добавления контакта.</param>
        /// <param name="removeButton">Кнопка удаления контакта.</param>
        /// <param name="editButton">Кнопка редактирования контакта.</param>
        /// <param name="randomizeButton">Кнопка генерации контакта.</param>
        private void SetEnabled(
            bool addButton,
            bool removeButton,
            bool editButton,
            bool randomizeButton)
        {
            IsEnabledAddButton = addButton;
            IsEnabledRemoveButton = removeButton;
            IsEnabledEditButton = editButton;
            IsEnabledRandomizeButton = randomizeButton;
        }

        /// <summary>
        ///     При вызове зажигает событие <see cref="PropertyChanged" />.
        /// </summary>
        /// <param name="propertyName">Имя свойства, вызвавшего метод.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}