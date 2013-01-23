<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShowParseErrors
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
        Me.lvErrors = New System.Windows.Forms.ListView
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader9 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader10 = New System.Windows.Forms.ColumnHeader
        Me.SuspendLayout()
        '
        'lvErrors
        '
        Me.lvErrors.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader9, Me.ColumnHeader10})
        Me.lvErrors.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvErrors.FullRowSelect = True
        Me.lvErrors.HideSelection = False
        Me.lvErrors.LabelEdit = True
        Me.lvErrors.Location = New System.Drawing.Point(0, 0)
        Me.lvErrors.Name = "lvErrors"
        Me.lvErrors.Size = New System.Drawing.Size(842, 345)
        Me.lvErrors.TabIndex = 0
        Me.lvErrors.UseCompatibleStateImageBehavior = False
        Me.lvErrors.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Pos failis"
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Nr"
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Viga"
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Märksõna"
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "Artikkel"
        '
        'frmShowParseErrors
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(842, 345)
        Me.Controls.Add(Me.lvErrors)
        Me.Name = "frmShowParseErrors"
        Me.Text = "Vead: "
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lvErrors As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
End Class
