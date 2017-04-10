
Namespace Security

    Public Structure TryLogInResult

        Private _UserExists As Boolean
        Public ReadOnly Property UserExists As Boolean
            <DebuggerStepThrough()>
            Get
                Return _UserExists
            End Get
        End Property

        Private _PasswordIsCorrect As Boolean
        Public ReadOnly Property PasswordIsCorrect As Boolean
            <DebuggerStepThrough()>
            Get
                Return _PasswordIsCorrect
            End Get
        End Property

        Private _UserIsVerified As Boolean
        Public ReadOnly Property UserIsVerified As Boolean
            <DebuggerStepThrough()>
            Get
                Return _UserIsVerified
            End Get
        End Property

        Private Sub New(NewUserExists As Boolean, NewPasswordIsCorrect As Boolean, NewUserIsVerified As Boolean)
            _UserExists = NewUserExists
            _PasswordIsCorrect = NewPasswordIsCorrect
            _UserIsVerified = NewUserIsVerified
        End Sub

        Public Shared Function UserDoesNotExist() As TryLogInResult
            Return New TryLogInResult(False, False, False)
        End Function

        Public Shared Function WrongPassword() As TryLogInResult
            Return New TryLogInResult(True, False, False)
        End Function

        Public Shared Function UserIsNotVerified() As TryLogInResult
            Return New TryLogInResult(True, True, False)
        End Function

        Public Shared Function Success() As TryLogInResult
            Return New TryLogInResult(True, True, True)
        End Function

    End Structure

End Namespace
