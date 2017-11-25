''' <summary>
''' Implements IServiceProvider for the application. This type is exposed through the App.Services
''' property and can be used for ContentManagers or other types that need access to an IServiceProvider.
''' </summary>
Public Class AppServiceProvider
    Implements IServiceProvider

    ' A map of service type to the services themselves
    Private ReadOnly _services As New Dictionary(Of Type, Object)

    ''' <summary>
    ''' Adds a new service to the service provider.
    ''' </summary>
    ''' <param name="serviceType">The type of service to add.</param>
    ''' <param name="service">The service object itself.</param>
    Public Sub AddService(serviceType As Type, service As Object)
        ' Validate the input
        If serviceType Is Nothing Then
            Throw New ArgumentNullException("serviceType")
        End If

        If service Is Nothing Then
            Throw New ArgumentNullException("service")
        End If

        If Not serviceType.IsAssignableFrom(service.GetType()) Then
            Throw New ArgumentException("service does not match the specified serviceType")
        End If

        ' Add the service to the dictionary
        _services.Add(serviceType, service)
    End Sub


    ''' <summary>
    ''' Gets a service from the service provider.
    ''' </summary>
    ''' <param name="serviceType">The type of service to retrieve.</param>
    ''' <returns>The service object registered for the specified type..</returns>
    Public Function GetService(serviceType As Type) As Object Implements IServiceProvider.GetService
        ' Validate the input
        If serviceType Is Nothing Then
            Throw New ArgumentNullException("serviceType")
        End If

        ' Retrieve the service from the dictionary
        Return _services(serviceType)
    End Function

    ''' <summary>
    ''' Removes a service from the service provider.
    ''' </summary>
    ''' <param name="serviceType">The type of service to remove.</param>
    Public Sub RemoveService(serviceType As Type)
        ' Validate the input
        If serviceType Is Nothing Then
            Throw New ArgumentNullException("serviceType")
        End If

        ' Remove the service from the dictionary
        _services.Remove(serviceType)
    End Sub
End Class