using System;
using System.IO;
using System.Windows.Forms;

namespace Ипотечный_калькулятор
{
    public partial class Form1 : Form
    {
        public Form1() // конструктор формы 1
        {
            InitializeComponent(); //инициализация компонентов
            listBox1.Enabled = false; //запред доступа к ListBox1
            label2.Visible = false; //скрытие надписи Выплата
            label6.Visible = false; //скрытие надписи Остаток 
            button1.Focus();
        }

        private void button3_Click(object sender, EventArgs e) //кнопка Выход
        {
            Close(); //закрытие формы
        }

        private void button1_Click(object sender, EventArgs e) //кнопка Дифференцированный тип платежа
        {
            Form SecForm = new Form2("Дифференцированный"); //создание объекта формы 2
            SecForm.ShowDialog(); //открытие второй формы в диалоговом режиме
            
            if (DataOne.Value != null) //DataOne для передачи данных между формами
            {
                listBox1.Items.Clear(); //очистка ListBox1
                foreach (var str in DataOne.Value) 
                    listBox1.Items.Add(str); //вывод графика платежей в ListBox1
                label2.Visible = true; //надпись Выплата
                label6.Visible = true; //надпись Остаток
                listBox1.Enabled = true;
            }
        }

        private void button2_Click_1(object sender, EventArgs e) //кнопка Очистить
        {
            listBox1.Items.Clear(); //очистка ListBox1
        }

        private void button4_Click(object sender, EventArgs e) //кнопка Аннуитетный тип платежа
        {
            Form SecForm = new Form2("Аннуитетный"); //создание объекта формы 2
            SecForm.ShowDialog(); //открытие второй формы в диалоговом режиме

            if (DataOne.Value != null) //DataOne для передачи данных между формами
            {
                listBox1.Items.Clear(); //очистка ListBox1
                foreach (var str in DataOne.Value)
                    listBox1.Items.Add(str); //вывод графика платежей в ListBox1
                label2.Visible = true; //надпись Выплата
                label6.Visible = false; //надпись Остаток
                listBox1.Enabled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e) //кнопка Экспорт
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog(); //создание объекта диалога
            fbd.ShowDialog(); //открытие диалога

            string path = fbd.SelectedPath; //сохрание пути для сохранения файла
            
            StreamWriter Sw = new StreamWriter(path + "\\График выплат.txt"); //создание объекта StreamWriter
            foreach (var a in DataOne.Value)
                Sw.WriteLine(a); //запись данных в файл
            Sw.Close(); //закрытие закрытие StreamWriter
        }
    }
}
