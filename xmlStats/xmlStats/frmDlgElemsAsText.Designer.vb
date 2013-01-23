<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDlgElemsAsText
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
        Me.lvNames = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.lvNamesAsText = New System.Windows.Forms.ListView
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.btnAddOne = New System.Windows.Forms.Button
        Me.btnRemoveOne = New System.Windows.Forms.Button
        Me.btnRemoveAll = New System.Windows.Forms.Button
        Me.btnAddName = New System.Windows.Forms.Button
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(627, 525)
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
        'lvNames
        '
        Me.lvNames.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.lvNames.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(186, Byte))
        Me.lvNames.FullRowSelect = True
        Me.lvNames.HideSelection = False
        Me.lvNames.Location = New System.Drawing.Point(12, 12)
        Me.lvNames.Name = "lvNames"
        Me.lvNames.Size = New System.Drawing.Size(291, 452)
        Me.lvNames.TabIndex = 1
        Me.lvNames.UseCompatibleStateImageBehavior = False
        Me.lvNames.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Nimi"
        Me.ColumnHeader1.Width = 230
        '
        'lvNamesAsText
        '
        Me.lvNamesAsText.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2})
        Me.lvNamesAsText.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(186, Byte))
        Me.lvNamesAsText.FullRowSelect = True
        Me.lvNamesAsText.HideSelection = False
        Me.lvNamesAsText.Location = New System.Drawing.Point(432, 12)
        Me.lvNamesAsText.Name = "lvNamesAsText"
        Me.lvNamesAsText.Size = New System.Drawing.Size(338, 452)
        Me.lvNamesAsText.TabIndex = 2
        Me.lvNamesAsText.UseCompatibleStateImageBehavior = False
        Me.lvNamesAsText.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Nimi"
        Me.ColumnHeader2.Width = 279
        '
        'btnAddOne
        '
        Me.btnAddOne.Location = New System.Drawing.Point(327, 12)
        Me.btnAddOne.Name = "btnAddOne"
        Me.btnAddOne.Size = New System.Drawing.Size(75, 23)
        Me.btnAddOne.TabIndex = 3
        Me.btnAddOne.Text = "--->"
        Me.btnAddOne.UseVisualStyleBackColor = True
        '
        'btnRemoveOne
        '
        Me.btnRemoveOne.Location = New System.Drawing.Point(327, 41)
        Me.btnRemoveOne.Name = "btnRemoveOne"
        Me.btnRemoveOne.Size = New System.Drawing.Size(75, 23)
        Me.btnRemoveOne.TabIndex = 4
        Me.btnRemoveOne.Text = "<---"
        Me.btnRemoveOne.UseVisualStyleBackColor = True
        '
        'btnRemoveAll
        '
        Me.btnRemoveAll.Location = New System.Drawing.Point(327, 141)
        Me.btnRemoveAll.Name = "btnRemoveAll"
        Me.btnRemoveAll.Size = New System.Drawing.Size(75, 23)
        Me.btnRemoveAll.TabIndex = 5
        Me.btnRemoveAll.Text = "<==="
        Me.btnRemoveAll.UseVisualStyleBackColor = True
        '
        'btnAddName
        '
        Me.btnAddName.Location = New System.Drawing.Point(695, 470)
        Me.btnAddName.Name = "btnAddName"
        Me.btnAddName.Size = New System.Drawing.Size(75, 23)
        Me.btnAddName.TabIndex = 6
        Me.btnAddName.Text = "Lisa"
        Me.btnAddName.UseVisualStyleBackColor = True
        '
        'frmDlgElemsAsText
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(785, 566)
        Me.Controls.Add(Me.btnAddName)
        Me.Controls.Add(Me.btnRemoveAll)
        Me.Controls.Add(Me.btnRemoveOne)
        Me.Controls.Add(Me.btnAddOne)
        Me.Controls.Add(Me.lvNamesAsText)
        Me.Controls.Add(Me.lvNames)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDlgElemsAsText"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Elemendid tekstina"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents lvNames As System.Windows.Forms.ListView
    Friend WithEvents lvNamesAsText As System.Windows.Forms.ListView
    Friend WithEvents btnAddOne As System.Windows.Forms.Button
    Friend WithEvents btnRemoveOne As System.Windows.Forms.Button
    Friend WithEvents btnRemoveAll As System.Windows.Forms.Button
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnAddName As System.Windows.Forms.Button

End Class
