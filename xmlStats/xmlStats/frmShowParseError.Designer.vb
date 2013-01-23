<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShowParseError
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
        Me.tbErrOffset = New System.Windows.Forms.TextBox
        Me.lblErrOffset = New System.Windows.Forms.Label
        Me.tbError = New System.Windows.Forms.TextBox
        Me.lblEerror = New System.Windows.Forms.Label
        Me.tbArtikkel = New System.Windows.Forms.TextBox
        Me.lblArtikkel = New System.Windows.Forms.Label
        Me.lblHeadWord = New System.Windows.Forms.Label
        Me.tbHeadWord = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'tbErrOffset
        '
        Me.tbErrOffset.Location = New System.Drawing.Point(12, 25)
        Me.tbErrOffset.Name = "tbErrOffset"
        Me.tbErrOffset.ReadOnly = True
        Me.tbErrOffset.Size = New System.Drawing.Size(116, 20)
        Me.tbErrOffset.TabIndex = 1
        '
        'lblErrOffset
        '
        Me.lblErrOffset.AutoSize = True
        Me.lblErrOffset.Location = New System.Drawing.Point(12, 9)
        Me.lblErrOffset.Name = "lblErrOffset"
        Me.lblErrOffset.Size = New System.Drawing.Size(101, 13)
        Me.lblErrOffset.TabIndex = 0
        Me.lblErrOffset.Text = "Vea positsioon failils"
        '
        'tbError
        '
        Me.tbError.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tbError.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(186, Byte))
        Me.tbError.Location = New System.Drawing.Point(12, 64)
        Me.tbError.Multiline = True
        Me.tbError.Name = "tbError"
        Me.tbError.Size = New System.Drawing.Size(296, 368)
        Me.tbError.TabIndex = 5
        '
        'lblEerror
        '
        Me.lblEerror.AutoSize = True
        Me.lblEerror.Location = New System.Drawing.Point(9, 48)
        Me.lblEerror.Name = "lblEerror"
        Me.lblEerror.Size = New System.Drawing.Size(28, 13)
        Me.lblEerror.TabIndex = 4
        Me.lblEerror.Text = "Viga"
        '
        'tbArtikkel
        '
        Me.tbArtikkel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbArtikkel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(186, Byte))
        Me.tbArtikkel.Location = New System.Drawing.Point(314, 25)
        Me.tbArtikkel.Multiline = True
        Me.tbArtikkel.Name = "tbArtikkel"
        Me.tbArtikkel.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbArtikkel.Size = New System.Drawing.Size(389, 407)
        Me.tbArtikkel.TabIndex = 7
        '
        'lblArtikkel
        '
        Me.lblArtikkel.AutoSize = True
        Me.lblArtikkel.Location = New System.Drawing.Point(311, 9)
        Me.lblArtikkel.Name = "lblArtikkel"
        Me.lblArtikkel.Size = New System.Drawing.Size(42, 13)
        Me.lblArtikkel.TabIndex = 6
        Me.lblArtikkel.Text = "Artikkel"
        '
        'lblHeadWord
        '
        Me.lblHeadWord.AutoSize = True
        Me.lblHeadWord.Location = New System.Drawing.Point(134, 9)
        Me.lblHeadWord.Name = "lblHeadWord"
        Me.lblHeadWord.Size = New System.Drawing.Size(54, 13)
        Me.lblHeadWord.TabIndex = 2
        Me.lblHeadWord.Text = "Märksõna"
        '
        'tbHeadWord
        '
        Me.tbHeadWord.Location = New System.Drawing.Point(134, 25)
        Me.tbHeadWord.Name = "tbHeadWord"
        Me.tbHeadWord.ReadOnly = True
        Me.tbHeadWord.Size = New System.Drawing.Size(174, 20)
        Me.tbHeadWord.TabIndex = 3
        '
        'frmShowParseError
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(715, 444)
        Me.Controls.Add(Me.tbHeadWord)
        Me.Controls.Add(Me.lblHeadWord)
        Me.Controls.Add(Me.lblArtikkel)
        Me.Controls.Add(Me.tbArtikkel)
        Me.Controls.Add(Me.lblEerror)
        Me.Controls.Add(Me.tbError)
        Me.Controls.Add(Me.lblErrOffset)
        Me.Controls.Add(Me.tbErrOffset)
        Me.Name = "frmShowParseError"
        Me.Text = "Viga: "
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbErrOffset As System.Windows.Forms.TextBox
    Friend WithEvents lblErrOffset As System.Windows.Forms.Label
    Friend WithEvents tbError As System.Windows.Forms.TextBox
    Friend WithEvents lblEerror As System.Windows.Forms.Label
    Friend WithEvents tbArtikkel As System.Windows.Forms.TextBox
    Friend WithEvents lblArtikkel As System.Windows.Forms.Label
    Friend WithEvents lblHeadWord As System.Windows.Forms.Label
    Friend WithEvents tbHeadWord As System.Windows.Forms.TextBox
End Class
