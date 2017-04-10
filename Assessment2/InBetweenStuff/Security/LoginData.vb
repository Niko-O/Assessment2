
Namespace Security

    Public Structure LoginData

        Private _IsLoggedIn As Boolean
        Public ReadOnly Property IsLoggedIn As Boolean
            <DebuggerStepThrough()>
            Get
                Return _IsLoggedIn
            End Get
        End Property

        Private _User As Models.User
        Public ReadOnly Property User As Models.User
            <DebuggerStepThrough()>
            Get
                Return _User
            End Get
        End Property

        Private _ApplicablePermissions As System.Collections.ObjectModel.ReadOnlyCollection(Of Models.Permission)
        Public ReadOnly Property ApplicablePermissions As System.Collections.ObjectModel.ReadOnlyCollection(Of Models.Permission)
            <DebuggerStepThrough()>
            Get
                Return _ApplicablePermissions
            End Get
        End Property

        Private Sub New(NewIsLoggedIn As Boolean, NewUser As Models.User, NewApplicablePermissions As IEnumerable(Of Models.Permission))
            _IsLoggedIn = NewIsLoggedIn
            _User = NewUser
            _ApplicablePermissions = New System.Collections.ObjectModel.ReadOnlyCollection(Of Models.Permission)(NewApplicablePermissions.ToList)
        End Sub

        Public Function HasPermission(Permission As Models.Permission) As Boolean
            Return ApplicablePermissions.Contains(Permission)
        End Function

        Public Shared Function FromLoggedIn(User As Models.User) As LoginData
            Return New LoginData(True, User, User.Role.RolePermission.Select(Function(i) DirectCast(i.Permission, Models.Permission)))
        End Function
        
        Public Shared Function FromGuest(ApplicablePermissions As IEnumerable(Of Models.Permission)) As LoginData
            Return New LoginData(False, Nothing, ApplicablePermissions)
        End Function

    End Structure

End Namespace
