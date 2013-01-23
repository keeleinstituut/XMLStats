Imports System.Windows.Forms

Public Class frmDlgAddName

    Dim fk As String '"findKey"
    Public ReadOnly Property propFK() As String
        Get
            Return fk
        End Get
    End Property
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        fk = tbNodeName.Text.Trim
        If (fk.Length > 0) Then
            fk = fk.Substring(fk.IndexOf(":") + 1)
            If (rbAttribute.Checked) Then
                fk = "@" & fk
            End If
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
