
Namespace Validation

    Public Class ValidationFailedException
        Inherits Exception

        Dim _FormData As FormData.FormData
        Public ReadOnly Property FormData As FormData.FormData
            <DebuggerStepThrough()>
            Get
                Return _FormData
            End Get
        End Property

        Dim _ValidationResult As ValidationResult
        Public ReadOnly Property ValidationResult As ValidationResult
            <DebuggerStepThrough()>
            Get
                Return _ValidationResult
            End Get
        End Property

        Public Sub New(NewMessage As String, NewFormData As FormData.FormData, NewValidationResult As ValidationResult)
            MyBase.New(NewMessage)
            _FormData = NewFormData
            _ValidationResult = NewValidationResult
        End Sub

    End Class

End Namespace
