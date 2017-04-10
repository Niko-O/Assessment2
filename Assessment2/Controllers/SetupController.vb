Namespace Assessment2
    Public Class SetupController
        Inherits System.Web.Mvc.Controller

        <HttpGet()>
        Function Setup() As ActionResult
            ViewData("RedirectToMainPageInsteadOfReloadingOnLogin") = True
            ViewData("RedirectToMainPageInsteadOfReloadingOnLogout") = True

            Using Entities As New Models.BrisDataEntities
                Entities.Role.AddObject(New Models.Role With {.Id = New Guid("fa1d1c85-6931-4552-b43f-19cd7fba323c"), .Name = "Guest"})
                Entities.Role.AddObject(New Models.Role With {.Id = New Guid("eaa0536b-430c-4390-83a3-2efba13f19e5"), .Name = "Unverified User"})
                Entities.Role.AddObject(New Models.Role With {.Id = New Guid("d5d0961c-47b0-4f01-9d4c-545590a41c3d"), .Name = "Registered User"})
                Entities.Role.AddObject(New Models.Role With {.Id = New Guid("99b8dac5-e17b-42ab-a5e5-67e5d7a34389"), .Name = "Moderator"})
                Entities.Role.AddObject(New Models.Role With {.Id = New Guid("75256411-dab9-4f34-a008-8261dcc21e76"), .Name = "Administrator"})
                Entities.Role.AddObject(New Models.Role With {.Id = New Guid("55d5b95b-5828-4b7e-9282-a18e919326cf"), .Name = "Test Role 1"})
                Entities.Role.AddObject(New Models.Role With {.Id = New Guid("6a704233-9ed2-4050-a4ac-fcd7f47df48e"), .Name = "Test Role 2"})
                Entities.SaveChanges()

                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("651b5feb-7fc7-4224-b17d-08f27556c8be"), .FK_Role = New Guid("fa1d1c85-6931-4552-b43f-19cd7fba323c"), .Permission = 1})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("534d4345-59f8-4929-8e9d-0a89e8eb8afa"), .FK_Role = New Guid("fa1d1c85-6931-4552-b43f-19cd7fba323c"), .Permission = 5})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("12fb3ce9-95cd-496d-83fb-13de88884166"), .FK_Role = New Guid("eaa0536b-430c-4390-83a3-2efba13f19e5"), .Permission = 1})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("0134d595-3dbf-469d-84c3-18922dd90fd4"), .FK_Role = New Guid("eaa0536b-430c-4390-83a3-2efba13f19e5"), .Permission = 5})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("b55322f8-16dd-4469-92de-19827fc95b18"), .FK_Role = New Guid("d5d0961c-47b0-4f01-9d4c-545590a41c3d"), .Permission = 1})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("18efaf05-0451-4f14-b480-21bc0308c7b9"), .FK_Role = New Guid("d5d0961c-47b0-4f01-9d4c-545590a41c3d"), .Permission = 2})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("7ffa7df8-a2cc-4c70-9808-341089b9c845"), .FK_Role = New Guid("d5d0961c-47b0-4f01-9d4c-545590a41c3d"), .Permission = 5})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("0f6f5d21-4232-4362-b249-371cc8e3aba6"), .FK_Role = New Guid("d5d0961c-47b0-4f01-9d4c-545590a41c3d"), .Permission = 6})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("a5d8e989-dcd6-47c8-a309-3b713ad8d34f"), .FK_Role = New Guid("d5d0961c-47b0-4f01-9d4c-545590a41c3d"), .Permission = 11})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("6f7761e0-9e0f-43af-863d-3e7e7374e88a"), .FK_Role = New Guid("99b8dac5-e17b-42ab-a5e5-67e5d7a34389"), .Permission = 1})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("40b1345d-8ec9-4381-b290-477f611e07a8"), .FK_Role = New Guid("99b8dac5-e17b-42ab-a5e5-67e5d7a34389"), .Permission = 2})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("dea208d2-baec-486a-8e2d-538443b27db0"), .FK_Role = New Guid("99b8dac5-e17b-42ab-a5e5-67e5d7a34389"), .Permission = 3})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("9249f6c8-9b47-4534-8a79-562ab6f9f8fb"), .FK_Role = New Guid("99b8dac5-e17b-42ab-a5e5-67e5d7a34389"), .Permission = 5})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("4d9fa0cc-d828-4d6e-86e6-58e34752d0ba"), .FK_Role = New Guid("99b8dac5-e17b-42ab-a5e5-67e5d7a34389"), .Permission = 6})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("71c010ac-69b6-422d-b989-63bff3161184"), .FK_Role = New Guid("99b8dac5-e17b-42ab-a5e5-67e5d7a34389"), .Permission = 7})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("7f6b8ad0-3982-4bc1-9398-65c3e7056902"), .FK_Role = New Guid("99b8dac5-e17b-42ab-a5e5-67e5d7a34389"), .Permission = 11})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("0caaa98c-e5fa-45a7-8eaf-686da6d6f8d3"), .FK_Role = New Guid("99b8dac5-e17b-42ab-a5e5-67e5d7a34389"), .Permission = 12})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("c498dea9-a159-4685-a322-6ea7bc3ef472"), .FK_Role = New Guid("75256411-dab9-4f34-a008-8261dcc21e76"), .Permission = 1})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("7493fc29-fbd7-459e-83a7-6f4eeaf847fe"), .FK_Role = New Guid("75256411-dab9-4f34-a008-8261dcc21e76"), .Permission = 2})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("4262ba82-1018-494a-aaad-71525e129b57"), .FK_Role = New Guid("75256411-dab9-4f34-a008-8261dcc21e76"), .Permission = 3})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("46cf5938-3884-4435-a316-73890b33b3c5"), .FK_Role = New Guid("75256411-dab9-4f34-a008-8261dcc21e76"), .Permission = 4})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("a375ff57-22f3-424d-be2b-7d2ffe968762"), .FK_Role = New Guid("75256411-dab9-4f34-a008-8261dcc21e76"), .Permission = 5})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("f1fdfd6f-6aa0-47da-a0ce-82e914de3832"), .FK_Role = New Guid("75256411-dab9-4f34-a008-8261dcc21e76"), .Permission = 6})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("b1b6fd72-a627-496b-b126-83a6667fd3a2"), .FK_Role = New Guid("75256411-dab9-4f34-a008-8261dcc21e76"), .Permission = 7})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("96252e68-7d7a-498c-8d80-94bacf5b82d4"), .FK_Role = New Guid("75256411-dab9-4f34-a008-8261dcc21e76"), .Permission = 8})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("e7a32f68-231f-4948-af06-9c3a6ba1b3dc"), .FK_Role = New Guid("75256411-dab9-4f34-a008-8261dcc21e76"), .Permission = 9})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("80860852-f15a-4ef8-a80d-9e7f26916ac2"), .FK_Role = New Guid("75256411-dab9-4f34-a008-8261dcc21e76"), .Permission = 10})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("ff3236f0-737c-4ff6-acb2-9fa27a3f4946"), .FK_Role = New Guid("75256411-dab9-4f34-a008-8261dcc21e76"), .Permission = 11})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("b677fe5a-75e6-4b4d-93b0-a4c3f6feccbf"), .FK_Role = New Guid("75256411-dab9-4f34-a008-8261dcc21e76"), .Permission = 12})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("3b40c0fa-466e-4d0e-a25e-b652f399d090"), .FK_Role = New Guid("75256411-dab9-4f34-a008-8261dcc21e76"), .Permission = 13})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("1415432a-3291-4e87-ad5d-bc2d36fb3585"), .FK_Role = New Guid("55d5b95b-5828-4b7e-9282-a18e919326cf"), .Permission = 1})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("b8b3c9ce-1d6d-4a9c-ac51-c00a44ec9c08"), .FK_Role = New Guid("55d5b95b-5828-4b7e-9282-a18e919326cf"), .Permission = 3})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("b77471c0-25cf-4d68-834d-c4dee7f9b37a"), .FK_Role = New Guid("55d5b95b-5828-4b7e-9282-a18e919326cf"), .Permission = 5})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("cce1ba3f-1c49-40c7-9127-d2476932cee5"), .FK_Role = New Guid("55d5b95b-5828-4b7e-9282-a18e919326cf"), .Permission = 7})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("a8998954-5c85-4791-a042-d36d0b63249d"), .FK_Role = New Guid("55d5b95b-5828-4b7e-9282-a18e919326cf"), .Permission = 9})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("75f23fa8-52ae-49c6-990b-d42cbe55fe0f"), .FK_Role = New Guid("55d5b95b-5828-4b7e-9282-a18e919326cf"), .Permission = 11})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("b29397d4-1ad5-4eca-9c9f-d65b4b218323"), .FK_Role = New Guid("55d5b95b-5828-4b7e-9282-a18e919326cf"), .Permission = 13})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("0ebe01a2-2011-4d0d-9fc6-dc97ae69be66"), .FK_Role = New Guid("6a704233-9ed2-4050-a4ac-fcd7f47df48e"), .Permission = 2})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("f9737044-434d-42a7-8080-dcaec586ce15"), .FK_Role = New Guid("6a704233-9ed2-4050-a4ac-fcd7f47df48e"), .Permission = 4})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("bcf67a06-4ddd-472a-a9a9-e12b0129f3a6"), .FK_Role = New Guid("6a704233-9ed2-4050-a4ac-fcd7f47df48e"), .Permission = 6})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("2f949b5f-c5fb-409b-91aa-f66fb619f7f4"), .FK_Role = New Guid("6a704233-9ed2-4050-a4ac-fcd7f47df48e"), .Permission = 8})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("44a79777-b27d-4429-a4f5-fa5749c71b07"), .FK_Role = New Guid("6a704233-9ed2-4050-a4ac-fcd7f47df48e"), .Permission = 10})
                Entities.RolePermission.AddObject(New Models.RolePermission With {.Id = New Guid("1a672492-0b58-4aea-a4de-fc0d17fd7b05"), .FK_Role = New Guid("6a704233-9ed2-4050-a4ac-fcd7f47df48e"), .Permission = 12})
                Entities.SaveChanges()

                Entities.Manufacturer.AddObject(New Models.Manufacturer With {.Id = New Guid("bbc9283b-87e1-48a8-b536-5c8d686399d3"), .Name = "Chiquita"})
                Entities.Manufacturer.AddObject(New Models.Manufacturer With {.Id = New Guid("f09ef837-b077-451f-b528-fab711bc42ba"), .Name = "Dole"})
                Entities.Manufacturer.AddObject(New Models.Manufacturer With {.Id = New Guid("fc19db99-c421-4cc9-896d-779a2ace46aa"), .Name = "Jamaica Bananas"})
                Entities.SaveChanges()

                Entities.User.AddObject(New Models.User With {.Id = New Guid("55db8e43-2c99-4c0d-be25-ac9d2219c977"), .Name = "Admin", .Email = "admin@localhost", .PasswordHash = Security.Security.HashPasswordWithDefaultSettingsAndNewHash("Password1").ToString, .FK_Role = New Guid("75256411-dab9-4f34-a008-8261dcc21e76")})
                Entities.User.AddObject(New Models.User With {.Id = New Guid("1ad45292-2940-4a55-b164-7356053190a8"), .Name = "Mod", .Email = "admin@localhost", .PasswordHash = Security.Security.HashPasswordWithDefaultSettingsAndNewHash("Password1").ToString, .FK_Role = New Guid("99b8dac5-e17b-42ab-a5e5-67e5d7a34389")})
                Entities.User.AddObject(New Models.User With {.Id = New Guid("c7b62e55-0a16-487b-bcf9-f56fa63c4ec0"), .Name = "User 1", .Email = "admin@localhost", .PasswordHash = Security.Security.HashPasswordWithDefaultSettingsAndNewHash("Password1").ToString, .FK_Role = New Guid("d5d0961c-47b0-4f01-9d4c-545590a41c3d")})
                Entities.User.AddObject(New Models.User With {.Id = New Guid("a2e0eb3c-3ba0-486c-897a-539d96dcc5bf"), .Name = "User 2", .Email = "admin@localhost", .PasswordHash = Security.Security.HashPasswordWithDefaultSettingsAndNewHash("Password1").ToString, .FK_Role = New Guid("eaa0536b-430c-4390-83a3-2efba13f19e5")})
                Entities.SaveChanges()

                Entities.RegistrationProcess.AddObject(New Models.RegistrationProcess With {.Id = New Guid("4580b4e3-7f82-4604-a57b-9a0cc50d2aed"), .FK_User = New Guid("a2e0eb3c-3ba0-486c-897a-539d96dcc5bf"), .RandomValue = New Byte() {1, 2, 3, 4}, .UniqueValue = New Guid("ce9ae3b2-1fcc-4877-b575-02c2abf846d3"), .ExpirationDateTime = New DateTime(2017, 4, 20, 11, 52, 30)})
                Entities.SaveChanges()

                Entities.Banana.AddObject(New Models.Banana With {.Id = New Guid("b05a3b9d-284f-4cc8-99e1-73dc88e70958"), .Name = "Dole 01", .FK_Manufacturer = New Guid("f09ef837-b077-451f-b528-fab711bc42ba"), .Radiation = 10, .FK_CreatedBy = New Guid("55db8e43-2c99-4c0d-be25-ac9d2219c977")})
                Entities.Banana.AddObject(New Models.Banana With {.Id = New Guid("cbd53aac-e6d3-4260-b74c-1ecbe0ff3713"), .Name = "Dole 02", .FK_Manufacturer = New Guid("f09ef837-b077-451f-b528-fab711bc42ba"), .Radiation = 12, .FK_CreatedBy = New Guid("1ad45292-2940-4a55-b164-7356053190a8")})
                Entities.Banana.AddObject(New Models.Banana With {.Id = New Guid("0a96bd60-dbe6-4c8e-9b3d-2df194679166"), .Name = "Chiquita 01", .FK_Manufacturer = New Guid("bbc9283b-87e1-48a8-b536-5c8d686399d3"), .Radiation = 30, .FK_CreatedBy = New Guid("c7b62e55-0a16-487b-bcf9-f56fa63c4ec0")})
                Entities.SaveChanges()

                Entities.BananaConsumption.AddObject(New Models.BananaConsumption With {.Id = New Guid("7f537388-fc9a-42d5-b52c-3307e81aea0e"), .FK_User = New Guid("55db8e43-2c99-4c0d-be25-ac9d2219c977"), .FK_Banana = New Guid("b05a3b9d-284f-4cc8-99e1-73dc88e70958"), .Amount = 10})
                Entities.BananaConsumption.AddObject(New Models.BananaConsumption With {.Id = New Guid("3418195a-d0fa-4cf4-b7e4-835bf7c500f6"), .FK_User = New Guid("55db8e43-2c99-4c0d-be25-ac9d2219c977"), .FK_Banana = New Guid("cbd53aac-e6d3-4260-b74c-1ecbe0ff3713"), .Amount = 20})
                Entities.BananaConsumption.AddObject(New Models.BananaConsumption With {.Id = New Guid("9023b7db-3762-47e7-8847-9d0ec40723ed"), .FK_User = New Guid("55db8e43-2c99-4c0d-be25-ac9d2219c977"), .FK_Banana = New Guid("0a96bd60-dbe6-4c8e-9b3d-2df194679166"), .Amount = 30})
                Entities.BananaConsumption.AddObject(New Models.BananaConsumption With {.Id = New Guid("50686eef-8e19-437c-a13d-017a7852833f"), .FK_User = New Guid("1ad45292-2940-4a55-b164-7356053190a8"), .FK_Banana = New Guid("b05a3b9d-284f-4cc8-99e1-73dc88e70958"), .Amount = 5})
                Entities.BananaConsumption.AddObject(New Models.BananaConsumption With {.Id = New Guid("1105bab8-8734-4686-b117-2ae7ac4f3dd6"), .FK_User = New Guid("1ad45292-2940-4a55-b164-7356053190a8"), .FK_Banana = New Guid("0a96bd60-dbe6-4c8e-9b3d-2df194679166"), .Amount = 6})
                Entities.BananaConsumption.AddObject(New Models.BananaConsumption With {.Id = New Guid("4284b551-daaa-420c-8b28-c076a879aabd"), .FK_User = New Guid("c7b62e55-0a16-487b-bcf9-f56fa63c4ec0"), .FK_Banana = New Guid("b05a3b9d-284f-4cc8-99e1-73dc88e70958"), .Amount = 100})
                Entities.BananaConsumption.AddObject(New Models.BananaConsumption With {.Id = New Guid("bd3eeeba-9610-4424-8601-1e32e9011e8d"), .FK_User = New Guid("c7b62e55-0a16-487b-bcf9-f56fa63c4ec0"), .FK_Banana = New Guid("cbd53aac-e6d3-4260-b74c-1ecbe0ff3713"), .Amount = 200})
                Entities.BananaConsumption.AddObject(New Models.BananaConsumption With {.Id = New Guid("6d4715ed-021c-482e-a01b-97c4556f2d39"), .FK_User = New Guid("c7b62e55-0a16-487b-bcf9-f56fa63c4ec0"), .FK_Banana = New Guid("0a96bd60-dbe6-4c8e-9b3d-2df194679166"), .Amount = 128})
                Entities.SaveChanges()

                Security.Security.InvalidateLoginDataCache()
            End Using
            Return View()
        End Function

        <HttpGet()>
        Public Function Truncate() As ActionResult
            ViewData("RedirectToMainPageInsteadOfReloadingOnLogin") = True
            ViewData("RedirectToMainPageInsteadOfReloadingOnLogout") = True

            Using Entities As New Models.BrisDataEntities
                For Each i In Entities.BananaConsumption
                    Entities.BananaConsumption.DeleteObject(i)
                Next
                Entities.SaveChanges()
                For Each i In Entities.Banana
                    Entities.Banana.DeleteObject(i)
                Next
                Entities.SaveChanges()
                For Each i In Entities.RegistrationProcess
                    Entities.RegistrationProcess.DeleteObject(i)
                Next
                Entities.SaveChanges()
                For Each i In Entities.User
                    Entities.User.DeleteObject(i)
                Next
                Entities.SaveChanges()
                For Each i In Entities.Manufacturer
                    Entities.Manufacturer.DeleteObject(i)
                Next
                Entities.SaveChanges()
                For Each i In Entities.RolePermission
                    Entities.RolePermission.DeleteObject(i)
                Next
                Entities.SaveChanges()
                For Each i In Entities.Role
                    Entities.Role.DeleteObject(i)
                Next
                Entities.SaveChanges()
                Security.Security.InvalidateLoginDataCache()
            End Using
            Return View()
        End Function

        <HttpGet()>
        Public Function ShowTableData() As ActionResult
            ViewData("RedirectToMainPageInsteadOfReloadingOnLogin") = False
            ViewData("RedirectToMainPageInsteadOfReloadingOnLogout") = False

            Using Entities As New Models.BrisDataEntities
                Return View(New ViewModel.Setup.ShowTableDataViewModel With { _
                    .Role = Entities.Role.ToList, _
                    .RolePermission = Entities.RolePermission.ToList, _
                    .Manufacturer = Entities.Manufacturer.ToList, _
                    .User = Entities.User.ToList, _
                    .RegistrationProcess = Entities.RegistrationProcess.ToList, _
                    .Banana = Entities.Banana.ToList, _
                    .BananaConsumption = Entities.BananaConsumption.ToList
                })
            End Using
        End Function

    End Class
End Namespace