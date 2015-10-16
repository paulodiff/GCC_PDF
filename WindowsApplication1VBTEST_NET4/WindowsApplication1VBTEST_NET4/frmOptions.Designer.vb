<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOptions
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
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid()
        Me.btnSaveOptions = New System.Windows.Forms.Button()
        Me.btnReadXML = New System.Windows.Forms.Button()
        Me.btnHash256 = New System.Windows.Forms.Button()
        Me.lblFileDiConfigurazione = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PropertyGrid1.Location = New System.Drawing.Point(12, 12)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.Size = New System.Drawing.Size(776, 226)
        Me.PropertyGrid1.TabIndex = 2
        '
        'btnSaveOptions
        '
        Me.btnSaveOptions.Location = New System.Drawing.Point(653, 289)
        Me.btnSaveOptions.Name = "btnSaveOptions"
        Me.btnSaveOptions.Size = New System.Drawing.Size(116, 24)
        Me.btnSaveOptions.TabIndex = 13
        Me.btnSaveOptions.Text = "Salva opzioni"
        Me.btnSaveOptions.UseVisualStyleBackColor = True
        '
        'btnReadXML
        '
        Me.btnReadXML.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnReadXML.Location = New System.Drawing.Point(401, 277)
        Me.btnReadXML.Name = "btnReadXML"
        Me.btnReadXML.Size = New System.Drawing.Size(109, 36)
        Me.btnReadXML.TabIndex = 14
        Me.btnReadXML.Text = "ReadXML"
        Me.btnReadXML.UseVisualStyleBackColor = True
        '
        'btnHash256
        '
        Me.btnHash256.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnHash256.Location = New System.Drawing.Point(240, 276)
        Me.btnHash256.Name = "btnHash256"
        Me.btnHash256.Size = New System.Drawing.Size(85, 37)
        Me.btnHash256.TabIndex = 15
        Me.btnHash256.Text = "HASH256"
        Me.btnHash256.UseVisualStyleBackColor = True
        '
        'lblFileDiConfigurazione
        '
        Me.lblFileDiConfigurazione.AutoSize = True
        Me.lblFileDiConfigurazione.ForeColor = System.Drawing.Color.Red
        Me.lblFileDiConfigurazione.Location = New System.Drawing.Point(12, 261)
        Me.lblFileDiConfigurazione.Name = "lblFileDiConfigurazione"
        Me.lblFileDiConfigurazione.Size = New System.Drawing.Size(106, 13)
        Me.lblFileDiConfigurazione.TabIndex = 16
        Me.lblFileDiConfigurazione.Text = "File di configurazione"
        '
        'frmOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 385)
        Me.Controls.Add(Me.lblFileDiConfigurazione)
        Me.Controls.Add(Me.btnHash256)
        Me.Controls.Add(Me.btnReadXML)
        Me.Controls.Add(Me.btnSaveOptions)
        Me.Controls.Add(Me.PropertyGrid1)
        Me.Name = "frmOptions"
        Me.Text = "frmOptions"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
    Friend WithEvents btnSaveOptions As System.Windows.Forms.Button
    Friend WithEvents btnReadXML As System.Windows.Forms.Button
    Friend WithEvents btnHash256 As System.Windows.Forms.Button
    Friend WithEvents lblFileDiConfigurazione As System.Windows.Forms.Label
End Class
