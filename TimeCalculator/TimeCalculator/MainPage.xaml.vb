Partial Public Class MainPage
    Inherits PhoneApplicationPage

    ' Конструктор
    Public Sub New()
        InitializeComponent()
    End Sub

    Property ClassTime As Object

    ' Простая кнопка Щелкните обработчик событий, чтобы перейти на вторую страницу
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        NavigationService.Navigate(New Uri("/GamePage.xaml", UriKind.Relative))
    End Sub

    Dim tc As ClassTime = New ClassTime()

    Private Function Check() As Boolean
        Dim n As Integer
        If (Int32.TryParse(BSH.Text, n) And Int32.TryParse(BSM.Text, n) And Int32.TryParse(BSS.Text, n) And Int32.TryParse(BEH.Text, n) And Int32.TryParse(BEM.Text, n) And Int32.TryParse(BES.Text, n)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub TimeCall()
        If Check() Then
            tc.StartHour = Convert.ToInt32(BSH.Text)
            tc.StartMin = Convert.ToInt32(BSM.Text)
            tc.StartSec = Convert.ToInt32(BSS.Text)
            tc.EndHour = Convert.ToInt32(BEH.Text)
            tc.EndMin = Convert.ToInt32(BEM.Text)
            tc.EndSec = Convert.ToInt32(BES.Text)
        End If
    End Sub

    Private Sub LayoutRoot_LostFocus(sender As Object, e As RoutedEventArgs) Handles LayoutRoot.LostFocus
        TimeCall()
        TB.Text = tc.Calculate()
    End Sub
End Class
