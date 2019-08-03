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
            //�d�l���ǂݍ��݃v���O�����R�[�h�ō쐬����csv�t�@�C���ꏊ�w��         
            TextFieldParser parser = new TextFieldParser(@"C:\elephants\elephants_test\elephants_test\Master\data_teigi.csv", Encoding.GetEncoding("Shift_JIS"));
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");// ","��؂�
            //csv�t�@�C���������^�ϐ��ɓǂݍ���
            var data_dic = new Dictionary<string, string>();

            while (!parser.EndOfData)
            {
                string[] row = parser.ReadFields();//1�s���ǂݍ���
                data_dic.Add(row[0], row[1]);
                
            }

            TextFieldParser parser2 = new TextFieldParser(@"C:\elephants\elephants_test\elephants_test\Master\PB.csv", Encoding.GetEncoding("Shift_JIS"));
            parser2.TextFieldType = FieldType.Delimited;
            parser2.SetDelimiters(",,");// ","��؂�

            string query_make = "";


            while (!parser2.EndOfData)
            {
                string[] row2 = parser2.ReadFields();//1�s���ǂݍ���
                int cols = row2.Length;
                query_make = query_make + "CREATE TABLE  [" + row2[0] + "](�i�ڔԍ� nchar(10), �i�ږ��� nvarchar(50), ���l�@nvarchar(70),";
                for (int i = 1; i < cols; i++)
                {
                    if(row2[i] != "")
                    {
                        Console.WriteLine(query_make);
                        query_make = query_make + " \"" + row2[i] + "\" " + data_dic[row2[i]] + ",";
                    }


                }
                
                query_make = query_make + "ID tinyint, ���[�U�[�l�[�� nvarchar(15), �X�V�� datetime);\r\n";
                File.WriteAllText(@"C:\elephants\elephants_test\elephants_test\Master\query_maketbl.txt", query_make);

            }
        }
    }
}

            
                    