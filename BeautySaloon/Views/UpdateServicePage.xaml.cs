using BeautySaloon.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeautySaloon.Views
{
    /// <summary>
    /// Логика взаимодействия для UpdateServicePage.xaml
    /// </summary>
    public partial class UpdateServicePage : Page
    {
        public Service Service { get; }

        public List<int> Durations { get; set; } = new();

        public List<int> Discounts { get; set; } = new();

        private List<ServicePhoto> newPhotos = new();

        public UpdateServicePage(Service service)
        {

            
            Service = service;

            for (int i = 15; i <= 420; i += 5)
            {
                Durations.Add(i);
            }

            for (int i = 0; i <= 95; i += 5)
            {
                Discounts.Add(i);
            }

            InitializeComponent();

            if (Service.Id == 0)
            {
                headerLabel.Content = "Добавление услуги";
            }
            else
            {
                headerLabel.Content = "Изменение услуги";
                Session.Instance
                    .Context
                    .Services
                    .Entry(service)
                    .Collection(s => s.ServicePhotos).Load();
            }

            DataContext = this;
        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            if (Service.Id != 0)
            {
                foreach (var photo in newPhotos)
                {
                    Service.ServicePhotos.Remove(photo);
                }
                Session.Instance.Context.Entry(Service).Reload();
            }

            NavigationService.GoBack();
        }

        private void saveChanges(object sender, RoutedEventArgs e)
        {
            if (Service.Id == 0)
            {
                Session.Instance.Context.Add(Service);
            }

            try
            {
                Session.Instance.Context.SaveChanges();
                MessageBox.Show("Данные сохранены.");
                NavigationService.GoBack();
            }
            catch
            {
                MessageBox.Show("При сохранении данных возникла ошибка!");
                if (Service.Id == 0)
                {
                    Session.Instance.Context.Remove(Service);
                }
            }
        }

        private void updateImage(object sender, RoutedEventArgs e)
        {
            // экземпляр OpenFileDialog для вызова диалогового окна
            var dialog = new OpenFileDialog
            {
                Filter = "Png Images|*.png"
            };

            // вызываем окно, проверяем, что файл был успешно выбран
            var result = dialog.ShowDialog();
            if (result != true)
            {
                return;
            }

            // генерируем случайное имя файла
            string newFilename = Guid.NewGuid().ToString().Replace("-", "") + ".png";
            // и путь для копирования
            string pathToCopy = $"Assets/Images/Services/{newFilename}";

            // выполняем копирование и записываем значение в Service.MainImagePath
            try
            {
                File.Copy(dialog.FileName, pathToCopy);
                Service.MainImagePath = newFilename;
            }
            catch
            {
                MessageBox.Show("Ошибка при копировании файла!");
            }
        }

        private void addServicePhoto(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Png Images|*.png"
            };

            var result = dialog.ShowDialog();
            if (result != true)
            {
                return;
            }

            string newFilename = Guid.NewGuid().ToString().Replace("-", "") + ".png";
            string pathToCopy = $"Assets/Images/Services/{newFilename}";

            try
            {
                File.Copy(dialog.FileName, pathToCopy);
                var photo = new ServicePhoto { Service = this.Service, PhotoPath = newFilename };
                Service.ServicePhotos.Add(photo);
                newPhotos.Add(photo);
            }
            catch
            {
                MessageBox.Show("Ошибка при копировании файла!");
            }
        }
    }
}
