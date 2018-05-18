using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Fb_InstaWpf.Model;

namespace Fb_InstaWpf.Helper
{
    public class MessageDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OtherUserTemplate;
        public DataTemplate LoginUserTemplate;
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return null;
            }
            FrameworkElement FrameworkElement = container as FrameworkElement;
            if (FrameworkElement != null)
            {
                var fbUserMessageInfo = item as FbUserMessageInfo;
                if (fbUserMessageInfo != null)
                {
                    if (fbUserMessageInfo.UserType == 0)
                    {
                        LoginUserTemplate = FrameworkElement.FindResource("LoginUserDataTemplate") as DataTemplate;
                        return LoginUserTemplate;
                    }
                    else
                    {
                        OtherUserTemplate = FrameworkElement.FindResource("OtherUserDataTemplate") as DataTemplate;
                        return OtherUserTemplate;
                    }
                }
            }
            return null;
        }
    }
}
