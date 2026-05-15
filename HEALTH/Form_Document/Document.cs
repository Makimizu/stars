using System;
using System.Globalization;
using System.IO;
using iText.Html2pdf;

namespace HEALTH.Form_Document
{
    public class Document
    {
        public static string Document_PKS_KLINIK(
            string tanggalpengajuan,
            string nomorsurat,
            string namaPerusahaan,
            string alamat,
            string namaRS)
        {
            string html = @"
<!DOCTYPE html>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
  <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
  <title>Perjanjian Kerjasama PT. Asuransi Takaful Keluarga dengan Klinik Provider</title>
  <style type='text/css'>
    body {
      font-family: Arial, sans-serif;
      font-size: 10pt;
      line-height: 1.6;
      margin: 15px;
      color: #333;
    }
    p {
        margin: 0 0 14px;
        text-align: justify;
    }
    .justify-list {
        padding-left: 1.2rem;
        margin: 0;
    }
    .justify-list li {
        margin-bottom: 0.5rem;
        text-align: justify;
        text-justify: inter-word;
        text-align-last: justify;
    }

    .underline {
      text-decoration: underline;
    }
    .center {
      text-align: center;
    }
    table {
      width: 100%;
      border-collapse: collapse;
      margin: 20px 0;
      table-layout: fixed;
    }
    table, th, td {
      border: 1px solid #ddd;
    }
    th, td {
      padding: 8px;
      text-align: left;
    }

    table th:first-child,
    table td:first-child {
      width: 60pt; /* pakai pt untuk Spire.Doc lebih stabil */
    }

    .center {
          text-align: center;          /* semua isi di tengah */
          font-family: 'Arial', serif;
        }
    .center p strong {
      font-weight: bold;           /* tebal */
    }
    .center p {
      text-align: center !important; /* override justify */
      margin: 6px 0;                 /* spasi antar baris */
    }

    .center p.small {
      font-size: 14px;
      font-style: italic;
    }
      .justify-ol li {
      text-align: justify;
      text-justify: inter-word;
    }

    .space {
      margin-top: 150px;
      margin-bottom: 150px;
    }
  </style>
</head>
<body>
    <div class='center'>
        <p><strong>*Bismillahirrahmanirrahiim*</strong></p>
        <p>PERJANJIAN KERJASAMA</p>
        <p>antara</p>
        <p>PT.ASURANSI TAKAFUL KELUARGA</p>
        <p>dengan</p>
        <p>{{NamaPerusahaan}}</p>
        <p>tentang</p>
        <p>PELAYANAN PENGOBATAN KESEHATAN DAN PENGOBATAN SECARA BERLANGGANAN</p>
        <p>Nomor : ............</p>
        <p>Nomor : ............</p>
    </div>

    <p>Perjanjian Kerjasama Program Pelayanan Pengobatan Kesehatan Secara Berlangganan (selanjutnya disebut sebagai 'Perjanjian') ini, dibuat pada hari ini, {{hari}} tanggal {{tanggal}} bulan {{bulan}} tahun {{tahun}}, oleh dan antara :</p>

    <p><strong>I. PT.ASURANSI TAKAFUL KELUARGA,</strong> berkedudukan di Jakarta dan berkantor pusat di Graha Takaful Indonesia, Jalan Mampang Prapatan Raya No. 100, Jakarta Selatan, dalam hal ini diwakili oleh<strong> Penny Hikmahwati</strong> selaku <strong>Direktur Operasional</strong>, berdasarkan Akta No. 24 tanggal 14 Agustus 2023 dibuat dihadapan Arry Supratno, SH., Notaris di Jakarta, dan telah mendapat penerimaan pemberitahuan dari Menteri Hukum dan Hak Asasi Manusia Republik Indonesia melalui surat No.AHU-AH.01.09-0153011 tanggal 18 Agustus 2023, dari dan karenanya sah bertindak mewakili Direksi PT. Asuransi Takaful Keluarga, untuk selanjutnya disebut disebut '<strong>PIHAK PERTAMA</strong>'.</p>

    <p><strong>II. {{NamaPerusahaan}}</strong> yang berkedudukan di beralamat di Jalan {{AlamatPerusahaan}} diwakili oleh <strong>.....................</strong> selaku Direktur ................, berdasarkan<strong> Surat Keputusan.........................pada tanggal......</strong> dari dan karenanya sah bertindak untuk dan atas nama<strong>..........................</strong>, selanjutnya disebut '<strong>PIHAK KEDUA</strong>'.</p>

    <p><strong>PIHAK PERTAMA dan PIHAK KEDUA</strong> (selanjutnya disebut 'PARA PIHAK') menerangkan terlebih dahulu sebagai berikut :</p>

    <ol class='justify-ol'>
        <li>PIHAK PERTAMA adalah perusahaan asuransi jiwa berdasarkan prinsip syariah, dengan ini bermaksud bekerja sama dengan PIHAK KEDUA untuk mendapatkan pelayanan pengobatan kesehatan bagi Peserta asuransi PIHAK PERTAMA.</li>
        <li>PIHAK PERTAMA selain mengelola administrasi klaim kesehatan secara mandiri, juga menunjuk TPA (Third Party Administrator) untuk melaksanakan pekerjaan pengelolaan administrasi klaim asuransi kesehatan pada provider yang ditunjuk dan disetujui oleh PIHAK PERTAMA dengan menggunakan sistem infrastruktur yang dimiliki oleh TPA tersebut.</li>
        <li>PIHAK KEDUA adalah Klinik Kesehatan yang dengan ini bersedia dan menyatakan sanggup untuk bekerjasama dengan PIHAK PERTAMA dengan segala fasilitasnya untuk memberikan pelayanan pengobatan kesehatan kepada Peserta asuransi PIHAK PERTAMA.</li>
    </ol>

    <p><strong>MAKA</strong>, berdasarkan hal-hal tersebut Pasal ini, PARA PIHAK telah saling bermufakat dan bersetuju untuk dan dengan ini menetapkan Perjanjian, berdasarkan syarat dan ketentuan sebagai berikut :</p>

    <p class='center'>PASAL 1<br/>DEFINISI</p>

    <p>Selain definisi yang disebutkan dalam bagian lain pada Perjanjian ini, istilah-istilah berikut yang diawali dengan huruf kapital dalam Perjanjian ini mempunyai arti seperti tercantum di bawah ini :</p>

    <ol class='justify-ol'>
        <li><strong>Asuransi Kesehatan</strong> adalah program asuransi kesehatan yang diselenggarakan oleh PIHAK PERTAMA bagi Peserta dan keluarganya untuk memperoleh pelayanan kesehatan pada PIHAK KEDUA sesuai dengan manfaat yang dipilihnya.</li>
        <li><strong>Hari Kalender</strong> adalah tiap-tiap hari dalam kalender termasuk hari libur Nasional yang ditetapkan oleh Pemerintah atau hari libur lainnya yang ditetapkan oleh PIHAK PERTAMA.</li>
        <li><strong>Hari Kerja</strong> adalah Hari Senin sampai dengan Jum'at kecuali hari Libur Nasional yang ditetapkan oleh pemerintah atau hari libur lainnya yang ditetapkan oleh PIHAK PERTAMA.</li>
        <li><strong>Kartu Peserta</strong> adalah Kartu pengenal Peserta yang dikeluarkan oleh PIHAK PERTAMA untuk memperoleh pelayanan kesehatan dari PIHAK KEDUA dan sebagai jaminan pembayaran oleh PIHAK PERTAMA atas pelayanan kesehatan yang telah diberikan PIHAK KEDUA kepada Peserta, yang dilengkapi nomor, nama Peserta, nama perusahaan, logo TPA dan tanggal berlaku.</li>
        <li><strong>Obat/Therapi</strong> adalah Semua jenis obat-obatan yang sesuai dengan kebutuhan medis yang dapat diberikan dengan diminum, disuntik, dioles, dihirup, atau diteteskan sesuai dengan Daftar Obat Esensial Nasional (DOEN), ISO atau MIMS yang diakui oleh Departemen Kesehatan dalam rangka penyembuhan atau pemeliharaan kesehatan kecuali dan tidak termasuk jenis obat yang bersifat makanan atau suplemen, obat-obatan yang masih bersifat percobaan atau hipotesa atau vitamin yang tidak sesuai dengan indikasi untuk penyembuhan penyakit.</li>
        <li><strong>Operasi</strong> adalah Tindakan medis spesialistis yang menggunakan sayatan pada organ tubuh dengan atau tanpa tenaga anestesi pada ruangan tertentu.</li>
        <li><strong>Perjanjian</strong> adalah Semua persyaratan dan kondisi yang disetujui, ditandatangani, dan ditetapkan oleh PARA PIHAK dalam Perjanjian ini, termasuk lampiran-lampiran, penambahan maupun perubahannya.</li>
        <li><strong>Peserta</strong> adalah Karyawan atau Karyawati perusahaan berikut dengan Tanggungan (Anak, Istri/Suami) ataupun perorangan yang didaftarkan menjadi Peserta Asuransi Kesehatan pada PIHAK PERTAMA.</li>
        <li><strong>Rawat Inap</strong> adalah Perawatan kesehatan yang harus dilaksanakan secara terus-menerus di dalam suatu Rumah Sakit sekurang-kurangnya selama 12 (dua belas) jam untuk pengobatan yang diperlukan sesuai dengan penyakit atau cedera yang dapat dijamin. Pelayanan Rawat Inap diberikan berdasarkan surat pengantar dari dokter umum atau dokter spesialis, kecuali dalam kasus gawat darurat<em>(emergency)</em>.</li>
        <li><strong>Rawat Jalan</strong> adalah Perawatan kesehatan yang harus dilaksanakan di dalam suatu Rumah Sakit/ Klinik yang diperlukan sesuai dengan penyakit atau cedera yang didasarkan atas indikasi medis dilaksanakan dalam hari yang sama dan tidak membutuhkan perawatan inap.</li>
        <li><strong>Surat Jaminan</strong> adalah Surat yang dikeluarkan oleh PIHAK PERTAMA yang dipergunakan sebagai alat bukti guna melaksanakan pelayanan kesehatan oleh PIHAK KEDUA sesuai dengan isi surat jaminan tersebut.</li>
        <li><strong>Tarif</strong> adalah Harga komponen atau tindakan yang dibebankan kepada PIHAK PERTAMA sebagai imbalan atas pelayanan yang diterima Peserta dari PIHAK KEDUA yang berlaku pada saat Peserta memperoleh pelayanan kesehatan yang setiap perubahannya akan diberitahukan kepada PIHAK PERTAMA dari waktu ke waktu.</li>
        <li><strong>TPA (Third Party Administrator)</strong> adalah suatu perusahaan yang bergerak di bidang jasa pelayanan administrasi kesehatan yang telah bekerjasama dengan PIHAK PERTAMA guna memberikan jasa pengelolaan administrasi klaim kepada PIHAK PERTAMA dengan menggunakan sistem dan infrastruktur untuk memeriksa keabsahan dan memasukkan data secara elektronik.</li>
        <li><strong>Clinical pathway</strong> adalah proses multidisiplin terkait perawatan pasien secara tepat waktu dengan sumber daya dan tatalaksana yang sesuai guna meningkatkan mutu pelayanan dan mencegah variasi tatalaksana yang tidak perlu.</li>
        <li><strong>Medical efficacy</strong> adalah ukuran seberapa efektif suatu pengobatan, prosedur medis, atau intervensi medis dalam mencapai hasil yang diinginkan dalam pengobatan atau suatu kondisi medis.</li>
        <li><strong>Utilization review</strong> adalah suatu metode untuk menjamin mutu pelayanan terkait penghematan biaya dengan mekanisme pengendalian biaya utilization review dengan memeriksa apakah pelayanan secara medis perlu diberikan dan apakah pelayanan diberikan secara tepat.</li>
    </ol>

    <p class='center'>PASAL 2<br/>PENUNJUKAN DAN PENETAPAN</p>

    <p>PIHAK PERTAMA menunjuk dan menetapkan PIHAK KEDUA dan PIHAK KEDUA dengan ini menerima penunjukan dan penetapan dari PIHAK PERTAMA sebagai Klinik yang menyelenggarakan pelayanan pengobatan kesehatan secara berlangganan kepada Peserta asuransi PIHAK PERTAMA.</p>

    <p class='center'>PASAL 3<br/>RUANG LINGKUP PERJANJIAN</p>

    <ol class='justify-ol'>
        <li>PIHAK KEDUA berkewajiban memberikan fasilitas pelayanan pengobatan kesehatan Rawat Jalan kepada Peserta asuransi PIHAK PERTAMA sesuai dengan Kartu Peserta Asuransi yang diterbitkan oleh PIHAK PERTAMA.</li>
        <li>Kartu kepesertaan asuransi sebagaimana dimaksud dalam ayat(1) Pasal ini terdiri Kartu Peserta asuransi yang melalui proses validasi dan verifikasi TPA selanjutnya disebut dengan '<strong>Proses TPA</strong>', yang berlaku untuk Rawat Jalan.Kartu kepesertaan yang tidak melalui proses TPA selanjutnya disebut dengan<strong>'Proses Takaful'</strong>.</li>
        <li>Kelas pengobatan dan fasilitas lainnya yang menjadi hak dari Peserta asuransi PIHAK PERTAMA tercantum dalam Kartu Peserta sebagaimana dimaksud dalam ayat (2) Pasal ini.</li>
    </ol>

    <p class='center'>PASAL 4<br/>PEMBEBASAN UANG PEMBAYARAN</p>

    <ol class='justify-ol'>
        <li>Dengan berlakunya Perjanjian ini, maka Peserta asuransi PIHAK PERTAMA yang memerlukan Pelayanan Pengobatan Kesehatan di Klinik PIHAK KEDUA dengan menunjukkan Kartu Peserta Asuransi dibebaskan dari prosedur pembayaran.</li>
        <li>Pembebasan prosedur pembayaran sebagaimana dimaksud ayat (1) Pasal ini diatur dengan ketentuan sebagai berikut :
            <ol type = 'a'>
                <li> Untuk Peserta yang menggunakan kartu Peserta dengan proses TPA, maka PIHAK PERTAMA akan menanggung biaya Rawat Jalan sesuai dengan batas/limit santunan Peserta, yang didapatkan setelah hasil proses TPA.Sedangkan untuk biaya yang telah melewati batas/limit manfaat merupakan tanggung jawab dari Peserta dan harus dibayarkan langsung ke PIHAK KEDUA pada saat selesai dari pelayanan.</li>
                <li>Untuk Peserta asuransi yang menggunakan kartu Peserta dengan proses Takaful, maka biaya pelayanan awat Jalan sepenuhnya ditanggung oleh Peserta dan wajib dibayarkan oleh Peserta kepada PIHAK KEDUA pada saat selesai perawatan<strong> kecuali untuk kartu Peserta belogo Takaful bertuliskan VIP Customer.</strong></li>
            </ol>
        </li>
        <li>Pembebasan prosedur pembayaran sebagaimana dimaksud ayat (1) dan(2) Pasal ini adalah dengan ketentuan PIHAK KEDUA tidak memberikan pelayanan yang bertentangan dalam Perjanjian ini dan/atau sesuai dengan jumlah santunan yang dimiliki oleh Peserta.</li>
    </ol>

    <p class='center'>PASAL 5<br/>FASILITAS YANG DISEDIAKAN</p>

    <p>Fasilitas pelayanan pengobatan kesehatan yang disediakan oleh PIHAK KEDUA adalah yang sesuai dengan hak Peserta asuransi PIHAK PERTAMA yang meliputi :</p>

    <ol class='justify-ol'>
        <li>Rawat Jalan :
            <ol type = 'a'>
                <li> Pemeriksaan dan pengobatan dokter umum/dokter spesialis;</li>
                <li>Pemeriksaan Laboratorium dan alat diagnostik lain atas indikasi medis;</li>
                <li>Pemberian obat-obatan yang sesuai dengan Daftar Obat Esensial Nasional(DOEN), ISO dan MIMS;</li>
                <li>Operasi kecil dengan anesthesi lokal tanpa penyulit yang tidak memerlukan tindakan Rawat Inap; dan</li>
                <li>Penggantian biaya imunisasi dasar dari dokter umum atau bidan bagi bayi berumur dibawah satu tahun meliputi : BCG, DPT, Polio dan campak.</li>
            </ol>
        </li>
        <li>Gigi, pemeriksaan dan pengobatan dokter gigi yang meliputi :
            <ol type = 'a'>
                <li> Pencabutan gigi tanpa penyulit(non impacted);</li>
                <li>Pengobatan syaraf gigi;</li>
                <li>Tambal gigi; dan</li>
                <li>Pembersihan karang gigi.</li>
            </ol>
        </li>
        <li>Obat-obatan, yang berhubungan dengan pengobatan dan sesuai dengan Daftar Obat Esensial Nasional(DOEN) ISO dan MIMS;.</li>
    </ol>

    <p class='center'>PASAL 6<br/>BIAYA PELAYANAN</p>

    <ol class='justify-ol'>
        <li>Tarif fasilitas pelayanan perawatan kesehatan dan pengobatan bagi Peserta asuransi PIHAK PERTAMA adalah yang berlaku pada PIHAK KEDUA.</li>
        <li>Apabila terjadi perubahan tarif, maka PIHAK KEDUA akan memberitahukan secara tertulis kepada PIHAK PERTAMA selambat-lambatnya 30 (tiga puluh) hari sebelum tarif baru diberlakukan.</li>
        <li>Jika perubahan tarif tersebut tidak diberitahukan kepada PIHAK PERTAMA, maka tarif yang berlaku adalah tarif yang dimiliki oleh PIHAK PERTAMA.</li>
        <li>Segala akibat yang timbul karena perubahan tarif yang tidak diberitahukan kepada PIHAK PERTAMA, sepenuhnya menjadi resiko dan tanggung jawab pihak PIHAK KEDUA.</li>
    </ol>

    <p class='center'>PASAL 7<br/>KETENTUAN PELAYANAN MEDIS</p>

    <p>PIHAK KEDUA dalam hal memberikan pelayanan pengobatan kesehatan wajib memenuhi ketentuan pelayanan medis yang ditetapkan oleh PIHAK PERTAMA, sebagai berikut :</p>

    <ol class='justify-ol'>
        <li>Memperhatikan Daftar Pengecualian yang tercantum pada bagian lampiran yang merupakan satu kesatuan dan bagian yang tak terpisahkan dari Perjanjian ini.</li>
        <li>Pemberian obat-obatan harus selalu berpedoman pada Daftar Obat Generik dan Daftar Obat Esensial Nasional (DOEN), ISO dan MIMS;</li>
    </ol>

    <p class='center'>PASAL 8<br/>TATA CARA PELAYANAN MEDIS</p>

    <ol class='justify-ol'>
        <li>PIHAK KEDUA akan memberikan pelayanan kesehatan kepada Peserta yang memiliki Kartu Peserta yang masih berlaku.PIHAK KEDUA wajib melakukan pengecekan dan validasi atas jenis kartu yang digunakan.</li>
        <li>Bilamana Peserta PIHAK PERTAMA menunjukkan Kartu Peserta dengan menggunakan sistem Takaful, maka untuk pelaksanaan pelayanan Rawat Jalan dilaksanakan dengan menggunakan sistem <em>reimbursement</em>, <strong>kecuali untuk kartu Peserta belogo Takaful bertuliskan VIP Customer.</strong></li>
        <li>Sebelum pemeriksaan dan/atau pengobatan terhadap Peserta, maka PIHAK KEDUA harus meminta Peserta asuransi PIHAK PERTAMA untuk :
            <ol type='a' class='nested'>
                <li> Dalam keadaan proses validasi TPA tidak bisa dilakukan / didapatkan(< em > off line </ em >), maka PIHAK KEDUA harus melakukan konfirmasi ke PIHAK PERTAMA pada jam kerja, dan/ atau melalui Administrasi TPA jika dalam kondisi di luar jam kerja/ hari libur dengan menyebutkan Identitas Peserta asuransi tersebut.</li>            
                <li> Setelah proses TPA dan pemeriksaan dan / atau pengobatan dilakukan maka selanjutnya adalah proses sistem TPA yang hasilnya adalah Surat Pengesahan dan Tagihan yang tercetak dari Sistem TPA.</li>                 
                <li> Peserta memberikan Kartu Peserta Asuransi yang diterbitkan oleh PIHAK PERTAMA atas nama yang bersangkutan yang masih berlaku, guna proses TPA dan pemeriksaan dan / atau pengobatan dapat dilakukan.</li>                      
            </ol>                      
            </li>
        <li>Setelah dilakukan pelayanan pemeriksaan dan/atau pengobatan, PIHAK KEDUA berkewajiban mengembalikan kartu Peserta asuransi atas nama yang bersangkutan kepada Peserta.</li>
    </ol>

    <p class='center'>PASAL 9<br/>TATA CARA PENGAJUAN PENAGIHAN</p>

    <ol class='justify-ol'>
        <li>PIHAK KEDUA akan mengirimkan nota tagihan dengan perinciannya kepada PIHAK PERTAMA setelah selesainya pengobatan Peserta.</li>
        <li>Kuitansi penagihan biaya pelayanan pemeriksaan dan/atau pengobatan dari PIHAK KEDUA kepada PIHAK PERTAMA wajib dilengkapi dengan dokumen-dokumen pendukung sebagai berikut :
            <ol type = 'a'>
                <li> Kuitansi Asli;</li>
                <li>Resume Medis/Diagnosa Medis;</li>
                <li>Bukti/Lampiran/Perincian(Obat, laboratorium, radiologi, rujukan dan diagnostik lain);</li>
                <li>Copy surat jaminan; dan</li>
                <li>Fotocopy Kartu Peserta.</li>
            </ol>
        </li>
        <li>Dokumen tagihan sebagaimaan dimaksud dalam ayat(2) pasal ini dikirim oleh PIHAK KEDUA kepada PIHAK PERTAMA pada alamat di bawah ini :
            <p><strong>PT.Asuransi Takaful Keluarga</strong><br/>
            Graha Takaful Indonesia<br/>
            Jl.Mampang Prapatan Raya no. 100<br/>
            Jakarta Selatan 122790</p>
        </li>
        <li>Dokumen tagihan yang menggunakan proses TPA, maka PIHAK KEDUA dapat mengirimkan langsung ke TPA tersebut.</li>
    </ol>

    <p class='center'>PASAL 10<br/>JANGKA WAKTU PENAGIHAN</p>

    <ol class='justify-ol'>
        <li>Jangka waktu penagihan biaya pemeriksaan dan/atau pengobatan dari PIHAK KEDUA kepada PIHAK PERTAMA selambat-lambatnya adalah 30 (tiga puluh) Hari Kalender setelah tanggal pelayanan pemeriksaan dan/atau pengobatan Peserta asuransi PIHAK PERTAMA di PIHAK KEDUA atau setelah lepas pengobatan rawat jalan dari PIHAK KEDUA.</li>
        <li>PIHAK PERTAMA tidak berkewajiban untuk melakukan pembayaran atas tagihan yang dikirimkan oleh PIHAK KEDUA setelah melewati jangka waktu sebagaimana ditentukan pada ayat 1 Pasal ini.</li>
    </ol>

    <p class='center'>PASAL 11<br/>PELAYANAN PESERTA</p>

    <p>PIHAK KEDUA melaksanakan pelayanan pengobatan kesehatan bagi Peserta asuransi PIHAK PERTAMA sesuai dengan ketentuan yang berlaku pada PIHAK PERTAMA.</p>

    <p class='center'>PASAL 12<br/>SISTEM PEMBAYARAN</p>

    <ol class='justify-ol'>
        <li>PIHAK PERTAMA wajib melakukan pembayaran atas biaya pengobatan Peserta kepada PIHAK KEDUA selambat-lambatnya 30 (tiga puluh) Hari Kalender sejak tagihan tersebut diterima oleh PIHAK PERTAMA secara lengkap dan benar.</li>
        <li>Setiap pembayaran dilakukan PIHAK PERTAMA kepada PIHAK KEDUA melalui transfer/pemindah-bukuan pada rekening Bank PIHAK KEDUA yaitu :
            <p>Nomor Rekening : <strong>..............................</strong><br/>
            Bank : <strong>..............................</strong><br/>
            Cabang : <strong>..............................</strong><br/>
            Atas nama : <strong>..............................</strong></p>
        </li>
    </ol>

    <p class='center'>PASAL 13<br/>TUGAS DAN KEWAJIBAN</p>

    <p>Selain yang diatur dalam Pasal-Pasal lain Perjanjian Kerjasama, PIHAK KEDUA memiliki Tugas dan Tanggung Jawab sebagai berikut :</p>

    <ol class='justify-ol'>
        <li>PIHAK KEDUA berkewajiban menjamin bahwa pelayanan dan/atau pengobatan medis yang diberikan kepada Peserta asuransi PIHAK PERTAMA adalah :
            <ol type = 'a'>
                <li> Konsisten dengan diagnosa dan prosedur pelayanan medis yang lazim untuk penyakit atau cidera yang membutuhkan pelayanan Rawat Jalan.</li>
                <li>Sesuai dengan standard pelayanan medis yang berlaku(Departemen Kesehatan Republik Indonesia).</li>
                <li>Tidak untuk dimanfaatkan secara negatif oleh Peserta asuransi PIHAK PERTAMA atau oknum PIHAK KEDUA.</li>
            </ol>
        </li>
        <li>PIHAK KEDUA berkewajiban mengambil semua tindakan yang sepantasnya guna mencegah penyalahgunaan dari limit santunan, termasuk tetapi tidak terbatas pada hal-hal dibawah ini :
            <ol type = 'a'>
                <li> Permintaan Peserta asuransi PIHAK PERTAMA untuk mengubah tanggal pemeriksaan dan/atau pengobatan ataupun diagnosa penyakit.</li>
                <li>Permintaan pelayanan medis seperti pemeriksaan laboratorium atau penunjang diagnostik lain yang tidak diperlukan secara medis.</li>
                <li>Permintaan untuk mengadakan tagihan sampai pada jumlah limit santunan untuk pelayanan yang diberikan kepada orang lain yang tidak sesuai dengan nama yang tertera pada Kartu Peserta Asuransi PIHAK PERTAMA.</li>
                <li>PIHAK KEDUA wajib memperhatikan Daftar Pengecualian yang tercantum pada bagian lampiran yang merupakan satu kesatuan yang tak terpisahkan dari Perjanjian ini.</li>
                <li>Dalam hal PIHAK PERTAMA membutuhkan laporan medis Peserta asuransinya dari PIHAK KEDUA, PIHAK PERTAMA menjamin bahwa telah memiliki otorisasi dari Peserta asuransinya untuk memperoleh laporan medisnya dari PIHAK KEDUA.</li>
            </ol>
        </li>
        <li>PIHAK KEDUA bertanggungjawab dan menjamin atas kebenaran keterangan medis Peserta asuransi PIHAK PERTAMA yang pertama kali diterima oleh PIHAK PERTAMA yang dituangkan/dinyatakan oleh PIHAK KEDUA dalam Formulir Pengobatan Medis setelah pasien meninggalkan Klinik, serta PIHAK PERTAMA dibebaskan dari segala tuntutan/gugatan dalam bentuk apapun dari PIHAK KEDUA atau pihak manapun atas kesalahan atau kelalaian dalam pengisian Formulir Pengobatan Medis.</li>
        <li>PIHAK KEDUA berkewajiban mengetahui jumlah biaya santunan Peserta PIHAK PERTAMA dan apabila terjadi selisih biaya perawatan dengan santunan atau biaya yang tidak mendapat penggantian dari PIHAK PERTAMA, maka PIHAK KEDUA wajib secara langsung menagih atas kekurangan biaya tersebut kepada Peserta sebelum Peserta meninggalkan klinik PIHAK KEDUA.</li>
    </ol>

    <p class='center'>PASAL 14<br/>JANGKA WAKTU PERJANJIAN</p>

    <ol class='justify-ol'>
        <li>Perjanjian berlaku dalam jangka waktu 3 (tiga) tahun, terhitung sejak tanggal ditandatangani dan dapat diakhiri sewaktu-waktu atau diadakan perubahan-perubahan berdasarkan persetujuan PARA PIHAK.</li>
        <li>Perjanjian ini diperpanjang secara otomatis untuk tahun-tahun berikutnya jika tidak ada permintaan perbatalan atau perubahan dari salah satu Pihak.</li>
        <li>Masing-masing PIHAK berhak untuk melakukan evaluasi atas pelaksanaan Perjanjian ini minimal 1 (satu) tahun sekali.</li>
        <li>Pihak yang menginginkan berakhirnya Perjanjian ini atau mengadakan perubahan-perubahan berkewajiban menyampaikan kepada pihak lainnya selambat-lambatnya 60 (enam puluh) hari sebelum tanggal berakhirnya Perjanjian atau tanggal dimulainya perubahan-perubahan yang dikehendaki.</li>
    </ol>

    <p class='center'>PASAL 15<br/>PEMUTUSAN/PEMBATALAN PERJANJIAN</p>

    <ol class='justify-ol'>
        <li>PIHAK PERTAMA maupun PIHAK KEDUA berhak secara sepihak dan tanpa melakukan tuntutan apapun untuk membatalkan sebagian atau seluruh dan bahkan bila dimungkinkan memutuskan Perjanjian ini dengan pemberitahuan secara tertulis terlebih dahulu, apabila masing-masing Pihak telah lalai memenuhi syarat-syarat dan ketentuan-ketentuan dari Perjanjian ini dan kelalaian itu tidak dapat diperbaiki dalam jangka waktu 30 (tiga puluh) hari sejak diterimanya surat pemberitahuan atau apabila tidak dapat melaksanakan kewajiban-kewajiban sehingga mengakibatkan dampak negatif yang sangat berarti menurut Perjanjian ini.</li>
        <li>Pemutusan/Pembatalan Perjanjian dapat juga dilakukan apabila baik PIHAK PERTAMA maupun PIHAK KEDUA tidak melaksanakan kewajiban sebagaimana tertuang dalam Perjanjian ini dan tidak memperbaiki kinerjanya setelah mendapat peringatan tertulis sebanyak 3 (tiga) kali secara berturut-turut dalam waktu 1 (satu) bulan.</li>
        <li>Dalam hal terjadi pemutusan/pembatalan Perjanjian karena tidak terpenuhinya kinerja salah satu pihak sebagaimana ketentuan ayat (2) Pasal ini maka PIHAK PERTAMA dan PIHAK KEDUA sepakat untuk merundingkan dan menyelesaikan kewajiban-kewajiban yang timbul dalam Perjanjian ini yang belum terselesaikan.</li>
    </ol>

    <p class='center'>PASAL 16<br/>PERNYATAAN DAN JAMINAN</p>

    <ol class='justify-ol'>
        <li>PARA PIHAK mempunyai tugas dan tanggung jawab yang sama untuk saling memberikan informasi atas setiap adanya perubahan informasi dan sistem atau prosedur yang menyangkut teknis pelaksanaan Perjanjian ini.</li>
        <li>PARA PIHAK menyatakan dan menjamin hal-hal sebagai berikut :
            <ol type = 'a'>
                <li> Untuk melaksanakan ketentuan-ketentuan dalam Perjanjian ini dengan penuh tanggung jawab dan atas dasar hubungan yang saling menguntungkan.</li>
                <li>Telah mempunyai kuasa dan wewenang penuh untuk mengikatkan diri dalam Perjanjian ini dan untuk mengambil semua tindakan yang diperlukan untuk penandatanganan serta pelaksanaan Perjanjian ini.</li>
                <li>Secara sah memegang semua perizinan, persetujuan yang berhubungan dan diperlukan, serta persetujuan-persetujuan lain yang mungkin dibutuhkan untuk melaksanakan usahanya, termasuk setiap izin, persetujuan-persetujuan lain yang berhubungan dengan kegiatan operasi dari pelayanan perbankan dan perasuransian yang disediakan berdasarkan Perjanjian ini.</li>
                <li>Penandatanganan dan pelaksanaan Perjanjian ini tidak bertentangan atau melanggar ketentuan hukum, peraturan, penetapan, keputusan administrasi atau hukum atau kebijakan pemerintah Indonesia atau departemen lainnya, perwakilan, badan-badan atau Pihak yang berwenang lainnya.</li>
            </ol>
        </li>
    </ol>

    <p class='center'>PASAL 17<br/>KEADAAN MEMAKSA ATAU FORCE MAJEURE</p>

    <ol class='justify-ol'>
        <li>Kewajiban salah satu Pihak dalam Perjanjian ini akan ditangguhkan sepanjang dan selama pelaksanaannya terhalang oleh peristiwa-peristiwa yang terjadi diluar kemampuan dan/atau kekuasaan PARA PIHAK antara lain namun tidak terbatas pada : persengketaan perburuhan, musibah/bencana alam, perubahan terhadap peraturan perundang-undangan, perang atau keadaan yang timbul dari atau sebagai akibat dari perang, baik yang dinyatakan maupun yang tidak, huru-hara atau tindakan sabotase oleh teroris atau tindak pidana lainnya, makar atau pemberontakan, kebakaran, peledakan, gempa bumi, badai, banjir, letusan gunung berapi, kekeringan atau kondisi cuaca yang luar biasa buruk, kecelakaan atau sebab-sebab lain yang sejenis yang untuk selanjutnya disebut Keadaan Memaksa atau<em> Force Majeure</em>.Ketentuan bahwa suatu peristiwa termasuk ke dalam Keadaan Memaksa atau<em>Force Majeure</em> adalah setelah diumumkan dan ditetapkan oleh pemerintah atau asosiasi, mana yang lebih dahulu.</li>
        <li>Dalam hal terjadi Keadaan Memaksa atau <em>Force Majeure</em> PARA PIHAK bersetuju bahwa pihak yang tidak terkena Keadaan Memaksa atau<em> Force Majeure</em> tidak dapat mengajukan tuntutan hukum terhadap pihak yang terkena Keadaan Memaksa atau<em> Force Majeure</em>.</li>
        <li>Pihak yang terkena Keadaan Memaksa atau <em>Force Majeure</em> harus segera, namun tidak lebih dari 14 (empat belas) Hari Kerja, memberitahukan kepada Pihak yang tidak terkena Keadaan Memaksa atau<em> Force Majeure</em> secara tertulis terhitung sejak terjadinya Keadaan Memaksa atau<em> Force Majeure</em> mengenai penangguhan pelaksanaan Perjanjian, alasannya dan perkiraan lamanya penangguhan.</li>
        <li>Apabila Pihak yang terkena Keadaan Memaksa atau<em> Force Majeure</em> tersebut lalai untuk dan tidak memberitahukan kepada Pihak yang tidak terkena Keadaan Memaksa atau<em> Force Majeure</em> dalam kurun waktu sebagaimana ditentukan dalam ayat (3) Pasal ini, maka seluruh kerugian, risiko dan konsekuensi yang mungkin timbul menjadi beban dan tanggung jawab Pihak yang terkena Keadaan Memaksa atau<em>Force Majeure</em> tersebut.</li>
        <li>Pihak yang terkena Keadaan Memaksa atau <em>Force Majeure</em> wajib berusaha semaksimal mungkin untuk memulai kembali pekerjaan dan/atau kewajiban lain dalam Perjanjian ini setelah keadaan <em>Force Majeure</em> selesai.</li>
    </ol>

    <p class='center'>PASAL 18<br/>PEMBERITAHUAN</p>

    <p>Setiap pemberitahuan yang diperlukan atau diberikan oleh satu pihak kepada pihak lainnya, dialamatkan kepada :</p>

    <table>
        <tr>
            <td><strong>PIHAK PERTAMA</strong><br/>
                <strong>PT.ASURANSI TAKAFUL KELUARGA</strong><br/>
                Graha Takaful Indonesia<br/>
                Jl. Mampang Prapatan Raya No. 100 Jakarta Selatan<br/>
                Telephone : 021 -- 7991234<br/>
                Layanan Peserta 24 Jam : 021-79190005<br/>
                Email : <a href = 'mailto:provrelation-atk@takaful.com'> provrelation-atk@takaful.com</a><br/>
                Kontak Person : Provider Team<br/>
                a.Untuk konfirmasi masalah Kerjasama Ext: 1113<br/>
                b.Untuk konfirmasi masalah Jaminan/Tindakan(Jam Kerja) Ext: 1002<br/>
                c.Billing dan Pembayaran (Keuangan) Ext: 1258<br/>
                d.Konfirmasi Claim Ext: 1095</td>
            <td><strong>PIHAK KEDUA</strong><br/>
                <strong>KLINIK....................</strong><br/>
                Alamat : <strong>....................</strong><br/>
                Telephone : <strong>....................</strong><br/>
                Email : <strong>....................</strong><br/>
                Kontak Person :<br/>
                PIC Marketing : <strong>.................................</strong><br/>
                PIC Medis : <strong>.................................</strong><br/>
                PIC Keuangan : <strong>.................................</strong><br/>
                Administrasi & Penjaminan : <strong>.................................</strong></td>
        </tr>
    </table>

    <p class='center'>PASAL 19<br/>HUKUM YANG BERLAKU</p>

    <p>Perjanjian ini tunduk dan wajib ditafsirkan menurut ketentuan dan peraturan perundang-undangan yang berlaku di wilayah Republik Indonesia.</p>

    <p class='center'>PASAL 20<br/>PENYELESAIAN PERSELISIHAN</p>

    <ol class='justify-ol'>
        <li>PARA PIHAK Sepakat bahwa setiap dan semua perselisihan yang mungkin timbul sebagai akibat dari penafsiran dan/atau pelaksanaan Perjanjian ini akan diselesaikan secara musyawarah untuk mufakat.</li>
        <li>PARA PIHAK sepakat bahwa bila dalam tenggang waktu selambat-lambatnya 30 (tiga puluh) Hari Kalender terhitung sejak tanggal terjadinya perselisihan, perselisihan tidak dapat diselesaikan secara musyawarah untuk mufakat, maka akan diselesaikan melalui Pengadilan Negeri Jakarta Selatan.</li>
    </ol>

    <p class='center'>PASAL 21<br/>KERAHASIAAN</p>

    <ol class='justify-ol'>
        <li>Kecuali diwajibkan oleh peraturan perundang-undangan yang berlaku, tidak ada satu pihakpun dalam Perjanjian ini yang dibenarkan untuk menginformasikan isi dari Perjanjian ini dan/atau memanfaatkan data-data yang digunakan dalam pelaksanaan Perjanjian ini baik yang bersifat teknis maupun komersial dalam bentuk apapun, selanjutnya disebut 'Informasi Rahasia'.</li>
        <li>Informasi Rahasia dapat disampaikan dan dipakai oleh PARA PIHAK, karyawan/pegawai PARA PIHAK maupun pihak lainnya yang diberi wewenang oleh dan memiliki kewenangan atas perusahaan PARA PIHAK, untuk mengetahui dan menggunakan Informasi Rahasia dengan ketentuan pihak-pihak lain tersebut telah mendapatkan persetujuan tertulis terlebih dahulu dari PARA PIHAK.</li>
    </ol>

    <p class='center'>PASAL 22<br/>KESELURUHAN PERJANJIAN</p>

    <ol class='justify-ol'>
        <li>Perjanjian ini merupakan keseluruhan Perjanjian antara PARA PIHAK berkenaan dengan materi yang diperjanjikan.</li>
        <li>Perjanjian ini membatalkan dan menggantikan kesepakatan yang dibuat sebelumnya oleh PARA PIHAK yang dilakukan secara lisan maupun tulisan.</li>
    </ol>

    <p class='center'>PASAL 23<br/>KETERPISAHAN</p>

    <ol class='justify-ol'>
        <li>Dalam hal suatu ketentuan yang terdapat dalam Perjanjian ini dinyatakan sebagai tidak sah atau tidak dapat diberlakukan secara hukum baik secara keseluruhan maupun sebagian, maka ketidaksahan atau ketidakberlakuan tersebut hanya berkaitan pada ketentuan ini atau sebagian daripadanya saja.Sedangkan ketentuan lainnya dari Perjanjian ini akan tetap berlaku dan mempunyai kekuatan hukum secara penuh.</li>
        <li>PARA PIHAK selanjutnya setuju bahwa terhadap ketentuan yang tidak sah atau tidak dapat diberlakukan tersebut sebagaimana dimaksud dalam ayat(1) Pasal ini akan diganti dengan ketentuan yang sah menurut hukum dan sejauh serta sedapat mungkin dapat mencerminkan maksud dan tujuan komersial atas dibuatnya ketentuan tersebut.</li>
    </ol>

    <p class='center'>PASAL 24<br/>PENGALIHAN HAK</p>

    <ol class='justify-ol'>
        <li>Hak dan kewajiban yang timbul berdasarkan Perjanjian ini tidak dapat dialihkan oleh salah satu pihak kepada siapapun tanpa persetujuan tertulis terlebih dahulu dari pihak lainnya.</li>
        <li>Setiap pihak yang menerima pengalihan hak wajib untuk menyetujui secara tertulis untuk mengikatkan diri pada ketentuan dalam Perjanjian ini secara keseluruhan tanpa ada yang dikecualikan.</li>
    </ol>

    <p class='center'>PASAL 25<br/>PERUBAHAN DAN TAMBAHAN</p>

    <ol class='justify-ol'>
        <li>Apabila dikemudian hari terdapat hal-hal yang belum diatur atau belum cukup diatur atau perlu dilakukan perubahan-perubahan atau perpanjangan jangka waktu dalam Perjanjian ini dan bila dipandang perlu, maka PARA PIHAK sepakat untuk menyatakan secara tertulis dalam amandemen atau addendum yang ditandatangani oleh PARA PIHAK dan merupakan bagian yang tidak terpisahkan dari Perjanjian ini.</li>
        <li>Tidak ada perubahan atau modifikasi atau penambahan pada Perjanjian ini yang dianggap sah atau mengikat PARA PIHAK, kecuali dinyatakan secara tertulis dan ditandatangani oleh PARA PIHAK.</li>
        <li>Dalam hal setelah ditandatanganinya Perjanjian ini terjadi suatu perubahan dalam peraturan perundang-undangan yang secara material dapat mendatangkan kerugian kepada PARA PIHAK, maka PARA PIHAK sepakat untuk mengadakan perundingan kembali sehingga dapat menghilangkan atau memperkecil kerugian yang diderita oleh salah satu pihak.</li>
    </ol>

    <p class='center'>PASAL 26<br/>PENUTUP</p>

    <p>Perjanjian ini dibuat dan ditandatangani dalam rangkap 2 (dua) asli, bermeterai cukup, masing-masing mempunyai bunyi dan kekuatan hukum yang sama, 1 (satu) asli untuk PIHAK PERTAMA dan 1 (satu) asli untuk PIHAK KEDUA, ditandatangani oleh PIHAK PERTAMA dan PIHAK KEDUA pada tanggal sebagaimana disebutkan pada awal Perjanjian ini.</p>

    <table>
        <tr>
            <td><strong>PIHAK PERTAMA</strong><br/>
                <strong>PT ASURANSI TAKAFUL KELUARGA</strong><br/>
                <span class='underline'><strong>Penny Hikmahwati</strong></span><br/>
                <strong>Direktur Operasional</strong></td>
            <td><strong>PIHAK KEDUA,</strong><br/>
                <strong>KLINIK.........................</strong><br/>
                <strong>.........................</strong><br/>
                <strong>Direktur</strong></td>
        </tr>
    </table>

    <p>Perjanjian Kerjasama antara PT. Asuransi Takaful Keluarga dengan <strong>.......................</strong> tentang Pelayanan Kesehatan dan Pengobatan Secara Berlangganan</p>

    <p>Lampiran :</p>
    <p>DAFTAR PENGECUALIAN</p>

    <p>PIHAK PERTAMA tidak memberikan jaminan untuk pelayanan-pelayanan berikut, dan menjadi beban dari Peserta :</p>

    <ol class='justify-ol'>
        <li>Akibat perang atau bertugas aktif dalam militer atau angkatan bersenjata dari suatu negara atau Badan Internasional, terlibat demonstrasi, huru-hara (langsung dan tidak langsung), pemberontakan, atau keributan sipil, perbuatan melawan atau melanggar hukum; bencana alam; radiasi dan kontaminasi yang bersifat massal.</li>
        <li>Cedera yang diakibatkan oleh perbuatan sendiri oleh /dengan bantuan pihak lain yang memiliki kepentingan dengan manfaat peserta, misalnya percobaan bunuh diri atau melanggar hukum dan terorisme.Cedera atau penyakit yang disebabkan oleh penggunaan alkohol. Narkotika, psikotropika atau zat adiktif lainnya.</li>
        <li>Olahraga tertentu yang membahayakan (panjat gunung/tebing, hang gliding, balap mobil/motor, diving, parasut, tinju, akrobatik, gantole, terbang layang dan sejenisnya).</li>
        <li>Segala kondisi yang berhubungan dengan penyakit yang ditularkan melalui hubungan seksual /golongan penyakit kelamin dan segala akibatnya.HIV, AIDS (Aquired Immune Defienciency Syndrome) dan ARC(AIDS Related Complex) dan segala akibatnya.</li>
        <li>Pengobatan dan tindakan medis yang masih dikategorikan eksperimen termasuk tapi tidak terbataspada Therapy Ozon, Hyperbaric Therapy(kecuali yang dilakukan oleh dokter Spesialis Kelautan), Chelation Therapy, Brainwash Therapy, Stemcell Therapy, tindakan Laser Eximer, pengobatan akupunktur(kecuali yang dilakukan oleh dokter Spesialis akupunktur), perawatan kesehatan di Spa, Health Hydros, dan tempat perawatan tradisional(alternatif).</li>
        <li>Pengobatan atau tindakan medis untuk Congenital(bawaan dari lahir) yang termasuk tapi tidak terbatas pada hernia dan epilepsy(khusus untuk Peserta di bawah usia 12 tahun), VSD, ASD, bibir sumbing, telapak kaki leper, pertumbuhan otot atau tulang secara tidak normal, cerebral palsy, hydrocephalus, dan cacat bawaan lainnya.</li>
        <li>Pengobatan atau tindakan medis untuk Kelainan herediter (penyakit keturunan) yang diakibatkankelainan jumlah kromosom termasuk tapi tidak terbatas pada debil, embicil, mongoloid, cretinism, thallasemia, haemophillia dan kelainan kromosom lainnya.</li>
        <li>Pengobatan atau tindakan medis untuk gangguan tumbuh kembang yang termasuk tapi tidak terbatas pada autisme, retardasi mental, ADHD, dan kelainan tumbuh kembang lainnya.</li>
        <li>Pemeriksaan kesehatan yang tidak ada hubungannya dengan pengobatan atau diagnosa dari suatu penyakit yang dijamin.</li>
        <li>Setiap pengobatan yang bukan berdasarkan indikasi medis atau tidak diperlukan secara medis, perawatan atau tindakan medis yang lebih bersifat kosmetik atau kenyamanan, termasuk tapi tidak terbatas pada pengobatan jerawat, tahi lalat, keloid, dry eyes, mata lelah, dan perawatan kosmetik atau kenyamanan lainnya.</li>
        <li>Pengobatan dan tindakan medis yang dilakukan oleh keluarga dekat Peserta atau oleh seseorang tinggal serumah atau bekerja sama dengan Peserta.</li>
        <li>Pengobatan dan tindakan medis yang tidak sesuai dengan benefit Peserta.</li>
        <li>Segala jenis upaya pencegahan penyakit termasuk tetapi tidak terbatas pada imunisasi/vaksinasi, kecuali di atur dalam manfaat imuninasi.</li>
        <li>Segala sesuatu yang berhubungan dengan kehamilan, segala penyakit yang berhubungan dengan kehamilan kecuali diatur dalam Manfaat tambahan Melahirkan, Segala sesuatu yang berhubungan dengan tindakan untuk mendapatkan kesuburan serta upaya pencegahan kehamilan termasuk tapi tidak terbatas pada pengobatan PCOS (Polycystic Ovary Syndrome) Inseminasi buatan dan tindakan untuk mendapatkan keturunan berikutnya.Segala sesuatu yang berhubungan dengan gangguan menstruasi (menstruasi disorder) akibat kelainan hormonal termasuk tapi tidak terbatas gangguan pre menopouse.</li>
        <li>Gangguan akibat sinar radio aktif dari setiap bahan bakar nuklir atau limbah nuklir, bencana alam (gempa bumi, banjir, letusan gunung berapi, badai tsunami dan sejenisnya).</li>
        <li>Penggantian protesa tangan, protesa mata, protesa kaki dan alat bantu pendengaran.</li>
        <li><em>Cosmetic Surgery</em> (operasi plastik) ataupun hal-hal yang berhubungan dengan upaya pemulihan perawatan kecantikan bagian tubuh.</li>
        <li>Alat pacu jantung, transplantasi organ tubuh termasuk sumsum tulang, akupuntur, Haemodialisa (cuci darah), pengobatan kanker, operasi jantung.</li>
        <li>Jasa-jasa non medis yang diberikan oleh rumah sakit, seperti biaya telpon, fax, salon, video, televisi, sauna, laundry, mini bar dan lain-lain.</li>
        <li>Pembelian obat-obatan tanpa resep Dokter, obat atau bahan yang tidak ada hubungannya dengan penyakit yang diderita termasuk bahan pembersih gigi, obat jerawat, obat-obatan untuk mempercantik diri dan obat-obatan tradisional/herbal atau penyegar mata.</li>
        <li>Semua bentuk multivitamin dan suplemen yang tidak memenuhi semua kriteria (hanya jika diresepkan oleh dokter; tidak diresepkan secara tunggal; berkorelasi dengan Penyakit yang dijamin dan sedang diderita; dalam jumlah yang wajar menurut penilaian Perusahaan; bertujuan untuk penyembuhan dan bukan untuk pencegahan; Bukan produk yang dipasarkan secara Multi Level Marketing).</li>
        <li>Pengobatan terhadap penyakit kejiwaan psikologis atau gangguan mental(<em>mental disorder</em>) dan gangguan psikologis lainnya termasuk semua gejala sisa atau sequele dari penyakit tersebut.</li>
    </ol>
   
    <p class='space'>&nbsp;</p>
    <p>Lampiran II : Contoh Kartu Peserta PT.Asuransi Takaful Keluarga</p>

    <table style='width:100%; table-layout:fixed; border-collapse:collapse;' 
        width='100%' 
        cellspacing='0' 
        cellpadding='0'>
        <tr>
        <th style='width:5%'>No</th>
        <th style='width:45%'>Kartu Peserta</th>
        <th style='width:50%'>Keterangan</th>
        </tr>
        <tr>
            <td>1</td>
            <td> <img src='{{KARTU1}}' style='max-width:300px; max-height:194px;' /></td>
            <td><strong>TAKAFUL-FULLERTON</strong><br/>
                <strong>Jaminan FULLERTON</strong><br/>
                021 -- 29976326<br/>
                <a href = 'mailto:case.managers@fullertonhealth.com'>case.managers @fullertonhealth.com</a></td>
        </tr>
        <tr>
            <td>2</td>
            <td> <img src='{{KARTU2}}' sstyle='max-width:300px; max-height:194px;' /></td>
            <td><strong>TAKAFUL-ADMEDIKA :</strong><br/>
                <strong>Jaminan ADMEDIKA</strong><br/>
                021-29647599<br/>
                <a href = 'mailto:takaful@admedika.co.id'> takaful@admedika.co.id</a></td>
        </tr>
        <tr>
            <td>3</td>
            <td> <img src='{{KARTU3}}' sstyle='max-width:300px; max-height:217px;' /></td>
            <td><strong>TAKAFUL -- VIP CUSTOMER</strong><br/>
                <strong>Jaminan TAKAFUL</strong><br/>
                021-79190005<br/>
                <a href = 'mailto:askes.penjaminan@takaful.com'> askes.penjaminan@takaful.com</a><br/>
                <strong>Seluruh biaya di jaminan Takaful</strong><br/>
                <a href = 'https://ecard.takaful.com/takafulsuratpenjaminan/'> https://ecard.takaful.com/takafulsuratpenjaminan/</a></td>
        </tr>
        <tr>
            <td>4</td>
            <td> <img src='{{KARTU4}}' sstyle='max-width:300px; max-height:296px;' /></td>
            <td><strong>TAKAFUL -- HALODOC</strong><br/>
                <strong>Jaminan Halodoc</strong><br/>
                021-39506663<br/>
                <a href = 'mailto:heidy@halodoc.com'> heidy@halodoc.com</a></td>
        </tr>
    </table>
</body>
</html>
";

            string[] parts = tanggalpengajuan.Split('-');

            string tanggal = parts[0]; // "01"
            string bulan = parts[1];   // "07"
            string tahun = parts[2];   // "2025"

            DateTime tanggalhari;
            DateTime.TryParseExact(tanggalpengajuan, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tanggalhari);

            string hari = tanggalhari.ToString("dddd", new CultureInfo("id-ID"));
            // hasil: "Selasa"

            return html
                .Replace("{{TanggalPengajuan}}", tanggalpengajuan)
                .Replace("{{NomorSurat}}", nomorsurat)
                .Replace("{{NamaPerusahaan}}", namaPerusahaan)
                .Replace("{{AlamatPerusahaan}}", alamat)
                .Replace("{{NamaRumahSakit}}", namaRS)
                .Replace("{{bulan}}", bulan)
                .Replace("{{tahun}}", tahun)
                .Replace("{{hari}}", hari)
                .Replace("{{tanggal}}", tanggal);
        }

