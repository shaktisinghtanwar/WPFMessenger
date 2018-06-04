using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using Fb_InstaWpf.Helper;
using Fb_InstaWpf.Model;
using Fb_InstaWpf.ViewModel;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Fb_InstaWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ChatMessenger : Window
    {

        //public ObservableCollection<FbpageInboxUserInfo> Files { get; set; }
        List<FbpageInboxUserInfo> Files = new List<FbpageInboxUserInfo>();
        public FbpageInboxUserInfo ObjFbpageInboxUserInfo = new FbpageInboxUserInfo();
        private readonly BackgroundWorker worker = new BackgroundWorker();
        public ChatMessenger()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel();
            //this.DataContext = new MasterViewModel();

        }

        private void BindUserInfoByApi()
        {

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {

            }
        }

        private void workerFbComment_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {

        }

        public void dispatcherTimerNavigatePageUrl1()
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void RichTextBoxmsngr_KeyDown(object sender, KeyEventArgs e)
        {
            if (msgtxtbox2.Text.Contains("Write a reply..."))
            {
                msgtxtbox2.Text = "";
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Loginworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                ReadOnlyCollection<IWebElement> bluebarNodeelement = ChromeWebDriver.FindElements(By.Id("blueBarDOMInspector"));
                if (bluebarNodeelement.Count > 0)
                {
                    MessageBox.Show("Login Successfully..!");
                    ChromeWebDriver.Manage().Window.Maximize();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void Loginworker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string appStartupPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                // string appStartupPath = Path.Combine(Environment.CurrentDirectory);
                const string url = "https://en-gb.facebook.com/login/";

                _options.AddArgument("--disable-notifications");
                _options.AddArgument("--disable-extensions");
                _options.AddArgument("--test-type");
                //options.AddArgument("--headless");
                _options.AddArgument("--log-level=3");
                ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService(appStartupPath);
                chromeDriverService.HideCommandPromptWindow = true;
                ChromeWebDriver = new ChromeDriver(chromeDriverService, _options);
                ChromeWebDriver.Manage().Window.Maximize();
                ChromeWebDriver.Navigate().GoToUrl(url);
                try
                {
                    ((IJavaScriptExecutor)ChromeWebDriver).ExecuteScript("window.onbeforeunload = function(e){};");
                }
                catch (Exception)
                {

                }

                ReadOnlyCollection<IWebElement> emailElement = ChromeWebDriver.FindElements(By.Id("email"));
                if (emailElement.Count > 0)
                {

                    emailElement[0].SendKeys("rishusingh77777@gmail.com");
                    //emailElement[0].SendKeys(TextBoxUserEmail.Text);

                }
                ReadOnlyCollection<IWebElement> passwordElement = ChromeWebDriver.FindElements(By.Id("pass"));
                if (passwordElement.Count > 0)
                {
                    passwordElement[0].SendKeys("1234567#rk");
                    //passwordElement[0].SendKeys(TextBox_Password.Text);

                }
                ReadOnlyCollection<IWebElement> signInElement = ChromeWebDriver.FindElements(By.Id("loginbutton"));
                if (signInElement.Count > 0)
                {
                    signInElement[0].Click();
                    ChromeWebDriver.Navigate().GoToUrl("https://www.facebook.com/pages/?category=your_pages");
                    ChromeWebDriver.Navigate().GoToUrl("https://www.facebook.com/TP-1996120520653285/inbox/?selected_item_id=100002948674558");
                    Thread.Sleep(2000);
                    string pageSource = ChromeWebDriver.PageSource;
                    HtmlDocument htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(pageSource);
                    var listNodeElement =
                        htmlDocument.DocumentNode.SelectNodes(
                            "//div[@class='_4ik4 _4ik5']");
                    if (listNodeElement != null)
                    {

                        LstItemUserName = listNodeElement[0].ChildNodes[0].InnerText;

                        Files.Add(new FbpageInboxUserInfo() { InboxUserName = LstItemUserName });

                    }
                    //LstNames.ItemsSource = Files;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public string TextMessage;
        private int _messageIdount;
        public ObservableCollection<FbUserInfo> LstFbUserInfo = new ObservableCollection<FbUserInfo>();
        private readonly SqLiteHelper objLiteHelper;
        public string urlName;
        public ChromeDriver ChromeWebDriver;
        readonly ChromeOptions _options = new ChromeOptions();
        private readonly Queue<string> _queueUrl = new Queue<string>();

        private void TextBoxUserpassword_KeyDown(object sender, KeyEventArgs e)
        {
            //if (TextBoxUserpassword.Text.Contains("Password"))
            //{
            //    TextBoxUserpassword.Text = "";
            //}

        }

        private void TextBoxUsername_KeyDown(object sender, KeyEventArgs e)
        {
            //if (TextBoxUsername.Text.Contains("Email or Phone"))
            //{
            //    TextBoxUsername.Text = "";
            //}
        }
        public string LstItemUserName { get; set; }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void TabLeftItemMessenger_GotFocus(object sender, RoutedEventArgs e)
        {
            TabRightItemInsta.Visibility = Visibility.Hidden;
            TabRightItemFacebook.Visibility = Visibility.Hidden;
            TabRightItemMessenger.IsSelected = true;
        }

        private void TabLeftFacebookItem_GotFocus(object sender, RoutedEventArgs e)
        {
            TabRightItemMessenger.Visibility = Visibility.Hidden;
            TabRightItemInsta.Visibility = Visibility.Hidden;
            TabRightItemFacebook.Visibility = Visibility.Visible;
            TabRightItemFacebook.IsSelected = true;
        }

        private void TabLeftItemInsta_GotFocus(object sender, RoutedEventArgs e)
        {
            TabRightItemMessenger.Visibility = Visibility.Hidden;
            TabRightItemFacebook.Visibility = Visibility.Hidden;
            TabRightItemInsta.Visibility = Visibility.Visible;
            TabRightItemInsta.IsSelected = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TabRightItemInsta.Visibility = Visibility.Hidden;
            TabRightItemFacebook.Visibility = Visibility.Hidden;
            ImageProgressbar.Visibility = Visibility.Hidden;
            //cmbUser.Text = "---Select---";
            //cmbUser.Items.Insert(0, "Please select any value");

        }

        private void btnUserLogins_Click(object sender, RoutedEventArgs e)
        {
            AddLoginUsers addLoginUsers = new AddLoginUsers();
            addLoginUsers.ShowDialog();
        }

        private void cmbUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             
           
        }

    }
}
