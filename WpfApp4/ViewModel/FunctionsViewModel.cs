﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
<<<<<<< HEAD
=======
using System.Windows.Markup;
using WpfApp4.ViewModel;
>>>>>>> gfgd

namespace WpfApp4.View
{
    public class FunctionsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<UserModel> _users;
        private UserModel _selectedUser;
        private int _inputUserId;
        private string _newUsername;
        private string _newPassword;


        public ObservableCollection<UserModel> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public UserModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                if (_selectedUser != value)
                {
                    _selectedUser = value;
                    OnPropertyChanged(nameof(SelectedUser));
                    (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        public int InputUserId
        {
            get { return _inputUserId; }
            set
            {
                _inputUserId = value;
                OnPropertyChanged(nameof(InputUserId));
            }
        }

        public string NewUsername
        {
            get { return _newUsername; }
            set
            {
                _newUsername = value;
                OnPropertyChanged(nameof(NewUsername));
            }
        }

        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }

        public ICommand EditCommand { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand LightThemeCommand { get; private set; }
        public ICommand DarkThemeCommand { get; private set; }



        public FunctionsViewModel()
        {
            Users = new ObservableCollection<UserModel>();
            EditCommand = new RelayCommand(Edit, CanEdit);
            BackCommand = new RelayCommand(Back);
            DeleteCommand = new RelayCommand(Delete, CanDelete);
            LightThemeCommand = new RelayCommand(LightTheme);
            DarkThemeCommand = new RelayCommand(DarkTheme);
            LoadUsersFromDatabase();
            SelectedUser = new UserModel(); // Проверьте, есть ли у вас конструктор по умолчанию
        }

        private void LoadUsersFromDatabase()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-KDP83D4;Initial Catalog=User;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT id, login, password FROM users";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Users.Add(new UserModel
                            {
                                UserID = Convert.ToInt32(reader["id"]),
                                Username = reader["login"].ToString(),
                                Password = reader["password"].ToString(),
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных из БД: {ex.Message}");
            }
        }


        private void Edit(object parameter)
        {
            try
            {
<<<<<<< HEAD
                    string connectionString = "Data Source=DESKTOP-D3VB68L;Initial Catalog=Users;Integrated Security=True";
=======
                    string connectionString = "Data Source=DESKTOP-KDP83D4;Initial Catalog=User;Integrated Security=True";
>>>>>>> gfgd
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                    string updateQuery = "UPDATE users SET login = @NewUsername, password = @NewPassword WHERE id = @UserID";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@NewUsername", NewUsername);
                    updateCommand.Parameters.AddWithValue("@NewPassword", NewPassword);
                    updateCommand.Parameters.AddWithValue("@UserID", SelectedUser.UserID);

                    int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Пользователь успешно обновлен!");
                        DataWindow data = new DataWindow();
                        data.Show();
                        Application.Current.Windows[0].Close();
                        LoadUsersFromDatabase();
                        }
                        else
                        {
                            MessageBox.Show("Не удалось обновить пользователя.");
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении пользователя: {ex.Message}");
                }
        }
            

        private void Delete(object parameter)
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-KDP83D4;Initial Catalog=User;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM users WHERE id = @UserID";
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@UserID", SelectedUser.UserID);

                    int rowsAffected = deleteCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Пользователь успешно удален!");
                        Users.Remove(SelectedUser); // Удаление из коллекции после удаления из БД
                        SelectedUser = null; // Сброс выбранного пользователя после удаления
                        DataWindow data = new DataWindow();
                        data.Show();
                        Application.Current.Windows[0].Close();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось удалить пользователя.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении пользователя: {ex.Message}");
            }
        }

        private bool CanDelete(object parameter)
        {
            return SelectedUser != null; // Разрешить удаление, если пользователь выбран в ComboBox
        }
        private void Back(object parameter)
        {
            DataWindow DataWindow = new DataWindow();
            DataWindow.Show();
            Application.Current.Windows[0].Close();
        }
        private void LightTheme(object parameter)
        {
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("Themes/Light.xaml", UriKind.Relative) });
        }

        private void DarkTheme (object paramter)
        {
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("Themes/Dark.xaml", UriKind.Relative) });
        }
        private bool CanEdit(object parameter)
        {
            return !string.IsNullOrWhiteSpace(NewUsername) && !string.IsNullOrWhiteSpace(NewPassword);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
