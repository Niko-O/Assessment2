Public Class Mail

    Public Shared Function SendConfirmationMail(Registration As Models.RegistrationProcess) As String
        Dim RoundtripData As New Security.RoundtripData(Registration.RandomValue, Registration.UniqueValue)
        Dim Recipient = Registration.User.Email
        Dim Title = "Confirm registration at BRIS"
        Dim Body = String.Join(Environment.NewLine, _
                               "You have registered an account at BRIS with the name " & Registration.User.Name, _
                               "Click on the following link to confirm your registration:", _
                               "http://localhost/User/VerifyRegistration?RoundtripData=" & HttpUtility.UrlEncode(RoundtripData.ToEncodedString, System.Text.Encoding.ASCII), _
                               "This will expire at " & Registration.ExpirationDateTime.ToShortDateString & " " & Registration.ExpirationDateTime.ToLongTimeString & ".")
        Return String.Join(Environment.NewLine, Recipient, Title, Body)
    End Function

End Class
