' класс для работы с временными интервалами
Public Class ClassTime
    Dim StartSec_ As Integer 'начальное число секунд
    Dim StartMin_ As Integer 'начальное число минут
    Dim StartHour_ As Integer 'начальное число часов
    Dim EndSec_ As Integer 'конечное число секунд
    Dim EndMin_ As Integer 'конечное число минут
    Dim EndHour_ As Integer 'конечное число часов

    ' свойства
    Public Property StartSec As Integer
        Get
            Return StartSec_
        End Get
        Set(value As Integer)
            StartSec_ = value
        End Set
    End Property

    Public Property StartMin As Integer
        Get
            Return StartMin_
        End Get
        Set(value As Integer)
            StartMin_ = value
        End Set
    End Property

    Public Property StartHour As Integer
        Get
            Return StartHour_
        End Get
        Set(value As Integer)
            StartHour_ = value
        End Set
    End Property

    Public Property EndSec As Integer
        Get
            Return EndSec_
        End Get
        Set(value As Integer)
            EndSec_ = value
        End Set
    End Property

    Public Property EndMin As Integer
        Get
            Return EndMin_
        End Get
        Set(value As Integer)
            EndMin_ = value
        End Set
    End Property

    Public Property EndHour As Integer
        Get
            Return EndHour_
        End Get
        Set(value As Integer)
            EndHour_ = value
        End Set
    End Property

    Public Sub BorderCheck() ' Преобразование к общему виду
        If (StartSec_ > 60) Then
            StartMin_ += StartSec_ / 60
            StartSec_ -= 60
        End If
        If (StartMin_ > 60) Then
            StartHour_ += StartMin_ / 60
            StartMin_ -= 60
        End If
        If (EndSec_ > 60) Then
            EndMin_ += EndSec_ / 60
            EndSec_ -= 60
        End If
        If (EndMin_ > 60) Then
            EndHour_ += EndMin_ / 60
            EndMin_ -= 60
        End If
    End Sub

    Public Function ZeroCheck(value1 As Integer, value2 As Integer) As String ' Проверка на 0
        If (value1 - value2 = 0) Then
            Return "00"
        Else
            Dim d = value1 - value2
            If (d < 0) Then
                d = -d
            End If
            Return d.ToString
        End If
    End Function

    Public Function Calculate() As String ' Подсчёт
        BorderCheck()
        Return ZeroCheck(StartHour_, EndHour_) + ":" + ZeroCheck(StartMin_, EndMin_) + ":" + ZeroCheck(StartSec_, EndSec_)
    End Function

End Class
