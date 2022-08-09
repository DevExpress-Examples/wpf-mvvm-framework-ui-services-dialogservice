Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Windows

Namespace Example.ViewModel

    Public Class MainViewModel

        Protected ReadOnly Property DialogService As IDialogService
            Get
                Return GetService(Of IDialogService)()
            End Get
        End Property

        Private registrationViewModel As RegistrationViewModel = Nothing

        Public Sub ShowRegistrationForm()
            If registrationViewModel Is Nothing Then registrationViewModel = New RegistrationViewModel()
            Dim registerCommand As UICommand = New UICommand() With {.Caption = "Register", .IsCancel = False, .IsDefault = True, .Command = New DelegateCommand(Of CancelEventArgs)(Sub(x) myExecuteMethod(), Function(x) Not String.IsNullOrEmpty(registrationViewModel.UserName))}
            Dim cancelCommand As UICommand = New UICommand() With {.Id = MessageBoxResult.Cancel, .Caption = "Cancel", .IsCancel = True, .IsDefault = False}
            Dim result As UICommand = DialogService.ShowDialog(dialogCommands:=New List(Of UICommand)() From {registerCommand, cancelCommand}, title:="Registration Dialog", viewModel:=registrationViewModel)
            If result Is registerCommand Then
            '...
            End If
        End Sub

        Private Sub myExecuteMethod()
            Call Debug.Print("Registration complete")
        End Sub
    End Class
End Namespace
