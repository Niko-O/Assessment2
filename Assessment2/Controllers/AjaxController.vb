Public Class AjaxController
    Inherits System.Web.Mvc.Controller

    Private Shared ReadOnly FormDataTypes As New Dictionary(Of String, Type) From _
    {
        {"RegisterUser", GetType(Validation.FormData.RegisterUserFormData)},
        {"Login", GetType(Validation.FormData.LoginFormData)},
        {"EditOrCreateBanana", GetType(Validation.FormData.EditOrCreateBananaFormData)},
        {"EditUserSettings", GetType(Validation.FormData.EditUserSettingsFormData)}
    }

    <HttpPost()>
    Public Function ValidateFormData(Data As ValidateFormDataRequest) As ActionResult
        Dim Deserializer As New System.Web.Script.Serialization.JavaScriptSerializer
        Dim FormData = DirectCast(Deserializer.Deserialize(Data.FormData, FormDataTypes(Data.FormName)), Validation.FormData.FormData)
        Return Json(AjaxResponse.FromSuccess(FormData.Validate))
    End Function

    <HttpPost()>
    Public Function Login(FormData As Validation.FormData.LoginFormData) As ActionResult
        Dim LoginData = Security.Security.GetLoginData
        If LoginData.IsLoggedIn Then
            Return Json(AjaxResponse.FromFail("You are already logged in. Congratulations, you broke something."))
        End If
        FormData.AssertIsValid()
        Dim Result = Security.Security.TryLogIn(FormData.UserName, FormData.Password)
        If Not Result.UserExists Then
            Return Json(AjaxResponse.FromFail("This user does not exist."))
        End If
        If Not Result.PasswordIsCorrect Then
            Return Json(AjaxResponse.FromFail("The password is wrong."))
        End If
        If Not Result.UserIsVerified Then
            Return Json(AjaxResponse.FromFail("You are not verified."))
        End If
        Return Json(AjaxResponse.FromSuccess(Nothing))
    End Function

    <HttpPost()>
    Public Function Logout() As ActionResult
        Dim LoginData = Security.Security.GetLoginData
        If Not LoginData.IsLoggedIn Then
            Throw New UnauthorizedAccessException("Yeah, sure. Log out while you are not logged in.")
        End If
        Security.Security.LogOut()
        Return Json(AjaxResponse.FromSuccess(Nothing))
    End Function

    <HttpPost()>
    Public Function RegisterUser(FormData As Validation.FormData.RegisterUserFormData) As ActionResult
        Using Entities As New Models.BrisDataEntities
            If Entities.User.Any(Function(i) i.Name = FormData.UserName) Then
                Return Json(AjaxResponse.FromFail("Somebody took that user name just before you clicked the button."))
            End If
            FormData.AssertIsValid()
            Dim PasswordHash = Security.Security.HashPasswordWithDefaultSettingsAndNewHash(FormData.Password)
            Dim RoundtripData = Security.Security.GenerateRoundtripData
            Dim ExpirationTime = DateTime.Now.AddDays(7)
            Dim User As New Models.User With {.Id = Guid.NewGuid, .Name = FormData.UserName, .Email = FormData.Email, .PasswordHash = PasswordHash.ToString, .FK_Role = Models.DefaultRoles.UnverifiedUser}
            Entities.User.AddObject(User)
            Entities.SaveChanges()
            Dim Registration As New Models.RegistrationProcess With {.Id = Guid.NewGuid, .User = User, .FK_User = User.Id, .RandomValue = RoundtripData.RandomValue, .UniqueValue = RoundtripData.UniqueValue, .ExpirationDateTime = ExpirationTime}
            Entities.RegistrationProcess.AddObject(Registration)
            Entities.SaveChanges()
            Dim Debug_Mail = Mail.SendConfirmationMail(Registration)
            Return Json(AjaxResponse.FromSuccess(New With {.ExpirationDateTime = ExpirationTime.ToShortDateString & " " & ExpirationTime.ToLongTimeString, .Debug_Mail = Debug_Mail}))
        End Using
    End Function

    <HttpPost()>
    Public Function RoleHasUsers(RoleId As Guid) As ActionResult
        Dim LoginData = Security.Security.GetLoginData
        If Not LoginData.HasPermission(Models.Permission.EditRoles) Then
            Throw New UnauthorizedAccessException("You don't have the sufficient permissions to do this. Also: This is either a bug or you are manually sending requests.")
        End If
        Using Entities As New Models.BrisDataEntities
            Return Json(AjaxResponse.FromSuccess(Entities.User.Any(Function(i) i.FK_Role = RoleId)))
        End Using
    End Function

    <HttpPost()>
    Public Function DeleteRole(RoleId As Guid) As ActionResult
        Dim LoginData = Security.Security.GetLoginData
        If Not LoginData.HasPermission(Models.Permission.EditRoles) Then
            Throw New UnauthorizedAccessException("You don't have the sufficient permissions to do this. Also: This is either a bug or you are manually sending requests.")
        End If
        If Models.DefaultRoles.AllDefaultRoles.Contains(RoleId) Then
            Throw New InvalidOperationException("Default roles cannot be deleted.")
        End If
        Using Entities As New Models.BrisDataEntities
            For Each i In Entities.User.Where(Function(j) j.FK_Role = RoleId)
                i.FK_Role = Models.DefaultRoles.RegisteredUser
            Next
            For Each i In Entities.RolePermission.Where(Function(j) j.FK_Role = RoleId)
                Entities.RolePermission.DeleteObject(i)
            Next
            Entities.SaveChanges()
            Entities.Role.DeleteObject(Entities.Role.Single(Function(i) i.Id = RoleId))
            Entities.SaveChanges()
            Security.Security.InvalidateLoginDataCache()
        End Using
        Return Json(AjaxResponse.FromSuccess(Nothing))
    End Function

    <HttpPost()>
    Public Function UpdateRolePermissions(RoleId As Guid, Permissions As Integer()) As ActionResult
        Dim LoginData = Security.Security.GetLoginData
        If Not LoginData.HasPermission(Models.Permission.EditRoles) Then
            Return Json(AjaxResponse.FromFail("You don't have the sufficient permissions to do this. Also: This is either a bug or you are manually sending requests, OR you managed to lock yourself out."))
        End If
        Using Entities As New Models.BrisDataEntities
            For Each i In Entities.RolePermission.Where(Function(j) j.FK_Role = RoleId)
                Entities.RolePermission.DeleteObject(i)
            Next
            If Permissions IsNot Nothing Then
                For Each i In Permissions
                    Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = Guid.NewGuid, .FK_Role = RoleId, .Permission = i})
                Next
            End If
            Entities.SaveChanges()
            Security.Security.InvalidateLoginDataCache()
        End Using
        Return Json(AjaxResponse.FromSuccess(Nothing))
    End Function

    <HttpPost()>
    Public Function UpdateUserRole(UserId As Guid, RoleId As Guid) As ActionResult
        Dim LoginData = Security.Security.GetLoginData
        If Not LoginData.HasPermission(Models.Permission.EditRoles) Then
            Throw New UnauthorizedAccessException("You don't have the sufficient permissions to do this. Also: This is either a bug or you are manually sending requests.")
        End If
        Using Entities As New Models.BrisDataEntities
            Entities.User.Single(Function(i) i.Id = UserId).FK_Role = RoleId
            Entities.SaveChanges()
            Security.Security.InvalidateLoginDataCache()
        End Using
        Return Json(AjaxResponse.FromSuccess(Nothing))
    End Function

    <HttpPost()>
    Public Function AddRemoveBananaConsumption(BananaId As Guid, Amount As Integer) As ActionResult
        Dim LoginData = Security.Security.GetLoginData
        If Not LoginData.IsLoggedIn Then
            Throw New UnauthorizedAccessException("I have no Idea who you are. Log in first.")
        End If
        If Amount = 0 Then
            Return Json(AjaxResponse.FromSuccess(Nothing))
        End If
        Using Entities As New Models.BrisDataEntities
            Dim Consumption = Entities.BananaConsumption.SingleOrDefault(Function(i) i.FK_User = LoginData.User.Id AndAlso i.FK_Banana = BananaId)
            Dim CurrentAmount = If(Consumption Is Nothing, 0, Consumption.Amount)
            Dim NewAmount = CurrentAmount + Amount
            If NewAmount < 0 Then
                Return Json(AjaxResponse.FromFail("The new Amount would result in a negative value."))
            End If
            If CurrentAmount = 0 Then
                Entities.BananaConsumption.AddObject(New Models.BananaConsumption With {.Id = Guid.NewGuid, .FK_User = LoginData.User.Id, .FK_Banana = BananaId, .Amount = NewAmount})
            ElseIf NewAmount = 0 Then
                Entities.BananaConsumption.DeleteObject(Consumption)
            Else
                Consumption.Amount = NewAmount
            End If
            Entities.SaveChanges()
        End Using
        Return Json(AjaxResponse.FromSuccess(Nothing))
    End Function

    <HttpPost()>
    Public Function EditOrCreateBanana(FormData As Validation.FormData.EditOrCreateBananaFormData) As ActionResult
        Dim LoginData = Security.Security.GetLoginData
        If Not LoginData.HasPermission(If(FormData.ExistingBananaId Is Nothing, Models.Permission.CreateBanana, Models.Permission.EditBanana)) Then
            Throw New UnauthorizedAccessException("You are not allowed to do this.")
        End If
        FormData.AssertIsValid()
        Dim Radiation = Integer.Parse(FormData.RadiationString)
        Dim CreatedById As Guid? = Nothing
        If LoginData.IsLoggedIn Then
            CreatedById = LoginData.User.Id
        End If
        Using Entities As New Models.BrisDataEntities
            Dim Banana As Models.Banana
            If FormData.ExistingBananaId Is Nothing Then
                Banana = New Models.Banana With {.Id = Guid.NewGuid}
            Else
                Banana = Entities.Banana.Single(Function(i) i.Id = FormData.ExistingBananaId.Value)
            End If
            Banana.FK_Manufacturer = FormData.ManufacturerId
            Banana.Radiation = Radiation
            Banana.FK_CreatedBy = CreatedById
            Banana.Barcode = FormData.Barcode
            If FormData.ExistingBananaId Is Nothing Then
                Entities.Banana.AddObject(Banana)
            End If
            Entities.SaveChanges()
        End Using
        Return Json(AjaxResponse.FromSuccess(Nothing))
    End Function

    <HttpPost()>
    Public Function EditUserSettings(FormData As Validation.FormData.EditUserSettingsFormData) As ActionResult
        Dim LoginData = Security.Security.GetLoginData
        Dim CanChangeThisUsersSettings = (LoginData.IsLoggedIn AndAlso LoginData.User.Id = FormData.UserId) OrElse LoginData.HasPermission(Models.Permission.EditOtherUsersSettings)
        If Not CanChangeThisUsersSettings Then
            Throw New UnauthorizedAccessException("You are not logged in.")
        End If
        FormData.AssertIsValid()
        Using Entities As New Models.BrisDataEntities
            Dim User = Entities.User.Single(Function(i) i.Id = FormData.UserId)
            User.Name = FormData.UserName
            User.Email = FormData.Email
            If Not String.IsNullOrEmpty(FormData.Password) Then
                Dim OldPasswordHash = Security.PasswordHash.Parse(User.PasswordHash)
                Dim NewPasswordHash = Security.Security.HashPassword(FormData.Password, OldPasswordHash.Salt, OldPasswordHash.IterationCount, OldPasswordHash.OutputLength)
                User.PasswordHash = NewPasswordHash.ToString
            End If
            Entities.SaveChanges()
        End Using
        Return Json(AjaxResponse.FromSuccess(Nothing))
    End Function

End Class