        public static string Document_PKS_RumahSakit(
            string tanggalpengajuan,
            string nomorsurat,
            string namaPerusahaan,
            string alamat,
            string namaRS)
        {
            string html = @"
                <!DOCTYPE html>
                <html xmlns='http://www.w3.org/1999/xhtml'>
                <head>
                  <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
                  <title>Perjanjian Kerjasama PT. Asuransi Takaful Keluarga dengan Klinik Provider</title>
                    <style type='text/css'>
                    body {
                        font-family: Arial, sans-serif;
                        font-size: 10pt;
                        line-height: 1.6;
                        margin: 15px;
                        color: #333;
                    }
                    p {
                        margin: 0 0 14px;
                        text-align: justify;
                    }
                    .justify-list {
                        padding-left: 1.2rem;
                        margin: 0;
                    }
                    .justify-list li {
                        margin-bottom: 0.5rem;
                        text-align: justify;
                        text-justify: inter-word;
                        text-align-last: justify;
                    }

                    .underline {
                        text-decoration: underline;
                    }
                    .center {
                        text-align: center;
                    }
                    table {
                        width: 100%;
                        border-collapse: collapse;
                        margin: 20px 0;
                        table-layout: fixed;
                    }
                    table, th, td {
                        border: 1px solid #ddd;
                    }
                    th, td {
                        padding: 8px;
                        text-align: left;
                    }

                    table th:first-child,
                    table td:first-child {
                        width: 60pt; /* pakai pt untuk Spire.Doc lebih stabil */
                    }

                    .center {
                            text-align: center;          /* semua isi di tengah */
                            font-family: 'Arial', serif;
                        }
                    .center p strong {
                        font-weight: bold;           /* tebal */
                    }
                    .center p {
                        text-align: center !important; /* override justify */
                        margin: 6px 0;                 /* spasi antar baris */
                    }

                    .center p.small {
                        font-size: 14px;
                        font-style: italic;
                    }
                        .justify-ol li {
                        text-align: justify;
                        text-justify: inter-word;
                    }

                    .space {
                        margin-top: 150px;
                        margin-bottom: 150px;
                    }
                    </style>
                </head>
                <body>
                    <div class='center'>
                        <p><strong>*Bismillahirrahmanirrahiim*</strong></p>
                        <p>PERJANJIAN KERJASAMA</p>
                        <p>antara</p>
                        <p>PT.ASURANSI TAKAFUL KELUARGA</p>
                        <p>dengan</p>
                        <p{{NamaPerusahaan}} </p>
                        <p>tentang</p>
                        <p>PELAYANAN PENGOBATAN KESEHATAN DAN PENGOBATAN SECARA BERLANGGANAN</p>
                        <p>Nomor : ............</p>
                        <p>Nomor : ............</p>
                    </div>

                    <p>Perjanjian Kerjasama Program Pelayanan Pengobatan Kesehatan Secara Berlangganan (selanjutnya disebut sebagai 'Perjanjian') ini, dibuat pada hari ini, {{hari}} tanggal {{tanggal}} bulan {{bulan}} tahun {{tahun}}, oleh dan antara :</p>

                    <p><strong>I.PT.ASURANSI TAKAFUL KELUARGA,</strong> berkedudukan di Jakarta dan berkantor pusat di Graha Takaful Indonesia, Jalan Mampang Prapatan Raya No. 100, Jakarta Selatan, dalam hal ini diwakili oleh<strong> Penny Hikmahwati</strong> selaku <strong>Direktur Operasional</strong>, berdasarkan Akta No. 24 tanggal 14 Agustus 2023 dibuat dihadapan Arry Supratno, SH., Notaris di Jakarta, dan telah mendapat penerimaan pemberitahuan dari Menteri Hukum dan Hak Asasi Manusia Republik Indonesia melalui surat No.AHU-AH.01.09-0153011 tanggal 18 Agustus 2023, dari dan karenanya sah bertindak mewakili Direksi PT. Asuransi Takaful Keluarga, untuk selanjutnya disebut disebut '<strong>PIHAK PERTAMA</strong>'.</p>

                    <p><strong>II. ..............................</strong> yang berkedudukan di beralamat di Jalan {{AlamatPerusahaan}} diwakili oleh <strong>.....................</strong> selaku Direktur ................, berdasarkan<strong> Surat Keputusan.........................pada tanggal......</strong> dari dan karenanya sah bertindak untuk dan atas nama<strong>..........................</strong>, selanjutnya disebut '<strong>PIHAK KEDUA</strong>'.</p>

                    <p><strong>PIHAK PERTAMA dan PIHAK KEDUA</strong> (selanjutnya disebut 'PARA PIHAK') menerangkan terlebih dahulu sebagai berikut :</p>

                    <ol class='justify-ol'>
                        <li>PIHAK PERTAMA adalah perusahaan asuransi jiwa berdasarkan prinsip syariah, dengan ini bermaksud bekerja sama dengan PIHAK KEDUA untuk mendapatkan pelayanan pengobatan kesehatan bagi Peserta asuransi PIHAK PERTAMA.</li>
                        <li>PIHAK PERTAMA selain mengelola administrasi klaim kesehatan secara mandiri, juga menunjuk TPA (Third Party Administrator) untuk melaksanakan pekerjaan pengelolaan administrasi klaim asuransi kesehatan pada provider yang ditunjuk dan disetujui oleh PIHAK PERTAMA dengan menggunakan sistem infrastruktur yang dimiliki oleh TPA tersebut.</li>
                        <li>PIHAK KEDUA adalah Klinik Kesehatan yang dengan ini bersedia dan menyatakan sanggup untuk bekerjasama dengan PIHAK PERTAMA dengan segala fasilitasnya untuk memberikan pelayanan pengobatan kesehatan kepada Peserta asuransi PIHAK PERTAMA.</li>
                    </ol>

                    <p><strong>MAKA</strong>, berdasarkan hal-hal tersebut Pasal ini, PARA PIHAK telah saling bermufakat dan bersetuju untuk dan dengan ini menetapkan Perjanjian, berdasarkan syarat dan ketentuan sebagai berikut :</p>

                    <p class='center'>PASAL 1<br/>DEFINISI</p>

                    <p>Selain definisi yang disebutkan dalam bagian lain pada Perjanjian ini, istilah-istilah berikut yang diawali dengan huruf kapital dalam Perjanjian ini mempunyai arti seperti tercantum di bawah ini :</p>

                    <ol class='justify-ol'>
                        <li><strong>Asuransi Kesehatan</strong> adalah program asuransi kesehatan yang diselenggarakan oleh PIHAK PERTAMA bagi Peserta dan keluarganya untuk memperoleh pelayanan kesehatan pada PIHAK KEDUA sesuai dengan manfaat yang dipilihnya.</li>
                        <li><strong>Hari Kalender</strong> adalah tiap-tiap hari dalam kalender termasuk hari libur Nasional yang ditetapkan oleh Pemerintah atau hari libur lainnya yang ditetapkan oleh PIHAK PERTAMA.</li>
                        <li><strong>Hari Kerja</strong> adalah Hari Senin sampai dengan Jum'at kecuali hari Libur Nasional yang ditetapkan oleh pemerintah atau hari libur lainnya yang ditetapkan oleh PIHAK PERTAMA.</li>
                        <li><strong>Kartu Peserta</strong> adalah Kartu pengenal Peserta yang dikeluarkan oleh PIHAK PERTAMA untuk memperoleh pelayanan kesehatan dari PIHAK KEDUA dan sebagai jaminan pembayaran oleh PIHAK PERTAMA atas pelayanan kesehatan yang telah diberikan PIHAK KEDUA kepada Peserta, yang dilengkapi nomor, nama Peserta, nama perusahaan, logo TPA dan tanggal berlaku.</li>
                        <li><strong>Obat/Therapi</strong> adalah Semua jenis obat-obatan yang sesuai dengan kebutuhan medis yang dapat diberikan dengan diminum, disuntik, dioles, dihirup, atau diteteskan sesuai dengan Daftar Obat Esensial Nasional (DOEN), ISO atau MIMS yang diakui oleh Departemen Kesehatan dalam rangka penyembuhan atau pemeliharaan kesehatan kecuali dan tidak termasuk jenis obat yang bersifat makanan atau suplemen, obat-obatan yang masih bersifat percobaan atau hipotesa atau vitamin yang tidak sesuai dengan indikasi untuk penyembuhan penyakit.</li>
                        <li><strong>Operasi</strong> adalah Tindakan medis spesialistis yang menggunakan sayatan pada organ tubuh dengan atau tanpa tenaga anestesi pada ruangan tertentu.</li>
                        <li><strong>Perjanjian</strong> adalah Semua persyaratan dan kondisi yang disetujui, ditandatangani, dan ditetapkan oleh PARA PIHAK dalam Perjanjian ini, termasuk lampiran-lampiran, penambahan maupun perubahannya.</li>
                        <li><strong>Peserta</strong> adalah Karyawan atau Karyawati perusahaan berikut dengan Tanggungan (Anak, Istri/Suami) ataupun perorangan yang didaftarkan menjadi Peserta Asuransi Kesehatan pada PIHAK PERTAMA.</li>
                        <li><strong>Rawat Inap</strong> adalah Perawatan kesehatan yang harus dilaksanakan secara terus-menerus di dalam suatu Rumah Sakit sekurang-kurangnya selama 12 (dua belas) jam untuk pengobatan yang diperlukan sesuai dengan penyakit atau cedera yang dapat dijamin. Pelayanan Rawat Inap diberikan berdasarkan surat pengantar dari dokter umum atau dokter spesialis, kecuali dalam kasus gawat darurat<em>(emergency)</em>.</li>
                        <li><strong>Rawat Jalan</strong> adalah Perawatan kesehatan yang harus dilaksanakan di dalam suatu Rumah Sakit/ Klinik yang diperlukan sesuai dengan penyakit atau cedera yang didasarkan atas indikasi medis dilaksanakan dalam hari yang sama dan tidak membutuhkan perawatan inap.</li>
                        <li><strong>Surat Jaminan</strong> adalah Surat yang dikeluarkan oleh PIHAK PERTAMA yang dipergunakan sebagai alat bukti guna melaksanakan pelayanan kesehatan oleh PIHAK KEDUA sesuai dengan isi surat jaminan tersebut.</li>
                        <li><strong>Tarif</strong> adalah Harga komponen atau tindakan yang dibebankan kepada PIHAK PERTAMA sebagai imbalan atas pelayanan yang diterima Peserta dari PIHAK KEDUA yang berlaku pada saat Peserta memperoleh pelayanan kesehatan yang setiap perubahannya akan diberitahukan kepada PIHAK PERTAMA dari waktu ke waktu.</li>
                        <li><strong>TPA (Third Party Administrator)</strong> adalah suatu perusahaan yang bergerak di bidang jasa pelayanan administrasi kesehatan yang telah bekerjasama dengan PIHAK PERTAMA guna memberikan jasa pengelolaan administrasi klaim kepada PIHAK PERTAMA dengan menggunakan sistem dan infrastruktur untuk memeriksa keabsahan dan memasukkan data secara elektronik.</li>
                        <li><strong>Clinical pathway</strong> adalah proses multidisiplin terkait perawatan pasien secara tepat waktu dengan sumber daya dan tatalaksana yang sesuai guna meningkatkan mutu pelayanan dan mencegah variasi tatalaksana yang tidak perlu.</li>
                        <li><strong>Medical efficacy</strong> adalah ukuran seberapa efektif suatu pengobatan, prosedur medis, atau intervensi medis dalam mencapai hasil yang diinginkan dalam pengobatan atau suatu kondisi medis.</li>
                        <li><strong>Utilization review</strong> adalah suatu metode untuk menjamin mutu pelayanan terkait penghematan biaya dengan mekanisme pengendalian biaya utilization review dengan memeriksa apakah pelayanan secara medis perlu diberikan dan apakah pelayanan diberikan secara tepat.</li>
                    </ol>

                    <p class='center'>PASAL 2<br/>PENUNJUKAN DAN PENETAPAN</p>

                    <p>PIHAK PERTAMA menunjuk dan menetapkan PIHAK KEDUA dan PIHAK KEDUA dengan ini menerima penunjukan dan penetapan dari PIHAK PERTAMA sebagai Klinik yang menyelenggarakan pelayanan pengobatan kesehatan secara berlangganan kepada Peserta asuransi PIHAK PERTAMA.</p>

                    <p class='center'>PASAL 3<br/>RUANG LINGKUP PERJANJIAN</p>

                    <ol class='justify-ol'>
                        <li>PIHAK KEDUA berkewajiban memberikan fasilitas pelayanan pengobatan kesehatan Rawat Jalan kepada Peserta asuransi PIHAK PERTAMA sesuai dengan Kartu Peserta Asuransi yang diterbitkan oleh PIHAK PERTAMA.</li>
                        <li>Kartu kepesertaan asuransi sebagaimana dimaksud dalam ayat(1) Pasal ini terdiri Kartu Peserta asuransi yang melalui proses validasi dan verifikasi TPA selanjutnya disebut dengan '<strong>Proses TPA</strong>', yang berlaku untuk Rawat Jalan.Kartu kepesertaan yang tidak melalui proses TPA selanjutnya disebut dengan<strong>'Proses Takaful'</strong>.</li>
                        <li>Kelas pengobatan dan fasilitas lainnya yang menjadi hak dari Peserta asuransi PIHAK PERTAMA tercantum dalam Kartu Peserta sebagaimana dimaksud dalam ayat (2) Pasal ini.</li>
                    </ol>

                    <p class='center'>PASAL 4<br/>PEMBEBASAN UANG PEMBAYARAN</p>

                    <ol class='justify-ol'>
                        <li>Dengan berlakunya Perjanjian ini, maka Peserta asuransi PIHAK PERTAMA yang memerlukan Pelayanan Pengobatan Kesehatan di Klinik PIHAK KEDUA dengan menunjukkan Kartu Peserta Asuransi dibebaskan dari prosedur pembayaran.</li>
                        <li>Pembebasan prosedur pembayaran sebagaimana dimaksud ayat (1) Pasal ini diatur dengan ketentuan sebagai berikut :
                            <ol type = 'a'>
                                <li> Untuk Peserta yang menggunakan kartu Peserta dengan proses TPA, maka PIHAK PERTAMA akan menanggung biaya Rawat Jalan sesuai dengan batas/limit santunan Peserta, yang didapatkan setelah hasil proses TPA.Sedangkan untuk biaya yang telah melewati batas/limit manfaat merupakan tanggung jawab dari Peserta dan harus dibayarkan langsung ke PIHAK KEDUA pada saat selesai dari pelayanan.</li>
                                <li>Untuk Peserta asuransi yang menggunakan kartu Peserta dengan proses Takaful, maka biaya pelayanan awat Jalan sepenuhnya ditanggung oleh Peserta dan wajib dibayarkan oleh Peserta kepada PIHAK KEDUA pada saat selesai perawatan<strong> kecuali untuk kartu Peserta belogo Takaful bertuliskan VIP Customer.</strong></li>
                            </ol>
                        </li>
                        <li>Pembebasan prosedur pembayaran sebagaimana dimaksud ayat (1) dan(2) Pasal ini adalah dengan ketentuan PIHAK KEDUA tidak memberikan pelayanan yang bertentangan dalam Perjanjian ini dan/atau sesuai dengan jumlah santunan yang dimiliki oleh Peserta.</li>
                    </ol>

                    <p class='center'>PASAL 5<br/>FASILITAS YANG DISEDIAKAN</p>

                    <p>Fasilitas pelayanan pengobatan kesehatan yang disediakan oleh PIHAK KEDUA adalah yang sesuai dengan hak Peserta asuransi PIHAK PERTAMA yang meliputi :</p>

                    <ol class='justify-ol'>
                        <li>Rawat Jalan :
                            <ol type = 'a'>
                                <li> Pemeriksaan dan pengobatan dokter umum/dokter spesialis;</li>
                                <li>Pemeriksaan Laboratorium dan alat diagnostik lain atas indikasi medis;</li>
                                <li>Pemberian obat-obatan yang sesuai dengan Daftar Obat Esensial Nasional(DOEN), ISO dan MIMS;</li>
                                <li>Operasi kecil dengan anesthesi lokal tanpa penyulit yang tidak memerlukan tindakan Rawat Inap; dan</li>
                                <li>Penggantian biaya imunisasi dasar dari dokter umum atau bidan bagi bayi berumur dibawah satu tahun meliputi : BCG, DPT, Polio dan campak.</li>
                            </ol>
                        </li>
                        <li>Gigi, pemeriksaan dan pengobatan dokter gigi yang meliputi :
                            <ol type = 'a'>
                                <li> Pencabutan gigi tanpa penyulit(non impacted);</li>
                                <li>Pengobatan syaraf gigi;</li>
                                <li>Tambal gigi; dan</li>
                                <li>Pembersihan karang gigi.</li>
                            </ol>
                        </li>
                        <li>Obat-obatan, yang berhubungan dengan pengobatan dan sesuai dengan Daftar Obat Esensial Nasional(DOEN) ISO dan MIMS;.</li>
                    </ol>

                    <p class='center'>PASAL 6<br/>BIAYA PELAYANAN</p>

                    <ol class='justify-ol'>
                        <li>Tarif fasilitas pelayanan perawatan kesehatan dan pengobatan bagi Peserta asuransi PIHAK PERTAMA adalah yang berlaku pada PIHAK KEDUA.</li>
                        <li>Apabila terjadi perubahan tarif, maka PIHAK KEDUA akan memberitahukan secara tertulis kepada PIHAK PERTAMA selambat-lambatnya 30 (tiga puluh) hari sebelum tarif baru diberlakukan.</li>
                        <li>Jika perubahan tarif tersebut tidak diberitahukan kepada PIHAK PERTAMA, maka tarif yang berlaku adalah tarif yang dimiliki oleh PIHAK PERTAMA.</li>
                        <li>Segala akibat yang timbul karena perubahan tarif yang tidak diberitahukan kepada PIHAK PERTAMA, sepenuhnya menjadi resiko dan tanggung jawab pihak PIHAK KEDUA.</li>
                    </ol>

                    <p class='center'>PASAL 7<br/>KETENTUAN PELAYANAN MEDIS</p>

                    <p>PIHAK KEDUA dalam hal memberikan pelayanan pengobatan kesehatan wajib memenuhi ketentuan pelayanan medis yang ditetapkan oleh PIHAK PERTAMA, sebagai berikut :</p>

                    <ol class='justify-ol'>
                        <li>Memperhatikan Daftar Pengecualian yang tercantum pada bagian lampiran yang merupakan satu kesatuan dan bagian yang tak terpisahkan dari Perjanjian ini.</li>
                        <li>Pemberian obat-obatan harus selalu berpedoman pada Daftar Obat Generik dan Daftar Obat Esensial Nasional (DOEN), ISO dan MIMS;</li>
                    </ol>

                    <p class='center'>PASAL 8<br/>TATA CARA PELAYANAN MEDIS</p>

                    <ol class='justify-ol'>
                        <li>PIHAK KEDUA akan memberikan pelayanan kesehatan kepada Peserta yang memiliki Kartu Peserta yang masih berlaku.PIHAK KEDUA wajib melakukan pengecekan dan validasi atas jenis kartu yang digunakan.</li>
                        <li>Bilamana Peserta PIHAK PERTAMA menunjukkan Kartu Peserta dengan menggunakan sistem Takaful, maka untuk pelaksanaan pelayanan Rawat Jalan dilaksanakan dengan menggunakan sistem <em>reimbursement</em>, <strong>kecuali untuk kartu Peserta belogo Takaful bertuliskan VIP Customer.</strong></li>
                        <li>Sebelum pemeriksaan dan/atau pengobatan terhadap Peserta, maka PIHAK KEDUA harus meminta Peserta asuransi PIHAK PERTAMA untuk :
                            <table>
                                <tr>
                                    <td>a.</td>
                                    <td>Dalam keadaan proses validasi TPA tidak bisa dilakukan/didapatkan (<em>off line</em>), maka PIHAK KEDUA harus melakukan konfirmasi ke PIHAK PERTAMA pada jam kerja, dan/atau melalui Administrasi TPA jika dalam kondisi diluar jam kerja/hari libur dengan menyebutkan Identitas Peserta asuransi tersebut.</td>
                                </tr>
                                <tr>
                                    <td>b.</td>
                                    <td>Setelah proses TPA dan pemeriksaan dan/atau pengobatan dilakukan maka selanjutnya adalah proses sistem TPA yang hasilnya adalah Surat Pengesahan dan Tagihan yang tercetak dari Sistem TPA.</td>
                                </tr>
                                <tr>
                                    <td>c.</td>
                                    <td>Peserta memberikan Kartu Peserta Asuransi yang diterbitkan oleh PIHAK PERTAMA atas nama yang bersangkutan yang masih berlaku, guna proses TPA dan pemeriksaan dan/atau pengobatan dapat dilakukan.</td>
                                </tr>
                            </table>
                        </li>
                        <li>Setelah dilakukan pelayanan pemeriksaan dan/atau pengobatan, PIHAK KEDUA berkewajiban mengembalikan kartu Peserta asuransi atas nama yang bersangkutan kepada Peserta.</li>
                    </ol>

                    <p class='center'>PASAL 9<br/>TATA CARA PENGAJUAN PENAGIHAN</p>

                    <ol class='justify-ol'>
                        <li>PIHAK KEDUA akan mengirimkan nota tagihan dengan perinciannya kepada PIHAK PERTAMA setelah selesainya pengobatan Peserta.</li>
                        <li>Kuitansi penagihan biaya pelayanan pemeriksaan dan/atau pengobatan dari PIHAK KEDUA kepada PIHAK PERTAMA wajib dilengkapi dengan dokumen-dokumen pendukung sebagai berikut :
                            <ol type = 'a'>
                                <li> Kuitansi Asli;</li>
                                <li>Resume Medis/Diagnosa Medis;</li>
                                <li>Bukti/Lampiran/Perincian(Obat, laboratorium, radiologi, rujukan dan diagnostik lain);</li>
                                <li>Copy surat jaminan; dan</li>
                                <li>Fotocopy Kartu Peserta.</li>
                            </ol>
                        </li>
                        <li>Dokumen tagihan sebagaimaan dimaksud dalam ayat(2) pasal ini dikirim oleh PIHAK KEDUA kepada PIHAK PERTAMA pada alamat di bawah ini :
                            <p><strong>PT.Asuransi Takaful Keluarga</strong><br/>
                            Graha Takaful Indonesia<br/>
                            Jl.Mampang Prapatan Raya no. 100<br/>
                            Jakarta Selatan 122790</p>
                        </li>
                        <li>Dokumen tagihan yang menggunakan proses TPA, maka PIHAK KEDUA dapat mengirimkan langsung ke TPA tersebut.</li>
                    </ol>

                    <p class='center'>PASAL 10<br/>JANGKA WAKTU PENAGIHAN</p>
 
                    <ol class='justify-ol'>
                        <li>Jangka waktu penagihan biaya pemeriksaan dan/atau pengobatan dari PIHAK KEDUA kepada PIHAK PERTAMA selambat-lambatnya adalah 30 (tiga puluh) Hari Kalender setelah tanggal pelayanan pemeriksaan dan/atau pengobatan Peserta asuransi PIHAK PERTAMA di PIHAK KEDUA atau setelah lepas pengobatan rawat jalan dari PIHAK KEDUA.</li>
                        <li>PIHAK PERTAMA tidak berkewajiban untuk melakukan pembayaran atas tagihan yang dikirimkan oleh PIHAK KEDUA setelah melewati jangka waktu sebagaimana ditentukan pada ayat 1 Pasal ini.</li>
                    </ol>

                    <p class='center'>PASAL 11<br/>PELAYANAN PESERTA</p>

                    <p>PIHAK KEDUA melaksanakan pelayanan pengobatan kesehatan bagi Peserta asuransi PIHAK PERTAMA sesuai dengan ketentuan yang berlaku pada PIHAK PERTAMA.</p>

                    <p class='center'>PASAL 12<br/>SISTEM PEMBAYARAN</p>

                    <ol class='justify-ol'>
                        <li>PIHAK PERTAMA wajib melakukan pembayaran atas biaya pengobatan Peserta kepada PIHAK KEDUA selambat-lambatnya 30 (tiga puluh) Hari Kalender sejak tagihan tersebut diterima oleh PIHAK PERTAMA secara lengkap dan benar.</li>
                        <li>Setiap pembayaran dilakukan PIHAK PERTAMA kepada PIHAK KEDUA melalui transfer/pemindah-bukuan pada rekening Bank PIHAK KEDUA yaitu :
                            <p>Nomor Rekening : <strong>..............................</strong><br/>
                            Bank : <strong>..............................</strong><br/>
                            Cabang : <strong>..............................</strong><br/>
                            Atas nama : <strong>..............................</strong></p>
                        </li>
                    </ol>

                    <p class='center'>PASAL 13<br/>TUGAS DAN KEWAJIBAN</p>

                    <p>Selain yang diatur dalam Pasal-Pasal lain Perjanjian Kerjasama, PIHAK KEDUA memiliki Tugas dan Tanggung Jawab sebagai berikut :</p>

                    <ol class='justify-ol'>
                        <li>PIHAK KEDUA berkewajiban menjamin bahwa pelayanan dan/atau pengobatan medis yang diberikan kepada Peserta asuransi PIHAK PERTAMA adalah :
                            <ol type = 'a'>
                                <li> Konsisten dengan diagnosa dan prosedur pelayanan medis yang lazim untuk penyakit atau cidera yang membutuhkan pelayanan Rawat Jalan.</li>
                                <li>Sesuai dengan standard pelayanan medis yang berlaku(Departemen Kesehatan Republik Indonesia).</li>
                                <li>Tidak untuk dimanfaatkan secara negatif oleh Peserta asuransi PIHAK PERTAMA atau oknum PIHAK KEDUA.</li>
                            </ol>
                        </li>
                        <li>PIHAK KEDUA berkewajiban mengambil semua tindakan yang sepantasnya guna mencegah penyalahgunaan dari limit santunan, termasuk tetapi tidak terbatas pada hal-hal dibawah ini :
                            <ol type = 'a'>
                                <li> Permintaan Peserta asuransi PIHAK PERTAMA untuk mengubah tanggal pemeriksaan dan/atau pengobatan ataupun diagnosa penyakit.</li>
                                <li>Permintaan pelayanan medis seperti pemeriksaan laboratorium atau penunjang diagnostik lain yang tidak diperlukan secara medis.</li>
                                <li>Permintaan untuk mengadakan tagihan sampai pada jumlah limit santunan untuk pelayanan yang diberikan kepada orang lain yang tidak sesuai dengan nama yang tertera pada Kartu Peserta Asuransi PIHAK PERTAMA.</li>
                                <li>PIHAK KEDUA wajib memperhatikan Daftar Pengecualian yang tercantum pada bagian lampiran yang merupakan satu kesatuan yang tak terpisahkan dari Perjanjian ini.</li>
                                <li>Dalam hal PIHAK PERTAMA membutuhkan laporan medis Peserta asuransinya dari PIHAK KEDUA, PIHAK PERTAMA menjamin bahwa telah memiliki otorisasi dari Peserta asuransinya untuk memperoleh laporan medisnya dari PIHAK KEDUA.</li>
                            </ol>
                        </li>
                        <li>PIHAK KEDUA bertanggungjawab dan menjamin atas kebenaran keterangan medis Peserta asuransi PIHAK PERTAMA yang pertama kali diterima oleh PIHAK PERTAMA yang dituangkan/dinyatakan oleh PIHAK KEDUA dalam Formulir Pengobatan Medis setelah pasien meninggalkan Klinik, serta PIHAK PERTAMA dibebaskan dari segala tuntutan/gugatan dalam bentuk apapun dari PIHAK KEDUA atau pihak manapun atas kesalahan atau kelalaian dalam pengisian Formulir Pengobatan Medis.</li>
                        <li>PIHAK KEDUA berkewajiban mengetahui jumlah biaya santunan Peserta PIHAK PERTAMA dan apabila terjadi selisih biaya perawatan dengan santunan atau biaya yang tidak mendapat penggantian dari PIHAK PERTAMA, maka PIHAK KEDUA wajib secara langsung menagih atas kekurangan biaya tersebut kepada Peserta sebelum Peserta meninggalkan klinik PIHAK KEDUA.</li>
                    </ol>

                    <p class='center'>PASAL 14<br/>JANGKA WAKTU PERJANJIAN</p>

                    <ol class='justify-ol'>
                        <li>Perjanjian berlaku dalam jangka waktu 3 (tiga) tahun, terhitung sejak tanggal ditandatangani dan dapat diakhiri sewaktu-waktu atau diadakan perubahan-perubahan berdasarkan persetujuan PARA PIHAK.</li>
                        <li>Perjanjian ini diperpanjang secara otomatis untuk tahun-tahun berikutnya jika tidak ada permintaan perbatalan atau perubahan dari salah satu Pihak.</li>
                        <li>Masing-masing PIHAK berhak untuk melakukan evaluasi atas pelaksanaan Perjanjian ini minimal 1 (satu) tahun sekali.</li>
                        <li>Pihak yang menginginkan berakhirnya Perjanjian ini atau mengadakan perubahan-perubahan berkewajiban menyampaikan kepada pihak lainnya selambat-lambatnya 60 (enam puluh) hari sebelum tanggal berakhirnya Perjanjian atau tanggal dimulainya perubahan-perubahan yang dikehendaki.</li>
                    </ol>

                    <p class='center'>PASAL 15<br/>PEMUTUSAN/PEMBATALAN PERJANJIAN</p>

                    <ol class='justify-ol'>
                        <li>PIHAK PERTAMA maupun PIHAK KEDUA berhak secara sepihak dan tanpa melakukan tuntutan apapun untuk membatalkan sebagian atau seluruh dan bahkan bila dimungkinkan memutuskan Perjanjian ini dengan pemberitahuan secara tertulis terlebih dahulu, apabila masing-masing Pihak telah lalai memenuhi syarat-syarat dan ketentuan-ketentuan dari Perjanjian ini dan kelalaian itu tidak dapat diperbaiki dalam jangka waktu 30 (tiga puluh) hari sejak diterimanya surat pemberitahuan atau apabila tidak dapat melaksanakan kewajiban-kewajiban sehingga mengakibatkan dampak negatif yang sangat berarti menurut Perjanjian ini.</li>
                        <li>Pemutusan/Pembatalan Perjanjian dapat juga dilakukan apabila baik PIHAK PERTAMA maupun PIHAK KEDUA tidak melaksanakan kewajiban sebagaimana tertuang dalam Perjanjian ini dan tidak memperbaiki kinerjanya setelah mendapat peringatan tertulis sebanyak 3 (tiga) kali secara berturut-turut dalam waktu 1 (satu) bulan.</li>
                        <li>Dalam hal terjadi pemutusan/pembatalan Perjanjian karena tidak terpenuhinya kinerja salah satu pihak sebagaimana ketentuan ayat (2) Pasal ini maka PIHAK PERTAMA dan PIHAK KEDUA sepakat untuk merundingkan dan menyelesaikan kewajiban-kewajiban yang timbul dalam Perjanjian ini yang belum terselesaikan.</li>
                    </ol>

                    <p class='center'>PASAL 16<br/>PERNYATAAN DAN JAMINAN</p>

                    <ol class='justify-ol'>
                        <li>PARA PIHAK mempunyai tugas dan tanggung jawab yang sama untuk saling memberikan informasi atas setiap adanya perubahan informasi dan sistem atau prosedur yang menyangkut teknis pelaksanaan Perjanjian ini.</li>
                        <li>PARA PIHAK menyatakan dan menjamin hal-hal sebagai berikut :
                            <ol type = 'a'>
                                <li> Untuk melaksanakan ketentuan-ketentuan dalam Perjanjian ini dengan penuh tanggung jawab dan atas dasar hubungan yang saling menguntungkan.</li>
                                <li>Telah mempunyai kuasa dan wewenang penuh untuk mengikatkan diri dalam Perjanjian ini dan untuk mengambil semua tindakan yang diperlukan untuk penandatanganan serta pelaksanaan Perjanjian ini.</li>
                                <li>Secara sah memegang semua perizinan, persetujuan yang berhubungan dan diperlukan, serta persetujuan-persetujuan lain yang mungkin dibutuhkan untuk melaksanakan usahanya, termasuk setiap izin, persetujuan-persetujuan lain yang berhubungan dengan kegiatan operasi dari pelayanan perbankan dan perasuransian yang disediakan berdasarkan Perjanjian ini.</li>
                                <li>Penandatanganan dan pelaksanaan Perjanjian ini tidak bertentangan atau melanggar ketentuan hukum, peraturan, penetapan, keputusan administrasi atau hukum atau kebijakan pemerintah Indonesia atau departemen lainnya, perwakilan, badan-badan atau Pihak yang berwenang lainnya.</li>
                            </ol>
                        </li>
                    </ol>

                    <p class='center'>PASAL 17<br/>KEADAAN MEMAKSA ATAU FORCE MAJEURE</p>

                    <ol class='justify-ol'>
                        <li>Kewajiban salah satu Pihak dalam Perjanjian ini akan ditangguhkan sepanjang dan selama pelaksanaannya terhalang oleh peristiwa-peristiwa yang terjadi diluar kemampuan dan/atau kekuasaan PARA PIHAK antara lain namun tidak terbatas pada : persengketaan perburuhan, musibah/bencana alam, perubahan terhadap peraturan perundang-undangan, perang atau keadaan yang timbul dari atau sebagai akibat dari perang, baik yang dinyatakan maupun yang tidak, huru-hara atau tindakan sabotase oleh teroris atau tindak pidana lainnya, makar atau pemberontakan, kebakaran, peledakan, gempa bumi, badai, banjir, letusan gunung berapi, kekeringan atau kondisi cuaca yang luar biasa buruk, kecelakaan atau sebab-sebab lain yang sejenis yang untuk selanjutnya disebut Keadaan Memaksa atau<em> Force Majeure</em>.Ketentuan bahwa suatu peristiwa termasuk ke dalam Keadaan Memaksa atau<em>Force Majeure</em> adalah setelah diumumkan dan ditetapkan oleh pemerintah atau asosiasi, mana yang lebih dahulu.</li>
                        <li>Dalam hal terjadi Keadaan Memaksa atau <em>Force Majeure</em> PARA PIHAK bersetuju bahwa pihak yang tidak terkena Keadaan Memaksa atau<em> Force Majeure</em> tidak dapat mengajukan tuntutan hukum terhadap pihak yang terkena Keadaan Memaksa atau<em> Force Majeure</em>.</li>
                        <li>Pihak yang terkena Keadaan Memaksa atau <em>Force Majeure</em> harus segera, namun tidak lebih dari 14 (empat belas) Hari Kerja, memberitahukan kepada Pihak yang tidak terkena Keadaan Memaksa atau<em> Force Majeure</em> secara tertulis terhitung sejak terjadinya Keadaan Memaksa atau<em> Force Majeure</em> mengenai penangguhan pelaksanaan Perjanjian, alasannya dan perkiraan lamanya penangguhan.</li>
                        <li>Apabila Pihak yang terkena Keadaan Memaksa atau<em> Force Majeure</em> tersebut lalai untuk dan tidak memberitahukan kepada Pihak yang tidak terkena Keadaan Memaksa atau<em> Force Majeure</em> dalam kurun waktu sebagaimana ditentukan dalam ayat (3) Pasal ini, maka seluruh kerugian, risiko dan konsekuensi yang mungkin timbul menjadi beban dan tanggung jawab Pihak yang terkena Keadaan Memaksa atau<em>Force Majeure</em> tersebut.</li>
                        <li>Pihak yang terkena Keadaan Memaksa atau <em>Force Majeure</em> wajib berusaha semaksimal mungkin untuk memulai kembali pekerjaan dan/atau kewajiban lain dalam Perjanjian ini setelah keadaan <em>Force Majeure</em> selesai.</li>
                    </ol>

                    <p class='center'>PASAL 18<br/>PEMBERITAHUAN</p>

                    <p>Setiap pemberitahuan yang diperlukan atau diberikan oleh satu pihak kepada pihak lainnya, dialamatkan kepada :</p>

                    <table>
                        <tr>
                            <td><strong>PIHAK PERTAMA</strong><br/>
                                <strong>PT.ASURANSI TAKAFUL KELUARGA</strong><br/>
                                Graha Takaful Indonesia<br/>
                                Jl. Mampang Prapatan Raya No. 100 Jakarta Selatan<br/>
                                Telephone : 021 -- 7991234<br/>
                                Layanan Peserta 24 Jam : 021-79190005<br/>
                                Email : <a href = 'mailto:provrelation-atk@takaful.com'> provrelation-atk@takaful.com</a><br/>
                                Kontak Person : Provider Team<br/>
                                a.Untuk konfirmasi masalah Kerjasama Ext: 1113<br/>
                                b.Untuk konfirmasi masalah Jaminan/Tindakan(Jam Kerja) Ext: 1002<br/>
                                c.Billing dan Pembayaran (Keuangan) Ext: 1258<br/>
                                d.Konfirmasi Claim Ext: 1095</td>
                            <td><strong>PIHAK KEDUA</strong><br/>
                                <strong>KLINIK....................</strong><br/>
                                Alamat : <strong>....................</strong><br/>
                                Telephone : <strong>....................</strong><br/>
                                Email : <strong>....................</strong><br/>
                                Kontak Person :<br/>
                                PIC Marketing : <strong>.................................</strong><br/>
                                PIC Medis : <strong>.................................</strong><br/>
                                PIC Keuangan : <strong>.................................</strong><br/>
                                Administrasi & Penjaminan : <strong>.................................</strong></td>
                        </tr>
                    </table>

                    <p class='center'>PASAL 19<br/>HUKUM YANG BERLAKU</p>

                    <p>Perjanjian ini tunduk dan wajib ditafsirkan menurut ketentuan dan peraturan perundang-undangan yang berlaku di wilayah Republik Indonesia.</p>

                    <p class='center'>PASAL 20<br/>PENYELESAIAN PERSELISIHAN</p>

                    <ol class='justify-ol'>
                        <li>PARA PIHAK Sepakat bahwa setiap dan semua perselisihan yang mungkin timbul sebagai akibat dari penafsiran dan/atau pelaksanaan Perjanjian ini akan diselesaikan secara musyawarah untuk mufakat.</li>
                        <li>PARA PIHAK sepakat bahwa bila dalam tenggang waktu selambat-lambatnya 30 (tiga puluh) Hari Kalender terhitung sejak tanggal terjadinya perselisihan, perselisihan tidak dapat diselesaikan secara musyawarah untuk mufakat, maka akan diselesaikan melalui Pengadilan Negeri Jakarta Selatan.</li>
                    </ol>

                    <p class='center'>PASAL 21<br/>KERAHASIAAN</p>

                    <ol class='justify-ol'>
                        <li>Kecuali diwajibkan oleh peraturan perundang-undangan yang berlaku, tidak ada satu pihakpun dalam Perjanjian ini yang dibenarkan untuk menginformasikan isi dari Perjanjian ini dan/atau memanfaatkan data-data yang digunakan dalam pelaksanaan Perjanjian ini baik yang bersifat teknis maupun komersial dalam bentuk apapun, selanjutnya disebut 'Informasi Rahasia'.</li>
                        <li>Informasi Rahasia dapat disampaikan dan dipakai oleh PARA PIHAK, karyawan/pegawai PARA PIHAK maupun pihak lainnya yang diberi wewenang oleh dan memiliki kewenangan atas perusahaan PARA PIHAK, untuk mengetahui dan menggunakan Informasi Rahasia dengan ketentuan pihak-pihak lain tersebut telah mendapatkan persetujuan tertulis terlebih dahulu dari PARA PIHAK.</li>
                    </ol>

                    <p class='center'>PASAL 22<br/>KESELURUHAN PERJANJIAN</p>

                    <ol class='justify-ol'>
                        <li>Perjanjian ini merupakan keseluruhan Perjanjian antara PARA PIHAK berkenaan dengan materi yang diperjanjikan.</li>
                        <li>Perjanjian ini membatalkan dan menggantikan kesepakatan yang dibuat sebelumnya oleh PARA PIHAK yang dilakukan secara lisan maupun tulisan.</li>
                    </ol>

                    <p class='center'>PASAL 23<br/>KETERPISAHAN</p>

                    <ol class='justify-ol'>
                        <li>Dalam hal suatu ketentuan yang terdapat dalam Perjanjian ini dinyatakan sebagai tidak sah atau tidak dapat diberlakukan secara hukum baik secara keseluruhan maupun sebagian, maka ketidaksahan atau ketidakberlakuan tersebut hanya berkaitan pada ketentuan ini atau sebagian daripadanya saja.Sedangkan ketentuan lainnya dari Perjanjian ini akan tetap berlaku dan mempunyai kekuatan hukum secara penuh.</li>
                        <li>PARA PIHAK selanjutnya setuju bahwa terhadap ketentuan yang tidak sah atau tidak dapat diberlakukan tersebut sebagaimana dimaksud dalam ayat(1) Pasal ini akan diganti dengan ketentuan yang sah menurut hukum dan sejauh serta sedapat mungkin dapat mencerminkan maksud dan tujuan komersial atas dibuatnya ketentuan tersebut.</li>
                    </ol>

                    <p class='center'>PASAL 24<br/>PENGALIHAN HAK</p>

                    <ol class='justify-ol'>
                        <li>Hak dan kewajiban yang timbul berdasarkan Perjanjian ini tidak dapat dialihkan oleh salah satu pihak kepada siapapun tanpa persetujuan tertulis terlebih dahulu dari pihak lainnya.</li>
                        <li>Setiap pihak yang menerima pengalihan hak wajib untuk menyetujui secara tertulis untuk mengikatkan diri pada ketentuan dalam Perjanjian ini secara keseluruhan tanpa ada yang dikecualikan.</li>
                    </ol>

                    <p class='center'>PASAL 25<br/>PERUBAHAN DAN TAMBAHAN</p>

                    <ol class='justify-ol'>
                        <li>Apabila dikemudian hari terdapat hal-hal yang belum diatur atau belum cukup diatur atau perlu dilakukan perubahan-perubahan atau perpanjangan jangka waktu dalam Perjanjian ini dan bila dipandang perlu, maka PARA PIHAK sepakat untuk menyatakan secara tertulis dalam amandemen atau addendum yang ditandatangani oleh PARA PIHAK dan merupakan bagian yang tidak terpisahkan dari Perjanjian ini.</li>
                        <li>Tidak ada perubahan atau modifikasi atau penambahan pada Perjanjian ini yang dianggap sah atau mengikat PARA PIHAK, kecuali dinyatakan secara tertulis dan ditandatangani oleh PARA PIHAK.</li>
                        <li>Dalam hal setelah ditandatanganinya Perjanjian ini terjadi suatu perubahan dalam peraturan perundang-undangan yang secara material dapat mendatangkan kerugian kepada PARA PIHAK, maka PARA PIHAK sepakat untuk mengadakan perundingan kembali sehingga dapat menghilangkan atau memperkecil kerugian yang diderita oleh salah satu pihak.</li>
                    </ol>

                    <p class='center'>PASAL 26<br/>PENUTUP</p>

                    <p>Perjanjian ini dibuat dan ditandatangani dalam rangkap 2 (dua) asli, bermeterai cukup, masing-masing mempunyai bunyi dan kekuatan hukum yang sama, 1 (satu) asli untuk PIHAK PERTAMA dan 1 (satu) asli untuk PIHAK KEDUA, ditandatangani oleh PIHAK PERTAMA dan PIHAK KEDUA pada tanggal sebagaimana disebutkan pada awal Perjanjian ini.</p>

                    <table>
                        <tr>
                            <td><strong>PIHAK PERTAMA</strong><br/>
                                <strong>PT ASURANSI TAKAFUL KELUARGA</strong><br/>
                                <span class='underline'><strong>Penny Hikmahwati</strong></span><br/>
                                <strong>Direktur Operasional</strong></td>
                            <td><strong>PIHAK KEDUA,</strong><br/>
                                <strong>KLINIK.........................</strong><br/>
                                <strong>.........................</strong><br/>
                                <strong>Direktur</strong></td>
                        </tr>
                    </table>

                    <p>Perjanjian Kerjasama antara PT. Asuransi Takaful Keluarga dengan <strong>.......................</strong> tentang Pelayanan Kesehatan dan Pengobatan Secara Berlangganan</p>

                    <p>Lampiran :</p>
                    <p>DAFTAR PENGECUALIAN</p>

                    <p>PIHAK PERTAMA tidak memberikan jaminan untuk pelayanan-pelayanan berikut, dan menjadi beban dari Peserta :</p>

                    <ol class='justify-ol'>
                        <li>Akibat perang atau bertugas aktif dalam militer atau angkatan bersenjata dari suatu negara atau Badan Internasional, terlibat demonstrasi, huru-hara (langsung dan tidak langsung), pemberontakan, atau keributan sipil, perbuatan melawan atau melanggar hukum; bencana alam; radiasi dan kontaminasi yang bersifat massal.</li>
                        <li>Cedera yang diakibatkan oleh perbuatan sendiri oleh /dengan bantuan pihak lain yang memiliki kepentingan dengan manfaat peserta, misalnya percobaan bunuh diri atau melanggar hukum dan terorisme.Cedera atau penyakit yang disebabkan oleh penggunaan alkohol. Narkotika, psikotropika atau zat adiktif lainnya.</li>
                        <li>Olahraga tertentu yang membahayakan (panjat gunung/tebing, hang gliding, balap mobil/motor, diving, parasut, tinju, akrobatik, gantole, terbang layang dan sejenisnya).</li>
                        <li>Segala kondisi yang berhubungan dengan penyakit yang ditularkan melalui hubungan seksual /golongan penyakit kelamin dan segala akibatnya.HIV, AIDS (Aquired Immune Defienciency Syndrome) dan ARC(AIDS Related Complex) dan segala akibatnya.</li>
                        <li>Pengobatan dan tindakan medis yang masih dikategorikan eksperimen termasuk tapi tidak terbataspada Therapy Ozon, Hyperbaric Therapy(kecuali yang dilakukan oleh dokter Spesialis Kelautan), Chelation Therapy, Brainwash Therapy, Stemcell Therapy, tindakan Laser Eximer, pengobatan akupunktur(kecuali yang dilakukan oleh dokter Spesialis akupunktur), perawatan kesehatan di Spa, Health Hydros, dan tempat perawatan tradisional(alternatif).</li>
                        <li>Pengobatan atau tindakan medis untuk Congenital(bawaan dari lahir) yang termasuk tapi tidak terbatas pada hernia dan epilepsy(khusus untuk Peserta di bawah usia 12 tahun), VSD, ASD, bibir sumbing, telapak kaki leper, pertumbuhan otot atau tulang secara tidak normal, cerebral palsy, hydrocephalus, dan cacat bawaan lainnya.</li>
                        <li>Pengobatan atau tindakan medis untuk Kelainan herediter (penyakit keturunan) yang diakibatkankelainan jumlah kromosom termasuk tapi tidak terbatas pada debil, embicil, mongoloid, cretinism, thallasemia, haemophillia dan kelainan kromosom lainnya.</li>
                        <li>Pengobatan atau tindakan medis untuk gangguan tumbuh kembang yang termasuk tapi tidak terbatas pada autisme, retardasi mental, ADHD, dan kelainan tumbuh kembang lainnya.</li>
                        <li>Pemeriksaan kesehatan yang tidak ada hubungannya dengan pengobatan atau diagnosa dari suatu penyakit yang dijamin.</li>
                        <li>Setiap pengobatan yang bukan berdasarkan indikasi medis atau tidak diperlukan secara medis, perawatan atau tindakan medis yang lebih bersifat kosmetik atau kenyamanan, termasuk tapi tidak terbatas pada pengobatan jerawat, tahi lalat, keloid, dry eyes, mata lelah, dan perawatan kosmetik atau kenyamanan lainnya.</li>
                        <li>Pengobatan dan tindakan medis yang dilakukan oleh keluarga dekat Peserta atau oleh seseorang tinggal serumah atau bekerja sama dengan Peserta.</li>
                        <li>Pengobatan dan tindakan medis yang tidak sesuai dengan benefit Peserta.</li>
                        <li>Segala jenis upaya pencegahan penyakit termasuk tetapi tidak terbatas pada imunisasi/vaksinasi, kecuali di atur dalam manfaat imuninasi.</li>
                        <li>Segala sesuatu yang berhubungan dengan kehamilan, segala penyakit yang berhubungan dengan kehamilan kecuali diatur dalam Manfaat tambahan Melahirkan, Segala sesuatu yang berhubungan dengan tindakan untuk mendapatkan kesuburan serta upaya pencegahan kehamilan termasuk tapi tidak terbatas pada pengobatan PCOS (Polycystic Ovary Syndrome) Inseminasi buatan dan tindakan untuk mendapatkan keturunan berikutnya.Segala sesuatu yang berhubungan dengan gangguan menstruasi (menstruasi disorder) akibat kelainan hormonal termasuk tapi tidak terbatas gangguan pre menopouse.</li>
                        <li>Gangguan akibat sinar radio aktif dari setiap bahan bakar nuklir atau limbah nuklir, bencana alam (gempa bumi, banjir, letusan gunung berapi, badai tsunami dan sejenisnya).</li>
                        <li>Penggantian protesa tangan, protesa mata, protesa kaki dan alat bantu pendengaran.</li>
                        <li><em>Cosmetic Surgery</em> (operasi plastik) ataupun hal-hal yang berhubungan dengan upaya pemulihan perawatan kecantikan bagian tubuh.</li>
                        <li>Alat pacu jantung, transplantasi organ tubuh termasuk sumsum tulang, akupuntur, Haemodialisa (cuci darah), pengobatan kanker, operasi jantung.</li>
                        <li>Jasa-jasa non medis yang diberikan oleh rumah sakit, seperti biaya telpon, fax, salon, video, televisi, sauna, laundry, mini bar dan lain-lain.</li>
                        <li>Pembelian obat-obatan tanpa resep Dokter, obat atau bahan yang tidak ada hubungannya dengan penyakit yang diderita termasuk bahan pembersih gigi, obat jerawat, obat-obatan untuk mempercantik diri dan obat-obatan tradisional/herbal atau penyegar mata.</li>
                        <li>Semua bentuk multivitamin dan suplemen yang tidak memenuhi semua kriteria (hanya jika diresepkan oleh dokter; tidak diresepkan secara tunggal; berkorelasi dengan Penyakit yang dijamin dan sedang diderita; dalam jumlah yang wajar menurut penilaian Perusahaan; bertujuan untuk penyembuhan dan bukan untuk pencegahan; Bukan produk yang dipasarkan secara Multi Level Marketing).</li>
                        <li>Pengobatan terhadap penyakit kejiwaan psikologis atau gangguan mental(<em>mental disorder</em>) dan gangguan psikologis lainnya termasuk semua gejala sisa atau sequele dari penyakit tersebut.</li>
                    </ol>
                    
                  
                    <p class='space'>&nbsp;</p>
                    <p>Lampiran II : Contoh Kartu Peserta PT.Asuransi Takaful Keluarga</p>
                    
                    <table style='width:100%; table-layout:fixed; border-collapse:collapse;' 
                        width='100%' 
                        cellspacing='0' 
                        cellpadding='0'>
                        <tr>
                        <th style='width:5%'>No</th>
                        <th style='width:45%'>Kartu Peserta</th>
                        <th style='width:50%'>Keterangan</th>
                        </tr>
                        <tr>
                            <td>1</td>
                            <td> <img src='{{KARTU1}}' style='max-width:300px; max-height:194px;' /></td>
                            <td><strong>TAKAFUL-FULLERTON</strong><br/>
                                <strong>Jaminan FULLERTON</strong><br/>
                                021 -- 29976326<br/>
                                <a href = 'mailto:case.managers@fullertonhealth.com'>case.managers @fullertonhealth.com</a></td>
                        </tr>
                        <tr>
                            <td>2</td>
                            <td> <img src='{{KARTU2}}' sstyle='max-width:300px; max-height:194px;' /></td>
                            <td><strong>TAKAFUL-ADMEDIKA :</strong><br/>
                                <strong>Jaminan ADMEDIKA</strong><br/>
                                021-29647599<br/>
                                <a href = 'mailto:takaful@admedika.co.id'> takaful@admedika.co.id</a></td>
                        </tr>
                        <tr>
                            <td>3</td>
                            <td> <img src='{{KARTU3}}' sstyle='max-width:300px; max-height:217px;' /></td>
                            <td><strong>TAKAFUL -- VIP CUSTOMER</strong><br/>
                                <strong>Jaminan TAKAFUL</strong><br/>
                                021-79190005<br/>
                                <a href = 'mailto:askes.penjaminan@takaful.com'> askes.penjaminan@takaful.com</a><br/>
                                <strong>Seluruh biaya di jaminan Takaful</strong><br/>
                                <a href = 'https://ecard.takaful.com/takafulsuratpenjaminan/'> https://ecard.takaful.com/takafulsuratpenjaminan/</a></td>
                        </tr>
                        <tr>
                            <td>4</td>
                            <td> <img src='{{KARTU4}}' sstyle='max-width:300px; max-height:296px;' /></td>
                            <td><strong>TAKAFUL -- HALODOC</strong><br/>
                                <strong>Jaminan Halodoc</strong><br/>
                                021-39506663<br/>
                                <a href = 'mailto:heidy@halodoc.com'> heidy@halodoc.com</a></td>
                        </tr>
                    </table>
                </body>
                </html>
                ";

            string[] parts = tanggalpengajuan.Split('-');

            string tanggal = parts[0]; // "01"
            string bulan = parts[1];   // "07"
            string tahun = parts[2];   // "2025"

            DateTime tanggalhari;
            DateTime.TryParseExact(tanggalpengajuan, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tanggalhari);

            string hari = tanggalhari.ToString("dddd", new CultureInfo("id-ID"));
            // hasil: "Selasa"

            return html
                .Replace("{{TanggalPengajuan}}", tanggalpengajuan)
                .Replace("{{NomorSurat}}", nomorsurat)
                .Replace("{{NamaPerusahaan}}", namaPerusahaan)
                .Replace("{{AlamatPerusahaan}}", alamat)
                .Replace("{{NamaRumahSakit}}", namaRS)
                .Replace("{{bulan}}", bulan)
                .Replace("{{tahun}}", tahun)
                .Replace("{{hari}}", hari)
                .Replace("{{tanggal}}", tanggal);
        }

