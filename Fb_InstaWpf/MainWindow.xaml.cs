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
        List<FbpageInboxUserInfo>  Files=new List<FbpageInboxUserInfo>();
        public FbpageInboxUserInfo ObjFbpageInboxUserInfo=new FbpageInboxUserInfo();
        private readonly BackgroundWorker worker = new BackgroundWorker();
        public ChatMessenger()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel();


            //this.DataContext = Files;
        }

       

       
        

        private void BindUserInfoByApi()
        {
            //foreach (var item in record)
            //{
            //    FbpageInboxUserInfo FbpageInboxUserInfo = new FbpageInboxUserInfo();
            //    FbpageInboxUserInfo.InboxUserName = item.username;
            //    FbpageInboxUserInfo.InboxUserImage = item.image;
            //    LstNames.Items.Add(FbpageInboxUserInfo);
            //}
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
        private void worker_RunWorkerCompleted(object sender,
                                               RunWorkerCompletedEventArgs e)
        {
            //update ui once worker complete his work
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
        }

        private void testEmoji_Click(object sender, RoutedEventArgs e)
        {
          
        }
      
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            if ( this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Minimized;
            }
            else if(this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Minimized;
            }
            
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void btnMinimize_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Minimized;
            }
            else if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Minimized;
            }
        }

        private void btnMaximize_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void btnExit_Click_1(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
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
            //try
            //{
            //    if (btnLogin.Content.Equals("Login"))
            //    {
            //        btnLogin.Content = "Logout";
            //        try
            //        {
            //            worker.DoWork += Loginworker_DoWork;
            //            worker.RunWorkerCompleted += Loginworker_RunWorkerCompleted;
            //            worker.RunWorkerAsync();

                        

            //        }
            //        catch (Exception)
            //        {
                            
                        
            //        }

            //    }
            //    else if (btnLogin.Content.Equals("Logout"))
            //    {
            //        btnLogin.Content = "Login";
            //        ReadOnlyCollection<IWebElement> fbAcsettingNodeElement = ChromeWebDriver.FindElements(By.Id("userNavigationLabel"));
            //        if (fbAcsettingNodeElement.Count > 0)
            //        {
            //            fbAcsettingNodeElement[0].Click();
            //        }

            //        ReadOnlyCollection<IWebElement> logOutNode = ChromeWebDriver.FindElements(By.XPath("//*[@class='_54nh']"));
            //        if (logOutNode.Count>0)
            //        {
            //            logOutNode[0].Click();
            //        }


            //        ChromeWebDriver.Quit();
            //    }
            //}
            //catch (Exception)
            //{
            //    // ignored
            //}
        }

        private void Loginworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                ReadOnlyCollection<IWebElement> bluebarNodeelement = ChromeWebDriver.FindElements(By.Id("blueBarDOMInspector"));
                if (bluebarNodeelement.Count>0)
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
      public  ObservableCollection<FbUserInfo> LstFbUserInfo = new ObservableCollection<FbUserInfo>();
        private readonly SqLiteHelper objLiteHelper;
        public string urlName;
        public ChromeDriver ChromeWebDriver;
        readonly ChromeOptions _options = new ChromeOptions();
        private readonly Queue<string> _queueUrl = new Queue<string>();

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {

            //string textcommet = (new TextRange(RichTextBoxmsngr.Document.ContentStart, RichTextBoxmsngr.Document.ContentEnd).Text).Trim();
            //try
            //{

            //    //_chromeWebDriver.Navigate().GoToUrl("https://www.facebook.com/messages/t/");
            //    ChromeWebDriver.Navigate().GoToUrl("https://www.facebook.com/pages/?category=your_pages");
            //    ReadOnlyCollection<IWebElement> yourPageMainNode = ChromeWebDriver.FindElements(By.Id("page_browser_your_pages"));
            //    if (yourPageMainNode.Count > 0)
            //    {
            //        ReadOnlyCollection<IWebElement> abcd =
            //            ChromeWebDriver.FindElements(By.XPath(".//*[@id='page_browser_your_pages']/div/div/div/div[1]/a"));
            //        if (abcd.Count > 0)
            //        {
            //            foreach (var abcdtemp in abcd)
            //            {
            //                var hrefString = abcdtemp.GetAttribute("href");
            //                _queueUrl.Enqueue(hrefString);
            //            }
            //            try
            //            {
            //                for (int i = 0; i <= _queueUrl.Count; i++)
            //                {

            //                    if (_queueUrl.Count > 0)
            //                    {
            //                        _messageIdount++;
            //                        urlName = _queueUrl.Dequeue();
            //                        ChromeWebDriver.Navigate().GoToUrl(urlName);
            //                        Thread.Sleep(3000);
            //                        ReadOnlyCollection<IWebElement> pageInbox = ChromeWebDriver.FindElements(By.XPath("//*[@role='tablist']/li[2]/a"));
            //                        if (pageInbox.Count > 0)
            //                        {
            //                            pageInbox[0].Click();
            //                            Thread.Sleep(3000);
            //                        }

            //                        ReadOnlyCollection<IWebElement> pageInboxFbMsngr = ChromeWebDriver.FindElements(By.XPath("//*[@id='u_0_t']/div/div/div/table/tbody/tr/td[1]/div/div[1]/div[1]/div[2]/div"));
            //                        if (pageInboxFbMsngr.Count > 0)
            //                        {
            //                            Thread.Sleep(2000);
            //                            pageInboxFbMsngr[0].Click();
            //                            Thread.Sleep(2000);
            //                        }
            //                        ReadOnlyCollection<IWebElement> writeNode =
            //                               ChromeWebDriver.FindElements(By.XPath("//*[@placeholder='Write a reply...']"));
            //                        if (writeNode.Count > 0)
            //                        {
            //                            Thread.Sleep(2000);
            //                            writeNode[0].SendKeys(textcommet);
            //                            Thread.Sleep(2000);
            //                            //writeNode[0].SendKeys(msgtxtbox.Text);

            //                        }
            //                        ReadOnlyCollection<IWebElement> submitnode =
            //                               ChromeWebDriver.FindElements(By.XPath("//*[@type='submit']"));
            //                        if (submitnode.Count > 0)
            //                        {
            //                            Thread.Sleep(2000);
            //                            submitnode[1].Click();
            //                            Thread.Sleep(2000);
            //                        }

            //                    }

            //                    LstFbUserInfo.Add(new FbUserInfo() { Select = false, Id = _messageIdount, Message = textcommet, PageUrl = urlName });
            //                    // string query = "INSERT INTO TblMessage(Select,Id,Message,PageUrl) values('" + false + "','" + messageIdount + "','" + textcommet + "','" + urlName + "')";
            //                    // SqLiteHelper sql = new SqLiteHelper();
            //                    // int yy = sql.ExecuteNonQuery(query);
            //                    // GetAllItems();
            //                    //MessageBox.Show("Save SucessFully");

            //                    // var dicsavedata = new Dictionary<string, string> { { "Message", textcommet }, { "PageUrl",urlName } };
            //                    // _objLiteHelper.Insert("TblMessage", dicsavedata);
            //                }


            //            }
            //            catch (Exception)
            //            {

            //                throw;
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }
        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {

            //MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            //mainWindowViewModel.LoginCommand = new DelegateCommand(LoginCommandHandler, null);
        }

        private void btnSendText_Click(object sender, RoutedEventArgs e)
        {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        private void TextBoxUserpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (TextBoxUserpassword.Text.Contains("Password"))
            {
                TextBoxUserpassword.Text = "";
            }

        }

        private void TextBoxUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (TextBoxUsername.Text.Contains("Email or Phone"))
            {
                TextBoxUsername.Text = "";
            }
        }
        public string LstItemUserName { get; set; }

        private void TextBoxUserpassword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

       

    
    }
}
