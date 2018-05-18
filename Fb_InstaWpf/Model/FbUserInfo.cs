using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Fb_InstaWpf.Model
{
   public class FbUserInfo :INotifyPropertyChanged
    {
       private bool _select;
       private int _id;
       private string _message;
       private string _pageUrl;

       public bool Select
       {
           get { return _select; }
           set { _select = value;OnPropertyChanged(); }
       }

       public int Id
       {
           get { return _id; }
           set { _id = value; OnPropertyChanged(); }
       }

       public string Message
       {
           get { return _message; }
           set { _message = value; OnPropertyChanged(); }
       }

       public string PageUrl
       {
           get { return _pageUrl; }
           set { _pageUrl = value; OnPropertyChanged(); }
       }

       public event PropertyChangedEventHandler PropertyChanged;

       protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
       {
           var handler = PropertyChanged;
           if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
       }
    }
}
