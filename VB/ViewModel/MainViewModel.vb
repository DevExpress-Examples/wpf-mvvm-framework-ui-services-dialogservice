Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Windows

Namespace Example.ViewModel
	Public Class MainViewModel
		Protected ReadOnly Property DialogService() As IDialogService
			Get
				Return Me.GetService(Of IDialogService)()
			End Get
		End Property
		Private registrationViewModel As RegistrationViewModel = Nothing
		Public Sub ShowRegistrationForm()
			If registrationViewModel Is Nothing Then
				registrationViewModel = New RegistrationViewModel()
			End If

			Dim registerCommand As New UICommand(id:= Nothing, caption:= "Register", command:= New DelegateCommand(Of CancelEventArgs)(Sub(cancelArgs)
				Try
					myExecuteMethod()
				Catch e As Exception
					Me.GetService(Of IMessageBoxService)().ShowMessage(e.Message, "Error", MessageButton.OK)
					cancelArgs.Cancel = True
				End Try
			End Sub, cancelArgs{get{Return Not String.IsNullOrEmpty(registrationViewModel.UserName)), isDefault:= True, isCancel:= False)
		End Sub
	End Class

			Private cancelCommand As New UICommand(id:= MessageBoxResult.Cancel, caption:= "Cancel", command:= Nothing, isDefault:= False, isCancel:= True)

			Private result As UICommand = DialogService.ShowDialog(dialogCommands:= New List(Of UICommand)() From {registerCommand, cancelCommand}, title:= "Registration Dialog", viewModel:= registrationViewModel)

			If result = registerCommand Then
				'...
			End If
End Namespace

'INSTANT VB TODO TASK: Local functions are not converted by Instant VB:
'		private void myExecuteMethod()
'		{
'			Debug.Print("Registration complete");
'		}
	}
}
