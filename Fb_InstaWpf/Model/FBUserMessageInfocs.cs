﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fb_InstaWpf.Model
{
  public  class FbUserMessageInfo
    {
        public string UserName { get; set; }
        public int UserType { get; set; }//0 Login User 1 for Other user
        public string Message { get; set; }
        public string Time { get; set; }
        public string otheruserimage { get; set; }
        public string loginguserimage { get; set; }
    
      public string otheruserFbimage { get; set; }
      public string loginguserFbimage { get; set; }

      
      public string loginguserInstaimage { get; set; }
      public string otheruserInstaimage { get; set; }

    }


 

    
}
