
Namespace Validation.FormData

    <Serializable()>
    Public MustInherit Class FormData

        Public MustOverride Function Validate() As ValidationResult

        Public Sub AssertIsValid()
            Dim Result = Validate()
            If Not Result.IsValid Then
                Dim Lines As New List(Of String)
                If Not String.IsNullOrEmpty(Result.GeneralValidationErrorMessage) Then
                    Lines.Add(Result.GeneralValidationErrorMessage)
                End If
                For Each i In Result.ValidationErrors
                    Lines.Add(String.Format("Error in field '{0}': {1}", i.FieldName, i.ErrorMessage))
                Next
                Throw New ValidationFailedException(String.Join(Environment.NewLine, Lines), Me, Result)
            End If
        End Sub

    End Class

End Namespace
