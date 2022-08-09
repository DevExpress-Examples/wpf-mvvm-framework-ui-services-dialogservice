Imports System.Windows

Namespace Example

    Public Partial Class App
        Inherits Application

        Public Sub New()
            AddHandler Startup, AddressOf OnStartup
        End Sub

        Private Overloads Sub OnStartup(ByVal sender As Object, ByVal e As StartupEventArgs)
            MainWindow = New MainWindow()
            MainWindow.Show()
        End Sub
    End Class
End Namespace
