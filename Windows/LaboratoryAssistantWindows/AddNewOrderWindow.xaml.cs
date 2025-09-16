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
using System.Windows.Shapes;

namespace MedLab.Windows.LaboratoryAssistantWindows
{
    /// <summary>
    /// Логика взаимодействия для AddNewOrderWindow.xaml
    /// </summary>
    public partial class AddNewOrderWindow : Window
    {
        MedLabEntities medLabEntities = new MedLabEntities();
        // Сделать возможность выбора сразу нескольких анализов в заказ
        User selectedUser;
        Order newOrder;
        public AddNewOrderWindow(User User)
        {
            InitializeComponent();
            try
            {
                ListService.ItemsSource = medLabEntities.Services.ToList();
                selectedUser = User;
                newOrder = new Order()
                {
                    DateOfCreation = DateTime.Now,
                    IdStatusOrder = 2,
                    IdUser = selectedUser.Id
                };
                medLabEntities.Orders.Add(newOrder);
                medLabEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }    
        }

        private void AddNewService_Click(object sender, RoutedEventArgs e)
        {
            var selectedService = ListService.SelectedItem as Service;
            AddBarcode addBarcode = new AddBarcode(newOrder, selectedService);
            addBarcode.Show();
        }

    }
}
