using BeautySaloon.Models;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для ServiceRegistration.xaml
    /// </summary>
    public partial class ServiceRegistration : Page
    {
        public Service Service { get; set; }

        public List<Service> Services { get; set; }

        public Client Client { get; set; }

        public List<Client> Clients { get; set; }



        public ServiceRegistration(Service service)
        {
            
            Service = service;
            Services = Session.Instance.Context.Services.OrderBy(s => s.Title).ToList();
            Clients = Session.Instance.Context.Clients.ToList();
            InitializeComponent();
            serviceDatePicker.DisplayDateStart = DateTime.Today; // нельзя выбрать даты до сегодняшнего дня
            serviceDatePicker.SelectedDate = DateTime.Today; // выбран сегодняшний день
            serviceTimePicker.SelectedTime = new DateTime().AddHours(DateTime.Now.Hour + 1); // выбрано время 12:00
            
        }

        private void registration(object sender, RoutedEventArgs e)
        {
            var time = serviceTimePicker.SelectedTime.Value;
            var date = serviceDatePicker.SelectedDate.Value;

            // складываем дату и время
            var startTime = date.AddTicks(time.Ticks);

            // создаем запись
            var clientService = new ClientService
            {
                Client = this.Client,
                Service = this.Service,
                StartTime = startTime,
                Comment = comment.Text
            };

            // сохраняем ее в бд
            Session.Instance.Context.ClientServices.Add(clientService);
            try
            {
                Session.Instance.Context.SaveChanges();
                MessageBox.Show("Клиент записан!");
                NavigationService.GoBack();
            }
            catch
            {
                MessageBox.Show("Ошибка при записи клиента!");
                Session.Instance.Context.ClientServices.Remove(clientService);
            }
        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }


    }
}
