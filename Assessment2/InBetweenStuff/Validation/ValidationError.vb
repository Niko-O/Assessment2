
Namespace Validation

    Public Structure ValidationError

        Private _FieldName As String
        Public ReadOnly Property FieldName As String
            <DebuggerStepThrough()>
            Get
                Return _FieldName
            End Get
        End Property

        Private _ErrorMessage As String
        Public ReadOnly Property ErrorMessage As String
            <DebuggerStepThrough()>
            Get
                Return _ErrorMessage
            End Get
        End Property

        Public Sub New(NewFieldName As String, NewErrorMessage As String)
            _FieldName = NewFieldName
            _ErrorMessage = NewErrorMessage
        End Sub

    End Structure

End Namespace
