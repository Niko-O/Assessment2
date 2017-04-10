
Namespace Models

    Public Class DefaultRoles
        Public Shared ReadOnly Guest As New Guid("fa1d1c85-6931-4552-b43f-19cd7fba323c")
        Public Shared ReadOnly UnverifiedUser As New Guid("eaa0536b-430c-4390-83a3-2efba13f19e5")
        Public Shared ReadOnly RegisteredUser As New Guid("d5d0961c-47b0-4f01-9d4c-545590a41c3d")
        Public Shared ReadOnly Moderator As New Guid("99b8dac5-e17b-42ab-a5e5-67e5d7a34389")
        Public Shared ReadOnly Administrator As New Guid("75256411-dab9-4f34-a008-8261dcc21e76")
        Public Shared ReadOnly AllDefaultRoles As Guid() = {Guest, UnverifiedUser, RegisteredUser, Moderator, Administrator}
    End Class

End Namespace
