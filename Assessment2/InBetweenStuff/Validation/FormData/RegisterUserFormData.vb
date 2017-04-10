
Namespace Validation.FormData

    <Serializable()>
    Public Class RegisterUserFormData
        Inherits UserSettingsFormData

        Public Overrides Function Validate() As ValidationResult
            Dim Result As New ValidationResult
            ValidateUserName(Result)
            ValidateEmail(Result)
            ValidatePassword(Result)
            ValidateConfirmPassword(Result)
            Return Result
        End Function

    End Class

End Namespace
