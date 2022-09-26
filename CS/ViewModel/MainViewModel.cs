using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace Example.ViewModel {
    public class MainViewModel : ViewModelBase {
        RegistrationViewModel registrationViewModel;

        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(); } }
        IDialogService DialogService { get { return GetService<IDialogService>(); } }

        public MainViewModel() {
            registrationViewModel = new RegistrationViewModel();
        }

        [Command]
        public void ShowRegistrationForm() {
            UICommand registerAsyncCommand = new UICommand(
               id: null,
               caption: "Register Async",
               command: new AsyncCommand<CancelEventArgs>(
                   async cancelArgs => {
                       try {
                           await MyAsyncExecuteMethod();
                       }
                       catch (Exception e) {
                           MessageBoxService.ShowMessage(e.Message, "Error", MessageButton.OK);
                           cancelArgs.Cancel = true;
                       }
                   },
                   cancelArgs => !string.IsNullOrEmpty(registrationViewModel.UserName)
               ),
               asyncDisplayMode: AsyncDisplayMode.Wait,
               isDefault: false,
               isCancel: false
           );

            UICommand registerCommand = new UICommand(
                id: null,
                caption: "Register",
                command: new DelegateCommand<CancelEventArgs>(
                    cancelArgs => {
                        try {
                            MyExecuteMethod();
                        }
                        catch (Exception e) {
                            MessageBoxService.ShowMessage(e.Message, "Error", MessageButton.OK);
                            cancelArgs.Cancel = true;
                        }
                    },
                    cancelArgs => !string.IsNullOrEmpty(registrationViewModel.UserName) && !((IAsyncCommand)registerAsyncCommand.Command).IsExecuting
                ),
                isDefault: true,
                isCancel: false
            );

            UICommand cancelCommand = new UICommand(
                id: MessageBoxResult.Cancel,
                caption: "Cancel",
                command: null,
                isDefault: false,
                isCancel: true
            );

            UICommand result = DialogService.ShowDialog(
                dialogCommands: new List<UICommand>() { registerAsyncCommand, registerCommand, cancelCommand },
                title: "Registration Dialog",
                viewModel: registrationViewModel
            );

            if (result == registerCommand) {
                // ...
            }
        }

        void MyExecuteMethod() {
            // ...
        }

        async Task MyAsyncExecuteMethod() {
            await Task.Delay(2000);
            // ...
        }
    }
}
