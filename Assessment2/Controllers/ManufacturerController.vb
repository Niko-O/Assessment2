Public Class ManufacturerController
    Inherits System.Web.Mvc.Controller

    <HttpGet()>
    Public Function Show(ManufacturerId As Guid) As ActionResult
        Dim LoginData = Security.Security.GetLoginData
        If Not LoginData.HasPermission(Models.Permission.ViewManufacturer) Then
            Return View(SharedViewNames.Error403Page)
        End If
        Using Entities As New Models.BrisDataEntities
            Dim Manufacturer = Entities.Manufacturer.SingleOrDefault(Function(i) i.Id = ManufacturerId)
            If Manufacturer Is Nothing Then
                Return View(SharedViewNames.Error404Page)
            End If
            Dim ViewModel As New ViewModel.Manufacturer.ShowManufacturerViewModel
            ViewModel.Manufacturer = Manufacturer
            ViewModel.Bananas = Entities.Banana.Where(Function(i) i.FK_Manufacturer = ManufacturerId).ToList
            ViewModel.ShowEditManufacturerLink = LoginData.HasPermission(Models.Permission.EditManufacturer)
            ViewModel.ShowDeleteManufacturerLink = (ViewModel.Bananas.Count = 0 OrElse LoginData.HasPermission(Models.Permission.DeleteBanana)) AndAlso LoginData.HasPermission(Models.Permission.DeleteManufacturer)
            Return View(ViewModel)
        End Using
    End Function

    <HttpGet()>
    Public Function List() As ActionResult
        Using Entities As New Models.BrisDataEntities
            ViewData("RedirectToMainPageInsteadOfReloadingOnLogin") = False
            ViewData("RedirectToMainPageInsteadOfReloadingOnLogout") = Not Entities.RolePermission.Any(Function(i) i.FK_Role = Models.DefaultRoles.Guest AndAlso i.Permission = Models.Permission.ViewManufacturer)
            Dim LoginData = Security.Security.GetLoginData
            If Not LoginData.HasPermission(Models.Permission.ViewManufacturer) Then
                Return View(SharedViewNames.Error403Page)
            End If
            Dim ViewModel As New ViewModel.Manufacturer.ManufacturersListViewModel
            Dim Manufacturers = Entities.Manufacturer.ToList
            ViewModel.Manufacturers = Manufacturers.Select(Function(i)
                                                               Return New ViewModel.Manufacturer.ManufacturerViewModel With { _
                                                                   .Manufacturer = i, _
                                                                   .NumberOfBananas = Entities.Banana.Count(Function(j) j.FK_Manufacturer = i.Id), _
                                                                   .ShowDeleteManufacturerLink = (.NumberOfBananas = 0 OrElse LoginData.HasPermission(Models.Permission.DeleteBanana)) AndAlso LoginData.HasPermission(Models.Permission.DeleteManufacturer)
                                                                 }
                                                           End Function).ToList
            ViewModel.ShowEditManufacturerLink = LoginData.HasPermission(Models.Permission.EditManufacturer)
            Return View(ViewModel)
        End Using
    End Function

End Class
