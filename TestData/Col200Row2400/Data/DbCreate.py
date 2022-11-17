import sqlite3
import shutil

# コピー
base_file_name = "Base_Col200Row2400.db"

for num in range(1, 23761):
  cur_db_file_name = str(num) + "_Col200Row2400.db"
  shutil.copyfile(base_file_name, cur_db_file_name)

  conn = sqlite3.connect(cur_db_file_name)
  cur = conn.cursor()
  cur.execute('UPDATE Col200Row2400 SET DBID=REPLACE(DBID,"1","' + str(num) + '")')
  conn.commit()
  conn.close()

