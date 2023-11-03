using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;

namespace HelloApp
{
    public partial class RadixSort : Form
    {
        public int[] array { get; set; }

        public RadixSort()
        {
            InitializeComponent();
        }

        private async void RadixSort_Load(object sender, EventArgs e)
        {
            SplitContainer splitContainer = new SplitContainer();
            splitContainer.Dock = DockStyle.Fill;
            this.Controls.Add(splitContainer);

            string filePath = "Определения\\RadixSort.txt";

            RichTextBox richTextBox = new RichTextBox();
            richTextBox.Multiline = true;
            richTextBox.WordWrap = true;
            richTextBox.ReadOnly = true;
            richTextBox.BorderStyle = BorderStyle.Fixed3D;
            richTextBox.AutoWordSelection = true;
            richTextBox.Dock = DockStyle.Fill;
            richTextBox.Location = new Point(5, 5);
            richTextBox.Font = new Font("Times New Roman", 12);
            richTextBox.ForeColor = Color.White;
            richTextBox.BackColor = Color.FromArgb(64, 64, 64);
            richTextBox.Text = File.ReadAllText(filePath);
            ChangeWordTitleSize("Радиксная сортировка", 18, richTextBox);
            ChangeWordTitleSize("Шаги процесса сортировки:", 18, richTextBox);

            splitContainer.Panel1.Controls.Add(richTextBox);

            FlowLayoutPanel flowLayoutPanelMain = new FlowLayoutPanel();
            flowLayoutPanelMain.AutoSize = true;
            flowLayoutPanelMain.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanelMain.WrapContents = true;
            flowLayoutPanelMain.Location = new Point(12, 12);
            splitContainer.Panel2.Controls.Add(flowLayoutPanelMain);

            Label label1 = new Label();
            label1.Text = "Массив, который хотим сортировать:";
            label1.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            label1.Size = new Size(428, 29);
            label1.ForeColor = Color.White;

            Label label2 = new Label();
            label2.Text = "Процесс сортировки:";
            label2.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            label2.Size = new Size(428, 29);
            label2.ForeColor = Color.White;

            Label label3 = new Label();
            label3.Text = "Процесс подсчитывания цифр в каждом разряде (countArray)";
            label3.Font = new Font("Segoe UI", 11);
            label3.Size = new Size(450, 29);
            label3.ForeColor = Color.White;

            Label label4 = new Label();
            label4.Text = "Процесс сохранения (outputArray)";
            label4.Font = new Font("Segoe UI", 11);
            label4.Size = new Size(450, 29);
            label4.ForeColor = Color.White;

            Button[] buttonArrayInput = new Button[array.Length];
            Button[] buttonArrayInputResult = new Button[array.Length];
            Button[] buttonArrayOutput = new Button[array.Length];
            Button[] buttonArrayCount = new Button[10];
            Button[] buttonArrayCountUniqNum = new Button[10];

            int[] arrayCount = new int[10];
            int[] arrayOutput = new int[array.Length];

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            TableLayoutPanel tableLayoutPanelResult = new TableLayoutPanel();
            TableLayoutPanel tableLayoutPanelOutput = new TableLayoutPanel();
            TableLayoutPanel tableLayoutPanelCount = new TableLayoutPanel();

            flowLayoutPanelMain.Controls.Add(label1);
            TableLayoutPanelMake(tableLayoutPanel, flowLayoutPanelMain, buttonArrayInput, array);

            int mx = array[0];
            int index1 = 0;
            for (int i = 1; i < array.Length; i++)
                if (array[i] > mx) 
                {
                    mx = array[i];
                    index1 = i;
                }
            buttonArrayInput[index1].BackColor = Color.Red;

            flowLayoutPanelMain.Controls.Add(label2);
            TableLayoutPanelMake(tableLayoutPanelResult, flowLayoutPanelMain, buttonArrayInputResult, array);

            flowLayoutPanelMain.Controls.Add(label3);
            tableLayoutPanelCount.AutoSize = true;
            tableLayoutPanelCount.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanelCount.ColumnCount = 10;
            tableLayoutPanelCount.RowCount = 2;
            flowLayoutPanelMain.Controls.Add(tableLayoutPanelCount);
            for (int i = 0; i < buttonArrayCountUniqNum.Length; i++)
            {
                Button button = new Button();
                button.Height = 30;
                button.Width = 50;
                button.BackColor = SystemColors.ControlDark;
                button.Text = i.ToString();
                buttonArrayCountUniqNum[i] = button;
                tableLayoutPanelCount.Controls.Add(button);
                tableLayoutPanelCount.SetCellPosition(button, new TableLayoutPanelCellPosition(i, 0));
            }
            for (int i = 0; i < buttonArrayCount.Length; i++)
            {
                arrayCount[i] = 0;
                Button button = new Button();
                button.Height = 50;
                button.Width = 50;
                button.BackColor = SystemColors.ControlDark;
                button.Text = arrayCount[i].ToString();
                buttonArrayCount[i] = button;
                tableLayoutPanelCount.Controls.Add(button);
                tableLayoutPanelCount.SetCellPosition(button, new TableLayoutPanelCellPosition(i, 1));
            }

            flowLayoutPanelMain.Controls.Add(label4);
            TableLayoutPanelMake(tableLayoutPanelOutput, flowLayoutPanelMain, buttonArrayOutput, arrayOutput);








            // Нахождения максимального значения в массиве
            int m = getMax(array, array.Length);

            // Выполнение сортировки подсчетом для каждого заряда чисел.
            // exp - это какой разряд передется (единиы(1), десятки(10) или сотни(100))
            for (int exp = 1; m / exp > 0; exp *= 10)
            {
                // Идет подсчет разрядов чисел массива array в массиве arrayCount.
                // Индексы массива count являются уникальными значениями.
                for (int i = 0; i < array.Length; i++) 
                {
                    arrayCount[(array[i] / exp) % 10]++;
                    await Task.Delay(500);
                    buttonArrayCount[(array[i] / exp) % 10].BackColor = Color.Red;
                    buttonArrayInputResult[i].BackColor = Color.Red;
                    buttonArrayCount[(array[i] / exp) % 10].Text = (arrayCount[(array[i] / exp) % 10]).ToString();
                    await Task.Delay(500);
                    buttonArrayCount[(array[i] / exp) % 10].BackColor = SystemColors.ControlDark;
                    buttonArrayInputResult[i].BackColor = SystemColors.ControlDark;
                }

                await Task.Delay(2000);

                for (int i=0; i < buttonArrayCount.Length; i++)
                {
                    buttonArrayCount[i].BackColor = Color.Green;
                }

                await Task.Delay(2000);

                // Измените count[i] так, чтобы count[i] теперь содержал фактическое положение этой цифры в выходных данных[]
                for (int i = 1; i < 10; i++)
                {
                    arrayCount[i] += arrayCount[i - 1];
                    buttonArrayCount[i].Text = arrayCount[i].ToString();
                }

                await Task.Delay(2000);

                // Build the output array
                for (int i = array.Length - 1; i >= 0; i--)
                {
                    arrayOutput[arrayCount[(array[i] / exp) % 10] - 1] = array[i];
                    buttonArrayOutput[arrayCount[(array[i] / exp) % 10] - 1].BackColor = Color.Violet;
                    buttonArrayInputResult[i].BackColor = Color.Violet;
                    buttonArrayCount[(array[i] / exp) % 10].BackColor = Color.Violet;
                    buttonArrayCountUniqNum[(array[i] / exp) % 10].BackColor = Color.Violet;
                    await Task.Delay(1000);
                    buttonArrayOutput[arrayCount[(array[i] / exp) % 10] - 1].Text = array[i].ToString();
                    buttonArrayOutput[arrayCount[(array[i] / exp) % 10] - 1].Height = 50 + 10 * array[i];
                    await Task.Delay(1000);
                    buttonArrayOutput[arrayCount[(array[i] / exp) % 10] - 1].BackColor = SystemColors.ControlDark;
                    buttonArrayInputResult[i].BackColor = SystemColors.ControlDark;
                    buttonArrayCount[(array[i] / exp) % 10].BackColor = Color.Green;
                    buttonArrayCountUniqNum[(array[i] / exp) % 10].BackColor = SystemColors.ControlDark;
                    arrayCount[(array[i] / exp) % 10]--;
                    buttonArrayCount[(array[i] / exp) % 10].Text = (arrayCount[(array[i] / exp) % 10]).ToString();
                }

                // Copy the output array to arr[], so that arr[] now
                // contains sorted numbers according to current
                // digit
                for (int i = 0; i < array.Length; i++)
                {
                    Button temp = new Button();
                    array[i] = arrayOutput[i];
                    buttonArrayInputResult[i].Text = buttonArrayOutput[i].Text;
                    buttonArrayInputResult[i].Height = 50 + 10 * array[i];
                }
                await Task.Delay(3000);

                for (int i=0; i< buttonArrayCount.Length; i++) 
                {
                    arrayCount[i] = 0;
                    buttonArrayCount[i].Text = arrayCount[i].ToString();
                    buttonArrayCount[i].BackColor = SystemColors.ControlDark;
                }

                for (int i = 0; i < buttonArrayOutput.Length; i++)
                {
                    buttonArrayOutput[i].Text = "0";
                    buttonArrayOutput[i].Height = 50;
                }
            }
        }