        public static string Document_Adendum(
            string tanggalpengajuan,
            string nomorsurat,
            string namaPerusahaan,
            string alamat,
            string namaRS)
        {
            string html = @"
            <!DOCTYPE html>
            <html lang='id'>
            <head>
                <meta charset = 'UTF-8'/>
 
                 <title> Addendum I Perjanjian Kerjasama </title>
                    <style type='text/css'>
                    body {
                        font-family: Arial, sans-serif;
                        font-size: 10pt;
                        line-height: 1.6;
                        margin: 15px;
                        color: #333;
                    }
                    p {
                        margin: 0 0 14px;
                        text-align: justify;
                    }
                    .justify-list {
                        padding-left: 1.2rem;
                        margin: 0;
                    }
                    .justify-list li {
                        margin-bottom: 0.5rem;
                        text-align: justify;
                        text-justify: inter-word;
                        text-align-last: justify;
                    }

                    .underline {
                        text-decoration: underline;
                    }
                    .center {
                        text-align: center;
                    }
                    table {
                        width: 100%;
                        border-collapse: collapse;
                        margin: 20px 0;
                        table-layout: fixed;
                    }
                    table, th, td {
                        border: 1px solid #ddd;
                    }
                    th, td {
                        padding: 8px;
                        text-align: left;
                    }

                    table th:first-child,
                    table td:first-child {
                        width: 60pt; /* pakai pt untuk Spire.Doc lebih stabil */
                    }

                    .center {
                            text-align: center;          /* semua isi di tengah */
                            font-family: 'Arial', serif;
                        }
                    .center p strong {
                        font-weight: bold;           /* tebal */
                    }
                    .center p {
                        text-align: center !important; /* override justify */
                        margin: 6px 0;                 /* spasi antar baris */
                    }

                    .center p.small {
                        font-size: 14px;
                        font-style: italic;
                    }
                        .justify-ol li {
                        text-align: justify;
                        text-justify: inter-word;
                    }

                    .space {
                        margin-top: 150px;
                        margin-bottom: 150px;
                    }
                    </style>
            </head>
            <body>

            <p class='center'>ADDENDUM I<br/>PERJANJIAN KERJASAMA</p>

            <p class='center'>ANTARA<br/> PT.ASURANSI TAKAFUL KELUARGA<br/>DENGAN<br/> RUMAH SAKIT {{NamaRumahSakit}} <br/>TENTANG<br/> PELAYANAN PERAWATAN KESEHATAN DAN PENGOBATAN<br/>SECARA BERLANGGANAN</p>

            <p class='center'>Nomor : ...........</p>

            <p>Pada hari ini,{{TanggalPengajuan}}.tahun Dua ribu dua puluh tiga(……-…..-….), bertempat di Jakarta, yang bertanda tangan di bawah ini:</p>

            <p>I.PT.ASURANSI TAKAFUL KELUARGA berkedudukan di Jakarta dan berkantor pusat di Graha Takaful Indonesia, Jalan Mampang Prapatan Raya No. 100, Jakarta Selatan, dalam hal ini diwakili oleh Penny Hikmahwati selaku Direktur Operasional, berdasarkan Akta No. 24 tanggal 14 Agustus 2023 dibuat di hadapan Arry Supratno, SH., Notaris di Jakarta, dan telah mendapat penerimaan pemberitahuan dari Menteri Hukum dan Hak Asasi Manusia Republik Indonesia melalui surat No.AHU-AH.01.09-0153011 tanggal 18 Agustus 2023 dan karenanya sah bertindak mewakili Direksi PT.Asuransi Takaful Keluarga, untuk selanjutnya disebut “PIHAK PERTAMA”.</p>

            <p>II.RUMAH SAKIT {{NamaRumahSakit}}, yang berkedudukan di {{AlamatPerusahaan}}, sesuai dengan Akta pendirian nomor …………………… beralamat di Jalan …………………………., dalam hal ini diwakili oleh ………………. selaku Direktur Rumah Sakit {{NamaRumahSakit}} berdasarkan Surat Keputusan……… dan karenanya bertindak untuk dan atas nama serta mewakili Rumah Sakit {{NamaRumahSakit}}, untuk selanjutnya disebut sebagai “PIHAK KEDUA”.</p>

            <p>PIHAK PERTAMA dan PIHAK KEDUA(selanjutnya disebut “PARA PIHAK”), menerangkan terlebih dahulu hal-hal sebagai berikut:</p>

            <ul class='justify-ol'>
                <li>Bahwa PARA PIHAK telah menandatangani Perjanjian Kerjasama Pelayanan Kesehatan dan Pengobatan Secara Berlangganan, pada tanggal ….......... dengan no. …............ selanjutnya disebut “Perjanjian Induk”.</li>
                <li>Bahwa Jangka Waktu Perjanjian Induk sebagaimana dimaksud huruf a telah berakhir masa berlakunya terhitung sejak …...................</li>
                <li>Bahwa selama rentang waktu setelah tanggal berakhirnya Perjanjian Induk sampai dengan Addendum ini dibuat PARA PIHAK sepakat untuk tetap tunduk pada ketentuan yang tercantum dalam Perjanjian Induk.</li>
            </ul>

            <p>Sehubungan hal-hal tersebut di atas, PARA PIHAK sepakat membuat Addendum atas Perjanjian Induk, selanjutnya disebut Addendum-I dengan ketentuan dan syarat-syarat sebagai berikut:</p>

            <p class='center'>PASAL 1<br/>PERUBAHAN</p>
            <ol class='justify-ol'>
                <li>Perjanjian berlaku dalam jangka waktu 3 (tiga) tahun, terhitung sejak tanggal ditandatangani dan dapat diakhiri sewaktu-waktu atau diadakan perubahan-perubahan berdasarkan persetujuan PARA PIHAK.</li>
                <li>Perjanjian ini diperpanjang secara otomatis untuk tahun berikutnya jika tidak ada permintaan perbatalan atau perubahan dari salah satu Pihak.</li>
                <li>Pihak yang menginginkan berakhirnya Perjanjian ini atau mengadakan perubahan-perubahan berkewajiban menyampaikan kepada pihak lainnya selambat-lambatnya 60 (enam puluh) hari sebelum tanggal berakhirnya Perjanjian atau tanggal dimulainya perubahan-perubahan yang dikehendaki.</li>
            </ol>

            <p class='center'>PASAL 2<br/>LAIN-LAIN</p>
            <ol class='justify-ol'>
                <li>Addendum I ini merupakan bagian yang mengikat dan tidak terpisahkan dari Perjanjian Induk.</li>
                <li>Ketentuan-ketentuan lain dalam Perjanjian Induk yang tidak diubah dengan Addendum I ini dinyatakan tetap berlaku sepanjang tidak bertentangan dengan Addendum I ini.</li>
                <li>Hal-hal lain yang mungkin timbul dan belum diatur dalam Perjanjian Induk dan Addendum I ini akan diatur kemudian dengan persetujuan tertulis PARA PIHAK yang akan dituangkan dalam bentuk Addendum yang merupakan bagian yang mengikat dan tidak terpisahkan dari Perjanjian Induk dan Addendum I ini.</li>
            </ol>

            <p>Demikian Addendum I ini dibuat dan ditandatangani pada hari, tanggal, bulan dan tahun sebagaimana tersebut di atas, dibuat dalam rangkap 2 (dua) asli, bermaterai cukup dan masing-masing mempunyai kekuatan hukum yang sama.</p>

            <table class='signature-table'>
                <tr>
                    <td><strong>PIHAK PERTAMA</strong><br/><br/>PT ASURANSI TAKAFUL KELUARGA<br/><br/><br/><br/><strong>Penny Hikmahwati</strong><br/>Direktur Operasional</td>
                    <td><strong>PIHAK KEDUA</strong><br/><br/>RS {{NamaRumahSakit}}<br/><br/><br/><br/><strong>....................................</strong><br/>Direktur</td>
                </tr>
            </table>

            </body>
            </html>
                        ";

            return html
                .Replace("{{TanggalPengajuan}}", tanggalpengajuan)
                .Replace("{{NomorSurat}}", nomorsurat)
                .Replace("{{NamaPerusahaan}}", namaPerusahaan)
                .Replace("{{AlamatPerusahaan}}", alamat)
                .Replace("{{NamaRumahSakit}}", namaRS);
        }
        
