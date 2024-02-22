
using BlApi;
using PL.Engineer;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    static readonly Bl s_bl = Factory.Get();

    /// <summary>
    /// initialize the main window
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// click to show the engineer list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnEngineer_Click(object sender, RoutedEventArgs e)
    {
        new EngineerListWindow().Show();
    }

    /// <summary>
    /// rest all the data base
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ResetDB(object sender, RoutedEventArgs e)
    {
        MessageBoxResult mbResult = MessageBox.Show(
        "Are you sure?", "reset",
        MessageBoxButton.YesNo,
        MessageBoxImage.Question,
        MessageBoxResult.Yes,
        MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
        if (mbResult == MessageBoxResult.Yes)
            s_bl.SpecialOperations.Reset();
    }

    private void InitDB(object sender, RoutedEventArgs e)
    {
        MessageBoxResult mbResult = MessageBox.Show(
       "Are you sure?", "reset",
       MessageBoxButton.YesNo,
       MessageBoxImage.Question,
       MessageBoxResult.Yes,
       MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
        if (mbResult == MessageBoxResult.Yes)
            s_bl.SpecialOperations.Init();
    }
}
