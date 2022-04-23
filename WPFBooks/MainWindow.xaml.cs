using HomeLibrary;
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

namespace WPFBooks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Random getrandom = new Random();
        private Book bookItem;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListBoxOnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {
                Book bookItem = (Book)item.DataContext;
                this.bookItem = bookItem;
                idTextBlock.Text = bookItem.Id.ToString();
                titleTextBox.Text = bookItem.Title;
                authorTextBox.Text = bookItem.Author;
                yearTextBox.Text = bookItem.Year.ToString();
                isReadCheckbox.IsChecked = bookItem.IsRead;
                // formatTextBox.Text = bookItem.Format.ToString();
                formatComboBox.ItemsSource = Enum.GetValues(typeof(BookFormat)).Cast<BookFormat>();
                formatComboBox.SelectedItem = bookItem.Format;
                deleteButton.Visibility = Visibility.Visible;
            }
        }

        private void TextChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            var item = sender as TextBox;
            if (item != null && bookItem != null) {
                if (item.Name.ToString().Contains("title"))
                {
                    bookItem.Title = item.Text;
                    listOfBooks.Items.Refresh();
                } else if (item.Name.ToString().Contains("author"))
                {
                    bookItem.Author = item.Text;
                } else if (item.Name.ToString().Contains("year"))
                {
                    try
                    {
                        bookItem.Year = Int32.Parse(item.Text);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Input string is invalid.");
                    }
                }
            }
        }

        private void CheckBoxClickedEventHandler(object sender, RoutedEventArgs e)
        {
            var item = sender as CheckBox;
            if (item != null && bookItem != null)
            {
                if (item.IsChecked == null)
                {
                    bookItem.IsRead = false;
                }
                else
                {
                    bookItem.IsRead = (bool)item.IsChecked;
                }
                listOfBooks.Items.Refresh();
            }
        }

        private void ComboBoxChangedEventHandler(object sender, SelectionChangedEventArgs args)
        {
            var item = sender as ComboBox;
            if (item != null && bookItem != null)
            {
                BookFormat format = (BookFormat)Enum.Parse(typeof(BookFormat), item.SelectedItem.ToString()); ;
                bookItem.Format = format;
            }
        }

        private void AddButtonClickedEventHandler(object sender, RoutedEventArgs e)
        {
            Book newBook = new Book(getrandom.Next(30));
            newBook.Title = titlePopUpTextBox.Text;
            newBook.Author = authorPopUpTextBox.Text;

            if (isReadPopUpCheckbox.IsChecked != null)
            {
                newBook.IsRead = (bool)isReadPopUpCheckbox.IsChecked;
            } else
            {
                newBook.IsRead = false;
            }

            if (formatPopUpComboBox.SelectedItem != null)
            {
                newBook.Format = (BookFormat)formatPopUpComboBox.SelectedItem;
            }
            
            try
            {
                newBook.Year = Int32.Parse(yearPopUpTextBox.Text);
            }
            catch (FormatException)
            {
                Console.WriteLine("Input string for year is invalid.");
            }

            MyBookCollection.CollectionOfBooks.Add(newBook);
            listOfBooks.Items.Refresh();
            titlePopUpTextBox.Text = "";
            authorPopUpTextBox.Text = "";
            isReadPopUpCheckbox.IsChecked = false;
            formatPopUpComboBox.SelectedItem = null;
            yearPopUpTextBox.Text = "";
            addPopUp.IsOpen = false;
        }

        private void DeleteButtonClickedEventHandler(object sender, RoutedEventArgs e)
        {
            if (bookItem != null)
            {
                MyBookCollection.CollectionOfBooks.Remove(bookItem);
                bookItem = null;
                listOfBooks.Items.Refresh();
                idTextBlock.Text = "";
                titleTextBox.Text = "";
                authorTextBox.Text = "";
                yearTextBox.Text = "";
                isReadCheckbox.IsChecked = false;
                formatComboBox.SelectedItem = null;
                formatComboBox.ItemsSource = null;
                deleteButton.Visibility = Visibility.Hidden;
                deletePopUp.IsOpen = false;
            }
        }
    }

}
