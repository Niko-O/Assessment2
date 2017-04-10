
Namespace Security

    Public Structure PasswordHash

        Private _Hash As Byte()
        Public ReadOnly Property Hash As Byte()
            <DebuggerStepThrough()>
            Get
                Return _Hash
            End Get
        End Property

        Private _Salt As Byte()
        Public ReadOnly Property Salt As Byte()
            <DebuggerStepThrough()>
            Get
                Return _Salt
            End Get
        End Property

        Private _IterationCount As Integer
        Public ReadOnly Property IterationCount As Integer
            <DebuggerStepThrough()>
            Get
                Return _IterationCount
            End Get
        End Property

        Private _OutputLength As Integer
        Public ReadOnly Property OutputLength As Integer
            <DebuggerStepThrough()>
            Get
                Return _OutputLength
            End Get
        End Property

        Public Sub New(NewHash As Byte(), NewSalt As Byte(), NewIterationCount As Integer, NewOutputLength As Integer)
            If NewHash Is Nothing Then
                Throw New ArgumentNullException("NewHash")
            End If
            If NewSalt Is Nothing Then
                Throw New ArgumentNullException("NewSalt")
            End If
            _Hash = NewHash
            _Salt = NewSalt
            _IterationCount = NewIterationCount
            _OutputLength = NewOutputLength
        End Sub

        Public Overrides Function ToString() As String
            Return String.Join("$", Convert.ToBase64String(Hash), Convert.ToBase64String(Salt), IterationCount.ToString, OutputLength.ToString)
        End Function

        Public Shared Function Parse(Input As String) As PasswordHash
            Dim Result = TryParse(Input)
            If Result Is Nothing Then
                Throw New ArgumentException("The input is not a valid password hash", "Input")
            End If
            Return Result.Value
        End Function

        Public Shared Function TryParse(Input As String) As PasswordHash?
            If String.IsNullOrEmpty(Input) Then
                Return Nothing
            End If
            Dim Parts = Input.Split("$"c)
            If Parts.Length <> 4 Then
                Return Nothing
            End If

            Dim HashBytes As Byte()
            Dim Salt As Byte()
            Dim IterationCount As Integer
            Dim OutputLength As Integer

            Try
                HashBytes = Convert.FromBase64String(Parts(0))
            Catch ex As FormatException
                Return Nothing
            End Try

            Try
                Salt = Convert.FromBase64String(Parts(1))
            Catch ex As FormatException
                Return Nothing
            End Try

            If Not Integer.TryParse(Parts(2), IterationCount) Then
                Return Nothing
            End If

            If Not Integer.TryParse(Parts(3), OutputLength) Then
                Return Nothing
            End If

            Return New PasswordHash(HashBytes, Salt, IterationCount, OutputLength)
        End Function

    End Structure

End Namespace
