
Namespace Security

    Public Class Security

        Private Shared LoginDataCache As LoginData? = Nothing

        Public Shared Function CreateSalt(Length As Integer) As Byte()
            Using Rng As New System.Security.Cryptography.RNGCryptoServiceProvider
                Dim Result = New Byte(Length - 1) {}
                Rng.GetBytes(Result)
                Return Result
            End Using
        End Function

        Public Shared Function HashPasswordWithDefaultSettingsAndNewHash(PlaintextPassword As String) As PasswordHash
            Return HashPassword(PlaintextPassword, CreateSalt(512), 5000, 512)
        End Function

        Public Shared Function HashPassword(PlaintextPassword As String, Salt As Byte(), IterationCount As Integer, OutputLength As Integer) As PasswordHash
            Using DeriveBytes As New System.Security.Cryptography.Rfc2898DeriveBytes(PlaintextPassword, Salt, IterationCount)
                Return New PasswordHash(DeriveBytes.GetBytes(OutputLength), Salt, IterationCount, OutputLength)
            End Using
        End Function

        Public Shared Function VerifyPassword(PlaintextPasswordToVerify As String, ExistingPasswordHash As PasswordHash) As Boolean
            Dim NewHash = HashPassword(PlaintextPasswordToVerify, ExistingPasswordHash.Salt, ExistingPasswordHash.IterationCount, ExistingPasswordHash.OutputLength)
            Return NewHash.Hash.SequenceEqual(ExistingPasswordHash.Hash)
        End Function

        Public Shared Sub InvalidateLoginDataCache()
            LoginDataCache = Nothing
        End Sub

        Public Shared Function TryLogIn(UserName As String, PlaintextPassword As String) As TryLogInResult
            If TryGetLoggedInUserId() IsNot Nothing Then
                Throw New InvalidOperationException("There is already a user logged in. Call LogOut first.")
            End If
            Using Entities As New Models.BrisDataEntities
                Dim User = Entities.User.SingleOrDefault(Function(i) i.Name = UserName)
                If User Is Nothing Then
                    Return TryLogInResult.UserDoesNotExist
                End If
                If Not VerifyPassword(PlaintextPassword, PasswordHash.Parse(User.PasswordHash)) Then
                    Return TryLogInResult.WrongPassword
                End If
                If User.Role.Id = Models.DefaultRoles.UnverifiedUser Then
                    Return TryLogInResult.UserIsNotVerified
                End If
                SetLoggedInUserId(User.Id)
                LoginDataCache = LoginData.FromLoggedIn(User)
                Return TryLogInResult.Success
            End Using
        End Function

        Public Shared Sub LogOut()
            If LoginDataCache Is Nothing Then
                Throw New InvalidOperationException("There is no user logged in.")
            End If
            SetLoggedInUserId(Nothing)
            LoginDataCache = Nothing
        End Sub

        Public Shared Function GetLoginData() As LoginData
            If LoginDataCache Is Nothing Then
                LoginDataCache = GetLoginDataCore()
            End If
            Return LoginDataCache.Value
        End Function

        Private Shared Function GetLoginDataCore() As LoginData
            Dim Id = TryGetLoggedInUserId()
            Using Entities As New Models.BrisDataEntities
                If Id IsNot Nothing Then
                    Dim User = Entities.User.SingleOrDefault(Function(i) i.Id = Id.Value)
                    If User IsNot Nothing Then
                        Return LoginData.FromLoggedIn(User)
                    End If
                End If
                Return LoginData.FromGuest(Entities.RolePermission.Where(Function(i) i.FK_Role = Models.DefaultRoles.Guest).Select(Function(i) DirectCast(i.Permission, Models.Permission)))
            End Using
        End Function

        Private Shared Function TryGetLoggedInUserId() As Guid?
            Dim Temp = HttpContext.Current.Session("LoggedInUserId")
            If Temp Is Nothing Then
                Return Nothing
            End If
            Return DirectCast(Temp, Guid)
        End Function

        Private Shared Sub SetLoggedInUserId(Id As Guid?)
            If Id Is Nothing Then
                HttpContext.Current.Session("LoggedInUserId") = Nothing
            Else
                HttpContext.Current.Session("LoggedInUserId") = Id.Value
            End If
        End Sub

        Public Shared Function GenerateRoundtripData() As RoundtripData
            Dim RandomValue = New Byte(128 - 1) {}
            Using Rnd As New System.Security.Cryptography.RNGCryptoServiceProvider
                Rnd.GetBytes(RandomValue)
            End Using
            Dim UniqueValue = Guid.NewGuid
            Return New RoundtripData(RandomValue, UniqueValue)
        End Function

    End Class

End Namespace