        static void TableLayoutPanelMake(TableLayoutPanel tableLayoutPanel, FlowLayoutPanel flowLayoutPanelMain, Button[] arrayButton, int[] array)
        {
            tableLayoutPanel.AutoSize = true;
            tableLayoutPanel.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel.ColumnCount = array.Length;
            tableLayoutPanel.RowCount = 0;
            flowLayoutPanelMain.Controls.Add(tableLayoutPanel);

            for (int i = 0; i < arrayButton.Length; i++)
            {
                Button button = new Button();
                button.Height = 50 + 10 * array[i];
                button.Width = 50;
                button.BackColor = SystemColors.ControlDark;
                button.Text = array[i].ToString();
                arrayButton[i] = button;
                tableLayoutPanel.Controls.Add(button);
                tableLayoutPanel.SetCellPosition(button, new TableLayoutPanelCellPosition(i, 0));
            }
        }


        private static void ChangeWordTitleSize(string word, int newSize, RichTextBox richTextBox1)
        {
            int start = richTextBox1.Text.IndexOf(word);
            if (start >= 0)
            {
                richTextBox1.Select(start, word.Length);
                richTextBox1.SelectionFont = new System.Drawing.Font(richTextBox1.Font.FontFamily, newSize, FontStyle.Bold);
            }
        }