        public static string Document_Kerjasama_Sementara(
            string tanggalpengajuan,
            string nomorsurat,
            string namaPerusahaan,
            string alamat,            
            string namaRS,
            string tanggalsurat,
            string kota)
        {
            string tandaTanganPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "TTD.jpg");
            string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "STEMPEL.jpg");

            // Pastikan file benar-benar ada
            if (!File.Exists(tandaTanganPath))
                throw new FileNotFoundException("File tanda tangan tidak ditemukan", tandaTanganPath);

            if (!File.Exists(logoPath))
                throw new FileNotFoundException("File logo tidak ditemukan", logoPath);

            // Untuk HTML Spire.Doc: langsung pakai path absolut (Windows pakai slash / biar aman)
            string tandaTanganSrc = tandaTanganPath.Replace("\\", "/");
            string logoSrc = logoPath.Replace("\\", "/");


            string html = @"
<!DOCTYPE html>
<html lang='id'>
<head>
    <meta charset='UTF-8' />
    <title>Perjanjian Kerjasama Sementara</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            font-size: 10pt;
            line-height: 1.6;
            margin: 2cm;
        }
        li {
            margin: 0 0 10px 0;
        }
        p { margin: 0 0 10px; text-align: justify; margin-bottom: 14px;}
        .center {
            text-align: center;
        }
        .right {
            text-align: right;
        }
        .signature {
            margin-top: 50px;
        }
        .contact-info {
            margin-top: 15px;
        }
        .center-text {
            text-align: justify;
        }
        .justified-text {
            text-align: justify;
        }
        .content {
            line-height: 1.6;
            margin: 20px;
            font-family: Arial, sans-serif;
        }
        ul {
            text-align: justify;
            padding-left: 0;
            list-style-position: inside;
        }
    
    </style>
