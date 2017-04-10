
Namespace Validation.FormData

    <Serializable()>
    Public Class LoginFormData
        Inherits FormData

        Public Property UserName As String
        Public Property Password As String

        Public Overrides Function Validate() As ValidationResult
            Dim Result As New ValidationResult
            Using Entities As New Models.BrisDataEntities
                If Entities.User.SingleOrDefault(Function(i) i.Name = UserName) Is Nothing Then
                    Result.AddError("UserName", "This User does not exist.")
                End If
            End Using
            Return Result
        End Function

    End Class

End Namespace
