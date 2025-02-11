using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace fox_code_editor.packages
{
    /// <summary>
    /// new_proj.xaml の相互作用ロジック
    /// </summary>
    public partial class new_proj : UserControl
    {
        public new_proj()
        {
            InitializeComponent();
        }

        private async void package_yes__btn(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            // ZIP ファイルの URL
            string zipFileUrl = "https://github.com/usoftware-gr/software-lineup/raw/refs/heads/main/fce/package/fce_pkg1.zip";

            // ダウンロードした ZIP ファイルを保存するローカルパス
            string localZipFilePath = Path.Combine(Path.GetTempPath(), "fce_pkg1.zip");

            // ZIP ファイルをダウンロード
            using (HttpClient client = new HttpClient())
            {
                byte[] zipFileBytes = await client.GetByteArrayAsync(zipFileUrl);
                await File.WriteAllBytesAsync(localZipFilePath, zipFileBytes);
            }

            // 解凍先のディレクトリ
            string extractDirectory = proj_name.Text;

            // 解凍先のディレクトリが存在しない場合は作成
            if (!Directory.Exists(extractDirectory))
            {
                Directory.CreateDirectory(extractDirectory);
            }

            // ZIP ファイルを開いて解凍
            using (ZipArchive archive = ZipFile.OpenRead(localZipFilePath))
            {
                archive.ExtractToDirectory(extractDirectory);
            }

            string fullPath = Path.Combine(Path.GetFullPath(extractDirectory), "fce_prj1");

            // テキストファイルのパス
            string outputFilePath = "pkg_data.udapp";

            // ファイルに書き込む
            using (StreamWriter sw = new StreamWriter(outputFilePath))
            {
                sw.Write("<this_pack_path> " + fullPath + "\n" + "<files> [index.html] [about.html] [main.js] [main.css] "); // WriteLineではなくWriteを使用
            }

            

        }

    }
}
