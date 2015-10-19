Public Class frmOptions

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles btnSaveOptions.Click
        My.Settings.Save()
    End Sub

    Private Sub frmOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        PropertyGrid1.SelectedObject = My.Settings
        'My.Settings.Save()

        Me.lblFileDiConfigurazione.Text = My.Settings.DCC_FOLDER_NAME + "\" + My.Settings.XML_CONFIG_FILE

    End Sub

    Private Sub btnReadXML_Click(sender As Object, e As EventArgs) Handles btnReadXML.Click
        'Hash256File(My.Settings.XmlConfigFile)
        'getTimeStamp()
    End Sub

    Private Sub btnHash256_Click(sender As Object, e As EventArgs) Handles btnHash256.Click
        'ReadXML()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub
End Class