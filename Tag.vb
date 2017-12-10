Imports System.IO
Public Class Tag
    Private Mp3HeaderBitsStr As Mp3HeaderBitsString
    Private MP3HeaderBits As BitArray
    Private Mp3FInfo As FileInfo
    Private XingHeaderBytes(115) As Byte
    Private Mp3HeaderPosition, Mp3ClearSize As Integer
    Dim nSize, extensao As String
    Dim verificaTag As Boolean
    Dim verificaGetXingHeader As Boolean

    Private Structure Mp3HeaderBitsString
        Dim MPEGTypeBitsString As String
        Dim LayerBitsString As String
        Dim BitrateBitsString As String
        Dim FreqBitsString As String
        Dim ChannelModeBitsString As String
    End Structure

    Public Structure IDTAG
        Dim SongTitle As String
        Dim Artist As String
        Dim Album As String
        Dim Year As String
        Dim Comment As String
        Dim GenreID As Integer
        Dim Url As String
        Dim size As String
        Dim formato As String
        Dim Duracao As String
    End Structure

    Public Structure XingHeader
        Dim NumberOfFrames As Integer
        Dim FileLenght As Integer
        Dim TOC() As Integer
        Dim VBRScale As Integer
    End Structure

    Public Enum MPEGType
        MPEG1
        MPEG2
        MPEG2_5
    End Enum

    Public Enum LayerType
        LayerI
        LayerII
        LayerIII
    End Enum

    Public Enum ProtectionType
        ProtectedByCRC
        NotProtected
    End Enum

    Public Enum CopyRight
        CopyRighted
        NotCopyRighted
    End Enum

    Public Enum ChannelMode
        Stereo
        JointStereo_Stereo
        DualChannel_2MonoChannels
        SingleChannel_Mono
    End Enum

    '-------------------------------------------------------------------------------------------------------
    'esta propriedade de apenas leitura devolve uma estrutura Tag.ID3v1TA.
    '-------------------------------------------------------------------------------------------------------
    Public ReadOnly Property ID3v1() As IDTAG
        Get
            Return GetID3v1TAG(Mp3FInfo.FullName)
        End Get
    End Property

    '-------------------------------------------------------------------------------------------------------
    'Esta propriedade devolve o System.IO.FileInfo do ficheiro Mp3.
    '-------------------------------------------------------------------------------------------------------
    Public ReadOnly Property Mp3FileInfo()
        Get
            Return Mp3FInfo
        End Get
    End Property

    '-------------------------------------------------------------------------------------------------------
    'Constructor
    '-------------------------------------------------------------------------------------------------------
    Public Sub New(ByVal MP3FilePath As String)
        'Procura pela existencia do ficheiro mp3 e faz uma excepçao se este nao existir.
        If File.Exists(MP3FilePath) Then
            'Obtem o Mp3FileInfo a  partir do caminho dado.
            Mp3FInfo = New FileInfo(MP3FilePath)

            If Not GetMP3HeaderBytes(Mp3FInfo.FullName) Then
                'Faz uma excepçao se o GetMP3HeaderBytes() falhar.
                'Throw New Exception("O ficheiro '" & MP3FilePath & "' não é um ficheiro de mp3 válido.")
            End If
        Else
            Throw New Exception("O ficheiro '" & MP3FilePath & "' não existe.")
        End If
    End Sub

    '**************************************** - Private Procedures - ***************************************

    '-------------------------------------------------------------------------------------------------------
    'GetMp3FileStream(ByVal MP3FilePath As String) As FileStream : Devolve um System.IO.FileStream a partir do caminho dado.
    '-------------------------------------------------------------------------------------------------------
    Private Function GetMp3FileStream(ByVal MP3FilePath As String) As FileStream
        Dim MP3FileStream As FileStream
        Try
            MP3FileStream = New FileStream(MP3FilePath, FileMode.Open)
        Catch Exc As IOException
            'Throw New Exception("Can't open file '" & MP3FilePath & "'.")
            Exit Function
        Catch Exc As Exception
            Throw New Exception("Ocorreu um erro ao tentar abrir o ficheiro '" & MP3FilePath & "'.")
            Exit Function
        End Try
        If MP3FileStream.CanRead Then
            MP3FileStream.Position = 0
        Else
            Throw New Exception("Não é possível ler o ficheiro '" & MP3FilePath & "'.")
            Exit Function
        End If
        Return MP3FileStream
    End Function

    '-------------------------------------------------------------------------------------------------------
    'GetMP3HeaderBytes(ByVal MP3FilePath As String) As Boolean : Obtem um BitArray através das estruturas Mp3HeaderBits e Mp3HeaderBitsStr. Devolve um valor em booleano, falso se este der erro.
    '-------------------------------------------------------------------------------------------------------
    Private Function GetMP3HeaderBytes(ByVal MP3FilePath As String) As Boolean
        Dim MP3FileStream As FileStream = GetMp3FileStream(MP3FilePath)
        Dim TempMP3HeaderBits As BitArray
        Dim MP3HeaderBytes(2) As Byte
        Dim IsSyncByte As Boolean
        Dim i, j, Index, BitOffSet, StartPosition As Integer
        MP3HeaderBits = New BitArray(24)

        Try
            While (MP3FileStream.Position + 4) <= MP3FileStream.Length

                'Lê um byte do ficheiro e verifica se os seus bits sao “11111111” 
                '(isto corresponde a um valor inteiro de valor=255)
                If MP3FileStream.ReadByte = 255 Then
                    Mp3HeaderPosition = MP3FileStream.Position
                    IsSyncByte = True
                End If

                While IsSyncByte

                    'Le os proximos 3 bytes
                    MP3FileStream.Read(MP3HeaderBytes, 0, 3)
                    TempMP3HeaderBits = New BitArray(MP3HeaderBytes)

                    'verifica os bytes nr 9,10,11 para asegurar que têm a sincronizaçao => the Mp3 Header bytes
                    For i = 7 To 5 Step -1
                        If Not TempMP3HeaderBits.Item(i) Then
                            IsSyncByte = False
                            MP3FileStream.Position -= 3
                            Exit While
                        End If
                    Next
                    Index = 0
                    BitOffSet = 0

                    'Agora temos os principais bits do ficheiro mas estao na forma "8 7 6 5 4 3 2 1 16 15 14..." 
                    'depois pomos na forma de "1 2 3 4 5 6 7 8 10 11..." com os 2 loops seguintes.
                    For j = 0 To 2
                        For i = 7 To 0 Step -1
                            MP3HeaderBits.Item(Index) = TempMP3HeaderBits.Item(BitOffSet + i)
                            Index += 1
                        Next
                        BitOffSet += 8
                    Next

                    'Obtem a estrutura do Mp3HeaderBitsStr
                    With Mp3HeaderBitsStr
                        .MPEGTypeBitsString = GetBitsString(3, 4)
                        .LayerBitsString = GetBitsString(5, 6)
                        .BitrateBitsString = GetBitsString(8, 11)
                        .FreqBitsString = GetBitsString(12, 13)
                        .ChannelModeBitsString = GetBitsString(16, 17)

                        'Procura um valor errado MPEGTypeBits) 
                        If .MPEGTypeBitsString = "01" Then
                            IsSyncByte = False
                            MP3FileStream.Position -= 3
                            Exit While
                        End If

                        'Procura um valor errado na LAyerBits, apenas precisamos da LayerIII
                        If .LayerBitsString = "11" Or .LayerBitsString = "10" Or .LayerBitsString = "00" Then
                            IsSyncByte = False
                            MP3FileStream.Position -= 3
                            Exit While
                        End If

                        'Procura um valor errado na taxa de bits, nao podem ser tipo “1111”
                        If .BitrateBitsString = "1111" Then
                            IsSyncByte = False
                            MP3FileStream.Position -= 3
                            Exit While
                        End If

                        'Procura um valor errado na FrequencyBits
                        If .FreqBitsString = "11" Then
                            IsSyncByte = False
                            MP3FileStream.Position -= 3
                            Exit While
                        Else
                            'Se tudo em cima estiver correcto, temos o nosso Mp3Header
                            Mp3ClearSize = (MP3FileStream.Length - Mp3HeaderPosition - 128)
                            Return True
                        End If
                    End With

                End While
            End While

            Return False
        Catch Exc As Exception
            'Throw New Exception(Exc.Message)
        Finally
            MP3FileStream.Close()
        End Try
    End Function

    '-------------------------------------------------------------------------------------------------------
    'CheckForZeroBytes(ByVal ByteToCheck As Byte) As Byte
    '-------------------------------------------------------------------------------------------------------
    Private Function CheckForZeroBytes(ByVal ByteToCheck As Byte) As Byte
        Dim ByteToStringConv As New System.Text.UTF8Encoding()
        Dim EmptyCharByte() As Byte = ByteToStringConv.GetBytes(" ")
        If ByteToCheck = 0 Then
            Return EmptyCharByte(0)
        Else
            Return ByteToCheck
        End If
    End Function

    '-------------------------------------------------------------------------------------------------------
    'GetBitsString(ByVal StartIndex As Integer, ByVal EndIndex As Integer) As String
    '-------------------------------------------------------------------------------------------------------
    Private Function GetBitsString(ByVal StartIndex As Integer, ByVal EndIndex As Integer) As String
        Dim BitsString As String
        Dim i As Integer
        For i = StartIndex To EndIndex
            Select Case MP3HeaderBits.Item(i)
                Case True : BitsString += "1"
                Case False : BitsString += "0"
            End Select
        Next
        Return BitsString
    End Function
    Sub tamanho(ByVal tamanho As Object)
        Dim nBytes As Double
        Dim nBytes2 As Double

        If tamanho > 1024 Then
            nBytes = Val(tamanho / 1024)
            nBytes2 = Val(nBytes / 1024)
            If nBytes2 < 1 Then
                'KB
                nBytes = Math.Round(nBytes, 2).ToString("F1")
                nSize = nBytes & " " & "KB"
            Else
                'MB
                nBytes2 = Math.Round(nBytes2, 2).ToString("F2")
                nSize = nBytes2 & " " & "MB"
            End If

        End If
    End Sub

    '-------------------------------------------------------------------------------------------------------
    'GetID3v1TAG(ByVal MP3FilePath) As ID3v1TAG. 
    '-------------------------------------------------------------------------------------------------------
    Private Function GetID3v1TAG(ByVal MP3FilePath) As IDTAG
        Dim MP3FileStream As FileStream = GetMp3FileStream(MP3FilePath)
        Dim ByteToStringConv As New System.Text.UTF8Encoding()
        Dim CID3v1 As IDTAG
        Dim ID3v1Bytes(127), SongTitleBytes(29), ArtistBytes(29) As Byte
        Dim AlbumBytes(29), CommentBytes(29), YearBytes(3) As Byte
        Dim i As Integer
        Dim MySize As String = FileLen(MP3FilePath)
        ' Dim fs As New FileStream(MP3FilePath, FileMode.Open)

        ' Le a tag MP3.
        Try
            MP3FileStream.Seek(0 - 128, SeekOrigin.End)
        Catch ex As Exception
        End Try

        Dim tag(2) As Byte
        MP3FileStream.Read(tag, 0, 3)

        ' Verifa se a tag existe.

        If System.Text.Encoding.ASCII.GetString(tag).Trim = "TAG" Then
            verificaTag = True

            Try
                'Obtemos os ultimos 128 bytes do ficheiro
                MP3FileStream.Position = MP3FileStream.Length - 128
                MP3FileStream.Read(ID3v1Bytes, 0, 128)
            Catch
                Throw New Exception("Não é possível ler o ficheiro '" & MP3FilePath & "'.")
                Exit Function
            Finally
                MP3FileStream.Close()
            End Try

            'e poe-nos em arrays individuais para facilitar o trabalho
            For i = 0 To 29
                SongTitleBytes(i) = CheckForZeroBytes(ID3v1Bytes(3 + i))
                ArtistBytes(i) = CheckForZeroBytes(ID3v1Bytes(33 + i))
                AlbumBytes(i) = CheckForZeroBytes(ID3v1Bytes(63 + i))
                CommentBytes(i) = CheckForZeroBytes(ID3v1Bytes(97 + i))
            Next

            For i = 0 To 3
                YearBytes(i) = CheckForZeroBytes(ID3v1Bytes(93 + i))
            Next

            'Definimos a nossa estrutura CID3v1 e devolve-nos a mesma
            With CID3v1
                .SongTitle = Path.GetFileNameWithoutExtension(MP3FilePath)
                .Artist = ByteToStringConv.GetString(ArtistBytes)
                .Album = ByteToStringConv.GetString(AlbumBytes)
                .Comment = ByteToStringConv.GetString(CommentBytes)
                .Year = Trim(ByteToStringConv.GetString(YearBytes))
                .GenreID = CType(ID3v1Bytes(127), Integer)
                .Url = MP3FilePath
                tamanho(MySize)
                If .Year.Length < 4 Then
                    .Year = Nothing
                End If
                extensao = Path.GetExtension(MP3FilePath)
                Select Case LCase(extensao)
                    Case ".aac"
                        .formato = "AAC"
                    Case ".mp3"
                        .formato = "MP3"
                    Case ".m4a"
                        .formato = "M4A"
                    Case ".wma"
                        .formato = "WMA"
                End Select
                .size = nSize
            End With
        Else
            verificaTag = False

            'obtem e guarda o nome da musica
            tamanho(MySize)
            With CID3v1
                .SongTitle = Path.GetFileNameWithoutExtension(MP3FilePath)
                .Url = MP3FilePath
                .size = nSize
                extensao = Path.GetExtension(MP3FilePath)
                Select Case LCase(extensao)
                    Case ".aac"
                        .formato = "AAC"
                    Case ".mp3"
                        .formato = "MP3"
                    Case ".m4a"
                        .formato = "M4A"
                    Case ".wma"
                        .formato = "WMA"
                End Select
            End With

        End If
        'fs.Close()


        Return CID3v1
    End Function

    '***************************************** - Public Procedures - ***************************************

    '-------------------------------------------------------------------------------------------------------
    'IsVBR() : Devolve Verdadeiro se o ficheiro Mp3 for VBR.
    '-------------------------------------------------------------------------------------------------------
    Public Function IsVBR() As Boolean
        Dim MP3FileStream As FileStream = GetMp3FileStream(Mp3FInfo.FullName)
        Dim ByteToStringConv As New System.Text.UTF8Encoding()
        Dim XingBytes(3) As Byte

        Try
            If Not GetChannelMode() = ChannelMode.SingleChannel_Mono Then
                MP3FileStream.Position = Mp3HeaderPosition + 35
                MP3FileStream.Read(XingBytes, 0, 4)
            Else
                MP3FileStream.Position = Mp3HeaderPosition + 20
                MP3FileStream.Read(XingBytes, 0, 4)
            End If

            If ByteToStringConv.GetString(XingBytes) = "Xing" Then
                MP3FileStream.Read(XingHeaderBytes, 0, 116)
                Return True
            Else
                Return False
            End If
        Catch
            'Faz uma excepçao
            'Throw New Exception("Can't read file '" & Mp3FInfo.FullName & "'.")
            Exit Function
        Finally
            'MP3FileStream.Close()
        End Try

    End Function

    '-------------------------------------------------------------------------------------------------------
    'GetXingHeader() : Devolve uma estrutura XingHeader.
    '-------------------------------------------------------------------------------------------------------
    Public Function GetXingHeader() As XingHeader
        Dim CXingHeader As XingHeader
        Dim BitConv As BitConverter
        Dim FrameCountBytes(3), FileLenghtBytes(3), VBRScaleBytes(3) As Byte
        Dim Index, TOC(99), i As Integer

        If IsVBR() Then
            For i = 3 To 0 Step -1
                VBRScaleBytes(Index) = XingHeaderBytes(112 + i)
                FrameCountBytes(Index) = XingHeaderBytes(4 + i)
                FileLenghtBytes(Index) = XingHeaderBytes(8 + i)
                Index += 1
            Next

            For i = 0 To 99
                TOC(i) = CType(XingHeaderBytes(12 + i), Integer)
            Next
            verificaGetXingHeader = True
        Else
            'Throw New Exception("'" & Mp3FInfo.FullName & "' não é VBR")
            verificaGetXingHeader = False
            Exit Function
        End If

        With CXingHeader
            .FileLenght = BitConv.ToInt32(FileLenghtBytes, 0)
            .NumberOfFrames = BitConv.ToInt32(FrameCountBytes, 0)
            .VBRScale = BitConv.ToInt32(VBRScaleBytes, 0)
            .TOC = TOC
        End With
        Return CXingHeader

    End Function

    '-------------------------------------------------------------------------------------------------------
    'GetMPEGType() : Devolve o tipo MPEG do ficheiro MPEG(Mp3) seleccionado, como uma enumeraçao Mp3Class.PEGType.
    '-------------------------------------------------------------------------------------------------------
    Public Function GetMPEGType() As MPEGType

        Select Case Mp3HeaderBitsStr.MPEGTypeBitsString
            Case "11" : Return MPEGType.MPEG1
            Case "10" : Return MPEGType.MPEG2
            Case "00" : Return MPEGType.MPEG2_5
        End Select
    End Function

    '-------------------------------------------------------------------------------------------------------
    'GetLayer() : Devolve o tipo layer do ficheiro Mp3 seleccionado, como uma enumeraçao Mp3Class.LayerType.
    '-------------------------------------------------------------------------------------------------------
    Public Function GetLayer() As LayerType

        Select Case Mp3HeaderBitsStr.LayerBitsString
            Case "01" : Return LayerType.LayerIII
            Case "10" : Return LayerType.LayerII
            Case "11" : Return LayerType.LayerI
        End Select
    End Function

    '-------------------------------------------------------------------------------------------------------
    'GetProtection() as ProtectionType :Devolve o tipo de Proteccçao do ficheiro Mp3 seleccionado, como uma enumeraçao Mp3Class.ProtectionType.
    '-------------------------------------------------------------------------------------------------------
    Public Function GetProtection() As ProtectionType
        If Not MP3HeaderBits.Item(7) Then
            Return ProtectionType.ProtectedByCRC
        Else
            Return ProtectionType.NotProtected
        End If
    End Function

    '-------------------------------------------------------------------------------------------------------
    'GetBitrate() as Integer :Devolve a taxa de bits do ficheiro Mp3 seleccionado, em bits por segundo
    '-------------------------------------------------------------------------------------------------------
    Public Function GetBitrate() As Integer
        Dim BitrateArray() As Integer

        If GetMPEGType() = MPEGType.MPEG1 Then
            Dim TmpBitrateArray() As Integer = {32, 40, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 256, 320}
            BitrateArray = TmpBitrateArray
        Else
            Dim TmpBitrateArray() As Integer = {8, 16, 24, 32, 40, 48, 56, 64, 80, 96, 112, 128, 144, 160}
            BitrateArray = TmpBitrateArray
        End If

        If Not IsVBR() Then
            Select Case Mp3HeaderBitsStr.BitrateBitsString
                Case "0001" : Return BitrateArray(0) * 1000
                Case "0010" : Return BitrateArray(1) * 1000
                Case "0011" : Return BitrateArray(2) * 1000
                Case "0100" : Return BitrateArray(3) * 1000
                Case "0101" : Return BitrateArray(4) * 1000
                Case "0110" : Return BitrateArray(5) * 1000
                Case "0111" : Return BitrateArray(6) * 1000
                Case "1000" : Return BitrateArray(7) * 1000
                Case "1001" : Return BitrateArray(8) * 1000
                Case "1010" : Return BitrateArray(9) * 1000
                Case "1011" : Return BitrateArray(10) * 1000
                Case "1100" : Return BitrateArray(11) * 1000
                Case "1101" : Return BitrateArray(12) * 1000
                Case "1110" : Return BitrateArray(13) * 1000
            End Select
        Else

            Dim CXingHeader As XingHeader = GetXingHeader()
            Dim LastByte, AverageFrameLenght, AverageBitrate As Integer

            If verificaGetXingHeader = False Then
                Exit Function
            End If

            With CXingHeader
                LastByte = Math.Round((.TOC(99) / 256) * .FileLenght, 0)
                AverageFrameLenght = Math.Round(.FileLenght / .NumberOfFrames, 0)
                AverageBitrate = Math.Round(((AverageFrameLenght * GetSamplingRateFreq()) / 144) / 1000, 0)
            End With
            Return AverageBitrate * 1000
        End If
    End Function

    '-------------------------------------------------------------------------------------------------------
    'GetSamplingRateFreq() as Integer : Devolve a taxa de amostragem da frequencia do ficheiro Mp3 seleccionado.
    '-------------------------------------------------------------------------------------------------------
    Public Function GetSamplingRateFreq() As String

        Select Case Mp3HeaderBitsStr.FreqBitsString
            Case "00" : Return "44,100"
            Case "01" : Return "48,000"
            Case "10" : Return "32,000"
            Case "11" : Return "22,050"
        End Select
    End Function

    '-------------------------------------------------------------------------------------------------------
    'GetChannelMode() as ChannelMode : Devolve o tipo de Canal do ficheiro Mp3 seleccionado, como uma enumeraçao Mp3Class.ChannelMode.
    '-------------------------------------------------------------------------------------------------------
    Public Function GetChannelMode() As ChannelMode

        Select Case Mp3HeaderBitsStr.ChannelModeBitsString
            Case "00" : Return ChannelMode.Stereo
            Case "01" : Return ChannelMode.JointStereo_Stereo
            Case "10" : Return ChannelMode.DualChannel_2MonoChannels
            Case "11" : Return ChannelMode.SingleChannel_Mono
        End Select
    End Function

    '-------------------------------------------------------------------------------------------------------
    'GetCopyRight() as CopyRight : Devolve o tipo de Copyright do ficheiro MPEG(Mp3) seleccionado, como uma enumeraçao Mp3Class.CopyRight.
    '-------------------------------------------------------------------------------------------------------
    Public Function GetCopyRight() As CopyRight
        If MP3HeaderBits.Item(20) Then
            Return CopyRight.CopyRighted
        Else
            Return CopyRight.NotCopyRighted
        End If
    End Function

    '-------------------------------------------------------------------------------------------------------
    'GetDuration() as Integer : Devolve a duraçao do ficheiro Mp3 seleccionado, em segundos.
    '-------------------------------------------------------------------------------------------------------
    Public Function GetDuration() As Integer
        Try
            Dim Duration As Integer = Math.Round(((Mp3ClearSize * 8) / GetBitrate()), 0)
            Return Duration
        Catch ex As Exception
        End Try

    End Function

    '-------------------------------------------------------------------------------------------------------
    'GetDurationString() as String : Devolve uma string com a duraçao do ficheiro Mp3 seleccionado na fomra:hh:mm:ss.
    '-------------------------------------------------------------------------------------------------------
    Public Function GetDurationString() As String
        Try
            Dim DurationString As String
            Dim CurrentDuration As Integer = GetDuration()
            Dim DurationHour As Integer = CurrentDuration \ 3600
            Dim DurationMin As Integer
            Dim DurationSec As Integer

            If DurationHour >= 1 Then
                DurationMin = ((CurrentDuration Mod 3600) \ 60)
                DurationSec = ((CurrentDuration Mod 3600) Mod 60)
                DurationString = Format(DurationHour, "00") & ":" & Format(DurationMin, "00") & ":" & Format(DurationSec, "00")
            Else
                DurationMin = CurrentDuration \ 60
                DurationSec = CurrentDuration Mod 60
                DurationString = Format(DurationMin, "00") & ":" & Format(DurationSec, "00")
            End If
            Return DurationString
        Catch ex As Exception
        End Try

    End Function

    '-------------------------------------------------------------------------------------------------------
    'GetGenreString(ByVal GenreID As Integer) As String : Devolve o nome do genero que corresponde a um dos genreID.
    '-------------------------------------------------------------------------------------------------------
    Public Function GetGenreString(ByVal GenreID As Integer) As String
        If verificaTag = True Then


            Dim AvailableGenres() As String = {"Blues", "Classic Rock", "Country", "Dance", "Disco", "Funk", "Grunge", _
            "Hip - Hop", "Jazz", "Metal", "New Age", "Oldies", "Other", "Pop", "R&B", "Rap", "Reggae", "Rock", "Techno", _
            "Industrial", "Alternative", "Ska", "Death Metal", "Pranks", "Soundtrack", "Euro -Techno", "Ambient", _
            "Trip -Hop", "Vocal", "Jazz Funk", "Fusion", "Trance", "Classical", "Instrumental", "Acid", "House", "Game", _
            "Sound Clip", "Gospel", "Noise", "AlternRock", "Bass", "Soul", "Punk", "Space", "Meditative", _
            "Instrumental Pop", "Instrumental Rock", "Ethnic", "Gothic", "Darkwave", "Techno -Industrial", "Electronic", _
            "Pop -Folk", "Eurodance", "Dream", "Southern Rock", "Comedy", "Cult", "Gangsta", "Top 40", "Christian Rap", _
            "Pop/Funk", "Jungle", "Native American", "Cabaret", "New Wave", "Psychadelic", "Rave", "Showtunes", "Trailer", _
            "Lo - Fi", "Tribal", "Acid Punk", "Acid Jazz", "Polka", "Retro", "Musical", "Rock & Roll", "Hard Rock", _
            "Folk", "Folk/Rock", "National Folk", "Swing", "Bebob", "Latin", "Revival", "Celtic", "Bluegrass", "Avantgarde", _
            "Gothic Rock", "Progressive Rock", "Psychedelic Rock", "Symphonic Rock", "Slow Rock", "Big Band", "Chorus", _
            "Easy Listening", "Acoustic", "Humour", "Speech", "Chanson", "Opera", "Chamber Music", "Sonata", "Symphony", _
            "Booty Bass", "Primus", "Porn Groove", "Satire", "Slow Jam", "Club", "Tango", "Samba", "Folklore", "Ballad", _
            "Power Ballad", "Rhythmic Soul", "Freestyle", "Duet", "Punk Rock", "Drum Solo", "A Cappella", "Euro - House", _
             "Dance Hall", "Goa", "Drum & Bass", "Club - House", "Hardcore", "Terror", "Indie", "BritPop", "Negerpunk", _
            "Polsk Punk", "Beat", "Christian Gangsta Rap", "Heavy Metal", "Black Metal", "Crossover", "Contemporary Christian", _
            "Christian Rock", "Merengue", "Salsa", "Thrash Metal", "Anime", "JPop", "Synthpop"}

            Try
                Return AvailableGenres(GenreID)
            Catch
                Return " "
            End Try
        Else
            Return ""
        End If
    End Function
End Class
