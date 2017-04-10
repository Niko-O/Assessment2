
Namespace Validation

    <Serializable()>
    Public Class ValidationResult

        Public Property IsValid As Boolean = True
        Public Property ValidationErrors As New List(Of ValidationError)
        Public Property GeneralValidationErrorMessage As String = Nothing

        Public Sub AddError(FieldName As String, ErrorMessage As String)
            IsValid = False
            ValidationErrors.Add(New ValidationError(FieldName, ErrorMessage))
        End Sub

    End Class

End Namespace
