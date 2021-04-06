using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace Example.ViewModel {
    public class MainViewModel {
        protected IDialogService DialogService { get { return this.GetService<IDialogService>(); } }
        RegistrationViewModel registrationViewModel = null;
        public void ShowRegistrationForm() {
            if(registrationViewModel == null)
                registrationViewModel = new RegistrationViewModel();

            UICommand registerCommand = new UICommand(
                id: null,
                caption: "Register",
                command: new DelegateCommand<CancelEventArgs>(
                    cancelArgs => {
                        try {
                            myExecuteMethod();
                        }
                        catch (Exception e) {
                            this.GetService<IMessageBoxService>().ShowMessage(e.Message, "Error", MessageButton.OK);
                            cancelArgs.Cancel = true;
                        }
                    },
                    cancelArgs => !string.IsNullOrEmpty(registrationViewModel.UserName)
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
                dialogCommands: new List<UICommand>() { registerCommand, cancelCommand },
                title: "Registration Dialog",
                viewModel: registrationViewModel
            );

            if (result == registerCommand) {
                //...
            }
        }

        private void myExecuteMethod() {
            Debug.Print("Registration complete");
        }
    }
}
