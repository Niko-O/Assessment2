
Namespace Validation.FormData

    <Serializable()>
    Public MustInherit Class UserSettingsFormData
        Inherits FormData

        Public Const UserNameSpecification As String = "The user name must be 1 to 100 characters long and must only contain alphanumeric characters, dashes (-) and underscores (_)."
        Public Const PasswordLengthSpecification As String = "The password must be at least 10 characters long (which is already pushing it)" ' and cannot be longer than 72 characters because bcrypt sucks."
        Public Const PasswordComplexitySpecification As String = "The password must contain at least one character of each of 3 of these 4 character classes: Upper case characters, lower case characters, numbers and these special characters:" & SpecialCharacterClass
        Public Const UpperCaseCharacterClass As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Public Const LowerCaseCharacterClass As String = "abcdefghijklmnopqrstuvwxyz"
        Public Const NumberCharacterClass As String = "0123456789"
        Public Const SpecialCharacterClass As String = "\'§\\!""$%&/()=?{[]}+*#,;.:-_<>|"

        Public Shared ReadOnly UserNameRegex As New System.Text.RegularExpressions.Regex("^[a-zA-Z0-9\-_]{1,100}$", RegexOptions.Compiled)
        Public Shared ReadOnly EmailRegex As New System.Text.RegularExpressions.Regex("^[^\s]+@[^\s]+$", RegexOptions.Compiled)

        Public Property UserName As String
        Public Property Email As String
        Public Property Password As String
        Public Property ConfirmPassword As String

        Protected Sub ValidateUserName(Result As ValidationResult)
            Using Entities As New Models.BrisDataEntities
                If Entities.User.SingleOrDefault(Function(i) i.Name = UserName) Is Nothing Then
                    If Not UserNameRegex.IsMatch(UserName) Then
                        Result.AddError("UserName", UserNameSpecification)
                    End If
                Else
                    Result.AddError("UserName", "This name is already taken by someone.")
                End If
            End Using
        End Sub

        Protected Sub ValidateEmail(Result As ValidationResult)
            Using Entities As New Models.BrisDataEntities
                If Entities.User.SingleOrDefault(Function(i) i.Email = Email) Is Nothing Then
                    If Not EmailRegex.IsMatch(Email) Then
                        Result.AddError("Email", "There's gotta be some text, an @ and some more text.")
                    End If
                Else
                    Result.AddError("Email", "There is alreay a user with this E-Mail address.")
                End If
            End Using
        End Sub

        Protected Sub ValidatePassword(Result As ValidationResult)
            If Password.Length < 10 Then
                Result.AddError("Password", "The password is not long enough." & Environment.NewLine & PasswordLengthSpecification)
                'Else If Password.Length > 72
                'Result.AddError("Password", "I love your enthusiasm, but... the password is longer than what the technical limitation of bcrypt allows. For some reason it just ignores everything over 72 characters. Sorry." & Environment.NewLine & PasswordLengthSpecification)
            End If
            Dim AllCharacterClasses = {UpperCaseCharacterClass, LowerCaseCharacterClass, NumberCharacterClass, SpecialCharacterClass}
            Dim NumberOfFittingCharacterClasses = AllCharacterClasses.Count(Function(i) i.Any(AddressOf Password.Contains))
            If NumberOfFittingCharacterClasses < 3 Then
                Result.AddError("Password", "The password only contains characters of " & NumberOfFittingCharacterClasses & " of the necessary 3 of 4 character classes." & Environment.NewLine & PasswordComplexitySpecification)
            End If
        End Sub

        Protected Sub ValidateConfirmPassword(Result As ValidationResult)
            If Password <> ConfirmPassword Then
                Result.AddError("ConfirmPassword", "The confirmation password does not match the one entered above.")
            End If
        End Sub

    End Class

End Namespace