</head>
<body>
<p></p><br/>
<p class='left'>Jakarta, {{TanggalSurat}} </p>
<p>No : {{NomorSurat}}
<br/>Lamp : -</p>
<p>Kepada Yth.
<br/>Direktur {{NamaPerusahaan}}
<br/>{{AlamatPerusahaan}}
<br/>{{Kota}}
</p>

<p><strong>Perihal: Perjanjian Kerjasama Sementara</strong></p>

<p>Assalamualaikum wr. wb</p>

<p>Teriring salam, semoga kita semua senantiasa diberikan kesehatan dan kemudahan oleh Yang Maha Kuasa untuk menjalankan aktivitas.</p>

<p>Terima kasih kami ucapkan atas kepercayaan yang telah diberikan untuk menjalin hubungan kerjasama dalam pelaksanaan Pelayanan Kesehatan Program Asuransi Kesehatan Asuransi Takaful Keluarga.</p>

<p>Menindaklanjuti pemberitahuan dan kesepakatan yang telah disampaikan sebelumnya perihal pelaksanaan pelayanan kesehatan bagi peserta Asuransi Kesehatan FULMEDICARE Asuransi Takaful Keluarga, 
bersama ini kami sampaikan sebagaimana berikut :</p>

<ul>
    <li>Asuransi Takaful Keluarga telah sepakat dan menyetujui untuk menunjuk serta menjalin hubungan kerjasama dengan {{NamaPerusahaan}} sebagai penyedia jasa pelayanan kesehatan.</li>
    <li>Pelayanan kerjasama ini juga akan dituangkan dalam bentuk Perjanjian Kerjasama Sementara selama Surat Perjanjian Kerjasama dalam proses finalisasi. Apabila terdapat peserta yang akan menggunakan 
    fasilitas dan pelayanan {{NamaPerusahaan}}  dan dapat menunjukkan Kartu Peserta mohon kiranya untuk dapat diterima dan dilayani dengan baik. Jika mengalami kendala dapat menghubungi 
    Layanan Peserta 24 jam kami di nomor 021-79190005.</li>
    <li>Dan atas pelayanan yang telah diberikan tersebut Asuransi Takaful Keluarga akan bertanggung jawab atas penagihan yang diajukan secara lengkap.</li>
    <li>Tagihan dari Rumah Sakit dapat di kirimkan ke masing – masing TPA atau ke alamat PT. Asuransi Takaful Keluarga untuk penjaminan langsung Takaful.</li>
    <li>Pembayaran ke Rumah Sakit akan dilakukan selambat-lambatnya 30 Hari Kalender.<br/><br/></li>
    <li>Untuk pelayanan Rawat Inap dan Rawat Jalan dilakukan dengan menghubungi Layanan Peserta di bawah ini :</li>
