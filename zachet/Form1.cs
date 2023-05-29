using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace zachet
{
    public partial class Form1 :Form
    {
        List <ZadA> zada=new List<ZadA>();
        List<ZadB> zadb = new List<ZadB>( );
        public Form1 ()
        {
            InitializeComponent( );
            
            
        }

        private void Form1_Load (object sender, EventArgs e)
        {

        }
        //чтение из файла строки
        public string FilRread(string file)
        {
                StreamReader sr = File.OpenText("text.txt");
                while ( !sr.EndOfStream )
                {

                     file = sr.ReadLine( );
                    ZadA a = new ZadA( );
                    a.file = file;
                    zada.Add(a);

                }
                sr.Close();
            return file;
          
        }
        //проверка строки
        static bool Chek( string stroca)
        {
            bool chec = false;
            //преоброзование символов в строку
            stroca = stroca.ToLower( );
                for(int i=0;i<stroca.Length;i++ )
                {
                char c =stroca [ i ];
                if ( c >= 'а' && c <= 'я' ) chec = true;
                
                
                }
     return chec;
           
        }
        //ишем слова которые совпадают в файле
        static int poisc (string stroca, string file)
        {

            List<string> list = file.Split(' ').ToList( );
            //берём значение из листа
            // IEnumerable возврашет сылку на другой интерфейс для перебора объектов в foreach
            IEnumerable<string> result =from s in list
                    //ищем совпадение слова с словом из файла
                where s.Contains(stroca)
                select s;
            int count = 0;
            foreach ( string s in result )
            {
                count++;
            }
            return count;
        }
      //добавление в лист  
        private void button4_Click (object sender, EventArgs e)
        {
            string file = "";
            file = FilRread(file);
            listBox1.Items.Add(file);

        }

        private void button1_Click (object sender, EventArgs e)
        {
            if ( textBox1.TextLength != 0 )
            {
                string stroca = textBox1.Text;
                
                if ( Chek(stroca) )
                {                    
                    string line = "";                   
                    line  =FilRread (line);
                    if ( line.Length != 0 )
                    {
                        int count = poisc(stroca.ToLower( ), line.ToLower( ));
                        if ( count > 0 ) MessageBox.Show($"Найдены {count} вхождения(ий) поискового запроса {stroca}");
                        else MessageBox.Show($"Не найдены входления(ий) поискового запроса {stroca}");
                    }
                    else MessageBox.Show("Файл пуст","ОШИБКА");
                    
                }
                else MessageBox.Show("некоректный ввод","ОШИБКА");
               
            }
            else MessageBox.Show("строка не заполненна","ОШИБКА");
        }
         //чтение массива из файла 
        public string MasRead (string file)
        {
                StreamReader sr = File.OpenText("mas.txt");
                while ( !sr.EndOfStream )
                {

                     file = sr.ReadLine( );
                    string [ ] s = file.Split(new char [ ] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    ZadB b = new ZadB( );
                    //for(int i=0;i<s.Length;i++ )
                    //{
                    //   b.mas[i] = int.Parse(s[i]);
                    //    i++;
                    //}
                  
                    //zadb.Add(b);

                }
                sr.Close( );
               return file;
            
        }

        private void button5_Click (object sender, EventArgs e)
        {
            listBox2.Items.Clear( );
            string file = "";
            file = MasRead(file);
            listBox2.Items.Add(file);
        }
       double [ ] mas = { 5.1, 1.3, 9.2, 2.3, 5.1, 3 };
        private void button2_Click (object sender, EventArgs e)
        {
         
            listBox2.Items.Clear( );
            var result = mas.GroupBy(x => x)//групируем числа из массива без повторов
                .Select(x => new { Number = x.Key, Chast = x.Count( ) }) //преобразуем в обьект с двумя свойствами:
                                                                             //Number значение, Frequency частота
                .OrderBy(x => x.Number); // сортируем обьект по возростанию
            listBox1.Items.Add("Числа - Частота");
            foreach ( var x in result )
            {
                string item = $"{x.Number} - {x.Chast}";
                listBox2.Items.Add(item);
            }
        }

        private void button3_Click (object sender, EventArgs e)
        {
            listBox2.Items.Clear( );
            var result = mas.GroupBy(x => x)//групируем числа из массива без повторов
                .Select(x => new { Number = x.Key, Chast = x.Count( ) })//преобразуем в обьект с двумя свойствами:
                                                                            //Number значение, Frequency частота
                .OrderBy(x => x.Number); // сортируем обьект по возростанию
            listBox1.Items.Add("Число * частоту - Частота");
            foreach ( var x in result )
            {
                string item = $"{x.Number * x.Chast} - {x.Chast}";
                listBox2.Items.Add(item);
            }
        }
    }
}
