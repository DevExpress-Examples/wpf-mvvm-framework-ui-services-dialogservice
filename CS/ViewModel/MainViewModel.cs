using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Example.ViewModel {
public class MainViewModel {
    protected IDialogService DialogService { get { return this.GetService<IDialogService>(); } }
    RegistrationViewModel registrationViewModel = null;
    public void ShowRegistrationForm() {
        if(registrationViewModel == null)
            registrationViewModel = new RegistrationViewModel();

        UICommand registerCommand = new UICommand() {
            Caption = "Register",
            IsCancel = false,
            IsDefault = true,
            Command = new DelegateCommand<CancelEventArgs>(
                x => myExecuteMethod(),
                x => !string.IsNullOrEmpty(registrationViewModel.UserName)),
        };
        
        UICommand cancelCommand = new UICommand() {
            Id = MessageBoxResult.Cancel,
            Caption = "Cancel",
            IsCancel = true,
            IsDefault = false,
        };

        UICommand result = DialogService.ShowDialog(
            dialogCommands: new List<UICommand>() { registerCommand, cancelCommand },
            title: "Registration Dialog",
            viewModel: registrationViewModel);

        if (result == registerCommand) {
            //...
        }
    }

    private void myExecuteMethod() {
        Debug.Print("Registration complete");
    }
}
}
