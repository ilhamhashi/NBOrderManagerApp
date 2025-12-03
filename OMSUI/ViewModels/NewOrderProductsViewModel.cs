using OrderManagerDesktopUI.Core;

namespace OrderManagerDesktopUI.ViewModels
{
    public class NewOrderProductsViewModel : ViewModel
    {
		private string noteText = "neworderproductsvm";

        public string NoteText
		{
			get { return noteText; }
			set { noteText = value; OnPropertyChanged(); }
		}



	}
}
