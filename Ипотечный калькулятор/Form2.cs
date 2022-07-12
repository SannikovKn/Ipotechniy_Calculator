using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Ипотечный_калькулятор
{
    public partial class Form2 : Form
    {

        public Form2(string Type) //конструктор формы 2
        {
            InitializeComponent(); //инициализация компонентов
            maskedTextBox1.Clear(); //очистка текстового поля с маской
            textBox2.Clear(); //очистка TextBox2
            textBox3.Clear(); //очистка TextBox2
            comboBox1.Text = Type; //передача в ComboBox1 типа платежа
            comboBox1.Enabled = false; //закрытие доступа к ComboBox1
            comboBox2.SelectedIndex = 0; //установка по умолчанию значения "рубль"
            radioButton2.Checked = false; //отена выбора radioButton2
            radioButton1.Checked = true; //установка выбора radioButton1
        }
       
        private void button2_Click(object sender, EventArgs e) //кнопка Закрыть
        {
            Close(); //закрыттие формы 2
        }

        private void button1_Click(object sender, EventArgs e) //кнопка Рассчитать
        {
            List<string> Schedule = new List<string>(); //создание листа для хранения графика выплат
            DateTime date = new DateTime(); //создание объекта DataTime
            string month = ""; //переменная для хранения названия месяца
            byte Proc_stavka; //переменная Процентная ставка
            uint CountMonth; //переменная для счета месяцев для выплат
            double Sum; //переменная для хранения Суммы кредита
            
            try //обработка исключений
            {
                Sum = Convert.ToUInt32(textBox2.Text); //конвертация суммы кредита в UInt
            }
            catch //если сумма кредита введена неверно
            {
                MessageBox.Show("Неверный ввод суммы кредита"); //показ сообщения для изменения сумму кредита
                textBox2.Clear(); //очистка TextBox2
                return; //прекращение выполнения метода
            }

            try
            {
                date = DateTime.Parse(maskedTextBox1.Text); //преобразования текста в дату
            }
            catch
            {
                MessageBox.Show("Неверный ввод даты"); //показ сообщения для изменения даты
                maskedTextBox1.Clear(); //очистка TextBox1
                return; //прекращение выполнения метода
            }

            try
            {
                if (radioButton1.Checked)  //подсчет количества месяцев для выплаты кредита
                    CountMonth = Convert.ToUInt32(textBox3.Text);
                else
                    CountMonth = Convert.ToUInt32(textBox3.Text) * 12;
            }
            catch
            {
                MessageBox.Show("Неверный ввод срока погашения кредита"); //показ сообщения для изменения срока
                textBox3.Clear(); //очистка TextBox3
                return; //прекращение выполнения метода
            }

            if (comboBox2.Text == "Рубль") //установка процентной ставки по валюте 
                Proc_stavka = 12;
            else if (comboBox2.Text == "Доллар")
                Proc_stavka = 5;
            else
                Proc_stavka = 4;
            
            if (comboBox1.Text == "Дифференцированный")  //если выбран Дифференцированный тип платежа
            {
                decimal S_osn, S_proc, S_ost, x; //переменные S-основной плтаеж, S-проценты на остаток
                                                 //S -остаток, x- выплата в текущем месяце
                S_ost = (decimal) Sum; //присвоение остатка для первого месяца
                S_osn = (decimal) Sum / CountMonth; //подсчет основного платежа для первого месяца

                while (CountMonth > 0) //цикл while, пока количество месяцев больше 0
                {
                    S_proc = (decimal) S_ost * Proc_stavka / 100 / 12; //подсчет процентов на остаток
                    x = S_osn + S_proc; //подсчет текущего платежа
                    S_ost -= S_osn; //подсчте остатка

                    switch (date.Month) //switch для установки текущего месяца
                    {
                        case 1: month = "Январь   "; break;
                        case 2: month = "Февраль"; break;
                        case 3: month = "Март       "; break;
                        case 4: month = "Апрель   "; break;
                        case 5: month = "Май        "; break;
                        case 6: month = "Июнь      "; break;
                        case 7: month = "Июль      "; break;
                        case 8: month = "Август    "; break;
                        case 9: month = "Сентябрь"; break;
                        case 10: month = "Октябрь  "; break;
                        case 11: month = "Ноябрь   "; break;
                        case 12: month = "Декабрь "; break;
                    }
                    Schedule.Add(month + " " + date.Year + "      " + Math.Round(x, 2) + "   " + Math.Round(S_ost, 2));
                    //заполнение списка графиком выплат
                    date = date.AddMonths(1); //изменение текущего месяца на следующий
                    CountMonth--; //умененьшение количества оставшихся месяцев на 1
                }
                DataOne.Value = Schedule; //заполнение списка для переноса в форму 1
                Close(); // закрытие формы
            }

            else
            {
                double MonthlyFee, Koef, i; //переменные Ежемесячный платеж, Коэффициент, Ежемесячная ставка

                i = (double) Proc_stavka / 1200; //подсчет ежемесячной ставки
                Koef =  i * Math.Pow(1 + i, CountMonth)/(Math.Pow(1 + i, CountMonth)-1); //подсчет коэффициента
                MonthlyFee = Koef * Sum; //подсчет ежемесячного платежа

                while (CountMonth > 0) //цикл while, пока количество месяцев больше 0
                {
                    switch (date.Month) //switch для установки текущего месяца
                    {
                        case 1: month = "Январь   "; break;
                        case 2: month = "Февраль"; break;
                        case 3: month = "Март       "; break;
                        case 4: month = "Апрель   "; break;
                        case 5: month = "Май        "; break;
                        case 6: month = "Июнь      "; break;
                        case 7: month = "Июль      "; break;
                        case 8: month = "Август    "; break;
                        case 9: month = "Сентябрь"; break;
                        case 10: month = "Октябрь  "; break;
                        case 11: month = "Ноябрь   "; break;
                        case 12: month = "Декабрь "; break;
                    }
                    Schedule.Add(month + " " + date.Year + "   " + Math.Round(MonthlyFee,2));
                    //заполнение списка графиком выплат
                    date = date.AddMonths(1); //изменение текущего месяца на следующий
                    CountMonth--; //умененьшение количества оставшихся месяцев на 1
                }
                DataOne.Value = Schedule; //заполнение списка для переноса в форму 1
                Close(); // закрытие формы
            }
        }
    }
}
