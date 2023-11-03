using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HelloApp
{
    public partial class CombSort : Form
    {
        public int[] array { get; set; }
        public CombSort()
        {
            InitializeComponent();
        }

        private async void CombSort_Load(object sender, EventArgs e)
        {
            SplitContainer splitContainer = new SplitContainer();
            splitContainer.Dock = DockStyle.Fill;
            this.Controls.Add(splitContainer);

            string filePath = "Определения\\CombSort.txt";

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
            ChangeWordTitleSize("Сортировка расчёской", 18, richTextBox);
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



            var arrayLength = array.Length;
            var currentStep = arrayLength - 1;

            while (currentStep > 1)
            {
                for (int i = 0; i + currentStep < array.Length; i++)
                {
                    await Task.Delay(500);
                    buttons[i].BackColor = Color.Red;
                    buttons[i + currentStep].BackColor = Color.Red;
                    await Task.Delay(500);
                    if (array[i] > array[i + currentStep])
                    {
                        Button tempButton = buttons[i + currentStep];
                        tableLayoutPanelResult.SetCellPosition(buttons[i], new TableLayoutPanelCellPosition(i + currentStep, 0));
                        tableLayoutPanelResult.SetCellPosition(tempButton, new TableLayoutPanelCellPosition(i, 0));
                        buttons[i + currentStep] = buttons[i];
                        buttons[i] = tempButton;
                        await Task.Delay(1000);
                        buttons[i].BackColor = SystemColors.ControlDark;
                        buttons[i + currentStep].BackColor = SystemColors.ControlDark;
                        Swap(ref array[i], ref array[i + currentStep]);
                    }
                    else
                    {
                        buttons[i].BackColor = SystemColors.ControlDark;
                        buttons[i + currentStep].BackColor = SystemColors.ControlDark;
                    }


                }

                currentStep = GetNextStep(currentStep);
            }

            //сортировка пузырьком
            for (var i = 1; i < arrayLength; i++)
            {
                var swapFlag = false;
                for (var j = 0; j < arrayLength - i; j++)
                {
                    await Task.Delay(500);
                    buttons[j].BackColor = Color.Red;
                    buttons[j + 1].BackColor = Color.Red;
                    await Task.Delay(500);
                    if (array[j] > array[j + 1])
                    {
                        Button tempButton = buttons[j + 1];
                        tableLayoutPanelResult.SetCellPosition(buttons[j], new TableLayoutPanelCellPosition(j + 1, 0));
                        tableLayoutPanelResult.SetCellPosition(tempButton, new TableLayoutPanelCellPosition(j, 0));
                        buttons[j + 1] = buttons[j];
                        buttons[j] = tempButton;
                        await Task.Delay(1000);
                        buttons[j].BackColor = SystemColors.ControlDark;
                        buttons[j + 1].BackColor = SystemColors.ControlDark;
                        Swap(ref array[j], ref array[j + 1]);
                        swapFlag = true;
                    }
                    else
                    {
                        buttons[j].BackColor = SystemColors.ControlDark;
                        buttons[j + 1].BackColor = SystemColors.ControlDark;
                    }
                }

                if (!swapFlag)
                {
                    break;
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




        //метод обмена элементов
        static void Swap(ref int value1, ref int value2)
        {
            var temp = value1;
            value1 = value2;
            value2 = temp;
        }

        //метод для генерации следующего шага
        static int GetNextStep(int s)
        {
            s = s - 1;
            return s > 1 ? s : 1;
        }
    }
}
