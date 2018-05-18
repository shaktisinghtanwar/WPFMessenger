using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Fb_InstaWpf.Helper;
using Fb_InstaWpf.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using HtmlAgilityPack;

namespace Fb_InstaWpf.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Field
        private ObservableCollection<FbpageInboxUserInfo> _userListInfo;
        private ObservableCollection<FacebookPageInboxmember> _fbPageInboxmember;
        private ObservableCollection<InstaInboxmember> _instaInboxmember;
        private FbpageInboxUserInfo _selectedUserInfo;
        private FacebookPageInboxmember SelectedFbPageInboxmember;
        private ObservableCollection<FbUserMessageInfo> messagingListInfo { get; set; }
        public string LstItemUserName { get; set; }
        public string TextBxValue { get; set; }
        public string Getimgurl { get; set; }
        public string TemppageNodeItem { get; set; }
        public string DataImg { get; set; }
        public string PageSource { get; set; }
        public ChromeDriver ChromeWebDriver;
        private int _messageIdount;
        public string UrlName;
        private string _textcommet = string.Empty;
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        readonly HtmlDocument _htmlDocument = new HtmlDocument();
        readonly ChromeOptions _options = new ChromeOptions();
        private readonly Queue<string> _myqueue = new Queue<string>();
        private readonly Queue<string> _queueUrl = new Queue<string>();
        private readonly Queue<string> _queueFbCmntImgUrl = new Queue<string>();
        private readonly Queue<string> _queueInstaImgUrl = new Queue<string>();
        private readonly Queue<string> _queueFbImgUrl = new Queue<string>();
        public List<String> LstPageUrl = new List<string>();

        #endregion

        #region Contructor
        public MainWindowViewModel()
        {
            InstaListmembers = new ObservableCollection<InstaInboxmember>();
            FbPageListmembers=new ObservableCollection<FacebookPageInboxmember>();
            //FbPageInboxmember = new ObservableCollection<FacebookPageInboxmember>();
            UserListInfo = new ObservableCollection<FbpageInboxUserInfo>();
            MessagingListInfo = new ObservableCollection<FbUserMessageInfo>();
            // BIndFbPageData();
            //BIndInstData();
           //BindUserInfo();
            SendMessageCommand = new DelegateCommand(SendMessageCommandHandler, null);
            LoginCommand = new DelegateCommand(LoginCommandHandler, null);
            FbMessengerListCommand = new DelegateCommand(FbMessengerListCommandHandler,null);
            IntaInboxCommand = new DelegateCommand(IntaInboxCommandHandler,null);
            FbPageInboxCommand = new DelegateCommand(FbPageInboxCommandHandler, null);
            
        }
        
        private void FbPageInboxCommandHandler(object obj)
        {
            try
            {
                string url = "https://www.facebook.com/pages/?category=your_pages";
                ChromeWebDriver.Navigate().GoToUrl(url);

                ReadOnlyCollection<IWebElement> pageNodeImgUrl = ChromeWebDriver.FindElements((By.XPath("//*[@class='clearfix _1vh8']/a")));
                if (pageNodeImgUrl.Count > 0)
                {
                    foreach (var pageNodeItem in pageNodeImgUrl)
                    {
                        TemppageNodeItem = pageNodeItem.GetAttribute("href");
                        LstPageUrl.Add(TemppageNodeItem);
                       
                        _queueFbCmntImgUrl.Enqueue(TemppageNodeItem);
                        
                    }

                    for (int i = 0; i < LstPageUrl.Count; i++)
                    {
                        ChromeWebDriver.Navigate().GoToUrl(new Uri(LstPageUrl[0]));
                        Thread.Sleep(2000);

                        ReadOnlyCollection<IWebElement> collection1 = ChromeWebDriver.FindElements(By.XPath("//*[@id='u_0_u']/div/div/div[1]/ul/li[2]/a"));
                        if (collection1.Count > 0)
                        {

                            collection1[0].Click();
                            
                        }
                        Thread.Sleep(2000);
                        ReadOnlyCollection<IWebElement> collectionTab2 = ChromeWebDriver.FindElements(By.ClassName("_32wr"));
                        if (collectionTab2.Count > 0)
                        {
                            collectionTab2[1].Click();
                        }
                        Thread.Sleep(2000);
                        PageSource = ChromeWebDriver.PageSource;
                        _htmlDocument.LoadHtml(PageSource);
                        HtmlNodeCollection imgNode =
                            _htmlDocument.DocumentNode.SelectNodes(
                                "//*[@id='u_0_t']/div/div/div/table/tbody/tr/td[1]/div/div[2]/div/div[1]/div/div/div/div/div/div/img");
                        if (imgNode != null)
                        {
                            foreach (var imgNodeItem in imgNode)
                            {
                                Getimgurl = imgNodeItem.Attributes["src"].Value.Replace(";", "&");

                                _queueFbImgUrl.Enqueue(Getimgurl);
                            }
                        }
                        
                    }
                    var listNodeElements =
                  _htmlDocument.DocumentNode.SelectNodes(
                      "//div[@class='_4ik4 _4ik5']");
                    if (listNodeElements != null)
                    {
                        for (int j = 0; j < listNodeElements.Count; j++)
                        {
                            if (j % 2 == 0)
                            {
                                LstfbItemUserName = listNodeElements[j].ChildNodes[0].InnerText;
                                var imgUrl = _queueFbImgUrl.Dequeue();

                                FbPageListmembers.Add(new FacebookPageInboxmember { FbPageImage = imgUrl, FbPageName = LstfbItemUserName });
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
       
        

        private void IntaInboxCommandHandler(object obj)
        {

           // string commentinsta = (new TextRange(RichTextBoxinsta.Document.ContentStart, RichTextBoxinsta.Document.ContentEnd).Text).Trim();
            string url = "https://www.facebook.com/TP-1996120520653285/inbox/";
            ChromeWebDriver.Navigate().GoToUrl(url);
            Thread.Sleep(3000);

            ReadOnlyCollection<IWebElement> collection = ChromeWebDriver.FindElements(By.ClassName("_32wr"));
            {
                if (collection.Count > 0)
                {
                    collection[2].Click();
                    Thread.Sleep(3000);
                }
            }

            ReadOnlyCollection<IWebElement> commentpostImgNodCollection = ChromeWebDriver.FindElements(By.XPath(".//*[@class='_11eg _5aj7']/div/div/img"));
            if (commentpostImgNodCollection.Count > 0)
            {
                for (int i = 0; i < commentpostImgNodCollection.Count; i++)
                {
                     DataImg = commentpostImgNodCollection[i].GetAttribute("src");
                     _queueInstaImgUrl.Enqueue(DataImg);
                }
            }
            ReadOnlyCollection<IWebElement> commentpostNameCollection = ChromeWebDriver.FindElements(By.XPath(".//*[@class='_284g _4bl9']/div/div/span"));
            if (commentpostNameCollection.Count>0)
            {
                for (int i = 0; i < commentpostNameCollection.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        string dataName = commentpostNameCollection[i].Text;
                        InstaListmembers.Add(new InstaInboxmember { InstaInboxUserImage = _queueInstaImgUrl.Dequeue(), InstaInboxUserName = dataName });
                    }

                }
            }
            //
        }

        private void BIndFbPageData()
        {


            FbPageListmembers.Add(new FacebookPageInboxmember { FbPageName = "Facebook Page1 Post", FbPageImage = "E:\\RAHUL_WORK\\WPF_Examples\\Fb_InstaWpf12052018\\Fb_InstaWpf\\Fb_InstaWpf\\Images\\download.jpg" });
            FbPageListmembers.Add(new FacebookPageInboxmember { FbPageName = "Facebook Page2 Post", FbPageImage = "E:\\RAHUL_WORK\\WPF_Examples\\Fb_InstaWpf12052018\\Fb_InstaWpf\\Fb_InstaWpf\\Images\\download.jpg" });
        }

        private void BIndInstData()
        {
            InstaListmembers.Add(new InstaInboxmember { InstaInboxUserName = "Instagram Page1 Post", InstaInboxUserImage = "E:\\RAHUL_WORK\\WPF_Examples\\Fb_InstaWpf12052018\\Fb_InstaWpf\\Fb_InstaWpf\\Images\\download.jpg" });
            InstaListmembers.Add(new InstaInboxmember { InstaInboxUserName = "Instagram Page2 Post", InstaInboxUserImage = "E:\\RAHUL_WORK\\WPF_Examples\\Fb_InstaWpf12052018\\Fb_InstaWpf\\Fb_InstaWpf\\Images\\download.jpg" });
        }


        private void LoginCommandHandler(object obj)
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
                    ChromeWebDriver.Navigate().GoToUrl(url);

                    ChromeWebDriver.Navigate().GoToUrl("https://www.facebook.com/TP-1996120520653285/inbox/?selected_item_id=100002948674558");
                    Thread.Sleep(2000);
                     GetFbMessengerListData();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GetFbMessengerListData()
        {
            PageSource = ChromeWebDriver.PageSource;
            _htmlDocument.LoadHtml(PageSource);
            HtmlNodeCollection imgNode =
                _htmlDocument.DocumentNode.SelectNodes(
                    "//*[@id='u_0_t']/div/div/div/table/tbody/tr/td[1]/div/div[2]/div/div[1]/div/div/div/div/div/div/img");
            if (imgNode != null)
            {
                foreach (var imgNodeItem in imgNode)
                {
                    Getimgurl = imgNodeItem.Attributes["src"].Value.Replace(";", "&");

                    _myqueue.Enqueue(Getimgurl);
                }
            }


            var listNodeElements =
                _htmlDocument.DocumentNode.SelectNodes(
                    "//div[@class='_4ik4 _4ik5']");
            if (listNodeElements != null)
            {
                for (int i = 0; i < listNodeElements.Count; i++)
                {
                    if (i%2 == 0)
                    {
                        LstItemUserName = listNodeElements[i].ChildNodes[0].InnerText;
                        var imgUrl = _myqueue.Dequeue();

                        UserListInfo.Add(new FbpageInboxUserInfo() {InboxUserName = LstItemUserName, InboxUserImage = imgUrl});
                    }
                }
            }
        }

        private void FbMessengerListCommandHandler(object obj)
        {
            GetFbMessengerListData();
        }

        #endregion
       

        #region Property

        #region User Info Details
      
        public ObservableCollection<FbpageInboxUserInfo> UserListInfo
        {

            get { return _userListInfo; }
            set
            {
                _userListInfo = value;
                OnPropertyChanged("UserListInfo");

            }
        }
        public FbpageInboxUserInfo SelectedUserInfo
        {

            get { return _selectedUserInfo; }
            set
            {
                _selectedUserInfo = value;
                BindUserMessage(SelectedUserInfo);
                OnPropertyChanged("SelectedUserInfo");

            }
        }


        #endregion



        #region FbPageListmembers Details
        public ObservableCollection<FacebookPageInboxmember> FbPageListmembers
        {
            get { return _fbPageInboxmember; }
            set { _fbPageInboxmember = value; }
        }

        #endregion

        #region FbPageListmembers Details
        public ObservableCollection<InstaInboxmember> InstaListmembers
        {
            get { return _instaInboxmember; }
            set { _instaInboxmember = value; }
        }

        #endregion



        #region Messaging Info

        public ObservableCollection<FbUserMessageInfo> MessagingListInfo
        {

            get { return messagingListInfo; }
            set
            {
                messagingListInfo = value;
                OnPropertyChanged("MessagingListInfo");

            }
        }
        
        #endregion

        #endregion

        #region Commands
     
        public DelegateCommand SendMessageCommand { get; set; }
        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand FbMessengerListCommand { get; set; }
        public DelegateCommand FbPageInboxCommand { get; set; }
        public DelegateCommand IntaInboxCommand { get; set; }
       

        #endregion
        #region Method
        private void BindUserInfo()
        {
            //BindUserInfoByApi();
            UserListInfo.Add(new FbpageInboxUserInfo { InboxUserName = "rahul baba" });
            UserListInfo.Add(new FbpageInboxUserInfo { InboxUserName = "YoYO baba" });
            UserListInfo.Add(new FbpageInboxUserInfo { InboxUserName = "Tiger baba" });

        }
        
        private void BindUserMessage(FbpageInboxUserInfo fbpageInboxUserInfo)
        {
            //Data Retrive
            MessagingListInfo = new ObservableCollection<FbUserMessageInfo>();
            if (fbpageInboxUserInfo.InboxUserName.Equals("YoYO baba"))
            {
                MessagingListInfo.Add(new FbUserMessageInfo { UserType = 0, Message = "rahul baba" });
                MessagingListInfo.Add(new FbUserMessageInfo { UserType = 1, Message = "YoYO baba" });
                MessagingListInfo.Add(new FbUserMessageInfo { UserType = 0, Message = "Tiger baba" });
            }
            else if (fbpageInboxUserInfo.InboxUserName.Equals("Tiger baba"))
            {
                MessagingListInfo.Add(new FbUserMessageInfo { UserType = 0, Message = "Gaurav baba" });
                MessagingListInfo.Add(new FbUserMessageInfo { UserType = 1, Message = "Sonam baba" });
                MessagingListInfo.Add(new FbUserMessageInfo { UserType = 0, Message = "kajal baba" });
            }
            else
            {
                //MessagingListInfo.Add(new FbUserMessageInfo { UserType = 0, Message = "Deepak baba" });
                //MessagingListInfo.Add(new FbUserMessageInfo { UserType = 1, Message = "Saurab baba" });
                //MessagingListInfo.Add(new FbUserMessageInfo { UserType = 0, Message = "abi baba" });

                GetUserChatBoxHistory();

            }
        }

        private void GetUserChatBoxHistory()
        {
            try
            {
                //ChromeWebDriver.Navigate().GoToUrl("https://www.facebook.com/pages/?category=your_pages");
                ChromeWebDriver.Navigate().GoToUrl("https://www.facebook.com/TP-1996120520653285/inbox/?selected_item_id=100002948674558");
                Thread.Sleep(2000);
                ReadOnlyCollection<IWebElement> yourPageMainNode = ChromeWebDriver.FindElements(By.ClassName("_2evr"));
                if (yourPageMainNode.Count > 0)
                {
                    ReadOnlyCollection<IWebElement> uiScrollableAreaContent = ChromeWebDriver.FindElements(By.ClassName("_2evr"));
                    if (uiScrollableAreaContent.Count > 0)
                    {
                        ReadOnlyCollection<IWebElement> RightNodeCollection = ChromeWebDriver.FindElements(By.XPath("//*[@class='clearfix _o46 _3erg _29_7 direction_ltr text_align_ltr']"));
                        if (RightNodeCollection.Count > 0)
                        {
                            for (int i = 0; i < RightNodeCollection.Count;i++ )
                            {
                                var rightsideText = RightNodeCollection[i].Text;
                                MessagingListInfo.Add(new FbUserMessageInfo { UserType = 0, Message = rightsideText });
                            }
                        }
                        ReadOnlyCollection<IWebElement> LeftNodeCollection = ChromeWebDriver.FindElements(By.XPath("//*[@class='clearfix _o46 _3erg _3i_m _nd_ direction_ltr text_align_ltr']"));
                        if (LeftNodeCollection.Count > 0)
                        {
                            for (int i = 0; i < LeftNodeCollection.Count; i++)
                            {
                                var leftsideText = LeftNodeCollection[i].Text;
                                MessagingListInfo.Add(new FbUserMessageInfo { UserType = 1, Message = leftsideText });
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

        public void SendMessageCommandHandler(object j)
        {
            _worker.DoWork += worker_DoWork;
            _worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            _worker.RunWorkerAsync();

           MessagingListInfo.Add(new FbUserMessageInfo { UserType = 0, Message = TextBxValue });
         
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           // throw new NotImplementedException();
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //MessagingListInfo.Add(new FbUserMessageInfo { UserType = 0, Message = textBxValue });
           // string textcommet = (new TextRange(RichTextBoxmsngr.Document.ContentStart, RichTextBoxmsngr.Document.ContentEnd).Text).Trim();
            //string textcommet = TextBox2.Text;
            try
            {
           ChromeWebDriver.Navigate().GoToUrl("https://www.facebook.com/TP-1996120520653285/inbox/?selected_item_id=100002948674558");
           Thread.Sleep(2000);
              //  ChromeWebDriver.Navigate().GoToUrl("https://www.facebook.com/pages/?category=your_pages");
                string pageSource = ChromeWebDriver.PageSource;

                       
                                    ReadOnlyCollection<IWebElement> writeNode =
                                           ChromeWebDriver.FindElements(By.XPath("//*[@placeholder='Write a reply...']"));
                                    if (writeNode.Count > 0)
                                    {
                                        Thread.Sleep(2000);
                                        writeNode[0].SendKeys(TextBxValue);
                                        Thread.Sleep(2000);
                                    }
                                    ReadOnlyCollection<IWebElement> submitnode =
                                           ChromeWebDriver.FindElements(By.XPath("//*[@type='submit']"));
                                    if (submitnode.Count > 0)
                                    {
                                        Thread.Sleep(2000);
                                        submitnode[1].Click();
                                        Thread.Sleep(2000);
                                    }
                      
                      
            }
          
             
            
            catch (Exception ex)
            {

            }
        }
        #endregion


        public string LstfbItemUserName { get; set; }
    }
}
