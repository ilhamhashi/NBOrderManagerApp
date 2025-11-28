using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagerDesktopUI.ViewModels;

namespace OrderManagerDesktopUI.Core
{
    public interface INavigationService
    {
        public ViewModel CurrentView { get; set; }
        public void NavigateTo<T>() where T : ViewModel;
    }
}