</ul>
<br/>
<p><strong>Layanan Penjaminan:</strong></p>

<table border='1' cellpadding='5' cellspacing='0' width='100%'>
    <tbody>
        <tr>
            <td>FULLERTON – TAKAFUL</td>
            <td>021 – 29976326</td>
            <td>case.managers@fullertonhealth.com</td>
        </tr>
        <tr>
            <td>ADMEDIKA – TAKAFUL</td>
            <td>021 – 29647599</td>
            <td>takaful@admedika.co.id</td>
        </tr>
		<tr>
            <td>HALODOC – TAKAFUL</td>
            <td>021 – 39506663</td>
            <td>heidy@halodoc.com</td>
        </tr>
        <tr>
            <td>TAKAFUL LANGSUNG</td>
            <td>021 – 79190005</td>
            <td>askes.penjaminan@takaful.com</td>
        </tr>
    </tbody>
</table>

<p><strong>Contact Person:</strong><br/>
1. Septian Alfi  (0821 1470 8355)
2. Fitra Abdul Malik (0813 1484 4965)<br/>
</p>

<ul start='7'>
    <li>Surat pemberitahuan ini adalah menjadi satu kesepakatan antara kedua belah pihak dan segera diwujudkan dalam satu perjanjian kerjasama. 
	Sementara surat perjanjian kerjasama dalam proses maka surat pemberitahuan ini menjadi satu pedoman untuk pelaksanaan selanjutnya.</li>
    <li>Sebagai tanda persetujuan mengenai hal tersebut diatas, kami mohon agar Bapak/Ibu dapat menandatangani surat ini dan mengirimkannya 
        kembali kepada kami melalui email ke provrelation-atk@takaful.com.</li>
</ul>

<p>Demikian hal ini kami sampaikan, besar harapan kami kiranya hubungan kerjasama ini dapat menjadi suatu hubungan kerjasama yang baik dan 
menguntungkan serta memudahkan bagi kedua belah pihak.</p>

<p> Atas perhatian dan kerjasama serta kepercayaan yang telah diberikan kami ucapkan terima kasih. </p>
<p>Wassalamu’alaikum wr. wb</p>

<table width='100%' style='margin-top:50px'>
    <tr>
        <td width='50%' style='vertical-align:top; text-align:left;'>
            <br/>
            <p><strong>PT. ASURANSI TAKAFUL KELUARGA</strong>
                 <table style='border:0;'>
                    <tr>
                      <td style='padding-right:10px; vertical-align:bottom;'>
                        <img src='{{TTD}}' style='max-width:120px; max-height:80px;' />
                      </td>                    
                    </tr>
                  </table>
            <strong>dr. Sri Setyaningsih</strong><br/>
            <strong>Head of Claim</strong></p>
        </td>
        <td width='50%' style='vertical-align:top; text-align:left;'>
            <p><strong>Menyetujui,<br/>{{NamaPerusahaan}}</strong><br/><br/><br/>
            <br/>
            <strong><u>...........................................</u></strong><br/>
            ...........................................</p>
        </td>
    </tr>
</table>

