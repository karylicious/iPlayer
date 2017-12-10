<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSync
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSync))
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.LabelMp3 = New System.Windows.Forms.Label
        Me.PictureBox2MP3 = New System.Windows.Forms.PictureBox
        Me.PictureBox1Pen = New System.Windows.Forms.PictureBox
        Me.LabelPen = New System.Windows.Forms.Label
        Me.PictureBox4 = New System.Windows.Forms.PictureBox
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.BtnPlay = New System.Windows.Forms.Button
        Me.BtnAnterior = New System.Windows.Forms.Button
        Me.BtnSeguinte = New System.Windows.Forms.Button
        Me.TrackBar1 = New System.Windows.Forms.TrackBar
        CType(Me.PictureBox2MP3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1Pen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.BackColor = System.Drawing.Color.Lavender
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(-1, 125)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(515, 277)
        Me.ListBox1.TabIndex = 16
        '
        'LabelMp3
        '
        Me.LabelMp3.AutoSize = True
        Me.LabelMp3.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelMp3.Location = New System.Drawing.Point(225, 15)
        Me.LabelMp3.Name = "LabelMp3"
        Me.LabelMp3.Size = New System.Drawing.Size(41, 18)
        Me.LabelMp3.TabIndex = 31
        Me.LabelMp3.Text = "MP3"
        Me.LabelMp3.Visible = False
        '
        'PictureBox2MP3
        '
        Me.PictureBox2MP3.Image = Global.iPlayer.My.Resources.Resources.iPod___Black_64
        Me.PictureBox2MP3.Location = New System.Drawing.Point(215, 40)
        Me.PictureBox2MP3.Name = "PictureBox2MP3"
        Me.PictureBox2MP3.Size = New System.Drawing.Size(63, 70)
        Me.PictureBox2MP3.TabIndex = 30
        Me.PictureBox2MP3.TabStop = False
        Me.PictureBox2MP3.Visible = False
        '
        'PictureBox1Pen
        '
        Me.PictureBox1Pen.Image = Global.iPlayer.My.Resources.Resources.pendrive_64x64
        Me.PictureBox1Pen.Location = New System.Drawing.Point(213, 46)
        Me.PictureBox1Pen.Name = "PictureBox1Pen"
        Me.PictureBox1Pen.Size = New System.Drawing.Size(67, 48)
        Me.PictureBox1Pen.TabIndex = 29
        Me.PictureBox1Pen.TabStop = False
        Me.PictureBox1Pen.Visible = False
        '
        'LabelPen
        '
        Me.LabelPen.AutoSize = True
        Me.LabelPen.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelPen.Location = New System.Drawing.Point(212, 12)
        Me.LabelPen.Name = "LabelPen"
        Me.LabelPen.Size = New System.Drawing.Size(81, 18)
        Me.LabelPen.TabIndex = 28
        Me.LabelPen.Text = "Pen Drive"
        Me.LabelPen.Visible = False
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = Global.iPlayer.My.Resources.Resources.mute_off1
        Me.PictureBox4.Location = New System.Drawing.Point(473, 55)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(20, 17)
        Me.PictureBox4.TabIndex = 102
        Me.PictureBox4.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.iPlayer.My.Resources.Resources.mute_on1
        Me.PictureBox3.Location = New System.Drawing.Point(350, 55)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(16, 18)
        Me.PictureBox3.TabIndex = 101
        Me.PictureBox3.TabStop = False
        '
        'BtnPlay
        '
        Me.BtnPlay.BackColor = System.Drawing.Color.LightSteelBlue
        Me.BtnPlay.FlatAppearance.BorderSize = 0
        Me.BtnPlay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.BtnPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.BtnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPlay.Image = Global.iPlayer.My.Resources.Resources.playerpp
        Me.BtnPlay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnPlay.Location = New System.Drawing.Point(61, 39)
        Me.BtnPlay.Name = "BtnPlay"
        Me.BtnPlay.Size = New System.Drawing.Size(45, 45)
        Me.BtnPlay.TabIndex = 98
        Me.BtnPlay.UseVisualStyleBackColor = False
        '
        'BtnAnterior
        '
        Me.BtnAnterior.BackColor = System.Drawing.Color.LightSteelBlue
        Me.BtnAnterior.FlatAppearance.BorderSize = 0
        Me.BtnAnterior.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.BtnAnterior.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.BtnAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAnterior.Image = Global.iPlayer.My.Resources.Resources.player_rew_32_enabled
        Me.BtnAnterior.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnAnterior.Location = New System.Drawing.Point(24, 39)
        Me.BtnAnterior.Name = "BtnAnterior"
        Me.BtnAnterior.Size = New System.Drawing.Size(45, 45)
        Me.BtnAnterior.TabIndex = 99
        Me.BtnAnterior.UseVisualStyleBackColor = False
        '
        'BtnSeguinte
        '
        Me.BtnSeguinte.BackColor = System.Drawing.Color.LightSteelBlue
        Me.BtnSeguinte.FlatAppearance.BorderSize = 0
        Me.BtnSeguinte.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.BtnSeguinte.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.BtnSeguinte.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSeguinte.Image = Global.iPlayer.My.Resources.Resources.player_fwd_32_enabled
        Me.BtnSeguinte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnSeguinte.Location = New System.Drawing.Point(105, 39)
        Me.BtnSeguinte.Name = "BtnSeguinte"
        Me.BtnSeguinte.Size = New System.Drawing.Size(45, 45)
        Me.BtnSeguinte.TabIndex = 97
        Me.BtnSeguinte.UseVisualStyleBackColor = False
        '
        'TrackBar1
        '
        Me.TrackBar1.Location = New System.Drawing.Point(367, 43)
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(105, 45)
        Me.TrackBar1.TabIndex = 100
        Me.TrackBar1.TickStyle = System.Windows.Forms.TickStyle.Both
        Me.TrackBar1.Value = 5
        '
        'FormSync
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(513, 440)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.BtnPlay)
        Me.Controls.Add(Me.BtnAnterior)
        Me.Controls.Add(Me.BtnSeguinte)
        Me.Controls.Add(Me.TrackBar1)
        Me.Controls.Add(Me.LabelMp3)
        Me.Controls.Add(Me.PictureBox2MP3)
        Me.Controls.Add(Me.PictureBox1Pen)
        Me.Controls.Add(Me.LabelPen)
        Me.Controls.Add(Me.ListBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormSync"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sync"
        CType(Me.PictureBox2MP3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1Pen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents LabelMp3 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2MP3 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1Pen As System.Windows.Forms.PictureBox
    Friend WithEvents LabelPen As System.Windows.Forms.Label
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents BtnPlay As System.Windows.Forms.Button
    Friend WithEvents BtnAnterior As System.Windows.Forms.Button
    Friend WithEvents BtnSeguinte As System.Windows.Forms.Button
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
End Class
