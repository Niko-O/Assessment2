Public Class RoleController
    Inherits System.Web.Mvc.Controller

    <HttpGet()>
    Public Function Edit() As ActionResult
        Using Entities As New Models.BrisDataEntities
            ViewData("RedirectToMainPageInsteadOfReloadingOnLogin") = False
            ViewData("RedirectToMainPageInsteadOfReloadingOnLogout") = Not Entities.RolePermission.Any(Function(i) i.FK_Role = Models.DefaultRoles.Guest AndAlso i.Permission = Models.Permission.EditRoles)
            Dim LoginData = Security.Security.GetLoginData
            If Not LoginData.HasPermission(Models.Permission.EditRoles) Then
                Return View(SharedViewNames.Error403Page)
            End If
            Return View(New ViewModel.Role.EditRoleViewModel With {
                    .AllPermissions = [Enum].GetValues(GetType(Models.Permission)).Cast(Of Models.Permission)().ToList, _
                    .AllRoles = Entities.Role.ToList.Select(Function(i) Tuple.Create(i, Entities.RolePermission.Where(Function(j) j.FK_Role = i.Id).ToList)).ToList, _
                    .AllUsers = Entities.User.ToList
                })
        End Using
    End Function

End Class
