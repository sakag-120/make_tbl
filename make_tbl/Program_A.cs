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
            TextFieldParser parser = new TextFieldParser(@"C:\elephants\elephants_test\elephants_test\Master\data_teigi.csv", Encoding.GetEncoding("Shift_JIS"));
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


            while (!parser2.EndOfData)
            {
                string[] row2 = parser2.ReadFields();//1行分読み込み
                int cols = row2.Length;
                query_make = query_make + "CREATE TABLE  [" + row2[0] + "](品目番号 nchar(10), 品目名称 nvarchar(50), 備考　nvarchar(70),";
                for (int i = 1; i < cols; i++)
                {
                    if(row2[i] != "")
                    {
                        Console.WriteLine(query_make);
                        query_make = query_make + " \"" + row2[i] + "\" " + data_dic[row2[i]] + ",";
                    }


                }
                
                query_make = query_make + "ID tinyint, ユーザーネーム nvarchar(15), 更新日 datetime);\r\n";
                File.WriteAllText(@"C:\elephants\elephants_test\elephants_test\Master\query_maketbl.txt", query_make);

            }
        }
    }
}

            
                    
