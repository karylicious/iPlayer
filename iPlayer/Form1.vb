Imports WMPLib
Imports System.Management
Imports System.IO
Imports System.Text

Public Class JanelaPrincipal
    Dim NomeMusica, nAlbum, nInterprete, NomeFormato, Canal, nCopyright, Url, _
    Comentarios, nGenero, RletraMusica, p, RletraInterprete, PinicialMusica, _
    duracaoT, LpalavraMusica, RpalavraMusica, Frequencia, NumStar, nDuracao, _
    caminho, PinicialInterprete, nTamanho, n, d, LpalavraInterprete, nt, RpalavraInterprete, RpalavraAlbum, LpalavraAlbum, PinicialAlbum, RletraAlbum As String
    Dim DataActual As Date
    Dim b As Boolean = False
    Dim r As New Random
    Dim CopyArray() As Integer
    Dim verificaShuffle As Boolean = False
    Dim verificaRepeatOne As Boolean = False
    Dim verificaRepeatAll As Boolean = False
    Dim AReprod_CD As Boolean = False
    Dim verificaPlay As Boolean = True
    Dim verificaPausa = False
    Dim verificaCD As Boolean = False
    Dim verificaEliminar = False
    Dim verificaTRadio As Boolean = False
    Dim button1 As New Button
    Dim tagpage As Boolean = True
    Dim verificaIndex As Boolean = False
    Public verificaV As Boolean = True
    Dim selected As Boolean = False
    Dim verificaPesquisa As Boolean = False
    Dim verificaPreenchido As Boolean = False
    Dim verificaCdDados As Boolean = False
    Dim verificAddFile As Boolean = False
    Dim num As Double
    Dim VerificaCopiar As Boolean = False
    Dim verificaStop As Boolean = False
    Dim verificaSelec As Boolean = False
    Dim verifican As Boolean = False
    Dim RadioOn As Boolean = False
    Dim verificaReproduzir As Boolean = False
    Dim MusicasATocar() As String
    Dim NomesAMostrar() As String
    Dim AlBunsAMostrar() As String
    Dim InterAMostrar() As String
    Dim arrayAux() As String
    Dim DuracaoAMostrar() As String
    Dim local As Integer = 32
    Dim IndexSelec As Integer = -1
    Dim NumReproduzidas() As Integer
    Dim primeiravez As Boolean = False
    Dim CountAReprod As Integer = -1
    Public NomeBtnActual As String
    Public TagBtnActual As Integer
    Dim nAno, TaxaBits, NumMusica, NumHistorico, minutos, Seg, horas, Meses, Anos, Semanas, dias, opc, n1, contador1, contador2, n2, n3, contador3, _
    DiaActual, MesActual, LTamanhoFinalMusica, RTamanhoFinalMusica, TamanhoInicioMusica, NumFormato, _
     SegundosMenos, MinutosMenos, naux, pMusica, index, Min, numTag, numLenMusica, numLenInterprete, _
    LTamanhoFinalInterprete, RTamanhoFinalInterprete, indexAReprod, pInterprete, TamanhoInicioInterprete, _
    TamanhoInicioAlbum, LTamanhoFinalAlbum, PlaylistActual, NumMusicaAReprod, k, numLenAlbum, pAlbum, RTamanhoFinalAlbum As Integer
    Dim KB, MB As Double
    Dim TemCapaBool As Boolean = False
    Dim CapaSemBool As Boolean = False
    Dim valido As Boolean = False
    Dim ClickBtn As Boolean = False
    Dim btn As Button
    Dim DataTableSemNome, DataTablePlaylist, DataTableHistorico As DataTable
    Dim loading As Boolean = False
    Dim Sync As Boolean = False
    Private WithEvents EventWatcher As ManagementEventWatcher
    Private _Query As WqlEventQuery
    Dim Files As String()
    Dim Drives() As DriveInfo

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DAL.CreateConnection(Application.StartupPath, "iPlayerOutroTipo.mdb")
        DAL.OpenConnection()

        StartWatcher()

        Dim playlist As WMPLib.IWMPPlaylist = AxWindowsMediaPlayer1.cdromCollection.Item(0).Playlist
        'verifica se o cd é um cd de dados
        Try
            d = playlist.Item(0).durationString
            If d = "00:00" Then
                verificaCdDados = True
            End If
        Catch ex As Exception
        End Try
        AxWindowsMediaPlayer1.URL = ""


        DiaActual = Now.Day
        MesActual = Now.Month
        If MesActual = 2 Then
            If DiaActual >= 28 And DiaActual <= 29 Then
                iPlayer_DAL.Playlist.LimparMusicasAntigas()
            End If
        Else
            If DiaActual >= 30 And DiaActual <= 31 Then
                iPlayer_DAL.Playlist.LimparMusicasAntigas()
            End If
        End If

        If playlist.count >= 1 Then
            If verificaCdDados = False Then
                BtnAudioCD.BackColor = Color.LightBlue
                BtnAudioCD.FlatAppearance.MouseDownBackColor = Color.LightBlue
                BtnAudioCD.FlatAppearance.MouseOverBackColor = Color.LightBlue
                BtnEjectarDisco.BackColor = Color.LightBlue
                BtnEjectarDisco.FlatAppearance.MouseDownBackColor = Color.LightBlue
                BtnEjectarDisco.FlatAppearance.MouseOverBackColor = Color.LightBlue
                PlaylistActual = 2
                BtnMusicas.BackColor = Color.Transparent
                BtnMusicas.FlatAppearance.MouseDownBackColor = Color.Silver
                BtnMusicas.FlatAppearance.MouseOverBackColor = Color.Silver
                verificaCD = True
                'BtnAudioCD.Text = playlist.name()
                PreencherCDAudio()
                naux = 2
            Else
                coisas2()
                verificaCD = False
                PlaylistActual = 1
                PreencherPlaylist(PlaylistActual)
            End If
        Else
            coisas2()
            verificaCD = False
            PlaylistActual = 1
            PreencherPlaylist(PlaylistActual)
        End If
        verificaCDAudio()
        DataActual = Now.Day & "-" & Now.Month & "-" & Now.Year & " " & Now.Hour & ":" & Now.Minute
        CarregarAsNovasPlaylist()
        opc = 1
        Me.Opacity = 0.1
        TimerOpacityLoad.Start()

        AxWindowsMediaPlayer1.settings.volume = TrackBar1.Value * 10
        AxWindowsMediaPlayer1.enableContextMenu = False
        With PictureBoxCapa
            .AllowDrop = True
        End With
        ReDim NumReproduzidas(ListView1.Items.Count - 1)
        For i As Integer = 0 To NumReproduzidas.Count - 1
            NumReproduzidas(i) = -1
        Next
    End Sub

    Private Sub InforToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        JanelaInformacoes.Show()
    End Sub

    Sub NovaPlaylist()

        Dim Button As New Button()
        Dim NomePlaylist As String

        Button.Parent = Panel3
        Button.Dock = DockStyle.None
        Button.ContextMenuStrip = ContextMenuStripPlaylist
        Button.Size = New Size(207, 34)
        Button.FlatStyle = FlatStyle.Flat
        Button.BackColor = Color.Silver
        Button.FlatAppearance.BorderSize = 0
        Button.FlatAppearance.MouseDownBackColor = Color.Transparent
        Button.FlatAppearance.MouseOverBackColor = Color.Transparent
        Button.Image = iPlayer.My.Resources.Resources.music_file_24x24
        Button.ImageAlign = ContentAlignment.MiddleLeft

        Dim Count As Integer = iPlayer_DAL.Playlist.CountFromPlaylist

        If Count < 7 Then

            Button.Text = "Lista sem nome1"
            Button.Tag = 7
            iPlayer_DAL.Playlist.Adicionar(Button.Text, Button.Tag, 10, Button.Text)

            Dim Tool As New ToolStripMenuItem("Lista sem nome1", iPlayer.My.Resources.Resources.music_file_24x24, New EventHandler(AddressOf NenhuToolStripMenuItem2_Click))
            Tool.Tag = 10
            AdicionarÀListaDeReproduçãoToolStripMenuItem.Enabled = True
            AdicionarÀListaDeReproduçãoToolStripMenuItem.DropDownItems.Add(Tool)

        ElseIf Count >= 7 Then
            For i As Integer = Count To Count

                Dim maiorTag As Integer = iPlayer_DAL.Playlist.MaiorTag
                Dim maiorTagContext As Integer = iPlayer_DAL.Playlist.MaiorTagContextMenuStrip

                DataTablePlaylist = iPlayer_DAL.Playlist.MostrarUsandoTag2(maiorTag)
                Dim NomeAux As String = DataTablePlaylist.Rows(0).Item("NomePlaylist")
                Dim h As Integer = CInt(Mid(NomeAux, 15))

                h += 1
                maiorTag += 1
                maiorTagContext += 1
                NomePlaylist = "Lista sem nome" & h
                iPlayer_DAL.Playlist.Adicionar(NomePlaylist, maiorTag, maiorTagContext, NomePlaylist)

                Button.Text = NomePlaylist
                Button.Tag = maiorTag

                Dim Tool As New ToolStripMenuItem(NomePlaylist, iPlayer.My.Resources.Resources.music_file_24x24, New EventHandler(AddressOf NenhuToolStripMenuItem2_Click))
                Tool.Tag = maiorTagContext
                AdicionarÀListaDeReproduçãoToolStripMenuItem.Enabled = True
                AdicionarÀListaDeReproduçãoToolStripMenuItem.DropDownItems.Add(Tool)
            Next
        End If

        Panel3.Controls.Add(Button)
        Button.BringToFront()
        Button.Dock = DockStyle.Top
        button1 = Button

        AddHandler button1.Click, AddressOf BtnMusicas_Click
        AddHandler button1.MouseDown, AddressOf BtnMusicas_Click


    End Sub

    Sub CarregarAsNovasPlaylist()

        Dim Count As Integer = iPlayer_DAL.Playlist.CountFromPlaylist
        Dim NomePlaylist As String

        For i As Integer = 7 To Count + 1

            DataTablePlaylist = iPlayer_DAL.Playlist.MostrarUsandoTag2(i)
            If DataTablePlaylist.Rows.Count <> 0 Then
                Dim Button As New Button()

                Button.Parent = Panel3
                Button.Location = New Point(0, 0)
                Button.Dock = DockStyle.Top
                Button.ContextMenuStrip = ContextMenuStripPlaylist
                Button.Size = New Size(207, 34)
                Button.FlatStyle = FlatStyle.Flat
                Button.BackColor = Color.Silver
                Button.FlatAppearance.BorderSize = 0
                Button.FlatAppearance.MouseDownBackColor = Color.Transparent
                Button.FlatAppearance.MouseOverBackColor = Color.Transparent
                Button.Image = iPlayer.My.Resources.Resources.music_file_24x24
                Button.ImageAlign = ContentAlignment.MiddleLeft



                Dim TagContext As Integer = DataTablePlaylist.Rows(0).Item("TagContextMenuStrip")

                Tag = i
                NomePlaylist = DataTablePlaylist.Rows(0).Item("NomeAux")

                Button.Text = NomePlaylist
                Button.Tag = Tag

                Dim Tool As New ToolStripMenuItem(NomePlaylist, iPlayer.My.Resources.Resources.music_file_24x24, New EventHandler(AddressOf NenhuToolStripMenuItem2_Click))
                Tool.Tag = TagContext
                AdicionarÀListaDeReproduçãoToolStripMenuItem.Enabled = True
                AdicionarÀListaDeReproduçãoToolStripMenuItem.DropDownItems.Add(Tool)
                Panel3.Controls.Add(Button)
                Button.BringToFront()
                Button.Dock = DockStyle.Top
                button1 = Button

                AddHandler button1.Click, AddressOf BtnMusicas_Click
                AddHandler button1.MouseDown, AddressOf BtnMusicas_Click

            End If
        Next
    End Sub

    Private Sub ContextMenuStripListView_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStripListView.Opening
        If PlaylistActual = 2 Then
            e.Cancel = (PlaylistActual = 2)
        Else
            Dim indexes As ListView.SelectedIndexCollection = ListView1.SelectedIndices
            If indexes.Count = 1 Then
                InformaçõesToolStripMenuItem2.Enabled = True
                ReiniciarContagemReprodToolStripMenuItem2.Enabled = True
            ElseIf indexes.Count > 1 Then
                ReiniciarContagemReprodToolStripMenuItem2.Enabled = True
                InformaçõesToolStripMenuItem2.Enabled = False
                InforToolStripMenuItem.Enabled = False
            Else
                e.Cancel = (indexes.Count = 0)
            End If
        End If
    End Sub

    Sub AddFile(ByVal caminho As String)
        'instancia a classe 
        Dim Audio As Tag
        Audio = New Tag(caminho)
        Dim Estrutura As Tag.IDTAG = Audio.ID3v1
        Dim AudioFileInfo As System.IO.FileInfo = Audio.Mp3FileInfo
        nDuracao = Audio.GetDurationString
        If nDuracao = "00:00" Then
            System.Windows.Forms.MessageBox.Show("Não foi possível adicionar o ficheiro" & " " & "'" & Estrutura.SongTitle & "'" & "!", "iPlayer", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        nInterprete = Trim(Estrutura.Artist)
        nAlbum = Trim(Estrutura.Album)
        NomeMusica = Trim(Estrutura.SongTitle)
        Comentarios = Trim(Estrutura.Comment)
        Estrutura.Year = Trim(Estrutura.Year)
        If Estrutura.Year = "" Then
        Else
            nAno = CInt(Estrutura.Year)
        End If

        nGenero = Trim(Audio.GetGenreString(Estrutura.GenreID))
        TaxaBits = Val(Audio.GetBitrate) / 1000
        Frequencia = Audio.GetSamplingRateFreq
        Canal = Trim(Audio.GetChannelMode.ToString)
        nCopyright = Trim(Audio.GetCopyRight.ToString)
        nTamanho = Estrutura.size
        Url = Trim(Estrutura.Url)
        NomeFormato = Trim(Estrutura.formato)
        PreencheBD()
    End Sub

    Private Sub AddFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddFileToolStripMenuItem.Click

        OpenFileDialog1.InitialDirectory = "C:\Users\" & GetUserName() & "\Music\"

        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim musicaExistente As Boolean = False
            Try

                For k As Integer = 0 To DataTableSemNome.Rows.Count - 1
                    Dim l As String = DataTableSemNome.Rows(k).Item("Url")
                    If OpenFileDialog1.FileName = DataTableSemNome.Rows(k).Item("Url") Then
                        musicaExistente = True
                    End If
                Next
            Catch ex As Exception
            End Try

            If musicaExistente = True Then '3
                Dim resposta As DialogResult = System.Windows.Forms.MessageBox.Show("Esta música já existe na Biblioteca. Deseja adicioná-la novamente?", "iPlayer", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If resposta = DialogResult.Yes Then
                    AddFile(OpenFileDialog1.FileName)
                End If
            Else
                AddFile(OpenFileDialog1.FileName)
                If nDuracao = "00:00" Then
                    OpenFileDialog1.FileName = ""
                    Exit Sub
                End If
            End If
            If PlaylistActual = 1 Then
                PreencherPlaylist(1)
            End If
            verificAddFile = True
        End If
        OpenFileDialog1.FileName = ""

    End Sub

    Sub PreencheBD()
        iPlayer_DAL.Interprete.Adicionar(nInterprete)
        iPlayer_DAL.Album.Adicionar(nAlbum)
        iPlayer_DAL.Genero.Adicionar(nGenero)
        NumFormato = iPlayer_DAL.Formato.MostrarNumFormato(NomeFormato)
        iPlayer_DAL.Musica.Adicionar(NumFormato, NomeMusica, nAno, nDuracao, DataActual, Url, nTamanho, TaxaBits, Frequencia, nCopyright, Canal, Comentarios)
    End Sub

    Function duracaoTotal(ByVal duracao As String) As String
        Dim j As String = duracao.Substring(0, 1)

        j = duracao.Substring(0, 2)
        minutos += j
        j = duracao.Substring(3, 2)
        Seg += j
        If Seg >= 60 Then
            Seg = Seg / 60
            minutos += Seg
            Seg = Nothing
        Else
            If minutos = 0 Then
                Return Seg & " " & "Segundo(s),"
            End If
        End If

        If minutos >= 60 Then
            horas = minutos / 60
        End If

        If horas = 24 Then
            dias = Math.Floor(horas / 24)
        ElseIf horas > 24 Then
            dias = Math.Floor(horas / 24)
        End If

        Semanas = Math.Floor(dias / 7)
        Meses = Math.Floor(dias / 31)
        Anos = Math.Floor(Meses / 12)


        If minutos >= 60 Then
            Return horas & " " & "Hora(s),"
        ElseIf minutos <> 0 Then
            Return minutos & " " & "Minuto(s),"
        End If

        If horas >= 24 Then
            Return dias & " " & "Dia(s),"
        ElseIf horas < 24 Then
            If horas <> 0 Then
                Return horas & " " & "Hora(s),"
            End If
        End If

        If dias >= 7 Then
            Return Semanas & " " & "Semana(s),"
        ElseIf dias < 7 Then
            If dias <> 0 Then
                Return dias & " " & "Dia(s),"
            End If
        End If

        If Semanas >= 31 Then
            Return Meses & " " & "Mese(s),"
        ElseIf Semanas < 31 Then
            If Semanas <> 0 Then
                Return Semanas & " " & "Semana(s),"
            End If
        End If

        If Meses >= 12 Then
            Return Anos & " " & "Ano(s),"
        ElseIf Meses < 12 Then
            If Meses <> 0 Then
                Return Meses & " " & "Mese(s),"
            End If
        End If
    End Function

    Public Sub PreencherPlaylist(ByVal NumPlaylist As Integer)

        DataTableSemNome = iPlayer_DAL.Playlist.MostrarUsandoNumPlaylist(NumPlaylist)
        Dim Duracao, duracaoT As String
        If verificaReproduzir = False Then
            ReDim MusicasATocar(DataTableSemNome.Rows.Count - 1)
            ReDim NomesAMostrar(DataTableSemNome.Rows.Count - 1)
            ReDim AlBunsAMostrar(DataTableSemNome.Rows.Count - 1)
            ReDim InterAMostrar(DataTableSemNome.Rows.Count - 1)
            ReDim DuracaoAMostrar(DataTableSemNome.Rows.Count - 1)
        End If
        ListView1.Items.Clear()
        For i As Integer = 0 To DataTableSemNome.Rows.Count - 1

            NomeMusica = DataTableSemNome.Rows(i).Item("NomeMusica")
            p = i + 1
            Dim ls As New ListViewItem(p & Space(5) & NomeMusica, 0)
            If DataTableSemNome.Rows.Count = 1 Then
                ListView1.Items.Add(ls)
            ElseIf DataTableSemNome.Rows.Count > 1 Then
                ListView1.Items.Add(ls)
            End If

            Duracao = DataTableSemNome.Rows(i).Item("Duracao")
            duracaoT = duracaoTotal(Duracao)
            ls.SubItems.Add(Duracao)

            Dim ninter As String = DataTableSemNome.Rows(i).Item("NomeInterprete")

            If DataTableSemNome.Rows(i).Item("NomeInterprete") <> Nothing Then
                ls.SubItems.Add(DataTableSemNome.Rows(i).Item("NomeInterprete"))
            Else
                ls.SubItems.Add("")
            End If


            If DataTableSemNome.Rows(i).Item("NomeAlbum") <> Nothing Then
                ls.SubItems.Add(DataTableSemNome.Rows(i).Item("NomeAlbum"))
            Else
                ls.SubItems.Add("")
            End If


            If DataTableSemNome.Rows(i).Item("Ano") > 0 Then
                ls.SubItems.Add(DataTableSemNome.Rows(i).Item("Ano"))
            Else
                ls.SubItems.Add("")
            End If


            nGenero = DataTableSemNome.Rows(i).Item("NomeGenero")

            If nGenero <> Nothing Then
                ls.SubItems.Add(nGenero)
            Else
                ls.SubItems.Add("")
            End If



            If DataTableSemNome.Rows(i).Item("DataAdicao") <> Nothing Then
                ls.SubItems.Add(DataTableSemNome.Rows(i).Item("DataAdicao"))
            Else
                ls.SubItems.Add("")
            End If
            Try
                If DataTableSemNome.Rows(i).Item("NumReproducao") <> Nothing Then
                    ls.SubItems.Add(DataTableSemNome.Rows(i).Item("NumReproducao"))
                    ReiniciarContagemReprodToolStripMenuItem2.Enabled = True
                Else
                    ls.SubItems.Add("")
                    ReiniciarContagemReprodToolStripMenuItem2.Enabled = False
                End If
            Catch ex As Exception
                ls.SubItems.Add("")
            End Try


            Try
                If DataTableSemNome.Rows(i).Item("UltimaReproducao") <> Nothing Then
                    ls.SubItems.Add(DataTableSemNome.Rows(i).Item("UltimaReproducao"))
                Else
                    ls.SubItems.Add("")

                End If
            Catch ex As Exception
                ls.SubItems.Add("")
            End Try


            Try

                ls.SubItems.Add(DataTableSemNome.Rows(i).Item("Classificacao"))
            Catch ex As Exception
                ls.SubItems.Add("")
            End Try


            ls.SubItems.Add(DataTableSemNome.Rows(i).Item("NumMusica"))

            If verificaReproduzir = False Then
                MusicasATocar(i) = DataTableSemNome.Rows(i).Item("NumMusica")
                NomesAMostrar(i) = DataTableSemNome.Rows(i).Item("NomeMusica")
                AlBunsAMostrar(i) = DataTableSemNome.Rows(i).Item("NomeAlbum")
                InterAMostrar(i) = DataTableSemNome.Rows(i).Item("NomeInterprete")
                DuracaoAMostrar(i) = DataTableSemNome.Rows(i).Item("Duracao")
            End If

            nt = DataTableSemNome.Rows(i).Item("Tamanho")

            For j As Integer = 0 To nt.Length - 1
                If nt.Substring(j, 2) = "KB" Then
                    KB += Mid(nt, 1, nt.Length - 3)
                    Exit For
                ElseIf nt.Substring(j, 2) = "MB" Then
                    MB += Mid(nt, 1, nt.Length - 3)
                    Exit For
                End If
            Next


            If KB > 1024 Then

                MB += KB
            End If
            num = MB

        Next
        If NumPlaylist > 6 Then
            PictureBoxListaPessoal.Visible = True
            BtnEmGre.Enabled = False
            BtnEmList.Enabled = False
        End If
        ColorirListview()

        semnome1()

        Dim semnome As String
        If ListView1.Items.Count = 1 Then
            semnome = "música"
        Else
            semnome = "músicas"
        End If

        If MB = Nothing Then 'KB
            LabelTudo.Text = ListView1.Items.Count & " " & semnome & "," & " " & duracaoT & " " & KB & " " & "KB"
        Else
            num = Math.Round(num, 2).ToString("F2")
            If num < 1024 Then 'MB
                LabelTudo.Text = ListView1.Items.Count & " " & semnome & "," & " " & duracaoT & " " & num & " " & "MB"
            Else
                If num > 1024 Then ' converte para GB
                    num = Math.Round((num / (1024)), 2).ToString("F2")
                    LabelTudo.Text = ListView1.Items.Count & " " & semnome & "," & " " & duracaoT & " " & num & " " & "GB"
                    If num > 1024 Then ' converte para TB
                        num = Math.Round((num / (1024)), 2).ToString("F2")
                        LabelTudo.Text = ListView1.Items.Count & " " & semnome & "," & " " & duracaoT & " " & num & " " & "TB"
                    End If
                End If
            End If

        End If

        If ListView1.Items.Count >= 1 Then
            If verificaReproduzir = False Then
                AnteriorToolStripMenuItem.Enabled = False
                AnteriorToolStripMenuItem1.Enabled = False
                SeguinteToolStripMenuItem.Enabled = False
                SeguinteToolStripMenuItem1.Enabled = False
                ReproduzirToolStripMenuItem.Enabled = True
                ReproduzirToolStripMenuItem1.Enabled = True
                BtnPlay.Image = iPlayer.My.Resources.playerpp
            Else
                AnteriorToolStripMenuItem.Enabled = True
                AnteriorToolStripMenuItem1.Enabled = True
                SeguinteToolStripMenuItem.Enabled = True
                SeguinteToolStripMenuItem1.Enabled = True
                ReproduzirToolStripMenuItem.Enabled = True
                ReproduzirToolStripMenuItem1.Enabled = True
            End If
            TextBoxPesq.Enabled = True
            CopiarToolStripMenuItem.Enabled = False
            CortarToolStripMenuItem1.Enabled = False
            SeleccionarTudoToolStripMenuItem1.Enabled = True
            AnularSelecçãoToolStripMenuItem1.Enabled = False
            ColarToolStripMenuItem2.Enabled = False
            PictureBoxListaPessoal.Visible = False
            TextBoxPesq.Enabled = True
            BtnEmGre.Enabled = True
            BtnEmList.Enabled = True
            Panel4.Enabled = True
            BtnCancelPesq.Enabled = True
            BtnPesq.Enabled = True
            tagpage = True
            EliminarToolStripMenuItem1.Enabled = False
            EmGrelhaToolStripMenuItem.Enabled = True
            EmListaToolStripMenuItem.Enabled = True
            LabelTudo.Visible = True
            If PlaylistActual > 6 Then
                If VerificaCopiar = True Then
                    ColarToolStripMenuItem2.Enabled = True
                End If
            End If
        ElseIf ListView1.Items.Count = 0 Then
            PictureBoxListaPessoal.Visible = False
            TextBoxPesq.Enabled = False
            If PlaylistActual > 6 Then
                PictureBoxListaPessoal.Visible = True
                BtnEmGre.Enabled = False
                BtnEmList.Enabled = False
                If VerificaCopiar = True Then
                    ColarToolStripMenuItem2.Enabled = True
                End If
                EmGrelhaToolStripMenuItem.Enabled = False
                EmListaToolStripMenuItem.Enabled = False
            Else
                BtnEmGre.Enabled = True
                BtnEmList.Enabled = True
                EmGrelhaToolStripMenuItem.Enabled = True
                EmListaToolStripMenuItem.Enabled = True
                ColarToolStripMenuItem2.Enabled = False
            End If
            If verificaReproduzir = False Then
                AnteriorToolStripMenuItem.Enabled = False
                AnteriorToolStripMenuItem1.Enabled = False
                SeguinteToolStripMenuItem.Enabled = False
                SeguinteToolStripMenuItem1.Enabled = False
                ReproduzirToolStripMenuItem.Enabled = False
                ReproduzirToolStripMenuItem1.Enabled = False
                BtnPlay.Image = iPlayer.My.Resources.playerpp_enabled
            Else
                AnteriorToolStripMenuItem.Enabled = True
                AnteriorToolStripMenuItem1.Enabled = True
                SeguinteToolStripMenuItem.Enabled = True
                SeguinteToolStripMenuItem1.Enabled = True
                ReproduzirToolStripMenuItem.Enabled = True
                ReproduzirToolStripMenuItem1.Enabled = True
            End If
            CopiarToolStripMenuItem.Enabled = False
            CortarToolStripMenuItem1.Enabled = False
            SeleccionarTudoToolStripMenuItem1.Enabled = False
            AnularSelecçãoToolStripMenuItem1.Enabled = False
            LabelTudo.Visible = False
            EliminarToolStripMenuItem1.Enabled = False
        End If
        If verificaReproduzir = True Then
            If PlaylistActual = naux Then
                ListView1.Items(indexAReprod).BackColor = Color.LightSkyBlue
            End If
        End If

        Anos = Nothing
        Meses = Nothing
        Semanas = Nothing
        horas = Nothing
        Seg = Nothing
        minutos = Nothing
        dias = Nothing
        num = Nothing
        KB = Nothing
        MB = Nothing
    End Sub

    Sub semnome1()
        If verificaV = False Then
            ImageList1.Images.Clear()
            ImageList1.Images.Add(iPlayer.My.Resources.nopCapa)
            ListView1.View = View.LargeIcon
            For i As Integer = 0 To ListView1.Items.Count - 1
                NumMusica = ListView1.Items(i).SubItems(10).Text
                DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusica)
                Try
                    ImageList1.Images.Add(Image.FromFile(DataTableSemNome.Rows(0).Item("Capa")))
                    ListView1.Items(i).ImageIndex = ImageList1.Images.Count - 1
                Catch ex As Exception
                    ListView1.Items(i).ImageIndex = 0
                End Try

            Next
        End If
    End Sub

    Private Sub TextBoxPesq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxPesq.TextChanged
        If verificaPesquisa = True Then
            Dim Por As String
            If TextBoxPesq.Text = "" Or TextBoxPesq.Text = "Pesquisar" Then
                BtnCancelPesq.Visible = False
                PreencherPlaylist(PlaylistActual)
                Exit Sub
            ElseIf TextBoxPesq.Text <> "" Then
                BtnCancelPesq.Visible = True
            End If

            If verificaPesquisa = True Then
                Me.Refresh()
                Select Case opc
                    Case 1
                        'por todos (inclue o género)

                        DataTableSemNome = iPlayer_DAL.Musica.ProcurarTodos(TextBoxPesq.Text, PlaylistActual)
                    Case 2
                        'int
                        Por = "NomeInterprete"
                        DataTableSemNome = iPlayer_DAL.Musica.Procurar(TextBoxPesq.Text, Por, PlaylistActual)
                    Case 3
                        'mus
                        Por = "NomeMusica"
                        DataTableSemNome = iPlayer_DAL.Musica.Procurar(TextBoxPesq.Text, Por, PlaylistActual)
                    Case 4
                        'album
                        Por = "NomeAlbum"
                        DataTableSemNome = iPlayer_DAL.Musica.Procurar(TextBoxPesq.Text, Por, PlaylistActual)
                End Select
                PreencherComBaseDaPesquisa()
            End If
        End If
    End Sub

    Sub PreencherComBaseDaPesquisa()

        ListView1.Items.Clear()
        LabelTudo.Visible = True
        Dim Duracao As String

        Try

            For i As Integer = 0 To DataTableSemNome.Rows.Count - 1

                NomeMusica = DataTableSemNome.Rows(i).Item("NomeMusica")
                p = i + 1
                Dim ls As New ListViewItem(p & Space(5) & NomeMusica, 0)
                ListView1.Items.Add(ls)

                Duracao = DataTableSemNome.Rows(i).Item("Duracao")
                duracaoT = duracaoTotal(Duracao)
                ls.SubItems.Add(Duracao)

                Dim ninter As String = DataTableSemNome.Rows(i).Item("NomeInterprete")

                If DataTableSemNome.Rows(i).Item("NomeInterprete") <> Nothing Then
                    ls.SubItems.Add(DataTableSemNome.Rows(i).Item("NomeInterprete"))
                Else
                    ls.SubItems.Add("")
                End If

                If DataTableSemNome.Rows(i).Item("NomeAlbum") <> Nothing Then
                    ls.SubItems.Add(DataTableSemNome.Rows(i).Item("NomeAlbum"))
                Else
                    ls.SubItems.Add("")
                End If


                If DataTableSemNome.Rows(i).Item("Ano") > 0 Then
                    ls.SubItems.Add(DataTableSemNome.Rows(i).Item("Ano"))
                Else
                    ls.SubItems.Add("")
                End If


                nGenero = DataTableSemNome.Rows(i).Item("NomeGenero")

                If nGenero <> Nothing Then
                    ls.SubItems.Add(nGenero)
                Else
                    ls.SubItems.Add("")
                End If


                If DataTableSemNome.Rows(i).Item("DataAdicao") <> Nothing Then
                    ls.SubItems.Add(DataTableSemNome.Rows(i).Item("DataAdicao"))
                Else
                    ls.SubItems.Add("")
                End If
                Try
                    If DataTableSemNome.Rows(i).Item("NumReproducao") <> Nothing Then
                        ls.SubItems.Add(DataTableSemNome.Rows(i).Item("NumReproducao"))
                        ReiniciarContagemReprodToolStripMenuItem2.Enabled = True
                    Else
                        ls.SubItems.Add("")

                    End If
                Catch ex As Exception
                    ls.SubItems.Add("")
                End Try


                Try
                    If DataTableSemNome.Rows(i).Item("UltimaReproducao") <> Nothing Then
                        ls.SubItems.Add(DataTableSemNome.Rows(i).Item("UltimaReproducao"))
                    Else
                        ls.SubItems.Add("")

                    End If
                Catch ex As Exception
                    ls.SubItems.Add("")
                End Try


                Try

                    ls.SubItems.Add(DataTableSemNome.Rows(i).Item("Classificacao"))
                    ReiniciarContagemReprodToolStripMenuItem2.Enabled = True
                Catch ex As Exception
                    ls.SubItems.Add("")
                End Try


                ls.SubItems.Add(DataTableSemNome.Rows(i).Item("NumMusica"))

                nt = DataTableSemNome.Rows(i).Item("Tamanho")

                For j As Integer = 0 To nt.Length - 1
                    If nt.Substring(j, 2) = "KB" Then
                        KB += Mid(nt, 1, nt.Length - 3)
                        Exit For
                    ElseIf nt.Substring(j, 2) = "MB" Then
                        MB += Mid(nt, 1, nt.Length - 3)
                        Exit For
                    End If
                Next


                If KB > 1024 Then

                    MB += KB
                End If
                num = MB
            Next
            ColorirListview()

            semnome1()


            Dim semnome As String
            If ListView1.Items.Count = 1 Then
                semnome = "música"
            Else
                semnome = "músicas"
            End If

            If MB = Nothing Then 'KB
                LabelTudo.Text = ListView1.Items.Count & " " & semnome & "," & " " & duracaoT & " " & KB & " " & "KB"
            Else
                num = Math.Round(num, 2).ToString("F2")
                If num < 1024 Then 'MB
                    LabelTudo.Text = ListView1.Items.Count & " " & semnome & "," & " " & duracaoT & " " & num & " " & "MB"
                Else
                    If num > 1024 Then ' converte para GB
                        num = Math.Round((num / (1024)), 2).ToString("F2")
                        LabelTudo.Text = ListView1.Items.Count & " " & semnome & "," & " " & duracaoT & " " & num & " " & "GB"
                        If num > 1024 Then ' converte para TB
                            num = Math.Round((num / (1024)), 2).ToString("F2")
                            LabelTudo.Text = ListView1.Items.Count & " " & semnome & "," & " " & duracaoT & " " & num & " " & "TB"
                        End If
                    End If
                End If

            End If


            Anos = Nothing
            Meses = Nothing
            Semanas = Nothing
            horas = Nothing
            Seg = Nothing
            minutos = Nothing
            dias = Nothing
            num = Nothing
            KB = Nothing
            MB = Nothing
        Catch ex As Exception
            LabelTudo.Visible = False
            Anos = Nothing
            Meses = Nothing
            Semanas = Nothing
            horas = Nothing
            Seg = Nothing
            minutos = Nothing
            dias = Nothing
            num = Nothing
            KB = Nothing
            MB = Nothing
        End Try

    End Sub

    Sub PreencherCDAudio()

        Dim playlist As WMPLib.IWMPPlaylist = AxWindowsMediaPlayer1.cdromCollection.Item(0).Playlist
        Dim duracao As String
        Dim Minutos As Integer

        ListView1.Clear()
        ListView1.Columns.Add("Nome")
        ListView1.Columns.Add("Duração")
        ListView1.Columns.Add("Intérprete")
        ListView1.Columns.Add("Álbum")
        ListView1.Columns.Add("Ano")
        ListView1.Columns.Add("Género")
        ListView1.Columns.Add("Url")
        ListView1.Columns(0).Width = 150
        ListView1.Columns(2).Width = 60
        ListView1.Columns(3).Width = 150
        ListView1.Columns(4).Width = 150
        ListView1.Columns(5).Width = 150
        ListView1.Columns(6).Width = 0
        If verificaReproduzir = False Then
            ReDim MusicasATocar(playlist.count - 1)
            ReDim NomesAMostrar(playlist.count - 1)
            ReDim AlBunsAMostrar(playlist.count - 1)
            ReDim InterAMostrar(playlist.count - 1)
            ReDim DuracaoAMostrar(playlist.count - 1)
        End If


        For i As Integer = 0 To playlist.count - 1
            p = i + 1
            Dim ls As New ListViewItem(p & Space(5) & playlist.Item(i).getItemInfo("Title"))
            ListView1.Items.Add(ls)
            duracao = playlist.Item(i).durationString
            Min = CInt(duracao.Substring(0, 2))
            Minutos += Min
            ls.SubItems.Add(playlist.Item(i).durationString)
            ls.SubItems.Add(playlist.Item(i).getItemInfo("Artist"))
            ls.SubItems.Add(playlist.Item(i).getItemInfo("Album"))
            ls.SubItems.Add(playlist.Item(i).getItemInfo("Year"))
            ls.SubItems.Add(playlist.Item(i).getItemInfo("Genre"))
            ls.SubItems.Add(playlist.Item(i).sourceURL)
            If verificaReproduzir = False Then
                MusicasATocar(i) = playlist.Item(i).sourceURL
                NomesAMostrar(i) = playlist.Item(i).getItemInfo("Title")
                AlBunsAMostrar(i) = playlist.Item(i).getItemInfo("Album")
                InterAMostrar(i) = playlist.Item(i).getItemInfo("Artist")
                DuracaoAMostrar(i) = playlist.Item(i).durationString
            End If
            BtnAudioCD.Text = playlist.Item(i).getItemInfo("Album")
        Next

        ColorirListview()

        If ListView1.Items.Count = 1 Then
            LabelTudo.Text = ListView1.Items.Count & " " & "música," & " " & Minutos & " " & "minutos"
        ElseIf ListView1.Items.Count > 1 Then
            LabelTudo.Text = ListView1.Items.Count & " " & "músicas," & " " & Minutos & " " & "minutos"
        Else
            LabelTudo.Visible = False
        End If
        TextBoxPesq.Enabled = False
        verificaVista()
        LabelTudo.Visible = True
        BtnEmGre.Enabled = False
        BtnEmList.Enabled = False
        EmGrelhaToolStripMenuItem.Enabled = False
        EmListaToolStripMenuItem.Enabled = False
        PictureBoxListaPessoal.Visible = False
        If PlaylistActual = naux Then
            If verificaReproduzir = True Then
                ListView1.Items(indexAReprod).BackColor = Color.LightSkyBlue
            End If
        End If
        DataTableSemNome = iPlayer_DAL.Playlist.MostrarUsandoNumPlaylist(1)
    End Sub

    Private Sub NovaListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NovaListToolStripMenuItem.Click
        NovaPlaylist()
    End Sub

    'Obtem o nome de login do usuário
    Public Function GetUserName() As String
        'verfica se o aplicativo usa autenticação Windows ou personalizada  e então usa essas informações para analisar a propriedade My.User.Name
        If TypeOf My.User.CurrentPrincipal Is  _
        Security.Principal.WindowsPrincipal Then
            'indica que o formato de nome é domain\username
            'split junta à string a "\"
            Dim parts() As String = Split(My.User.Name, "\")
            Dim username As String = parts(1)
            Return username
        Else
            Return My.User.Name
        End If
    End Function

    Private Sub SairToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SairToolStripMenuItem.Click
        TimerOpacity.Start()
    End Sub

    Sub AddFolder(ByVal caminho As String)
        Dim Musicas As String() = IO.Directory.GetFiles(caminho)
        Dim n As Integer = UBound(Musicas)
        Dim nurl As FileInfo
        Dim FullFileNames(n) As String

        For j As Integer = 0 To Musicas.Length - 1
            nurl = New FileInfo(Musicas(j))
            Dim EXT As String = LCase(nurl.Extension)
            If EXT = ".mp3" OrElse EXT = ".aac" OrElse EXT = ".wma" OrElse EXT = ".m4a" Then
                FullFileNames(j) = nurl.FullName
            End If
        Next
        Dim l, countAux As Integer
        For i As Integer = 0 To FullFileNames.Length - 1
            If FullFileNames(i) <> Nothing Then '2

                Dim musicaExistente As Boolean = False
                Try
                    For k As Integer = 0 To DataTableSemNome.Rows.Count - 1
                        If FullFileNames(i) = DataTableSemNome.Rows(k).Item("Url") Then
                            musicaExistente = True
                        End If
                    Next
                Catch ex As Exception
                End Try

                For p As Integer = 0 To FullFileNames.Length - 1
                    If FullFileNames(p) = Nothing Then
                        ReDim arrayAux(p)
                        For h As Integer = 0 To p - 1
                            arrayAux(h) = FullFileNames(h)
                        Next
                        Exit For
                    End If
                Next
                If musicaExistente = True Then '3
                    Dim resposta As DialogResult = System.Windows.Forms.MessageBox.Show("A música " & "'" & Path.GetFileNameWithoutExtension(FullFileNames(i)) & "'" & " já existe na Biblioteca. Deseja adicioná-la novamente?", "iPlayer", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If resposta = DialogResult.Yes Then '4
                        l += 1
                        Try
                            AddFile(arrayAux(i))
                            countAux = arrayAux.Length
                        Catch ex As Exception
                            AddFile(FullFileNames(i))
                            countAux = FullFileNames.Length
                        End Try


                        If nDuracao = "00:00" Then
                        Else
                            verificAddFile = True
                            Try
                                nurl = New FileInfo(arrayAux(i))
                            Catch ex As Exception
                                nurl = New FileInfo(FullFileNames(i))
                            End Try

                            aCarregar.Label2.Text = "A processar" & " " & l & " " & "de" & " " & countAux & ": " & Trim(nurl.Name)
                            aCarregar.Show()
                            Application.DoEvents()
                        End If
                    End If '4
                Else
                    l += 1

                    Try
                        AddFile(arrayAux(i))
                        countAux = arrayAux.Length
                    Catch ex As Exception
                        AddFile(FullFileNames(i))
                        countAux = FullFileNames.Length
                    End Try

                    If nDuracao = "00:00" Then
                    Else
                        verificAddFile = True
                        Try
                            nurl = New FileInfo(arrayAux(i))
                        Catch ex As Exception
                            nurl = New FileInfo(FullFileNames(i))
                        End Try

                        aCarregar.Label2.Text = "A processar" & " " & l & " " & "de" & " " & countAux & ": " & Trim(nurl.Name)
                        aCarregar.Show()
                        Application.DoEvents()
                    End If
                End If '3
            End If '2
            nInterprete = ""
            nAlbum = ""
            NomeMusica = ""
            Comentarios = ""
            nAno = Nothing
            nGenero = ""
            TaxaBits = Nothing
            nDuracao = Nothing
            Frequencia = ""
            Canal = ""
            nCopyright = ""
            nTamanho = Nothing
            Url = ""
            NomeFormato = ""

        Next
        aCarregar.Close()
    End Sub

    Private Sub AddFolderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddFolderToolStripMenuItem.Click
        If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then '1
            AddFolder(FolderBrowserDialog1.SelectedPath)
            If PlaylistActual = 1 Then
                PreencherPlaylist(PlaylistActual)
            End If
            verificAddFile = True
        End If '1
    End Sub

    Private Sub BtnShuffle_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BtnShuffle.MouseDown
        If verificaShuffle = False Then
            BtnShuffle.Image = iPlayer.My.Resources.Resources.Gnome_Media_Playlist_Shuffle_32_press
            LigarSessãoAleatóriaToolStripMenuItem1.Text = "Desligar sessão aleatória"
            verificaShuffle = True
        ElseIf verificaShuffle = True Then
            BtnShuffle.Image = iPlayer.My.Resources.Resources.Gnome_Media_Playlist_Shuffle_32
            LigarSessãoAleatóriaToolStripMenuItem1.Text = "Ligar sessão aleatória"
            verificaShuffle = False
        End If
    End Sub

    Private Sub BtnRepeat_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BtnRepeat.MouseDown

        If verificaRepeatOne = False Then
            If verificaRepeatAll = False Then
                BtnRepeat.Image = iPlayer.My.Resources.Resources.Gnome_Media_Playlist_Repeat_32_press
                RepetirDesligadoToolStripMenuItem1.Checked = False
                RepetirUmaToolStripMenuItem1.Checked = False
                RepetirTudoToolStripMenuItem1.Checked = True
                verificaRepeatAll = True
                verificaRepeatOne = False
            ElseIf verificaRepeatAll = True Then
                BtnRepeat.Image = iPlayer.My.Resources.Resources.Gnome_Media_Playlist_Repeat_one
                RepetirDesligadoToolStripMenuItem1.Checked = False
                RepetirUmaToolStripMenuItem1.Checked = True
                RepetirTudoToolStripMenuItem1.Checked = False
                verificaRepeatAll = False
                verificaRepeatOne = True
            End If

        Else

            BtnRepeat.Image = iPlayer.My.Resources.Resources.Gnome_Media_Playlist_Repeat_32
            RepetirDesligadoToolStripMenuItem1.Checked = True
            RepetirTudoToolStripMenuItem1.Checked = False
            RepetirUmaToolStripMenuItem1.Checked = False
            verificaRepeatOne = False
            verificaRepeatAll = False
        End If

    End Sub

    Private Sub BtnAddPlayList_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BtnAddPlayList.MouseUp
        BtnAddPlayList.Image = iPlayer.My.Resources.Resources.mail_find_32

    End Sub

    Private Sub BtnAddPlayList_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BtnAddPlayList.MouseDown
        BtnAddPlayList.Image = iPlayer.My.Resources.Resources.Gnome_List_Add_32_press
        NovaPlaylist()
    End Sub

    Sub verificaVista()
        If PlaylistActual <> 2 Then

            If verificaV = True Then
                BtnEmGre.Image = iPlayer.My.Resources.Resources._1_copy
                BtnEmList.Image = iPlayer.My.Resources.Resources._41
                ListView1.View = View.Details
                ListView1.LargeImageList = ImageList1
                EmGrelhaToolStripMenuItem.Checked = False
                EmListaToolStripMenuItem.Checked = True
            Else
                BtnEmList.Image = iPlayer.My.Resources.Resources._110
                BtnEmGre.Image = iPlayer.My.Resources.Resources._410
                ImageList1.Images.Clear()
                ImageList1.Images.Add(iPlayer.My.Resources.nopCapa)
                ListView1.View = View.LargeIcon
                For i As Integer = 0 To ListView1.Items.Count - 1
                    NumMusica = ListView1.Items(i).SubItems(10).Text
                    DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusica)
                    Try
                        ImageList1.Images.Add(Image.FromFile(DataTableSemNome.Rows(0).Item("Capa")))
                        ListView1.Items(i).ImageIndex = ImageList1.Images.Count - 1
                    Catch ex As Exception
                        ListView1.Items(i).ImageIndex = 0
                    End Try

                Next
                ListView1.LargeImageList = ImageList1
                EmGrelhaToolStripMenuItem.Checked = True
                EmListaToolStripMenuItem.Checked = False
            End If
        Else
            BtnEmGre.Image = iPlayer.My.Resources.Resources._1_copy
            BtnEmList.Image = iPlayer.My.Resources.Resources._41
            ListView1.View = View.Details
            ListView1.LargeImageList = ImageList1
            EmGrelhaToolStripMenuItem.Checked = False
            EmListaToolStripMenuItem.Checked = True

        End If
    End Sub

    Sub ColorirListview()
        For p As Integer = 0 To ListView1.Items.Count - 1
            ListView1.Items(p).UseItemStyleForSubItems = True
            ListView1.Items(p).BackColor = Color.White
        Next
        For p As Integer = 0 To ListView1.Items.Count - 1 Step 2
            ListView1.Items(p).UseItemStyleForSubItems = True
            ListView1.Items(p).BackColor = Color.Lavender
        Next
    End Sub

    Private Sub EmGrelhaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmGrelhaToolStripMenuItem.Click
        If verificaV = True Then
            verificaV = False
            verificaVista()
        End If
    End Sub

    Private Sub EmListaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmListaToolStripMenuItem.Click
        If verificaV = False Then
            verificaV = True
            verificaVista()
        End If
    End Sub

    Private Sub BtnEmList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEmList.Click
        If verificaV = False Then
            verificaV = True
            verificaVista()
        End If
    End Sub

    Private Sub BtnEmGre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEmGre.Click
        If verificaV = True Then
            verificaV = False
            verificaVista()
        End If
    End Sub

    Private Sub RepetirDesligadoToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RepetirDesligadoToolStripMenuItem1.Click
        BtnRepeat.Image = iPlayer.My.Resources.Resources.Gnome_Media_Playlist_Repeat_32
        RepetirDesligadoToolStripMenuItem1.Checked = True
        DesligadoToolStripMenuItem.Checked = True
        RepetirUmaToolStripMenuItem1.Checked = False
        UmaToolStripMenuItem.Checked = False
        TudoToolStripMenuItem1.Checked = False
        RepetirTudoToolStripMenuItem1.Checked = False
        verificaRepeatOne = False
        verificaRepeatAll = True
    End Sub

    Private Sub RepetirTudoToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RepetirTudoToolStripMenuItem1.Click
        BtnRepeat.Image = iPlayer.My.Resources.Resources.Gnome_Media_Playlist_Repeat_32_press
        RepetirDesligadoToolStripMenuItem1.Checked = False
        DesligadoToolStripMenuItem.Checked = False
        TudoToolStripMenuItem1.Checked = True
        RepetirTudoToolStripMenuItem1.Checked = True
        UmaToolStripMenuItem.Checked = False
        RepetirUmaToolStripMenuItem1.Checked = False
        verificaRepeatAll = True
        verificaRepeatOne = False
    End Sub

    Private Sub RepetirUmaToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RepetirUmaToolStripMenuItem1.Click
        BtnRepeat.Image = iPlayer.My.Resources.Resources.Gnome_Media_Playlist_Repeat_one
        RepetirDesligadoToolStripMenuItem1.Checked = False
        DesligadoToolStripMenuItem.Checked = False
        UmaToolStripMenuItem.Checked = True
        RepetirUmaToolStripMenuItem1.Checked = True
        TudoToolStripMenuItem1.Checked = False
        RepetirTudoToolStripMenuItem1.Checked = False
        verificaRepeatOne = True
        verificaRepeatAll = False
    End Sub

    Private Sub LigarSessãoAleatóriaToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LigarSessãoAleatóriaToolStripMenuItem1.Click
        If verificaShuffle = False Then
            BtnShuffle.Image = iPlayer.My.Resources.Resources.Gnome_Media_Playlist_Shuffle_32_press
            LigarSessãoAleatóriaToolStripMenuItem1.Text = "Desligar sessão aleatória"
            verificaShuffle = True
        ElseIf verificaShuffle = True Then
            BtnShuffle.Image = iPlayer.My.Resources.Resources.Gnome_Media_Playlist_Shuffle_32
            LigarSessãoAleatóriaToolStripMenuItem1.Text = "Ligar sessão aleatória"
            verificaShuffle = False
        End If
    End Sub

    Private Sub BtnAnterior_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BtnAnterior.MouseDown
        If verificaReproduzir = True Then
            BtnAnterior.Image = iPlayer.My.Resources.Resources.player_rew_press
        End If
    End Sub

    Private Sub BtnAnterior_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BtnAnterior.MouseUp
        ClickBtn = True
        MusicaAnterior()
    End Sub

    Private Sub BtnSeguinte_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BtnSeguinte.MouseDown
        If verificaReproduzir = True Then
            BtnSeguinte.Image = iPlayer.My.Resources.Resources.player_fwd_press
        End If
    End Sub

    Private Sub BtnSeguinte_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BtnSeguinte.MouseUp
        ClickBtn = True
        ProximaMusica()
    End Sub

    Sub ProximaMusica()
        If verificaPesquisa = False Then
            Try
                If verificaReproduzir = True Then
                    BtnSeguinte.Image = iPlayer.My.Resources.player_fwd_32
                    If verificaShuffle = False And verificaRepeatAll = False And verificaRepeatOne = False Then
                        Try
                            If AReprod_CD = False Then
                                indexAReprod += 1
                                NumMusicaAReprod = MusicasATocar(indexAReprod)
                                DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusicaAReprod)
                                caminho = DataTableSemNome.Rows(0).Item("Url")
                                AxWindowsMediaPlayer1.URL = caminho
                                AxWindowsMediaPlayer1.Ctlcontrols.play()
                                Exit Sub
                            Else
                                indexAReprod += 1
                                AxWindowsMediaPlayer1.URL = MusicasATocar(indexAReprod)
                                AxWindowsMediaPlayer1.Ctlcontrols.play()
                                Exit Sub
                            End If
                        Catch ex As Exception
                            AxWindowsMediaPlayer1.Ctlcontrols.stop()
                            parar()
                            Exit Sub
                        End Try
                    End If

                    If verificaRepeatOne = True Then ' independentemente se shuffle e o repetir estiverem ligados ou desligados
                        If ClickBtn = False Then
                            AxWindowsMediaPlayer1.URL = AxWindowsMediaPlayer1.URL
                            AxWindowsMediaPlayer1.Ctlcontrols.play()
                        Else
                            If AReprod_CD = False Then
                                ClickBtn = False
                                indexAReprod += 1
                                NumMusicaAReprod = MusicasATocar(indexAReprod)
                                DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusicaAReprod)
                                caminho = DataTableSemNome.Rows(0).Item("Url")
                                AxWindowsMediaPlayer1.URL = caminho
                                AxWindowsMediaPlayer1.Ctlcontrols.play()
                                Exit Sub
                            Else
                                ClickBtn = False
                                indexAReprod += 1
                                AxWindowsMediaPlayer1.URL = MusicasATocar(indexAReprod)
                                AxWindowsMediaPlayer1.Ctlcontrols.play()
                                Exit Sub
                            End If

                        End If

                    Else
                        '----------------------------------------------------------------------------------
                        If verificaShuffle = True Then
                            If verificaRepeatAll = True Then ' shuffle ligado, all ligado
                                CountAReprod += 1
                                indexAReprod = r.Next(0, NumReproduzidas.Count)
                                For i As Integer = 0 To NumReproduzidas.Count - 1
                                    'verifica se o proximo index ja foi reproduzido
                                    If NumReproduzidas(i) = indexAReprod Then
                                        i = 0
                                        indexAReprod = r.Next(0, NumReproduzidas.Count)
                                    End If

                                    'verifica se todas as musicas ja foram reproduzidas

                                    If NumReproduzidas(i) = -1 Then
                                        verificaPreenchido = False
                                    Else
                                        verificaPreenchido = True
                                    End If
                                Next

                                'como todas as musicas já foram reproduzidas, elimina os seus numeros do array
                                If verificaPreenchido = True Then
                                    For j As Integer = 0 To NumReproduzidas.Count - 1
                                        NumReproduzidas(j) = -1
                                        CountAReprod = 0
                                    Next
                                End If

                                NumReproduzidas(CountAReprod) = indexAReprod
                                IndexSelec = indexAReprod
                                If AReprod_CD = False Then
                                    NumMusicaAReprod = MusicasATocar(indexAReprod)
                                    DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusicaAReprod)
                                    caminho = DataTableSemNome.Rows(0).Item("Url")
                                    AxWindowsMediaPlayer1.URL = caminho
                                Else
                                    AxWindowsMediaPlayer1.URL = MusicasATocar(indexAReprod)
                                End If

                                AxWindowsMediaPlayer1.Ctlcontrols.play()

                                '---------------------------------------------------------------------------
                            Else ' shuffle ligado, all desligado
                                CountAReprod += 1
                                indexAReprod = r.Next(0, NumReproduzidas.Count)
                                Dim e As Integer = 0
                                For i As Integer = 0 To NumReproduzidas.Count - 1
                                    'verifica se o proximo index ja foi reproduzido

                                    If NumReproduzidas(e) = indexAReprod Then
                                        i = 0
                                        e = 0
                                        indexAReprod = r.Next(0, NumReproduzidas.Count)
                                    ElseIf NumReproduzidas(e) = -1 Then
                                        CountAReprod = e
                                        Exit For
                                    Else
                                        e += 1
                                    End If

                                    'verifica se todas as musicas ja foram reproduzidas
                                    If NumReproduzidas(e) = -1 Then
                                        verificaPreenchido = False
                                    Else
                                        verificaPreenchido = True
                                    End If
                                Next

                                'como todas as musicas já foram reproduzidas, elimina os seus numeros do array
                                If verificaPreenchido = True Then
                                    For j As Integer = 0 To NumReproduzidas.Count - 1
                                        For k As Integer = 0 To NumReproduzidas.Count - 1
                                            NumReproduzidas(k) = -1
                                        Next
                                        CountAReprod = 0
                                        AxWindowsMediaPlayer1.Ctlcontrols.stop()
                                        Exit Sub
                                    Next
                                End If
                                NumReproduzidas(CountAReprod) = indexAReprod
                                IndexSelec = indexAReprod
                            End If

                            If AReprod_CD = False Then
                                NumMusicaAReprod = MusicasATocar(indexAReprod)
                                DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusicaAReprod)
                                caminho = DataTableSemNome.Rows(0).Item("Url")
                                AxWindowsMediaPlayer1.URL = caminho
                            Else
                                AxWindowsMediaPlayer1.URL = MusicasATocar(indexAReprod)
                            End If
                            AxWindowsMediaPlayer1.Ctlcontrols.play()
                            '---------------------------------------------------------------------------------
                        Else ' shuffle desligado
                            If verificaRepeatAll = True Then ' all ligado
                                indexAReprod += 1
                                If AReprod_CD = False Then
                                    NumMusicaAReprod = MusicasATocar(indexAReprod)
                                    DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusicaAReprod)
                                    caminho = DataTableSemNome.Rows(0).Item("Url")
                                    AxWindowsMediaPlayer1.URL = caminho
                                Else
                                    AxWindowsMediaPlayer1.URL = MusicasATocar(indexAReprod)
                                End If
                                AxWindowsMediaPlayer1.Ctlcontrols.play()
                                IndexSelec = indexAReprod
                                '---------------------------------------------------------------------------------
                            Else 'shuffle desligado, all desligado
                                AxWindowsMediaPlayer1.Ctlcontrols.stop()
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                If verificaRepeatAll = True Then
                    indexAReprod = 0
                    If AReprod_CD = False Then
                        NumMusicaAReprod = MusicasATocar(indexAReprod)
                        DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusicaAReprod)
                        caminho = DataTableSemNome.Rows(0).Item("Url")
                        AxWindowsMediaPlayer1.URL = caminho
                    Else
                        AxWindowsMediaPlayer1.URL = MusicasATocar(indexAReprod)
                    End If
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    IndexSelec = indexAReprod
                Else
                    AxWindowsMediaPlayer1.Ctlcontrols.stop()
                End If
            End Try
        Else
            AxWindowsMediaPlayer1.Ctlcontrols.stop()
        End If
    End Sub

    Sub MusicaAnterior()
        If verificaPesquisa = False Then
            Try
                If verificaReproduzir = True Then
                    BtnAnterior.Image = iPlayer.My.Resources.player_rew_32
                    If verificaShuffle = False And verificaRepeatAll = False And verificaRepeatOne = False Then
                        Try
                            If AReprod_CD = False Then
                                indexAReprod -= 1
                                NumMusicaAReprod = MusicasATocar(indexAReprod)
                                DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusicaAReprod)
                                caminho = DataTableSemNome.Rows(0).Item("Url")
                                AxWindowsMediaPlayer1.URL = caminho
                                AxWindowsMediaPlayer1.Ctlcontrols.play()
                                Exit Sub
                            Else
                                indexAReprod -= 1
                                AxWindowsMediaPlayer1.URL = MusicasATocar(indexAReprod)
                                AxWindowsMediaPlayer1.Ctlcontrols.play()
                                Exit Sub
                            End If
                        Catch ex As Exception
                            AxWindowsMediaPlayer1.Ctlcontrols.stop()
                            parar()
                            Exit Sub
                        End Try
                    End If

                    If verificaRepeatOne = True Then ' independentemente se shuffle e o repetir estiverem ligados ou desligados
                        If ClickBtn = False Then
                            AxWindowsMediaPlayer1.URL = AxWindowsMediaPlayer1.URL
                            AxWindowsMediaPlayer1.Ctlcontrols.play()
                        Else
                            If AReprod_CD = False Then
                                ClickBtn = False
                                indexAReprod -= 1
                                NumMusicaAReprod = MusicasATocar(indexAReprod)
                                DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusicaAReprod)
                                caminho = DataTableSemNome.Rows(0).Item("Url")
                                AxWindowsMediaPlayer1.URL = caminho
                                AxWindowsMediaPlayer1.Ctlcontrols.play()
                                Exit Sub
                            Else
                                ClickBtn = False
                                indexAReprod -= 1
                                AxWindowsMediaPlayer1.URL = MusicasATocar(indexAReprod)
                                AxWindowsMediaPlayer1.Ctlcontrols.play()
                                Exit Sub
                            End If

                        End If
                    Else
                        '----------------------------------------------------------------------------------
                        If verificaShuffle = True Then
                            If verificaRepeatAll = True Then ' shuffle ligado, all ligado
                                CountAReprod += 1
                                indexAReprod = r.Next(0, NumReproduzidas.Count)
                                For i As Integer = 0 To NumReproduzidas.Count - 1
                                    'verifica se o proximo index ja foi reproduzido
                                    If NumReproduzidas(i) = indexAReprod Then
                                        i = 0
                                        indexAReprod = r.Next(0, NumReproduzidas.Count)
                                    End If

                                    'verifica se todas as musicas ja foram reproduzidas
                                    If NumReproduzidas(i) = -1 Then
                                        verificaPreenchido = False
                                    Else
                                        verificaPreenchido = True
                                    End If
                                Next

                                'como todas as musicas já foram reproduzidas, elimina os seus numeros do array
                                If verificaPreenchido = True Then
                                    For j As Integer = 0 To NumReproduzidas.Count - 1
                                        NumReproduzidas(j) = -1
                                        CountAReprod = 0
                                    Next
                                End If

                                NumReproduzidas(CountAReprod) = indexAReprod
                                IndexSelec = indexAReprod
                                If AReprod_CD = False Then
                                    NumMusicaAReprod = MusicasATocar(indexAReprod)
                                    DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusicaAReprod)
                                    caminho = DataTableSemNome.Rows(0).Item("Url")
                                    AxWindowsMediaPlayer1.URL = caminho
                                Else
                                    AxWindowsMediaPlayer1.URL = MusicasATocar(indexAReprod)
                                End If

                                AxWindowsMediaPlayer1.Ctlcontrols.play()

                                '---------------------------------------------------------------------------
                            Else ' shuffle ligado, all desligado
                                CountAReprod -= 1
                                indexAReprod = NumReproduzidas(CountAReprod)
                                IndexSelec = indexAReprod
                            End If

                            If AReprod_CD = False Then
                                NumMusicaAReprod = MusicasATocar(indexAReprod)
                                DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusicaAReprod)
                                caminho = DataTableSemNome.Rows(0).Item("Url")
                                AxWindowsMediaPlayer1.URL = caminho
                            Else
                                AxWindowsMediaPlayer1.URL = MusicasATocar(indexAReprod)
                            End If
                            AxWindowsMediaPlayer1.Ctlcontrols.play()
                            '---------------------------------------------------------------------------------
                        Else ' shuffle desligado
                            If verificaRepeatAll = True Then ' all ligado
                                indexAReprod -= 1
                                If AReprod_CD = False Then
                                    NumMusicaAReprod = MusicasATocar(indexAReprod)
                                    DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusicaAReprod)
                                    caminho = DataTableSemNome.Rows(0).Item("Url")
                                    AxWindowsMediaPlayer1.URL = caminho
                                Else
                                    AxWindowsMediaPlayer1.URL = MusicasATocar(indexAReprod)
                                End If
                                AxWindowsMediaPlayer1.Ctlcontrols.play()
                                IndexSelec = indexAReprod
                                '---------------------------------------------------------------------------------
                            Else 'shuffle desligado, all desligado
                                AxWindowsMediaPlayer1.Ctlcontrols.stop()
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                If verificaRepeatAll = True Then
                    indexAReprod = NumReproduzidas.Count - 1
                    If AReprod_CD = False Then
                        NumMusicaAReprod = MusicasATocar(indexAReprod)
                        DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusicaAReprod)
                        caminho = DataTableSemNome.Rows(0).Item("Url")
                        AxWindowsMediaPlayer1.URL = caminho
                    Else
                        AxWindowsMediaPlayer1.URL = MusicasATocar(indexAReprod)
                    End If
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    IndexSelec = indexAReprod
                Else
                    AxWindowsMediaPlayer1.Ctlcontrols.stop()
                End If
            End Try
        Else
            AxWindowsMediaPlayer1.Ctlcontrols.stop()
        End If
    End Sub

    Sub verificaTabPage()

        If tagpage Then

            BtnAddPlayList.Enabled = False
            BtnEmList.Image = iPlayer.My.Resources.Resources._110
            BtnEmGre.Image = iPlayer.My.Resources.Resources._1_copy
            TextBoxPesq.Enabled = False
            BtnEmGre.Enabled = False
            BtnEmList.Enabled = False
            Panel4.Enabled = False
            BtnCancelPesq.Enabled = False
            tagpage = False
            SeleccionarTudoToolStripMenuItem1.Enabled = False
            EmGrelhaToolStripMenuItem.Enabled = False
            CopiarToolStripMenuItem.Enabled = False
            EliminarToolStripMenuItem1.Enabled = False
            AnularSelecçãoToolStripMenuItem1.Enabled = False
            EmListaToolStripMenuItem.Enabled = False
            LabelTudo.Visible = False
            NovaListToolStripMenuItem.Enabled = False
            If verificaReproduzir = True Then
                EcrãCompletoToolStripMenuItem.Enabled = True
            Else
                EcrãCompletoToolStripMenuItem.Enabled = False
            End If
            If PictureBoxListaPessoal.Visible = True Then
                PictureBoxListaPessoal.Visible = False
            End If
        Else
            If PlaylistActual <> 2 Then
                If verificaV = True Then
                    BtnEmGre.Image = iPlayer.My.Resources.Resources._1_copy
                    BtnEmList.Image = iPlayer.My.Resources.Resources._41
                Else
                    BtnEmList.Image = iPlayer.My.Resources.Resources._110
                    BtnEmGre.Image = iPlayer.My.Resources.Resources._410
                End If
                BtnAddPlayList.Enabled = True
                EcrãCompletoToolStripMenuItem.Enabled = False
                TextBoxPesq.Enabled = True
                BtnEmGre.Enabled = True
                BtnEmList.Enabled = True

                EmGrelhaToolStripMenuItem.Enabled = True
                EmListaToolStripMenuItem.Enabled = True
            Else
                TextBoxPesq.Enabled = False
                BtnEmGre.Enabled = False
                BtnEmList.Enabled = False
                EcrãCompletoToolStripMenuItem.Enabled = False
                EmListaToolStripMenuItem.Enabled = False
                EmGrelhaToolStripMenuItem.Enabled = False
                CopiarToolStripMenuItem.Enabled = False
                EliminarToolStripMenuItem1.Enabled = False
                BtnEmGre.Image = iPlayer.My.Resources.Resources._1_copy
                BtnEmList.Image = iPlayer.My.Resources.Resources._41
            End If
            Panel4.Enabled = True
            NovaListToolStripMenuItem.Enabled = True
            BtnCancelPesq.Enabled = True
            tagpage = True

            Dim c As Integer
            Dim indexes As ListView.SelectedIndexCollection = ListView1.SelectedIndices

            If indexes.Count = 1 Then
                CopiarToolStripMenuItem.Enabled = True
                EliminarToolStripMenuItem1.Enabled = True
                AnularSelecçãoToolStripMenuItem1.Enabled = True
                ClassifToolStripMenuItem.Enabled = True
                InforToolStripMenuItem.Enabled = True

                For Each c In indexes
                    IndexSelec = c
                Next

                If verificaReproduzir = True Then
                    If naux = PlaylistActual Then
                        ListView1.Items(indexAReprod).BackColor = Color.LightSkyBlue
                        ListView1.Items(IndexSelec).Selected = True
                    End If
                Else
                    If naux = PlaylistActual Then
                        ListView1.Items(IndexSelec).Selected = True
                    End If
                End If
            End If

            If ListView1.Items.Count > 0 Then
                SeleccionarTudoToolStripMenuItem1.Enabled = True
                LabelTudo.Visible = True
            ElseIf ListView1.Items.Count = 0 Then
                SeleccionarTudoToolStripMenuItem1.Enabled = False
                LabelTudo.Visible = False
            End If

            If PlaylistActual > 6 Then
                If ListView1.Items.Count = 0 Then
                    BtnEmGre.Enabled = False
                    BtnEmList.Enabled = False
                    PictureBoxListaPessoal.Visible = True
                    EmListaToolStripMenuItem.Enabled = False
                    EmGrelhaToolStripMenuItem.Enabled = False
                End If
            End If
        End If
        ListView1.Focus()
    End Sub

    Private Sub TabControl1_Selecting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs) Handles TabControl1.Selecting
        verificaTabPage()
    End Sub

    Sub verificaCDAudio()
        If verificaCD = False Then
            Label3.Visible = False
            PanelBtnAudioCd.Visible = False
            BtnAudioCD.Visible = False
            BtnEjectarDisco.Visible = False
        Else
            Label3.Visible = True
            PanelBtnAudioCd.Visible = True
            BtnAudioCD.Visible = True
            BtnEjectarDisco.Visible = True
        End If
    End Sub

    Private Declare Function mciSendString Lib "winmm.dll" Alias "mciSendStringA" (ByVal lpstrCommand As String, ByVal lpstrReturnString As String, ByVal uReturnLength As Long, ByVal hwndCallback As Long) As Long

    Sub EjectarDisco()

        mciSendString("Set CDAudio Door Open Wait", 0&, 0&, 0&)
        If PlaylistActual = 2 Then
            If verificaReproduzir = True Then
                AxWindowsMediaPlayer1.Ctlcontrols.stop()
            End If
            ListView1.Clear()
            ListView1.Columns.Add("Nome")
            ListView1.Columns.Add("Duração")
            ListView1.Columns.Add("Intérprete")
            ListView1.Columns.Add("Álbum")
            ListView1.Columns.Add("Ano")
            ListView1.Columns.Add("Género")
            ListView1.Columns.Add("Data de adição")
            ListView1.Columns.Add("Reproduções")
            ListView1.Columns.Add("Última reprodução")
            ListView1.Columns.Add("Classificação")
            ListView1.Columns.Add("Url")
            coisas2()
            BtnMusicas.BackColor = Color.LightBlue
            PlaylistActual = 1
            PreencherPlaylist(PlaylistActual)
        End If
        BtnAudioCD.Visible = False
        BtnEjectarDisco.Visible = False
        Label3.Visible = False
        PanelBtnAudioCd.Visible = False
        verificaCD = False
        LabelARepr.Text = ""
        BtnAReprod.Visible = False
        PictureBoxCapa.SizeMode = PictureBoxSizeMode.Zoom
        PictureBoxCapa.Image = iPlayer.My.Resources.Resources.audioandradio_hover
        verificaCDAudio()
    End Sub

    Private Sub BtnEjectarDisco_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEjectarDisco.Click
        EjectarDisco()
    End Sub

    Private Sub EjectarDiscoToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EjectarDiscoToolStripMenuItem1.Click
        EjectarDisco()
    End Sub

    Private Sub TextBoxPesq_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxPesq.Leave
        If TextBoxPesq.Text = "" Then
            BtnCancelPesq.Visible = False
            verificaPesquisa = False
            TextBoxPesq.Text = "Pesquisar"
        End If
    End Sub

    Private Sub TextBoxPesq_MouseCaptureChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxPesq.MouseCaptureChanged
        If verificaPesquisa = False Then
            TextBoxPesq.Text = ""
            verificaPesquisa = True
        End If
    End Sub

    Private Sub AcercaDoBluePlayerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AcercaDoBluePlayerToolStripMenuItem.Click
        Acerca.ShowDialog()
    End Sub

    Private Sub TimerInterprete_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerInterprete.Tick
        'conta até 10
        If Not contador1 = 10 Then
            contador1 += 1
            If Len(nInterprete) > 42 Then
                TimerHAlbum.Stop()
                TimerHInterprete.Start()
            End If
        Else

            'verifica se location já chegou a 16
            If Not n1 = 16 Then

                local -= 2
                LabelInter.Location = New Point(-1, local)
                n1 = local
            Else
                ' se chegou location=16 conta até 10

                If Not contador2 = 10 Then
                    contador2 += 1
                    If Len(nAlbum) > 42 Then
                        TimerHInterprete.Stop()
                        TimerHAlbum.Start()
                    End If
                Else
                    'verifica se location ja chegou a 8
                    If Not n2 = 8 Then
                        local -= 2
                        LabelInter.Location = New Point(-1, local)
                        n2 = local
                    Else
                        'se chegou location=8 entao location=42
                        If Not contador3 = 1 Then
                            contador3 += 1
                            LabelInter.Location = New Point(-1, 42)
                            local = 42
                        Else
                            'verifica se location ja chegou a 32
                            If Not n3 = 32 Then

                                local -= 2
                                LabelInter.Location = New Point(-1, local)
                                n3 = local
                            Else
                                'se chegou location=32 entao volta tudo ao normal
                                contador1 = 0
                                n1 = 0
                                contador2 = 0
                                n2 = 0
                                contador3 = 0
                                n3 = 0
                            End If

                        End If

                    End If
                End If
            End If

        End If
    End Sub

    Private Sub EliminarToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EliminarToolStripMenuItem2.Click
        AlterarNomePlaylist.ShowDialog()
        btn.Text = AlterarNomePlaylist.nomeAlterado
    End Sub

    Private Sub TimerOpacityLoad_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerOpacityLoad.Tick
        If Me.Opacity < 1 Then
            Me.Opacity += 0.1
        Else
            TimerOpacityLoad.Stop()
        End If
    End Sub

    Private Sub TimerOpacity_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerOpacity.Tick
        Me.Opacity = Me.Opacity - 0.1
        If Me.Opacity <= 0.0 Then
            TimerOpacity.Stop()
            DAL.CloseConnection()
            Me.Close()
        End If
    End Sub

    Private Sub MostrarIPlayerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MostrarIPlayerToolStripMenuItem.Click
        Me.WindowState = 0
    End Sub

    Private Sub ContextMenuStripNotifyIcon_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStripNotifyIcon.Opening
        If Me.WindowState = 0 Then
            MostrarIPlayerToolStripMenuItem.Enabled = False
        ElseIf Me.WindowState = 1 Then
            MostrarIPlayerToolStripMenuItem.Enabled = True
        End If
    End Sub

    Private Sub ReproduzirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReproduzirToolStripMenuItem.Click
        If RadioOn = False Then
            Reproduzir2()
        Else
            PararRadio()
        End If
    End Sub

    Private Sub JanelaPrincipal_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If b = False Then
            EventWatcher.Stop()
            TimerOpacity.Start()
            e.Cancel = True
            b = True
        End If
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        If RadioOn = True Then
            System.Windows.Forms.MessageBox.Show("Tem de parar primeiro a emissão!", "iPlayer", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            ListView1.Focus()
            Dim index2 As Integer
            Dim indexes As ListView.SelectedIndexCollection = ListView1.SelectedIndices
            ReDim NumReproduzidas(ListView1.Items.Count - 1)
            For i As Integer = 0 To NumReproduzidas.Count - 1
                NumReproduzidas(i) = -1
            Next
            For Each index2 In indexes

                If PlaylistActual <> 2 Then
                    IndexSelec = index2
                    NumMusicaAReprod = ListView1.Items(IndexSelec).SubItems(10).Text
                    DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusicaAReprod)
                    caminho = DataTableSemNome.Rows(0).Item("Url")
                    AxWindowsMediaPlayer1.URL = caminho
                    AReprod_CD = False
                Else
                    IndexSelec = index2
                    AxWindowsMediaPlayer1.URL = ListView1.Items(IndexSelec).SubItems(6).Text
                    AReprod_CD = True
                End If

                If PlaylistActual <> naux Then
                    '----------------------------------------------------
                    ReDim MusicasATocar(ListView1.Items.Count - 1)
                    ReDim NomesAMostrar(ListView1.Items.Count - 1)
                    ReDim AlBunsAMostrar(ListView1.Items.Count - 1)
                    ReDim InterAMostrar(ListView1.Items.Count - 1)
                    ReDim DuracaoAMostrar(ListView1.Items.Count - 1)
                    Dim p As Integer
                    If PlaylistActual <> 2 Then
                        p = 10
                    Else
                        p = 6
                    End If
                    For i As Integer = 0 To ListView1.Items.Count - 1
                        MusicasATocar(i) = ListView1.Items(i).SubItems(p).Text
                        Dim g As String = ListView1.Items(i).SubItems(0).Text
                        NomesAMostrar(i) = g.Substring(2, g.Length - 2)
                        AlBunsAMostrar(i) = ListView1.Items(i).SubItems(3).Text
                        InterAMostrar(i) = ListView1.Items(i).SubItems(2).Text
                        DuracaoAMostrar(i) = ListView1.Items(i).SubItems(1).Text
                    Next

                    '----------------------------------------------------------------
                    naux = PlaylistActual
                End If
                AxWindowsMediaPlayer1.Ctlcontrols.play()
                verificAddFile = False
                ListView1.Items(index2).BackColor = Color.LightSkyBlue
                indexAReprod = index2
                NumReproduzidas(0) = index2
                CountAReprod += 1
                If naux = Nothing Then
                    naux = 1
                End If
            Next
        End If
    End Sub

    Sub coisas2()
        ListView1.Columns(10).Width = 0
        ListView1.Columns(7).Width = 60
        ListView1.Columns(0).Width = 150
        ListView1.Columns(2).Width = 60
        ListView1.Columns(3).Width = 150
        ListView1.Columns(4).Width = 150
        ListView1.Columns(5).Width = 50
        ListView1.Columns(6).Width = 150
    End Sub

    Private Sub BtnMusicas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReproRecent.Click, BtnMusicas.Click, BtnAudioCD.Click, BtnAnos2000a2010.Click, BtnAcresRecent.Click, Btn25MaisRepr.Click
        Dim b As Button = DirectCast(sender, Button)
        For i As Integer = 0 To Panel3.Controls.Count - 1
            Dim bx As Button
            If TypeOf Panel3.Controls(i) Is Button Then
                bx = DirectCast(Panel3.Controls(i), Button)
                bx.BackColor = Color.Silver
                bx.FlatAppearance.MouseDownBackColor = Color.Silver
                bx.FlatAppearance.MouseOverBackColor = Color.Silver
            End If
        Next

        If verificaCD = True Then
            For j As Integer = 0 To PanelBtnAudioCd.Controls.Count - 1
                Dim bx As Button
                If TypeOf Panel3.Controls(j) Is Button Then
                    bx = DirectCast(PanelBtnAudioCd.Controls(j), Button)
                    bx.BackColor = Color.Silver
                    bx.FlatAppearance.MouseDownBackColor = Color.Silver
                    bx.FlatAppearance.MouseOverBackColor = Color.Silver

                End If
            Next
        End If

        btn = b
        b.BackColor = Color.LightBlue
        b.FlatAppearance.MouseDownBackColor = Color.LightBlue
        b.FlatAppearance.MouseOverBackColor = Color.LightBlue
        NomeBtnActual = b.Text
        TagBtnActual = b.Tag
        If b.Tag <> PlaylistActual Then
            If LabelARepr.Text = "Item seleccionado" Then
                PictureBoxCapa.SizeMode = PictureBoxSizeMode.StretchImage
                PictureBoxCapa.Image = iPlayer.My.Resources.nada_seleccionado
            End If

            If b.Tag = 2 Then
                BtnEjectarDisco.BackColor = Color.LightBlue
                BtnEjectarDisco.FlatAppearance.MouseDownBackColor = Color.LightBlue
                BtnEjectarDisco.FlatAppearance.MouseOverBackColor = Color.LightBlue
            End If
            CortarToolStripMenuItem1.Enabled = False
            numTag = b.Tag
            Dim index As Integer = -1
            Dim indexes As ListView.SelectedIndexCollection = ListView1.SelectedIndices

            PlaylistActual = b.Tag
            ClassifToolStripMenuItem.Enabled = False
            InforToolStripMenuItem.Enabled = False
            CopiarToolStripMenuItem.Enabled = False
            SeleccionarTudoToolStripMenuItem1.Enabled = True
            AnularSelecçãoToolStripMenuItem1.Enabled = False


            If b.Tag >= 7 Then '2
                If VerificaCopiar = True Then
                    ColarToolStripMenuItem2.Enabled = True
                End If
            Else '2
                If VerificaCopiar = True Then
                    ColarToolStripMenuItem2.Enabled = False
                End If

            End If '2

            'caso alguma música esteja a ser reproduzida
            If verificaReproduzir = True Then '3
                'guarda a tag do btn que foi clicado primeiro para saber de que playlist é que a musica está a ser reproduzida
                If naux = Nothing Then
                    If verifican = False Then '1
                        naux = 1
                        verifican = True
                    End If '1
                End If

                If verificaPlay = True Then '****
                    If b.Tag <> naux Then '**
                        'caso enquanto uma musica estava na pausa mudou-se noutra playlist entao pára tudo
                        AxWindowsMediaPlayer1.Ctlcontrols.stop()
                        parar()
                    End If '**

                End If

                'verifica se a tag actual corresponde a tag da playlist de onde a musica está a ser reproduzida
                If b.Tag = naux Then '5
                    BtnPlay.Image = iPlayer.My.Resources.Resources.player_pause
                    ReproduzirToolStripMenuItem1.Text = "Pausa"
                    ReproduzirToolStripMenuItem.Text = "Pausa"
                    verificaStop = False
                    verificaPlay = False

                    If LabelARepr.Text = "Item seleccionado" Then
                        Try
                            For Each index In indexes
                                NumMusica = ListView1.Items(index).SubItems(10).Text
                                DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusica)
                            Next
                            PictureBoxCapa.ImageLocation = DataTableSemNome.Rows(0).Item("Capa")
                            PictureBoxCapa.SizeMode = PictureBoxSizeMode.StretchImage
                            ImageList1.Images.Add(Image.FromFile(DataTableSemNome.Rows(0).Item("Capa")))
                        Catch ex As Exception
                            PictureBoxCapa.SizeMode = PictureBoxSizeMode.StretchImage
                            PictureBoxCapa.Image = iPlayer.My.Resources.sem_capa
                        End Try
                    End If
                Else '5
                    BtnPlay.Image = iPlayer.My.Resources.Resources.player_stop_48
                    ReproduzirToolStripMenuItem1.Text = "Parar"
                    ReproduzirToolStripMenuItem.Text = "Parar"
                    verificaStop = True
                    verificaPlay = False
                    If LabelARepr.Text = "Item seleccionado" Then
                        PictureBoxCapa.Image = iPlayer.My.Resources.nada_seleccionado
                    End If
                End If '5
            Else '3
                'caso nenhuma música esteja a ser reproduzida
                'guarda a tag do btn que foi clicado primeiro para saber de que playlist é que a musica poderá a ser reproduzida
                naux = b.Tag
                verifican = True
            End If '3


            If verificaCD = True Then
                If b.Tag <> 2 Then
                    ListView1.Clear()
                    ListView1.Columns.Add("Nome")
                    ListView1.Columns.Add("Duração")
                    ListView1.Columns.Add("Intérprete")
                    ListView1.Columns.Add("Álbum")
                    ListView1.Columns.Add("Ano")
                    ListView1.Columns.Add("Género")
                    ListView1.Columns.Add("Data de adição")
                    ListView1.Columns.Add("Reproduções")
                    ListView1.Columns.Add("Última reprodução")
                    ListView1.Columns.Add("Classificação")
                    ListView1.Columns.Add("Url")
                    coisas2()
                    PreencherPlaylist(b.Tag)
                Else
                    PreencherCDAudio()
                End If
            Else
                PreencherPlaylist(b.Tag)
            End If


            If verificaReproduzir = False Then '10
                If ListView1.Items.Count = 0 Then
                    SeleccionarTudoToolStripMenuItem1.Enabled = False
                    BtnPlay.Image = iPlayer.My.Resources.playerpp_enabled
                    ReproduzirToolStripMenuItem.Enabled = False
                    ReproduzirToolStripMenuItem1.Enabled = False
                Else '10
                    SeleccionarTudoToolStripMenuItem1.Enabled = True
                    BtnPlay.Image = iPlayer.My.Resources.playerpp
                    ReproduzirToolStripMenuItem1.Text = "Reproduzir"
                    ReproduzirToolStripMenuItem.Text = "Reproduzir"
                    ReproduzirToolStripMenuItem.Enabled = True
                    ReproduzirToolStripMenuItem1.Enabled = True
                End If '10
            End If '9
            verificaVista()
        End If

        If RadioOn = True Then
            BtnPlay.Image = iPlayer.My.Resources.player_stop_48
        End If

        If verificaPesquisa = True Then
            verificaPesquisa = False
            TextBoxPesq.Text = "Pesquisar"
            BtnCancelPesq.Visible = False
        End If

        '-------------------
        AdicionarÀListaDeReproduçãoToolStripMenuItem.DropDownItems.Clear()
        Dim Count As Integer = iPlayer_DAL.Playlist.CountFromPlaylist
        For i As Integer = 7 To Count + 1
            If i = b.Tag Then
            Else
                DataTablePlaylist = iPlayer_DAL.Playlist.MostrarUsandoTag2(i)
                If DataTablePlaylist.Rows.Count <> 0 Then
                    Dim TagContext As Integer = DataTablePlaylist.Rows(0).Item("TagContextMenuStrip")
                    Dim NomePlaylist As String = DataTablePlaylist.Rows(0).Item("NomeAux")
                    Dim Tool As New ToolStripMenuItem(NomePlaylist, iPlayer.My.Resources.Resources.music_file_24x24, New EventHandler(AddressOf NenhuToolStripMenuItem2_Click))
                    Tool.Tag = TagContext
                    AdicionarÀListaDeReproduçãoToolStripMenuItem.Enabled = True
                    AdicionarÀListaDeReproduçãoToolStripMenuItem.DropDownItems.Add(Tool)
                End If
            End If
        Next
    End Sub

    Private Sub BtnPlay_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BtnPlay.MouseUp
        If RadioOn = False Then
            Reproduzir2()
        Else
            PararRadio()
        End If
    End Sub

    Private Sub BtnPlay_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BtnPlay.MouseDown
        If RadioOn = False Then
            If naux = 0 Then
                naux = PlaylistActual
            End If
            If PlaylistActual = naux Then
                If verificaPlay = True Then
                    If ListView1.Items.Count <> 0 Then
                        BtnPlay.Image = iPlayer.My.Resources.Resources.playerpp_press
                    End If
                ElseIf verificaPlay = False Then
                    If ListView1.Items.Count <> 0 Then
                        BtnPlay.Image = iPlayer.My.Resources.Resources.player_pause_press
                    End If
                End If
            Else
                If verificaReproduzir = True Then
                    BtnPlay.Image = iPlayer.My.Resources.Resources.player_stop_press
                    verificaStop = True
                End If
            End If
        Else
            BtnPlay.Image = iPlayer.My.Resources.Resources.player_stop_press
        End If
    End Sub

    Private Sub TimerNomeMusica_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerNomeMusica.Tick
        If numLenMusica > 42 Then

            LpalavraMusica = Mid(LpalavraMusica, TamanhoInicioMusica, LTamanhoFinalMusica)
            RletraMusica = Mid(RpalavraMusica, RTamanhoFinalMusica, 1)
            LabelNomeMus.Text = LpalavraMusica

            If TamanhoInicioMusica = 2 Then
                LpalavraMusica = LpalavraMusica & RletraMusica
                LabelNomeMus.Text = LpalavraMusica
                TamanhoInicioMusica = 1
                RTamanhoFinalMusica += 1
            End If
            TamanhoInicioMusica += 1
            numLenMusica -= 1
        Else
            Dim c As Integer = Len(PinicialMusica)
            If RTamanhoFinalMusica = Len(PinicialMusica) + 1 Then
                LpalavraMusica = Mid(LpalavraMusica, TamanhoInicioMusica, LTamanhoFinalMusica)
                LpalavraMusica = LpalavraMusica & Space(5)
                RpalavraMusica = RpalavraMusica
                LabelNomeMus.Text = LpalavraMusica
                RTamanhoFinalMusica += 2
            ElseIf RTamanhoFinalMusica = Len(PinicialMusica) + 3 Then
                RTamanhoFinalMusica = 1
                RletraMusica = Mid(RpalavraMusica, RTamanhoFinalMusica, 1)
                LpalavraMusica = Mid(LpalavraMusica, TamanhoInicioMusica, LTamanhoFinalMusica)
                LpalavraMusica = LpalavraMusica & RletraMusica
                LabelNomeMus.Text = LpalavraMusica
                RTamanhoFinalMusica += 1
            ElseIf RTamanhoFinalMusica = 45 Then
                If Not pMusica = 10 Then
                    pMusica += 1
                End If
                numLenMusica = Len(PinicialMusica)
                LTamanhoFinalMusica = 43
                RTamanhoFinalMusica = 44
                TamanhoInicioMusica = 1
                LpalavraMusica = PinicialMusica
                RpalavraMusica = PinicialMusica
                RletraMusica = ""
                pMusica = 0
            Else
                LpalavraMusica = Mid(LpalavraMusica, TamanhoInicioMusica, LTamanhoFinalMusica)
                RletraMusica = Mid(RpalavraMusica, RTamanhoFinalMusica, 1)
                LpalavraMusica = LpalavraMusica & RletraMusica
                LabelNomeMus.Text = LpalavraMusica
                If TamanhoInicioMusica = 2 Then
                    TamanhoInicioMusica = 1
                    RTamanhoFinalMusica += 1
                End If
                TamanhoInicioMusica += 1
            End If
        End If
    End Sub

    Private Sub TimerHAlbum_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerHAlbum.Tick
        If numLenAlbum > 43 Then
            LpalavraAlbum = Mid(LpalavraAlbum, TamanhoInicioAlbum, LTamanhoFinalAlbum)
            RletraAlbum = Mid(RpalavraAlbum, RTamanhoFinalAlbum, 1)
            LabelInter.Text = LpalavraAlbum

            If TamanhoInicioAlbum = 2 Then
                LpalavraAlbum = LpalavraAlbum & RletraAlbum
                LabelInter.Text = LpalavraAlbum
                TamanhoInicioAlbum = 1
                RTamanhoFinalAlbum += 1
            End If
            TamanhoInicioAlbum += 1
            numLenAlbum -= 1
        Else
            Dim c As Integer = Len(PinicialAlbum)
            If RTamanhoFinalAlbum = Len(PinicialAlbum) + 1 Then
                LpalavraAlbum = Mid(LpalavraAlbum, TamanhoInicioAlbum, LTamanhoFinalAlbum)
                LpalavraAlbum = LpalavraAlbum & Space(5)
                RpalavraAlbum = RpalavraAlbum
                LabelInter.Text = LpalavraAlbum
                RTamanhoFinalAlbum += 2
            ElseIf RTamanhoFinalAlbum = Len(PinicialAlbum) + 3 Then
                RTamanhoFinalAlbum = 1
                RletraAlbum = Mid(RpalavraAlbum, RTamanhoFinalAlbum, 1)
                LpalavraAlbum = Mid(LpalavraAlbum, TamanhoInicioAlbum, LTamanhoFinalAlbum)
                LpalavraAlbum = LpalavraAlbum & RletraAlbum
                LabelInter.Text = LpalavraAlbum
                RTamanhoFinalAlbum += 1
            ElseIf RTamanhoFinalAlbum = 45 Then
                If Not pAlbum = 10 Then
                    pAlbum += 1
                End If
                numLenAlbum = Len(PinicialAlbum)
                LTamanhoFinalAlbum = 43
                RTamanhoFinalAlbum = 44
                TamanhoInicioAlbum = 1
                LpalavraAlbum = PinicialAlbum
                RpalavraAlbum = PinicialAlbum
                RletraAlbum = ""
                pAlbum = 0
            Else
                LpalavraAlbum = Mid(LpalavraAlbum, TamanhoInicioAlbum, LTamanhoFinalAlbum)
                RletraAlbum = Mid(RpalavraAlbum, RTamanhoFinalAlbum, 1)
                LpalavraAlbum = LpalavraAlbum & RletraAlbum
                LabelInter.Text = LpalavraAlbum
                If TamanhoInicioAlbum = 2 Then
                    TamanhoInicioAlbum = 1
                    RTamanhoFinalAlbum += 1
                End If
                TamanhoInicioAlbum += 1
            End If
        End If
    End Sub

    Private Sub TimerHInterprete_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerHInterprete.Tick
        If numLenMusica > 42 Then
            LpalavraInterprete = Mid(LpalavraInterprete, TamanhoInicioInterprete, LTamanhoFinalInterprete)
            RletraInterprete = Mid(RpalavraInterprete, RTamanhoFinalInterprete, 1)
            LabelInter.Text = LpalavraInterprete

            If TamanhoInicioInterprete = 2 Then
                LpalavraInterprete = LpalavraInterprete & RletraInterprete
                LabelInter.Text = LpalavraInterprete
                TamanhoInicioInterprete = 1
                RTamanhoFinalInterprete += 1
            End If
            TamanhoInicioInterprete += 1
            numLenInterprete -= 1
        Else
            Dim c As Integer = Len(PinicialInterprete)
            If RTamanhoFinalInterprete = Len(PinicialInterprete) + 1 Then
                LpalavraInterprete = Mid(LpalavraInterprete, TamanhoInicioInterprete, LTamanhoFinalInterprete)
                LpalavraInterprete = LpalavraInterprete & Space(5)
                RpalavraInterprete = RpalavraInterprete
                LabelInter.Text = LpalavraInterprete
                RTamanhoFinalInterprete += 2
            ElseIf RTamanhoFinalInterprete = Len(PinicialInterprete) + 3 Then
                RTamanhoFinalInterprete = 1
                RletraInterprete = Mid(RpalavraInterprete, RTamanhoFinalInterprete, 1)
                LpalavraInterprete = Mid(LpalavraInterprete, TamanhoInicioInterprete, LTamanhoFinalInterprete)
                LpalavraInterprete = LpalavraInterprete & RletraInterprete
                LabelInter.Text = LpalavraInterprete
                RTamanhoFinalInterprete += 1
            ElseIf RTamanhoFinalInterprete = 45 Then
                If Not pInterprete = 10 Then
                    pInterprete += 1
                End If
                numLenInterprete = Len(PinicialInterprete)
                LTamanhoFinalInterprete = 43
                RTamanhoFinalInterprete = 44
                TamanhoInicioInterprete = 1
                LpalavraInterprete = PinicialInterprete
                RpalavraInterprete = PinicialInterprete
                RletraInterprete = ""
                pInterprete = 0
            Else
                LpalavraInterprete = Mid(LpalavraInterprete, TamanhoInicioInterprete, LTamanhoFinalInterprete)
                RletraInterprete = Mid(RpalavraInterprete, RTamanhoFinalInterprete, 1)
                LpalavraInterprete = LpalavraInterprete & RletraInterprete
                LabelInter.Text = LpalavraInterprete
                If TamanhoInicioInterprete = 2 Then
                    TamanhoInicioInterprete = 1
                    RTamanhoFinalInterprete += 1
                End If
                TamanhoInicioInterprete += 1
            End If
        End If
    End Sub

    Sub Reproduzir()
        TimerNomeMusica.Stop()
        TimerHAlbum.Stop()
        TimerHInterprete.Stop()
        TimerInterprete.Stop()
        NomeMusica = NomesAMostrar(indexAReprod)
        nAlbum = AlBunsAMostrar(indexAReprod)
        nInterprete = InterAMostrar(indexAReprod)
        If Len(NomeMusica) > 43 Then

            numLenMusica = Len(NomeMusica)
            PinicialMusica = NomeMusica
            LpalavraMusica = PinicialMusica
            RpalavraMusica = PinicialMusica
            LTamanhoFinalMusica = 43
            RTamanhoFinalMusica = 44
            TamanhoInicioMusica = 1
            TimerNomeMusica.Start()
            LabelNomeMus.Text = NomeMusica
            LabelNomeMus.Visible = True
        Else
            LabelNomeMus.Text = NomeMusica
            LabelNomeMus.Visible = True
        End If

        If Len(nInterprete) > 43 Then
            numLenInterprete = Len(nInterprete)
            PinicialInterprete = nInterprete
            LpalavraInterprete = PinicialInterprete
            RpalavraInterprete = PinicialInterprete
            LTamanhoFinalInterprete = 43
            RTamanhoFinalInterprete = 44
            TamanhoInicioInterprete = 1
            TimerHInterprete.Start()
            LabelInter.Text = nInterprete
            LabelInter.Visible = True
        Else
            LabelInter.Text = nInterprete
            LabelInter.Visible = True
        End If

        If Len(nAlbum) > 43 Then

            numLenAlbum = Len(nAlbum)
            PinicialAlbum = nAlbum
            LpalavraAlbum = PinicialAlbum
            RpalavraAlbum = PinicialAlbum
            LTamanhoFinalAlbum = 43
            RTamanhoFinalAlbum = 44
            TamanhoInicioAlbum = 1
            TimerHAlbum.Start()
            LabelInter.Text = nAlbum
            LabelInter.Visible = True
        Else
            LabelInter.Text = nAlbum
            LabelInter.Visible = True

        End If


        LabelInter.Visible = True
        If InterAMostrar(indexAReprod) <> "" Then
            LabelInter.Text = InterAMostrar(indexAReprod)
            If AlBunsAMostrar(indexAReprod) <> "" Then
                LabelInter.Text = LabelInter.Text & vbNewLine & AlBunsAMostrar(indexAReprod)
                LabelInter.Location = New Point(-1, 32)
                TimerInterprete.Start()
            Else
                LabelInter.Location = New Point(-1, 26)
                TimerInterprete.Stop()
            End If
        Else
            If AlBunsAMostrar(indexAReprod) <> "" Then
                LabelInter.Text = AlBunsAMostrar(indexAReprod)
                LabelInter.Location = New Point(-1, 26)
            Else
                TimerInterprete.Stop()
                LabelInter.Visible = False
            End If
        End If

        nDuracao = DuracaoAMostrar(indexAReprod)
        PictureBoxPrincipal.Visible = False
        PictureBoxPrincipal2.Visible = True
        AnteriorToolStripMenuItem1.Enabled = True
        SeguinteToolStripMenuItem1.Enabled = True
        AnteriorToolStripMenuItem.Enabled = True
        SeguinteToolStripMenuItem.Enabled = True

        If PlaylistActual <> 2 Then
            If AReprod_CD = False Then
                If verificaPausa = False Then

                    Try
                        DataTableHistorico = iPlayer_DAL.Historico.MostrarUsandoNumMusica(NumMusicaAReprod)
                        NumHistorico = DataTableHistorico.Rows(0).Item("NumHistorico")
                    Catch ex As Exception
                        NumHistorico = Nothing
                    End Try
                    Dim Reproducoes As Integer
                    Try
                        Reproducoes = DataTableHistorico.Rows(0).Item("NumReproducao")
                    Catch ex As Exception
                        Reproducoes = 0
                    End Try


                    If NumHistorico = Nothing Then
                        ' como a tabela historico ainda nao tem registos insere coisas

                        iPlayer_DAL.Historico.Adicionar(Reproducoes + 1, DataActual)

                        NumHistorico = iPlayer_DAL.Historico.UltimoNumPlaylist

                        'insere o numero da musica e da playlist 25 mais reproduzidas na tabela musicaplaylist
                        Dim count As Integer = iPlayer_DAL.Playlist.CountPlaylist25
                        If count < 25 Then
                            iPlayer_DAL.Playlist.CriarRelacao(5, NumMusicaAReprod)
                        End If
                        'insere o numero da musica e da playlist reproduzidas recentemente na tabela musicaplaylist 
                        iPlayer_DAL.Playlist.CriarRelacao(6, NumMusicaAReprod)


                        'cria a relacao com a tabela musica
                        iPlayer_DAL.Historico.CriarRelacao(NumMusicaAReprod, NumHistorico)
                        primeiravez = True
                        ListView1.Items(indexAReprod).SubItems(7).Text = 1
                        ListView1.Items(indexAReprod).SubItems(8).Text = DataActual

                    Else
                        Reproducoes += 1
                        iPlayer_DAL.Historico.Alterar(Reproducoes, DataActual, NumHistorico)
                        If PlaylistActual = naux Then
                            ListView1.Items(indexAReprod).SubItems(7).Text = Reproducoes
                            ListView1.Items(indexAReprod).SubItems(8).Text = DataActual
                        End If
                    End If
                End If
                CapaAReprod()
            End If
        Else
            NopCapaCD()
        End If
        LabelARepr.Text = "A reproduzir"
        LabelARepr.Visible = True
        BtnAReprod.Visible = True

    End Sub

    Sub Reproduzir2()
        verificAddFile = False
        If ListView1.Items.Count > 0 Then
            Dim index2 As Integer
            Dim indexes As ListView.SelectedIndexCollection = ListView1.SelectedIndices
            For Each index2 In indexes
                IndexSelec = index2
                If verificaIndex = False Then
                    indexAReprod = IndexSelec
                    verificaIndex = True
                End If
            Next
            If IndexSelec <> -1 Then

                If verificaStop = True Then
                    AxWindowsMediaPlayer1.Ctlcontrols.stop()
                    parar()
                Else

                    If AxWindowsMediaPlayer1.URL = "" Then
                        If PlaylistActual <> 2 Then
                            NumMusicaAReprod = ListView1.Items(indexAReprod).SubItems(10).Text
                            DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusicaAReprod)
                            caminho = DataTableSemNome.Rows(0).Item("Url")
                            AxWindowsMediaPlayer1.URL = caminho
                        Else
                            AxWindowsMediaPlayer1.URL = ListView1.Items(indexAReprod).SubItems(6).Text
                        End If
                        AxWindowsMediaPlayer1.Ctlcontrols.play()
                        Exit Sub

                    Else
                        If verificaPlay = True Then
                            'toca a musica
                            AxWindowsMediaPlayer1.Ctlcontrols.play()
                            verificaPausa = False
                            Exit Sub
                        Else
                            'aqui pausa a musica
                            verificaPausa = True
                            AxWindowsMediaPlayer1.Ctlcontrols.pause()
                            Exit Sub
                        End If
                    End If
                End If
            Else
                If verificaReproduzir = False Then
                    If PlaylistActual <> 2 Then
                        indexAReprod = 0
                        NumMusicaAReprod = ListView1.Items(indexAReprod).SubItems(10).Text
                        DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusicaAReprod)
                        caminho = DataTableSemNome.Rows(0).Item("Url")
                        AxWindowsMediaPlayer1.URL = caminho
                    Else
                        indexAReprod = 0
                        AxWindowsMediaPlayer1.URL = ListView1.Items(indexAReprod).SubItems(6).Text
                        AReprod_CD = True
                    End If
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                Else
                    If verificaPlay = True Then
                        AxWindowsMediaPlayer1.Ctlcontrols.play()
                        verificaPausa = False
                    Else
                        verificaPausa = True
                        AxWindowsMediaPlayer1.Ctlcontrols.pause()
                    End If

                End If
            End If
        End If
        If verificaReproduzir = True Then
            If verificaStop = True Then
                AxWindowsMediaPlayer1.Ctlcontrols.stop()
                parar()
            End If
        End If

    End Sub

    Private Sub BtnAReprod_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BtnAReprod.MouseDown
        BtnAReprod.Image = iPlayer.My.Resources.Resources.m1
        ListView1.Focus()
    End Sub

    Private Sub BtnAReprod_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BtnAReprod.MouseUp
        ListView1.Focus()
        BtnAReprod.Image = iPlayer.My.Resources.Resources.img1

        Dim indexes As ListView.SelectedIndexCollection = ListView1.SelectedIndices
        If PlaylistActual <> 2 Then

            If indexes.Count > 1 Then 'multiselec
                If LabelARepr.Text = "Item seleccionado" Then
                    LabelARepr.Text = "A reproduzir"

                    If verificaReproduzir = True Then
                        If AReprod_CD = False Then
                            CapaAReprod()
                        Else
                            NopCapaCD()
                        End If
                    Else
                        CapaNadaEmReprod()
                    End If

                ElseIf LabelARepr.Text = "A reproduzir" Then
                    LabelARepr.Text = "Item seleccionado"
                    CapaMultiSel()
                    selected = False
                    Exit Sub
                End If
            ElseIf indexes.Count = 1 Then 'apenas uma seleccionada

                If LabelARepr.Text = "Item seleccionado" Then
                    LabelARepr.Text = "A reproduzir"
                    If verificaReproduzir = True Then
                        If AReprod_CD = False Then
                            CapaAReprod()
                        Else
                            NopCapaCD()
                        End If
                    Else
                        CapaNadaEmReprod()
                    End If

                ElseIf LabelARepr.Text = "A reproduzir" Then
                    LabelARepr.Text = "Item seleccionado"
                    capaItemSel()
                End If

                MostrarClassificacao()

            ElseIf indexes.Count = 0 Then 'nenhuma seleccionada
                If LabelARepr.Text = "Item seleccionado" Then
                    LabelARepr.Text = "A reproduzir"
                    If verificaReproduzir = True Then
                        If AReprod_CD = False Then
                            CapaAReprod()
                        Else
                            NopCapaCD()
                        End If
                    Else
                        CapaNadaEmReprod()
                    End If

                ElseIf LabelARepr.Text = "A reproduzir" Then
                    LabelARepr.Text = "Item seleccionado"
                    CapaNadaSelec()
                End If
            End If
        Else ' caso haja um cd
            If LabelARepr.Text = "Item seleccionado" Then
                LabelARepr.Text = "A reproduzir"
                If verificaReproduzir = True Then
                    NopCapaCD()
                Else
                    CapaNadaEmReprod()
                End If
            ElseIf LabelARepr.Text = "A reproduzir" Then
                LabelARepr.Text = "Item seleccionado"

                If indexes.Count = 0 Then
                    CapaNadaSelec()
                ElseIf indexes.Count = 1 Then
                    NopCapaCD()
                ElseIf indexes.Count > 1 Then
                    CapaMultiSel()
                End If
            End If
        End If
    End Sub

    Sub capaItemSel()
        NumMusica = ListView1.Items(IndexSelec).SubItems(10).Text

        Try

            DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusica)
            PictureBoxCapa.ImageLocation = DataTableSemNome.Rows(0).Item("Capa")
            PictureBoxCapa.SizeMode = PictureBoxSizeMode.StretchImage
            ImageList1.Images.Add(Image.FromFile(PictureBoxCapa.ImageLocation))
            TemCapaBool = True
            CapaSemBool = False
        Catch ex As Exception
            CapaSemBool = True
            TemCapaBool = False
            PictureBoxCapa.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBoxCapa.Image = iPlayer.My.Resources.sem_capa
        End Try
    End Sub

    Sub CapaMultiSel()
        TemCapaBool = False
        CapaSemBool = False
        PictureBoxCapa.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBoxCapa.Image = iPlayer.My.Resources.MultiSelec
    End Sub

    Sub CapaAReprod()
        Try

            NumMusica = ListView1.Items(indexAReprod).SubItems(10).Text
            DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusica)
            PictureBoxCapa.ImageLocation = DataTableSemNome.Rows(0).Item("Capa")
            PictureBoxCapa.SizeMode = PictureBoxSizeMode.StretchImage
            TemCapaBool = True
            CapaSemBool = False
        Catch ex As Exception
            CapaSemBool = True
            TemCapaBool = False
            PictureBoxCapa.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBoxCapa.Image = iPlayer.My.Resources.sem_capa
        End Try
    End Sub

    Sub CapaNadaEmReprod()
        TemCapaBool = False
        CapaSemBool = False
        PictureBoxCapa.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBoxCapa.Image = iPlayer.My.Resources.nada_Em_Reprod
    End Sub

    Sub NopCapaCD()
        TemCapaBool = False
        CapaSemBool = False
        PictureBoxCapa.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBoxCapa.Image = iPlayer.My.Resources.NopCapaCD
    End Sub

    Sub CapaNadaSelec()
        TemCapaBool = False
        CapaSemBool = False
        PictureBoxCapa.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBoxCapa.Image = iPlayer.My.Resources.nada_seleccionado
    End Sub

    Private Sub SeleccionarTudoToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SeleccionarTudoToolStripMenuItem1.Click
        verificaSelec = True
        For i As Integer = 0 To ListView1.Items.Count - 1
            ListView1.Items(i).Selected = True
            If PlaylistActual <> 2 Then
                EliminarToolStripMenuItem1.Enabled = True
            End If
        Next
        Dim index2 As Integer = -1
        Dim indexes As ListView.SelectedIndexCollection = ListView1.SelectedIndices

        If indexes.Count > 1 Then
            LabelARepr.Text = "Item seleccionado"
            If LabelARepr.Text = "Item seleccionado" Then
                CapaMultiSel()
            Else
                If verificaReproduzir = False Then
                    CapaNadaEmReprod()
                Else
                    CapaAReprod()
                End If
            End If
            ClassifToolStripMenuItem.Enabled = False
            InforToolStripMenuItem.Enabled = False
        End If

        ListView1.Focus()
        AnularSelecçãoToolStripMenuItem1.Enabled = True
        BtnAReprod.Visible = True
        verificaSelec = False
    End Sub

    Sub MostrarClassificacao()
        DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusica)
        Dim star As String
        Try
            star = DataTableSemNome.Rows(0).Item("Classificacao")
        Catch ex As Exception
        End Try
        Select Case Len(star)
            Case 0
                ToolStripMenuItem3.Checked = False
                ToolStripMenuItem4.Checked = False
                ToolStripMenuItem5.Checked = False
                ToolStripMenuItem8.Checked = False
                ToolStripMenuItem9.Checked = False
                ToolStripMenuItem10.Checked = False
                ToolStripMenuItem11.Checked = False
                ToolStripMenuItem12.Checked = False
                ToolStripMenuItem13.Checked = False
                ToolStripMenuItem14.Checked = False
                NenhuToolStripMenuItem2.Checked = True
                NenhumToolStripMenuItem.Checked = True

            Case 1
                ToolStripMenuItem3.Checked = True
                ToolStripMenuItem10.Checked = True
                ToolStripMenuItem4.Checked = False
                ToolStripMenuItem5.Checked = False
                ToolStripMenuItem8.Checked = False
                ToolStripMenuItem9.Checked = False
                NenhumToolStripMenuItem.Checked = False
                ToolStripMenuItem11.Checked = False
                ToolStripMenuItem12.Checked = False
                ToolStripMenuItem13.Checked = False
                ToolStripMenuItem14.Checked = False
                NenhuToolStripMenuItem2.Checked = False

            Case 2

                ToolStripMenuItem3.Checked = False
                ToolStripMenuItem4.Checked = True
                ToolStripMenuItem11.Checked = True
                ToolStripMenuItem5.Checked = False
                ToolStripMenuItem8.Checked = False
                ToolStripMenuItem9.Checked = False
                NenhumToolStripMenuItem.Checked = False
                ToolStripMenuItem10.Checked = False
                ToolStripMenuItem12.Checked = False
                ToolStripMenuItem13.Checked = False
                ToolStripMenuItem14.Checked = False
                NenhuToolStripMenuItem2.Checked = False

            Case 3

                ToolStripMenuItem3.Checked = False
                ToolStripMenuItem4.Checked = False
                ToolStripMenuItem5.Checked = True
                ToolStripMenuItem12.Checked = True
                ToolStripMenuItem8.Checked = False
                ToolStripMenuItem9.Checked = False
                NenhuToolStripMenuItem2.Checked = False
                NenhumToolStripMenuItem.Checked = False
                ToolStripMenuItem10.Checked = False
                ToolStripMenuItem11.Checked = False
                ToolStripMenuItem13.Checked = False
                ToolStripMenuItem14.Checked = False

            Case 4

                ToolStripMenuItem3.Checked = False
                ToolStripMenuItem4.Checked = False
                ToolStripMenuItem5.Checked = False
                ToolStripMenuItem8.Checked = True
                ToolStripMenuItem13.Checked = True
                ToolStripMenuItem9.Checked = False
                NenhuToolStripMenuItem2.Checked = False
                NenhumToolStripMenuItem.Checked = False
                ToolStripMenuItem10.Checked = False
                ToolStripMenuItem11.Checked = False
                ToolStripMenuItem12.Checked = False
                ToolStripMenuItem14.Checked = False

            Case 5

                ToolStripMenuItem3.Checked = False
                ToolStripMenuItem4.Checked = False
                ToolStripMenuItem5.Checked = False
                ToolStripMenuItem8.Checked = False
                ToolStripMenuItem9.Checked = True
                ToolStripMenuItem14.Checked = True
                NenhumToolStripMenuItem.Checked = False
                ToolStripMenuItem10.Checked = False
                ToolStripMenuItem11.Checked = False
                ToolStripMenuItem12.Checked = False
                ToolStripMenuItem13.Checked = False
                NenhuToolStripMenuItem2.Checked = False
        End Select
    End Sub

    Private Sub ListView1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Leave
        If verificaReproduzir = True Then
            If PlaylistActual = naux Then
                ListView1.Items(indexAReprod).BackColor = Color.LightSkyBlue
                Me.Refresh()
            End If
        End If
        If IndexSelec <> -1 Then
            Try
                ListView1.Items(indexAReprod).Selected = False
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        If verificaSelec = False Then
            Dim index2 As Integer = -1
            Dim indexes As ListView.SelectedIndexCollection = ListView1.SelectedIndices
            If PlaylistActual = 2 Then
                If indexes.Count = 0 Then
                    AnularSelecçãoToolStripMenuItem1.Enabled = False
                    CapaNadaSelec()
                End If
            End If

            If indexes.Count <> 0 Then
                If PlaylistActual <> 2 Then '1
                    If indexes.Count > 1 Then 'multiselec
                        If PlaylistActual >= 7 Then
                            CortarToolStripMenuItem1.Enabled = True
                        End If
                        If LabelARepr.Text = "Item seleccionado" Then
                            CapaMultiSel()
                            InforToolStripMenuItem.Enabled = False
                            InformaçõesToolStripMenuItem2.Enabled = False
                            selected = False
                            Exit Sub
                        ElseIf LabelARepr.Text = "A reproduzir" Then
                            If verificaReproduzir = True Then
                                If AReprod_CD = False Then
                                    CapaAReprod()
                                Else
                                    NopCapaCD()
                                End If
                            Else
                                CapaNadaEmReprod()
                            End If

                        End If
                    ElseIf indexes.Count = 1 Then 'apenas uma seleccionada
                        For Each index2 In indexes
                            IndexSelec = index2
                            InforToolStripMenuItem.Enabled = True
                            ClassifToolStripMenuItem.Enabled = True
                            BtnAReprod.Visible = True
                            CopiarToolStripMenuItem.Enabled = True
                            SeleccionarTudoToolStripMenuItem1.Enabled = True
                            AnularSelecçãoToolStripMenuItem1.Enabled = True
                            If PlaylistActual >= 7 Then
                                CortarToolStripMenuItem1.Enabled = True
                            End If
                            If LabelARepr.Text = "" Then
                                LabelARepr.Text = "Item seleccionado"
                                BtnAReprod.Visible = True
                            End If
                            selected = True
                        Next
                        '--------------------------------

                        If LabelARepr.Text = "Item seleccionado" Then
                            capaItemSel()
                        ElseIf LabelARepr.Text = "A reproduzir" Then
                            If verificaReproduzir = True Then
                                If AReprod_CD = False Then
                                    CapaAReprod()
                                Else
                                    NopCapaCD()
                                End If
                            Else
                                CapaNadaEmReprod()
                            End If
                        End If

                        MostrarClassificacao()

                    ElseIf indexes.Count = 0 Then 'nenhuma seleccionada
                        InforToolStripMenuItem.Enabled = False
                        ClassifToolStripMenuItem.Enabled = False
                        BtnAReprod.Visible = True
                        CopiarToolStripMenuItem.Enabled = False
                        AnularSelecçãoToolStripMenuItem1.Enabled = False
                        selected = False
                        If PlaylistActual >= 7 Then
                            CortarToolStripMenuItem1.Enabled = False
                        End If
                        '---------------------
                        If LabelARepr.Text = "Item seleccionado" Then
                            capaItemSel()
                        ElseIf LabelARepr.Text = "A reproduzir" Then
                            If verificaReproduzir = True Then
                                If AReprod_CD = False Then
                                    CapaAReprod()
                                Else
                                    NopCapaCD()
                                End If
                            Else
                                CapaNadaEmReprod()
                            End If
                        End If
                    End If

                Else ' caso haja um cd
                    If indexes.Count > 0 Then
                        For Each index2 In indexes
                            IndexSelec = index2
                        Next
                        AnularSelecçãoToolStripMenuItem1.Enabled = True
                        BtnAReprod.Visible = True
                        NopCapaCD()
                    ElseIf indexes.Count = 0 Then
                        AnularSelecçãoToolStripMenuItem1.Enabled = False
                        CapaNadaSelec()
                    End If

                    If LabelARepr.Text = "" Then
                        LabelARepr.Text = "Item seleccionado"
                    End If
                End If '1
                If PlaylistActual > 1 And PlaylistActual < 7 Then
                    EliminarToolStripMenuItem1.Enabled = False
                    EliminarToolStripMenuItem3.Enabled = False
                Else
                    EliminarToolStripMenuItem1.Enabled = True
                    EliminarToolStripMenuItem3.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub ReproduzirToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReproduzirToolStripMenuItem1.Click
        If RadioOn = False Then
            Reproduzir2()
        Else
            PararRadio()
        End If
    End Sub

    Private Sub TimerDuracao_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerDuracao.Tick
        TrackBar2.Maximum = AxWindowsMediaPlayer1.currentMedia.duration
        Try
            TrackBar2.Value = AxWindowsMediaPlayer1.Ctlcontrols.currentPosition
        Catch ex As Exception
        End Try

        LabelDuracaoMais.Text = AxWindowsMediaPlayer1.Ctlcontrols.currentPositionString
        LabelDuracaoMenos.Text = AxWindowsMediaPlayer1.currentMedia.durationString
        If primeiravez = True Then
            If LabelDuracaoMenos.Text <> "" Then
                If PlaylistActual = naux Then
                    ListView1.Items(indexAReprod).SubItems(1).Text = LabelDuracaoMenos.Text
                    iPlayer_DAL.Musica.AlterarDuracao(NumMusicaAReprod, LabelDuracaoMenos.Text)
                    primeiravez = False
                    PreencherPlaylist(PlaylistActual)
                End If
            End If
        End If
    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        AxWindowsMediaPlayer1.settings.volume = TrackBar1.Value * 10
    End Sub

    Private Sub TrackBar2_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar2.Scroll
        LabelDuracaoMais.Text = AxWindowsMediaPlayer1.Ctlcontrols.currentPositionString
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar2.Value / 100
    End Sub

    Private Sub TrackBar2_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TrackBar2.MouseUp
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar2.Value
    End Sub

    Public Sub NenhuToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem9.Click, ToolStripMenuItem8.Click, ToolStripMenuItem5.Click, ToolStripMenuItem4.Click, ToolStripMenuItem3.Click, ReiniciarContagemReprodToolStripMenuItem2.Click, NenhuToolStripMenuItem2.Click, InformaçõesToolStripMenuItem2.Click, EliminarToolStripMenuItem3.Click, AdicionarÀListaDeReproduçãoToolStripMenuItem.Click
        Dim T As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        Dim tagcontextmenustrip, index As Integer

        tagcontextmenustrip = T.Tag
        Dim ArrayNum() As Integer
        Dim indexes As ListView.SelectedIndexCollection = ListView1.SelectedIndices
        Try
            NumMusica = ListView1.Items(index).SubItems(10).Text
        Catch ex As Exception
            Exit Sub
        End Try

        Select Case tagcontextmenustrip
            Case 1
                JanelaInformacoes.ShowDialog()
            Case 2
                Classificar(0, NumMusica)
            Case 3
                Classificar(1, NumMusica)
            Case 4
                Classificar(2, NumMusica)
            Case 5
                Classificar(3, NumMusica)
            Case 6
                Classificar(4, NumMusica)
            Case 7
                Classificar(5, NumMusica)
            Case 8 'reiniciar contagens
                If indexes.Count = 1 Then
                    Dim n As Integer
                    Dim str As String
                    For Each n In indexes
                        str = ListView1.Items(n).SubItems(0).Text
                    Next
                    Dim resposta As DialogResult = System.Windows.Forms.MessageBox.Show("Tem a certeza de que pretende reiniciar as contagens de reprodução  da música " & "'" & str & "'" & "?", "iPlayer", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If resposta = DialogResult.Yes Then
                        NumMusica = CInt(ListView1.Items(IndexSelec).SubItems(10).Text)
                        iPlayer_DAL.Musica.ReiniciarContagem(NumMusica)
                        ListView1.Items(IndexSelec).SubItems(7).Text = ""
                    End If

                ElseIf indexes.Count > 1 Then
                    Dim resposta As DialogResult = System.Windows.Forms.MessageBox.Show("Tem a certeza de que pretende reiniciar as contagens de reprodução  das músicas seleccionadas?", "iPlayer", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If resposta = DialogResult.Yes Then
                        ReDim ArrayNum(indexes.Count - 1)

                        For i As Integer = 0 To indexes.Count - 1
                            For Each n In indexes
                                ArrayNum(i) = ListView1.Items(n).SubItems(10).Text
                                ListView1.Items(n).SubItems(7).Text = ""
                                i += 1
                            Next
                            Exit For
                        Next

                        For j As Integer = 0 To ArrayNum.Count - 1
                            iPlayer_DAL.Musica.ReiniciarContagem(ArrayNum(j))
                        Next
                    End If
                End If

            Case 9
                EliminarMusica()
            Case Else
                Dim Tag As Integer = iPlayer_DAL.Playlist.MostrarTagUsandotagcontextmenustrip(tagcontextmenustrip)
                DataTablePlaylist = iPlayer_DAL.Playlist.MostrarUsandoNumPlaylist(Tag)
                If indexes.Count > 1 Then
                    ReDim ArrayNum(indexes.Count - 1)
                    Dim n As Integer
                    For i As Integer = 0 To indexes.Count - 1
                        For Each n In indexes
                            ArrayNum(i) = ListView1.Items(n).SubItems(10).Text
                            i += 1
                        Next
                        Exit For
                    Next
                    Dim k As Integer = 0

                    For j As Integer = 0 To ArrayNum.Count
                        If DataTablePlaylist.Rows.Count = 0 Then
                            iPlayer_DAL.Playlist.CriarRelacao(Tag, ArrayNum(j))
                        Else
                            For k = k To DataTablePlaylist.Rows.Count
                                If ArrayNum(j) = DataTablePlaylist.Rows(k).Item("NumMusica") Then
                                    Dim l As String = ListView1.Items(j).SubItems(0).Text
                                    System.Windows.Forms.MessageBox.Show("Não foi possível adicionar a música" & " " & "'" & l & "'," & " " & "visto que esta já existe na lista de reprodução indicada.", "iPlayer", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    Exit For
                                Else
                                    iPlayer_DAL.Playlist.CriarRelacao(Tag, ArrayNum(j))
                                End If
                                If j = DataTablePlaylist.Rows.Count - 1 Then
                                    Exit Sub
                                End If
                            Next
                        End If
                        k += 1
                        If j = DataTablePlaylist.Rows.Count - 1 Then
                            Exit Sub
                        End If
                    Next
                Else
                    Dim f As Integer
                    For Each f In indexes
                        NumMusica = ListView1.Items(f).SubItems(10).Text
                    Next
                    If DataTablePlaylist.Rows.Count = 0 Then
                        iPlayer_DAL.Playlist.CriarRelacao(Tag, NumMusica)
                    Else
                        For k As Integer = 0 To DataTablePlaylist.Rows.Count - 1
                            Dim j As Integer = DataTablePlaylist.Rows(k).Item("NumMusica")
                            If NumMusica = DataTablePlaylist.Rows(k).Item("NumMusica") Then
                                Dim l As String = ListView1.Items(f).SubItems(0).Text
                                System.Windows.Forms.MessageBox.Show("Não foi possível adicionar a música" & " " & "'" & l & "'," & " " & "visto que esta já existe na lista de reprodução indicada.", "iPlayer", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Exit Sub
                            End If
                        Next
                        iPlayer_DAL.Playlist.CriarRelacao(Tag, NumMusica)
                    End If
                End If
        End Select
    End Sub

    Sub Classificar(ByVal m As Integer, ByVal nummusica As Integer)

        Dim ArrayNum() As Integer
        Dim indexes As ListView.SelectedIndexCollection = ListView1.SelectedIndices
        Select Case m

            Case 0
                NumStar = ""
                ToolStripMenuItem3.Checked = False
                ToolStripMenuItem4.Checked = False
                ToolStripMenuItem5.Checked = False
                ToolStripMenuItem8.Checked = False
                ToolStripMenuItem9.Checked = False
                ToolStripMenuItem10.Checked = False
                ToolStripMenuItem11.Checked = False
                ToolStripMenuItem12.Checked = False
                ToolStripMenuItem13.Checked = False
                ToolStripMenuItem14.Checked = False
                NenhuToolStripMenuItem2.Checked = True
                NenhumToolStripMenuItem.Checked = True

                If indexes.Count > 1 Then
                    ReDim ArrayNum(indexes.Count - 1)
                    Dim n As Integer
                    For i As Integer = 0 To indexes.Count - 1
                        For Each n In indexes
                            ArrayNum(i) = ListView1.Items(n).SubItems(10).Text
                            ListView1.Items(n).SubItems(9).Text = NumStar
                            i += 1
                        Next
                        Exit For
                    Next

                    For j As Integer = 0 To ArrayNum.Count - 1
                        iPlayer_DAL.Musica.AlterarClassificacao(ArrayNum(j), NumStar)
                    Next
                Else
                    iPlayer_DAL.Musica.AlterarClassificacao(nummusica, NumStar)
                    ListView1.Items(index).SubItems(9).Text = NumStar
                End If

            Case 1
                NumStar = "★"
                ToolStripMenuItem3.Checked = True
                ToolStripMenuItem10.Checked = True
                ToolStripMenuItem4.Checked = False
                ToolStripMenuItem5.Checked = False
                ToolStripMenuItem8.Checked = False
                ToolStripMenuItem9.Checked = False
                NenhumToolStripMenuItem.Checked = False
                ToolStripMenuItem11.Checked = False
                ToolStripMenuItem12.Checked = False
                ToolStripMenuItem13.Checked = False
                ToolStripMenuItem14.Checked = False
                NenhuToolStripMenuItem2.Checked = False
                If indexes.Count > 1 Then
                    ReDim ArrayNum(indexes.Count - 1)
                    Dim n As Integer
                    For i As Integer = 0 To indexes.Count - 1
                        For Each n In indexes
                            ArrayNum(i) = ListView1.Items(n).SubItems(10).Text
                            ListView1.Items(n).SubItems(9).Text = NumStar
                            i += 1
                        Next
                        Exit For
                    Next

                    For j As Integer = 0 To ArrayNum.Count - 1
                        iPlayer_DAL.Musica.AlterarClassificacao(ArrayNum(j), NumStar)
                    Next
                Else
                    iPlayer_DAL.Musica.AlterarClassificacao(nummusica, NumStar)
                End If

            Case 2
                NumStar = "★★"
                ToolStripMenuItem3.Checked = False
                ToolStripMenuItem4.Checked = True
                ToolStripMenuItem11.Checked = True
                ToolStripMenuItem5.Checked = False
                ToolStripMenuItem8.Checked = False
                ToolStripMenuItem9.Checked = False
                NenhumToolStripMenuItem.Checked = False
                ToolStripMenuItem10.Checked = False
                ToolStripMenuItem12.Checked = False
                ToolStripMenuItem13.Checked = False
                ToolStripMenuItem14.Checked = False
                NenhuToolStripMenuItem2.Checked = False
                If indexes.Count > 1 Then
                    ReDim ArrayNum(indexes.Count - 1)
                    Dim n As Integer
                    For i As Integer = 0 To indexes.Count - 1
                        For Each n In indexes
                            ArrayNum(i) = ListView1.Items(n).SubItems(10).Text
                            ListView1.Items(n).SubItems(9).Text = NumStar
                            i += 1
                        Next
                        Exit For
                    Next

                    For j As Integer = 0 To ArrayNum.Count - 1
                        iPlayer_DAL.Musica.AlterarClassificacao(ArrayNum(j), NumStar)
                    Next
                Else
                    iPlayer_DAL.Musica.AlterarClassificacao(nummusica, NumStar)
                    ListView1.Items(index).SubItems(9).Text = NumStar
                End If

            Case 3
                NumStar = "★★★"
                ToolStripMenuItem3.Checked = False
                ToolStripMenuItem4.Checked = False
                ToolStripMenuItem5.Checked = True
                ToolStripMenuItem12.Checked = True
                ToolStripMenuItem8.Checked = False
                ToolStripMenuItem9.Checked = False
                NenhuToolStripMenuItem2.Checked = False
                NenhumToolStripMenuItem.Checked = False
                ToolStripMenuItem10.Checked = False
                ToolStripMenuItem11.Checked = False
                ToolStripMenuItem13.Checked = False
                ToolStripMenuItem14.Checked = False
                If indexes.Count > 1 Then
                    ReDim ArrayNum(indexes.Count - 1)
                    Dim n As Integer
                    For i As Integer = 0 To indexes.Count - 1
                        For Each n In indexes
                            ArrayNum(i) = ListView1.Items(n).SubItems(10).Text
                            ListView1.Items(n).SubItems(9).Text = NumStar
                            i += 1
                        Next
                        Exit For
                    Next

                    For j As Integer = 0 To ArrayNum.Count - 1
                        iPlayer_DAL.Musica.AlterarClassificacao(ArrayNum(j), NumStar)
                    Next
                Else
                    iPlayer_DAL.Musica.AlterarClassificacao(nummusica, NumStar)
                    ListView1.Items(index).SubItems(9).Text = NumStar
                End If

            Case 4
                NumStar = "★★★★"

                ToolStripMenuItem3.Checked = False
                ToolStripMenuItem4.Checked = False
                ToolStripMenuItem5.Checked = False
                ToolStripMenuItem8.Checked = True
                ToolStripMenuItem13.Checked = True
                ToolStripMenuItem9.Checked = False
                NenhuToolStripMenuItem2.Checked = False
                NenhumToolStripMenuItem.Checked = False
                ToolStripMenuItem10.Checked = False
                ToolStripMenuItem11.Checked = False
                ToolStripMenuItem12.Checked = False
                ToolStripMenuItem14.Checked = False
                If indexes.Count > 1 Then
                    ReDim ArrayNum(indexes.Count - 1)
                    Dim n As Integer
                    For i As Integer = 0 To indexes.Count - 1
                        For Each n In indexes
                            ArrayNum(i) = ListView1.Items(n).SubItems(10).Text
                            ListView1.Items(n).SubItems(9).Text = NumStar
                            i += 1
                        Next
                        Exit For
                    Next

                    For j As Integer = 0 To ArrayNum.Count - 1
                        iPlayer_DAL.Musica.AlterarClassificacao(ArrayNum(j), NumStar)
                    Next
                Else
                    iPlayer_DAL.Musica.AlterarClassificacao(nummusica, NumStar)
                    ListView1.Items(index).SubItems(9).Text = NumStar
                End If

            Case 5
                NumStar = "★★★★★"

                ToolStripMenuItem3.Checked = False
                ToolStripMenuItem4.Checked = False
                ToolStripMenuItem5.Checked = False
                ToolStripMenuItem8.Checked = False
                ToolStripMenuItem9.Checked = True
                ToolStripMenuItem14.Checked = True
                NenhumToolStripMenuItem.Checked = False
                ToolStripMenuItem10.Checked = False
                ToolStripMenuItem11.Checked = False
                ToolStripMenuItem12.Checked = False
                ToolStripMenuItem13.Checked = False
                NenhuToolStripMenuItem2.Checked = False

                If indexes.Count > 1 Then
                    ReDim ArrayNum(indexes.Count - 1)
                    Dim n As Integer
                    For i As Integer = 0 To indexes.Count - 1
                        For Each n In indexes
                            ArrayNum(i) = ListView1.Items(n).SubItems(10).Text
                            ListView1.Items(n).SubItems(9).Text = NumStar
                            i += 1
                        Next
                        Exit For
                    Next

                    For j As Integer = 0 To ArrayNum.Count - 1
                        iPlayer_DAL.Musica.AlterarClassificacao(ArrayNum(j), NumStar)
                    Next
                Else
                    iPlayer_DAL.Musica.AlterarClassificacao(nummusica, NumStar)
                    ListView1.Items(index).SubItems(9).Text = NumStar
                End If
        End Select
        NumStar = ""

    End Sub

    Private Sub NenhumToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem14.Click, ToolStripMenuItem13.Click, ToolStripMenuItem12.Click, ToolStripMenuItem11.Click, ToolStripMenuItem10.Click, NenhumToolStripMenuItem.Click
        Dim T As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        Dim n, index As Integer
        Dim indexes As ListView.SelectedIndexCollection = ListView1.SelectedIndices
        n = T.Tag
        NumMusica = ListView1.Items(index).SubItems(10).Text
        Select Case n
            Case 1
                Classificar(0, NumMusica)
            Case 2
                Classificar(1, NumMusica)
            Case 3
                Classificar(2, NumMusica)
            Case 4
                Classificar(3, NumMusica)
            Case 5
                Classificar(4, NumMusica)
            Case 6
                Classificar(5, NumMusica)
            Case 7
                JanelaInformacoes.Show()
        End Select
    End Sub

    Private Sub InforToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InforToolStripMenuItem.Click
        JanelaInformacoes.ShowDialog()
    End Sub

    Private Sub SeguinteToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SeguinteToolStripMenuItem1.Click
        ProximaMusica()
    End Sub

    Private Sub AnteriorToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnteriorToolStripMenuItem1.Click
        MusicaAnterior()
    End Sub

    Private Sub AnteriorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnteriorToolStripMenuItem.Click
        MusicaAnterior()
    End Sub

    Private Sub SeguinteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SeguinteToolStripMenuItem.Click
        ProximaMusica()
    End Sub

    Sub ListViewClick()
        If verificaReproduzir = True Then
            If LabelARepr.Text = "A reproduzir" Then
                CapaAReprod()
            ElseIf LabelARepr.Text = "Item seleccionado" Then
                capaItemSel()
            End If
        End If
    End Sub

    Sub parar()
        'pára tudo
        verificaReproduzir = False
        If ListView1.Items.Count = 0 Then
            BtnPlay.Image = iPlayer.My.Resources.playerpp_enabled
            ReproduzirToolStripMenuItem1.Enabled = False
            ReproduzirToolStripMenuItem.Enabled = False
        Else
            BtnPlay.Image = iPlayer.My.Resources.playerpp
            ReproduzirToolStripMenuItem1.Enabled = True
            ReproduzirToolStripMenuItem.Enabled = True
        End If

        ReproduzirToolStripMenuItem1.Text = "Reproduzir"
        ReproduzirToolStripMenuItem.Text = "Reproduzir"
        AnteriorToolStripMenuItem.Enabled = False
        AnteriorToolStripMenuItem1.Enabled = False
        SeguinteToolStripMenuItem1.Enabled = False
        SeguinteToolStripMenuItem.Enabled = False
        verificaPlay = True
        verifican = False
        CountAReprod = 0
        verificaStop = True
        verificaPausa = False
        TimerDuracao.Stop()
        TimerInterprete.Stop()
        TimerNomeMusica.Stop()
        PictureBoxPrincipal.Image = iPlayer.My.Resources.BlueDisc11
        PictureBoxPrincipal.Visible = True
        BtnAReprod.Visible = False
        LabelARepr.Text = ""
        BtnAnterior.Image = iPlayer.My.Resources.player_rew_32_enabled
        BtnSeguinte.Image = iPlayer.My.Resources.player_fwd_32_enabled
        PictureBoxCapa.SizeMode = PictureBoxSizeMode.Zoom
        PictureBoxCapa.Image = iPlayer.My.Resources.audioandradio_hover
        IndexSelec = -1
        ColorirListview()
        AxWindowsMediaPlayer1.URL = ""
        AReprod_CD = False
    End Sub

    Private Sub AxWindowsMediaPlayer1_CdromMediaChange(ByVal sender As Object, ByVal e As AxWMPLib._WMPOCXEvents_CdromMediaChangeEvent) Handles AxWindowsMediaPlayer1.CdromMediaChange
        Dim playlist As WMPLib.IWMPPlaylist = AxWindowsMediaPlayer1.cdromCollection.Item(0).Playlist
        If playlist.count = 0 Then
            EjectarDisco()
        Else
            'verifica se o cd é um cd de dados
            Try
                d = playlist.Item(0).durationString
                If d = "00:00" Then
                    verificaCdDados = True
                End If
            Catch ex As Exception
            End Try
        End If
        AxWindowsMediaPlayer1.URL = ""

        If playlist.count >= 1 Then
            If verificaCdDados = False Then
                BtnAudioCD.BackColor = Color.LightBlue
                BtnAudioCD.FlatAppearance.MouseDownBackColor = Color.LightBlue
                BtnAudioCD.FlatAppearance.MouseOverBackColor = Color.LightBlue
                BtnEjectarDisco.BackColor = Color.LightBlue
                BtnEjectarDisco.FlatAppearance.MouseDownBackColor = Color.LightBlue
                BtnEjectarDisco.FlatAppearance.MouseOverBackColor = Color.LightBlue
                PlaylistActual = 2
                BtnMusicas.BackColor = Color.Transparent
                BtnMusicas.FlatAppearance.MouseDownBackColor = Color.Silver
                BtnMusicas.FlatAppearance.MouseOverBackColor = Color.Silver
                verificaCD = True
                BtnAudioCD.Text = playlist.name()
                verificaCDAudio()
                PreencherCDAudio()

                If LabelARepr.Text = "A reproduzir" Then
                    If verificaReproduzir = True Then
                        If PlaylistActual = 2 Then
                            PictureBoxCapa.SizeMode = PictureBoxSizeMode.CenterImage
                            PictureBoxCapa.Image = iPlayer.My.Resources.NopCapaCD
                        Else
                            CapaAReprod()
                        End If
                    Else
                        If PlaylistActual = 2 Then
                            PictureBoxCapa.SizeMode = PictureBoxSizeMode.CenterImage
                            PictureBoxCapa.Image = iPlayer.My.Resources.NopCapaCD
                        Else
                            PictureBoxCapa.SizeMode = PictureBoxSizeMode.StretchImage
                            PictureBoxCapa.Image = iPlayer.My.Resources.nada_Em_Reprod
                        End If
                    End If
                ElseIf LabelARepr.Text = "Item seleccionado" Then
                    PictureBoxCapa.SizeMode = PictureBoxSizeMode.CenterImage
                    PictureBoxCapa.Image = iPlayer.My.Resources.NopCapaCD
                End If
            End If
        End If
    End Sub

    Private Sub AxWindowsMediaPlayer1_DoubleClickEvent(ByVal sender As System.Object, ByVal e As AxWMPLib._WMPOCXEvents_DoubleClickEvent) Handles AxWindowsMediaPlayer1.DoubleClickEvent
        AxWindowsMediaPlayer1.fullScreen = False
    End Sub

    Sub copiar()
        Dim index As Integer
        Dim indexes As ListView.SelectedIndexCollection = ListView1.SelectedIndices
        ReDim CopyArray(indexes.Count - 1)
        For i As Integer = 0 To indexes.Count - 1
            For Each index In indexes
                CopyArray(i) = ListView1.Items(index).SubItems(10).Text
                i += 1
            Next
            VerificaCopiar = True
            Exit Sub
        Next
    End Sub

    Private Sub CopiarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopiarToolStripMenuItem.Click
        copiar()
        If PlaylistActual > 6 Then
            ColarToolStripMenuItem2.Enabled = True
        End If
    End Sub

    Private Sub EliminarToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EliminarToolStripMenuItem1.Click
        EliminarMusica()
    End Sub

    Sub CoisasDoEliminar()
        Dim indexes As ListView.SelectedIndexCollection = ListView1.SelectedIndices
        If ListView1.Items.Count = indexes.Count Then
            ListView1.Items.Clear()
            EliminarToolStripMenuItem1.Enabled = False
            AnteriorToolStripMenuItem.Enabled = False
            AnteriorToolStripMenuItem1.Enabled = False
            SeguinteToolStripMenuItem.Enabled = False
            SeguinteToolStripMenuItem1.Enabled = False
            ReproduzirToolStripMenuItem.Enabled = False
            ReproduzirToolStripMenuItem1.Enabled = False
            ClassifToolStripMenuItem.Enabled = False
            InforToolStripMenuItem.Enabled = False
            CopiarToolStripMenuItem.Enabled = False
            CortarToolStripMenuItem1.Enabled = False
            SeleccionarTudoToolStripMenuItem1.Enabled = False
            AnularSelecçãoToolStripMenuItem1.Enabled = False
            ColarToolStripMenuItem2.Enabled = False
            LabelTudo.Text = ""
            BtnPlay.Image = iPlayer.My.Resources.playerpp_enabled
        Else
            PreencherPlaylist(PlaylistActual)
        End If

        BtnAReprod.Visible = False
        LabelARepr.Text = ""
        PictureBoxCapa.SizeMode = PictureBoxSizeMode.Zoom
        PictureBoxCapa.Image = iPlayer.My.Resources.audioandradio_hover
        selected = False
    End Sub

    Sub countM1()
        aCarregar.Show()
        Application.DoEvents()
        countI1()
    End Sub

    Sub countI1()
        Dim indexes As ListView.SelectedIndexCollection = ListView1.SelectedIndices

        If aCarregar.Visible = True Then
            k += 1
            aCarregar.Label2.Text = "A eliminar" & " " & k & " " & "de" & " " & indexes.Count & ": " & n.Substring(4, n.Length - 4)
        End If
        DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusica)
        Try
            My.Computer.FileSystem.DeleteFile(DataTableSemNome.Rows(0).Item("Capa"))
        Catch ex As Exception
        End Try
    End Sub

    Sub EliminarMusica()
        Dim index As Integer
        Dim indexes As ListView.SelectedIndexCollection = ListView1.SelectedIndices
        k = 0
        If ListView1.SelectedItems.Count = 1 Then
            Dim resposta As DialogResult = System.Windows.Forms.MessageBox.Show("Tem a certeza que deseja eliminar a música seleccionada ?", "iPlayer", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resposta = DialogResult.Yes Then
                NumMusica = ListView1.Items(IndexSelec).SubItems(10).Text
                ListView1.Focus()
                For Each index In indexes
                    If index = indexAReprod Then
                        If verificaReproduzir = True Then
                            AxWindowsMediaPlayer1.Ctlcontrols.stop()
                        End If
                    End If
                Next
                If PlaylistActual < 7 Then
                    For Each index In indexes
                        verificaEliminar = True
                        countI1()
                        iPlayer_DAL.Musica.Remover(NumMusica)
                    Next
                    If verificaEliminar = False Then
                        countI1()
                        iPlayer_DAL.Musica.RemoverUsandoTag(PlaylistActual, NumMusica)
                    End If
                Else
                    For Each index In indexes
                        verificaEliminar = True
                        countI1()
                        iPlayer_DAL.Musica.RemoverUsandoTag(PlaylistActual, NumMusica)
                    Next
                    If verificaEliminar = False Then
                        countI1()
                        iPlayer_DAL.Musica.RemoverUsandoTag(PlaylistActual, NumMusica)
                    End If
                End If
                CoisasDoEliminar()
            End If

        ElseIf ListView1.SelectedItems.Count > 1 Then
            Dim respostas As DialogResult = System.Windows.Forms.MessageBox.Show("Tem a certeza que deseja eliminar as músicas seleccionadas ?", "Exclui arquivos selecionados", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If respostas = DialogResult.Yes Then
                If PlaylistActual < 7 Then
                    For Each index In indexes
                        If index = indexAReprod Then
                            If verificaReproduzir = True Then
                                AxWindowsMediaPlayer1.Ctlcontrols.stop()
                            End If
                        End If
                        NumMusica = ListView1.Items(index).SubItems(10).Text
                        n = ListView1.Items(index).SubItems(0).Text
                        countM1()
                        iPlayer_DAL.Musica.Remover(NumMusica)
                    Next
                Else

                    For Each index In indexes
                        NumMusica = ListView1.Items(index).SubItems(10).Text
                        n = ListView1.Items(index).SubItems(0).Text
                        countM1()
                        iPlayer_DAL.Musica.RemoverUsandoTag(PlaylistActual, NumMusica)
                    Next
                End If
                aCarregar.Close()
                CoisasDoEliminar()
            End If
        End If
    End Sub

    Private Sub AnularSelecçãoToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnularSelecçãoToolStripMenuItem1.Click
        For i As Integer = 0 To ListView1.Items.Count - 1
            ListView1.Items(i).Selected = False
        Next

        Dim indexes As ListView.SelectedIndexCollection = ListView1.SelectedIndices

        If indexes.Count = 0 Then
            If LabelARepr.Text = "Item seleccionado" Then
                CapaNadaSelec()
            Else
                If verificaReproduzir = False Then
                    CapaNadaEmReprod()
                Else
                    CapaAReprod()
                End If
            End If
            AnularSelecçãoToolStripMenuItem1.Enabled = False
            CopiarToolStripMenuItem.Enabled = False
            EliminarToolStripMenuItem1.Enabled = False
            IndexSelec = -1
        End If
    End Sub

    Private Sub LigarSessãoAleatóriaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LigarSessãoAleatóriaToolStripMenuItem.Click
        If verificaShuffle = False Then
            BtnShuffle.Image = iPlayer.My.Resources.Resources.Gnome_Media_Playlist_Shuffle_32_press
            LigarSessãoAleatóriaToolStripMenuItem1.Text = "Desligar sessão aleatória"
            LigarSessãoAleatóriaToolStripMenuItem.Text = "Desligar sessão aleatória"
            verificaShuffle = True
        ElseIf verificaShuffle = True Then
            BtnShuffle.Image = iPlayer.My.Resources.Resources.Gnome_Media_Playlist_Shuffle_32
            LigarSessãoAleatóriaToolStripMenuItem1.Text = "Ligar sessão aleatória"
            LigarSessãoAleatóriaToolStripMenuItem.Text = "Desligar sessão aleatória"
            verificaShuffle = False
        End If
    End Sub

    Private Sub DesligadoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DesligadoToolStripMenuItem.Click
        BtnRepeat.Image = iPlayer.My.Resources.Resources.Gnome_Media_Playlist_Repeat_32
        RepetirDesligadoToolStripMenuItem1.Checked = True
        DesligadoToolStripMenuItem.Checked = True
        RepetirUmaToolStripMenuItem1.Checked = False
        UmaToolStripMenuItem.Checked = False
        TudoToolStripMenuItem1.Checked = False
        RepetirTudoToolStripMenuItem1.Checked = False
        verificaRepeatOne = False
        verificaRepeatAll = True
    End Sub

    Private Sub TudoToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TudoToolStripMenuItem1.Click
        BtnRepeat.Image = iPlayer.My.Resources.Resources.Gnome_Media_Playlist_Repeat_32_press
        RepetirDesligadoToolStripMenuItem1.Checked = False
        DesligadoToolStripMenuItem.Checked = False
        TudoToolStripMenuItem1.Checked = True
        RepetirTudoToolStripMenuItem1.Checked = True
        UmaToolStripMenuItem.Checked = False
        RepetirUmaToolStripMenuItem1.Checked = False
        verificaRepeatAll = True
        verificaRepeatOne = False
    End Sub

    Private Sub UmaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UmaToolStripMenuItem.Click
        BtnRepeat.Image = iPlayer.My.Resources.Resources.Gnome_Media_Playlist_Repeat_one
        RepetirDesligadoToolStripMenuItem1.Checked = False
        DesligadoToolStripMenuItem.Checked = False
        UmaToolStripMenuItem.Checked = True
        RepetirUmaToolStripMenuItem1.Checked = True
        TudoToolStripMenuItem1.Checked = False
        RepetirTudoToolStripMenuItem1.Checked = False
        verificaRepeatOne = True
        verificaRepeatAll = False
    End Sub

    Sub verificaUrl()
        Dim NovoCaminho, str As String
        str = ListView1.Items(IndexSelec).SubItems(0).Text
        Dim resposta As DialogResult = System.Windows.Forms.MessageBox.Show("Não foi possível utilizar a musica " & "'" & str.Substring(3, str.Length - 3) & "'" & " " & " por não ter sido possível encontrar o ficheiro original. Deseja localizá-lo?", "iPlayer", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resposta = DialogResult.Yes Then
            OpenFileDialog1.InitialDirectory = caminho
            If OpenFileDialog1.ShowDialog = DialogResult.OK Then
                NovoCaminho = OpenFileDialog1.FileName
                iPlayer_DAL.Musica.Alterar(Nothing, Nothing, Nothing, Nothing, Nothing, NumMusica, Nothing, NovoCaminho)
                caminho = NovoCaminho
                caminho = ListView1.Items(index).SubItems(10).Text
            End If
        End If
    End Sub

    Private Sub AxWindowsMediaPlayer1_MediaError(ByVal sender As Object, ByVal e As AxWMPLib._WMPOCXEvents_MediaErrorEvent) Handles AxWindowsMediaPlayer1.MediaError
        If RadioOn = False Then
            If AReprod_CD = False Then
                verificaUrl()
            Else
                AxWindowsMediaPlayer1.Ctlcontrols.stop()
                parar()
            End If
        Else
            System.Windows.Forms.MessageBox.Show("Ocorreu um erro ao contactar o serviço de rádio. Verifique a ligação à Internet, ou volte  a tentar mais tarde.", "iPlayer", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            PararRadio()
        End If
    End Sub

    Private Sub AxWindowsMediaPlayer1_PlayStateChange(ByVal sender As Object, ByVal e As AxWMPLib._WMPOCXEvents_PlayStateChangeEvent) Handles AxWindowsMediaPlayer1.PlayStateChange
        If AxWindowsMediaPlayer1.playState = WMPPlayState.wmppsMediaEnded Then
            verificaIndex = False
            ProximaMusica()

        ElseIf AxWindowsMediaPlayer1.playState = WMPPlayState.wmppsPaused Then
            verificaPlay = True
            BtnPlay.Image = iPlayer.My.Resources.playerpp
            ReproduzirToolStripMenuItem1.Text = "Reproduzir"
            ReproduzirToolStripMenuItem.Text = "Reproduzir"
            TimerDuracao.Stop()
        ElseIf AxWindowsMediaPlayer1.playState = WMPPlayState.wmppsPlaying Then
            If RadioOn = False Then
                If verificAddFile = False Then

                    If naux = PlaylistActual Then
                        verificaReproduzir = True
                        verificaStop = False
                        BtnAnterior.Image = iPlayer.My.Resources.player_rew_32
                        BtnSeguinte.Image = iPlayer.My.Resources.player_fwd_32
                        BtnPlay.Image = iPlayer.My.Resources.player_pause
                        ReproduzirToolStripMenuItem1.Text = "Pausa"
                        ReproduzirToolStripMenuItem.Text = "Pausa"
                        verificaPlay = False
                        ColorirListview()
                        ListView1.Items(indexAReprod).BackColor = Color.LightSkyBlue
                    End If
                    TimerDuracao.Start()
                    Reproduzir()
                End If
            Else
                verificaTRadio = True
                TimerRadio.Start()
            End If
        ElseIf AxWindowsMediaPlayer1.playState = WMPPlayState.wmppsStopped Then
            If RadioOn = False Then
                parar()
            End If
        ElseIf AxWindowsMediaPlayer1.playState = WMPPlayState.wmppsReady Then
            If verificaReproduzir = True Then
                AxWindowsMediaPlayer1.Ctlcontrols.play()
            End If
            If RadioOn = True Then
                AxWindowsMediaPlayer1.Ctlcontrols.play()
            End If
        ElseIf AxWindowsMediaPlayer1.playState = WMPPlayState.wmppsReconnecting Then
            aCarregar.Close()
            System.Windows.Forms.MessageBox.Show("Ocorreu um erro ao contactar o serviço de rádio. Verifique a ligação à Internet, ou volte  a tentar mais tarde.", "iPlayer", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            PararRadio()
        ElseIf AxWindowsMediaPlayer1.playState = WMPPlayState.wmppsBuffering Then
            aCarregar.Show()
            verificaTRadio = False
            aCarregar.Label2.Text = "A estabelecer ligação"
            TimerRadio.Stop()
        End If
        ListView1.Focus()
    End Sub

    'Private Sub ListView1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Leave
    '    InforToolStripMenuItem.Enabled = False
    '    ClassifToolStripMenuItem.Enabled = False
    'End Sub

    Private Sub EliminarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EliminaToolStripMenuItem.Click
        EliminarPlaylist(PlaylistActual)
        Dim n As Integer = PlaylistActual
        PlaylistActual -= 1
        For i As Integer = 0 To Panel3.Controls.Count - 1
            Dim bx As Button
            If TypeOf Panel3.Controls(i) Is Button Then
                bx = DirectCast(Panel3.Controls(i), Button)
                bx.BackColor = Color.Silver
                bx.FlatAppearance.MouseDownBackColor = Color.Silver
                bx.FlatAppearance.MouseOverBackColor = Color.Silver

                If bx.Tag = PlaylistActual Then
                    bx.BackColor = Color.LightBlue
                    bx.FlatAppearance.MouseDownBackColor = Color.LightBlue
                    bx.FlatAppearance.MouseOverBackColor = Color.LightBlue
                    PreencherPlaylist(PlaylistActual)
                    verificaVista()
                    If verificaReproduzir = False Then
                        If ListView1.Items.Count = 0 Then
                            BtnPlay.Image = iPlayer.My.Resources.playerpp_enabled
                            BtnAnterior.Image = iPlayer.My.Resources.player_rew_32_enabled
                            BtnSeguinte.Image = iPlayer.My.Resources.player_fwd_32_enabled
                        Else
                            BtnPlay.Image = iPlayer.My.Resources.playerpp
                        End If
                    Else
                        If ListView1.Items.Count = 0 Then
                            BtnPlay.Image = iPlayer.My.Resources.playerpp_enabled
                            BtnAnterior.Image = iPlayer.My.Resources.player_rew_32_enabled
                            BtnSeguinte.Image = iPlayer.My.Resources.player_fwd_32_enabled
                        Else
                            BtnPlay.Image = iPlayer.My.Resources.playerpp
                        End If
                    End If

                    Exit Sub
                ElseIf bx.Tag = n Then
                    bx.Visible = False
                End If
            End If
        Next
    End Sub

    Private Sub CortarToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CortarToolStripMenuItem1.Click
        copiar()
        EliminarMusica()
    End Sub

    Sub EliminarPlaylist(ByVal tag As Integer)
        iPlayer_DAL.Playlist.Remover(tag)
    End Sub

    Private Sub ColarToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ColarToolStripMenuItem2.Click

        If PictureBoxListaPessoal.Visible = True Then
            PictureBoxListaPessoal.Visible = False
        End If
        For i As Integer = 0 To CopyArray.Length - 1
            For j As Integer = 0 To ListView1.Items.Count - 1
                If CopyArray(i) = ListView1.Items(j).SubItems(10).Text Then
                    Dim n As String = ListView1.Items(j).SubItems(0).Text
                    System.Windows.Forms.MessageBox.Show("Não foi possível adicionar a música" & " " & "'" & n & "'," & " " & "visto que esta já existe na lista de reprodução actual.", "iPlayer", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    iPlayer_DAL.Playlist.CriarRelacao(PlaylistActual, CopyArray(i))
                End If
                If i = CopyArray.Length - 1 Then
                    Exit For
                End If
            Next
        Next
        PreencherPlaylist(PlaylistActual)
        VerificaCopiar = False
        ColarToolStripMenuItem2.Enabled = False
    End Sub

    Private Sub BtnCancelPesq_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BtnCancelPesq.MouseDown
        BtnCancelPesq.Image = iPlayer.My.Resources.cancel1
    End Sub

    Private Sub BtnCancelPesq_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BtnCancelPesq.MouseUp
        BtnCancelPesq.Image = iPlayer.My.Resources.cancel2
        TextBoxPesq.Text = "Pesquisar"
        verificaPesquisa = False
        PreencherPlaylist(PlaylistActual)
    End Sub

    'Private Sub ListView1_ColumnClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles ListView1.ColumnClick
    '    If verificaReproduzir = False Then
    '        ' Determine whether the column is the same as the last column clicked.
    '        If e.Column <> sortColumn Then
    '            ' Set the sort column to the new column.
    '            sortColumn = e.Column
    '            ' Set the sort order to Descending by default.
    '            ListView1.Sorting = SortOrder.Descending
    '        Else
    '            ' Determine what the last sort order was and change it.
    '            If ListView1.Sorting = SortOrder.Ascending Then
    '                ListView1.Sorting = SortOrder.Descending
    '            Else
    '                ListView1.Sorting = SortOrder.Ascending
    '            End If
    '        End If
    '        ' Call the sort method to manually sort.
    '        ListView1.Sort()
    '        ' Set the ListViewItemSorter property to a new ListViewItemComparer
    '        ' object.
    '        ListView1.ListViewItemSorter = New ListViewItemComparer(e.Column, ListView1.Sorting)
    '        ColorirListview()
    '    End If
    'End Sub

    'Class ListViewItemComparer
    '    Implements IComparer
    '    Private col As Integer
    '    Private order As SortOrder

    '    Public Sub New()
    '        col = 0
    '        order = SortOrder.Ascending
    '    End Sub

    '    Public Sub New(ByVal column As Integer, ByVal order As SortOrder)
    '        col = column
    '        Me.order = order
    '    End Sub

    '    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
    '        Dim returnVal As Integer = -1
    '        Try
    '            returnVal = [String].Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)

    '            ' Determine whether the sort order is descending.
    '            If order = SortOrder.Descending Then
    '                ' Invert the value returned by String.Compare.
    '                returnVal *= -1
    '            End If

    '            Return returnVal
    '        Catch ex As Exception
    '        End Try
    '    End Function
    'End Class

    Private Sub PictureBoxCapa_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PictureBoxCapa.DragDrop
        If PlaylistActual <> 2 Then
            Dim capa As String
            If IndexSelec <> -1 Then
                If CapaSemBool = True OrElse TemCapaBool = True Then
                    If (e.Data.GetDataPresent( _
                       DataFormats.FileDrop)) Then
                        Dim files() As String
                        files = e.Data.GetData( _
                           DataFormats.FileDrop)

                        For Each file As String In files
                            file = UCase(file)
                            If file.EndsWith(".GIF") Or _
                               file.EndsWith(".JPG") Or _
                               file.EndsWith(".JPEG") Or _
                               file.EndsWith(".PNG") Or _
                               file.EndsWith(".BMP") Then
                                PictureBoxCapa.Image = New Bitmap(file)
                                '---------------------------
                                If PlaylistActual = naux Then
                                    If verificaReproduzir = True Then
                                        If LabelARepr.Text = "A reproduzir" Then
                                            NumMusica = ListView1.Items(indexAReprod).SubItems(10).Text
                                            DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusica)
                                        End If
                                    Else
                                        NumMusica = ListView1.Items(IndexSelec).SubItems(10).Text
                                        DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusica)
                                    End If

                                Else
                                    If verificaReproduzir = True Then
                                        DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusicaAReprod)
                                    End If
                                End If


                                Try
                                    capa = DataTableSemNome.Rows(0).Item("Capa")
                                Catch ex As Exception
                                    capa = ""
                                End Try


                                If capa <> "" Then
                                    My.Computer.FileSystem.DeleteFile(capa)
                                End If
                                '---------------------------
                                Dim nurl As FileInfo = New FileInfo(file)
                                Dim sd As String = nurl.Extension
                                Dim ext As String = Path.GetFileNameWithoutExtension(file)
                                My.Computer.FileSystem.CopyFile(file, Application.StartupPath & "\" & "Capas\" & ext & NumMusica & sd, True)
                                capa = Application.StartupPath & "\" & "Capas\" & ext & NumMusica & sd

                                iPlayer_DAL.Musica.AlterarCapa(NumMusica, capa)
                                If verificaV = False Then
                                    ImageList1.Images.Clear()
                                    ImageList1.Images.Add(iPlayer.My.Resources.nopCapa)
                                    ListView1.View = View.LargeIcon
                                    For i As Integer = 0 To ListView1.Items.Count - 1
                                        NumMusica = ListView1.Items(i).SubItems(10).Text
                                        DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(NumMusica)
                                        Try
                                            ImageList1.Images.Add(Image.FromFile(DataTableSemNome.Rows(0).Item("Capa")))
                                            ListView1.Items(i).ImageIndex = ImageList1.Images.Count - 1
                                        Catch ex As Exception
                                            ListView1.Items(i).ImageIndex = 0
                                        End Try
                                    Next
                                    ListView1.LargeImageList = ImageList1
                                End If
                            End If
                            System.Threading.Thread.Sleep(2000)
                            Application.DoEvents()
                        Next
                    End If
                    PictureBoxCapa.SizeMode = PictureBoxSizeMode.StretchImage
                    RectangleShape1.Visible = False
                End If
            End If
        End If
    End Sub

    Private Sub PictureBoxCapa_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PictureBoxCapa.DragEnter
        If PlaylistActual <> 2 Then
            'If IndexSelec <> -1 Then
            If CapaSemBool = True OrElse TemCapaBool = True Then

                If (e.Data.GetDataPresent( _
                    DataFormats.FileDrop)) Then
                    '---if this is a file drop---
                    e.Effect = DragDropEffects.All
                End If
                RectangleShape1.Visible = True
            Else
                e.Effect = DragDropEffects.None
                PictureBoxCapa.Cursor = Cursors.No
            End If
            'End If
        End If
    End Sub

    Private Sub PictureBoxCapa_DragLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBoxCapa.DragLeave
        RectangleShape1.Visible = False
    End Sub

    Private Sub PictureBoxCapa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBoxCapa.Click
        If PlaylistActual <> 2 Then
            Dim indexes As ListView.SelectedIndexCollection = ListView1.SelectedIndices
            If LabelARepr.Text = "Item seleccionado" Then
                If indexes.Count <> Nothing Then
                    If indexes.Count = 1 Then
                        If TemCapaBool = True Then
                            JanelaCapa.BackgroundImage = PictureBoxCapa.Image
                            If PlaylistActual = naux Then
                                Dim s As String = ListView1.Items(IndexSelec).SubItems(0).Text
                                JanelaCapa.Text = s.Substring(4, s.Length - 4)
                                JanelaCapa.ShowDialog()
                            End If
                        End If
                    End If
                End If
            ElseIf LabelARepr.Text = "A reproduzir" Then
                If verificaReproduzir = True Then
                    If TemCapaBool = True Then
                        JanelaCapa.BackgroundImage = PictureBoxCapa.Image
                        If PlaylistActual = naux Then
                            Dim s As String = ListView1.Items(IndexSelec).SubItems(0).Text
                            JanelaCapa.Text = s.Substring(4, s.Length - 4)
                            JanelaCapa.ShowDialog()
                        End If
                    End If
                End If
            End If

        End If
    End Sub

    Private Sub PictureBoxCapa_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBoxCapa.MouseEnter
        PictureBoxCapa.Cursor = Cursors.Hand
    End Sub

    Private Sub EcrãCompletoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EcrãCompletoToolStripMenuItem.Click
        If AxWindowsMediaPlayer1.fullScreen = False Then
            AxWindowsMediaPlayer1.fullScreen = True
        End If
    End Sub

    Private Sub PesqInterToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PesqTodosToolStripMenuItem.Click, PesqMusToolStripMenuItem.Click, PesqInterToolStripMenuItem.Click, PesqAlbumToolStripMenuItem.Click
        Dim b As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        opc = b.Tag
        Select Case b.Tag
            Case 1
                If PesqTodosToolStripMenuItem.Checked = False Then
                    PesqTodosToolStripMenuItem.Checked = True
                    PesqMusToolStripMenuItem.Checked = False
                    PesqAlbumToolStripMenuItem.Checked = False
                    PesqInterToolStripMenuItem.Checked = False
                End If
            Case 2
                If PesqInterToolStripMenuItem.Checked = False Then
                    PesqInterToolStripMenuItem.Checked = True
                    PesqMusToolStripMenuItem.Checked = False
                    PesqAlbumToolStripMenuItem.Checked = False
                    PesqTodosToolStripMenuItem.Checked = False
                End If
            Case 3
                If PesqMusToolStripMenuItem.Checked = False Then
                    PesqMusToolStripMenuItem.Checked = True
                    PesqInterToolStripMenuItem.Checked = False
                    PesqAlbumToolStripMenuItem.Checked = False
                    PesqTodosToolStripMenuItem.Checked = False
                End If
            Case 4
                If PesqAlbumToolStripMenuItem.Checked = False Then
                    PesqAlbumToolStripMenuItem.Checked = True
                    PesqMusToolStripMenuItem.Checked = False
                    PesqInterToolStripMenuItem.Checked = False
                    PesqTodosToolStripMenuItem.Checked = False
                End If
        End Select
    End Sub

    Private Sub SairToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SairToolStripMenuItem1.Click
        TimerOpacity.Start()
    End Sub

    Private Sub JanelaPrincipal_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragDrop
        If verificaV = True Then
            If PlaylistActual = 1 Then
                If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                    RectangleShape2.Visible = False
                    Me.Refresh()
                    Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
                    For Each file As String In files
                        Dim nurl As FileInfo

                        nurl = New FileInfo(file)
                        Dim EXT As String = LCase(nurl.Extension)
                        If EXT = ".mp3" OrElse EXT = ".aac" OrElse EXT = ".wma" OrElse EXT = ".m4a" Then
                            Dim musicaExistente As Boolean = False
                            Try

                                For k As Integer = 0 To DataTableSemNome.Rows.Count - 1
                                    If file = DataTableSemNome.Rows(k).Item("Url") Then
                                        musicaExistente = True
                                    End If
                                Next
                            Catch ex As Exception
                            End Try

                            If musicaExistente = True Then '3
                                Dim resposta As DialogResult = System.Windows.Forms.MessageBox.Show("Esta música já existe na Biblioteca. Deseja adicioná-la novamente?", "iPlayer", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                If resposta = DialogResult.Yes Then
                                    AddFile(file)
                                    If PlaylistActual = 1 Then
                                        PreencherPlaylist(1)
                                    End If
                                End If
                            Else
                                AddFile(file)
                                If PlaylistActual = 1 Then
                                    PreencherPlaylist(1)
                                End If
                            End If


                        ElseIf EXT = "" Then
                            'significa que é uma pasta
                            AddFolder(file)
                            If PlaylistActual = 1 Then
                                PreencherPlaylist(PlaylistActual)
                            End If
                        End If
                    Next
                    verificAddFile = True
                End If
            End If
        End If
    End Sub

    Private Sub JanelaPrincipal_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragEnter
        If verificaV = False Then
            e.Effect = DragDropEffects.None
            Me.Cursor = Cursors.No
        Else
            If PlaylistActual <> 1 Then
                e.Effect = DragDropEffects.None
                Me.Cursor = Cursors.No
            Else
                If (e.Data.GetDataPresent( _
                   DataFormats.FileDrop)) Then
                    '---if this is a file drop---
                    Dim files() As String
                    files = e.Data.GetData( _
                       DataFormats.FileDrop)
                    Dim url As FileInfo = New FileInfo(files(0))
                    If url.Extension = "" Then
                        'significa que é uma pasta
                        e.Effect = DragDropEffects.All
                        RectangleShape2.BringToFront()
                        valido = True
                        RectangleShape2.Visible = True
                        If ListView1.Items.Count < 24 Then
                            RectangleShape2.Width = 644
                        Else
                            RectangleShape2.Width = 632
                        End If
                    Else
                        For Each file As String In files
                            file = UCase(file)
                            If file.EndsWith(".MP3") Or _
                               file.EndsWith(".WMA") Or _
                               file.EndsWith(".AAC") Or _
                               file.EndsWith(".M4A") Then
                                e.Effect = DragDropEffects.All
                                RectangleShape2.BringToFront()
                                valido = True
                                RectangleShape2.Visible = True
                                If ListView1.Items.Count < 24 Then
                                    RectangleShape2.Width = 644
                                Else
                                    RectangleShape2.Width = 632
                                End If
                            Else
                                e.Effect = DragDropEffects.None
                                Me.Cursor = Cursors.No
                                Exit Sub
                            End If
                        Next
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub JanelaPrincipal_DragLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DragLeave
        Me.Cursor = Cursors.Default
        RectangleShape2.Visible = False
        If valido = True Then
            valido = False
            Me.Refresh()
        End If
    End Sub

    Private Sub Antena1ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSFToolStripMenuItem.Click, RadioPopularToolStripMenuItem.Click, RenascençaToolStripMenuItem.Click, RádioComercialToolStripMenuItem.Click, RádioClubePortuguêsToolStripMenuItem.Click, OrbitalToolStripMenuItem.Click, NovaEraToolStripMenuItem.Click, MixFMToolStripMenuItem.Click, MegaFMToolStripMenuItem.Click, HorizonteFMToolStripMenuItem.Click, CidadeFMToolStripMenuItem.Click, BestRockFMToolStripMenuItem.Click, Antena3ToolStripMenuItem.Click, Antena2ToolStripMenuItem.Click, Antena1ToolStripMenuItem.Click
        Dim b As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        If RadioOn = True Then
            aCarregar.Show()
            verificaTRadio = False
            aCarregar.Label2.Text = "A estabelecer ligação"
            LabelDuracaoMais.Text = "00:00"
        End If

        Select Case b.Tag
            Case 1
                'Antena 1
                Antena1ToolStripMenuItem.Checked = True
                Antena2ToolStripMenuItem.Checked = False
                Antena3ToolStripMenuItem.Checked = False
                BestRockFMToolStripMenuItem.Checked = False
                CidadeFMToolStripMenuItem.Checked = False
                MegaFMToolStripMenuItem.Checked = False
                HorizonteFMToolStripMenuItem.Checked = False
                MixFMToolStripMenuItem.Checked = False
                NovaEraToolStripMenuItem.Checked = False
                OrbitalToolStripMenuItem.Checked = False
                RádioClubePortuguêsToolStripMenuItem.Checked = False
                RádioComercialToolStripMenuItem.Checked = False
                RadioPopularToolStripMenuItem.Checked = False
                RenascençaToolStripMenuItem.Checked = False
                TSFToolStripMenuItem.Checked = False
                If RadioOn = True Then
                    AxWindowsMediaPlayer1.URL = "mms://rdp.oninet.pt/antena1"
                    LabelNomeMus.Text = "Antena 1"
                End If

            Case 2
                'Antena 2
                Antena1ToolStripMenuItem.Checked = False
                Antena2ToolStripMenuItem.Checked = True
                Antena3ToolStripMenuItem.Checked = False
                BestRockFMToolStripMenuItem.Checked = False
                CidadeFMToolStripMenuItem.Checked = False
                MegaFMToolStripMenuItem.Checked = False
                HorizonteFMToolStripMenuItem.Checked = False
                MixFMToolStripMenuItem.Checked = False
                NovaEraToolStripMenuItem.Checked = False
                OrbitalToolStripMenuItem.Checked = False
                RádioClubePortuguêsToolStripMenuItem.Checked = False
                RádioComercialToolStripMenuItem.Checked = False
                RadioPopularToolStripMenuItem.Checked = False
                RenascençaToolStripMenuItem.Checked = False
                TSFToolStripMenuItem.Checked = False
                If RadioOn = True Then
                    AxWindowsMediaPlayer1.URL = "mms://rdp.oninet.pt/antena2"
                    LabelNomeMus.Text = "Antena 2"
                End If
            Case 3
                'Antena 3
                Antena1ToolStripMenuItem.Checked = False
                Antena2ToolStripMenuItem.Checked = False
                Antena3ToolStripMenuItem.Checked = True
                BestRockFMToolStripMenuItem.Checked = False
                CidadeFMToolStripMenuItem.Checked = False
                MegaFMToolStripMenuItem.Checked = False
                HorizonteFMToolStripMenuItem.Checked = False
                MixFMToolStripMenuItem.Checked = False
                NovaEraToolStripMenuItem.Checked = False
                OrbitalToolStripMenuItem.Checked = False
                RádioClubePortuguêsToolStripMenuItem.Checked = False
                RádioComercialToolStripMenuItem.Checked = False
                RadioPopularToolStripMenuItem.Checked = False
                RenascençaToolStripMenuItem.Checked = False
                TSFToolStripMenuItem.Checked = False
                If RadioOn = True Then
                    AxWindowsMediaPlayer1.URL = "mms://rdp.oninet.pt/antena3"
                    LabelNomeMus.Text = "Antena 3"
                End If
            Case 4
                'Best Rock FM
                Antena1ToolStripMenuItem.Checked = False
                Antena2ToolStripMenuItem.Checked = False
                Antena3ToolStripMenuItem.Checked = False
                BestRockFMToolStripMenuItem.Checked = True
                CidadeFMToolStripMenuItem.Checked = False
                MegaFMToolStripMenuItem.Checked = False
                HorizonteFMToolStripMenuItem.Checked = False
                MixFMToolStripMenuItem.Checked = False
                NovaEraToolStripMenuItem.Checked = False
                OrbitalToolStripMenuItem.Checked = False
                RádioClubePortuguêsToolStripMenuItem.Checked = False
                RádioComercialToolStripMenuItem.Checked = False
                RadioPopularToolStripMenuItem.Checked = False
                RenascençaToolStripMenuItem.Checked = False
                TSFToolStripMenuItem.Checked = False
                If RadioOn = True Then
                    AxWindowsMediaPlayer1.URL = "http://bestrock.clix.pt/asx/estrangeiro/bestrockfm20.asx"
                    LabelNomeMus.Text = "Best Rock FM"
                End If
            Case 5
                'Cidade FM
                Antena1ToolStripMenuItem.Checked = False
                Antena2ToolStripMenuItem.Checked = False
                Antena3ToolStripMenuItem.Checked = False
                BestRockFMToolStripMenuItem.Checked = False
                CidadeFMToolStripMenuItem.Checked = True
                MegaFMToolStripMenuItem.Checked = False
                HorizonteFMToolStripMenuItem.Checked = False
                MixFMToolStripMenuItem.Checked = False
                NovaEraToolStripMenuItem.Checked = False
                OrbitalToolStripMenuItem.Checked = False
                RádioClubePortuguêsToolStripMenuItem.Checked = False
                RádioComercialToolStripMenuItem.Checked = False
                RadioPopularToolStripMenuItem.Checked = False
                RenascençaToolStripMenuItem.Checked = False
                TSFToolStripMenuItem.Checked = False
                If RadioOn = True Then
                    AxWindowsMediaPlayer1.URL = "http://cidadefm.clix.pt/asx/estrangeiro/cidade96.asx"
                    LabelNomeMus.Text = "Cidade FM"
                End If
            Case 6
                ' Mega FM
                Antena1ToolStripMenuItem.Checked = False
                Antena2ToolStripMenuItem.Checked = False
                Antena3ToolStripMenuItem.Checked = False
                BestRockFMToolStripMenuItem.Checked = False
                CidadeFMToolStripMenuItem.Checked = False
                MegaFMToolStripMenuItem.Checked = True
                HorizonteFMToolStripMenuItem.Checked = False
                MixFMToolStripMenuItem.Checked = False
                NovaEraToolStripMenuItem.Checked = False
                OrbitalToolStripMenuItem.Checked = False
                RádioClubePortuguêsToolStripMenuItem.Checked = False
                RádioComercialToolStripMenuItem.Checked = False
                RadioPopularToolStripMenuItem.Checked = False
                RenascençaToolStripMenuItem.Checked = False
                TSFToolStripMenuItem.Checked = False
                If RadioOn = True Then
                    AxWindowsMediaPlayer1.URL = "mms://espalhabrasas.sapo.pt/MegaFM"
                    LabelNomeMus.Text = "Mega FM"
                End If
            Case 7
                'Horizonte FM
                Antena1ToolStripMenuItem.Checked = False
                Antena2ToolStripMenuItem.Checked = False
                Antena3ToolStripMenuItem.Checked = False
                BestRockFMToolStripMenuItem.Checked = False
                CidadeFMToolStripMenuItem.Checked = False
                MegaFMToolStripMenuItem.Checked = False
                HorizonteFMToolStripMenuItem.Checked = True
                MixFMToolStripMenuItem.Checked = False
                NovaEraToolStripMenuItem.Checked = False
                OrbitalToolStripMenuItem.Checked = False
                RádioClubePortuguêsToolStripMenuItem.Checked = False
                RádioComercialToolStripMenuItem.Checked = False
                RadioPopularToolStripMenuItem.Checked = False
                RenascençaToolStripMenuItem.Checked = False
                TSFToolStripMenuItem.Checked = False
                If RadioOn = True Then
                    AxWindowsMediaPlayer1.URL = "mms://stream.radio.com.pt/ROLI-ENC-436"
                    LabelNomeMus.Text = "Horizonte FM"
                End If
            Case 8
                'Mix FM
                Antena1ToolStripMenuItem.Checked = False
                Antena2ToolStripMenuItem.Checked = False
                Antena3ToolStripMenuItem.Checked = False
                BestRockFMToolStripMenuItem.Checked = False
                CidadeFMToolStripMenuItem.Checked = False
                MegaFMToolStripMenuItem.Checked = False
                HorizonteFMToolStripMenuItem.Checked = False
                MixFMToolStripMenuItem.Checked = True
                NovaEraToolStripMenuItem.Checked = False
                OrbitalToolStripMenuItem.Checked = False
                RádioClubePortuguêsToolStripMenuItem.Checked = False
                RádioComercialToolStripMenuItem.Checked = False
                RadioPopularToolStripMenuItem.Checked = False
                RenascençaToolStripMenuItem.Checked = False
                TSFToolStripMenuItem.Checked = False
                If RadioOn = True Then
                    AxWindowsMediaPlayer1.URL = "http://mix.clix.pt/asx/estrangeiro/mix20.asx"
                    LabelNomeMus.Text = "Mix FM"
                End If
            Case 9
                'Nova Era
                Antena1ToolStripMenuItem.Checked = False
                Antena2ToolStripMenuItem.Checked = False
                Antena3ToolStripMenuItem.Checked = False
                BestRockFMToolStripMenuItem.Checked = False
                CidadeFMToolStripMenuItem.Checked = False
                MegaFMToolStripMenuItem.Checked = False
                HorizonteFMToolStripMenuItem.Checked = False
                MixFMToolStripMenuItem.Checked = False
                NovaEraToolStripMenuItem.Checked = True
                OrbitalToolStripMenuItem.Checked = False
                RádioClubePortuguêsToolStripMenuItem.Checked = False
                RádioComercialToolStripMenuItem.Checked = False
                RadioPopularToolStripMenuItem.Checked = False
                RenascençaToolStripMenuItem.Checked = False
                TSFToolStripMenuItem.Checked = False
                If RadioOn = True Then
                    AxWindowsMediaPlayer1.URL = "mms:\\stream.radio.com.pt\roli-enc-478"
                    LabelNomeMus.Text = "Nova Era"
                End If
            Case 10
                'Orbital
                Antena1ToolStripMenuItem.Checked = False
                Antena2ToolStripMenuItem.Checked = False
                Antena3ToolStripMenuItem.Checked = False
                BestRockFMToolStripMenuItem.Checked = False
                CidadeFMToolStripMenuItem.Checked = False
                MegaFMToolStripMenuItem.Checked = False
                HorizonteFMToolStripMenuItem.Checked = False
                MixFMToolStripMenuItem.Checked = False
                NovaEraToolStripMenuItem.Checked = False
                OrbitalToolStripMenuItem.Checked = True
                RádioClubePortuguêsToolStripMenuItem.Checked = False
                RádioComercialToolStripMenuItem.Checked = False
                RadioPopularToolStripMenuItem.Checked = False
                RenascençaToolStripMenuItem.Checked = False
                TSFToolStripMenuItem.Checked = False
                If RadioOn = True Then
                    AxWindowsMediaPlayer1.URL = "http://www.orbital.pt/player/play.asx"
                    LabelNomeMus.Text = "Orbital"
                End If
            Case 11
                'Rádio Comercial
                Antena1ToolStripMenuItem.Checked = False
                Antena2ToolStripMenuItem.Checked = False
                Antena3ToolStripMenuItem.Checked = False
                BestRockFMToolStripMenuItem.Checked = False
                CidadeFMToolStripMenuItem.Checked = False
                MegaFMToolStripMenuItem.Checked = False
                HorizonteFMToolStripMenuItem.Checked = False
                MixFMToolStripMenuItem.Checked = False
                NovaEraToolStripMenuItem.Checked = False
                OrbitalToolStripMenuItem.Checked = False
                RádioClubePortuguêsToolStripMenuItem.Checked = False
                RádioComercialToolStripMenuItem.Checked = True
                RadioPopularToolStripMenuItem.Checked = False
                RenascençaToolStripMenuItem.Checked = False
                TSFToolStripMenuItem.Checked = False
                If RadioOn = True Then
                    AxWindowsMediaPlayer1.URL = "http://radiocomercial.clix.pt/asx/estrangeiro/comercial96.asx"
                    LabelNomeMus.Text = "Rádio Comercial"
                End If
            Case 12
                'Rádio Clube Portugues
                Antena1ToolStripMenuItem.Checked = False
                Antena2ToolStripMenuItem.Checked = False
                Antena3ToolStripMenuItem.Checked = False
                BestRockFMToolStripMenuItem.Checked = False
                CidadeFMToolStripMenuItem.Checked = False
                MegaFMToolStripMenuItem.Checked = False
                HorizonteFMToolStripMenuItem.Checked = False
                MixFMToolStripMenuItem.Checked = False
                NovaEraToolStripMenuItem.Checked = False
                OrbitalToolStripMenuItem.Checked = False
                RádioClubePortuguêsToolStripMenuItem.Checked = True
                RádioComercialToolStripMenuItem.Checked = False
                RadioPopularToolStripMenuItem.Checked = False
                RenascençaToolStripMenuItem.Checked = False
                TSFToolStripMenuItem.Checked = False
                If RadioOn = True Then
                    AxWindowsMediaPlayer1.URL = "http://radioclube.clix.pt/asx/estrangeiro/rcp96.asx"
                    LabelNomeMus.Text = "Rádio Clube Português"
                End If
            Case 14
                'Rádio Popular
                Antena1ToolStripMenuItem.Checked = False
                Antena2ToolStripMenuItem.Checked = False
                Antena3ToolStripMenuItem.Checked = False
                BestRockFMToolStripMenuItem.Checked = False
                CidadeFMToolStripMenuItem.Checked = False
                MegaFMToolStripMenuItem.Checked = False
                HorizonteFMToolStripMenuItem.Checked = False
                MixFMToolStripMenuItem.Checked = False
                NovaEraToolStripMenuItem.Checked = False
                OrbitalToolStripMenuItem.Checked = False
                RádioClubePortuguêsToolStripMenuItem.Checked = False
                RádioComercialToolStripMenuItem.Checked = False
                RadioPopularToolStripMenuItem.Checked = True
                RenascençaToolStripMenuItem.Checked = False
                TSFToolStripMenuItem.Checked = False
                If RadioOn = True Then
                    AxWindowsMediaPlayer1.URL = "http://www.popularfm.com/teste.asx"
                    LabelNomeMus.Text = "Rádio Popular"
                End If
            Case 15
                'Renascença
                Antena1ToolStripMenuItem.Checked = False
                Antena2ToolStripMenuItem.Checked = False
                Antena3ToolStripMenuItem.Checked = False
                BestRockFMToolStripMenuItem.Checked = False
                CidadeFMToolStripMenuItem.Checked = False
                MegaFMToolStripMenuItem.Checked = False
                HorizonteFMToolStripMenuItem.Checked = False
                MixFMToolStripMenuItem.Checked = False
                NovaEraToolStripMenuItem.Checked = False
                OrbitalToolStripMenuItem.Checked = False
                RádioClubePortuguêsToolStripMenuItem.Checked = False
                RádioComercialToolStripMenuItem.Checked = False
                RadioPopularToolStripMenuItem.Checked = False
                RenascençaToolStripMenuItem.Checked = True
                TSFToolStripMenuItem.Checked = False
                If RadioOn = True Then
                    AxWindowsMediaPlayer1.URL = "http://www.rr.pt/live/stream_rr_new.asx"
                    LabelNomeMus.Text = "Renascença"
                End If
            Case 16
                'TSF
                Antena1ToolStripMenuItem.Checked = False
                Antena2ToolStripMenuItem.Checked = False
                Antena3ToolStripMenuItem.Checked = False
                BestRockFMToolStripMenuItem.Checked = False
                CidadeFMToolStripMenuItem.Checked = False
                MegaFMToolStripMenuItem.Checked = False
                HorizonteFMToolStripMenuItem.Checked = False
                MixFMToolStripMenuItem.Checked = False
                NovaEraToolStripMenuItem.Checked = False
                OrbitalToolStripMenuItem.Checked = False
                RádioClubePortuguêsToolStripMenuItem.Checked = False
                RádioComercialToolStripMenuItem.Checked = False
                RadioPopularToolStripMenuItem.Checked = False
                RenascençaToolStripMenuItem.Checked = False
                TSFToolStripMenuItem.Checked = True
                If RadioOn = True Then
                    AxWindowsMediaPlayer1.URL = "http://www.tsf.pt/tsfdirecto.asx"
                    LabelNomeMus.Text = "TSF"
                End If
        End Select
    End Sub

    Public Sub PararRadio()
        LigarToolStripMenuItem.Text = "Ligar"
        ReproduzirToolStripMenuItem.Text = "Reproduzir"
        ReproduzirToolStripMenuItem1.Text = "Reproduzir"
        If ListView1.Items.Count > 0 Then
            BtnPlay.Image = iPlayer.My.Resources.playerpp
            ReproduzirToolStripMenuItem.Enabled = True
            ReproduzirToolStripMenuItem1.Enabled = True
        Else
            BtnPlay.Image = iPlayer.My.Resources.playerpp_enabled
            ReproduzirToolStripMenuItem.Enabled = False
            ReproduzirToolStripMenuItem1.Enabled = False
        End If
        aCarregar.Close()
        TimerRadio.Stop()
        RadioOn = False
        AxWindowsMediaPlayer1.Ctlcontrols.stop()
        PictureBoxRadio.Visible = False
        PictureBoxPrincipal.Visible = True
        verificAddFile = False
        AxWindowsMediaPlayer1.URL = ""
    End Sub

    Private Sub LigarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LigarToolStripMenuItem.Click
        If verificaReproduzir = True Then
            System.Windows.Forms.MessageBox.Show("Tem de parar primeiro a reprodução!", "iPlayer", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            If LigarToolStripMenuItem.Text = "Ligar" Then
                LabelInter.Text = ""
                LabelDuracaoMenos.Text = ""
                RadioOn = True
                aCarregar.Show()
                aCarregar.Label2.Text = "A abrir o url"
                If Antena1ToolStripMenuItem.Checked = True Then
                    AxWindowsMediaPlayer1.URL = "mms://rdp.oninet.pt/antena1"
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    LabelNomeMus.Text = "Antena 1"
                ElseIf Antena2ToolStripMenuItem.Checked = True Then
                    AxWindowsMediaPlayer1.URL = "mms://rdp.oninet.pt/antena2"
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    LabelNomeMus.Text = "Antena 2"
                ElseIf Antena3ToolStripMenuItem.Checked = True Then
                    AxWindowsMediaPlayer1.URL = "mms://rdp.oninet.pt/antena3"
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    LabelNomeMus.Text = "Antena 3"
                ElseIf BestRockFMToolStripMenuItem.Checked = True Then
                    AxWindowsMediaPlayer1.URL = "http://bestrock.clix.pt/asx/estrangeiro/bestrockfm20.asx"
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    LabelNomeMus.Text = "Best Rock FM"
                ElseIf CidadeFMToolStripMenuItem.Checked = True Then
                    AxWindowsMediaPlayer1.URL = "http://cidadefm.clix.pt/asx/estrangeiro/cidade96.asx"
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    LabelNomeMus.Text = "Cidade FM"
                ElseIf HorizonteFMToolStripMenuItem.Checked = True Then
                    AxWindowsMediaPlayer1.URL = "mms://stream.radio.com.pt/ROLI-ENC-436"
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    LabelNomeMus.Text = "Horizonte FM"
                ElseIf MegaFMToolStripMenuItem.Checked = True Then
                    AxWindowsMediaPlayer1.URL = "mms://espalhabrasas.sapo.pt/MegaFM"
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    LabelNomeMus.Text = "Mega FM"
                ElseIf MixFMToolStripMenuItem.Checked = True Then
                    AxWindowsMediaPlayer1.URL = "http://mix.clix.pt/asx/estrangeiro/mix20.asx"
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    LabelNomeMus.Text = "Mix FM"
                ElseIf NovaEraToolStripMenuItem.Checked = True Then
                    AxWindowsMediaPlayer1.URL = "mms:\\stream.radio.com.pt\roli-enc-478"
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    LabelNomeMus.Text = "Nova Era"
                ElseIf OrbitalToolStripMenuItem.Checked = True Then
                    AxWindowsMediaPlayer1.URL = "http://www.orbital.pt/player/play.asx"
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    LabelNomeMus.Text = "Orbital"
                ElseIf RádioComercialToolStripMenuItem.Checked = True Then
                    AxWindowsMediaPlayer1.URL = "http://radiocomercial.clix.pt/asx/estrangeiro/comercial96.asx"
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    LabelNomeMus.Text = "Rádio Comercial"
                ElseIf RádioClubePortuguêsToolStripMenuItem.Checked = True Then
                    AxWindowsMediaPlayer1.URL = "http://radioclube.clix.pt/asx/estrangeiro/rcp96.asx"
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    LabelNomeMus.Text = "Rádio Clube Português"
                ElseIf RadioPopularToolStripMenuItem.Checked = True Then
                    AxWindowsMediaPlayer1.URL = "http://www.popularfm.com/teste.asx"
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    LabelNomeMus.Text = "Rádio Popular"
                ElseIf RenascençaToolStripMenuItem.Checked = True Then
                    AxWindowsMediaPlayer1.URL = "http://www.rr.pt/live/stream_rr_new.asx"
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    LabelNomeMus.Text = "Renascença"
                ElseIf TSFToolStripMenuItem.Checked = True Then
                    AxWindowsMediaPlayer1.URL = "http://www.tsf.pt/tsfdirecto.asx"
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    LabelNomeMus.Text = "TSF"
                End If
            Else
                PararRadio()
            End If
        End If
    End Sub

    Private Sub TimerRadio_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerRadio.Tick
        If AxWindowsMediaPlayer1.Ctlcontrols.currentPositionString <> "" Then
            aCarregar.Close()
            LabelDuracaoMais.Text = AxWindowsMediaPlayer1.Ctlcontrols.currentPositionString
            TrackBar2.Value = 0
            PictureBoxRadio.Visible = True
            PictureBoxPrincipal.Visible = False
            If verificaTRadio = True Then
                BtnPlay.Image = iPlayer.My.Resources.player_stop_48
                LigarToolStripMenuItem.Text = "Desligar"
                ReproduzirToolStripMenuItem1.Text = "Parar"
                ReproduzirToolStripMenuItem.Text = "Parar"
                LabelNomeMus.Visible = True
                verificAddFile = True
                verificaTRadio = False
            End If
        End If
    End Sub

    '-------------------------------------------------------


    Private Sub DeviceEvent(ByVal sender As Object, ByVal e As System.Management.EventArrivedEventArgs) Handles EventWatcher.EventArrived
        If CType(e.NewEvent, ManagementBaseObject)("__Class").ToString = "__InstanceCreationEvent" Then
            Sync = True
            Get_Drive()
        Else
            If CType(e.NewEvent, ManagementBaseObject)("__Class").ToString = "__InstanceDeletionEvent" Then
                Sync = False
                FormSync.Close()
            End If

        End If
    End Sub

    Public Sub StartWatcher()
        _Query = New WqlEventQuery("select * from __InstanceOperationEvent within 1 where TargetInstance ISA 'Win32_LogicalDisk'")
        EventWatcher = New ManagementEventWatcher(_Query)
        Try
            EventWatcher.Start()
        Catch ex As Exception
        End Try
    End Sub


    Sub coisas23()
        If Drives(Drives.Length - 1).RootDirectory.Exists = True Then

            Dim Files As String() = Directory.GetFiles(Drives(Drives.Length - 1).Name)
            Dim File2 As String()
            If Files.Length = 0 Then
                Dim str As String() = Directory.GetDirectories(Drives(Drives.Length - 1).Name)
                For Each i As String In str
                    File2 = Directory.GetFiles(i)
                    For Each File As String In File2

                        Dim url As FileInfo = New FileInfo(File)
                        If url.Extension = ".MP3" OrElse url.Extension = ".AAC" OrElse url.Extension = ".WMA" OrElse url.Extension = ".M4A" Then
                            FormSync.ListBox1.Items.Add(File)
                        ElseIf url.Extension = ".mp3" OrElse url.Extension = ".aac" OrElse url.Extension = ".wma" OrElse url.Extension = ".mp4" Then
                            FormSync.ListBox1.Items.Add(File)
                        End If
                    Next
                Next

                FormSync.ShowDialog()
                Exit Sub
            Else
                For Each File As String In Files
                    Dim url As FileInfo = New FileInfo(File)
                    If url.Extension = ".MP3" OrElse url.Extension = ".AAC" OrElse url.Extension = ".WMA" OrElse url.Extension = ".M4A" Then
                        FormSync.ListBox1.Items.Add(File)
                    ElseIf url.Extension = ".mp3" OrElse url.Extension = ".aac" OrElse url.Extension = ".wma" OrElse url.Extension = ".mp4" Then
                        FormSync.ListBox1.Items.Add(File)
                    End If
                Next
                '--------------------------------------------
                Dim str As String() = Directory.GetDirectories(Drives(Drives.Length - 1).Name)
                For Each i As String In str
                    File2 = Directory.GetFiles(i)
                    For Each File As String In File2

                        Dim url As FileInfo = New FileInfo(File)
                        If url.Extension = ".MP3" OrElse url.Extension = ".AAC" OrElse url.Extension = ".WMA" OrElse url.Extension = ".M4A" Then
                            FormSync.ListBox1.Items.Add(File)
                        ElseIf url.Extension = ".mp3" OrElse url.Extension = ".aac" OrElse url.Extension = ".wma" OrElse url.Extension = ".mp4" Then
                            FormSync.ListBox1.Items.Add(File)
                        End If
                    Next
                Next

                FormSync.ShowDialog()
            End If
        End If
    End Sub

    Public Sub Get_Drive()

        Try
            FormSync.ListBox1.Items.Clear()
        Catch ex As Exception
        End Try

        Dim BaseTime As DateTime = Now

        Drives = System.IO.DriveInfo.GetDrives

        If Drives(Drives.Length - 1).IsReady Then
            If Drives(Drives.Length - 1).DriveFormat = "FAT" Then
                FormSync.Text = "Pen Drive"
                FormSync.LabelPen.Visible = True
                FormSync.LabelMp3.Visible = False
                FormSync.PictureBox1Pen.Visible = True
                FormSync.PictureBox2MP3.Visible = False
                coisas23()
            ElseIf Drives(Drives.Length - 1).DriveFormat = "FAT32" Then
                FormSync.Text = "MP3"
                FormSync.LabelPen.Visible = False
                FormSync.LabelMp3.Visible = True
                FormSync.PictureBox2MP3.Visible = True
                FormSync.PictureBox1Pen.Visible = False
                coisas23()
            Else
                System.Windows.Forms.MessageBox.Show("Ligue um dispositivo.", "iPlayer", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            System.Windows.Forms.MessageBox.Show("Ligue um dispositivo.", "iPlayer", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Sub VisualizarASincronizaçãoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VisualizarASincronizaçãoToolStripMenuItem.Click
        Get_Drive()
    End Sub

End Class

