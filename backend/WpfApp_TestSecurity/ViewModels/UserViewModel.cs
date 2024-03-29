﻿using System;
using System.Collections.Generic;

namespace WpfApp_TestSecurity.ViewModels
{
    public class UserViewModel: BaseViewModel<UserViewModel>
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _middleName;
        private bool _status;

        /// <summary>
        /// Дополнительный идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор в БД
        /// </summary>
        public int IdMember { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login
        {
            get => Name;
            set
            {
                Name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName
        {
            get { return _middleName; }
            set
            {
                _middleName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Статус - Активен/Заблокироан
        /// </summary>
        public bool Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Строка данных, которая передаётся хеш-функции вместе с паролем
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Дата последней активности пользователя
        /// </summary>
        public DateTime? LastActivityDate { get; set; }
    }
}