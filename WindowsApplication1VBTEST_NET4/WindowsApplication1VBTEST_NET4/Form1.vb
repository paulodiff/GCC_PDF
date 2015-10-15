Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO

Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim md5String As String
        Dim sha256String As String
        Dim testFile As System.IO.FileInfo
        Dim dictionary As New Dictionary(Of String, String)
        Dim pdfFileName As String
        Dim xmlFileName As String
        Dim timeStamp As String = Format(Now, "yyyy_MM_dd_HH_mm")
        Dim folderPath As String

        pdfFileName = "DCC" + timeStamp + ".pdf"
        xmlFileName = My.Settings.XmlConfigFile

        'MsgBox("File di configurazione:" & xmlFileName)
        'Me.lblFileDiConfigurazione.Text = xmlFileName
        'MsgBox(ListBox1.Items.Count)
        If (ListBox1.Items.Count = 0) Then
            MsgBox("Mancano i file per il calcolo...  ")
            Return
        End If

        'Console.WriteLine(My.Settings.Connessione)
        'Console.WriteLine(My.Settings.Connessione)

        ' Adding command line arguments
        'For Each argument As String In My.Application.CommandLineArgs
        ' Add code here to use the argument. 
        'Next

        'MsgBox(CStr(timeStamp))
        Dim intPercent As Integer = 100 / (ListBox1.Items.Count + 1)
        ProgressBar1.Value = 0


        For l_index As Integer = 0 To ListBox1.Items.Count - 1
            Dim l_text As String = CStr(ListBox1.Items(l_index))


            LogLabel.Text = "Calcolo hash per : " + l_text
            ProgressBar1.Value = ProgressBar1.Value + intPercent

            'md5String = CreateMD5StringFromFile(l_text)
            sha256String = Hash256File(l_text)

            Console.WriteLine("Calcolo HASH per : {0} : {1} ", l_text, sha256String)
            testFile = My.Computer.FileSystem.GetFileInfo(l_text)
            folderPath = testFile.DirectoryName
            'MsgBox(folderPath)
            Dim fileName As String = testFile.Name
            'MsgBox(fileName)

            dictionary.Add(fileName, sha256String)
        Next



        ProgressBar1.Value = 90
        LogLabel.Text = "Creazione pdf in corso ..."

        pdfFileName = folderPath + "\" + pdfFileName

        CreatePdf(dictionary, pdfFileName, xmlFileName, folderPath)

        'getTimeStamp()

        'Dim md5String = CreateMD5StringFromFile("D:\tmp\Simple.pdf")
        'Debug.Print(" HASH " & md5String)

        'Hash256File("D:\tmp\Simple.pdf")

        ProgressBar1.Value = 100
        LogLabel.Text = "Operazione terminata: " + pdfFileName
        MsgBox("Operazione Terminata")

        End


    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        PropertyGrid1.SelectedObject = My.Settings
        'My.Settings.Save()
        For Each argument As String In My.Application.CommandLineArgs
            ' Add code here to use the argument. 
            ListBox1.Items.Add(argument)
            My.Settings.BatchMode = 1
        Next

        Me.lblFileDiConfigurazione.Text = My.Settings.XmlConfigFile

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then


            For Each file In OpenFileDialog1.FileNames
                ListBox1.Items.Add(file)
            Next file


            'Dim sr As New System.IO.StreamReader(OpenFileDialog1.FileName)
            'MessageBox.Show(sr.ReadToEnd)
            'sr.Close()
        End If
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles LogLabel.Click

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ReadXML()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        End
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Hash256File(My.Settings.XmlConfigFile)
        getTimeStamp()

    End Sub

    Public Sub New()

        ' Chiamata richiesta dalla finestra di progettazione.
        InitializeComponent()

        ' Aggiungere le eventuali istruzioni di inizializzazione dopo la chiamata a InitializeComponent().

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ListBox1.Items.Remove(ListBox1.SelectedItem)

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        My.Settings.Save()
    End Sub

    Private Sub Label2_Click_1(sender As Object, e As EventArgs)

    End Sub
End Class
