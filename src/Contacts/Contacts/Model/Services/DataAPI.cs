﻿namespace Contacts.Model.Services
{
    public class DataAPI
    {
        /// <summary>
        ///     Возвращает и задаёт имя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Возвращает и задаёт фамилию.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Возвращает и задаёт электронную почту.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Возвращает и задаёт номер телефона.
        /// </summary>
        public string Phone { get; set; }
    }
}