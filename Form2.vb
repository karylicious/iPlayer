Imports System.Text.RegularExpressions
Imports System.IO

Public Class JanelaInformacoes
    Dim verificaSeguinte As Boolean = False
    Dim verificaAnterior As Boolean = False
    Dim verificaBtn As Boolean = False
    Dim verificaRemoverCapa As Boolean = False
    Dim VerificaAddCapa As Boolean = False
    Dim verifica As Boolean = False
    Dim verificaNumeric_Simb As Boolean = False

    Dim capa As String
    Dim nummusica, indexActual As Integer
    Dim NomeGenero As String
    Dim DataTableSemNome As DataTable

    Private Sub BtnAddImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddImage.Click
        OpenFileDialog1.Filter = "Todas as imagens(*.bmp, *.gif, *.jpg, *.jpeg, *.png)|*.bmp; *.gif; *.jpg; *.jpeg; *.png|Mapa de bits do Windows(*.bmp)|(*.bmp)|Graphics Interchange Format(*.gif)|(*.gif)|JPEG File Interchange Format(*.jpg; *.jpeg)|(*.jpg, *.jpeg)|Portable Network Graphics(*.png)|(*.png)"
        OpenFileDialog1.InitialDirectory = "C:\Users\" & JanelaPrincipal.GetUserName() & "\Pictures\"

        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            PictureBoxCapa.ImageLocation = OpenFileDialog1.FileName
            PictureBox1.ImageLocation = OpenFileDialog1.FileName
            BtnDeleteImage.Enabled = True
            VerificaAddCapa = True
        End If
    End Sub

    Sub BuscarInformacoes(ByVal nummusica As Integer)
        DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(nummusica)
        Me.Text = DataTableSemNome.Rows(0).Item("NomeMusica")
        LabelNomeMus.Text = DataTableSemNome.Rows(0).Item("NomeMusica")

        If Len(LabelNomeMus.Text) > 52 Then
            LabelNomeMus.Text = (Mid(LabelNomeMus.Text, 1, 52))
            LabelNomeMus.Text = LabelNomeMus.Text & "..." & " " & "(" & DataTableSemNome.Rows(0).Item("Duracao") & ")"
        Else
            LabelNomeMus.Text = LabelNomeMus.Text & " " & "(" & DataTableSemNome.Rows(0).Item("Duracao") & ")"
        End If

        LabelTamanho.Text = DataTableSemNome.Rows(0).Item("Tamanho")
        LabelFreq.Text = DataTableSemNome.Rows(0).Item("Frequencia") & " " & "Hz"
        LabelTaxaBits.Text = DataTableSemNome.Rows(0).Item("TaxaBits") & " " & "kbps"
        LabelCopyRight.Text = DataTableSemNome.Rows(0).Item("Copyright")
        LabelCanal.Text = DataTableSemNome.Rows(0).Item("Canal")
        LabelDataAdicao.Text = DataTableSemNome.Rows(0).Item("DataAdicao")
        LabelLocalizacao.Text = DataTableSemNome.Rows(0).Item("Url")
        Try
            PictureBoxCapa.ImageLocation = DataTableSemNome.Rows(0).Item("Capa")
            PictureBox1.ImageLocation = DataTableSemNome.Rows(0).Item("Capa")
            If PictureBox1.ImageLocation = "" Then
                BtnDeleteImage.Enabled = False
            Else
                BtnDeleteImage.Enabled = True
            End If
        Catch ex As Exception
            PictureBoxCapa.ImageLocation = Nothing
            PictureBox1.Image = iPlayer.My.Resources.Resources.m_copy
            BtnDeleteImage.Enabled = False
        End Try


        LabelInterpMus.Text = DataTableSemNome.Rows(0).Item("NomeInterprete")
        LabelAlbumMus.Text = DataTableSemNome.Rows(0).Item("NomeAlbum")
        LabelFormato.Text = iPlayer_DAL.Formato.MostrarNomeFormato(nummusica)
        Try
            LabelReprod.Text = DataTableSemNome.Rows(0).Item("NumReproducao")
            LabelUltReprd.Text = DataTableSemNome.Rows(0).Item("UltimaReproducao")
        Catch
            LabelReprod.Text = 0
            LabelUltReprd.Text = "Indisponível"
        End Try

        TextBoxNomeMus.Text = DataTableSemNome.Rows(0).Item("NomeMusica")
        TextBoxAlbum.Text = DataTableSemNome.Rows(0).Item("NomeAlbum")
        TextBoxInterprete.Text = DataTableSemNome.Rows(0).Item("NomeInterprete")

        If DataTableSemNome.Rows(0).Item("Ano") > 0 Then
            ValidTextAno.Text = DataTableSemNome.Rows(0).Item("Ano")
        Else
            ValidTextAno.Text = ""
        End If

        TextBoxComentarios.Text = DataTableSemNome.Rows(0).Item("Comentarios")
        Try
            RichTextBoxLetra.Text = DataTableSemNome.Rows(0).Item("Letra")
        Catch ex As Exception
        End Try

        Try
            TextBoxGrupo.Text = DataTableSemNome.Rows(0).Item("NumGrupo")
        Catch ex As Exception
        End Try

        NomeGenero = DataTableSemNome.Rows(0).Item("NomeGenero")
        CheckBoxComboBoxGenero.Text = NomeGenero
    End Sub

    Sub clear()
        LabelNomeMus.Text = LabelNomeMus.Text & " " & "(" & DataTableSemNome.Rows(0).Item("Duracao") & ")"
        LabelTamanho.Text = ""
        LabelFreq.Text = ""
        LabelTaxaBits.Text = ""
        LabelCopyRight.Text = ""
        LabelCanal.Text = ""
        LabelDataAdicao.Text = ""
        LabelLocalizacao.Text = ""
        LabelInterpMus.Text = ""
        LabelAlbumMus.Text = ""
        LabelFormato.Text = ""
        LabelReprod.Text = ""
        LabelUltReprd.Text = ""
        TextBoxNomeMus.Text = ""
        TextBoxAlbum.Text = ""
        TextBoxInterprete.Text = ""
        ValidTextAno.Text = ""
        TextBoxComentarios.Text = ""
        RichTextBoxLetra.Text = ""
        TextBoxGrupo.Text = ""
        CheckBoxComboBoxGenero.Text = ""
    End Sub

    Sub GuardarInformacoes()

        If verificaRemoverCapa = True Then
            JanelaPrincipal.PictureBoxCapa.Image = iPlayer.My.Resources.sem_capa
            DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(nummusica)
            My.Computer.FileSystem.DeleteFile(DataTableSemNome.Rows(0).Item("Capa"))
            capa = ""
            iPlayer_DAL.Musica.AlterarCapa(nummusica, capa)

        ElseIf VerificaAddCapa = True Then
            JanelaPrincipal.PictureBoxCapa.ImageLocation = PictureBoxCapa.ImageLocation
            Dim nurl As FileInfo = New FileInfo(OpenFileDialog1.FileName)
            Dim sd As String = nurl.Extension
            Dim ext As String = Path.GetFileNameWithoutExtension(OpenFileDialog1.FileName)
            My.Computer.FileSystem.CopyFile(OpenFileDialog1.FileName, Application.StartupPath & "\" & "Capas\" & ext & nummusica & sd, True)
            capa = Application.StartupPath & "\" & "Capas\" & ext & nummusica & sd
            iPlayer_DAL.Musica.AlterarCapa(nummusica, capa)
        End If

        JanelaPrincipal.PictureBoxCapa.ImageLocation = capa
        If ValidTextAno.Text = "" Then
            ValidTextAno.Text = 0
        End If

        iPlayer_DAL.Musica.Alterar(TextBoxNomeMus.Text, CInt(ValidTextAno.Text), TextBoxComentarios.Text, RichTextBoxLetra.Text, nummusica, Nothing)
        iPlayer_DAL.Interprete.Alterar(nummusica, TextBoxInterprete.Text, TextBoxGrupo.Text)
        iPlayer_DAL.Album.Alterar(nummusica, TextBoxAlbum.Text)
        NomeGenero = CheckBoxComboBoxGenero.Text
        For i As Integer = 0 To NomeGenero.Length - 1

            Dim b As String = NomeGenero.Substring(i, 1)
            If IsNumeric(NomeGenero.Substring(i, 1)) Then
                System.Windows.Forms.MessageBox.Show("Não foi possível alterar o Género visto que não pode conter números!", "iPlayer", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                verificaNumeric_Simb = True
                Exit Sub
            End If
        Next



        Dim arrayAux() As Char = {"\", "|", "!", "#", "@", "£", "$", "§", "%", "€", "/", "{", "[", "(", "]", ")", "=", "}", "?", "»", "«", "'", "*", "+", "´", "`", "~", "^", "-", ".", ":", ";", "<", ">", ","}
        For i As Integer = 0 To NomeGenero.Length - 1

            Dim b As String = NomeGenero.Substring(i, 1)
            For l As Integer = 0 To arrayAux.Length - 1
                If arrayAux(l) = b Then
                    System.Windows.Forms.MessageBox.Show("Não foi possível alterar o Género visto que não pode conter simbolos! Exepto &", "iPlayer", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    verificaNumeric_Simb = True
                    Exit Sub
                End If
            Next
        Next

        iPlayer_DAL.Genero.Alterar(nummusica, NomeGenero)

    End Sub

    Private Sub JanelaInformacoes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        TabControlInfomacoes.SelectTab(0)
        Dim indexes As ListView.SelectedIndexCollection = JanelaPrincipal.ListView1.SelectedIndices
        Dim numItems As Integer = JanelaPrincipal.ListView1.Items.Count

        Dim index As Integer

        If JanelaPrincipal.ListView1.Items.Count > 1 Then
            BtnSeguinte.Visible = True
            BtnAnterior.Visible = True

            For Each index In indexes

                If index = 0 Then
                    BtnSeguinte.Enabled = True
                    BtnAnterior.Enabled = False
                ElseIf index = JanelaPrincipal.ListView1.Items.Count - 1 Then

                    BtnAnterior.Enabled = True
                    BtnSeguinte.Enabled = False

                Else

                    BtnAnterior.Enabled = True
                    BtnSeguinte.Enabled = True
                End If


                indexActual = index
                nummusica = JanelaPrincipal.ListView1.Items(index).SubItems(10).Text
            Next
        Else
            BtnSeguinte.Visible = False
            BtnAnterior.Visible = False
            nummusica = JanelaPrincipal.ListView1.Items(0).SubItems(10).Text
        End If
        verifica = False
        BuscarInformacoes(nummusica)
        LabelNumero.Text = indexActual + 1
        Try
            capa = DataTableSemNome.Rows(0).Item("Capa")
            BtnDeleteImage.Enabled = True
        Catch ex As Exception
            BtnDeleteImage.Enabled = False
        End Try
    End Sub

    Sub coisas()
        GuardarInformacoes()
        JanelaPrincipal.ListView1.Items(indexActual).SubItems(0).Text = indexActual + 1 & Space(5) & TextBoxNomeMus.Text
        JanelaPrincipal.ListView1.Items(indexActual).SubItems(2).Text = TextBoxInterprete.Text
        JanelaPrincipal.ListView1.Items(indexActual).SubItems(3).Text = TextBoxAlbum.Text
        If ValidTextAno.Text = 0 Then
            JanelaPrincipal.ListView1.Items(indexActual).SubItems(4).Text = ""
        Else
            JanelaPrincipal.ListView1.Items(indexActual).SubItems(4).Text = ValidTextAno.Text
        End If

        If verificaNumeric_Simb = False Then
            JanelaPrincipal.ListView1.Items(indexActual).SubItems(6).Text = NomeGenero
        End If
        JanelaPrincipal.ImageList1.Images.Clear()
        JanelaPrincipal.ImageList1.Images.Add(iPlayer.My.Resources.nopCapa)
        If JanelaPrincipal.verificaV = False Then
            For i As Integer = 0 To JanelaPrincipal.ListView1.Items.Count - 1
                nummusica = JanelaPrincipal.ListView1.Items(i).SubItems(10).Text
                DataTableSemNome = iPlayer_DAL.Musica.MostrarUsandoNumMusica(nummusica)
                Try
                    JanelaPrincipal.ImageList1.Images.Add(Image.FromFile(DataTableSemNome.Rows(0).Item("Capa")))
                    JanelaPrincipal.ListView1.Items(i).ImageIndex = JanelaPrincipal.ImageList1.Images.Count - 1
                Catch ex As Exception
                    JanelaPrincipal.ListView1.Items(i).ImageIndex = 0
                End Try
            Next
            JanelaPrincipal.ListView1.LargeImageList = JanelaPrincipal.ImageList1
        End If
        verificaRemoverCapa = False
        clear()
        Me.Close()
    End Sub

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click

        If TextBoxNomeMus.Text = "" Then
            System.Windows.Forms.MessageBox.Show("Não pode deixar o nome da música em branco!", "iPlayer", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else

            If ValidTextAno.Text <> "" Then
                Dim s As String = ValidTextAno.Text
                If Len(ValidTextAno.Text) < 4 Then
                    System.Windows.Forms.MessageBox.Show("Introduza um ano válido!", "iPlayer", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                Else
                    For i As Integer = 0 To s.Length - 1
                        Dim j As String = s.Substring(i, 1)
                        If s.Substring(i, 1) = "." OrElse s.Substring(i, 1) = "," Then
                            System.Windows.Forms.MessageBox.Show("Introduza um ano válido!", "iPlayer", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If
                    Next

                    If ValidTextAno.Text > Now.Year Then
                        System.Windows.Forms.MessageBox.Show("Introduza um ano válido!", "iPlayer", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    Else
                        If s.Substring(1, s.Length - 1) = 0 Then
                            System.Windows.Forms.MessageBox.Show("Introduza um ano válido!", "iPlayer", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        Else
                            coisas()
                        End If
                    End If
                End If
            Else
                coisas()
            End If
        End If

    End Sub

    Private Sub BtnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancelar.Click
        Me.Close()
        clear()
    End Sub

    Private Sub BtnAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAnterior.Click
        indexActual -= 1
        LabelNumero.Text = indexActual + 1
        If indexActual = 0 Then
            BtnSeguinte.Enabled = True
            BtnAnterior.Enabled = False

            nummusica = JanelaPrincipal.ListView1.Items(indexActual).SubItems(10).Text
            Me.Text = JanelaPrincipal.ListView1.Items(indexActual).SubItems(0).Text
        Else
            BtnSeguinte.Enabled = True
            BtnAnterior.Enabled = True
            nummusica = JanelaPrincipal.ListView1.Items(indexActual).SubItems(10).Text
            Me.Text = JanelaPrincipal.ListView1.Items(indexActual).SubItems(0).Text
        End If
        verificaBtn = False
        BuscarInformacoes(nummusica)
    End Sub

    Private Sub BtnSeguinte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSeguinte.Click
        indexActual += 1
        LabelNumero.Text = indexActual + 1
        If indexActual = JanelaPrincipal.ListView1.Items.Count - 1 Then
            BtnSeguinte.Enabled = False
            BtnAnterior.Enabled = True
            nummusica = JanelaPrincipal.ListView1.Items(indexActual).SubItems(10).Text
            Me.Text = JanelaPrincipal.ListView1.Items(indexActual).SubItems(0).Text
        Else
            BtnSeguinte.Enabled = True
            BtnAnterior.Enabled = True
            nummusica = JanelaPrincipal.ListView1.Items(indexActual).SubItems(10).Text
            Me.Text = JanelaPrincipal.ListView1.Items(indexActual).SubItems(0).Text
        End If
        verificaBtn = True
        BuscarInformacoes(nummusica)
    End Sub

    Private Sub BtnDeleteImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDeleteImage.Click
        verificaRemoverCapa = True
        PictureBox1.Image = iPlayer.My.Resources.m_copy
        PictureBoxCapa.ImageLocation = ""
        BtnDeleteImage.Enabled = False
    End Sub
End Class