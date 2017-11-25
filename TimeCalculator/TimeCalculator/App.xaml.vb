Partial Public Class App
    Inherits Application

    ''' <summary>
    ''' Обеспечивает быстрый доступ к корневому кадру приложения телефона.
    ''' </summary>
    ''' <returns>Корневой кадр приложения телефона.</returns>
    Public ReadOnly Property RootFrame As PhoneApplicationFrame
        Get
            Return _rootFrame
        End Get
    End Property
    Private _rootFrame As PhoneApplicationFrame

    ''' <summary>
    ''' Предоставляет приложению доступ к ContentManager.
    ''' </summary>
    Public ReadOnly Property Content As ContentManager
        Get
            Return _content
        End Get
    End Property
    Private _content As ContentManager

    ''' <summary>
    ''' Обеспечивает доступ к GameTimer, настроенному для передачи данных в FrameworkDispatcher.
    ''' </summary>
    Public ReadOnly Property FrameworkDispatcherTimer As GameTimer
        Get
            Return _frameworkDispatcherTimer
        End Get
    End Property
    Private _frameworkDispatcherTimer As GameTimer

    ''' <summary>
    ''' Предоставляет доступ к AppServiceProvider для приложения.
    ''' </summary>
    Public ReadOnly Property Services As AppServiceProvider
        Get
            Return _appServiceProvider
        End Get
    End Property
    Private _appServiceProvider As AppServiceProvider

    ''' <summary>
    ''' Конструктор объекта приложения.
    ''' </summary>
    Public Sub New()
        ' Стандартная инициализация Silverlight
        InitializeComponent()

        ' Инициализация телефона
        InitializePhoneApplication()

        ' Инициализация XNA
        InitializeXnaApplication()

        ' Отображение сведений о профиле графики во время отладки.
        If System.Diagnostics.Debugger.IsAttached Then
            ' Отображение текущих счетчиков частоты смены кадров.
            Application.Current.Host.Settings.EnableFrameRateCounter = True

            ' Отображение областей приложения, перерисовываемых в каждом кадре.
            'Application.Current.Host.Settings.EnableRedrawRegions = True

            ' Включение режима визуализации анализа нерабочего кода, 
            ' который показывает в виде цветных слоев области на странице, передаваемые на графический ускоритель.
            'Application.Current.Host.Settings.EnableCacheVisualization = True

            ' Отключите обнаружение простоя приложения, установив для свойства UserIdleDetectionMode
            ' объекта PhoneApplicationService приложения значение Disabled.
            ' Внимание! Используйте только в режиме отладки. Приложение, в котором отключено обнаружение бездействия пользователя, будет продолжать работать
            ' и потреблять энергию батареи, когда телефон не будет использоваться.
            Application.Current.Host.Settings.EnableFrameRateCounter = True
        End If
    End Sub

    ' Код для выполнения при запуске приложения (например, из меню "Пуск")
    ' Этот код не будет выполняться при повторной активации приложения
    Private Sub Application_Launching(sender As Object, e As LaunchingEventArgs)
    End Sub

    ' Код для выполнения при активации приложения (переводится в основной режим)
    ' Этот код не будет выполняться при первом запуске приложения
    Private Sub Application_Activated(sender As Object, e As ActivatedEventArgs)
    End Sub

    ' Код для выполнения при деактивации приложения (отправляется в фоновый режим)
    ' Этот код не будет выполняться при закрытии приложения
    Private Sub Application_Deactivated(sender As Object, e As DeactivatedEventArgs)
    End Sub

    ' Код для выполнения при закрытии приложения (например, при нажатии пользователем кнопки "Назад")
    ' Этот код не будет выполняться при деактивации приложения
    Private Sub Application_Closing(sender As Object, e As ClosingEventArgs)
    End Sub

    ' Код для выполнения в случае ошибки навигации
    Private Sub RootFrame_NavigationFailed(sender As Object, e As NavigationFailedEventArgs)
        If System.Diagnostics.Debugger.IsAttached Then
            ' Ошибка навигации; перейти в отладчик
            System.Diagnostics.Debugger.Break()
        End If
    End Sub

    ' Код для выполнения на необработанных исключениях
    Private Sub Application_UnhandledException(sender As Object, e As ApplicationUnhandledExceptionEventArgs) Handles Me.UnhandledException
        If System.Diagnostics.Debugger.IsAttached Then
            ' Произошло необработанное исключение; перейти в отладчик
            System.Diagnostics.Debugger.Break()
        End If
    End Sub

#Region "Инициализация приложения телефона"

    ' Избегайте двойной инициализации
    Private phoneApplicationInitialized As Boolean = False

    ' Не добавляйте в этот метод дополнительный код
    Private Sub InitializePhoneApplication()
        If phoneApplicationInitialized Then
            Return
        End If

        ' Создайте кадр, но не задавайте для него значение RootVisual; это позволит
        ' экрану-заставке оставаться активным, пока приложение не будет готово для визуализации.
        _rootFrame = New PhoneApplicationFrame()
        AddHandler RootFrame.Navigated, AddressOf CompleteInitializePhoneApplication

        ' Обработка сбоев навигации
        AddHandler RootFrame.NavigationFailed, AddressOf RootFrame_NavigationFailed

        ' Убедитесь, что инициализация не выполняется повторно
        phoneApplicationInitialized = True
    End Sub

    ' Не добавляйте в этот метод дополнительный код
    Private Sub CompleteInitializePhoneApplication(sender As Object, e As NavigationEventArgs)
        ' Задайте корневой визуальный элемент для визуализации приложения
        If RootVisual IsNot RootFrame Then
            RootVisual = RootFrame
        End If

        ' Удалите этот обработчик, т.к. он больше не нужен
        RemoveHandler RootFrame.Navigated, AddressOf CompleteInitializePhoneApplication
    End Sub

#End Region

#Region "Инициализация приложения XNA"

    ' Выполняет инициализацию типов XNA, необходимых для приложения.
    Private Sub InitializeXnaApplication()
        ' Создание поставщика услуг
        _appServiceProvider = New AppServiceProvider()

        ' Добавить SharedGraphicsDeviceManager к службам в качестве IGraphicsDeviceService для приложения
        For Each obj As Object In ApplicationLifetimeObjects
            If obj Is GetType(IGraphicsDeviceService) Then
                Services.AddService(GetType(IGraphicsDeviceService), obj)
            End If
        Next

        ' Создайте ContentManager для загрузки в приложение предварительно скомпилированных ресурсов
        _content = New ContentManager(Services, "Содержимое")

        ' Создайте GameTimer для получения XNA FrameworkDispatcher
        _frameworkDispatcherTimer = New GameTimer()
        AddHandler FrameworkDispatcherTimer.FrameAction, AddressOf FrameworkDispatcherFrameAction
        FrameworkDispatcherTimer.Start()
    End Sub

    ' Обработчик событий передает FrameworkDispatcher во все кадры.
    ' FrameworkDispatcher требуется для большинства событий XNA и
    ' некоторых функций, таких как воспроизведение SoundEffect.
    Private Sub FrameworkDispatcherFrameAction(sender As Object, e As EventArgs)
        FrameworkDispatcher.Update()
    End Sub

#End Region
End Class
