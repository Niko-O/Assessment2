
Namespace Validation.FormData

    <Serializable()>
    Public Class EditUserSettingsFormData
        Inherits UserSettingsFormData

        Public Property UserId As Guid

        Public Overrides Function Validate() As ValidationResult
            Dim Result As New ValidationResult
            Using Entities As New Models.BrisDataEntities
                Dim ExistingUser = Entities.User.Single(Function(i) i.Id = UserId)
                If ExistingUser.Name <> UserName Then
                    ValidateUserName(Result)
                End If
                If ExistingUser.Email <> Email Then
                    ValidateEmail(Result)
                End If
                If Not String.IsNullOrEmpty(Password) Then
                    ValidatePassword(Result)
                End If
                ValidateConfirmPassword(Result)
            End Using
            Return Result
        End Function

    End Class

End Namespace
