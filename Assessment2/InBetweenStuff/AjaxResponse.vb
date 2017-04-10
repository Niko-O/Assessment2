Public Class AjaxResponse

    Dim _Success As Boolean
    Public ReadOnly Property Success As Boolean
        <DebuggerStepThrough()>
        Get
            Return _Success
        End Get
    End Property

    Dim _Data As Object
    Public ReadOnly Property Data As Object
        <DebuggerStepThrough()>
        Get
            Return _Data
        End Get
    End Property

    Dim _ErrorMessage As String
    Public ReadOnly Property ErrorMessage As String
        <DebuggerStepThrough()>
        Get
            Return _ErrorMessage
        End Get
    End Property

    Private Sub New(NewSuccess As Boolean, NewData As Object, NewErrorMessage As String)
        _Success = NewSuccess
        _Data = NewData
        _ErrorMessage = NewErrorMessage
    End Sub

    Public Shared Function FromFail(NewErrorMessage As String) As AjaxResponse
        Return New AjaxResponse(False, Nothing, NewErrorMessage)
    End Function

    Public Shared Function FromSuccess(NewData As Object) As AjaxResponse
        Return New AjaxResponse(True, NewData, Nothing)
    End Function

End Class
