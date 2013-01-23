Imports System.Windows.Forms

Public Class frmDlgHelp

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmDlgHelp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            rtbHelpText.LoadFile(Application.StartupPath & "\tekst\Abi.rtf", RichTextBoxStreamType.RichText)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Abi tekst")
        End Try
    End Sub
End Class