</body>
</html>

            ";

            return html
                .Replace("{{TanggalPengajuan}}", tanggalpengajuan)
                .Replace("{{NomorSurat}}", nomorsurat)
                .Replace("{{NamaPerusahaan}}", namaPerusahaan)
                .Replace("{{AlamatPerusahaan}}", alamat)
                .Replace("{{Kota}}", kota)
                .Replace("{{NamaRumahSakit}}", namaRS)
                .Replace("{{TanggalSurat}}", tanggalsurat);
        }

        public static string Document_NDA(
           string tanggalpengajuan,
           string nomorsurat,
           string namaPerusahaan,
           string alamat,
           string namaRS)
        {
            string html = @"
<html>
<head>
                    <style type='text/css'>
                    body {
                        font-family: Arial, sans-serif;
                        font-size: 10pt;
                        line-height: 1.6;
                        margin: 15px;
                        color: #333;
                    }
                    p {
                        margin: 0 0 14px;
                        text-align: justify;
                    }
                    .justify-list {
                        padding-left: 1.2rem;
                        margin: 0;
                    }
                    .justify-list li {
                        margin-bottom: 0.5rem;
                        text-align: justify;
                        text-justify: inter-word;
                        text-align-last: justify;
                    }

                    .underline {
                        text-decoration: underline;
                    }
                    .center {
                        text-align: center;
                    }
                    table {
                        width: 100%;
                        border-collapse: collapse;
                        margin: 0px 0;
                        table-layout: fixed;
                    }
                    table, th, td {
                        border: none;
                    }
                    th, td {
                        padding: 8px;
                        text-align: left;
                    }

                    .center p strong {
                        font-weight: bold;           /* tebal */
                    }

                    .center p.title {
                      text-align: center; /* hanya untuk p.title */
                    }
                    .center p.small {
                        font-size: 12px;
                        font-style: italic;
                    }
                        .justify-ol li {
                        text-align: justify;
                        text-justify: inter-word;
                    }
                    h2.title {
                          text-align: center;
                          margin: 10px 0;  /* kecilkan jarak atas/bawah */
                          font-size: 20pt;
                        }

                        h3.subtitle {
                          text-align: center;
                          margin: 5px 0;  /* kecilkan jarak bawah */
                          font-size: 14pt;
                          font-style: italic;
                        }

                        .number {
                          text-align: center;
                          margin: 5px 0;
                          font-size: 10pt;
                        }

                        hr {
                          margin: 5px auto;
                        }
                    </style>
</head>
<body>
        <h2 class='title'>PERJANJIAN KERAHASIAAN</h2>
        <h3 class='subtitle'><i>NON – DISCLOSURE AGREEMENT</i></h3>
        <hr/>
        <div class='number'>NUMBER: MOU-ATK-DO-……./2025</div>    
    <table>
        <tr>
            <td width='50%'>
                <p>This <span class='underline'>Non Disclosure</span> Agreement is made and entered into on this day, 2025, _____ by and between:</p>
                
                <p>I. <span class='bold'>PT ASURANSI TAKAFUL KELUARGA</span>, a company duly formed and incorporated under the laws of Indonesia and having its principal place of business addressed at 
                Jalan Mampang Prapatan Raya No. 100, Jakarta Selatan, Indonesia, in this matter is represented by 
                <span class='bold'>Penny Hikmahwati</span>, acting in position as <span class='bold'>Operational Director</span>, therefore validly acting for and on behalf of (hereinafter called <span class='bold'>“FIRST PARTY”</span>);</p>
                <br/>
                <p>II. ……………………, a company duly formed and incorporated under the laws of Singapore and having its principal place of business addressed at ……………………, in this matter is represented by ……………, acting in position as ________, therefore validly acting for and on behalf of ………………… (hereinafter called <span class='bold'>“SECOND PARTY”</span>).<br/>                
                    FIRST PARTY</span> and <span class='bold'>SECOND PARTY</span> hereinafter are referred to collectively as <span class='italic'>“Parties”</span> or singularly as <span class='italic'>“Party”</span>, as the context may require.</p>             
                
                <ol class='justify-ol'>
                    <li><span class='bold italic'>WHEREAS</span>, the <span class='bold'>FIRST PARTY</span> as a limited liability company as a service sharia insurance provider, Agree to provide services in cooperation with the <span class='bold'>SECOND PARTY</span> as described in this Agreement.</li>
                    <li><span class='bold italic'>WHEREAS</span>, the <span class='bold'>SECOND PARTY</span> is a Company engaged in the field of insurance technology accordance with the objectives referred to in this agreement;</li>
                    <li>That the <span class='bold'>PARTIES</span> plan to cooperate Utilization of insurance technology (""Objectives"");</li>
                    <li>Whereas, in carrying out the Purpose as referred to in letter c above, the <span class='bold'>PARTIES</span> in this Agreement agree that the <span class='bold'>PARTIES</span> may act as the party receiving Confidential Information (""<span class='bold'>Receiving Party</span>"") from one of the Parties as the party providing Confidential Information (""<span class='bold'>Giving Party</span>""), where the Giving Party has and/or will disclose Confidential Information in accordance with the provisions in the Purpose stipulated in this Agreement.;</li>
                </ol> 
                
                <p>Based on the matters as mentioned above, the PARTIES hereby agree to enter into and execute this AGREEMENT with the following terms and conditions:</p>

                 <p class='center'><h3 style='text-align:center;'><b>ARTICLE 1<br>DEFINITION</b></h3></p>
                  <ol class='justify-ol'>
                    <li>In this AGREEMENT, Confidential Information means the Confidential Information of the Disclosing Party as follows: All information regarding the financial information, business, customer information, including the database of customer telephone numbers and/or all information relating to the business activities and plans, sales, programmes and merchandise, information, business opportunities and plans including marketing strategies of the Giving Party and/or its affiliates;</li>
                    <li>""Confidential Information"" shall also include all Confidential Information of the Giving Party relating to third party Information that the Giving Party may disclose to the Receiving Party for the purposes of performing this Purpose either orally, in writing or by any other means possible;</li>
                    <li>The obligations of the Receiving Party shall not apply specifically in the event that:
                      <ol type='a'>
                        <li>The Information is already public information and the disclosure of the Information is not an offence and/or fault of the Receiving Party;</li>
                        <li>The information has legitimately been in the possession of the Receiving Party prior to being provided and/or disclosed by the First Party so that at the time of disclosure it is not categorised as Confidential Information.</li>
                      </ol>
                    </li>
                    <li>Disclosure of Confidential Information by the Receiving Party shall not be deemed a breach of the obligations in this AGREEMENT in the event that:
                      <ol type='a'>
                        <li>Disclosure of Confidential Information is made to fulfil a decision or order from the Court or authorised government body;</li>
                        <li>Disclosure of Confidential Information is made because it is required by laws and regulations;</li>
                        <li>Necessary to fulfil the obligations of the Giving Party;</li>
                        <li>The Receiving Party shall promptly give prior written notice to the Giving Party of the provisions set forth in Article 1 letter b above, so that the Giving Party may take all necessary steps to protect such Confidential Information.</li>
                      </ol>
                    </li>
                  </ol>
                  
                  <p class='title'><h3 style='text-align:center;'><b>ARTICLE 2<br>NON – DISCLOSURE AND SECRECY</b></h3></p>
                   
                  <p>The Receiving Party undertakes and agrees, with respect to the Confidential Information:</p>
                    <ol class='justify-ol'>
                        <li>To use such Confidential Information only for the Purpose as have been agreed by both Parties;</li>
                        <li>Maintain secrecy of and not disclose or allow access to such Confidential Information to any third party, including but not limited to affiliates and consultants, unless the Receiving Party has given undertaking by entering into another similar agreement to bind themselves by the terms thereof, and the Receiving Party will shall bear the responsibility and be liable for any breach of this Agreement;</li>
                        <li>To implement and maintain such safeguards as deemed necessary to ensure the confidentiality of such information including to employees who have a need to know for the same purpose;</li>
                        <li>All Confidential Information (including copies made by the Receiving Party) shall remain as an integral part of the property of the Giving Party, and shall be returned and/or destroyed by the Receiving Party immediately after use and/or upon termination of this Agreement.</li>
                   </ol>
                  
                   <p class='title'><h3 style='text-align:center;'><b>ARTICLE 3<br>OWNERSHIP AND INFORMATION RETURN</b></h3></p>
                                      
                    <ol class='justify-ol'>
                        <li>All Confidential Information belonging to the Giving Party on the scope of work and requests from the Giving Party in the contract that will be discussed and agreed upon in a separate agreement.</li>
                        <li>Upon the request of the Giving Party to the Receiving Party, within 5 (five) business days after such request, the Receiving Party shall immediately destroy and/or return to the Giving Party, at the option of the Giving Party:
                            <ol type='a'>
                                <li>All confidential information provided by the Giving Party to the Receiving Party, whether in the form of hard copy and/or soft copy;</li>
                                <li>All tools or documentation in the possession of the Receiving Party;</li>
                                <li>A written statement from the Receiving Party that the Receiving Party's obligation to return and/or delete the Confidential Information in this AGREEMENT has been performed.</li>
                            </ol>
                        </li>
                        <li>To implement and maintain such safeguards as deemed necessary to ensure the confidentiality of such information including to employees who have a need to know for the same purpose;</li>
                        <li>All Confidential Information (including copies made by the Receiving Party) shall remain as an integral part of the property of the Giving Party, and shall be returned and/or destroyed by the Receiving Party immediately after use and/or upon termination of this Agreement.</li>
                   </ol>
                  
                   <p class='title'><h3 style='text-align:center;'><b>ARTICLE 4<br>TIME PERIOD</b></h3></p>
                   <p>This Confidentiality Agreement shall be effective from the date as stated above, all confidential information provided by the PARTIES shall remain confidential and binding on the PARTIES regardless of the termination and/or expiry of the Cooperation Agreement (""Confidentiality Agreement Period"").</p>                   
                   
                  
                   <p class='title'><h3 style='text-align:center;'><b>ARTICLE 5<br>DISPLACEMENT</b></h3></p>
                   <p>Neither PARTY shall assign its rights and obligations under this AGREEMENT without the written consent of the other PARTY, which consent shall not be unreasonably withheld.                   
                   <br/><br/> 
                  
                   <p class='title'><h3 style='text-align:center;'><b>ARTICLE 6<br>FAIR COMPENSATION</b></h3></p>
                   The PARTIES' breach of either the content of the Agreement or the co-operation contained in this Agreement may result in irreparable and continuing damage to the other Party, for which there may be no appropriate remedy at law, and therefore the Party who has committed such wrongdoing (the ""Wrongdoing Party"") shall be obliged to provide equitable relief and/or take all actions necessary for the injured Party (the ""Wrongdoing Party""), or any other possible remedy (including financial relief where appropriate and necessary).                

                 
                   <p class='title'><h3 style='text-align:center;'><b>ARTICLE 7<br>OTHER PROVISIONS</b></h3></p>
                    <ol class='justify-ol'>
                        <li>The <span class='bold'>PARTIES</span> agree that this AGREEMENT shall be interpreted, construed and governed by the laws of the Republic of Singapore.</li>
                        <li>The <span class='bold'>PARTIES</span> agree to comply with all applicable laws and regulations in the Republic of Singapore.</li>
                        <li>In the event of any dispute between the <span class='bold'>PARTIES</span> as a result of the interpretation or implementation of this Confidentiality Agreement, the <span class='bold'>PARTIES</span> agree to resolve it first by deliberation to reach a consensus.</li>
                        <li>If within 30 (thirty) working days no consensus is reached by both <span class='bold'>PARTIES</span>, then the <span class='bold'>PARTIES</span> agree to settle it through the Registrar of the South Jakarta District Court.</li>
                   </ol>
                  
                   <p class='title'><h3 style='text-align:center;'><b>ARTICLE 8<br>GOVERNING LAW AND DISPUTE RESOLUTION</b></h3></p>
                    <ol class='justify-ol'>
                        <li>The PARTIES agree to execute this Agreement with full sense of responsibility and subject to all applicable provisions.</li>
                        <li>The PARTIES acknowledge and declare that in connection with the signing of this agreement each of the PARTIES:
                            <ol type='a'>
                                <li>Sign this agreement after proper scrutiny;</li>
                                <li>Have read and fully understood the terms of this agreement;</li>
                                <li>Have had sufficient opportunity to examine and confirm all the provisions of this agreement and all relevant facts and conditions.In the event of any inconsistency between the English and Indonesian versions of this Agreement, the English version shall prevail.</li>
                                <li>In the event of any discrepancy between the English version and the Indonesian version of this Agreement, the English version shall prevail.</li>
                            </ol>
                        </li>
                   </ol>

                    <p>
                        Thus this Agreement is made and signed by the PARTIES on sufficient stamp duty in duplicate 2 (two) 
                        and has the same legal force, 1 (one) set for the FIRST PARTY and 1 (one) set for the SECOND PARTY.
                    </p>
                    <p>
                        Signed by / Ditandatangani oleh<br>
                        For and on behalf of / Untuk dan atas nama<br>
                        <b>PT ASURANSI TAKAFUL KELUARGA</b>
                    </p>
                    <br><br><br>
                    <p>
                        Name/Nama: _PENNY HIKMAHWATI<br>
                        Designation/Kedudukan: <u>Direktur Operasional</u><br>
                        Date/Tanggal: ________________
                    </p>            
            </td>
            <td width='50%'>
                <p>Perjanjian Kerahasiaan ini dibuat dan ditandatangani pada hari ______, 2025, oleh dan antara:</p>

                <p>I. <span class='bold'>PT ASURANSI TAKAFUL KELUARGA</span>, suatu perusahaan yang dibentuk dan didirikan di bawah Undang-undang Indonesia 
                dengan alamat kantor Graha Takaful Indonesia, Jalan Mampang Prapatan Raya No. 100, Jakarta Selatan, Indonesia 
                dalam hal ini diwakili oleh <span class='bold'>Penny Hikmahwati</span> bertindak dalam jabatannya sebagai <span class='bold'>Direktur Operasional</span> sehingga sah bertindak untuk dan atas nama (selanjutnya disebut sebagai <span class='bold'>“PIHAK PERTAMA”</span>);</p>

                <p>II. ……………………, Sebuah perusahaan yang didirikan dan didaftarkan secara sah berdasarkan hukum Singapura, dengan alamat tempat usaha utamanya di ……………………, dalam hal ini diwakili oleh …………… bertindak dalam jabatannya sebagai ________ sehingga sah bertindak untuk dan atas nama …………………… (selanjutnya disebut sebagai <span class='bold'>“PIHAK KEDUA”</span>)<br/>
                    <span class='bold'>PIHAK PERTAMA</span> dan <span class='bold'>PIHAK KEDUA</span> selanjutnya secara bersama akan disebut sebagai <span class='italic'>“Para Pihak”</span> atau sendiri-sendiri sebagai <span class='italic'>“Pihak”</span>, sebagaimana disebutkan di atas.</p>

                <ol class='justify-ol'>
                    <li><span class='bold'>BAHWA</span>, <span class='bold'>PIHAK PERTAMA</span> sebagai perseroan terbatas sebagai penyedia jasa asuransi jiwa berdasarkan prinsip syariah, sepakat untuk bekerjasama dengan <span class='bold'>PIHAK KEDUA</span> sebagaimana dijelaskan dalam Perjanjian ini.</li>
                    <li><span class='bold'>BAHWA</span>, <span class='bold'>PIHAK KEDUA</span> adalah suatu Perusahaan yang bergerak dalam bidang teknologi asuransi sesuai dengan tujuan yang dimaksud dalam perjanjian ini;</li>
                    <li>Bahwa <span class='bold'>PARA PIHAK</span> berencana untuk melakukan kerja sama pemanfaatan teknologi asuransi (""<span class='bold'>Tujuan</span>"").</li>
                    <li>Bahwa, dalam melaksanakan Tujuan sebagaimana dimaksud huruf c di atas, <span class='bold'>PARA PIHAK</span> dalam Perjanjian ini sepakat dengan ketentuan bahwa <span class='bold'>PARA PIHAK</span> dapat bertindak selaku pihak yang menerima Informasi Rahasia (""<span class='bold'>Pihak Penerima</span>"") dari salah satu Pihak selaku pihak yang memberikan Informasi Rahasia (""<span class='bold'>Pihak Pemberi</span>""), dimana Pihak Pemberi telah dan/atau akan mengungkapkan Informasi Rahasia sesuai dengan ketentuan dalam Tujuan yang diatur dalam Perjanjian ini.;</li>
                </ol>

                <p>Berdasarkan hal-hal sebagaimana disebut di atas, PARA PIHAK dengan ini sepakat untuk mengadakan dan menandatangani PERJANJIAN ini dengan syarat-syarat dan ketentuan sebagai berikut:</p>

                <p class='title'><h3 style='text-align:center;'><b>PASAL 1<br>DEFINISI</b></h3></p>
                <ol class='justify-ol'>
                    <li>Seluruh informasi mengenai informasi keuangan, usaha, informasi mengenai pelanggan, termasuk di dalamnya yaitu database nomor telepon pelanggan dan/atau semua informasi yang berkaitan dengan kegiatan dan rencana usaha, penjualan, program dan barang dagangan, informasi, peluang usaha dan rencana termasuk strategi pemasaran dari Pihak Pemberi dan/atau afiliasinya;</li>
                    <li>“Informasi Rahasia” juga meliputi seluruh Informasi Rahasia dari Pihak Pemberi yang berkaitan dengan Informasi pihak ketiga yang mungkin diungkapkan oleh Pihak Pemberi kepada Pihak Penerima untuk keperluan pelaksanaan Tujuan ini baik secara lisan, tertulis, ataupun dengan cara lain yang dimungkinkan;</li>
                    <li>Kewajiban-kewajiban bagi Pihak Penerima tidak akan berlaku khusus dalam hal:
                      <ol type='a'>
                        <li>Informasi tersebut sudah menjadi informasi publik dan pengungkapannya bukan merupakan pelanggaran dan/atau kesalahan Pihak Penerima;</li>
                        <li>Informasi tersebut telah sah berada dalam penguasaan Pihak Penerima sebelum diberikan dan/atau diungkapkan oleh Pihak Pertama sehingga pada saat diungkapkan tidak lagi dikategorikan sebagai Informasi Rahasia.</li>
                      </ol>
                    </li>
                    <li>Pengungkapan Informasi Rahasia oleh Pihak Penerima tidak dapat dianggap sebagai pelanggaran dari kewajiban dalam PERJANJIAN ini dalam hal:
                      <ol type='a'>
                        <li>Pengungkapan Informasi Rahasia dilakukan untuk memenuhi putusan maupun perintah dari Pengadilan maupun badan pemerintahan yang berwenang;</li>
                        <li>Pengungkapan Informasi Rahasia dilakukan karena diwajibkan oleh hukum dan peraturan perundang-undangan;</li>
                        <li>Diperlukan untuk memenuhi kewajiban dari Pihak Pemberi;</li>
                        <li>Pihak Penerima wajib dengan segera memberikan pemberitahuan secara tertulis sebelumnya kepada Pihak Pemberi perihal ketentuan yang diatur dalam Pasal 1 huruf b di atas, sehingga Pihak Pemberi dapat mengambil segala macam langkah yang dibutuhkan untuk melindungi Informasi Rahasia tersebut.</li>
                      </ol>
                    </li>
                </ol>
                  
                <p class='title'><h3 style='text-align:center;'><b>PASAL 2<br>LARANGAN PENGUNGKAPAN KERAHASIAAN</b></h3></p>
                   
                    <p>Pihak Penerima menerima dan sepakat, terkait dengan Informasi Rahasia:</p>
                    <ol class='justify-ol'>
                        <li>Untuk mempergunakan informasi Rahasia hanya untuk tujuan sebagaimana disepakati bersama oleh Para Pihak;</li>
                        <li>Menjaga kerahasiaan dan tidak mengungkapkan atau meberikan akses terhadap Informasi Rahasia tersebut kepada pihak ketiga, termasuk tapi tidak terbatas pada afiliasi dan konsultan Pihak Penerima, kecuali Pihak Penerima telah memasuki perjanjian yang serupa untuk mengikat diri mereka dengan ketentuan dimaksud, dan Pihak Penerima akan menanggung kewajiban dan untuk bertanggung jawab terhadap segala kebocoran atas Perjanjian ini;</li>
                        <li>Untuk menerapkan dan melakukan penjagaan yang dianggap perlu untuk menjamin kerahasian informasi dimaksud termasuk kepada karyawan yang memiliki kebutuhan untuk mengetahui tujuan yang sama;</li>
                        <li>Seluruh Informasi Rahasia (termasuk salinan yang dibuat oleh Pihak Penerima) harus tetap menjadi satu kesatuan sebagai hak milik Pihak Pemberi, dan harus dikembalikan dan/atau dihancurkan oleh Pihak Penerima segera setelah dipergunakan dan/atau setelah berakhirnya Perjanjian ini.</li>
                   </ol>
                                      
                   <p class='title'><h3 style='text-align:center;'><b>PASAL 3<br>KEPEMILIKAN DAN PENGEMBALIAN INFORMASI</b></h3></p>
                   
                    <ol class='justify-ol'>
                        <li>Semua Informasi Rahasia milik Pihak Pemberi mengenai ruang lingkup pekerjaan dan permintaan dari Pihak Pemberi dalam kontrak yang akan dibahas dan disepakati dalam perjanjian terpisah.</li>
                        <li>Atas permintaan dari Pihak Pemberi kepada Pihak Penerima maka dalam waktu 5 (lima) hari kerja setelah permintaan tersebut, maka Pihak Penerima wajib dengan segera untuk menghancurkan dan/atau mengembalikan kepada Pihak Pemberi, berdasarkan pilihan dari Pihak Pemberi:
                            <ol type='a'>
                                <li>Seluruh informasi rahasia yang diberikan oleh Pihak Pemberi kepada Pihak Penerima, baik berbentuk hard copy dan/atau soft copy;</li>
                                <li>Seluruh alat ataupun dokumentasi yang berada dalam penguasaan Pihak Penerima;</li>
                                <li>Surat Pernyataan tertulis dari Pihak Penerima bahwa kewajiban Pihak Penerima atas pengembalian dan/atau penghapusan Informasi Rahasia dalam PERJANJIAN ini telah dilaksanakan.</li>
                            </ol>
                        </li>
                        <li>To implement and maintain such safeguards as deemed necessary to ensure the confidentiality of such information including to employees who have a need to know for the same purpose;</li>
                        <li>All Confidential Information (including copies made by the Receiving Party) shall remain as an integral part of the property of the Giving Party, and shall be returned and/or destroyed by the Receiving Party immediately after use and/or upon termination of this Agreement.</li>
                   </ol>

                   <p class='title'><h3 style='text-align:center;'><b>PASAL 4<br>PERIODE WAKTU</b></h3></p>
                   <p>Perjanjian Kerahasiaan ini berlaku sejak tanggal sebagaimana tersebut di atas, semua informasi-informasi rahasia yang telah diberikan oleh PARA PIHAK akan selalu menjadi rahasia dan mengikat PARA PIHAK tanpa memandang pemutusan dan/atau berakhirnya Perjanjian Kerjasama (""Periode Perjanjian Kerahasiaan"").</p>                   
                  
                   <p class='title'><h3 style='text-align:center;'><b>PASAL 5<br>PENGALIHAN</b></h3></p>
                   <p>PARA PIHAK tidak akan mengalihkan hak dan kewajibannya dalam PERJANJIAN ini tanpa persetujuan tertulis dari PIHAK lain, dimana persetujuan tersebut tidak akan ditunda tanpa alasan yang jelas dan wajar.</p>
                  
                   <p class='title'><h3 style='text-align:center;'><b>PASAL 6<br>PENGGANTIAN YANG ADIL</b></h3></p>
                   <p>Pelanggaran PARA PIHAK terhadap baik isi dari Perjanjian maupun kerjasama yang terdapat dalam Perjanjian ini dapat mengakibatkan kerusakan yang tak dapat diperbaiki dan berkelanjutan bagi Pihak lain, dimana mungkin tidak akan terdapat penggantian yang sesuai secara hukum, karenanya Pihak yang telah melakukan kesalahan tersebut (”Pihak yang Merugikan”) berkewajiban untuk memberikan penggantian yang adil dan/atau melakukan seluruh tindakan yang diperlukan Pihak yang dirugikan (”Pihak yang Dirugikan”), ataupun segala usaha lain yang dimungkinkan (termasuk di dalamnya penggantian keuangan apabila sesuai dan diperlukan).</p>              
                  
                   <p class='title'><h3 style='text-align:center;'><b>ARTICLE 7<br>HUKUM YANG MENGATUR DAN PENYELESAIAN PERSELISIHAN</b></h3></p>
                    <ol class='justify-ol'>
                        <li><span class='bold'>PARA PIHAK</span> sepakat bahwa PERJANJIAN ini ditafsirkan, diartikan dan diatur berdasarkan hukum Negara Singapura</li>
                        <li><span class='bold'>PARA PIHAK</span> sepakat untuk tunduk kepada seluruh peraturan dan perundang-undangan yang berlaku di Negara Singapur.</li>
                        <li>Dalam hal terjadi perselisihan di antara <span class='bold'>PARA PIHAK</span> sebagai akibat dari penafsiran atau pelaksanaan Perjanjian Kerahasiaan ini, <span class='bold'>PARA PIHAK</span> sepakat untuk menyelesaikannya terlebih dahulu secara musyawarah untuk mufakat.</li>
                        <li>Apabila dalam waktu 30 (tiga puluh) hari kerja tidak tercapainya mufakat bagi kedua belah PIHAK, maka <span class='bold'>PARA PIHAK</span> sepakat untuk menyelesaikannya melalui Panitera Pengadilan Negeri Jakarta Selatan.</li>
                   </ol>
                  
                   <p class='title'><h3 style='text-align:center;'><b>PASAL 8<br>KETENTUAN LAIN</b></h3></p>
                    <ol class='justify-ol'>
                        <li><span class='bold'>PARA PIHAK</span> sepakat untuk melaksanakan Perjanjian ini dengan rasa penuh tanggung jawab dan tunduk pada seluruh ketentuan yang berlaku.</li>
                        <li><span class='bold'>PARA PIHAK</span> mengakui dan menyatakan bahwa sehubungan dengan penandatanganan perjanjian ini masing – masing PIHAK:
                            <ol type='a'>
                                <li>Menandatangani perjanjian ini setelah meneliti secara patut;</li>
                                <li>Telah membaca dan memahami secara penuh ketentuan perjanjian ini;</li>
                                <li>Telah mendapatkan kesempatan yang memadai untuk memeriksa dan mengkonfirmasikan semua ketentuan dalam perjanjian ini beserta semua fakta dan kondisi yang terkait.</li>
                                <li>Dalam hal terjadi ketidaksesuaian antara versi Bahasa Inggris dan Bahasa Indonesia dari Perjanjian ini, versi Bahasa Inggris yang akan berlaku.</li>
                            </ol>
                        </li>
                   </ol>

                    <p>
                        Thus this Agreement is made and signed by the PARTIES on sufficient stamp duty in duplicate 2 (two) 
                        and has the same legal force, 1 (one) set for the FIRST PARTY and 1 (one) set for the SECOND PARTY.
                    </p>
                    <p>
                        Signed by / Ditandatangani oleh<br>
                        For and on behalf of / Untuk dan atas nama<br>
                        <b>PT ASURANSI TAKAFUL KELUARGA</b>
                    </p>
                    <br><br><br>
                    <p>
                        Name/Nama: _PENNY HIKMAHWATI<br>
                        Designation/Kedudukan: <u>Direktur Operasional</u><br>
                        Date/Tanggal: ________________
                    </p>            

            </td>
        </tr>
    </table>
</body>
</html>
";


            return html
                .Replace("{{TanggalPengajuan}}", tanggalpengajuan)
                .Replace("{{NomorSurat}}", nomorsurat)
                .Replace("{{NamaPerusahaan}}", namaPerusahaan)
                .Replace("{{AlamatPerusahaan}}", alamat)
                .Replace("{{NamaRumahSakit}}", namaRS);
        }

        public static string DocumentNDANasional(
             string tanggalpengajuan,
           string nomorsurat,
           string namaPerusahaan,
           string alamat,
           string namaRS)
        {
            string tandaTanganPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "TTD.jpg");
            string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "STEMPEL.jpg");

            // Pastikan file benar-benar ada
            if (!File.Exists(tandaTanganPath))
                throw new FileNotFoundException("File tanda tangan tidak ditemukan", tandaTanganPath);

            if (!File.Exists(logoPath))
                throw new FileNotFoundException("File logo tidak ditemukan", logoPath);

            // Untuk HTML Spire.Doc: langsung pakai path absolut (Windows pakai slash / biar aman)
            string tandaTanganSrc = tandaTanganPath.Replace("\\", "/");
            string logoSrc = logoPath.Replace("\\", "/");


            string html = @"
            <!DOCTYPE html>
            <html lang='id'>
            <head>
                <meta charset='UTF-8' />
                <title>Perjanjian Kerjasama Sementara</title>
                <style type='text/css'>
                    body {
                        font-family: Arial, sans-serif;
                        font-size: 10pt;
                        line-height: 1.6;
                        margin: 20px;
                        color: #333;
                    }
                    p {
                        margin: 0 0 14px;
                        text-align: justify;
                    }
                    .center {
                        text-align: center;
                        font-weight: bold;
                    }
                    ol.romawi {
                        list-style-type: upper-roman; /* otomatis I, II, III */
                        padding-left: 18px;
                    }
                    ol.romawi li {
                        margin-bottom: 12px;
                        text-align: justify;
                        text-justify: inter-word;
                    }
                    .abjad {
                        list-style-type: lower-alpha; /* otomatis a., b., c. */
                        padding-left: 20px;           /* jarak dari kiri */
                    }
                    .abjad li {
                        margin-bottom: 10px;
                        text-align: justify;
                        text-justify: inter-word;
                    }
                    .angka {
                        list-style-type: decimal; /* default: 1,2,3 */
                        padding-left: 20px;       /* jarak dari kiri */
                    }
                    .angka li {
                        margin-bottom: 8px;
                        text-align: justify;      /* teks rata kanan-kiri */
                        text-justify: inter-word;
                    }
                    .signature-table {
                        width: 100%;
                        margin: 40px auto;
                        border-collapse: collapse;
                    }
                    .signature-table td {
                        width: 50%;
                        text-align: center;
                        vertical-align: top;
                        padding: 40px 20px;   /* jarak besar untuk ruang tanda tangan */
                        border: none;         /* hilangkan garis tabel */
                    }
                    .signature-table strong {
                        font-weight: bold;
                    }
                    .signature-table u {
                        text-decoration: underline;
                    }
               </style>
            </head>
            <body>

                <p class='center'>PERJANJIAN KERAHASIAAN DATA BERSAMA</p>
                <p class='center'>ANTARA<br/> PT ……………………………………<br/>DENGAN<br/> PT ASURANSI TAKAFUL KELUARGA</p>
                <p class='center'>Nomor : ...........</p>
                <p class='center'>Nomor : ...........</p>

                <p>
                    Perjanjian Kerahasiaan Data Bersama (untuk selanjutnya disebut sebagai “Perjanjian”) 
                    ini dibuat pada tanggal ……………………………………. , oleh dan antara:
                </p>

                <ol class='romawi'>
                    <li>
                        <strong>PT ………………, berkedudukan di …………, Jalan ……………….. No. ………………., Jakarta ………., 
                        dalam hal ini diwakili oleh ……………. selaku ………………., berdasarkan Akta No. ......... tanggal .................. 
                        dibuat dihadapan ..............., SH., Notaris di ................., dan telah mendapat penerimaan pemberitahuan 
                        dari Menteri Hukum dan Hak Asasi Manusia Republik Indonesia melalui surat No. ……………………. tanggal ………………….., 
                        dari dan karenanya sah bertindak mewakili Direksi PT. …………………………., </strong>untuk selanjutnya disebut 
                        “PIHAK PERTAMA”;
                    </li>

                    <li>
                        <strong>PT ASURANSI TAKAFUL KELUARGA (“Tafakul”)</strong>, berkedudukan di Jakarta, Graha Takaful Indonesia, 
                        Jalan Mampang Prapatan Raya No. 100, Jakarta Selatan, dalam hal ini diwakili oleh <strong>Penny Hikmahwati</strong> 
                        selaku <strong>Direktur Operasional</strong>, berdasarkan Akta No. 24 tanggal 14 Agustus 2023 dibuat dihadapan 
                        Arry Supratno, SH., Notaris di Jakarta, dan telah mendapat penerimaan pemberitahuan dari Menteri 
                        Hukum dan Hak Asasi Manusia Republik Indonesia melalui surat No. AHU-AH.01.09-0153011 tanggal 
                        18 Agustus 2023, dari dan karenanya sah bertindak mewakili Direksi PT. Asuransi Takaful Keluarga, 
                        untuk selanjutnya disebut PIHAK KEDUA.;
                    </li>
                </ol>
                
                <p>Pihak Pertama dan Pihak Kedua selanjutnya masing-masing disebut sebagai “Pihak”, dan secara bersama-sama disebut sebagai “Para Pihak”.</p>
                <p>Para Pihak tersebut di atas terlebih dahulu menerangkan hal-hal sebagai berikut:</p>
                <ol type='a' class='abjad'>
                    <li>PIHAK PERTAMA adalah perseroan terbatas yang bergerak di bidang ………………………….;</li>
                    <li>PIHAK KEDUA adalah perseroan terbatas yang bergerak dalam bidang Asuransi Jiwa yang menjalankan kegiatan operasional berdasarkan prinsip Syariah;</li>
                    <li>Para Pihak bermaksud untuk mendiskusikan sesuatu hubungan usaha potensial yang saling menguntungkan (“Transaksi Potensial”);</li>
                    <li>Para Pihak akan saling bertukar Informasi Rahasia tertentu (sebagaimana didefinisikan di bawah ini), yang tidak ingin dipublikasikan atau diungkapkan kepada pihak ketiga manapun tanpa persetujuan tertulis sebelumnya dari Pihak Pengungkap (sebagaimana didefinisikan di bawah ini), untuk tujuan mendiskusikan Transaksi Potensial (“Tujuan”); dan</li>
                    <li>Para Pihak setuju untuk menandatangani Perjanjian ini untuk menjamin kerahasiaan Informasi Rahasia yang diungkapkan oleh salah satu Pihak kepada Pihak lainnya. </li>
                </ol>
                
                <p>Berdasarkan hal-hal tersebut di atas, Para Pihak dengan ini sepakat untuk membuat dan melaksanakan Perjanjian ini dengan syarat dan ketentuan sebagai berikut:</p> 
                
                <p class='center'>PASAL 1</br>DEFINISI DAN INTERPRETASI</p>
                <p>“Informasi Rahasia” adalah semua informasi yang bersifat rahasia dalam segala bentuk dan yang diungkapkan dengan segala cara dari satu Pihak (“Pihak Pengungkap”) dan/atau Perwakilannya kepada Pihak lainnya (“Pihak Penerima”) yang mencakup:</p>
                <ol class='angka'>
                    <li>Pengungkap adalah Pihak yang memberikan, mengungkapkan, dan/atau membuka Informasi Rahasia kepada Pihak lain sehubungan dengan pelaksanaan perjanjian.</li>
                    <li>Penerima adalah Pihak yang menerima Informasi Rahasia dari Pihak Pemberi sehubungan dengan pelaksanaan perjanjian</li>
                    <li>Semua informasi (baik lisan, elektronik, tulisan, ataupun bentuk lain) termasuk, tetapi tidak terbatas pada, data dan/atau informasi pribadi para konsumen, direktur, karyawan dan kegiatan usaha dari masing-masing Pihak, yang diberikan (baik sebelum maupun setelah tanggal Perjanjian ini) oleh atau atas nama Para Pihak (termasuk oleh komisaris, direktur, pejabat, karyawan, afiliasi, agen, penasihat, atau wakil dari mereka masing-masing), baik yang disusun oleh Pihak yang bersangkutan maupun oleh pihak lain;</li>
                    <li>Semua informasi rahasia milik masing-masing Pihak yang telah diungkapkan kepada Pihak Penerima sehubungan dengan Transaksi Potensial (termasuk, tetapi tidak terbatas, pada semua informasi keuangan dan informasi hak milik, metodologi, pangkal data (database), kekayaan intelektual, rahasia dagang dan ilmu-ilmu yang bersifat rahasia); </li>
                    <li>Semua informasi rahasia milik Para Pihak dan afiliasinya yang telah diungkapkan kepada Pihak Penerima sehubungan dengan Transaksi Potensial (termasuk, tetapi tidak terbatas, pada semua informasi keuangan dan informasi hak milik, metodologi, pangkal data (database), kekayaan intelektual, rahasia dagang dan ilmu-ilmu yang bersifat rahasia);</li>
                    <li>Isi dari seluruh salinan dan petikan-petikannya, seluruh analisa, kompilasi, perhitungan, kajian, ringkasan, memorandum, nota dan catatan-catatan atau dokumen-dokumen lain yang disusun oleh Pihak Penerima atau Wakil dari Pihak Penerima atau sebaliknya yang disusun atas namanya, yang memuat atau mengandung informasi-informasi sebagaimana dimaksud dan berdasarkan atau memasukan ke dalamnya informasi-informasi sebagaimana dimaksud dalam ayat 1, ayat 2 dan ayat 3 di atas;</li>
                    <li>Informasi-informasi yang wajib dijaga kerahasiaannya sesuai dengan peraturan perundang-undangan yang berlaku di Republik Indonesia.</li>
                </ol>
                
                <p class='center'>PASAL 2</br>RUANG LINGKUP</p>
                <p>Pihak Pengungkap akan memberikan Informasi Rahasia kepada Pihak Penerima dan petugas, direktur, sekutu, anggota, pegawai, agen, konsultan, penasihat, kuasa dan/atau akuntan (secara bersama-sama disebut sebagai “Perwakilan”), Pihak Penerima dan perwakilannya setuju untuk memperlakukan Informasi Rahasia sesuai dengan ketentuan-ketentuan dalam Perjanjian ini.</p>

                <p class='center'>PASAL 3</br>TIDAK TERMASUK INFORMASI RAHASIA</p>
                <p>Kewajiban kerahasiaan dalam Perjanjian ini tidak berlaku terhadap Informasi Rahasia dengan kriteria sebagai berikut:</p>
                <ol class='angka'>
                    <li>Telah menjadi milik atau diketahui oleh Pihak Penerima atau salah satu Perwakilannya sebelum pengungkapan oleh Pihak Pengungkap;</li>
                    <li>Dikembangkan secara mandiri oleh Pihak Penerima atau salah satu Perwakilannya;</li>
                    <li>Diungkapkan oleh kepada Pihak Penerima atau salah satu Perwakilannya oleh pihak ketiga tanpa adanya kewajiban kerahasiaan; atau</li>
                    <li>Merupakan atau menjadi suatu bagian dari domain publik yang tidak diakibatkan oleh kesalahan atau pelanggaran Perjanjian ini oleh Pihak Penerima atau Perwakilannya.</li>
                </ol>
                
                <p class='center'>PASAL 4</br>KEWAJIBAN MENJAGA KERAHASIAAN</p>
                <ol class='angka'>
                    <li>Pihak Penerima wajib menjaga kerahasiaan setiap dan seluruh Informasi Rahasia yang diterima dari Pihak Pengungkap dan tidak akan mengungkapkan Informasi Rahasia dengan cara apapun; dengan ketentuan:
                        <ol type='a' class='abjad'>
                            <li>Pihak Penerima dapat mengungkapkan setiap Informasi Rahasia berdasarkan persetujuan tertulis dari Pihak Pengungkap; </li>
                            <li>Setiap Informasi Rahasia yang mungkin diungkapkan kepada Perwakilan Pihak Penerima yang sewajarnya membutuhkan akses terhadap Informasi Rahasia untuk tujuan membantu evaluasi Tujuan; dan </li>
                            <li>Dengan tunduk pada ketentuan Pasal 7 Perjanjian, Pihak Penerima (termasuk Perwakilannya) dapat mengungkapkan setiap Informasi Rahasia apabila diwajibkan atau diminta oleh pengadilan atau setiap pihak berwenang di pemerintahan atau berdasarkan peraturan manapun, yang mana Pihak Penerima (termasuk Perwakilannya) setuju untuk memberitahukan Pihak Pengungkap mengenai pengungkapan tersebut dalam 1 (satu) hari kerja setelah pengungkapan tersebut.</li>
                         </ol>
                    </li>
                    <li>Pihak Penerima wajib memberitahukan Pihak Pengungkap secepatnya setelah ditemukannya penggunaan tanpa otorisasi, atau setiap pelanggaran apapun atas Perjanjian ini oleh Pihak Penerima (atau Perwakilannya), dan akan bekerjasama dengan Pihak Pengungkap dengan cara wajar untuk membantu Pihak Pengungkap mendapatkan kembali kepemilikan Informasi Rahasia dan menghindari adanya penggunaan tanpa otorisasi lebih lanjut.</li>
                    <li>Pihak Penerima termasuk Perwakilannya berjanji untuk mematuhi syarat dan ketentuan dalam Perjanjian ini dan akan bertanggung jawab atas setiap pelanggaran ketentuan Perjanjian ini. Pihak Penerima sepakat untuk mengganti rugi Pihak Pengungkap dari setiap kerugian atau kerusakan yang disebabkan atau sehubungan dengan pengungkapan atas setiap Informasi Rahasia yang dilakukan sebagai akibat pelanggaran Perjanjian ini.</li>
                    <li>Seluruh Informasi Rahasia akan tetap menjadi milik Pihak Pengungkap. Dengan mengungkapkan informasi kepada Pihak Penerima atau setiap Perwakilannya, Pihak Pengungkap tidak memberikan hak secara tersurat ataupun tersirat kepada Pihak Penerima atau setiap Perwakilannya atas setiap paten, hak cipta, merek dagang, atau rahasia-rahasia dagang milik Pihak Pengungkap.</li>
                </ol>

                <p class='center'>PASAL 5</br>KERAHASIAAN ATAS ADANYA DISKUSI</p>
                <p>Tanpa persetujuan tertulis sebelumnya dari Pihak lainnya, atau kecuali sebagaimana diharuskan berdasarkan hukum, peraturan atau proses hukum yang berlaku, baik Pihak Pengungkap, Pihak Penerima, ataupun setiap pihak yang bertindak atas nama salah satu Pihak (termasuk Perwakilan Pihak Penerima) tidak akan mengungkapkan kepada pihak ketiga manapun bahwa diskusi sedang berlangsung di antara Para Pihak.</p>
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <p class='center'>PASAL 6</br>PENGEMBALIAN ATAU PENGHANCURAN INFORMASI RAHASIA</p>
                <ol class='angka'>
                    <li>Pihak Penerima setuju bahwa kapanpun diminta oleh Pihak Pengungkap secara tertulis atau, tanpa permintaan tertulis oleh Pihak Pengungkap, pada saat pengakhiran Perjanjian ini, agar:
                        <ol type='a' class='ajad'>
                            <li>Seluruh salinan dari Informasi Rahasia yang diberikan kepada Pihak Penerima oleh atau atas nama Pihak Pengungkap wajib dikembalikan kepada Pihak Pengungkap paling lama dalam 5 (lima) hari kerja setelah tanggal permintaan tertulis tersebut; dan </li>
                            <li>Seluruh catatan, studi, laporan, memorandum dan dokumen lainnya yang dipersiapkan oleh Pihak Penerima atau Perwakilannya yang mengandung atau mencerminkan Informasi Rahasia wajib dihancurkan.</li>
                            <li>Dalam hal Informasi Rahasia tersebut dihancurkan, maka Pihak Penerima wajib mengirimkan bukti pemusnahan kepada Pihak Pengungkap dalam waktu selambat-lambatnya 3 (tiga) hari kalender setelah permintaan pemusnahan Informasi Rahasia disampaikan oleh Pihak Pengungkap.</li>
                        </ol>
                    </li>
                    <li>
                        Ketentuan Ayat (1) huruf a di atas tidak berlaku untuk Informasi Rahasia yang disimpan secara elektronik, menghancurkan Informasi Rahasia yang disimpan secara elektronik tersebut hanya sejauh yang sewajarnya bisa dilakukan.
                    </li>
                    <li>
                        Pihak Penerima dan Perwakilannya dapat menahan salinan Informasi Rahasia sejauh dimana penahanan tersebut diwajibkan untuk menunjukkan kepatuhan terhadap hukum, aturan, peraturan atau standar profesional yang berlaku, atau sesuai dengan ketentuan kebijakan internal terkait penyimpanan rekaman-rekaman secara umum, dengan ketentuan bahwa Pihak Pengungkap akan diberitahukan dan setuju untuk adanya penahanan tersebut dan bahwa setiap informasi yang ditahan akan disimpan sesuai dengan syarat-syarat dalam Perjanjian ini; 
                    </li>
                </ol>

                <p class='center'>PASAL 7</br>PENGUNGKAPAN KEPADA PIHAK KETIGA</p>
                <p>Kecuali untuk pemberian informasi sebagaimana dimaksud dalam Pasal 8 Perjanjian, sebelum Pihak Penerima melakukan pengungkapan Informasi Rahasia kepada pihak ketiga, termasuk tetapi tidak terbatas pada para konsultan, akuntan publik atau pejabat lokal, Pihak Penerima wajib:</p>
                <ol class='angka'>
                    <li>
                        Mendapatkan persetujuan secara tertulis terlebih dahulu dari Pihak Pengungkap untuk mengungkapkan Informasi Rahasia kepada pihak ketiga tersebut, dan
                    </li>
                    <li>Mendapatkan persetujuan tertulis, yang diberikan oleh pihak ketiga tersebut, antara Pihak Penerima dan pihak ketiga untuk:
                        <ol type='a' class='ajad'>
                            <li>Menahan semua Informasi Rahasia sebagaimana ditentukan dalam Perjanjian ini dan untuk tidak menggunakannya untuk tujuan selain yang ditentukan dalam Perjanjian ini, dan</li>
                            <li>Menjamin bahwa setiap pengungkapan dari Informasi Rahasia harus sesuai dengan hukum yang berlaku.</li>
                        </ol>
                    </li>
                </ol>

                <p class='center'>PASAL 8</br>PENGUNGKAPAN INFORMASI RAHASIA</p>
                <ol class='angka'>
                    <li>
                        Pihak Penerima dapat mengungkapkan ataupun menggunakan Informasi Rahasia untuk tujuan awal termasuk tetapi tidak terbatas pengungkapan oleh Pihak Penerima kepada pihak Reasuransi, maupun pihak lain yang bekerjasama dengan Pihak Penerima guna memenuhi kewajiban Pihak Penerima berdasarkan Perjanjian ini ataupun perjanjian yang akan dibuat kemudian hari.
                    </li>
                    <li>Dalam hal Pihak Penerima atau setiap Perwakilannya diwajibkan atau diminta untuk mengungkapkan Informasi Rahasia atas perintah pengadilan atau oleh otoritas pemerintah atau pengaturan manapun, Pihak Penerima wajib (dengan ketentuan bahwa perbuatan ini diizinkan dan sejauh sewajarnya dapat dilakukan):
                        <ol type='a' class='ajad'>
                            <li>Secepatnya memberitahukan Pihak Pengungkap, paling lambat dalam 1 (satu) hari kerja setelah tanggal permintaan atau perintah untuk mengungkapkan, oleh karenanya Pihak Pengungkap dapat mencari perintah perlindungan atau upaya hukum wajar lainnya;</li>
                            <li>Bekerja sama dengan Pihak Pengungkap, dengan biaya dan pengeluaran Pihak Pengungkap sendiri, dalam usahanya untuk mencari perintah perlindungan atau upaya hukum</li>
                        </ol>
                    </li>
                </ol>

                <p class='center'>PASAL 9</br>UPAYA HUKUM</p>
                <p>Para Pihak mengakui bahwa apabila terjadi pelanggaran atas Perjanjian ini dan menyebabkan kerugian yang tidak dapat diperbaiki oleh Pihak Pengungkap maka Pihak Pengungkap berhak untuk mencari penyelesaian atas pelanggaran Perjanjian ini yang dilakukan oleh Pihak Penerima atau Perwakilannya.</p>

                <p class='center'>PASAL 10</br>TANPA IZIN</p>
                <p>Hal-hal yang terdapat dalam Perjanjian ini tidak akan diartikan sebagai pemberian atau hak apapun kepada Pihak Penerima dan Perwakilannya untuk menggunakan Informasi Rahasia atau setiap bagian di dalamnya untuk tujuan apapun (selain untuk Tujuan) termasuk atas izin atau hak atas setiap paten, hak cipta atau hak kekayaan intelektual.</p>

                <p class='center'>PASAL 11</br>PERNYATAAN PUBLIK</p>
                <p>Para Pihak sepakat bahwa segala pembicaraan diantara mereka akan dilakukan secara rahasia. Pihak Penerima tidak akan memberikan pernyataan kepada pers atau publik mengenai pembicaraan yang berhubungan dengan suatu transaksi antara Para Pihak atau membuka dengan suatu cara kepada pihak ketiga fakta dari pembicaraan yang telah dilakukan, tanpa persetujuan tertulis sebelumnya dari Pemberi.</p>

                <p class='center'>PASAL 12</br>PERNYATAAN PUBLIK</p>
                <p>Kegagalan, keterlambatan atau penundaan oleh salah satu Pihak untuk menjalankan haknya berdasarkan Perjanjian ini atau kegagalan, keterlambatan atau penundaan salah satu Pihak untuk meminta Pihak lainnya agar memenuhi ketentuan-ketentuan dalam Perjanjian ini, tidak akan dianggap sebagai pengesampingan atau pelepasan hak, wewenang atau tuntutan oleh salah satu P ihak untuk dikemudian hari menuntut Pihak lainnya untuk memenuhi kewajibannya berdasarkan ketentuan-ketentuan dalam Perjanjian ini.</p>
                
                <p class='center'>PASAL 13</br>KESELURUHAN PERJANJIAN DAN AMANDEMEN</p>
                <p>Hal-hal lain yang belum atau tidak cukup diatur dalam Perjanjian ini, akan ditetapkan dan disepakati secara musyawarah oleh Para Pihak dan akan dituangkan dalam suatu perjanjian tertulis yang berlaku efektif setelah ditandatangani oleh Para Pihak, yang merupakan satu kesatuan dan bagian yang tidak terpisahkan dari Perjanjian ini.</p>

                <p class='center'>PASAL 14</br>JANGKA WAKTU</p>
                <p>Perjanjian ini mulai berlaku sejak tanggal ditandatangani, dan segala kewajiban kerahasiaan yang diatur dalam Perjanjian ini tetap berlaku walaupun setelah penyelesaian, pengakhiran ataupun pembatalan Perjanjian.</p>

                <p class='center'>PASAL 15</br>HUKUM YANG BERLAKU, PENYELESAIAN PERSELISIHAN DAN DOMISILI HUKUM</p>
                <ol class='angka'>
                    <li>Perjanjian ini dibuat, ditafsirkan dan dilaksanakan berdasarkan hukum negara Republik Indonesia.</li>
                    <li>Setiap perselisihan yang timbul sehubungan dengan Perjanjian ini, akan diupayakan untuk diselesaikan terlebih dahulu oleh Para Pihak dengan melakukan musyawarah untuk mencapai mufakat, baik dengan menggunakan jasa mediator independen maupun melalui pembicaraan antara wakil-wakil masing-masing Pihak.</li>
                    <li>Apabila penyelesaian perselisihan secara musyawarah tidak berhasil mencapai mufakat sampai dengan 30 (tiga puluh) Hari Kalender sejak dimulainya musyawarah tersebut, maka <strong>PARA PIHAK</strong>  sepakat dan setuju untuk memilih tempat kedudukan hukum yang tetap dan tidak berubah di Kantor Kepaniteraan Pengadilan Negeri dimana tergugat berdomisili. Pilihan kedudukan hukum tersebut tidak membatasi <strong>PARA PIHAK</strong> untuk memilih penyelesaian permasalahan di tempat kedudukan hukum lainnya sepanjang tetap sesuai dengan ketentuan hukum dan peraturan perundang-undangan yang berlaku.</li>
                </ol>

                <p class='center'>PASAL 16</br>LAIN-LAIN</p>
                <ol class='angka'>
                    <li>Perjanjian ini tidak membuktikan atau membuat suatu perwakilan, persekutuan, atau hubungan lainnya yang serupa antara Para Pihak. Setiap Pihak tidak berhak menggunakan nama, nama dagang, merek dagang, merek jasa, atau tanda lainnya dari Pihak lain dalam periklanan, publisitas, atau kegiatan lainnya.</li>
                    <li>Jika salah satu ketentuan Perjanjian ini tidak dapat diberlakukan, Para Pihak sepakat bahwa ketidakberlakuan tersebut tidak akan mempengaruhi keberlakuan dari ketentuan yang lain dalam Perjanjian ini, dan selanjutnya Para Pihak sepakat untuk mengganti ketentuan yang tidak berlaku tersebut dengan ketentuan yang berlaku yang sedapat mungkin mencerminkan maksud semula dari Para Pihak.</li>
                    <li>Perjanjian ini tidak dapat dialihkan oleh salah satu Pihak tanpa izin tertulis dari Pihak lainnya.</li>
                    <li>Perjanjian ini termasuk konsiderannya merupakan pernyataan kesepakatan Para Pihak yang lengkap dan eksklusif, dan menggantikan semua kesepakatan lainnya, secara lisan maupun tulisan, sehubungan dengan hal-hal pokok dalam Perjanjian ini. Perjanjian ini hanya dapat diubah berdasarkan kesepakatan tertulis yang ditandatangani oleh Para Pihak.</li>
                    <li>Setiap pemberitahuan atau dokumen yang disyaratkan sehubungan dengan Perjanjian ini harus secara tertulis dan dikirimkan dan dianggap telah diberikan sebagaimana ditentukan sebagai berikut: (a) dikirimkan secara langsung atau melalui kurir pada tanggal penerimaan yang tercantum dalam tanda terima dari penerima atau konfirmasi pengiriman yang diterima pengirim dari kurir, (b) dikirimkan melalui faksimile, berdasarkan bukti pengiriman faksimile yang berhasil, dan (c) dikirimkan melalui e-mail, berdasarkan bukti pengiriman e-mail yang berhasil. Pemberitahuan atau dokumen dikirimkan kepada alamat yang tertera di atas atau alamat lainnya yang diberitahukan secara tertulis oleh tiap Pihak.</li>
                    <li>Setiap judul dari pasal-pasal atau bagian lain Perjanjian hanya untuk memudahkan pembacaan Perjanjian dan tidak mempengaruhi penafsiran Perjanjian ini.</li>
                </ol>

               <p>Demikian Perjanjian ini dibuat dalam 2 (dua) salinan asli, masing-masing sama bunyinya dan bermeterai cukup serta mempunyai kekuatan hukum yang sama serta mengikat Para Pihak setelah ditandatangani oleh Para Pihak.</p>

                <table class='signature-table'>
                    <tr>
                        <td>
                            <strong>PIHAK PERTAMA<br/><br/>
                            PT …………………………………</strong><br/><br/><br/><br/><br/><br/>
                            <strong><u>....................</u></strong><br/>
                            Direktur
                        </td>
                        <td>
                            <strong>PIHAK KEDUA<br/><br/>
                            PT ASURANSI TAKAFUL KELUARGA</strong><br/><br/><br/><br/><br/><br/>
                            <strong><u>Penny Hikmahwati</u></strong><br/>
                            Direktur Operasional
                        </td>
                    </tr>
                </table>   

                </body>
            </html>";


            return html
               .Replace("{{TanggalPengajuan}}", tanggalpengajuan)
               .Replace("{{NomorSurat}}", nomorsurat)
               .Replace("{{NamaPerusahaan}}", namaPerusahaan)
               .Replace("{{AlamatPerusahaan}}", alamat)
               .Replace("{{NamaRumahSakit}}", namaRS);
        }
    public static string Document_Perpanjangan_PKS(
    string tanggalpengajuan,
    string nomorsurat,
    string namaPerusahaan,
    string alamat,
    string namaRS,
    string tanggalsurat,string kota)
        {
            string tandaTanganPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "TTD2.jpg");
            string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "STEMPEL.jpg");

            // Pastikan file benar-benar ada
            if (!File.Exists(tandaTanganPath))
                throw new FileNotFoundException("File tanda tangan tidak ditemukan", tandaTanganPath);

            if (!File.Exists(logoPath))
                throw new FileNotFoundException("File logo tidak ditemukan", logoPath);

            // Untuk HTML Spire.Doc: langsung pakai path absolut (Windows pakai slash / biar aman)
            string tandaTanganSrc = tandaTanganPath.Replace("\\", "/");
            string logoSrc = logoPath.Replace("\\", "/");

            string html = @" 
            <!DOCTYPE html>
            <html>
            <head>
                <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'/>
                <title>Surat Perjanjian</title>
                <style>
                    body {
                        width: 21cm;
                        height: 29.7cm;
                        margin: 4cm;
                        font-family: Arial, sans-serif;
                        font-size: 10pt;
                        line-height: 1.5;
                    }

                    .footer {
                        text-align: left;
                    }

                    .signature {
                        margin-top: 30px;
                    }

                    p {
                        margin: 0 0 10px;
                        text-align: justify;
                    }

                 .info-surat {
                        padding-top: 100px; /* Ini akan benar-benar mendorong teks ke bawah */
                        margin-bottom: 30px;
                        text-align: left;
                   }
                    ol {
                        padding-left: 20px;
                        margin: 10px 0;
                    }

                    strong {
                        font-weight: bold;
                    }
                    ol.justified {
                    margin: 0;
                    padding-left: 25px;   /* atur jarak angka dengan teks */
                    }

                    ol.justified li {
                    text-align: justify;  /* isi list rata kiri-kanan */
                    margin-bottom: 10px;  /* spasi antar list */
                    }
                </style>
            </head>
            <body>

                <div style='height: 55px;'></div> <!-- Ini pengganti padding/margin -->

                <div>
                    Jakarta, {{TanggalSurat}}<br/>
                    No : {{NomorSurat}}<br/>
                    Lamp : Draf Perjanjian Kerjasama
                </div>
                <br/>
                <p>
                    Kepada Yth.<br/>
                    <strong>Direktur</strong><br/>
                    <strong>{{NamaPerusahaan}}</strong><br/>
                    {{AlamatPerusahaan}}<br/>
                    {{Kota}}<br/>
                </p>
                <br/>
                <p><strong>Perihal: Konfirmasi Perpanjangan Perjanjian Kerjasama</strong></p>
                <br/>
                <p>Assalamualaikum Wr Wb</p>
                
                <p>Teriring salam, semoga Bapak/Ibu senantiasa diberikan kesehatan dan kemudahan oleh Yang Maha Kuasa untuk menjalankan aktivitas dan pekerjaan setiap hari. Aamiin.</p>

                <p>Terima kasih kami ucapkan atas kepercayaan yang telah diberikan untuk dapat menjalin hubungan kerjasama dalam pelaksanaan Pelayanan Kesehatan Program Asuransi Kesehatan PT Asuransi Takaful Keluarga (ATK).</p>

                <p>Dalam rangka peningkatan pelayanan kepada Peserta Asuransi Kesehatan ATK (FULMEDICARE dan Hospital Plan), bersama ini kami informasikan sebagai berikut :</p> 
                    <ol class='justified'>
                      <li>
                        Terkait dengan perjanjian kerajasama pelayanan kesehatan antara ATK dengan <strong>{{NamaPerusahaan}}</strong> 
                        yang akan berakhir masa berlakunya, bersama ini Kami konfirmasikan kesediaan untuk perpanjangan 
                        Perjanjian Kerjasama tersebut;
                      </li>
                      <li>
                        Berikut kami lampirkan Draft PKS PT. Asuransi Takaful Keluarga, mohon dapat di review 
                        dan dipelajari. Apabila ada hal-hal yang perlu didiskusikan dapat menghubungi 
                        <b>Fitra Abdul Malik - 081314844965</b> dan email ke 
                        <b>abdul.malik@takaful.com</b> atau <b>provrelation-atk@takaful.com</b>;
                      </li>
                      <li>
                        Selama proses finalisasi perpanjangan Perjanjian ini, mohon untuk Peserta ATK yang menggunakan 
                        fasilitas kesehatan di <strong>{{NamaPerusahaan}}</strong> dapat dilayani seperti biasa, dan ATK menjamin 
                        pembayarannya sesuai dengan PKS sebelumnya.
                      </li>
                    </ol>

                <p>Demikian informasi yang dapat kami sampaikan, atas perhatiannya kami ucapkan terima kasih.</p>

                <p>Wassalamualaikum Wr Wb</p>

                <div class='signature'>
                    <p>
                        <strong>PT. ASURANSI TAKAFUL KELUARGA</strong>
                         <table style='border:0;'>
                            <tr>
                              <td style='padding-right:10px; vertical-align:bottom;'>
                                <img src='{{TTD}}' style='max-width:120px; max-height:80px;' />
                              </td>

                            </tr>
                          </table>
                        <u><strong>Fitra Abdul Malik, SKM, AAAK, AAAIJ</strong></u><br/>
                        Provider Relation & TPA
                    </p>
                </div>
                <br/>
                <p>
                    <strong>Tembusan:</strong><br/>
                    Arsip
                </p>

            </body>
            </html>
            ";


            return html
                .Replace("{{TanggalPengajuan}}", tanggalpengajuan)
                .Replace("{{NomorSurat}}", nomorsurat)
                .Replace("{{NamaPerusahaan}}", namaPerusahaan)
                .Replace("{{AlamatPerusahaan}}", alamat)
                .Replace("{{NamaRumahSakit}}", namaRS)
                .Replace("{{TanggalSurat}}",tanggalsurat)
                .Replace("{{Kota}}",kota);
        }
    public static string Document_Penawaran_PKS(
    string tanggalpengajuan,
    string nomorsurat,
    string namaPerusahaan,
    string alamat,
    string namaRS,
    string tanggalsurat, string kota)
        {
            string tandaTanganPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "TTD2.jpg");
            string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "STEMPEL.jpg");

            // Pastikan file benar-benar ada
            if (!File.Exists(tandaTanganPath))
                throw new FileNotFoundException("File tanda tangan tidak ditemukan", tandaTanganPath);

            if (!File.Exists(logoPath))
                throw new FileNotFoundException("File logo tidak ditemukan", logoPath);

            // Untuk HTML Spire.Doc: langsung pakai path absolut (Windows pakai slash / biar aman)
            string tandaTanganSrc = tandaTanganPath.Replace("\\", "/");
            string logoSrc = logoPath.Replace("\\", "/");

            string html = @" 
            <!DOCTYPE html>
            <html>
            <head>
                <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'/>
                <title>Surat Perjanjian</title>
                <style>
                    body {
                        width: 21cm;
                        height: 29.7cm;
                        margin: 4cm;
                        font-family: Arial, sans-serif;
                        font-size: 10pt;
                        line-height: 1;
                    }

                    .footer {
                        text-align: left;
                    }

                    .signature {
                        margin-top: 30px;
                    }

                    p {
                        margin: 0 0 10px;
                        text-align: justify;
                    }

                 .info-surat {
                        padding-top: 100px; /* Ini akan benar-benar mendorong teks ke bawah */
                        margin-bottom: 30px;
                        text-align: left;
                   }
                    ol {
                        padding-left: 20px;
                        margin: 10px 0;
                    }

                    strong {
                        font-weight: bold;
                    }
                    ol.justified {
                    margin: 0;
                    padding-left: 25px;   /* atur jarak angka dengan teks */
                    }

                    ol.justified li {
                    text-align: justify;  /* isi list rata kiri-kanan */
                    margin-bottom: 10px;  /* spasi antar list */
                    }
                </style>
            </head>
            <body>

                <div style='height: 55px;'></div> <!-- Ini pengganti padding/margin -->

                <div>
                    Jakarta, {{TanggalSurat}}<br/>
                    No : {{NomorSurat}}<br/>
                    Lamp : Draf Perjanjian Kerjasama
                </div>
                <br/>
                <p>
                    Kepada Yth.<br/>
                    <strong>Direktur</strong><br/>
                    <strong>{{NamaPerusahaan}}</strong><br/>
                    {{AlamatPerusahaan}}<br/>
                    {{Kota}}<br/>
                </p>
                <br/>
                <p><strong>Perihal: Permohonan Surat Penawaran</strong></p>
                <br/>
                <p>Assalamualaikum Wr Wb</p>
                <br/>
                <p>Teriring salam, semoga kita semua senantiasa diberikan kesehatan dan kemudahan oleh Yang Maha Kuasa untuk menjalankan aktivitas.</p>
                <br/>
                <p>Bersama ini kami mengajukan permohonan Kerjasama kepada {{NamaPerusahaan}} dalam hal Pelayanan Kesehatan bagi peserta asuransi kesehatan yang dikelola oleh PT Asuransi Takaful Keluarga.</p>
                <br/>
                <p>Berikut kami lampirkan Draft Perjanjian Kerjasama (PKS) dan PKS Sementara, mohon dapat direview dan dipelajari, apabila ada hal-hal yang perlu didiskusikan dapat menghubungi contact person kami <strong>Fitra Abdul Malik</strong> (021-7991234 ext. 1086; hp 081314844965).</p> 
                <br/>
                <p>Besar harapan kami kerjasama dalam pelayanan Kesehatan antara {{NamaPerusahaan}} dengan PT. Asuransi Takaful Keluarga dapat terlaksana dengan baik. </p>
                <br/>
                <p>Selama proses finalisasi perjanjian kerjasama, apabila ada peserta kami yang membutuhkan pelayanan kesehatan, mohon dapat dilayani dengan menggunakan sistem provider, dan kami bertanggung jawab untuk pembayaran tagihan sesuai dengan Penjaminan. </p>
                <br/>
                <p>Atas perhatian dan kerjasama serta kepercayaan yang telah diberikan kami ucapkan terima kasih.</p>
                <br/>
                <p>Wassalamualaikum Wr Wb</p>
                <div class='signature'>
                    <p>
                        <strong>PT. ASURANSI TAKAFUL KELUARGA</strong>
                         <table style='border:0;'>
                            <tr>
                              <td style='padding-right:10px; vertical-align:bottom;'>
                                <img src='{{TTD}}' style='max-width:120px; max-height:80px;' />
                              </td>
                            </tr>
                          </table>
                        <u><strong>Fitra Abdul Malik, SKM, AAAK, AAAIJ</strong></u><br/>
                        Provider Relation & TPA
                    </p>
                </div>

                <p>
                    <strong>Tembusan:</strong><br/>
                    Arsip
                </p>

            </body>
            </html>
            ";


            return html
                .Replace("{{TanggalPengajuan}}", tanggalpengajuan)
                .Replace("{{NomorSurat}}", nomorsurat)
                .Replace("{{NamaPerusahaan}}", namaPerusahaan)
                .Replace("{{AlamatPerusahaan}}", alamat)
                .Replace("{{NamaRumahSakit}}", namaRS)
                .Replace("{{TanggalSurat}}", tanggalsurat)
                .Replace("{{Kota}}", kota);
        }

    }
}