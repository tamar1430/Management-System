using BlApi;
using BO;
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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for AddUpdateEngineer.xaml
    /// </summary>
    public partial class AddUpdateEngineer : Window
    {
        static readonly Bl s_bl = Factory.Get();

        static string addOrUpdate;

        public event EventHandler CloseWindow;

        public AddUpdateEngineer(int currentEngineerId = 0)
        {
            InitializeComponent();
            try
            {
                CurrentEngineer = currentEngineerId == 0 ? new BO.Engineer()
                : s_bl.Engineer.Read(currentEngineerId);
                addOrUpdate = currentEngineerId == 0 ? "add"
                : "update";
            }
            catch
            {
                MessageBox.Show($"Engineer with id {currentEngineerId} doesn't exist");
            }
        }

        public static readonly DependencyProperty CurrentEngineerProperty =
            DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(AddUpdateEngineer), new PropertyMetadata(null));

        public BO.Engineer CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }
        

        private void AddUpdateEngineerSubmit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (addOrUpdate == "add")
                {
                    s_bl.Engineer.Create(CurrentEngineer);
                    MessageBox.Show("Engineer created successfully");
                    CloseWindow(sender,e);
                    this.Close();
                }
                else
                {
                    s_bl.Engineer.Update(CurrentEngineer);
                    MessageBox.Show("Engineer updated successfully");
                    CloseWindow(sender, e);
                    this.Close();
                }

            }
            catch(BlInorrectData ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(Exception)
            {
                MessageBox.Show("can't save the changes");
            }
        }
    }
}
