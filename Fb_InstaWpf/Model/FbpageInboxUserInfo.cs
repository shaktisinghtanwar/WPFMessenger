using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Fb_InstaWpf.Model
{
   public class FbpageInboxUserInfo
    {
       // public int InboxUserId { get; set; }
        public string InboxUserName { get; set; }
        public string InboxUserImage { get; set; }

    }

   public class FacebookPageInboxmember
   {
       public string FbPageName { get; set; }
       public string FbPageImage { get; set; }

   }

   public class InstaInboxmember
   {
       public string InstaInboxUserName { get; set; }
       public string InstaInboxUserImage { get; set; }

   }

}
