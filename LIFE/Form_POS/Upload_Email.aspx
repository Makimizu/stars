<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload_Email.aspx.cs" Inherits="LIFE.Form_POS.Upload_Email" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload Status Kirim</title>

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <style>
        body {
            font-family: 'Segoe UI', sans-serif;
            background: #f9fafb;
        }
        .container {
            width: 450px;
            margin: 60px auto;
            background: #fff;
            padding: 30px;
            border-radius: 15px;
            box-shadow: 0 4px 20px rgba(0,0,0,0.1);
        }
        h2 {
            text-align: center;
            color: #333;
            margin-bottom: 25px;
        }
        .btn {
            background-color: #0078d7;
            color: white;
            border: none;
            padding: 10px 20px;
            width: 100%;
            border-radius: 8px;
            cursor: pointer;
            transition: 0.3s;
        }
        .btn:hover {
            background-color: #005a9e;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Upload Status Kirim</h2>

            <asp:FileUpload ID="FileUploadExcel" runat="server" />
            <br /><br />
            <asp:Button ID="BtnUpload" runat="server" CssClass="btn" Text="Upload File Excel" OnClientClick="return showLoading();" OnClick="BtnUpload_Click" />
        </div>
    </form>

    <script>
        function showLoading() {
            Swal.fire({
                title: 'Uploading...',
                text: 'Sedang memproses file, mohon tunggu sebentar.',
                allowOutsideClick: false,
                didOpen: () => { Swal.showLoading(); }
            });
            return true;
        }

        function showSuccess(filename) {
            Swal.fire({
                icon: 'success',
                title: 'Upload Berhasil!',
                html: 'File <b>' + filename + '</b> berhasil diimport ke database.',
                confirmButtonText: 'OK',
                confirmButtonColor: '#0078d7'
            });
        }

        function showError(msg) {
            Swal.fire({
                icon: 'error',
                title: 'Upload Gagal!',
                text: msg,
                confirmButtonText: 'OK',
                confirmButtonColor: '#d33'
            });
        }
    </script>
</body>
</html>
