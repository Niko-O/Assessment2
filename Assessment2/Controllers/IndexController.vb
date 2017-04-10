Public Class IndexController
    Inherits System.Web.Mvc.Controller

    <HttpGet()>
    Public Function Index() As ActionResult
        ViewData("RedirectToMainPageInsteadOfReloadingOnLogin") = False
        ViewData("RedirectToMainPageInsteadOfReloadingOnLogout") = False
        'Dim LoginData = Security.Security.GetLoginData
        'If Not LoginData.IsLoggedIn Then
        '    Security.Security.TryLogIn("Admin", "Password1")
        'End If
        Return View()
    End Function

End Class
