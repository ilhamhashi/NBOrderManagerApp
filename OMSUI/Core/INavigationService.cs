namespace OrderManagerDesktopUI.Core;
public interface INavigationService
{
    ViewModel CurrentNestedView { get; }
    ViewModel CurrentView { get; }
    void NavigateTo<T>() where T : ViewModel;
    void NavigateToNested<T>() where T : ViewModel;
}
