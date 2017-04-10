Public Class UserController
    Inherits System.Web.Mvc.Controller

    <HttpGet()>
    Public Function Show(UserID As Guid) As ActionResult
        Using Entities As New Models.BrisDataEntities
            ViewData("RedirectToMainPageInsteadOfReloadingOnLogin") = False
            ViewData("RedirectToMainPageInsteadOfReloadingOnLogout") = Not Entities.RolePermission.Any(Function(i) i.FK_Role = Models.DefaultRoles.Guest AndAlso i.Permission = Models.Permission.AccessOtherUsersProfile)
            Dim LoginData = Security.Security.GetLoginData
            Dim IsCurrentlyLoggedInUser = LoginData.IsLoggedIn AndAlso LoginData.User.Id = UserID
            Dim CanAccessProfile = IsCurrentlyLoggedInUser OrElse LoginData.HasPermission(Models.Permission.AccessOtherUsersProfile)
            If Not CanAccessProfile Then
                Return View(SharedViewNames.Error403Page)
            End If
            Dim User = Entities.User.SingleOrDefault(Function(i) i.Id = UserID)
            If User Is Nothing Then
                Return View(SharedViewNames.Error404Page)
            End If
            Dim ViewModel As New ViewModel.User.UserProfileViewModel With { _
                .User = User, _
                .ShowUserSettingsLink = IsCurrentlyLoggedInUser OrElse LoginData.HasPermission(Models.Permission.EditOtherUsersSettings), _
                .ShowBananasLink = IsCurrentlyLoggedInUser OrElse LoginData.HasPermission(Models.Permission.AccessOtherUsersBananas)
            }
            Dim Consumptions = Entities.BananaConsumption.Where(Function(i) i.FK_User = UserID).ToList
            ViewModel.NumberOfBananas = Consumptions.Sum(Function(i) i.Amount)
            ViewModel.TotalRadiation = Consumptions.Sum(Function(i) Entities.Banana.Single(Function(j) j.Id = i.FK_Banana).Radiation * i.Amount)
            Return View(ViewModel)
        End Using
    End Function

    <HttpGet()>
    Public Function Settings(UserId As Guid) As ActionResult
        Using Entities As New Models.BrisDataEntities
            ViewData("RedirectToMainPageInsteadOfReloadingOnLogin") = False
            ViewData("RedirectToMainPageInsteadOfReloadingOnLogout") = Not Entities.RolePermission.Any(Function(i) i.FK_Role = Models.DefaultRoles.Guest AndAlso i.Permission = Models.Permission.EditOtherUsersSettings)
            Dim LoginData = Security.Security.GetLoginData
            Dim EditedUserIsLoggedInUser = LoginData.IsLoggedIn AndAlso LoginData.User.Id = UserId
            Dim CanAccess = EditedUserIsLoggedInUser OrElse LoginData.HasPermission(Models.Permission.EditOtherUsersSettings)
            If Not CanAccess Then
                Return View(SharedViewNames.Error403Page)
            End If
            Dim EditedUser As Models.User
            If EditedUserIsLoggedInUser Then
                EditedUser = LoginData.User
            Else
                EditedUser = Entities.User.SingleOrDefault(Function(i) i.Id = UserId)
                If EditedUser Is Nothing Then
                    Return View(SharedViewNames.Error404Page)
                End If
            End If
            Return View(EditedUser)
        End Using
    End Function

    <HttpGet()>
    Public Function Register() As ActionResult
        ViewData("RedirectToMainPageInsteadOfReloadingOnLogin") = False
        ViewData("RedirectToMainPageInsteadOfReloadingOnLogout") = False
        Dim LoginData = Security.Security.GetLoginData
        If LoginData.IsLoggedIn Then
            Return View(SharedViewNames.AlreadyLoggedInPage)
        End If
        Return View()
    End Function

    <HttpGet()>
    Public Function VerifyRegistration(RoundtripData As String) As ActionResult
        Const VerificationFailedViewPath = "~/Views/User/VerificationFailedPage.vbhtml"
        Const VerificationSuccessfulViewPath = "~/Views/User/VerificationSuccessfulPage.vbhtml"

        ViewData("RedirectToMainPageInsteadOfReloadingOnLogin") = True
        ViewData("RedirectToMainPageInsteadOfReloadingOnLogout") = True
        Dim LoginData = Security.Security.GetLoginData
        If LoginData.IsLoggedIn Then
            Return View(SharedViewNames.AlreadyLoggedInPage)
        End If
        Dim RoundtripDataParsed = Security.RoundtripData.TryParseEncoded(RoundtripData)
        If RoundtripDataParsed Is Nothing Then
            Return View(VerificationFailedViewPath, "This link is invalid.")
        End If
        Using Entities As New Models.BrisDataEntities
            Dim RoundtripDataValue = RoundtripDataParsed.Value
            Dim RandomValueDummyArray = {RoundtripDataValue.RandomValue}
            Dim Registration = Entities.RegistrationProcess.SingleOrDefault(Function(i) i.UniqueValue = RoundtripDataValue.UniqueValue AndAlso RandomValueDummyArray.Contains(i.RandomValue))
            If Registration Is Nothing Then
                Return View(VerificationFailedViewPath, "Either you are alredy verified or there is no user expected to verify through this link.")
            End If
            If DateTime.Now > Registration.ExpirationDateTime Then
                Entities.RegistrationProcess.DeleteObject(Registration)
                Entities.SaveChanges()
                Return View(VerificationFailedViewPath, "Your verification expired on " & Registration.ExpirationDateTime.ToShortDateString & " " & Registration.ExpirationDateTime.ToLongTimeString & ".")
            End If
            Dim User = Entities.User.Single(Function(i) i.Id = Registration.FK_User)
            User.FK_Role = Models.DefaultRoles.RegisteredUser
            Entities.SaveChanges()
            Return View(VerificationSuccessfulViewPath)
        End Using
    End Function

End Class
