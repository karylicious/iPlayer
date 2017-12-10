<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JanelaInformacoes
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim CheckBoxProperties1 As PresentationControls.CheckBoxProperties = New PresentationControls.CheckBoxProperties
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(JanelaInformacoes))
        Me.BtnOK = New System.Windows.Forms.Button
        Me.TabControlInfomacoes = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.LabelLocalizacao = New System.Windows.Forms.Label
        Me.LabelUltReprd = New System.Windows.Forms.Label
        Me.LabelReprod = New System.Windows.Forms.Label
        Me.LabelDataAdicao = New System.Windows.Forms.Label
        Me.LabelCanal = New System.Windows.Forms.Label
        Me.LabelTaxaBits = New System.Windows.Forms.Label
        Me.LabelCopyRight = New System.Windows.Forms.Label
        Me.LabelFreq = New System.Windows.Forms.Label
        Me.LabelTamanho = New System.Windows.Forms.Label
        Me.LabelFormato = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.LabelInterpMus = New System.Windows.Forms.Label
        Me.LabelAlbumMus = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.LabelNomeMus = New System.Windows.Forms.Label
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer
        Me.LineShape2 = New Microsoft.VisualBasic.PowerPacks.LineShape
        Me.LineShape1 = New Microsoft.VisualBasic.PowerPacks.LineShape
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.ValidTextAno = New ValidText.ValidText
        Me.CheckBoxComboBoxGenero = New PresentationControls.CheckBoxComboBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.TextBoxComentarios = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.TextBoxGrupo = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.TextBoxAlbum = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.TextBoxInterprete = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.TextBoxNomeMus = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.BtnDeleteImage = New System.Windows.Forms.Button
        Me.BtnAddImage = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.PictureBoxCapa = New System.Windows.Forms.PictureBox
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.RichTextBoxLetra = New System.Windows.Forms.RichTextBox
        Me.BtnCancelar = New System.Windows.Forms.Button
        Me.BtnAnterior = New System.Windows.Forms.Button
        Me.BtnSeguinte = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.Label1 = New System.Windows.Forms.Label
        Me.LabelNumero = New System.Windows.Forms.Label
        Me.TabControlInfomacoes.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBoxCapa, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnOK
        '
        Me.BtnOK.Location = New System.Drawing.Point(379, 474)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(99, 24)
        Me.BtnOK.TabIndex = 19
        Me.BtnOK.Text = "OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'TabControlInfomacoes
        '
        Me.TabControlInfomacoes.Controls.Add(Me.TabPage1)
        Me.TabControlInfomacoes.Controls.Add(Me.TabPage2)
        Me.TabControlInfomacoes.Controls.Add(Me.TabPage3)
        Me.TabControlInfomacoes.Controls.Add(Me.TabPage4)
        Me.TabControlInfomacoes.Location = New System.Drawing.Point(7, 3)
        Me.TabControlInfomacoes.Name = "TabControlInfomacoes"
        Me.TabControlInfomacoes.SelectedIndex = 0
        Me.TabControlInfomacoes.Size = New System.Drawing.Size(576, 458)
        Me.TabControlInfomacoes.TabIndex = 15
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.LabelNumero)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.LabelLocalizacao)
        Me.TabPage1.Controls.Add(Me.LabelUltReprd)
        Me.TabPage1.Controls.Add(Me.LabelReprod)
        Me.TabPage1.Controls.Add(Me.LabelDataAdicao)
        Me.TabPage1.Controls.Add(Me.LabelCanal)
        Me.TabPage1.Controls.Add(Me.LabelTaxaBits)
        Me.TabPage1.Controls.Add(Me.LabelCopyRight)
        Me.TabPage1.Controls.Add(Me.LabelFreq)
        Me.TabPage1.Controls.Add(Me.LabelTamanho)
        Me.TabPage1.Controls.Add(Me.LabelFormato)
        Me.TabPage1.Controls.Add(Me.Label26)
        Me.TabPage1.Controls.Add(Me.Label25)
        Me.TabPage1.Controls.Add(Me.Label24)
        Me.TabPage1.Controls.Add(Me.Label21)
        Me.TabPage1.Controls.Add(Me.Label20)
        Me.TabPage1.Controls.Add(Me.LabelInterpMus)
        Me.TabPage1.Controls.Add(Me.LabelAlbumMus)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.PictureBox1)
        Me.TabPage1.Controls.Add(Me.LabelNomeMus)
        Me.TabPage1.Controls.Add(Me.ShapeContainer1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(568, 432)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Sumário"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'LabelLocalizacao
        '
        Me.LabelLocalizacao.Location = New System.Drawing.Point(52, 398)
        Me.LabelLocalizacao.Name = "LabelLocalizacao"
        Me.LabelLocalizacao.Size = New System.Drawing.Size(497, 13)
        Me.LabelLocalizacao.TabIndex = 29
        '
        'LabelUltReprd
        '
        Me.LabelUltReprd.AutoSize = True
        Me.LabelUltReprd.Location = New System.Drawing.Point(122, 369)
        Me.LabelUltReprd.Name = "LabelUltReprd"
        Me.LabelUltReprd.Size = New System.Drawing.Size(65, 13)
        Me.LabelUltReprd.TabIndex = 27
        Me.LabelUltReprd.Text = "Indisponível"
        '
        'LabelReprod
        '
        Me.LabelReprod.AutoSize = True
        Me.LabelReprod.Location = New System.Drawing.Point(122, 348)
        Me.LabelReprod.Name = "LabelReprod"
        Me.LabelReprod.Size = New System.Drawing.Size(0, 13)
        Me.LabelReprod.TabIndex = 26
        '
        'LabelDataAdicao
        '
        Me.LabelDataAdicao.AutoSize = True
        Me.LabelDataAdicao.Location = New System.Drawing.Point(122, 327)
        Me.LabelDataAdicao.Name = "LabelDataAdicao"
        Me.LabelDataAdicao.Size = New System.Drawing.Size(0, 13)
        Me.LabelDataAdicao.TabIndex = 25
        '
        'LabelCanal
        '
        Me.LabelCanal.AutoSize = True
        Me.LabelCanal.Location = New System.Drawing.Point(122, 306)
        Me.LabelCanal.Name = "LabelCanal"
        Me.LabelCanal.Size = New System.Drawing.Size(0, 13)
        Me.LabelCanal.TabIndex = 24
        '
        'LabelTaxaBits
        '
        Me.LabelTaxaBits.AutoSize = True
        Me.LabelTaxaBits.Location = New System.Drawing.Point(122, 264)
        Me.LabelTaxaBits.Name = "LabelTaxaBits"
        Me.LabelTaxaBits.Size = New System.Drawing.Size(0, 13)
        Me.LabelTaxaBits.TabIndex = 23
        '
        'LabelCopyRight
        '
        Me.LabelCopyRight.AutoSize = True
        Me.LabelCopyRight.Location = New System.Drawing.Point(122, 285)
        Me.LabelCopyRight.Name = "LabelCopyRight"
        Me.LabelCopyRight.Size = New System.Drawing.Size(0, 13)
        Me.LabelCopyRight.TabIndex = 22
        '
        'LabelFreq
        '
        Me.LabelFreq.AutoSize = True
        Me.LabelFreq.Location = New System.Drawing.Point(122, 243)
        Me.LabelFreq.Name = "LabelFreq"
        Me.LabelFreq.Size = New System.Drawing.Size(0, 13)
        Me.LabelFreq.TabIndex = 21
        '
        'LabelTamanho
        '
        Me.LabelTamanho.AutoSize = True
        Me.LabelTamanho.Location = New System.Drawing.Point(122, 222)
        Me.LabelTamanho.Name = "LabelTamanho"
        Me.LabelTamanho.Size = New System.Drawing.Size(0, 13)
        Me.LabelTamanho.TabIndex = 20
        '
        'LabelFormato
        '
        Me.LabelFormato.AutoSize = True
        Me.LabelFormato.Location = New System.Drawing.Point(122, 201)
        Me.LabelFormato.Name = "LabelFormato"
        Me.LabelFormato.Size = New System.Drawing.Size(0, 13)
        Me.LabelFormato.TabIndex = 19
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(23, 327)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(94, 13)
        Me.Label26.TabIndex = 18
        Me.Label26.Text = "Data de adição:"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(45, 243)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(72, 13)
        Me.Label25.TabIndex = 17
        Me.Label25.Text = "Frequência:"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(52, 285)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(65, 13)
        Me.Label24.TabIndex = 16
        Me.Label24.Text = "Copyright:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(76, 306)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(41, 13)
        Me.Label21.TabIndex = 15
        Me.Label21.Text = "Canal:"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(38, 264)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(79, 13)
        Me.Label20.TabIndex = 14
        Me.Label20.Text = "Taxa de bits:"
        '
        'LabelInterpMus
        '
        Me.LabelInterpMus.Location = New System.Drawing.Point(183, 69)
        Me.LabelInterpMus.Name = "LabelInterpMus"
        Me.LabelInterpMus.Size = New System.Drawing.Size(366, 13)
        Me.LabelInterpMus.TabIndex = 13
        Me.LabelInterpMus.Text = "interprte"
        '
        'LabelAlbumMus
        '
        Me.LabelAlbumMus.Location = New System.Drawing.Point(183, 92)
        Me.LabelAlbumMus.Name = "LabelAlbumMus"
        Me.LabelAlbumMus.Size = New System.Drawing.Size(366, 13)
        Me.LabelAlbumMus.TabIndex = 12
        Me.LabelAlbumMus.Text = "album"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(26, 369)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(91, 13)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Última reprod.:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(33, 348)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(84, 13)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Reproduções:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(54, 222)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Tamanho:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(59, 201)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Formato:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(26, 397)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Em:"
        '
        'PictureBox1
        '
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Image = Global.iPlayer.My.Resources.Resources.m_copy
        Me.PictureBox1.Location = New System.Drawing.Point(51, 45)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(113, 105)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'LabelNomeMus
        '
        Me.LabelNomeMus.Location = New System.Drawing.Point(183, 45)
        Me.LabelNomeMus.Name = "LabelNomeMus"
        Me.LabelNomeMus.Size = New System.Drawing.Size(346, 13)
        Me.LabelNomeMus.TabIndex = 0
        Me.LabelNomeMus.Text = "Nome da música "
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(3, 3)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.LineShape2, Me.LineShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(562, 426)
        Me.ShapeContainer1.TabIndex = 11
        Me.ShapeContainer1.TabStop = False
        '
        'LineShape2
        '
        Me.LineShape2.BorderColor = System.Drawing.Color.DarkGray
        Me.LineShape2.Name = "LineShape2"
        Me.LineShape2.X1 = 11
        Me.LineShape2.X2 = 527
        Me.LineShape2.Y1 = 391
        Me.LineShape2.Y2 = 391
        '
        'LineShape1
        '
        Me.LineShape1.BorderColor = System.Drawing.Color.DarkGray
        Me.LineShape1.Name = "LineShape1"
        Me.LineShape1.X1 = 9
        Me.LineShape1.X2 = 525
        Me.LineShape1.Y1 = 169
        Me.LineShape1.Y2 = 169
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.ValidTextAno)
        Me.TabPage2.Controls.Add(Me.CheckBoxComboBoxGenero)
        Me.TabPage2.Controls.Add(Me.Label23)
        Me.TabPage2.Controls.Add(Me.TextBoxComentarios)
        Me.TabPage2.Controls.Add(Me.Label22)
        Me.TabPage2.Controls.Add(Me.TextBoxGrupo)
        Me.TabPage2.Controls.Add(Me.Label16)
        Me.TabPage2.Controls.Add(Me.TextBoxAlbum)
        Me.TabPage2.Controls.Add(Me.Label13)
        Me.TabPage2.Controls.Add(Me.Label12)
        Me.TabPage2.Controls.Add(Me.TextBoxInterprete)
        Me.TabPage2.Controls.Add(Me.Label11)
        Me.TabPage2.Controls.Add(Me.TextBoxNomeMus)
        Me.TabPage2.Controls.Add(Me.Label10)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(568, 432)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Informação"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'ValidTextAno
        '
        Me.ValidTextAno.FieldReference = Nothing
        Me.ValidTextAno.Location = New System.Drawing.Point(424, 367)
        Me.ValidTextAno.MaskEdit = ""
        Me.ValidTextAno.MaxLength = 4
        Me.ValidTextAno.MessageLanguage = ValidText.ValidText.MessageLanguages.English
        Me.ValidTextAno.Multiline = True
        Me.ValidTextAno.Name = "ValidTextAno"
        Me.ValidTextAno.RegExPattern = ValidText.ValidText.RegularExpressionModes.Custom
        Me.ValidTextAno.Required = False
        Me.ValidTextAno.ShowErrorIcon = False
        Me.ValidTextAno.Size = New System.Drawing.Size(128, 23)
        Me.ValidTextAno.TabIndex = 37
        Me.ValidTextAno.ValidationMode = ValidText.ValidText.ValidationModes.Numbers
        Me.ValidTextAno.ValidText = ""
        '
        'CheckBoxComboBoxGenero
        '
        CheckBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CheckBoxComboBoxGenero.CheckBoxProperties = CheckBoxProperties1
        Me.CheckBoxComboBoxGenero.DisplayMemberSingleItem = ""
        Me.CheckBoxComboBoxGenero.FormattingEnabled = True
        Me.CheckBoxComboBoxGenero.Items.AddRange(New Object() {"Anime", "Ballad ", "Banda Sonora", "Batida", "Blue", "Cabo Zouk", "Caribbean Soul", "Classical", "Dance", "Electronic", "Épocas festivas", "Folk", "Hip Hop", "House", "Inclassificável", "Industrial", "Jazz", "Jogo", "Kizomba", "Kuduro", "Latin", "Ligeira", "Música Infantil", "New Age", "Other", "Pop", "Rap", "R&B", "Reggae", "Religiosa", "Rock", "Techno", "Trace", "Vocal", "World", "Zouk"})
        Me.CheckBoxComboBoxGenero.Location = New System.Drawing.Point(16, 367)
        Me.CheckBoxComboBoxGenero.Name = "CheckBoxComboBoxGenero"
        Me.CheckBoxComboBoxGenero.Size = New System.Drawing.Size(389, 21)
        Me.CheckBoxComboBoxGenero.TabIndex = 36
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(17, 351)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(48, 13)
        Me.Label23.TabIndex = 31
        Me.Label23.Text = "Género"
        '
        'TextBoxComentarios
        '
        Me.TextBoxComentarios.Location = New System.Drawing.Point(16, 248)
        Me.TextBoxComentarios.Multiline = True
        Me.TextBoxComentarios.Name = "TextBoxComentarios"
        Me.TextBoxComentarios.Size = New System.Drawing.Size(536, 66)
        Me.TextBoxComentarios.TabIndex = 30
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(17, 232)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(79, 13)
        Me.Label22.TabIndex = 29
        Me.Label22.Text = "Comentários"
        '
        'TextBoxGrupo
        '
        Me.TextBoxGrupo.Location = New System.Drawing.Point(16, 173)
        Me.TextBoxGrupo.Multiline = True
        Me.TextBoxGrupo.Name = "TextBoxGrupo"
        Me.TextBoxGrupo.Size = New System.Drawing.Size(536, 23)
        Me.TextBoxGrupo.TabIndex = 18
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(17, 157)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(41, 13)
        Me.Label16.TabIndex = 17
        Me.Label16.Text = "Grupo"
        '
        'TextBoxAlbum
        '
        Me.TextBoxAlbum.Location = New System.Drawing.Point(16, 124)
        Me.TextBoxAlbum.Multiline = True
        Me.TextBoxAlbum.Name = "TextBoxAlbum"
        Me.TextBoxAlbum.Size = New System.Drawing.Size(536, 23)
        Me.TextBoxAlbum.TabIndex = 12
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(17, 108)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(43, 13)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "Álbum" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(427, 351)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(29, 13)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "Ano"
        '
        'TextBoxInterprete
        '
        Me.TextBoxInterprete.Location = New System.Drawing.Point(16, 76)
        Me.TextBoxInterprete.Multiline = True
        Me.TextBoxInterprete.Name = "TextBoxInterprete"
        Me.TextBoxInterprete.Size = New System.Drawing.Size(536, 23)
        Me.TextBoxInterprete.TabIndex = 8
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(17, 60)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(67, 13)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "Intérprete"
        '
        'TextBoxNomeMus
        '
        Me.TextBoxNomeMus.Location = New System.Drawing.Point(16, 29)
        Me.TextBoxNomeMus.Multiline = True
        Me.TextBoxNomeMus.Name = "TextBoxNomeMus"
        Me.TextBoxNomeMus.Size = New System.Drawing.Size(536, 23)
        Me.TextBoxNomeMus.TabIndex = 6
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(17, 14)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(39, 13)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "Nome"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.BtnDeleteImage)
        Me.TabPage3.Controls.Add(Me.BtnAddImage)
        Me.TabPage3.Controls.Add(Me.Panel1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(568, 432)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Capa"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'BtnDeleteImage
        '
        Me.BtnDeleteImage.Enabled = False
        Me.BtnDeleteImage.Location = New System.Drawing.Point(179, 325)
        Me.BtnDeleteImage.Name = "BtnDeleteImage"
        Me.BtnDeleteImage.Size = New System.Drawing.Size(83, 24)
        Me.BtnDeleteImage.TabIndex = 16
        Me.BtnDeleteImage.Text = "Eliminar"
        Me.BtnDeleteImage.UseVisualStyleBackColor = True
        '
        'BtnAddImage
        '
        Me.BtnAddImage.Location = New System.Drawing.Point(90, 325)
        Me.BtnAddImage.Name = "BtnAddImage"
        Me.BtnAddImage.Size = New System.Drawing.Size(83, 24)
        Me.BtnAddImage.TabIndex = 15
        Me.BtnAddImage.Text = "Adicionar"
        Me.BtnAddImage.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.PictureBoxCapa)
        Me.Panel1.Location = New System.Drawing.Point(81, 56)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(379, 225)
        Me.Panel1.TabIndex = 17
        '
        'PictureBoxCapa
        '
        Me.PictureBoxCapa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBoxCapa.Location = New System.Drawing.Point(77, 31)
        Me.PictureBoxCapa.Name = "PictureBoxCapa"
        Me.PictureBoxCapa.Size = New System.Drawing.Size(203, 163)
        Me.PictureBoxCapa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxCapa.TabIndex = 1
        Me.PictureBoxCapa.TabStop = False
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.RichTextBoxLetra)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(568, 432)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Letra"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'RichTextBoxLetra
        '
        Me.RichTextBoxLetra.Location = New System.Drawing.Point(32, 22)
        Me.RichTextBoxLetra.Name = "RichTextBoxLetra"
        Me.RichTextBoxLetra.Size = New System.Drawing.Size(504, 387)
        Me.RichTextBoxLetra.TabIndex = 1
        Me.RichTextBoxLetra.Text = ""
        '
        'BtnCancelar
        '
        Me.BtnCancelar.Location = New System.Drawing.Point(484, 474)
        Me.BtnCancelar.Name = "BtnCancelar"
        Me.BtnCancelar.Size = New System.Drawing.Size(99, 24)
        Me.BtnCancelar.TabIndex = 18
        Me.BtnCancelar.Text = "Cancelar"
        Me.BtnCancelar.UseVisualStyleBackColor = True
        '
        'BtnAnterior
        '
        Me.BtnAnterior.Location = New System.Drawing.Point(7, 474)
        Me.BtnAnterior.Name = "BtnAnterior"
        Me.BtnAnterior.Size = New System.Drawing.Size(99, 24)
        Me.BtnAnterior.TabIndex = 16
        Me.BtnAnterior.Text = "Anterior"
        Me.BtnAnterior.UseVisualStyleBackColor = True
        '
        'BtnSeguinte
        '
        Me.BtnSeguinte.Location = New System.Drawing.Point(112, 474)
        Me.BtnSeguinte.Name = "BtnSeguinte"
        Me.BtnSeguinte.Size = New System.Drawing.Size(99, 24)
        Me.BtnSeguinte.TabIndex = 17
        Me.BtnSeguinte.Text = "Seguinte"
        Me.BtnSeguinte.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(63, 182)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Número:"
        '
        'LabelNumero
        '
        Me.LabelNumero.AutoSize = True
        Me.LabelNumero.Location = New System.Drawing.Point(122, 184)
        Me.LabelNumero.Name = "LabelNumero"
        Me.LabelNumero.Size = New System.Drawing.Size(0, 13)
        Me.LabelNumero.TabIndex = 31
        '
        'JanelaInformacoes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(590, 500)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.TabControlInfomacoes)
        Me.Controls.Add(Me.BtnCancelar)
        Me.Controls.Add(Me.BtnAnterior)
        Me.Controls.Add(Me.BtnSeguinte)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(606, 538)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(606, 538)
        Me.Name = "JanelaInformacoes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form2"
        Me.TabControlInfomacoes.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBoxCapa, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents TabControlInfomacoes As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents LabelNomeMus As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents TextBoxComentarios As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents TextBoxGrupo As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TextBoxAlbum As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TextBoxInterprete As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNomeMus As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents BtnDeleteImage As System.Windows.Forms.Button
    Friend WithEvents BtnAddImage As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBoxCapa As System.Windows.Forms.PictureBox
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents RichTextBoxLetra As System.Windows.Forms.RichTextBox
    Friend WithEvents BtnCancelar As System.Windows.Forms.Button
    Friend WithEvents BtnAnterior As System.Windows.Forms.Button
    Friend WithEvents BtnSeguinte As System.Windows.Forms.Button
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents LineShape1 As Microsoft.VisualBasic.PowerPacks.LineShape
    Friend WithEvents LineShape2 As Microsoft.VisualBasic.PowerPacks.LineShape
    Friend WithEvents CheckBoxComboBoxGenero As PresentationControls.CheckBoxComboBox
    Friend WithEvents LabelInterpMus As System.Windows.Forms.Label
    Friend WithEvents LabelAlbumMus As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents LabelFormato As System.Windows.Forms.Label
    Friend WithEvents LabelUltReprd As System.Windows.Forms.Label
    Friend WithEvents LabelReprod As System.Windows.Forms.Label
    Friend WithEvents LabelDataAdicao As System.Windows.Forms.Label
    Friend WithEvents LabelCanal As System.Windows.Forms.Label
    Friend WithEvents LabelTaxaBits As System.Windows.Forms.Label
    Friend WithEvents LabelCopyRight As System.Windows.Forms.Label
    Friend WithEvents LabelFreq As System.Windows.Forms.Label
    Friend WithEvents LabelTamanho As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents LabelLocalizacao As System.Windows.Forms.Label
    Friend WithEvents ValidTextAno As ValidText.ValidText
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LabelNumero As System.Windows.Forms.Label
End Class
