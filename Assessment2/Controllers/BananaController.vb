Public Class BananaController
    Inherits System.Web.Mvc.Controller

    <HttpGet()>
    Public Function List() As ActionResult
        Using Entities As New Models.BrisDataEntities
            ViewData("RedirectToMainPageInsteadOfReloadingOnLogin") = False
            ViewData("RedirectToMainPageInsteadOfReloadingOnLogout") = Not Entities.RolePermission.Any(Function(i) i.FK_Role = Models.DefaultRoles.Guest AndAlso i.Permission = Models.Permission.ViewBanana)
            Dim LoginData = Security.Security.GetLoginData
            If Not LoginData.HasPermission(Models.Permission.ViewBanana) Then
                Return View(SharedViewNames.Error403Page)
            End If
            Return View(New ViewModel.Banana.BananasListViewModel With { _
                    .Bananas = Entities.Banana.Select(Function(i) New ViewModel.Banana.BananaManufacturerTuple With {.Banana = i, .Manufacturer = Entities.Manufacturer.FirstOrDefault(Function(j) j.Id = i.FK_Manufacturer)}).ToList, _
                    .ShowManufacturerLink = LoginData.HasPermission(Models.Permission.ViewManufacturer), _
                    .ShowEditBananaLink = LoginData.HasPermission(Models.Permission.EditBanana), _
                    .ShowDeleteBananaLink = LoginData.HasPermission(Models.Permission.DeleteBanana), _
                    .ShowAddBananaLink = LoginData.HasPermission(Models.Permission.CreateBanana), _
                    .ShowConsumeBananaLink = LoginData.IsLoggedIn})
        End Using
    End Function

    <HttpGet()>
    Public Function ByUser(UserId As Guid) As ActionResult
        Using Entities As New Models.BrisDataEntities
            ViewData("RedirectToMainPageInsteadOfReloadingOnLogin") = False
            ViewData("RedirectToMainPageInsteadOfReloadingOnLogout") = Not Entities.RolePermission.Any(Function(i) i.FK_Role = Models.DefaultRoles.Guest AndAlso i.Permission = Models.Permission.AccessOtherUsersBananas)
            Dim LoginData = Security.Security.GetLoginData
            Dim CanAccess = (LoginData.IsLoggedIn AndAlso LoginData.User.Id = UserId) OrElse LoginData.HasPermission(Models.Permission.AccessOtherUsersBananas)
            If Not CanAccess Then
                Return View(SharedViewNames.Error403Page)
            End If
            Dim User = Entities.User.SingleOrDefault(Function(i) i.Id = UserId)
            If User Is Nothing Then
                Return View(SharedViewNames.Error404Page)
            End If
            Dim Consumptions = Entities.BananaConsumption.Where(Function(i) i.FK_User = UserId).ToList
            Dim ViewModel As New ViewModel.Banana.BananasByUserViewModel
            ViewModel.Bananas = Consumptions.Select(Function(i)
                                                        Dim Result As New ViewModel.Banana.BananaAmountTuple
                                                        Result.Banana = Entities.Banana.FirstOrDefault(Function(j) j.Id = i.FK_Banana)
                                                        Result.Amount = i.Amount
                                                        Result.ManufacturerName = Entities.Manufacturer.FirstOrDefault(Function(j) j.Id = Result.Banana.FK_Manufacturer).Name
                                                        Return Result
                                                    End Function).ToList
            ViewModel.TotalBananasEaten = Consumptions.Sum(Function(i) i.Amount)
            ViewModel.TotalRadiation = Consumptions.Sum(Function(i) i.Amount * Entities.Banana.FirstOrDefault(Function(j) j.Id = i.FK_Banana).Radiation)

            Return View(ViewModel)
        End Using
    End Function

    <HttpGet()>
    Public Function EditOrCreateBanana(BananaId As Guid?) As ActionResult
        Using Entities As New Models.BrisDataEntities
            ViewData("RedirectToMainPageInsteadOfReloadingOnLogin") = False
            ViewData("RedirectToMainPageInsteadOfReloadingOnLogout") = Not Entities.RolePermission.Any(Function(i) i.FK_Role = Models.DefaultRoles.Guest AndAlso i.Permission = If(BananaId Is Nothing, Models.Permission.CreateBanana, Models.Permission.EditBanana))
            Dim LoginData = Security.Security.GetLoginData
            If Not LoginData.HasPermission(If(BananaId Is Nothing, Models.Permission.CreateBanana, Models.Permission.EditBanana)) Then
                Return View(SharedViewNames.Error403Page)
            End If
            Dim Banana As Models.Banana = Nothing
            If BananaId IsNot Nothing Then
                Banana = Entities.Banana.SingleOrDefault(Function(i) i.Id = BananaId.Value)
                If Banana Is Nothing Then
                    Return View(SharedViewNames.Error404Page)
                End If
            End If
            Return View(New ViewModel.Banana.EditOrCreateBananaViewModel With {.Banana = Banana, .AllManufacturers = Entities.Manufacturer.OrderBy(Function(i) i.Name).ToList})
        End Using
    End Function

End Class
