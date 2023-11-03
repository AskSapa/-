using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HelloApp
{
    public partial class InsertionSort : Form
    {
        public int[] array { get; set; }
        public InsertionSort()
        {
            InitializeComponent();
        }

        private async void InsertionSort_Load(object sender, EventArgs e)
        {
            SplitContainer splitContainer = new SplitContainer();
            splitContainer.Dock = DockStyle.Fill;
            this.Controls.Add(splitContainer);

            string filePath = "Определения\\InsertionSort.txt";

            RichTextBox richTextBox = new RichTextBox();
            richTextBox.Multiline = true;
            richTextBox.WordWrap = true;
            richTextBox.ReadOnly = true;
            richTextBox.BorderStyle = BorderStyle.Fixed3D;
            richTextBox.AutoWordSelection = true;
            richTextBox.Dock = DockStyle.Fill;
            richTextBox.Location = new Point(5, 5);
            richTextBox.Font = new Font("Times New Roman", 13);
            richTextBox.ForeColor = Color.White;
            richTextBox.BackColor = Color.FromArgb(64, 64, 64);
            richTextBox.Text = File.ReadAllText(filePath);
            ChangeWordTitleSize("Сортировка вставками", 18, richTextBox);
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

            Button[] buttons = new Button[array.Length];

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            TableLayoutPanel tableLayoutPanelResult = new TableLayoutPanel();

            flowLayoutPanelMain.Controls.Add(label1);
            TableLayoutPanelMake(tableLayoutPanel, flowLayoutPanelMain, buttons, array);
            flowLayoutPanelMain.Controls.Add(label2);
            TableLayoutPanelMake(tableLayoutPanelResult, flowLayoutPanelMain, buttons, array);



            
            int n = array.Length;
            for (int i = 1; i < n; ++i)
            {
                await Task.Delay(1000);
                buttons[i].BackColor = Color.Green;
                int key = array[i];
                Button keyButton = buttons[i];
                int j = i - 1;

                while (j >= 0 && array[j] > key)
                {
                    await Task.Delay(1000);
                    buttons[j].BackColor = Color.Red;
                    array[j + 1] = array[j];
                    tableLayoutPanelResult.SetCellPosition(buttons[j], new TableLayoutPanelCellPosition(j + 1, 0));
                    buttons[j+1] = buttons[j];
                    j--;
                    await Task.Delay(1000);
                }
                array[j + 1] = key;
                tableLayoutPanelResult.SetCellPosition(keyButton, new TableLayoutPanelCellPosition(j + 1, 0));
                buttons[j + 1] = keyButton;

                await Task.Delay(1000);

                for (int b=0; b<buttons.Length; b++)
                {
                    buttons[b].BackColor = SystemColors.ControlDark;
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


        void sort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 1; i < n; ++i)
            {
                int key = arr[i];
                int j = i - 1;

                // Move elements of arr[0..i-1],
                // that are greater than key,
                // to one position ahead of
                // their current position
                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = key;
            }
        }
    }
}
