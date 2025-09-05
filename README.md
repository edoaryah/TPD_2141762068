# Aplikasi Arsip Surat - Proyek Uji Kompetensi BNSP

Merupakan proyek aplikasi web yang dibangun untuk memenuhi skenario tugas praktik demonstrasi pada Sertifikasi Kompetensi BNSP skema "Pemrograman Software Komputer". Proyek ini dibangun menggunakan framework **ASP.NET Core MVC (.NET 8)** dengan database **PostgreSQL**. Untuk antarmuka pengguna, proyek ini menggunakan **Bootstrap 5**.

---

## Tujuan

Tujuan dari proyek ini adalah untuk membuat sebuah aplikasi berbasis web yang dapat digunakan oleh perangkat kelurahan (studi kasus: Desa Karangduren) untuk mengarsipkan surat-surat resmi. Aplikasi ini diharapkan dapat petugas untuk mengunggah, mencari, melihat, mengunduh, dan mengelola surat-surat yang telah diarsipkan dalam format PDF.

---

## Fitur Aplikasi

- **Manajemen Arsip Surat**:

  - Mengunggah surat baru dalam format PDF.
  - Menampilkan daftar surat.
  - Mencari surat berdasarkan Judul atau Nomor Surat.
  - Melihat detail, mengunduh, dan mengubah data arsip surat.
  - Menghapus arsip surat.

- **Manajemen Kategori Surat**:

  - Fungsi Tambah, Lihat, Ubah, dan Hapus (CRUD) untuk kategori.
  - Fitur **Soft Delete**, di mana kategori yang dihapus tidak benar-benar hilang untuk menjaga histori data.
  - Validasi untuk mencegah adanya nama kategori yang duplikat.

- **Antarmuka Pengguna**:
  - Notifikasi menggunakan **Toastify** untuk pesan sukses atau gagal.

---

## Langkah-langkah Instalasi

1.  **Clone Proyek**
    Salin proyek ini ke komputer lokal Anda.

    ```bash
    git clone [URL_REPOSITORY_ANDA]
    ```

2.  **Buat Database PostgreSQL**
    Jalankan perintah SQL berikut untuk membuat database baru.

    ```sql
    CREATE DATABASE "TPD_2141762068";
    ```

3.  **Konfigurasi Koneksi Database**
    Buka file `appsettings.json` dan sesuaikan `ConnectionString` dengan username dan password PostgreSQL Anda.

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Host=localhost;Database=TPD_2141762068;Username=postgres;Password=password_anda"
    },
    ```

4.  **Install Dependensi Frontend**
    Buka terminal di dalam folder proyek dan jalankan:

    ```bash
    npm install
    ```

    _(Atau `yarn` jika Anda menggunakannya)_

5.  **Terapkan Migrasi Database**
    Perintah ini akan membuat semua tabel yang dibutuhkan secara otomatis.

    ```bash
    dotnet ef database update
    ```

6.  **Jalankan Aplikasi**
    ```bash
    dotnet run
    ```
    Aplikasi akan berjalan di `https://localhost:5001` atau `http://localhost:5000`.

---

## Screenshot

_(Tempat untuk menempelkan screenshot aplikasi Anda)_

---

## Tentang Pengembang

Aplikasi ini disusun dan dikembangkan oleh:

| Nama              | Program Studi              | NIM        |
| ----------------- | -------------------------- | ---------- |
| Edo Arya Hermawan | D4 Sistem Informasi Bisnis | 2141762068 |
