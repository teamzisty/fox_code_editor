using fox_code_editor.packages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace fox_code_editor
{
    /// <summary>
    /// package_unzip1.xaml の相互作用ロジック
    /// </summary>
    public partial class package_unzip1 : System.Windows.Controls.UserControl
    {
        public package_unzip1()
        {
            InitializeComponent();
        }


        private async void unzip(object sender, RoutedEventArgs e)
        {
            set_prj.Visibility = Visibility.Visible;
        }

        private async void package_yes__btn(object sender, RoutedEventArgs e)
        {
            this.Cursor = System.Windows.Input.Cursors.Wait;

            // ZIP ファイルの URL
            string zipFileUrl = "https://github.com/umaidango/software-lineup/raw/refs/heads/main/fce/package/fce_pkg1.zip";

            // ダウンロードした ZIP ファイルを保存するローカルパス
            string localZipFilePath = Path.Combine(Path.GetTempPath(), "fce_pkg1.zip");

            // ZIP ファイルをダウンロード
            using (HttpClient client = new HttpClient())
            {
                byte[] zipFileBytes = await client.GetByteArrayAsync(zipFileUrl);
                await File.WriteAllBytesAsync(localZipFilePath, zipFileBytes);
            }

            // 解凍先のディレクトリ
            string extractDirectory = "project/" + proj_name.Text;

            // 解凍先のディレクトリが存在しない場合は作成
            if (!Directory.Exists(extractDirectory))
            {
                Directory.CreateDirectory(extractDirectory);
            }

            try
            {
                // ZIP ファイルを開いて解凍
                using (ZipArchive archive = ZipFile.OpenRead(localZipFilePath))
                {
                    archive.ExtractToDirectory(extractDirectory);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "エラー", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }

            
            // テキストファイルのパス
            string outputFilePath = "pkg_data.udapp";

            // ファイルに書き込む
            using (StreamWriter sw = new StreamWriter(outputFilePath))
            {
                sw.Write("<open> fce_prj1"); // WriteLineではなくWriteを使用
            }


            this.Cursor = System.Windows.Input.Cursors.Arrow;
            System.Windows.Forms.MessageBox.Show("プロジェクトを作成しました。\nPKGストアのウィンドウを閉じて\n[ファイル]のプロジェクトを開くを押してプロジェクトを選びます。");
                        

        }
    }
}
