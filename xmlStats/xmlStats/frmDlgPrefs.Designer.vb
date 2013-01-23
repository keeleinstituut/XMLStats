<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDlgPrefs
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.btnElemsAsText = New System.Windows.Forms.Button()
        Me.btnExcludeElems = New System.Windows.Forms.Button()
        Me.lblMsNimi = New System.Windows.Forms.Label()
        Me.tbMsNimi = New System.Windows.Forms.TextBox()
        Me.tbArtNimi = New System.Windows.Forms.TextBox()
        Me.lblArtNimi = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(277, 274)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'btnElemsAsText
        '
        Me.btnElemsAsText.Location = New System.Drawing.Point(12, 54)
        Me.btnElemsAsText.Name = "btnElemsAsText"
        Me.btnElemsAsText.Size = New System.Drawing.Size(144, 36)
        Me.btnElemsAsText.TabIndex = 1
        Me.btnElemsAsText.Text = "Elemendid tekstina"
        Me.btnElemsAsText.UseVisualStyleBackColor = True
        '
        'btnExcludeElems
        '
        Me.btnExcludeElems.Location = New System.Drawing.Point(12, 12)
        Me.btnExcludeElems.Name = "btnExcludeElems"
        Me.btnExcludeElems.Size = New System.Drawing.Size(144, 36)
        Me.btnExcludeElems.TabIndex = 2
        Me.btnExcludeElems.Text = "Välja jäetavad elemendid/atribuudid"
        Me.btnExcludeElems.UseVisualStyleBackColor = True
        '
        'lblMsNimi
        '
        Me.lblMsNimi.AutoSize = True
        Me.lblMsNimi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(186, Byte))
        Me.lblMsNimi.Location = New System.Drawing.Point(12, 126)
        Me.lblMsNimi.Name = "lblMsNimi"
        Me.lblMsNimi.Size = New System.Drawing.Size(207, 13)
        Me.lblMsNimi.TabIndex = 3
        Me.lblMsNimi.Text = "Märksõna rollis oleva elemendi nimi"
        '
        'tbMsNimi
        '
        Me.tbMsNimi.Location = New System.Drawing.Point(12, 142)
        Me.tbMsNimi.Name = "tbMsNimi"
        Me.tbMsNimi.Size = New System.Drawing.Size(207, 20)
        Me.tbMsNimi.TabIndex = 4
        '
        'tbArtNimi
        '
        Me.tbArtNimi.Location = New System.Drawing.Point(12, 195)
        Me.tbArtNimi.Name = "tbArtNimi"
        Me.tbArtNimi.Size = New System.Drawing.Size(207, 20)
        Me.tbArtNimi.TabIndex = 5
        '
        'lblArtNimi
        '
        Me.lblArtNimi.AutoSize = True
        Me.lblArtNimi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(186, Byte))
        Me.lblArtNimi.Location = New System.Drawing.Point(12, 179)
        Me.lblArtNimi.Name = "lblArtNimi"
        Me.lblArtNimi.Size = New System.Drawing.Size(184, 13)
        Me.lblArtNimi.TabIndex = 6
        Me.lblArtNimi.Text = "Artikli rollis oleva elemendi nimi"
        '
        'frmDlgPrefs
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(435, 315)
        Me.Controls.Add(Me.lblArtNimi)
        Me.Controls.Add(Me.tbArtNimi)
        Me.Controls.Add(Me.tbMsNimi)
        Me.Controls.Add(Me.lblMsNimi)
        Me.Controls.Add(Me.btnExcludeElems)
        Me.Controls.Add(Me.btnElemsAsText)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDlgPrefs"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Eelistused"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents btnElemsAsText As System.Windows.Forms.Button
    Friend WithEvents btnExcludeElems As System.Windows.Forms.Button
    Friend WithEvents lblMsNimi As System.Windows.Forms.Label
    Friend WithEvents tbMsNimi As System.Windows.Forms.TextBox
    Friend WithEvents tbArtNimi As System.Windows.Forms.TextBox
    Friend WithEvents lblArtNimi As System.Windows.Forms.Label

End Class
