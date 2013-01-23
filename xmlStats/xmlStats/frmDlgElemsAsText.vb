Imports System.Windows.Forms

Public Class frmDlgElemsAsText

    Dim wTitle As String
    Dim setting As String

    Public WriteOnly Property propWTtitle() As String
        Set(ByVal value As String)
            wTitle = value
        End Set
    End Property

    Public WriteOnly Property propSetting() As String
        Set(ByVal value As String)
            setting = value
        End Set
    End Property

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnRemoveAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveAll.Click
        lvNamesAsText.Items.Clear()
    End Sub

    Private Sub btnAddOne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddOne.Click
        If (lvNames.SelectedItems.Count = 1) Then
            Call procAddOne(lvNames.SelectedItems(0))
        End If
    End Sub

    Private Sub procAddOne(ByVal lvi As ListViewItem)
        Dim qn As String = lvi.Tag
        Dim fk As String
        If (qn.StartsWith("@")) Then
            qn = qn.Substring(1)
            fk = "@" & qn.Substring(qn.IndexOf(":") + 1)
        Else
            fk = qn.Substring(qn.IndexOf(":") + 1)
        End If
        Dim found As Boolean = False
        Dim foundLvi() As ListViewItem = lvNamesAsText.Items.Find(fk, False)
        For Each fLvi As ListViewItem In foundLvi
            If (fLvi.Name = fk) Then 'Find ei ole tõstutundlik
                found = True
                Exit For
            End If
        Next
        If Not (found) Then
            Dim uusLvi As ListViewItem
            If (fk.StartsWith("@")) Then
                uusLvi = New ListViewItem(fk)
            Else
                uusLvi = New ListViewItem("<" & fk & ">")
            End If
            uusLvi.Tag = fk
            uusLvi.Name = fk
            lvNamesAsText.Items.Add(uusLvi)
        End If
    End Sub

    Private Sub procRemoveOne(ByVal lvi As ListViewItem)
        lvNamesAsText.Items.Remove(lvi)
    End Sub

    Private Sub btnRemoveOne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveOne.Click
        If (lvNamesAsText.SelectedItems.Count = 1) Then
            Call procRemoveOne(lvNamesAsText.SelectedItems(0))
        End If
    End Sub

    Private Sub lvNames_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvNames.DoubleClick
        Call procAddOne(lvNames.SelectedItems(0))
    End Sub

    Private Sub lvNamesAsText_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvNamesAsText.DoubleClick
        Call procRemoveOne(lvNamesAsText.SelectedItems(0))
    End Sub

    Private Sub btnAddName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddName.Click
        Dim dlgResult As DialogResult
        dlgResult = frmDlgAddName.ShowDialog()
        If (dlgResult = Windows.Forms.DialogResult.OK) Then
            Dim fk As String = frmDlgAddName.propFK
            Dim uusLvi As ListViewItem
            If (fk.StartsWith("@")) Then
                uusLvi = New ListViewItem(fk)
            Else
                uusLvi = New ListViewItem("<" & fk & ">")
            End If
            uusLvi.Tag = fk
            uusLvi.Name = fk
            Call procAddOne(uusLvi)
        End If
    End Sub

    Private Sub frmDlgElemsAsText_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lvi As ListViewItem
        Dim val As String = String.Empty
        If (setting = "namesAsText") Then
            val = My.Settings.namesAsText 'salvestatud on localName-d
        ElseIf (setting = "excludeNodes") Then
            val = My.Settings.excludeNodes 'salvestatud on localName-d
        ElseIf (setting = "enumNames") Then
            val = My.Settings.enumNames 'salvestatud on localName-d
        End If
        If (val.Length > 2) Then 'eraldatud on nad semidega
            Dim elNamesAsText() As String = val.Substring(1, val.Length - 2).Split(";")
            For Each elName As String In elNamesAsText
                If (elName.Chars(0) = "@") Then
                    lvi = New ListViewItem(elName)
                Else
                    lvi = New ListViewItem("<" & elName & ">")
                End If
                lvi.Name = elName
                lvi.Tag = elName
                lvNamesAsText.Items.Add(lvi)
            Next
        End If

        Dim nimi As String
        For Each kvp As KeyValuePair(Of String, Integer) In xmlNimed
            nimi = kvp.Key
            If (nimi.Chars(0) = "@") Then
                lvi = New ListViewItem(nimi)
            Else
                lvi = New ListViewItem("<" & nimi & ">")
            End If
            lvi.Name = nimi
            lvi.Tag = nimi
            lvNames.Items.Add(lvi)
        Next

        Me.Text = wTitle
    End Sub
End Class
