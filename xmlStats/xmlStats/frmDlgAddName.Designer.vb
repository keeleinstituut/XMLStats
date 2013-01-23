<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDlgAddName
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.gbType = New System.Windows.Forms.GroupBox
        Me.rbAttribute = New System.Windows.Forms.RadioButton
        Me.rbElement = New System.Windows.Forms.RadioButton
        Me.lblName = New System.Windows.Forms.Label
        Me.tbNodeName = New System.Windows.Forms.TextBox
        Me.TableLayoutPanel1.SuspendLayout()
        Me.gbType.SuspendLayout()
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(294, 147)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 1
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
        'gbType
        '
        Me.gbType.Controls.Add(Me.rbAttribute)
        Me.gbType.Controls.Add(Me.rbElement)
        Me.gbType.Location = New System.Drawing.Point(12, 12)
        Me.gbType.Name = "gbType"
        Me.gbType.Size = New System.Drawing.Size(425, 53)
        Me.gbType.TabIndex = 2
        Me.gbType.TabStop = False
        Me.gbType.Text = "Tüüp"
        '
        'rbAttribute
        '
        Me.rbAttribute.AutoSize = True
        Me.rbAttribute.Location = New System.Drawing.Point(358, 19)
        Me.rbAttribute.Name = "rbAttribute"
        Me.rbAttribute.Size = New System.Drawing.Size(61, 17)
        Me.rbAttribute.TabIndex = 1
        Me.rbAttribute.Text = "Atribuut"
        Me.rbAttribute.UseVisualStyleBackColor = True
        '
        'rbElement
        '
        Me.rbElement.AutoSize = True
        Me.rbElement.Checked = True
        Me.rbElement.Location = New System.Drawing.Point(6, 19)
        Me.rbElement.Name = "rbElement"
        Me.rbElement.Size = New System.Drawing.Size(63, 17)
        Me.rbElement.TabIndex = 0
        Me.rbElement.TabStop = True
        Me.rbElement.Text = "Element"
        Me.rbElement.UseVisualStyleBackColor = True
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(12, 80)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(27, 13)
        Me.lblName.TabIndex = 0
        Me.lblName.Text = "Nimi"
        '
        'tbNodeName
        '
        Me.tbNodeName.Location = New System.Drawing.Point(12, 96)
        Me.tbNodeName.Name = "tbNodeName"
        Me.tbNodeName.Size = New System.Drawing.Size(425, 20)
        Me.tbNodeName.TabIndex = 1
        '
        'frmDlgAddName
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(452, 188)
        Me.Controls.Add(Me.tbNodeName)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.gbType)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDlgAddName"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Lisa uus nimi"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.gbType.ResumeLayout(False)
        Me.gbType.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents gbType As System.Windows.Forms.GroupBox
    Friend WithEvents rbAttribute As System.Windows.Forms.RadioButton
    Friend WithEvents rbElement As System.Windows.Forms.RadioButton
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents tbNodeName As System.Windows.Forms.TextBox

End Class
