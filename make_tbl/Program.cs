//==================================================
//　　　　　　　　　Program.cs
//設計マスタとデータ定義csvデータから、
//仕様項目／部品構成表を作成するクエリを記述するプログラム
//　　　　坂口　裕宜　2019.7.1 最終更新
//  Key項目の導入
//==============================================//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.FileIO;//parse
using System.Data.SqlClient;
using System.IO;

namespace make_tbl
{
    class Program
    {
        static void Main(string[] args)
        {
            //仕様書読み込みプログラムコードで作成したcsvファイル場所指定         
            TextFieldParser parser = new TextFieldParser(@"C:\elephants\elephants_test\elephants_test\Master\koumoku_teigi.csv", Encoding.GetEncoding("Shift_JIS"));
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");// ","区切り
            //csvファイルを辞書型変数に読み込み
            var data_dic = new Dictionary<string, string>();

            while (!parser.EndOfData)
            {
                string[] row = parser.ReadFields();//1行分読み込み
                data_dic.Add(row[0], row[1]);
                
            }

            TextFieldParser parser2 = new TextFieldParser(@"C:\elephants\elephants_test\elephants_test\Master\PB.csv", Encoding.GetEncoding("Shift_JIS"));
            parser2.TextFieldType = FieldType.Delimited;
            parser2.SetDelimiters(",,");// ","区切り

            string query_make = "";
            string query_del = "";


            while (!parser2.EndOfData)
            {
                string[] row2 = parser2.ReadFields();//1行分読み込み
                int cols = row2.Length;
                query_del += "DROP TABLE ["+ row2[0] + "];\r\n";
                query_make +=  "CREATE TABLE  [" + row2[0] + "](品目番号 " +
                    data_dic["品目番号"] + ", 品目名称 "+ data_dic["品目名称"] + ", 備考　" + data_dic["備考"] +
                    ", 個数　" + data_dic["個数"] + ", 重量　" + data_dic["重量"]+", ";
                for (int i = 1; i < cols; i++)
                {
                    if(row2[i] != "")
                    {
                        Console.WriteLine(query_make);
                        query_make += " \"" + row2[i] + "\" " + data_dic[row2[i]] + ",";
                    }
                }
                
                query_make += " ユーザーネーム " + data_dic["ユーザーネーム"] +
                    ", 更新日 " + data_dic["更新日"] + ", \"key\" " + data_dic["key"] + ");\r\n";
                File.WriteAllText(@"C:\elephants\elephants_test\elephants_test\Master\query_maketbl.txt", query_make);
                File.WriteAllText(@"C:\elephants\elephants_test\elephants_test\Master\query_deltbl.txt", query_del);

            }
        }
    }
}

            
                    
