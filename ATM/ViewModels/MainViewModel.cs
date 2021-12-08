using ATM.Command;
using ATM.Models;
using ATM.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ATM.ViewModels
{
    
   public class MainViewModel:BaseViewModel
   {      
        public RelayCommand TransferCommand { get; set; }
        public RelayCommand LoadDataCommand { get; set; }

        public FakeRepo _repo { get; set; }
        public List<Employee> employees { get; set; }
        
        public Card Card { get; set; }
        int c = 0;
        public MainWindow MainWindow { get; set; }
        public static object obj = new object();
        DispatcherTimer dispatcher = new DispatcherTimer();
        public string item { get; set; }
        public MainViewModel(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            dispatcher.Interval = TimeSpan.FromSeconds(2);
            dispatcher.Tick += Dispatcher_Tick;
            employees = new List<Employee>();
            _repo = new FakeRepo();
            employees = _repo.GetAll();


            LoadDataCommand = new RelayCommand((sender) =>
            {
                Find();
                if (MainWindow.CardComboBox.SelectedItem != null)
                {
                    foreach (var employee in employees)
                    {
                        foreach (var card in employee.Cards)
                        {
                            if (card.Number == item)
                            {
                                MainWindow.FullnameTxtBlock.Text = employee.Fullname;
                                MainWindow.BalanceTxtBlock.Text = card.Balance;
                                MainWindow.aznTxt.Visibility = Visibility.Visible;
                                Card = card;
                                break;
                            }
                           
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Choose Card");
                }
            });

            TransferCommand = new RelayCommand((sender) =>
            {
                lock (obj)
                {
                    dispatcher.Start();
                    if (decimal.Parse(Card.Balance.ToString()) >= decimal.Parse(MainWindow.TransferMoneTxtBlock.Text))
                    {
                        MainWindow.azn2Txt.Visibility = Visibility.Visible;
                        Thread.Sleep(2000);
                        Card.Balance = (decimal.Parse(Card.Balance.ToString()) - decimal.Parse(MainWindow.TransferMoneTxtBlock.Text)).ToString();
                        mainWindow.BalanceTxtBlock.Text = Card.Balance;
                    }
                    else
                    {
                        MessageBox.Show("Transfer Declined");
                    }
                }
            });
        }
        private void Dispatcher_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Decimal.Parse(MainWindow.TransferMoneTxtBlock.Text) > 0)
                {
                    MainWindow.TransferMoneTxtBlock.Text = (Decimal.Parse(MainWindow.TransferMoneTxtBlock.Text) - 10).ToString();
                    MainWindow.MoneyLoadTxtBlock.Text = (c += 10).ToString();
                }
                else
                {
                    dispatcher.Stop();
                }
            }
            catch (Exception)
            {
            }
        }
        public void Find()
        {
            bool finded = false;
            var word =MainWindow.CardComboBox.SelectedItem.ToString();
            foreach (var w in word)
            {
                if (w == ':')
                {
                    finded = true;
                }
                if (finded)
                {
                    if (w != ':' && w != ' ')
                    {
                        item += w;
                    }

                }
            }
        }
    }

}

   

