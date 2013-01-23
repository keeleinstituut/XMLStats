<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDlgXMLValidate
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
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblXMLFile = New System.Windows.Forms.Label()
        Me.lblXSDFile = New System.Windows.Forms.Label()
        Me.tbXMLFile = New System.Windows.Forms.TextBox()
        Me.tbXSDFile = New System.Windows.Forms.TextBox()
        Me.btnBrowseXML = New System.Windows.Forms.Button()
        Me.btnBrowseXSD = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
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
        Me.OK_Button.Text = "Valideeri"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Välju"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.10256!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.89744!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 44.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.lblXMLFile, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lblXSDFile, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.tbXMLFile, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.tbXSDFile, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.btnBrowseXML, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnBrowseXSD, 2, 1)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(12, 12)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(411, 57)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'lblXMLFile
        '
        Me.lblXMLFile.AutoSize = True
        Me.lblXMLFile.Location = New System.Drawing.Point(3, 0)
        Me.lblXMLFile.Name = "lblXMLFile"
        Me.lblXMLFile.Size = New System.Drawing.Size(45, 13)
        Me.lblXMLFile.TabIndex = 0
        Me.lblXMLFile.Text = "XML fail"
        '
        'lblXSDFile
        '
        Me.lblXSDFile.AutoSize = True
        Me.lblXSDFile.Location = New System.Drawing.Point(3, 28)
        Me.lblXSDFile.Name = "lblXSDFile"
        Me.lblXSDFile.Size = New System.Drawing.Size(45, 13)
        Me.lblXSDFile.TabIndex = 1
        Me.lblXSDFile.Text = "XSD fail"
        '
        'tbXMLFile
        '
        Me.tbXMLFile.Enabled = False
        Me.tbXMLFile.Location = New System.Drawing.Point(54, 3)
        Me.tbXMLFile.Name = "tbXMLFile"
        Me.tbXMLFile.Size = New System.Drawing.Size(309, 20)
        Me.tbXMLFile.TabIndex = 2
        '
        'tbXSDFile
        '
        Me.tbXSDFile.Enabled = False
        Me.tbXSDFile.Location = New System.Drawing.Point(54, 31)
        Me.tbXSDFile.Name = "tbXSDFile"
        Me.tbXSDFile.Size = New System.Drawing.Size(309, 20)
        Me.tbXSDFile.TabIndex = 3
        '
        'btnBrowseXML
        '
        Me.btnBrowseXML.Location = New System.Drawing.Point(369, 3)
        Me.btnBrowseXML.Name = "btnBrowseXML"
        Me.btnBrowseXML.Size = New System.Drawing.Size(35, 22)
        Me.btnBrowseXML.TabIndex = 4
        Me.btnBrowseXML.Text = "Otsi"
        Me.btnBrowseXML.UseVisualStyleBackColor = True
        '
        'btnBrowseXSD
        '
        Me.btnBrowseXSD.Location = New System.Drawing.Point(369, 31)
        Me.btnBrowseXSD.Name = "btnBrowseXSD"
        Me.btnBrowseXSD.Size = New System.Drawing.Size(35, 23)
        Me.btnBrowseXSD.TabIndex = 5
        Me.btnBrowseXSD.Text = "Otsi"
        Me.btnBrowseXSD.UseVisualStyleBackColor = True
        '
        'frmDlgXMLValidate
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(435, 315)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDlgXMLValidate"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "XML-i valideerimine"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblXMLFile As System.Windows.Forms.Label
    Friend WithEvents lblXSDFile As System.Windows.Forms.Label
    Friend WithEvents tbXMLFile As System.Windows.Forms.TextBox
    Friend WithEvents tbXSDFile As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowseXML As System.Windows.Forms.Button
    Friend WithEvents btnBrowseXSD As System.Windows.Forms.Button

End Class
