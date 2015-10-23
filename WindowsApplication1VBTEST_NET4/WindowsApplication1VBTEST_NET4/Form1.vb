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
        xmlFileName = My.Settings.DCC_FOLDER_NAME + "\" + My.Settings.XML_CONFIG_FILE

        'MsgBox("File di configurazione:" & xmlFileName)
        'Me.lblFileDiConfigurazione.Text = xmlFileName
        'MsgBox(ListBox1.Items.Count)
        If (ListBox1.Items.Count = 0) Then
            MsgBox("Aggiungere i file alla lista per la compilazione della copia conforme!")
            Return
        End If

        'Console.WriteLine(My.Settings.Connessione)
        'Console.WriteLine(My.Settings.Connessione)

        ' Adding command line arguments
        'For Each argument As String In My.Application.CommandLineArgs
        ' Add code here to use the argument. 
        'Next

        'MsgBox(CStr(timeStamp))
        Dim intPercent As Integer = Int(100 / (ListBox1.Items.Count + 1))
        ProgressBar1.Value = 0


        For l_index As Integer = 0 To ListBox1.Items.Count - 1
            Dim l_text As String = CStr(ListBox1.Items(l_index))


            LogLabel.Text = "Calcolo hash per : " + l_text
            LogLabel.Refresh()
            ProgressBar1.Value = ProgressBar1.Value + intPercent

            'md5String = CreateMD5StringFromFile(l_text)
            sha256String = Hash256File(l_text)

            Console.WriteLine("Calcolo HASH per : {0} : {1} ", l_text, sha256String)
            testFile = My.Computer.FileSystem.GetFileInfo(l_text)
            folderPath = testFile.DirectoryName
            'Dim lastWriteTime = testFile.LastWriteTimeUtc
            'Dim fileSize As String = CStr(testFile.Length)
            'MsgBox(folderPath)
            Dim fileName As String = testFile.Name
            'MsgBox(fileName)

            dictionary.Add(fileName, "SHA-256:" + sha256String + vbCrLf + "Data ultima modifica (UTC):" + CStr(testFile.LastWriteTimeUtc) + vbCrLf + "Dimensione (bytes):" + CStr(testFile.Length))
        Next



        ProgressBar1.Value = 90
        LogLabel.Text = "Creazione pdf in corso ... ATTENDERE!"
        LogLabel.Refresh()

        pdfFileName = folderPath + "\" + pdfFileName

        My.Application.Log.WriteEntry("pdfFileName " + pdfFileName)

        CreatePdf(dictionary, pdfFileName, xmlFileName, folderPath)

        'getTimeStamp()

        'Dim md5String = CreateMD5StringFromFile("D:\tmp\Simple.pdf")
        'Debug.Print(" HASH " & md5String)

        'Hash256File("D:\tmp\Simple.pdf")

        ProgressBar1.Value = 100
        LogLabel.Text = "Operazione terminata: " + pdfFileName
        MsgBox("Dichiarazione Copia Conforme Generata " + vbCrLf + pdfFileName)

        End


    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        My.Application.Log.WriteEntry("START APPLICATION!")

        'PropertyGrid1.SelectedObject = My.Settings
        'My.Settings.Save()
        For Each argument As String In My.Application.CommandLineArgs
            ' Add code here to use the argument. 
            ListBox1.Items.Add(argument)
            My.Settings.BATCH_MODE = 1
            Button1_Click(sender, e)

        Next

        'Me.lblFileDiConfigurazione.Text = My.Settings.XmlConfigFile

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

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        ReadXML()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        End
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        'Hash256File(My.Settings.XmlConfigFile)
        'getTimeStamp()
        Dim myFirstForm As frmOptions
        myFirstForm = New frmOptions
        myFirstForm.Show()
    End Sub

    Public Sub New()

        ' Chiamata richiesta dalla finestra di progettazione.
        InitializeComponent()

        ' Aggiungere le eventuali istruzioni di inizializzazione dopo la chiamata a InitializeComponent().

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ListBox1.Items.Remove(ListBox1.SelectedItem)

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs)
        My.Settings.Save()
    End Sub

    Private Sub Label2_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Dim myForm As frmOptions
        myForm = New frmOptions
        myForm.ShowDialog()
    End Sub

    Private Sub Label2_Click_2(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class
