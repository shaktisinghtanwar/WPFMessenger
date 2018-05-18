using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
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
using System.Windows.Shapes;
using Fb_InstaWpf.Helper;
using Fb_InstaWpf.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Fb_InstaWpf
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            dataGrid1.ItemsSource = _lstFbUserInfo;
            //DataContext = this;

        }
        private readonly SqLiteHelper _objLiteHelper;
        public string urlName;
        public ChromeDriver _chromeWebDriver;
        ChromeOptions options = new ChromeOptions();
        private readonly BackgroundWorker worker = new BackgroundWorker();
        private Queue<string> _queueUrl = new Queue<string>();

        ObservableCollection<FbUserInfo> _lstFbUserInfo = new ObservableCollection<FbUserInfo>();


        private void ButtonFbLogin_Click(object sender, RoutedEventArgs e)
        {
            string appStartupPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // string appStartupPath = Path.Combine(Environment.CurrentDirectory);
            string url = "https://en-gb.facebook.com/login/";

            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--test-type");
            //options.AddArgument("--headless");
            options.AddArgument("--log-level=3");
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService(appStartupPath);
            chromeDriverService.HideCommandPromptWindow = true;
            _chromeWebDriver = new ChromeDriver(chromeDriverService, options);
            _chromeWebDriver.Manage().Window.Maximize();
            _chromeWebDriver.Navigate().GoToUrl(url);
            try
            {
                ((IJavaScriptExecutor)_chromeWebDriver).ExecuteScript("window.onbeforeunload = function(e){};");
            }
            catch (Exception)
            {

            }

            ReadOnlyCollection<IWebElement> emailElement = _chromeWebDriver.FindElements(By.Id("email"));
            if (emailElement.Count > 0)
            {

                emailElement[0].SendKeys("rishusingh77777@gmail.com");

            }
            ReadOnlyCollection<IWebElement> passwordElement = _chromeWebDriver.FindElements(By.Id("pass"));
            if (passwordElement.Count > 0)
            {
                passwordElement[0].SendKeys("1234567#rk");

            }
            ReadOnlyCollection<IWebElement> signInElement = _chromeWebDriver.FindElements(By.Id("loginbutton"));
            if (signInElement.Count > 0)
            {
                signInElement[0].Click();
            }
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           // throw new NotImplementedException();
        }
        public string textMessage;
        private int messageIdount;
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
           
            string textcommet = (new TextRange(RichTextBoxmsngr.Document.ContentStart, RichTextBoxmsngr.Document.ContentEnd).Text).Trim(); 
            //string textcommet = TextBox2.Text;
            try
            {
                
                //_chromeWebDriver.Navigate().GoToUrl("https://www.facebook.com/messages/t/");
                _chromeWebDriver.Navigate().GoToUrl("https://www.facebook.com/pages/?category=your_pages");
                string pageSource = _chromeWebDriver.PageSource;

                ReadOnlyCollection<IWebElement> yourPageMainNode = _chromeWebDriver.FindElements(By.Id("page_browser_your_pages"));
                if (yourPageMainNode.Count > 0)
                {
                    ReadOnlyCollection<IWebElement> abcd =
                        _chromeWebDriver.FindElements(By.XPath(".//*[@id='page_browser_your_pages']/div/div/div/div[1]/a"));
                    if (abcd.Count > 0)
                    {
                        foreach (var abcdtemp in abcd)
                        {
                            var hrefString = abcdtemp.GetAttribute("href");
                            _queueUrl.Enqueue(hrefString);
                        }
                        try
                        {
                            for (int i = 0; i <= _queueUrl.Count; i++)
                            {
                               
                                if (_queueUrl.Count > 0)
                                { messageIdount++;
                                    urlName = _queueUrl.Dequeue();
                                    _chromeWebDriver.Navigate().GoToUrl(urlName);
                                    Thread.Sleep(3000);
                                    ReadOnlyCollection<IWebElement> pageInbox = _chromeWebDriver.FindElements(By.XPath("//*[@role='tablist']/li[2]/a"));
                                    if (pageInbox.Count > 0)
                                    {
                                        pageInbox[0].Click();
                                        Thread.Sleep(3000);
                                    }

                                    ReadOnlyCollection<IWebElement> pageInboxFbMsngr = _chromeWebDriver.FindElements(By.XPath("//*[@id='u_0_t']/div/div/div/table/tbody/tr/td[1]/div/div[1]/div[1]/div[2]/div"));
                                    if (pageInboxFbMsngr.Count > 0)
                                    {
                                        Thread.Sleep(2000);
                                        pageInboxFbMsngr[0].Click();
                                        Thread.Sleep(2000);
                                    }
                                    ReadOnlyCollection<IWebElement> writeNode =
                                           _chromeWebDriver.FindElements(By.XPath("//*[@placeholder='Write a reply...']"));
                                    if (writeNode.Count > 0)
                                    {
                                        Thread.Sleep(2000);
                                        writeNode[0].SendKeys(textcommet);
                                        Thread.Sleep(2000);
                                        //writeNode[0].SendKeys(msgtxtbox.Text);

                                    }
                                    ReadOnlyCollection<IWebElement> submitnode =
                                           _chromeWebDriver.FindElements(By.XPath("//*[@type='submit']"));
                                    if (submitnode.Count > 0)
                                    {
                                        Thread.Sleep(2000);
                                        submitnode[1].Click();
                                        Thread.Sleep(2000);
                                    }

                                    //var textstr = 




                                }
                               
                                _lstFbUserInfo.Add(new FbUserInfo() {Select=false, Id= messageIdount, Message = textcommet, PageUrl = urlName });
                               // string query = "INSERT INTO TblMessage(Select,Id,Message,PageUrl) values('" + false + "','" + messageIdount + "','" + textcommet + "','" + urlName + "')";
                               // SqLiteHelper sql = new SqLiteHelper();
                               // int yy = sql.ExecuteNonQuery(query);
                               // GetAllItems();
                               //MessageBox.Show("Save SucessFully");

                               // var dicsavedata = new Dictionary<string, string> { { "Message", textcommet }, { "PageUrl",urlName } };
                               // _objLiteHelper.Insert("TblMessage", dicsavedata);
                            }
                          
                           
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public DataTable DtCampaign { get; set; }
        private void GetAllItems()
        {
            string query = "select * from TblMessage";
            SqLiteHelper sql = new SqLiteHelper();
            DtCampaign = sql.GetDataTable(query);
           // dataGrid.ItemsSource = DtCampaign;
        }




        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
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

        private void testEmoji_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                _chromeWebDriver.Navigate().GoToUrl("https://www.facebook.com/pages/?category=your_pages");

                string pageSource = _chromeWebDriver.PageSource;

                ReadOnlyCollection<IWebElement> yourPageMainNode = _chromeWebDriver.FindElements(By.Id("page_browser_your_pages"));
                if (yourPageMainNode.Count > 0)
                {
                    ReadOnlyCollection<IWebElement> abcd =
                        _chromeWebDriver.FindElements(By.XPath(".//*[@id='page_browser_your_pages']/div/div/div/div[1]/a"));
                    if (abcd.Count > 0)
                    {
                        foreach (var abcdtemp in abcd)
                        {
                            var hrefString = abcdtemp.GetAttribute("href");
                            _queueUrl.Enqueue(hrefString);
                        }
                        try
                        {
                            foreach (var _queueUrlUrlTemp in _queueUrl)
                            {
                                if (_queueUrl.Count > 0)
                                {
                                    _chromeWebDriver.Navigate().GoToUrl(_queueUrlUrlTemp);
                                    Thread.Sleep(2000);
                                    ReadOnlyCollection<IWebElement> pageInbox = _chromeWebDriver.FindElements(By.XPath("//*[@role='tablist']/li[2]/a"));
                                    if (pageInbox.Count > 0)
                                    {
                                        pageInbox[0].Click();
                                        Thread.Sleep(3000);
                                        ReadOnlyCollection<IWebElement> pageInboxFbMsngr = _chromeWebDriver.FindElements(By.XPath("//*[@id='u_0_t']/div/div/div/table/tbody/tr/td[1]/div/div[1]/div[1]/div[2]/div"));
                                        if (pageInboxFbMsngr.Count > 0)
                                        {

                                            pageInboxFbMsngr[0].Click();
                                            Thread.Sleep(2000);
                                            ReadOnlyCollection<IWebElement> dcs =
                                            _chromeWebDriver.FindElements(By.XPath("//*[@id='u_0_t']/div/div/div/table/tbody/tr/td[1]/div/div[2]/div/div[1]/div/div[1]/div/div"));
                                            if (dcs.Count > 0)
                                            {
                                                var textstr = dcs[0].Text.Split();
                                                var participaterName = textstr[0] + " " + textstr[0];
                                                dcs[0].Click();
                                                Thread.Sleep(3000);
                                                ReadOnlyCollection<IWebElement> writeNode =
                                           _chromeWebDriver.FindElements(By.XPath("//*[@alt='Post a Sticker']"));
                                                if (writeNode.Count > 0)
                                                {
                                                    writeNode[0].Click();
                                                    Thread.Sleep(5000);
                                                    ReadOnlyCollection<IWebElement> writeNode1 =
                                           _chromeWebDriver.FindElements(By.XPath("//*[@id='js_18j']/div/div/div/table/tbody/tr[1]/td[1]/div/div"));
                                                    if (writeNode1.Count > 0)
                                                    {

                                                    }

                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }
                        catch (Exception)
                        {

                            throw;
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                options.AddArgument("--disable-notifications");
                //_chromeWebDriver.Navigate().GoToUrl("https://www.facebook.com/messages/t/");
                _chromeWebDriver.Navigate().GoToUrl("https://www.facebook.com/pages/?category=your_pages");
                options.AddArgument("--disable-notifications");
                string pageSource = _chromeWebDriver.PageSource;

                ReadOnlyCollection<IWebElement> yourPageMainNode = _chromeWebDriver.FindElements(By.Id("page_browser_your_pages"));
                if (yourPageMainNode.Count > 0)
                {
                    ReadOnlyCollection<IWebElement> abcd =
                        _chromeWebDriver.FindElements(By.XPath(".//*[@id='page_browser_your_pages']/div/div/div/div[1]/a"));
                    if (abcd.Count > 0)
                    {
                        foreach (var abcdtemp in abcd)
                        {
                            var hrefString = abcdtemp.GetAttribute("href");
                            _queueUrl.Enqueue(hrefString);
                        }
                        try
                        {
                            for (int i = 0; i < _queueUrl.Count; i++)
                            {


                                if (_queueUrl.Count > 0)
                                {
                                    _chromeWebDriver.Navigate().GoToUrl(_queueUrl.Dequeue());
                                    Thread.Sleep(2000);
                                    ReadOnlyCollection<IWebElement> pageInbox = _chromeWebDriver.FindElements(By.XPath("//*[@role='tablist']/li[2]/a"));
                                    if (pageInbox.Count > 0)
                                    {
                                        pageInbox[0].Click();
                                        Thread.Sleep(3000);
                                        ReadOnlyCollection<IWebElement> pageInboxFbMsngr = _chromeWebDriver.FindElements(By.XPath("//*[@id='u_0_t']/div/div/div/table/tbody/tr/td[1]/div/div[1]/div[1]/div[2]/div"));
                                        if (pageInboxFbMsngr.Count > 0)
                                        {

                                            pageInboxFbMsngr[0].Click();
                                            Thread.Sleep(2000);
                                            ReadOnlyCollection<IWebElement> dcs =
                                            _chromeWebDriver.FindElements(By.XPath("//*[@id='u_0_t']/div/div/div/table/tbody/tr/td[1]/div/div[2]/div/div[1]/div/div[1]/div/div"));
                                            if (dcs.Count > 0)
                                            {
                                                var textstr = dcs[0].Text.Split();
                                                var participaterName = textstr[0] + " " + textstr[0];
                                                dcs[0].Click(); 
                                                Thread.Sleep(3000);
                                                ReadOnlyCollection<IWebElement> writeNode =
                                           _chromeWebDriver.FindElements(By.XPath("//*[@data-tooltip-content='Send a file']"));
                                                if (writeNode.Count > 0)
                                                {
                                                    writeNode[0].Click();
                                                    Thread.Sleep(5000);
                                                    // writeNode[0].SendKeys(msgtxtbox.Text);
                                                    ReadOnlyCollection<IWebElement> submitnode =
                                           _chromeWebDriver.FindElements(By.XPath("//*[@type='submit']"));
                                                    if (submitnode.Count > 0)
                                                    {
                                                        submitnode[1].Click();
                                                        options.AddArgument("--disable-notifications");
                                                    }
                                                }
                                                //var textstr = 
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Test Comleted.");
                                    _chromeWebDriver.Navigate().GoToUrl("https://www.facebook.com/pages/?category=your_pages");
                                }

                                
                            }
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                        //dispatcherTimerNavigatePageUrl.Tick += new EventHandler(dispatcherTimerNavigatePageUrl_Tick);
                        //dispatcherTimerNavigatePageUrl.Interval = new TimeSpan(1000);
                        //dispatcherTimerNavigatePageUrl.Start();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnInstaimgUpld_Click_1(object sender, RoutedEventArgs e)
        {
            ReadOnlyCollection<IWebElement> fbcommnet = _chromeWebDriver.FindElements(By.XPath(".//*[@class='_4k8x']"));
            if (fbcommnet.Count > 0)
            {
                for (int i = 0; i < fbcommnet.Count; i++)
                {
                    {
                        fbcommnet[i].Click();
                        Thread.Sleep(3000);
                        ReadOnlyCollection<IWebElement> sendcomment = _chromeWebDriver.FindElements(By.ClassName("UFICommentPhotoIcon"));
                        if (sendcomment.Count > 0)
                        {
                            sendcomment[0].Click();
                            Thread.Sleep(10000);
                            ReadOnlyCollection<IWebElement> clickfile = _chromeWebDriver.FindElements(By.XPath(".//*[@class='notranslate _5rpu']"));
                            if (clickfile.Count > 0)
                            {
                                Thread.Sleep(7000);
                                clickfile[0].SendKeys(OpenQA.Selenium.Keys.Enter);
                                Thread.Sleep(2000);
                            }
                        }
                    }
                }
            }
        }

        private void Btninsta_Click(object sender, RoutedEventArgs e)
        {
            string commentinsta = (new TextRange(RichTextBoxinsta.Document.ContentStart, RichTextBoxinsta.Document.ContentEnd).Text).Trim(); 
            string url = "https://www.facebook.com/TP-1996120520653285/inbox/";
            _chromeWebDriver.Navigate().GoToUrl(url);
            Thread.Sleep(3000);

            ReadOnlyCollection<IWebElement> collection = _chromeWebDriver.FindElements(By.ClassName("_32wr"));
            {
                if (collection.Count > 0)
                {
                    collection[2].Click();
                    Thread.Sleep(3000);
                }
            }

            ReadOnlyCollection<IWebElement> commentpost = _chromeWebDriver.FindElements(By.XPath(".//*[@class='_284g _4bl9']"));
            if (commentpost.Count > 0)
            {
                for (int i = 0; i < commentpost.Count; i++)
                {
                    {
                        commentpost[i].Click();
                        Thread.Sleep(3000);
                        ReadOnlyCollection<IWebElement> sendcomment = _chromeWebDriver.FindElements(By.XPath(".//*[@class='_58al']"));
                        if (sendcomment.Count > 0)
                        {
                            sendcomment[0].SendKeys(commentinsta);
                            Thread.Sleep(2000);
                            sendcomment[0].SendKeys(OpenQA.Selenium.Keys.Enter);
                        }
                    }
                }
            }
        }

       


        
    }
    public class Employee
    {
        public string Name { get; set; }
        public int EmpId { get; set; }
        public int salary { get; set; }
    }  

    public class ListtoDataTable
    {
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }  
}
