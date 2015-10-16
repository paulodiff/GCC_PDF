Public Class frmOptions

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles btnSaveOptions.Click
        My.Settings.Save()
    End Sub

    Private Sub frmOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        PropertyGrid1.SelectedObject = My.Settings
        'My.Settings.Save()

        Me.lblFileDiConfigurazione.Text = My.Settings.XmlConfigFile

    End Sub

    Private Sub btnReadXML_Click(sender As Object, e As EventArgs) Handles btnReadXML.Click
        'Hash256File(My.Settings.XmlConfigFile)
        'getTimeStamp()
    End Sub

    Private Sub btnHash256_Click(sender As Object, e As EventArgs) Handles btnHash256.Click
        ReadXML()
    End Sub
End Class