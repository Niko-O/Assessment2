
Namespace ViewModel.Role

    Public Class EditRoleViewModel

        Public Property AllPermissions As List(Of Models.Permission)
        Public Property AllRoles As List(Of Tuple(Of Models.Role, List(Of Models.RolePermission)))
        Public Property AllUsers As List(Of Models.User)

    End Class

End Namespace
