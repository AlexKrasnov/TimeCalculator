Partial Public Class GamePage
    Inherits PhoneApplicationPage
    Private contentManager As ContentManager
    Private timer As GameTimer
    Private spriteBatch As SpriteBatch

    Public Sub New()
        InitializeComponent()

        ' Получить диспетчер содержимого из приложения
        contentManager = DirectCast(Application.Current, App).Content

        ' Создайте таймер для этой страницы
        timer = New GameTimer()
        timer.UpdateInterval = TimeSpan.FromTicks(333333)
        AddHandler timer.Update, AddressOf OnUpdate
        AddHandler timer.Draw, AddressOf OnDraw
    End Sub

    Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
        ' В графическом устройстве включите визуализацию XNA для режима совместного использования
        SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(True)

        ' Создание нового SpriteBatch, который может использоваться для рисования текстур.
        spriteBatch = New SpriteBatch(SharedGraphicsDeviceManager.Current.GraphicsDevice)

        ' TODO: загрузите сюда содержимое игры с помощью Me.content

        ' Запуск таймера
        timer.Start()

        MyBase.OnNavigatedTo(e)
    End Sub

    Protected Overrides Sub OnNavigatedFrom(e As NavigationEventArgs)
        ' Остановка таймера
        timer.Stop()

        ' В графическом устройстве выключите визуализацию XNA для режима совместного использования
        SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(False)

        MyBase.OnNavigatedFrom(e)
    End Sub

    ''' <summary>
    ''' Позволяет странице выполнять логику, такую как обновление окружения,
    ''' поиск конфликтов, сбор входных данных и воспроизведение звука.
    ''' </summary>
    Private Sub OnUpdate(sender As Object, e As GameTimerEventArgs)
        ' TODO: добавьте здесь логику обновления
    End Sub

    ''' <summary>
    ''' Разрешает самостоятельную прорисовку страницы.
    ''' </summary>
    Private Sub OnDraw(sender As Object, e As GameTimerEventArgs)
        SharedGraphicsDeviceManager.Current.GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue)

        ' TODO: добавьте здесь свой код прорисовки
    End Sub
End Class
