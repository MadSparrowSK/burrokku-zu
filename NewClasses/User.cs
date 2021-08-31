using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_1._0
{
    /// <summary>
    /// Класс, хранящий в себе данные для одного пользователя
    /// </summary>
    public class User
    {
        /// <summary>
        /// Пароль для входа в аккаунт
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// ФИО пользователя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Номер варианта пользователя
        /// </summary>
        public int Option { get; set; }
        /// <summary>
        /// Флаг, указывающий является ли этот пользователь активным
        /// </summary>
        public bool Mark { get; set; }
        /// <summary>
        /// Все сохранения 
        /// </summary>
        public List<Diagramm> Saves { get; set;}
        public User()
        {

        }

        public User(string name, string password, int option, List<Diagramm> saves)
        {
            Name = name;
            Password = password;
            Option = option;
            Saves = saves;
            Mark = false;
        }

    }
    public static class AllUsers
    {
        public static List<User> Users { get; set; }
        static AllUsers()
        {
            Users = new List<User>();
        }
        /// <summary>
        /// Находит и возвращает указанного пользователя
        /// </summary>
        /// <param name="SelectedUser"></param>
        /// <returns></returns>
        public static User FindUser(User SelectedUser)
        {
            foreach (User user in Users)
            {
                if ((user.Name == SelectedUser.Name) && (user.Password == SelectedUser.Password) && (user.Option == SelectedUser.Option))
                {
                    return user;
                }
            }
            return null;
        }
        /// <summary>
        /// Находит и возвращает указанного пользователя
        /// </summary>
        /// <param name="SelectedUser"></param>
        /// <returns></returns>
        public static User FindUser(string name, string password)
        {
            foreach (User user in Users)
            {
                if ((user.Name == name) && (user.Password == password))
                {
                    return user;
                }
            }
            return null;
        }
        /// <summary>
        /// Помечает пользователя как активного
        /// </summary>
        /// <param name="SelectedUser"></param>
        public static void MarkUser(User SelectedUser)
        {
            foreach (User user in Users)
            {
                if((user.Name == SelectedUser.Name) && (user.Password == SelectedUser.Password) && (user.Option == SelectedUser.Option))
                {
                    user.Mark = true;
                }
            }
        }
        /// <summary>
        /// Убирает метку активности с выбранного на данный момент пользователя
        /// </summary>
        public static void RemoveMark()
        {
            foreach (User user in Users)
            {
                if (user.Mark == true)
                {
                    user.Mark = false;
                }
            }
        }
        /// <summary>
        /// Находит и возвращает активного пользователя
        /// </summary>
        /// <returns></returns>
        public static User FindeMarkedUser()
        {
            foreach (User user in Users)
            {
                if (user.Mark == true)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
