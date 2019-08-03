#==================================================
#　　　　　　　　　mak_teigi.py
#仕様書エクセルファイルから仕様項目とデータ定義
#を抽出するプログラム
#　　　　坂口　裕宜　2019.7.28 最終更新
# 仕様項目の不一致バグ除け
#==================================================
import sys,os,openpyxl,csv

wb = openpyxl.load_workbook("C:\\elephants\\elephants_test\\仕様書\\仕様書分解.xlsm", data_only=True)

#仕様項目を読み出すシートは助けるゾウメインプログラムと同様にする。
shiyou_sheet      = wb.get_sheet_by_name('仕様指示書－作成－')

#読みだすシートは選択式仕様書を分解したもの
koumoku_sheet = wb.get_sheet_by_name('リスト－仕様指示書－')

koumoku = ""
dic_teigi = dict()
dic_koumoku = dict()

#辞書型変数に
for row in range(4, 100):
    koumoku = str(shiyou_sheet['D' + str(row)].value).replace('\n','')
    koumoku_num = koumoku[0:koumoku.find('．')]
    dic_koumoku[koumoku_num] = koumoku

#辞書型変数に、指定した行から、仕様項目と仕様内容の文字数を代入する。
#文字数は最大値のみを代入していく。

for row in range(4,503):
    if  str(koumoku_sheet['C' + str(row)].value).replace('\n','') != "None":
        koumoku = str(koumoku_sheet['C' + str(row)].value).replace('\n','')
        koumoku_num = koumoku[0:koumoku.find('．')]
        koumoku = dic_koumoku[koumoku_num]
        dic_teigi[koumoku] = "1"
    if int(dic_teigi[koumoku]) < len(str(koumoku_sheet['D' + str(row)].value).replace('\n','')):
        dic_teigi[koumoku] = str(len(str(koumoku_sheet['D' + str(row)].value).replace('\n',''))) 
#変数（仕様項目名）とそのデータ定義を指定した場所と名前でcsvファイルに書き込む
with open("C:\\elephants\\elephants_test\\elephants_test\\Master\\koumoku_teigi.csv", 'w') as f:
    writer = csv.writer(f)
    writer.writerow(['品目番号'       ,'nchar(10)'])
    writer.writerow(['品目名称'       ,'nchar(50)'])
    writer.writerow(['参照表名'       ,'nvarchar(50)'])
    writer.writerow(['備考'           ,'nvarchar(70)'])
    writer.writerow(['ID'             ,'tinyint'])
    writer.writerow(['key'             ,'int identity(1,1) NOT NULL PRIMARY KEY'])
    writer.writerow(['ユーザーネーム' ,'nvarchar(15)'])
    writer.writerow(['架装タイプ'     ,'nvarchar(15)'])
    writer.writerow(['架装分類'       ,'nvarchar(15)'])
    writer.writerow(['架装型式'       ,'nvarchar(15)'])
    writer.writerow(['ロードセル'     ,'nvarchar(5)'])
    writer.writerow(['反転装置'       ,'nvarchar(5)'])
    writer.writerow(['都庁仕様'       ,'nvarchar(5)'])
    writer.writerow(['メーカー'       ,'nvarchar(10)'])
    writer.writerow(['車型'           ,'nvarchar(30)'])
    writer.writerow(['クラス'         ,'nvarchar(10)'])
    writer.writerow(['タイヤ型式'     ,'nvarchar(25)'])
    writer.writerow(['WB'             ,'smallint'])
    writer.writerow(['個数'           ,'tinyint'])
    writer.writerow(['重量'           ,'float'])
    writer.writerow(['更新日'         ,'datetime'])

    for key, value in dic_teigi.items():
            writer.writerow([key,'nvarchar(' + value + ')'])    

  