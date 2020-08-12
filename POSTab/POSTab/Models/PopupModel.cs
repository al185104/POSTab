using POSTab.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace POSTab.Models
{
    public class PopupModel : BindableObject
    {
        public string Title { get; set; }
        public string Head { get; set; }
        public string Body { get; set; }
        public string Feet { get; set; }
        public string OKButtonLabel { get; set; }
        public string YesButtonLabel { get; set; }
        public string NoButtonLabel { get; set; }
        public bool ToExit { get; set; }
        public int Delay { get; set; }
        public string Animation { get; set; }
        public bool IsProcessFooterVisible { get; set; }
        public bool IsCelebration { get; set; }
        public string ReturnViewModel { get; set; }
    }
}
