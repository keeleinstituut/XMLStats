Imports System.Windows.Forms

Public Class frmDlgPrefs

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim msNimi As String = tbMsNimi.Text.Trim
        If msNimi.IndexOf(":") > -1 Then
            msNimi = msNimi.Substring(msNimi.IndexOf(":") + 1)
            tbMsNimi.Text = msNimi
        End If
        My.Settings.msNimi = msNimi

        Dim artNimi As String = tbArtNimi.Text.Trim
        If artNimi.IndexOf(":") > -1 Then
            artNimi = artNimi.Substring(artNimi.IndexOf(":") + 1)
            tbArtNimi.Text = artNimi
        End If
        My.Settings.artNimi = artNimi

        My.Settings.Save()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnElemsAsText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElemsAsText.Click
        Dim frm As New frmDlgElemsAsText
        frm.propWTtitle = "Elemendid tekstina"
        frm.propSetting = "namesAsText"
        Dim dlgResult As DialogResult = frm.ShowDialog()
        If (dlgResult = Windows.Forms.DialogResult.OK) Then
            Dim nimed As String = ";"
            For Each lvi As ListViewItem In frm.lvNamesAsText.Items
                nimed &= lvi.Tag.ToString & ";"
            Next
            My.Settings.namesAsText = nimed
            My.Settings.Save()
            namesAsText = My.Settings.namesAsText
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub btnExcludeElems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcludeElems.Click
        Dim frm As New frmDlgElemsAsText
        frm.propWTtitle = "Analüüsist välja jäetavad elemendid/atribuudid"
        frm.propSetting = "excludeNodes"
        Dim dlgResult As DialogResult = frm.ShowDialog()
        If (dlgResult = Windows.Forms.DialogResult.OK) Then
            Dim nimed As String = ";"
            For Each lvi As ListViewItem In frm.lvNamesAsText.Items
                nimed &= lvi.Tag.ToString & ";"
            Next
            My.Settings.excludeNodes = nimed
            My.Settings.Save()
            excludeNames = My.Settings.excludeNodes
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub frmDlgPrefs_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tbMsNimi.Text = My.Settings.msNimi
        tbArtNimi.Text = My.Settings.artNimi
    End Sub
End Class