        // Функция для получения максимального значения в массива.
        public static int getMax(int[] arr, int n)
        {
            int mx = arr[0];
            for (int i = 1; i < n; i++)
                if (arr[i] > mx)
                    mx = arr[i];
            return mx;
        }

        // A function to do counting sort of arr[] according to
        // the digit represented by exp.
        public static void countSort(int[] arr, int n, int exp)
        {
            int[] output = new int[n]; // output array
            int i;
            int[] count = new int[10];

            // Инициализируется массив count из 0
            for (i = 0; i < 10; i++)
                count[i] = 0;

            // Идет подсчет разрядов чисел массива arr в массиве count.
            // Индексы массива count являются уникальными значениями.
            for (i = 0; i < n; i++)
                count[(arr[i] / exp) % 10]++;

            // Измените count[i] так, чтобы count[i] теперь содержал фактическое положение этой цифры в выходных данных[]
            for (i = 1; i < 10; i++)
                count[i] += count[i - 1];

            // Build the output array
            for (i = n - 1; i >= 0; i--)
            {
                output[count[(arr[i] / exp) % 10] - 1] = arr[i];
                count[(arr[i] / exp) % 10]--;
            }

            // Copy the output array to arr[], so that arr[] now
            // contains sorted numbers according to current
            // digit
            for (i = 0; i < n; i++)
                arr[i] = output[i];
        }

        // Основная функуция для сортировки массива arr[i] с использованием RadixSort.
        public static void radixsort(int[] arr, int n)
        {
            // Нахождения максимального значения в массиве
            int m = getMax(arr, n);

            // Выполнение сортировки подсчетом для каждого заряда чисел.
            // exp - это какой разряд передется (единиы(1), десятки(10) или сотни(100))
            for (int exp = 1; m / exp > 0; exp *= 10)
                countSort(arr, n, exp);
        }
    }
}
