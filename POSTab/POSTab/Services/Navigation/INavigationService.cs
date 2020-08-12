using POSTab.ViewModels.Base;
using System.Threading.Tasks;

namespace POSTab.Services.Navigation
{
    public interface INavigationService
    {
		ViewModelBase PreviousPageViewModel { get; }

        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task NavigateToPopUpAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task NavigateToPopUpAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
        /// <summary>
        /// Remove the top object from the stack.
        /// </summary>
        /// <param name="popup"></param>
        /// <returns></returns>
        Task RemoveLastFromBackStackAsync(bool popup=false);
        /// <summary>
        /// Remove all objects in the stack
        /// </summary>
        /// <param name="popup"></param>
        /// <returns></returns>
        Task RemoveBackStackAsync(bool popup=false);
    }
}