
Namespace Security

    Public Structure RoundtripData

        Private _RandomValue As Byte()
        Public ReadOnly Property RandomValue As Byte()
            <DebuggerStepThrough()>
            Get
                Return _RandomValue
            End Get
        End Property

        Private _UniqueValue As Guid
        Public ReadOnly Property UniqueValue As Guid
            <DebuggerStepThrough()>
            Get
                Return _UniqueValue
            End Get
        End Property

        Public Sub New(NewRandomValue As Byte(), NewUniqueValue As Guid)
            _RandomValue = NewRandomValue
            _UniqueValue = NewUniqueValue
        End Sub

        Public Function ToEncodedString() As String
            Return Convert.ToBase64String(New System.Text.UTF8Encoding(False).GetBytes(<x a=<%= Base16.Encode(RandomValue) %> b=<%= Base16.Encode(UniqueValue.ToByteArray) %>/>.ToString))
        End Function

        Public Shared Function TryParseEncoded(Encoded As String) As RoundtripData?
            'ToDo: Add defensive checks
            Dim Xml = XElement.Parse(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(Encoded)))
            Return New RoundtripData(Base16.Decode(Xml.@a), New Guid(Base16.Decode(Xml.@b)))
        End Function

    End Structure

End Namespace
