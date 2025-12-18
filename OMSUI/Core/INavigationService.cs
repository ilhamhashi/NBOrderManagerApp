namespace OrderManagerDesktopUI.Core;
public interface INavigationService
{
    ViewModelBase CurrentNestedView { get; }
    ViewModelBase CurrentView { get; }
    void NavigateTo<T>() where T : ViewModelBase;
    void NavigateToNested<T>() where T : ViewModelBase;
}
